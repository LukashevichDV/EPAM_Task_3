using System;
using Task_3.Interfaces;

namespace Task_3.MyEventArgs
{
    public class CallEventArgs : EventArgs, ICallingEventArgs
    {
        public int TelephoneNumber { get; private set; }
        public int TargetTelephoneNumber { get; private set; }
        public Guid Id { get; private set; }


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