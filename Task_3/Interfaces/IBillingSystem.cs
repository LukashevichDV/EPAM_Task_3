using Task_3.BillingSystem;

namespace Task_3.Interfaces
{
    public interface IBillingSystem
    {
        Report GetReport(int telephoneNumber);
    }
}