using System;
using System.Messaging;
using webapi_msmqServer.Models.Fruits;

namespace webapi_msmqServer.Models.MSMQ
{
    public class MessageSender
    {
        public void Send(Basket ObjToSend)
        {
            var queue = QueueProvider.Instance.Queue;
//            var message = new MsmqMessage<Basket>(ObjToSend);


            using (var trn = new MessageQueueTransaction())
            {
                trn.Begin();
                BinaryMessageFormatter formatter = new BinaryMessageFormatter();

                var message = new Message
                {
                    Body = ObjToSend,
                    Label = Environment.MachineName,
                    UseDeadLetterQueue = true,
                    Recoverable = true,
                    Formatter = formatter,
                    Priority = MessagePriority.Highest,
                    TimeToBeReceived = MsmqConfiguration.ResponseTimeOut
                };


                queue.Send(message, MessageQueueTransactionType.Single);
                trn.Commit();
            }
        }

//        using (MessageQueueTransaction transaction = new MessageQueueTransaction())
//        {
//            transaction.Begin();
//            using (var queue = new MessageQueue(@fullQueue, QueueAccessMode.Send))
//            {
//                BinaryMessageFormatter formatter = new BinaryMessageFormatter();
//                // XmlMessageFormatter formatter = new XmlMessageFormatter(new Type[] { typeof(Testing) });
//
//                var testing = new Testing { myBody = string.Format("Hello {0}", Environment.UserName), myMessageText = "Header" };
//                var message = new Message
//                {
//                    Body = testing,
//                    Label = Environment.MachineName,
//                    UseDeadLetterQueue = true,
//                    Recoverable = true,
//                    Formatter = formatter
//                };
//                queue.Send(message, MessageQueueTransactionType.Single);
//
//            }
//            transaction.Commit(); 
//
//        }
    }
}