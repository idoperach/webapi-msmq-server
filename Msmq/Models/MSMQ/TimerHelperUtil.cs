using System;
using System.Messaging;
using System.Timers;

namespace webapi_msmqServer.Models.MSMQ
{
    public class TimerHelperUtil : Timer
    {
        public Message Message { get; set; }
        public DateTime TimeStemp { get; set; }

        public TimerHelperUtil(double interval) : base(interval)
        {
        }
    }
}