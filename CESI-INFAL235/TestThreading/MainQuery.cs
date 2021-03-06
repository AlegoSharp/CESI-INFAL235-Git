﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestThreading.Classes;

namespace TestThreading
{
    class MainQuery
    {
        private static List<TaskResult> Results { get; set; }
        static void Main(string[] args)
        {
            string arg1 = "";
            string arg2 = "";
            string arg3 = "";
            bool modeNoReponse = true;
            int leftPosition = 0;

            if(Console.CursorLeft > 0)
                leftPosition = Console.CursorLeft;

            if (args.Length > 3)
            {
                arg1 = args[0];

            }
            else
            {
                arg1 = "5";

            }
            switch (arg1)
            {
                case "0":
                    SingleThread(); Environment.Exit(0); break;

                case "1":
                    MultiThreadAuCarre(); Environment.Exit(0); break;

                case "2":
                    MultiThread(); Environment.Exit(0); break;

                case "3":
                    MultiTaskMultiThreadFive(); Environment.Exit(0); break;

                case "4":
                    MultiTaskMultiThreadTen(); Environment.Exit(0); break;

                case "5":
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine("...");
                    Console.SetCursorPosition(0, 3);
                    Console.WriteLine("...");

                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Nb thread");
                    //Console.CursorTop++;
                    arg2 = Console.ReadLine();
                    Console.WriteLine("Nb query to api");
                    //Console.CursorTop++;
                    arg3 = Console.ReadLine();
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine(Environment.NewLine);

                    MultiTaskMultiThreadTen(int.Parse(arg2),int.Parse(arg3), leftPosition);
                    break;
            }
            Console.CursorLeft = leftPosition + 30;
            Main(args);
        }

        public static void SingleThread()
        {
            string adr = Properties.Api.Default.Adresse;
            ATask a = new ATask();
            Program p = new Program();
            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);


            a.Action = p.CallDate;
            var tasks = a.RunPoolOfTasks(1000);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Minutes : " + (a.Start - a.End).Minutes);
            Console.WriteLine("Secondes : " + (a.Start - a.End).Seconds);
            Console.WriteLine("Millisecondes : " + (a.Start - a.End).Milliseconds);
            Console.ReadLine();
        }
        public static void MultiThreadAuCarre()
        {
            string adr = Properties.Api.Default.Adresse;
            ATask a = new ATask();
            Program p = new Program();
            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);

            a.Action = p.CallDate;
            var tasks = a.RunPoolOfTasks(true);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Minutes : " + (a.Start - a.End).Minutes);
            Console.WriteLine("Secondes : " + (a.Start - a.End).Seconds);
            Console.WriteLine("Millisecondes : " + (a.Start - a.End).Milliseconds);
            Console.ReadLine();
        }

        public static void MultiThread()
        {
            string adr = Properties.Api.Default.Adresse;
            ATask a = new ATask();
            Program p = new Program();
            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);


            a.Action = p.CallDate;
            var tasks = a.RunPoolOfTasks(false);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Minutes : " + (a.Start - a.End).Minutes);
            Console.WriteLine("Secondes : " + (a.Start - a.End).Seconds);
            Console.WriteLine("Millisecondes : " + (a.Start - a.End).Milliseconds);
            Console.ReadLine();
        }
        public static void MultiTaskMultiThreadFive()
        {
            string adr = Properties.Api.Default.Adresse;
            ATask a = new ATask();
            Program p = new Program();
            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);


            a.Action = p.CallDate;
            var tasks = a.RunPoolOfTasks(5);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Minutes : " + (a.Start - a.End).Minutes);
            Console.WriteLine("Secondes : " + (a.Start - a.End).Seconds);
            Console.WriteLine("Millisecondes : " + (a.Start - a.End).Milliseconds);
            Console.ReadLine();
        }

        public static void MultiTaskMultiThreadTen()
        {
            string adr = Properties.Api.Default.Adresse;
            ATask a = new ATask();
            Program p = new Program();
            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);


            a.Action = p.CallDate;
            var tasks = a.RunPoolOfTasks(10);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Minutes : " + (a.Start - a.End).Minutes);
            Console.WriteLine("Secondes : " + (a.Start - a.End).Seconds);
            Console.WriteLine("Millisecondes : " + (a.Start - a.End).Milliseconds);
            Console.ReadLine();
        }

        public static void MultiTaskMultiThreadTen(int nbThread,int nbQuery,int leftPos)
        {
            MainQuery.Results = new List<TaskResult>();
            string adr = Properties.Api.Default.Adresse;

            Program p = new Program();
            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);
            p.Mode = ProgramMode.NoAnswer;

            ATask a = new ATask(nbQuery);
            a.LeftPosition = leftPos;
            a.ProcessCompleted += A_ProcessCompleted;
            a.Action = p.CallDate;

            var tasks = a.RunPoolOfTasks(nbQuery/ nbThread,true);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorLeft = leftPos;
            Console.WriteLine("NB THREAD : " + nbThread);
            Console.CursorLeft = leftPos;

            Console.WriteLine("NB CALLS : " + nbQuery);
            Console.CursorLeft = leftPos;

            Console.WriteLine("Minutes : " + (a.Start - a.End).Minutes);
            Console.CursorLeft = leftPos;

            Console.WriteLine("Secondes : " + (a.Start - a.End).Seconds);
            Console.CursorLeft = leftPos;

            Console.WriteLine("Millisecondes : " + (a.Start - a.End).Milliseconds);
            Console.CursorLeft = leftPos;

            for (int i =0; i< Results.Count;i++)
            {
                var res = Results[i];
                Console.WriteLine("Thread N° : " + i + "            Durée :" + (res.End - res.Start));
            }

            Console.ReadLine();
        }

        private static void A_ProcessCompleted(TaskResult tr)
        {
            MainQuery.Results.Add(tr);
            if(Results.Count == 999)
            {

            }
        }

    }
}
