using System;
using System.Collections.Generic;

namespace MyRedditApp.Helpers
{
    /// <summary>
    ///     Simple ServiceLocator implementation.
    ///     sealed class: is to indicate that the class is specialized and there is no need 
    ///     to extend it to provide any additional functionality through inheritance to override its behavior. 
    ///     A sealed class is often used to encapsulate a logic that needs to be used across the program 
    ///     but without any alteration to it. 
    /// </summary>
    public sealed class ServiceLocator
    {
        static readonly Lazy<ServiceLocator> instance = new Lazy<ServiceLocator>(() => new ServiceLocator());
        readonly Dictionary<Type, Lazy<object>> registeredServices = new Dictionary<Type, Lazy<object>>();

        /// <summary>
        ///     Singleton instance for default service locator
        /// </summary>
        public static ServiceLocator Instance => instance.Value;

        /// <summary>
        ///     Add a new contract + service implementation
        /// </summary>
        /// <typeparam name="TContract">Contract type</typeparam>
        /// <typeparam name="TService">Service type</typeparam>
        public void Register<TContract, TService>() where TService : new()
        {
            registeredServices[typeof(TContract)] =
                new Lazy<object>(() => Activator.CreateInstance(typeof(TService)));
        }

        /// <summary>
        ///     This resolves a service type and returns the implementation. Note that this
        ///     assumes the key used to register the object is of the appropriate type or
        ///     this method will throw an InvalidCastException!
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>Implementation</returns>
        public T Get<T>() where T : class
        {
            Lazy<object> service;
            if (registeredServices.TryGetValue(typeof(T), out service))
            {
                return (T)service.Value;
            }

            return null;
        }
    }
}

