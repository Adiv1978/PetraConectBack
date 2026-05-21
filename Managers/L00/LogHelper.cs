using System.Text;

namespace PetraConectBack.Managers.L00
{
    public class LogHelper
    {
        private readonly string _logPath;
        private static readonly object _lockObject = new object();

        public LogHelper(string logPath)
        {
            if (string.IsNullOrWhiteSpace(logPath))
            {
                throw new ArgumentException("La ruta para almacenar el log no puede estar vacía.", nameof(logPath));
            }

            _logPath = logPath.Trim();
        }

        public void WriteInfo(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            string directoryPath = _logPath;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = "log" + DateTime.Now.ToString("ddMMyy_HH") + ".log";
            string fullPath = Path.Combine(directoryPath, fileName);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("--------------------------------------------------");
            builder.AppendLine("Fecha   : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            builder.AppendLine("Tipo    : INFO");
            builder.AppendLine("Mensaje : " + message.Trim());
            builder.AppendLine("--------------------------------------------------");

            lock (_lockObject)
            {
                File.AppendAllText(fullPath, builder.ToString(), Encoding.UTF8);
            }
        }

        public void WriteWarning(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            string directoryPath = _logPath;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = "log" + DateTime.Now.ToString("ddMMyy_HH") + ".log";
            string fullPath = Path.Combine(directoryPath, fileName);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("--------------------------------------------------");
            builder.AppendLine("Fecha   : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            builder.AppendLine("Tipo    : WARNING");
            builder.AppendLine("Mensaje : " + message.Trim());
            builder.AppendLine("--------------------------------------------------");

            lock (_lockObject)
            {
                File.AppendAllText(fullPath, builder.ToString(), Encoding.UTF8);
            }
        }

        public void WriteError(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            string directoryPath = _logPath;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = "log" + DateTime.Now.ToString("ddMMyy_HH") + ".log";
            string fullPath = Path.Combine(directoryPath, fileName);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("--------------------------------------------------");
            builder.AppendLine("Fecha   : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            builder.AppendLine("Tipo    : ERROR");
            builder.AppendLine("Mensaje : " + message.Trim());
            builder.AppendLine("--------------------------------------------------");

            lock (_lockObject)
            {
                File.AppendAllText(fullPath, builder.ToString(), Encoding.UTF8);
            }
        }

        public void WriteException(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            string directoryPath = _logPath;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = "log" + DateTime.Now.ToString("ddMMyy_HH") + ".log";
            string fullPath = Path.Combine(directoryPath, fileName);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("--------------------------------------------------");
            builder.AppendLine("Fecha      : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            builder.AppendLine("Tipo       : EXCEPTION");
            builder.AppendLine("Mensaje    : " + exception.Message);
            builder.AppendLine("Origen     : " + exception.Source);
            builder.AppendLine("Metodo     : " + exception.TargetSite);
            builder.AppendLine("StackTrace : ");
            builder.AppendLine(exception.StackTrace);

            if (exception.InnerException != null)
            {
                builder.AppendLine("InnerException Mensaje    : " + exception.InnerException.Message);
                builder.AppendLine("InnerException StackTrace : ");
                builder.AppendLine(exception.InnerException.StackTrace);
            }

            builder.AppendLine("--------------------------------------------------");

            lock (_lockObject)
            {
                File.AppendAllText(fullPath, builder.ToString(), Encoding.UTF8);
            }
        }

        public void WriteDebug(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            string directoryPath = _logPath;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = "log" + DateTime.Now.ToString("ddMMyy_HH") + ".log";
            string fullPath = Path.Combine(directoryPath, fileName);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("--------------------------------------------------");
            builder.AppendLine("Fecha   : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            builder.AppendLine("Tipo    : DEBUG");
            builder.AppendLine("Mensaje : " + message.Trim());
            builder.AppendLine("--------------------------------------------------");

            lock (_lockObject)
            {
                File.AppendAllText(fullPath, builder.ToString(), Encoding.UTF8);
            }
        }
    }
}
