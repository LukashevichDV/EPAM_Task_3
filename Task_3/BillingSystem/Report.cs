using System.Collections.Generic;

namespace Task_3.BillingSystem
{
    public class Report
    {
        private IList<RecordReport> ListRecords;


        public Report()
        {
            ListRecords = new List<RecordReport>();
        }


        public void AddRecord(RecordReport record)
        {
            ListRecords.Add(record);
        }


        public IList<RecordReport> GetRecords()
        {
            return ListRecords;
        }
    }
}