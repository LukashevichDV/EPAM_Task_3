using System;
using Task_3.Enum;
using Task_3.MyEventArgs;

namespace Task_3.ATE
{
    public class Terminal
    {
        public int Number { get; }
        private Port TerminalPort;
        private Guid Id;

        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<AnswerEventArgs> AnswerEvent;
        public event EventHandler<EndCallEventArgs> EndCallEvent;


        public Terminal(int number, Port port)
        {
            Number = number;
            TerminalPort = port;
        }


        protected virtual void RaiseCallEvent(int targetNumber)
        {
            CallEvent?.Invoke(this, new CallEventArgs(Number, targetNumber));
        }


        protected virtual void RaiseAnswerEvent(int targetNumber, CallState state, Guid id)
        {
            AnswerEvent?.Invoke(this, new AnswerEventArgs(Number, targetNumber, state, id));
        }


        protected virtual void RaiseEndCallEvent(Guid id)
        {
            EndCallEvent?.Invoke(this, new EndCallEventArgs(id, Number));
        }


        public void Call(int targetNumber)
        {
            RaiseCallEvent(targetNumber);
        }


        public void TakeIncomingCall(object sender, CallEventArgs e)
        {
            bool flag = true;
            Id = e.Id;
            Console.WriteLine("Have incoming Call at number: {0} to terminal {1}", e.TelephoneNumber, e.TargetTelephoneNumber);
            while (flag == true)
            {
                Console.WriteLine("Answer? Y/N");
                char k = Console.ReadKey().KeyChar;
                switch (k)
                {
                    case 'Y':
                    case 'y':
                        flag = false;
                        Console.WriteLine();
                        AnswerToCall(e.TelephoneNumber, CallState.Answered, e.Id);
                        break;
                    case 'N':
                    case 'n':
                        flag = false;
                        Console.WriteLine();
                        EndCall();
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }
        }

        public void ConnectToPort()
        {
            if (TerminalPort.Connect(this))
            {
                TerminalPort.CallPortEvent += TakeIncomingCall;
                TerminalPort.AnswerPortEvent += TakeAnswer;
            }
        }


        public void AnswerToCall(int target, CallState state, Guid id)
        {
            RaiseAnswerEvent(target, state, id);
        }


        public void EndCall()
        {
            RaiseEndCallEvent(Id);
        }


        public void TakeAnswer(object sender, AnswerEventArgs e)
        {
            Id = e.Id;
            if (e.StateInCall == CallState.Answered)
            {
                Console.WriteLine("Terminal with number: {0}, have answer on call a number: {1}", e.TelephoneNumber, e.TargetTelephoneNumber);
            }
            else
            {
                Console.WriteLine("Terminal with number: {0}, have rejected call", e.TelephoneNumber);
            }
        }
    }
}