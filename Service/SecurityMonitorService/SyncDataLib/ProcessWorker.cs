using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncDataLib
{
    public class ProcessWorker
    {
        private NLog.Logger logger;
        public void Start(ConfigSetting setting, NLog.LogFactory logFactory)
        {
            this.logger = logFactory.GetLogger(this.GetType().Name);
            var syncProcess = GetProcessObj(setting.IsDummyData);
            syncProcess.Process(setting, logger);
          
        }

        private SyncProcess GetProcessObj(string isDummyData)
        {
            bool isDummy = false;
            bool result = bool.TryParse(isDummyData, out isDummy);
            if (result && isDummy)
            {
                return new SyncDummyProcess();
            }
            else
            {
                return new SyncFromFileProcess();
            }
        }
    }
}
