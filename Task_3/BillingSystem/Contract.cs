using System;
using Task_3.Enum;
using Task_3.Interfaces;

namespace Task_3.BillingSystem
{
    public class Contract : IContract
    {

        static Random randomNumeral = new Random();


        public Subscriber Subscriber { get; private set; }
        public int Number { get; private set; }
        public Tariff Tariff { get; private set; }
        private DateTime LastTariffUpdateDate;


        public Contract(Subscriber subscriber, TariffType tariffType)
        {
            LastTariffUpdateDate = DateTime.Now;
            Subscriber = subscriber;
            Number = randomNumeral.Next(1000000, 9999999);
            Tariff = new Tariff(tariffType);
        }


        public bool ChangeTariff(TariffType tariffType)
        {
            if (DateTime.Now.AddMonths(-1) >= LastTariffUpdateDate)
            {
                LastTariffUpdateDate = DateTime.Now;
                Tariff = new Tariff(tariffType);
                Console.WriteLine("Tariff has been changed !");
                return true;
            }
            else
            {
                Console.WriteLine("You can't change tariff in this month !");
                return false;
            }
        }
    }
}