using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

using ArduinoUtilities;

namespace remote_email
{
    public class RemoteChannel : IPinMapping
    {
        public RemoteChannel(byte channelCode)
        {
            ChannelCode = channelCode;
            PinNumber = -1;
            Description = string.Empty;
            ListeningForResponsePackage = true;
        }

        public int PinNumber { get; set; }
        public string Description { get; set; }
        public bool ListeningForResponsePackage { get; set; }
        public byte ChannelCode { get; set; }

        public ArduinoPinUtilities.SetPinEventHandler SetPinEventHandler { get; set; }
        public SerialPortUtilities.ResponsePackageRecievedEventHandler ResponsePackageRecievedEventHandler { get; set; }
        public SerialPortUtilities.ToggleListeningForResponsePackageEventHandler ToggleListeningForResponsePackageEventHandler { get; set; }

        public byte[] DigitalWriteCommandPackageCode(bool turnOn)
        {
            throw new NotImplementedException();
        }
        public byte[] AnalogWriteCommandPackageCode(int intensity)
        {
            throw new NotImplementedException();
        }
        public byte[] SetPinModeCommandPackageCode(int pinMode)
        {
            throw new NotImplementedException();
        }

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
