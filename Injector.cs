using System;
using UnityEngine;

namespace Framework.Yggdrasil
{
    public static class Injector
    {
        public static bool Initialized { get; private set; }
        public static IServiceInjector Instance { get; private set; }

        public static T GetService<T>() where T : IService
        {
            if (Instance == null)
            {
                throw new NullReferenceException("IServiceInjector is null");
            }

            return Instance.GetService<T>();
        }

        public static void SetInjector<T>() where T : IServiceInjector, new()
        //Injector是第一个加载的Service，要求不依赖其他任何服务，可以直接new一个对象
        {
#if UNITY_EDITOR
            //通常情况下只会在项目启动时调用一次
            if (Initialized)
                Debug.LogWarning("Injector is already initialized");
#endif
            Initialized = true;
            Instance?.OnRemove();

            var instance = new T();
            Instance = instance;

            Instance.OnAdd();
        }
    }
}