using IoCFromScratch.Interfaces;

namespace IoCFromScratch.Services
{
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Count of Emails sent
        /// </summary>
        private int EmailCount { get; set; } = 0;

        public void SendEmail(string message)
        {
            Console.WriteLine($"Sending email [{EmailCount}]: " + message);
            EmailCount++;
        }
    }
}