using System;
using System.Net.Mail;

namespace HeySavings.Models
{
    public class SendEmail
    {
        public SendEmail()
        {
        }
       public static string SendMail(string email)
        {
            string Otp = GenerateRandomNo().ToString();
            try
            {
                 
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("heysavingsapp@gmail.com");
                mail.To.Add(email);
             
                mail.Subject = "HeySavings - Codul tau de verificare";
                string htmlString = @"<html><body><p>Dear HeySavings User,</p><p>Codul tau de autentificare este: "+Otp+"<p>From,<br>-HeySavings</br></p></body></html>";
                mail.IsBodyHtml = true;
                mail.Body = htmlString;
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("heysavingsapp@gmail.com", "qxhbpzvdnwamqsrx");

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                return null;
            }
            return Otp;
        }

        //Generate RandomNo
        static int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

    }
}
