using System;
using Task_3.Interfaces;

namespace Task_3.MyEventArgs
{
    public class CallEventArgs : EventArgs, ICallingEventArgs
    {
        public int TelephoneNumber { get; }
        public int TargetTelephoneNumber { get; }
        public Guid Id { get; }


        public CallEventArgs(int number, int target)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
        }


        public CallEventArgs(int number, int target, Guid id)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            Id = id;
        }
    }
}