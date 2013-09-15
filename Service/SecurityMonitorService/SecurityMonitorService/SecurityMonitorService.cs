using SyncDataLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace SecurityMonitorService
{
    public partial class SecurityMonitorService : SixBigSystem.Service.ServiceBase.ServiceBase
    {
        private System.Timers.Timer synctimer = null;
        public SecurityMonitorService()
        {
            InitializeComponent();

            synctimer = new System.Timers.Timer();
            synctimer.Interval = 15000;
            synctimer.Elapsed += synctimer_Elapsed;
        }

        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "-c")
            {
                SecurityMonitorService svc = new SecurityMonitorService();
                svc.OnStart(args);
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
                return;
            }

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new SecurityMonitorService() 
            };

            ServiceBase.Run(ServicesToRun);
        }

        protected override void OnStart(string[] args)
        {
            if (!string.IsNullOrEmpty(ConfigSetting.configSetting.Interval))
            {
                int interval = 15000;
                if (!int.TryParse(ConfigSetting.configSetting.Interval, out interval)) { interval = 15000; }
                this.synctimer.Interval = interval;
            }

            this.synctimer.Enabled = true;
            this.Log.Info("OnSart 服务已经启动");
        }

        private void DebugStart()
        {

        }

        protected override void OnStop()
        {
            this.synctimer.Enabled = false;
            this.Log.Info("OnSart 服务已经停止");
        }

        private void synctimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var worker = new ProcessWorker();
            worker.Start(ConfigSetting.configSetting, this.LogFactory);
        }
    }
}
