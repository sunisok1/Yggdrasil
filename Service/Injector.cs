using System;

namespace Core.Service
{
    public static class Injector
    {
        private static IServiceInjector? Instance { get; set; }

        public static void SetInjector(IServiceInjector? injector)
        {
            Instance?.OnDestroy();

            Instance = injector;

            Instance?.OnStart();
        }

        public static T GetService<T>() where T : IService
        {
            if (Instance == null)
            {
                throw new NullReferenceException("IServiceInjector is null");
            }

            return Instance.GetInstance<T>();
        }
    }
}