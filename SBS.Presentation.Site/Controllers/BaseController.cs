using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SBS.Presentation.Site.Filters;
using SBS.Tool.Logging;

namespace SBS.Presentation.Site.Controllers
{
    [ExceptionHandler]
    public class BaseController : Controller
    {
        protected Logger Logger
        {
            get
            {
                return Logger.GetLogger("Controller");
            }
        }

    }
}
