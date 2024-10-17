namespace IoCFromScratch.Container
{
    public class IoCContainer
    {
        private readonly Dictionary<Type, Type> _types = [];

        // Register a service (interface to concrete class mapping)
        public void Register<TInterface, TImplementation>()
        {
            _types[typeof(TInterface)] = typeof(TImplementation);
        }

        // Register concrete types without an interface
        public void Register<TImplementation>()
        {
            _types[typeof(TImplementation)] = typeof(TImplementation);
        }

        // Resolve a service
        public TInterface Resolve<TInterface>()
        {
            return (TInterface)Resolve(typeof(TInterface));
        }

        private object Resolve(Type interfaceType)
        {
            if (!_types.TryGetValue(interfaceType, out Type? implementationType))
            {
                throw new Exception($"No type registered for {interfaceType.Name}");
            }

            // Get constructor info
            var constructorInfo = implementationType.GetConstructors()[0];
            var parameters = constructorInfo.GetParameters();

            // If the constructor has no parameters, create the instance
            if (parameters.Length == 0)
            {
                return Activator.CreateInstance(implementationType);
            }

            // If there are parameters, resolve them recursively
            var parameterInstances = new List<object>();
            foreach (var parameter in parameters)
            {
                var parameterInstance = Resolve(parameter.ParameterType);
                parameterInstances.Add(parameterInstance);
            }

            // Create the instance with resolved parameters
            return constructorInfo.Invoke(parameterInstances.ToArray());
        }
    }
}
