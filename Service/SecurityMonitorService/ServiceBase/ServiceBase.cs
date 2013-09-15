using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;

namespace SixBigSystem.Service.ServiceBase
{
    public class ServiceBase : System.ServiceProcess.ServiceBase
    {
        private NLog.Logger logger;
        private NLog.LogFactory logFactory;

        protected NLog.Logger Log
        {
            get { return this.logger; }
        }

        protected NLog.LogFactory LogFactory
        {
            get { return this.logFactory; }
        }

        public virtual string GetConfigFileName()
        {
            return this.GetType().Assembly.Location + ".config";
        }

        public ServiceBase()
        {
            this.logFactory = new SixBigSystem.Service.Log.LogFactory(GetConfigFileName()).NLogFactory;

            this.logger = this.LogFactory.GetLogger(this.GetType().Name);
        }

    }
}
