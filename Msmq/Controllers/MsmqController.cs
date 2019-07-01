using System.Collections.Generic;
using System.Web.Http;
using webapi_msmqServer.Models.Fruits;

namespace webapi_msmqServer.Controllers
{
    public class MsmqController : ApiController
    {
        private MsmqLogic logic { get; }

        public MsmqController()
        {
            logic = new MsmqLogic();
        }

        [Route]
        [HttpGet]
        public  IHttpActionResult WriteToqueue()
        {
            logic.Send(new Basket
            {
                Name = "myBasket",
                Fruits = new List<BaseFruit>
                {
                    new Banana
                    {
                        GrowingSeason = Season.Summer,
                        Name = "banani",
                        Size = 18
                    },
                    new Apple
                    {
                        Color = AppleColor.Green,
                        GrowingSeason = Season.Fall,
                        Name = "apploni"
                    }
                }
            });

            return Ok("hello");
        }
    }
}