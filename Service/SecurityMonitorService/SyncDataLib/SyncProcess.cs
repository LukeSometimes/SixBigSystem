using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncDataLib
{
    public abstract class SyncProcess
    {
        private NLog.Logger logger;
        public virtual void Process(ConfigSetting inputData, NLog.Logger logger)
        {
            this.logger = logger;
            LoadData();
            ResolveData();
            ImportData();
        }

        public abstract void LoadData();
        public abstract void ResolveData();
        public abstract void ImportData();
    }
}
