using webapi_msmqServer.Models.Fruits;
using webapi_msmqServer.Models.MSMQ;

namespace webapi_msmqServer.Controllers
{
    public class MsmqLogic
    {
        public void Send(Basket message)
        {
            new MessageSender().Send(message);

            HandleSent();
        }

        public void HandleSent()
        {
        }
    }
}