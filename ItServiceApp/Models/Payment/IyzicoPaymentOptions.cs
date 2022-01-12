using Iyzipay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItServiceApp.Models
{
    public class IyzicoPaymentOptions : Options
    {
        public const string Key = "IyzicoOptions";
        public string ThreadsCallbackUrl { get; set; }
    }
}
