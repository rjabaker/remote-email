using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArduinoUtilities;

namespace remote_email
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteManager remoteManager = new RemoteManager();
            Loop(remoteManager);
        }

        static void Loop(RemoteManager manager)
        {
            string input = string.Empty;
            string[] parsed;
            while (input != "exit")
            {
                input = Console.ReadLine();
                parsed = input.Split(' ');
                if (parsed.Length == 0) continue;
                switch (parsed[0])
                {
                    case "add":
                        if (parsed.Length > 1)
                        {
                            System.Net.Mail.MailAddress address = new System.Net.Mail.MailAddress(parsed[1]);
                            if(!manager.sendTo.Contains(address)) manager.sendTo.Add(address);
                        }
                        break;
                    case "remove":
                        if (parsed.Length > 1)
                        {
                            System.Net.Mail.MailAddress address = new System.Net.Mail.MailAddress(parsed[1]);
                            if(manager.sendTo.Contains(address)) manager.sendTo.Remove(address);
                        }
                        break;
                    case "print":
                        foreach (System.Net.Mail.MailAddress address in manager.sendTo)
                        {
                            Console.WriteLine("> " + address.Address);
                        }
                        if (manager.sendTo.Count == 0)
                        {
                            Console.WriteLine("> EMPTY");
                        }
                        break;
                    case "clear":
                        manager.sendTo.Clear();
                        break;
                    case "exit":
                        break;
                }
            }
        }
    }
}
