using Microsoft.Extensions.Logging;
using System;

namespace ShoppingProject.Data.Logging
{
    /// <summary>
    /// This type eliminates the need to depend directly on the ASP.NET Core logging types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAppLogger<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void Log(LogLevel logLevel, Exception exception, string message, params object[] args);
    }
}
