using System;
using Task_3.Enum;

namespace Task_3.BillingSystem
{
    public class RecordReport
    {
        public CallType CallType { get; private set; }
        public int Number { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime Time { get; private set; }
        public int Cost { get; private set; }


        public RecordReport(CallType callType, int number, DateTime date, DateTime time, int cost)
        {
            CallType = callType;
            Number = number;
            Date = date;
            Time = time;
            Cost = cost;
        }
    }
}