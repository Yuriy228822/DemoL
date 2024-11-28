using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DemoL.Model
{
    public class Logger
    {
        private static readonly string LogFilePath = "logs.txt";

        public static void Log(string message, params object[] args)
        {
            try
            {
                string logMessage = $"{DateTime.Now:G} = {message}{Environment.NewLine}";
                File.AppendAllText(LogFilePath,logMessage);            
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка записи логов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
