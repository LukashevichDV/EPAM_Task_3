using System.Collections.Generic;
using Task_3.BillingSystem;
using Task_3.Enum;

namespace Task_3.Interfaces
{
    public interface IRenderReport
    {
        void Render(Report report);
        IEnumerable<RecordReport> SortCalls(Report report, SortType sortType);
    }
}