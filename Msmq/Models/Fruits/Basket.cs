using System;
using System.Collections.Generic;

namespace webapi_msmqServer.Models.Fruits
{
    [Serializable]
    public class Basket : BaseModel
    {
        public List<BaseFruit> Fruits { get; set; }
    }
}