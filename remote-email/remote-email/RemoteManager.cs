using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO.Ports;

namespace remote_email
{
    public class RemoteManager
    {
        SerialPort serialPort;
        RemoteChannel channelA;
        RemoteChannel channelB;

        NetworkCredential credentials;
        string messageA;
        string messageB;

        public List<MailAddress> sendTo;

        public RemoteManager()
        {
            credentials = new NetworkCredential("rojaalba.dev.channela@gmail.com", "development");
            messageA = "This is an automated message from remote channel A.";
            messageB = "This is an automated message from remote channel B.";

            sendTo = new List<MailAddress>();

            // serialPort = new SerialPort("COM5", 9600);
            channelA = new RemoteChannel(5);
            channelB = new RemoteChannel(4);

            // serialPort.DataReceived += serialPort_DataReceived;
            // serialPort.Open();
        }

        public void OpenSerialPort(string port, int baudRate)
        {
            serialPort = new SerialPort(port, baudRate);
            serialPort.DataReceived += serialPort_DataReceived;
            serialPort.Open();
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ComponentMappings_ResponseEvent((byte)serialPort.ReadByte());
        }

        public void ComponentMappings_ResponseEvent(byte responsePackage)
        {
            if (responsePackage == channelA.ChannelCode)
            {
                Console.WriteLine("Channel A triggered!");
                if (sendTo.Count > 0)
                {
                    channelA.SendEmail(credentials, sendTo, "Remote Channel A Automated Message", messageA);
                    Console.WriteLine("Message sent.");
                }
            }
            else if (responsePackage == channelB.ChannelCode)
            {
                Console.WriteLine("Channel B triggered!");
                if (sendTo.Count > 0)
                {
                    channelB.SendEmail(credentials, sendTo, "Remote Channel B Automated Message", messageB);
                    Console.WriteLine("Message sent.");
                }
            }
            else
            {
                Console.WriteLine("Anonymous channel triggered: " + responsePackage);
            }
        }
    }
}
