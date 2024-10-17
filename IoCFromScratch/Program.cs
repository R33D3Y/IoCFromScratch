using IoCFromScratch.Container;
using IoCFromScratch.Interfaces;
using IoCFromScratch.Services;

namespace IoCFromScratch
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create IoC container
            var container = new IoCContainer();

            // Register interface to concrete class mapping
            container.Register<IEmailService, EmailService>();

            // Register concrete types without an interface
            container.Register<NotificationService>();

            // Resolve NotificationService
            var notificationService = container.Resolve<NotificationService>();

            // Use the service
            notificationService.Notify("Hello from IoC Container!");
        }
    }
}