using IoCFromScratch.Interfaces;
using System.Diagnostics;

namespace IoCFromScratch.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string message)
        {
            Debug.WriteLine("Sending email: " + message);
        }
    }
}