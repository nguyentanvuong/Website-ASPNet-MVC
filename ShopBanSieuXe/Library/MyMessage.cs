using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBanSieuXe.Library
{
    public class MyMessage
    {
        public string Msg { get; set; }
        public string Type { get; set; }
        public MyMessage(string msg, string type)
        {
            this.Msg = msg;
            this.Type = type;
        }
    }
}