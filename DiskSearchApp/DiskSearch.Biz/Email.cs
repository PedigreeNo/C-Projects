using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DiskSearch.Biz
{
    public class Email
    {
        public Task Send(string username, string passwort)
        {
          return Task.Run(() =>
            {
                try
                {
                    var smtpServer = new SmtpClient("smtp.gmail.com")
                    {
                        EnableSsl = true,
                        Port = 587,
                        Credentials = new NetworkCredential(username, passwort)
                    };
                    var mail = new MailMessage()
                    {
                        From = new MailAddress(username),
                        Subject = "Greetings",
                        Body = "Hello"
                    };
                    var attachment = new Attachment("D:\\TextWriter\\Abbild.txt");
                    mail.Attachments.Add(attachment);
                    mail.To.Add(username);
                    smtpServer.Send(mail);
                    attachment.Dispose();
                    smtpServer.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }
    }
}