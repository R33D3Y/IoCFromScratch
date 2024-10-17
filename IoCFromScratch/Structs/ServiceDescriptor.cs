using IoCFromScratch.Enums;

namespace IoCFromScratch.Structs
{
    /// <summary>
    /// Represents a descriptor for a service, containing its implementation type and lifetime scope.
    /// </summary>
    /// <param name="ImplementationType">The concrete type that implements the service or interface.</param>
    /// <param name="Lifetime">The lifetime of the service, indicating whether it is transient or singleton.</param>
    internal record struct ServiceDescriptor(Type ImplementationType, Lifetime Lifetime)
    {
        /// <summary>
        /// Implicitly converts a <see cref="ServiceDescriptor"/> to a tuple of <see cref="Type"/> and <see cref="Lifetime"/>.
        /// </summary>
        /// <returns>A tuple containing the implementation type and lifetime.</returns>
        public static implicit operator (Type implementationType, Lifetime lifetime)(ServiceDescriptor value)
        {
            return (value.ImplementationType, value.Lifetime);
        }

        /// <summary>
        /// Implicitly converts a tuple of <see cref="Type"/> and <see cref="Lifetime"/> to a <see cref="ServiceDescriptor"/>.
        /// </summary>
        /// <returns>A new <see cref="ServiceDescriptor"/> initialized with the provided tuple values.</returns>
        public static implicit operator ServiceDescriptor((Type implementationType, Lifetime lifetime) value)
        {
            return new ServiceDescriptor(value.implementationType, value.lifetime);
        }
    }
}