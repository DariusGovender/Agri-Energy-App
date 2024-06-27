using System;
using System.IO;
using System.IO.Pipelines;

namespace Agri_Energy_Application.Utils
{
    public class AuthLogger
    {
        // Singleton instance
        public static readonly AuthLogger instance = new AuthLogger();

        private readonly StreamWriter errorFileWriter;
        private readonly StreamWriter loginAttemptsFileWriter;

        //Logs a failed login attempt into a textfile
        public void LogError(string username)
        {
            string loginAttemptMessage = $"{DateTime.Now} - FAILED LOGIN ATTEMPT: {username}";
            string logFilePath = Path.Combine("logs", "auth_errors.log");

            using (StreamWriter errorFileWriter = File.AppendText(logFilePath))
            {
                errorFileWriter.WriteLine(loginAttemptMessage);
            }
        }

        //Logs a successful login attempt into a textfile
        public void LogSuccess(string username)
        {
            string loginAttemptMessage = $"{DateTime.Now} - SUCCESSFUL LOGIN ATTEMPT: {username}";
            string logFilePath = Path.Combine("logs", "auth_success.log");

            using (StreamWriter errorFileWriter = File.AppendText(logFilePath))
            {
                errorFileWriter.WriteLine(loginAttemptMessage);
            }
        }

        // Clear the content of both log files
        public void ClearLog()
        {
            errorFileWriter.Flush();
            errorFileWriter.BaseStream.SetLength(0);

            loginAttemptsFileWriter.Flush();
            loginAttemptsFileWriter.BaseStream.SetLength(0);
        }
    }
}

