using System;
using TaskFlow.CrossCutting.Logging.Interfaces;

namespace TaskFlow.CrossCutting.Logging
{
    public class ConsoleLogger : IConsoleLoggerService
    {
        public void Information(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[INFO] {DateTime.Now}: {message}");
            Console.ResetColor();
        }

        public void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARN] {DateTime.Now}: {message}");
            Console.ResetColor();
        }

        public void Error(string message, Exception ex = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            if (ex == null)
                Console.WriteLine($"[ERROR] {DateTime.Now}: {message}");
            else
            {
                Console.WriteLine($"[ERROR] {DateTime.Now}: {message} - Exception: {ex.Message}");
            }

            Console.ResetColor();
        }

        public void Debug(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[DEBUG] {DateTime.Now}: {message}");
            Console.ResetColor();
        }
    }
}
