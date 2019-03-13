using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPassDataToView.Models
{
    public class JsonValue
    {
        public string PARTNER_CODE { get; set; }

        public string CREDENTIAL_CODE { get; set; }

        public string order_id { get; set; }

        public string description { get; set; }

        public int price { get; set; }

        public string currency { get; set; }

        public string channel { get; set; }

        public string notify_url { get; set; }

        public string alphaOperator{ get; set; }


        public JsonValue()
        {
            PARTNER_CODE = "xxxx"; // Your Partner code
            CREDENTIAL_CODE = "xxxxxxxxxxxxxxxxxxxxxxxxxxx"; // Your Credential Code

            //sample template,Shall be unique for the same partner. If repeated, order will be recognized as an existing order.
            order_id = "xxxxxx" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "xxxxxxxx";

            description = "xxxx"; //your description
            price = 1;
            currency = "CAD"; // default payment method. Allow method: CAD,CNY
            channel = "xxxxxxxxxx"; // Two channel available: Alipay,Wechat
            notify_url = "xxxxxxxxxxxxxxxxxx"; // not required
            alphaOperator = "xxxxxx";//variable name should be "operator" in Json
        }


    }
}