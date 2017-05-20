using Task_3.BillingSystem;
using Task_3.Enum;

namespace Task_3.Interfaces
{
    public interface IContract
    {
        Subscriber Subscriber { get; }
        int Number { get; }
        Tariff Tariff { get; }
        bool ChangeTariff(TariffType tariffType);
    }
}

