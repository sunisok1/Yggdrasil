using System;
using System.Collections.Generic;

namespace Framework.Yggdrasil.Services
{
    public interface IConfig
    {
    }

    public interface IConfigList<out T> : IConfig where T : class
    {
        IEnumerable<T> Values { get; }
    }

    public interface IConfigService : IService
    {
        T GetConfig<T>() where T : IConfig;
        void SetConfig<T>(T config) where T : IConfig;
        void DeleteConfig<T>() where T : IConfig;
        void LoadConfig<T>(string path) where T : IConfig;
        void SaveConfig<T>(string path) where T : IConfig;
        void RefreshConfigs();
        IEnumerable<IConfig> ListAllConfigs();
        bool HasConfig<T>();
        void AddConfigListener<T>(Action<T> listener) where T : IConfig;
        void RemoveConfigListener<T>(Action<T> listener) where T : IConfig;
    }
}