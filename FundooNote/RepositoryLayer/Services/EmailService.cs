using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace RepositoryLayer.Services
{
    public class EmailService
    {
        public static void SendEmail(string emailid, string token, string name)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("vaib.manapure9@gmail.com", "rwjwcozadxzaufue");
                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(emailid);
                msgObj.IsBodyHtml = true;
                msgObj.From = new MailAddress("vaib.manapure9@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.Body = "<html><body><p><b> Hi " + $"{name}" + "</b>,<br/>Please click the below link for reset password.<br/>" +
                   $"{token}" +
                   "<br/><br/><br/><b>Thanks&Regards </b><br/><b>Mail Team(donot - reply to this mail) </b ></ p ></ body ></ html>";
                client.Send(msgObj);

            }
        }
    }
}
