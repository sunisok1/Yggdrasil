using System;

namespace Framework.Yggdrasil.Services
{
    public interface ILoggerService : IService
    {
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogException(Exception exception);
    }
}