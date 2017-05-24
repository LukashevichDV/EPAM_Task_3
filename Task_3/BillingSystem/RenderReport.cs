using System;
using System.Collections.Generic;
using System.Linq;
using Task_3.Enum;
using Task_3.Interfaces;

namespace Task_3.BillingSystem
{

    public class RenderReport : IRenderReport
    {

        public void Render(Report report)
        {
            foreach (var record in report.GetRecords())
            {
                Console.WriteLine("Calls:\n Type {0} |\n Date: {1} |\n Duration: {2} | Cost: {3} | Telephone number: {4}",
                    record.CallType, record.Date, record.Time.ToString("mm:ss"), record.Cost, record.Number);
            }
        }


        public IEnumerable<RecordReport> SortCalls(Report report, SortType sortType)
        {
            var rep = report.GetRecords();
            switch (sortType)
            {
                case SortType.SortByCallType:
                    return rep.
                        OrderBy(x => x.CallType).
                        ToList();

                case SortType.SortByDate:
                    return rep.
                        OrderBy(x => x.Date).
                        ToList();

                case SortType.SortByCost:
                    return rep
                        .OrderBy(x => x.Cost)
                        .ToList();

                case SortType.SortByNumber:
                    return rep.
                        OrderBy(x => x.Number).
                        ToList();

                default:
                    return rep;
            }
        }
    }
}