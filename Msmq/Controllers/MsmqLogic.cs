using System;
using System.Messaging;
using System.Timers;
using webapi_msmqServer.Models.Fruits;
using webapi_msmqServer.Models.MSMQ;

namespace webapi_msmqServer.Controllers
{
    public class MsmqLogic
    {
        private Timer _timer;

        public MsmqLogic()
        {
            _timer = new Timer(TimeSpan.FromSeconds(5).Milliseconds);
            _timer.Stop();
        }

        public void Send(Basket basket)
        {
            BinaryMessageFormatter formatter = new BinaryMessageFormatter();

            var message = new Message
            {
                Body = basket,
                Label = Environment.MachineName,
                UseDeadLetterQueue = true,
                Recoverable = true,
                Formatter = formatter,
                Priority = MessagePriority.Highest,
                TimeToBeReceived = MsmqConfiguration.ResponseTimeOut
            };
            new MessageSender().Send(message);

            HandleSent(message);
        }

        public void HandleSent(Message message)
        {
            _timer.Elapsed += (sender, e) => MyElapsedMethod(sender, e, message);
            _timer.Start();
        }
       

        public void MyElapsedMethod(object sender, ElapsedEventArgs e, Message message)
        {
            if (DateTime.Now - message.ArrivedTime >= MsmqConfiguration.ResponseTimeOut)
            {
                // TODO: handle the message timedout, dispose, and send Message timedout.
                Console.WriteLine($"Message (id:{message.Id}) timedout!");
                return;
            }

            var messageRead = false;
            try
            {
                var messageFromQueue = QueueProvider.Instance.Queue.PeekById(message.Id);

                messageRead = messageFromQueue != null;
            }
            catch (Exception ex)
            {
                // TODO: check if this will get here if the message was read by B
            }
            

            if (messageRead)
            {
                //TODO: message received stop listening to if read, send success to A
                return;
            }
            
            // TODO: message wasn't received just yet, but you should still listen
        }
    }
}