﻿using System;
using System.Threading;
using Task_3.AutoTelephoneExchange;
using Task_3.BillingSystem;
using Task_3.Interfaces;

namespace Task_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IATE ate = new Ate();
            IRenderReport render = new RenderReport();
            IBillingSystem bs = new BillingSystem.BillingSystem(ate);

            IContract c1 = ate.RegisterContract(new Subscriber("Name", "Lastname"), Enum.TariffType.Small);
            IContract c2 = ate.RegisterContract(new Subscriber("Name1", "Lastname1"), Enum.TariffType.Medium);
            IContract c3 = ate.RegisterContract(new Subscriber("Name2", "Lastname2"), Enum.TariffType.Big);

            c1.Subscriber.AddMoney(10);
            var t1 = ate.GetNewTerminal(c1);
            var t2 = ate.GetNewTerminal(c2);
            var t3 = ate.GetNewTerminal(c3);

            t1.ConnectToPort();
            t2.ConnectToPort();
            t3.ConnectToPort();

            t1.Call(t2.Number);
            Thread.Sleep(3000);
            t2.EndCall();

            t3.Call(t1.Number);
            Thread.Sleep(1000);
            t3.EndCall();

            t2.Call(t1.Number);
            Thread.Sleep(2000);
            t1.EndCall();


            Console.WriteLine();
            Console.WriteLine("Sorted records:");
            foreach (var item in render.SortCalls(bs.GetReport(t1.Number), Enum.SortType.SortByCallType))
            {
                Console.WriteLine("Calls:\n Type {0} |\n Date: {1} |\n Duration: {2} | Cost: {3} | Telephone number: {4}",
                    item.CallType, item.Date, item.Time.ToString("mm:ss"), item.Cost, item.Number);
            }

            Console.ReadKey();
        }
    }
}