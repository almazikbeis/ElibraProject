﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace updater
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "ElibraUpdate v1.0.0.1";
                string process = args[1].Replace(".exe", "");

                Console.WriteLine("Terminate process!");
                while (Process.GetProcessesByName(process).Length > 0)
                {
                    Process[] myProcesses2 = Process.GetProcessesByName(process);
                    for (int i = 1; i < myProcesses2.Length; i++)
                    {
                        myProcesses2[i].Kill();
                    }
                    Thread.Sleep(300);
                }

                if (File.Exists(args[1]))
                {
                    File.Delete(args[1]);
                }

                File.Move(args[0], args[1]);

                Console.WriteLine("Starting " + args[1]);
                Process.Start(args[1]);
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }
    }
}
