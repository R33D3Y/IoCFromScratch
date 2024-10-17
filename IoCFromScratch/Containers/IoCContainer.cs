using IoCFromScratch.Enums;
using IoCFromScratch.Structs;

namespace IoCFromScratch.Containers
{
    public partial class IoCContainer
    {
        /// <summary>
        /// Dictionary to store the registered types and their lifetimes
        /// </summary>
        private readonly Dictionary<Type, ServiceDescriptor> _types = [];

        /// <summary>
        /// Dictionary to hold singleton instances
        /// </summary>
        private readonly Dictionary<Type, object> _singletons = [];

        /// <summary>
        /// Register a service (interface to concrete class mapping) with a specific lifetime
        /// </summary>
        public void Register<TInterface, TImplementation>(Lifetime lifetime = Lifetime.Transient)
        {
            _types[typeof(TInterface)] = (typeof(TImplementation), lifetime);
        }

        /// <summary>
        /// Register a concrete type with a specific lifetime
        /// </summary>
        public void Register<TImplementation>(Lifetime lifetime = Lifetime.Transient)
        {
            _types[typeof(TImplementation)] = (typeof(TImplementation), lifetime);
        }

        /// <summary>
        /// Resolve a service with additional parameters
        /// </summary>
        public TInterface Resolve<TInterface>(params object[] args)
        {
            return (TInterface)Resolve(typeof(TInterface), args);
        }

        private object Resolve(Type interfaceType, params object[] args)
        {
            if (!_types.ContainsKey(interfaceType))
            {
                throw new Exception($"No type registered for {interfaceType.Name}");
            }

            var serviceDescriptor = _types[interfaceType];

            // Check if it's a singleton and already created
            if (serviceDescriptor.Lifetime == Lifetime.Singleton)
            {
                if (_singletons.ContainsKey(serviceDescriptor.ImplementationType))
                {
                    return _singletons[serviceDescriptor.ImplementationType];  // Return existing singleton
                }
            }

            // Create an instance using the first available constructor
            var constructors = serviceDescriptor.ImplementationType.GetConstructors();
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                var parameterInstances = new List<object>();

                foreach (var parameter in parameters)
                {
                    var arg = args.FirstOrDefault(a => a.GetType() == parameter.ParameterType);

                    if (arg != null)
                    {
                        parameterInstances.Add(arg);  // Use user-supplied argument
                    }
                    else
                    {
                        var parameterInstance = Resolve(parameter.ParameterType);  // Resolve dependencies automatically
                        parameterInstances.Add(parameterInstance);
                    }
                }

                // If the number of resolved parameters matches the constructor's parameters, invoke it
                if (parameterInstances.Count == parameters.Length)
                {
                    var instance = constructor.Invoke(parameterInstances.ToArray());

                    // Store the instance if it's a singleton
                    if (serviceDescriptor.Lifetime == Lifetime.Singleton)
                    {
                        _singletons[serviceDescriptor.ImplementationType] = instance;
                    }

                    return instance;
                }
            }

            throw new Exception($"No matching constructor found for {interfaceType.Name}");
        }
    }
}