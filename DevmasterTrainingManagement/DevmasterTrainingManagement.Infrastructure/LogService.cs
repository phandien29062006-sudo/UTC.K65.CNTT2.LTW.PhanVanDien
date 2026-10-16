using System.Text;

namespace DevmasterTrainingManagement.Infrastructure
{
    public class LogService
    {
        private readonly string logFile;

        public LogService(string logFile = "error.log")
        {
            this.logFile = logFile;
        }

        public bool LogError(string message)
        {
            try
            {
                string? folder =
                    Path.GetDirectoryName(logFile);

                if (!string.IsNullOrEmpty(folder) &&
                    !Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string log =
                    $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] " +
                    $"ERROR: {message}";

                File.AppendAllText(
                    logFile,
                    log + Environment.NewLine,
                    Encoding.UTF8);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool LogInfo(string message)
        {
            try
            {
                string? folder =
                    Path.GetDirectoryName(logFile);

                if (!string.IsNullOrEmpty(folder) &&
                    !Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string log =
                    $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] " +
                    $"INFO: {message}";

                File.AppendAllText(
                    logFile,
                    log + Environment.NewLine,
                    Encoding.UTF8);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}