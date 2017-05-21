using System;

namespace Task_3.Interfaces
{
    public interface ICallingEventArgs
    {
        int TelephoneNumber { get; }
        int TargetTelephoneNumber { get; }
        Guid Id { get; }
    }
}