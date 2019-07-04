using System;
using System.Messaging;
using System.Timers;
using webapi_msmqServer.Models.Fruits;
using webapi_msmqServer.Models.MSMQ;

namespace webapi_msmqServer.Controllers
{
    public class MsmqLogic
    {
        private readonly object _padLock = new object();
        private Timer _sendTimer;
//        private ElapsedEventHandler timerOnElapsed = (sender, e) => MyElapsedMethod(sender, e);

        public void Send(Basket basket)
        {
            BinaryMessageFormatter formatter = new BinaryMessageFormatter();

            var message = new Message
            {
                Body = basket,
                Label = Environment.MachineName,
                UseDeadLetterQueue = true,
                Recoverable = true,
                UseJournalQueue = true,
                Formatter = formatter,
                Priority = MessagePriority.Highest,
                TimeToBeReceived = MsmqConfiguration.ResponseTimeOut
            };
            new MessageSender().Send(message);

            HandleSent(message);
        }

        public void HandleSent(Message message)
        {
            lock (_padLock)
            {
                if (_sendTimer != null)
                {
                    return;
                }

                _sendTimer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
                var timeStamp = DateTime.Now;

                
                _sendTimer.Elapsed += (sender, e) => MyElapsedMethod(timeStamp, message.Id);

                _sendTimer.Start();
            }

            
        }


        public void MyElapsedMethod(DateTime sentTimeTemp, string messageId)
        {
            lock (_padLock)
            {
                if (_sendTimer == null)
                {
                    return;
                }

                //TODO: LOCK in case two threads are here, continue if locked or _sendTimer is null (was disposed)
                if (DateTime.Now - sentTimeTemp >= MsmqConfiguration.ResponseTimeOut)
                {
                    // TODO: handle the message timedout, dispose, and send Message timedout.
                    Console.WriteLine($"Message (id:{messageId}) timedout!");

                    StopTimer();
                    return;
                }

                var messageRead = false;
                try
                {
                    var messageFromQueue = QueueProvider.Instance.Queue.PeekById(messageId);

                    messageRead = messageFromQueue != null;
                }
                catch (Exception ex)
                {
                    // TODO: invalid op ex is thrown when the message is not found in the queue
                }


                if (messageRead)
                {
                    //TODO: message received stop listening to if read, send success to A, also check if the message isn't in dead letter queue

                    return;
                }

                // TODO: message wasn't received just yet, but you should still listen    
            }
        }

        private void StopTimer()
        {
            var temp = _sendTimer;
            if (temp == null || _sendTimer == null || _padLock == null)
            {
                return;
            }

            _sendTimer = null;


            temp.Stop();
            temp.Close();
        }
    }
}