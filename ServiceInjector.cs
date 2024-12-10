using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Yggdrasil
{
    public class ServiceInjector : IServiceInjector
    {
        //用接口注册的服务
        private readonly Dictionary<Type, IService> m_serviceWithInterface = new();

        //直接注册的服务
        private readonly Dictionary<Type, IService> m_serviceWithType = new();

        public void OnAdd()
        {
            m_serviceWithInterface.Add(typeof(IServiceInjector), this);
        }

        public object Register(Type serviceInterface, Type implementType)
        {
            var constructor = implementType.GetConstructors().FirstOrDefault(c => c.GetCustomAttribute<ServiceConstructorAttribute>() != null);
            IService serviceInstance;
            if (constructor != null)
            {
                var parameters = constructor.GetParameters();
                var parameterInstances = new object[parameters.Length];
                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    // 如果参数类型是IService类型
                    if (typeof(IService).IsAssignableFrom(parameter.ParameterType))
                    {
                        // 检查_services字典中是否存在该服务
                        if (m_serviceWithInterface.TryGetValue(parameter.ParameterType, out var service))
                        {
                            // 如果存在，获取服务实例
                            parameterInstances[i] = service;
                        }
                        else
                        {
                            // 如果不存在，抛出异常
                            throw new InvalidOperationException($"Service of type {parameter.ParameterType.FullName} is not registered.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Parameter {parameter.Name} of type {parameter.ParameterType.FullName} is not an IService type.");
                    }
                }

                // 使用找到的构造函数和参数来创建服务实例
                serviceInstance = constructor.Invoke(parameterInstances) as IService ?? throw new InvalidOperationException();
            }
            else
            {
                serviceInstance = Activator.CreateInstance(implementType) as IService ?? throw new InvalidOperationException();
            }

            serviceInstance.OnAdd();
            if (m_serviceWithInterface.ContainsKey(serviceInterface))
            {
                Deregister(serviceInterface);
            }

            m_serviceWithInterface.Add(serviceInterface, serviceInstance);
            return serviceInstance;
        }

        public T Register<TInterface, T>() where TInterface : IService where T : TInterface => (T)Register(typeof(TInterface), typeof(T));

        public void Register(Type implementType, object impl)
        {
            if (impl == null) return;
            if (m_serviceWithType.TryGetValue(implementType, out var service))
                service.OnRemove();
            m_serviceWithType[implementType] = impl as IService;
        }

        public void Register<T>(T impl) where T : IService => Register(typeof(T), impl);

        public void Deregister(Type serviceInterface)
        {
            if (!m_serviceWithInterface.TryGetValue(serviceInterface, out var service) && !m_serviceWithType.TryGetValue(serviceInterface, out service))
            {
                Console.WriteLine($"Warning: Service of type {serviceInterface.FullName} is either not registered or has already been deregistered.");
                return;
            }

            service.OnRemove();
            m_serviceWithInterface.Remove(serviceInterface);
        }

        public void Deregister<TInterface>() where TInterface : IService => Deregister(typeof(TInterface));


        public T GetService<T>() where T : IService
        {
            var type = typeof(T);
            if (m_serviceWithInterface.TryGetValue(type, out var service))
                return (T)service;
            if (m_serviceWithType.TryGetValue(type, out service))
                return (T)service;
            throw new NullReferenceException($"service {type} is null");
        }
    }
}