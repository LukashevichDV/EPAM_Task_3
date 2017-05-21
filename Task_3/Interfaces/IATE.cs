using Task_3.ATE;
using Task_3.BillingSystem;
using Task_3.Enum;

namespace Task_3.Interfaces
{
    public interface IATE : IStorage<CallInformation>
    {
        Terminal GetNewTerminal(IContract contract);
        IContract RegisterContract(Subscriber subscriber, TariffType type);
        void CallingTo(object sender, ICallingEventArgs e);
    }
}