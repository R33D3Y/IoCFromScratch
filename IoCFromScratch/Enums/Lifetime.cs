namespace IoCFromScratch.Enums
{
    /// <summary>
    /// The lifetime of the Class
    /// </summary>
    public enum Lifetime
    {
        /// <summary>
        /// A new instance each time
        /// </summary>
        Transient,

        /// <summary>
        /// Same instance every time
        /// </summary>
        Singleton
    }
}