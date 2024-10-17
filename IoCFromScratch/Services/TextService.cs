using IoCFromScratch.Interfaces;
using System.Diagnostics;

namespace IoCFromScratch.Services
{
    public class TextService : ITextService
    {
        public void SendText(string message)
        {
            Debug.WriteLine("Sending text: " + message);
        }
    }
}