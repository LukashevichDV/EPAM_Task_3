using System;
using Task_3.Interfaces;

namespace Task_3.MyEventArgs
{
    public class EndCallEventArgs : EventArgs, ICallingEventArgs
    {
        public Guid Id { get; }
        public int TelephoneNumber { get; }
        public int TargetTelephoneNumber { get; private set; }


        public EndCallEventArgs(Guid id, int number)
        {
            Id = id;
            TelephoneNumber = number;
        }
    }
}