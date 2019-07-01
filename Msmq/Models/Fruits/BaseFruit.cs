using System;

namespace webapi_msmqServer.Models.Fruits
{
    [Serializable]
    public class BaseFruit : BaseModel
    {
        public Season GrowingSeason { get; set; }
    }
}