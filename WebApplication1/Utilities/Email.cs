using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplication1.Utilities
{
    public class Email
    {
        public Task SendAsync(string to, string cc, string subject, string body, List<string> attachments = null)
        {
            // Plug in your email service here to send an email.
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("cricketgoldfinch@gmail.com");
                mail.To.Add(to);
                if (cc != null)
                    mail.CC.Add(cc);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        mail.Attachments.Add(new Attachment(attachment));
                    }
                }
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("cricketgoldfinch@gmail.com", "goldfinch@123");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                return Task.FromResult(0);
            }
        }
    }
}