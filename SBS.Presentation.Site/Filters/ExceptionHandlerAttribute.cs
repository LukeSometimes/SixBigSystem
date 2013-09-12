using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using SBS.Tool.Logging;

namespace SBS.Presentation.Site.Filters
{
    public class ExceptionHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            this.LogError(controllerName, actionName, filterContext.Exception);

            var viewType = this.GetActionViewType(filterContext, actionName);
            if (viewType == typeof(PartialViewResult))
            {
                //filterContext.SessionRepository(site).ErrorInfo = filterContext.Exception;
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new
                    {
                        controller = "PartialError",
                        action = "PartialError"
                    }));
            }
            else if (viewType == typeof(JsonResult))
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { successful = false, error = filterContext.Exception.Message },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            filterContext.ExceptionHandled = true;
        }

        private Type GetActionViewType(Exception exception, string controllerName, string actionName)
        {
            var exceptionStackTrace = new StackTrace(exception);
            var viewMethod = exceptionStackTrace.GetFrames().Select(s => (MethodInfo)s.GetMethod()).
                Where(m => m.Name == actionName && m.DeclaringType == Type.GetType(string.Format("SBS.Presentation.Site.Controllers.{0}Controller", controllerName))).FirstOrDefault();
            return viewMethod.ReturnType;
        }

        private Type GetActionViewType(ControllerContext context, string actionName)
        {
            var controllerDescriptor = new ReflectedControllerDescriptor(context.Controller.GetType());
            var actionDescriptor = (System.Web.Mvc.ReflectedActionDescriptor)controllerDescriptor.FindAction(context, actionName);
            return actionDescriptor.MethodInfo.ReturnType;
        }

        private void LogError(string controllerName, string actionName, Exception exception)
        {
            var logger = Logger.GetLogger("Global");
            logger.Error(string.Format("An Error Occurred on Controller : {0}, Action : {1}", controllerName, actionName));
            logger.Error("Exception Trace:", exception);
        }
    }
}
