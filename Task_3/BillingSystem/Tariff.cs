using Task_3.Enum;

namespace Task_3.BillingSystem
{
    public class Tariff
    {
        public int CostByMonth { get; private set; }
        public int CostOfCallPerMinute { get; private set; }
        public int LimitMinutesInMonth { get; private set; }
        public TariffType TariffType { get; private set; }


        public Tariff(TariffType type)
        {
            TariffType = type;
            switch (TariffType)
            {
                case Enum.TariffType.Small:
                    {
                        CostByMonth = 10;
                        CostOfCallPerMinute = 10;
                        LimitMinutesInMonth = 10;
                        break;
                    }
                case Enum.TariffType.Medium:
                    {
                        CostByMonth = 20;
                        CostOfCallPerMinute = 20;
                        LimitMinutesInMonth = 20;
                        break;
                    }
                case Enum.TariffType.Big:
                    {
                        CostByMonth = 30;
                        CostOfCallPerMinute = 30;
                        LimitMinutesInMonth = 30;
                        break;
                    }
                default:
                    {
                        CostByMonth = 0;
                        CostOfCallPerMinute = 0;
                        LimitMinutesInMonth = 0;
                        break;
                    }
            }
        }

    }
}
