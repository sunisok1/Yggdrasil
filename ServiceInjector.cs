using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Yggdrasil
{
    public class ServiceInjector : IServiceInjector
    {
        private readonly Dictionary<Type, IService> _services = new();

        public void OnAdd()
        {
            _services.Add(typeof(IServiceInjector), this);
        }

        public void OnRemove()
        {
        }

        public void Register(Type serviceInterface, Type implementType)
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
                        if (_services.TryGetValue(parameter.ParameterType, out var service))
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
            if (_services.ContainsKey(serviceInterface))
            {
                Deregister(serviceInterface);
            }

            _services.Add(serviceInterface, serviceInstance);
        }

        public void Register<TInterface, T>() where TInterface : IService where T : TInterface => Register(typeof(TInterface), typeof(T));

        public void Deregister(Type serviceInterface)
        {
            if (!_services.TryGetValue(serviceInterface, out var service))
            {
                Console.WriteLine($"Warning: Service of type {serviceInterface.FullName} is either not registered or has already been deregistered.");
                return;
            }

            service.OnRemove();
            _services.Remove(serviceInterface);
        }

        public void Deregister<TInterface>() where TInterface : IService => Deregister(typeof(TInterface));


        public T GetService<T>() where T : IService
        {
            if (_services.TryGetValue(typeof(T), out var serviceBase))
                return (T)serviceBase;
            throw new NullReferenceException($"service {typeof(T)} is null");
        }
    }
}