namespace IoCFromScratch.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Send an Email with supplied message
        /// </summary>
        /// <param name="message">Message to send</param>
        public void SendEmail(string message);
    }
}