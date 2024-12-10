using System;

namespace Framework.Yggdrasil
{
    public interface IServiceInjector : IService
    {
        /// <summary>  
        /// 注册服务  
        /// </summary>
        /// <param name="serviceInterface">服务接口</param>
        /// <param name="implementType">服务实现</param>
        object Register(Type serviceInterface, Type implementType);

        /// <summary>
        /// 注册服务, 为了避免实例化的过程中的多次反射  
        /// </summary>
        /// <typeparam name="TInterface">服务接口</typeparam>
        /// <typeparam name="T">服务实现</typeparam>
        T Register<TInterface, T>() where TInterface : IService where T : TInterface;

        void Register(Type implementType, object impl);
        void Register<T>(T impl) where T : IService;

        /// <summary>
        /// 解注册服务
        /// </summary>
        void Deregister(Type serviceInterface);

        /// <summary>
        /// 解注册服务
        /// </summary>
        void Deregister<TInterface>() where TInterface : IService;

        /// <summary>  
        /// 创建或者获取服务  
        /// </summary>   
        /// <returns>服务对象</returns> 
        T GetService<T>() where T : IService;
    }
}