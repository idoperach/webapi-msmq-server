using System.Messaging;

namespace webapi_msmqServer.Models.MSMQ
{
    public sealed class QueueProvider
        {
            private static QueueProvider _instance;
            private static readonly object Padlock = new object();
            public MessageQueue Queue { get; }

            private QueueProvider()
            {
                Queue = GetOrCreateQueue();
            }

            public static QueueProvider Instance
            {
                get
                {
                    lock (Padlock)
                    {
                        return _instance ?? (_instance = new QueueProvider());
                    }
                }
            }

        private static MessageQueue GetOrCreateQueue()
        {
            var path = MsmqConfiguration.Path;
            var queue = !MessageQueue.Exists(path) ? MessageQueue.Create(path, true) : new MessageQueue(path);

//            queue.Formatter = new BinaryMessageFormatter();
            queue.UseJournalQueue = true;

            return queue;
        }
    }
}