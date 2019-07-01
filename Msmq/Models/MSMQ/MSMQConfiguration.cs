using System;
using System.Configuration;

namespace webapi_msmqServer.Models.MSMQ
{
    public static class MsmqConfiguration
    {
        public static string Path { get;}
        public static TimeSpan TimeToBeReceived { get; }
        public static TimeSpan ResponseTimeOut { get; }

        static MsmqConfiguration()
        {
            Path = ConfigurationManager.AppSettings["Path"];
//            TimeToBeReceived = TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings["TimeToBeReceived"]));
            ResponseTimeOut = TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings["ResponseTimeOut"]));
        }
    }
}