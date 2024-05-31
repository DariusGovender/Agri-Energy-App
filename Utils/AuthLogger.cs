using System;
using System.IO;
using System.IO.Pipelines;

namespace Agri_Energy_Application.Utils
{
    public class AuthLogger
    {
        public static readonly AuthLogger instance = new AuthLogger();

        private readonly StreamWriter errorFileWriter;
        private readonly StreamWriter loginAttemptsFileWriter;
        
        /*
        private AuthLogger()
        {
            loginAttemptsFileWriter = new StreamWriter("auth_errors.log", true);
            loginAttemptsFileWriter.AutoFlush = true;
        }
        */

        public void LogError(string username)
        {
            //string username = "empty";
            string loginAttemptMessage = $"{DateTime.Now} - FAILED LOGIN ATTEMPT: {username}";
            string logFilePath = Path.Combine("logs", "auth_errors.log");

            using (StreamWriter errorFileWriter = File.AppendText(logFilePath))
            {
                errorFileWriter.WriteLine(loginAttemptMessage);
            }
        }

        public void LogSuccess(string username)
        {
            string loginAttemptMessage = $"{DateTime.Now} - SUCCESSFUL LOGIN ATTEMPT: {username}";
            string logFilePath = Path.Combine("logs", "auth_success.log");

            using (StreamWriter errorFileWriter = File.AppendText(logFilePath))
            {
                errorFileWriter.WriteLine(loginAttemptMessage);
            }
        }

        public void ClearLog()
        {
            // Clear the content of both log files
            errorFileWriter.Flush();
            errorFileWriter.BaseStream.SetLength(0);

            loginAttemptsFileWriter.Flush();
            loginAttemptsFileWriter.BaseStream.SetLength(0);
        }
    }
}

