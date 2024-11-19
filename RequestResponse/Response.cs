using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayerCRUD.RequestResponse
{
    public class Response
    {
        public int PlayerId { get; set; } = 0; //same fieldname as PlayerEntity or different like this
        public string PlayerName { get; set; } = string.Empty;
        public string PlayerMobile { get; set; } = string.Empty;
    }
}