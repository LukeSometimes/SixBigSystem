using System.Configuration;
using System.Collections.Generic;

namespace SixBigSystem.Service.Log
{
    public interface ILogFactory
    {
        NLog.LogFactory NLogFactory { get; }
        NLog.Logger GetLogger(string name);
    }

    public class LogFactory : ILogFactory
    {
        public static readonly object lockObject = new object();
        public static Dictionary<string, NLog.LogFactory> logFactories =
            new Dictionary<string, NLog.LogFactory>(System.StringComparer.OrdinalIgnoreCase);

        private string serviceRunnerLogPath;
        private NLog.LogFactory nlogFactory;

        public LogFactory(string configFilename)
        {
            //
            // load ServiceRunner.exe.config
            //           
            var serviceRunnerConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (string.IsNullOrEmpty(configFilename))
                configFilename = serviceRunnerConfig.FilePath;

            //
            // get log location configuration
            //
            var logPathSetting = serviceRunnerConfig.AppSettings.Settings["logPath"];
            serviceRunnerLogPath = logPathSetting != null ? logPathSetting.Value : string.Empty;
            if (!string.IsNullOrEmpty(serviceRunnerLogPath) && !serviceRunnerLogPath.EndsWith("\\"))
                serviceRunnerLogPath += '\\';

            lock (lockObject)
            {
                if (logFactories.TryGetValue(configFilename, out nlogFactory))
                    return;
            }

            //
            // load NLog settings
            //
            var nlogConfig = new NLog.Config.XmlLoggingConfiguration(configFilename, false);
            UpdateNLogConfiguration(nlogConfig, serviceRunnerLogPath);

            nlogFactory = new NLog.LogFactory(nlogConfig);

            nlogFactory.ConfigurationReloaded += logFactory_ConfigurationReloaded;

            lock (lockObject)
            {
                if (!logFactories.ContainsKey(configFilename))
                    logFactories[configFilename] = nlogFactory;
            }
        }

        public NLog.LogFactory NLogFactory
        {
            get { return this.nlogFactory; }
        }

        public NLog.Logger GetLogger(string name)
        {
            return this.NLogFactory.GetLogger(name);
        }

        public static void UpdateNLogConfiguration(NLog.Config.LoggingConfiguration nlogConfig, string serviceRunnerLogPath)
        {
            if (nlogConfig.ConfiguredNamedTargets != null)
            {
                foreach (var t in nlogConfig.ConfiguredNamedTargets)
                {
                    var fileTarget = t as NLog.Targets.FileTarget;
                    if (fileTarget == null && t is NLog.Targets.Wrappers.WrapperTargetBase)
                        fileTarget = ((NLog.Targets.Wrappers.WrapperTargetBase)t).WrappedTarget as NLog.Targets.FileTarget;

                    if (fileTarget != null)
                    {
                        var filename = fileTarget.FileName.ToString();
                        filename = System.Text.RegularExpressions.Regex.Replace(filename, "%LOGPATH%", serviceRunnerLogPath, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        var tempFolder = System.IO.Path.GetTempPath();
                        filename = System.Text.RegularExpressions.Regex.Replace(filename, "%TEMP%", tempFolder, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        // Trim ' since it's part of the name when reading it
                        fileTarget.FileName = filename.Trim('\'');
                    }
                }
            }
        }

        private void logFactory_ConfigurationReloaded(object sender, NLog.Config.LoggingConfigurationReloadedEventArgs e)
        {
            UpdateNLogConfiguration(this.NLogFactory.Configuration, this.serviceRunnerLogPath);
        }
    }
}