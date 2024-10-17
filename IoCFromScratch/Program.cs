using IoCFromScratch.Containers;
using IoCFromScratch.Enums;
using IoCFromScratch.Interfaces;
using IoCFromScratch.Services;

namespace IoCFromScratch
{
    public class Program
    {
        public static void Main()
        {
            // Create IoC container
            var container = new IoCContainer();

            // Register services
            container.Register<IEmailService, EmailService>(Lifetime.Singleton);
            container.Register<NotificationService>(); // Transient by default

            // Resolve the same GuidService multiple times to show it's a singleton
            var notificationService1 = container.Resolve<NotificationService>();
            var notificationService2 = container.Resolve<NotificationService>();

            notificationService1.Notify("Hello from IoC Container!"); // Should output 0
            notificationService2.Notify("Hello from IoC Container!"); // Should output 1
        }
    }
}