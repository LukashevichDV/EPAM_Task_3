using System;
using System.Collections.Generic;
using System.Linq;
using Task_3.BillingSystem;
using Task_3.Enum;
using Task_3.Interfaces;
using Task_3.MyEventArgs;

namespace Task_3.AutoTelephoneExchange
{
    public class Ate : IATE
    {
        private IDictionary<int, Tuple<Port, IContract>> UsersData;
        private IList<CallInformation> CallList = new List<CallInformation>();


        public Ate()
        {
            UsersData = new Dictionary<int, Tuple<Port, IContract>>();

        }


        public Terminal GetNewTerminal(IContract contract)
        {
            var newPort = new Port();
            newPort.AnswerEvent += CallingTo;
            newPort.CallEvent += CallingTo;
            newPort.EndCallEvent += CallingTo;
            UsersData.Add(contract.Number, new Tuple<Port, IContract>(newPort, contract));
            var newTerminal = new Terminal(contract.Number, newPort);
            return newTerminal;
        }


        public IContract RegisterContract(Subscriber subscriber, TariffType type)
        {
            var contract = new Contract(subscriber, type);
            return contract;
        }


        public void CallingTo(object sender, ICallingEventArgs e)
        {
            if ((UsersData.ContainsKey(e.TargetTelephoneNumber) && e.TargetTelephoneNumber != e.TelephoneNumber) || e is EndCallEventArgs)
            {
                CallInformation inf = null;
                Port targetPort;
                Port port;
                int number;
                int targetNumber;
                if (e is EndCallEventArgs)
                {
                    var callListFirst = CallList.First(x => x.Id.Equals(e.Id));
                    if (callListFirst.MyNumber == e.TelephoneNumber)
                    {
                        targetPort = UsersData[callListFirst.TargetNumber].Item1;
                        port = UsersData[callListFirst.MyNumber].Item1;
                        number = callListFirst.MyNumber;
                        targetNumber = callListFirst.TargetNumber;
                    }
                    else
                    {
                        port = UsersData[callListFirst.TargetNumber].Item1;
                        targetPort = UsersData[callListFirst.MyNumber].Item1;
                        targetNumber = callListFirst.MyNumber;
                        number = callListFirst.TargetNumber;
                    }
                }
                else
                {
                    targetPort = UsersData[e.TargetTelephoneNumber].Item1;
                    port = UsersData[e.TelephoneNumber].Item1;
                    targetNumber = e.TargetTelephoneNumber;
                    number = e.TelephoneNumber;
                }
                if (targetPort.State != PortState.Connect || port.State != PortState.Connect) return;
                {
                    var tuple = UsersData[number];
                    var targetTuple = UsersData[targetNumber];

                    var eventArgs = e as AnswerEventArgs;
                    if (eventArgs != null)
                    {
                        var answerArgs = eventArgs;

                        if (!answerArgs.Id.Equals(Guid.Empty) && CallList.Any(x => x.Id.Equals(answerArgs.Id)))
                        {
                            inf = CallList.First(x => x.Id.Equals(answerArgs.Id));
                        }

                        if (inf != null)
                        {
                            targetPort.AnswerCall(answerArgs.TelephoneNumber, answerArgs.TargetTelephoneNumber, answerArgs.StateInCall, inf.Id);
                        }
                        else
                        {
                            targetPort.AnswerCall(answerArgs.TelephoneNumber, answerArgs.TargetTelephoneNumber, answerArgs.StateInCall);
                        }
                    }
                    var callEventArgs = e as CallEventArgs;
                    if (callEventArgs != null)
                    {
                        if (tuple.Item2.Subscriber.Money > tuple.Item2.Tariff.CostOfCallPerMinute)
                        {
                            var callArgs = callEventArgs;

                            if (callArgs.Id.Equals(Guid.Empty))
                            {
                                inf = new CallInformation(
                                    callArgs.TelephoneNumber,
                                    callArgs.TargetTelephoneNumber,
                                    DateTime.Now);
                                CallList.Add(inf);
                            }

                            if (!callArgs.Id.Equals(Guid.Empty) && CallList.Any(x => x.Id.Equals(callArgs.Id)))
                            {
                                inf = CallList.First(x => x.Id.Equals(callArgs.Id));
                            }
                            if (inf != null)
                            {
                                targetPort.IncomingCall(callArgs.TelephoneNumber, callArgs.TargetTelephoneNumber, inf.Id);
                            }
                            else
                            {
                                targetPort.IncomingCall(callArgs.TelephoneNumber, callArgs.TargetTelephoneNumber);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Terminal with number {0} is not enough money in the account!", e.TelephoneNumber);

                        }
                    }
                    if (!(e is EndCallEventArgs)) return;
                    {
                        var args = (EndCallEventArgs)e;
                        inf = CallList.First(x => x.Id.Equals(args.Id));
                        inf.EndCall = DateTime.Now;
                        var sumOfCall = tuple.Item2.Tariff.CostOfCallPerMinute * TimeSpan.FromTicks((inf.EndCall - inf.BeginCall).Ticks).TotalMinutes;
                        inf.Cost = (int)sumOfCall;
                        targetTuple.Item2.Subscriber.WithDraw(inf.Cost);
                        targetPort.AnswerCall(args.TelephoneNumber, args.TargetTelephoneNumber, CallState.Rejected, inf.Id);
                    }
                }
            }
            else if (!UsersData.ContainsKey(e.TargetTelephoneNumber))
            {
                Console.WriteLine("You have calling on non-existent number");
            }
            else
            {
                Console.WriteLine("You have calling on your number");
            }
        }


        public IList<CallInformation> GetInfoList()
        {
            return CallList;
        }
    }
}