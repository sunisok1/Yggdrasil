using System;

namespace Framework.Yggdrasil
{
    public static class Injector
    {
        public static bool Initialized { get; private set; }
        private static IServiceInjector Instance { get; set; }

        public static T GetService<T>() where T : IService
        {
            if (Instance == null)
            {
                throw new NullReferenceException("IServiceInjector is null");
            }

            return Instance.GetService<T>();
        }

        public static T SetInjector<T>() where T : IServiceInjector, new()
        {
            Initialized = true;
            Instance?.OnRemove();

            var instance = new T();
            Instance = instance;

            Instance.OnAdd();
            return instance;
        }
    }
}