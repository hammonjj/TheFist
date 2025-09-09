using System.Runtime.CompilerServices;

namespace Bitbox.Core.Logging
{
    public interface ILogger
    {
        void Debug(string message,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0);
        void Info(string message,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0);
        void Warning(string message,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0);
        void Error(string message,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0);
    }

}