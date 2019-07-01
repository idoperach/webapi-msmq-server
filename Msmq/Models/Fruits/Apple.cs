using System;

namespace webapi_msmqServer.Models.Fruits
{
    [Serializable]
    public class Apple : BaseFruit
    {
        public AppleColor Color { get; set; }
    }
}