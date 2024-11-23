using System;

namespace Framework.Yggdrasil.Service
{
    public interface IServiceInjector : IService
    {
        /// <summary>  
        /// 注册服务  
        /// </summary>
        /// <param name="serviceInterface">服务接口</param>
        /// <param name="implementType">服务实现</param>
        public void Register(Type serviceInterface, Type implementType);

        /// <summary>
        /// 注册服务, 为了避免实例化的过程中的多次反射  
        /// </summary>
        /// <typeparam name="TInterface">服务接口</typeparam>
        /// <typeparam name="T">服务实现</typeparam>
        public void Register<TInterface, T>() where TInterface : IService where T : TInterface;

        /// <summary>
        /// 解注册服务
        /// </summary>
        public void Deregister(Type serviceInterface);

        /// <summary>
        /// 解注册服务
        /// </summary>
        public void Deregister<TInterface>() where TInterface : IService;

        /// <summary>  
        /// 创建或者获取服务  
        /// </summary>   
        /// <returns>服务对象</returns> 
        public T GetInstance<T>() where T : IService;
    }
}