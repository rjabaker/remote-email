﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace remote_email
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteManager remoteManager = new RemoteManager();
            Loop(remoteManager);
        }

        static void OpenFunction(string[] input, RemoteManager manager)
        {
            if (input.Length < 3) return;
            switch (input[1])
            {
                case "serial":
                    if (input.Length == 4)
                    {
                        manager.OpenSerialPort(input[2].ToUpper(), Convert.ToInt32(input[3]));
                    }
                    break;
                default:
                    break;
            }
        }

        static void ReadInput(out string input, RemoteManager manager)
        {
            input = Console.ReadLine();
            string[] parsed = input.Split(' ');
            if (parsed.Length == 0) return;
            switch (parsed[0])
            {
                case "add":
                    if (parsed.Length > 1)
                    {
                        System.Net.Mail.MailAddress address = new System.Net.Mail.MailAddress(parsed[1]);
                        if (!manager.sendTo.Contains(address)) manager.sendTo.Add(address);
                    }
                    break;
                case "open":
                    OpenFunction(parsed, manager);
                    break;
                case "remove":
                    if (parsed.Length > 1)
                    {
                        System.Net.Mail.MailAddress address = new System.Net.Mail.MailAddress(parsed[1]);
                        if (manager.sendTo.Contains(address)) manager.sendTo.Remove(address);
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

        static void Loop(RemoteManager manager)
        {
            string input = string.Empty;
            while (input != "exit")
            {
                ReadInput(out input, manager);
            }
        }
    }
}
