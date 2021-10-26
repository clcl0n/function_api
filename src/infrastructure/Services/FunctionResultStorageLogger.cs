using System;
using System.IO;
using Infrastructure.Models;

namespace Infrastructure.Services
{
    public class FunctionResultStorageLogger
    {
        private readonly string _logsFolderPath;
        private readonly string _logFileName;
        private readonly string _logFilePath;
        private readonly object _locker = new Object();

        public FunctionResultStorageLogger(FunctionResultLoggingConfiguration functionResultLoggingConfiguration) {
            _logFileName = $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}.calculation_result.log";
            _logsFolderPath = functionResultLoggingConfiguration.FolderPath;
            if (!Directory.Exists(_logsFolderPath))
            {
                Directory.CreateDirectory(_logsFolderPath);
            }
            _logFilePath = Path.Join(_logsFolderPath, _logFileName);
        }

        public void Write(double input, double computedValue, long created)
        {
            lock (_locker)
            {
                using (StreamWriter sw = File.AppendText(_logFilePath))
                {
                    sw.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")}] {{ input: {input}, computed_value: {computedValue}, timestamp: {created} }}");
                }
            }
        }
    } 
}