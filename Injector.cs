using System;

namespace Framework.Yggdrasil
{
    public static class Injector
    {
        private static IServiceInjector Instance { get; set; }

        public static T SetInjector<T>()where T:IServiceInjector,new()
        {
            Instance?.OnDestroy();

            var instance = new T();
            Instance = instance;

            Instance.OnStart();
            return instance;
        }

        public static T GetService<T>() where T : IService
        {
            if (Instance == null)
            {
                throw new NullReferenceException("IServiceInjector is null");
            }

            return Instance.GetService<T>();
        }
    }
}