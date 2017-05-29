using System;
using Task_3.Enum;
using Task_3.Interfaces;

namespace Task_3.MyEventArgs
{
    public class AnswerEventArgs : EventArgs, ICallingEventArgs
    {
        public int TelephoneNumber { get; }
        public int TargetTelephoneNumber { get; }
        public CallState StateInCall;
        public Guid Id { get; }


        public AnswerEventArgs(int number, int target, CallState state)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            StateInCall = state;
        }


        public AnswerEventArgs(int number, int target, CallState state, Guid id)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            StateInCall = state;
            Id = id;
        }
    }
}