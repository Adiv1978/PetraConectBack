using PetraConectBack.Managers.L00;

namespace PetraConectBack.Managers.L10
{
    public class MngLog
    {
        private readonly LogHelper _logHelper;

        public MngLog(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            SettingHelper settingHelper = new SettingHelper(configuration);
            string logPath = settingHelper.GetLogPath();
            _logHelper = new LogHelper(logPath);
        }

        public void WriteInfo(string message)
        {
            _logHelper.WriteInfo(message);
        }

        public void WriteWarning(string message)
        {
            _logHelper.WriteWarning(message);
        }

        public void WriteError(string message)
        {
            _logHelper.WriteError(message);
        }

        public void WriteDebug(string message)
        {
            _logHelper.WriteDebug(message);
        }

        public void WriteException(Exception exception)
        {
            _logHelper.WriteException(exception);
        }
    }
}
