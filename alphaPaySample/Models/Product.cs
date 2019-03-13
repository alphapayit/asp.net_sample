using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPassDataToView.Models
{
    public class Product
    {

        //sample product
        public int product_qty { get; set; }

        public string product_name { get; set; }

        public double product_price { get; set; }

        public double subtotal { get; set; }

        public Product()
        {
            product_qty = 1;
            product_name = "Android Phone";
            product_price = 0.01;
            subtotal = 0.01;
        }
        

    }
}