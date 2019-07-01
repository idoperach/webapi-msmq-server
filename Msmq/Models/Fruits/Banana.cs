using System;

namespace webapi_msmqServer.Models.Fruits
{
    [Serializable]
    public class Banana : BaseFruit
    {
        public int Size { get; set; }
    }
}