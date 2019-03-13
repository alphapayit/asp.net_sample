using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPassDataToView.Models;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Web.Helpers;
using Newtonsoft.Json;
using System.IO;

namespace alphaPaySample.Controllers
{
    public class PayController : Controller
    {
        // GET: Pay
        public ActionResult Index()
        {
            string a = payCallbackPage();
            ViewBag.message = a;
            return View();
        }


        //creating QRcode order
        public string payCallbackPage() {
            JsonValue para = new JsonValue();
            string url = "https://pay.alphapay.ca/api/v1.0/gateway/partners/" + para.PARTNER_CODE + "/orders/" + para.order_id + "?" + queryParams();
            //send API request
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "PUT";
            req.ContentType = "application/json";
            //using your parameters
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                string json = "{ \"PARTNER_CODE\":\"xxxx\", " +
                    "\"CREDENTIAL_CODE\":\"xxxxxxxxxxxxxxxxxxxx\", " +
                    "\"order_id\":\"xxxxxxxxxxxxxxxxxxxxxxxx\"," +
                    "\"description\":\"xxxxxx\"," +
                    "\"price\":xx," +
                    "\"currency\":\"CAD\"," +
                    "channel:\"xxxxxxxxx\"," +
                    "operator:\"xxxxxxx\"}"; 

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            //get request response
            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();

                Console.WriteLine(result); // check your return message
            }
            //once the order created, redirect to pay url with required paramenters
     
            return "success";
        }

        private string queryParams()
        {
            JsonValue para = new JsonValue();
            long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string nonceStr = RandomString(15);
            string validStr = para.PARTNER_CODE + "&" + time + "&" + nonceStr + "&" + para.CREDENTIAL_CODE;
            string sign = GenerateSHA256String(validStr).ToLower();

            return "time=" + time + "&nonce_str=" + nonceStr + "&sign=" + sign;
        }

        private static Random random = new Random();
        private object HttpWebRequest;

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }

        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        
    }
}