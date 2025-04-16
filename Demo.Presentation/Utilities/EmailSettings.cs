using System.Net;
using System.Net.Mail;

namespace Demo.Presentation.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Cliend = new SmtpClient("smtp.gmail.com", 587);    
            Cliend.EnableSsl = true;
            Cliend.Credentials= new NetworkCredential("mahmoudatef057@gmail.com", "uxmuhktrkdsvjupo");
            Cliend.Send("mahmoudatef057@gmail.com",email.To,email.Subject,email.Body);
        }
    }
}
