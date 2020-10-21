using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MailHelper
    {
        public void SendMail(string toEmailAddress, string subject, string content)
        {
            string fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            string fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            string fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            string smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            string smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
            
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);//Usser và password của quảng trị
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
            

        }
    }
}
