using IoCFromScratch.Interfaces;

namespace IoCFromScratch.Services
{
    public class NotificationService
    {
        private readonly IEmailService _emailService;

        public NotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void Notify(string message)
        {
            _emailService.SendEmail(message);
        }
    }
}