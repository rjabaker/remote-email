using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace remote_email
{
    public class RemoteChannel
    {
        public RemoteChannel(byte channelCode)
        {
            ChannelCode = channelCode;
        }

        public byte ChannelCode { get; set; }

        public void SendEmail(System.Net.NetworkCredential credentials, List<MailAddress> sendTo, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(credentials.UserName);

                foreach(MailAddress address in sendTo) mail.To.Add(address);
                mail.Subject = subject;
                mail.Body = body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = credentials;
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
