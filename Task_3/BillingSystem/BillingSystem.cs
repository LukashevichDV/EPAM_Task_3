using System;
using System.Linq;
using Task_3.AutoTelephoneExchange;
using Task_3.Enum;
using Task_3.Interfaces;

namespace Task_3.BillingSystem
{
    public class BillingSystem : IBillingSystem
    {

        private IStorage<CallInformation> Storage;
        public BillingSystem(IStorage<CallInformation> storage)
        {
            Storage = storage;
        }


        public Report GetReport(int telephoneNumber)
        {
            var calls = Storage.GetInfoList().
                Where(x => x.MyNumber == telephoneNumber || x.TargetNumber == telephoneNumber).
                ToList();

            var report = new Report();
            foreach (var call in calls)
            {
                CallType callType;
                int number;
                if (call.MyNumber == telephoneNumber)
                {
                    callType = CallType.OutgoingCall;
                    number = call.TargetNumber;
                }
                else
                {
                    callType = CallType.IncomingCall;
                    number = call.MyNumber;
                }
                var record = new RecordReport(callType, number, call.BeginCall, new DateTime((call.EndCall - call.BeginCall).Ticks), call.Cost);
                report.AddRecord(record);
            }
            return report;
        }
    }
}