using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Linq;

namespace Ludo.Service
{
    public sealed class ServiceCollection
    {
        private readonly Dictionary<Type, Func<object>> _services = new Dictionary<Type, Func<object>>();

        public ServiceCollection AddSingleton<T>()
        {
            object obj = null;
            _services[typeof(T)] = () =>
            {
                if (obj == null)
                {
                    obj = CreateService<T>();
                }
                return obj;
            };
            return this;
        }

        public ServiceCollection AddSingleton<T>(T value)
        {
            Debug.Assert(value != null);
            _services[typeof(T)] = () => value;

            return this;
        }

        public ServiceCollection AddScope<T>()
        {
            _services[typeof(T)] = () => CreateService<T>();
            return this;
        }

        public T CreateService<T>() => (T)CreateService(typeof(T));

        public object GetService(Type type) => _services[type].Invoke();

        public T GetService<T>() => (T)GetService(typeof(T));

        public object CreateService(Type serviceType)
        {
            var constructors = serviceType.GetConstructors();

            var injectConstructor = constructors.Where(c => c.GetCustomAttributes(typeof(InjectAttribute), false).Any()).FirstOrDefault();
            if (injectConstructor == null)
            {
                injectConstructor = constructors
                    .OrderByDescending(c => c.GetParameters().Length)
                    .FirstOrDefault();
            }
            Debug.Assert(injectConstructor != null);
            var parameters = injectConstructor.GetParameters();
            object[] arguments = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (_services.TryGetValue(parameters[i].ParameterType, out var creator))
                {

                    arguments[i] = creator();
                }
            }

            return Activator.CreateInstance(serviceType, arguments);
        }
    }
}
