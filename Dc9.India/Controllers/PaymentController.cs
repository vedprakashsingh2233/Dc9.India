using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dc9.India.Controllers
{
    public class PaymentController : Controller
    {
        private string key = ConfigurationManager.AppSettings["RazorpayKey"];
        private string secret = ConfigurationManager.AppSettings["RazorpaySecret"];
        // GET: Payment
        [HttpPost]
        public ActionResult CreateOrder(decimal amount)
        {
            try
            {
                // Convert amount to paise (Razorpay uses paise)
                int amountInPaise = (int)(amount * 100);

                // Initialize Razorpay Client
                RazorpayClient client = new RazorpayClient(key, secret);

                // Create order options
                Dictionary<string, object> options = new Dictionary<string, object>
            {
                { "amount", amountInPaise },
                { "currency", "INR" },
                { "payment_capture", "1" } // Auto capture payment
            };

                // Create Order
                Order order = client.Order.Create(options);

                // Return order details to frontend
                return Json(new { success = true, orderId = order["id"], key = key, amount = amountInPaise });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult VerifyPayment(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            try
            {
                // Verify Payment Signature
                Dictionary<string, string> attributes = new Dictionary<string, string>
            {
                { "razorpay_payment_id", razorpay_payment_id },
                { "razorpay_order_id", razorpay_order_id },
                { "razorpay_signature", razorpay_signature }
            };

                RazorpayClient client = new RazorpayClient(key, secret);
                Utils.verifyPaymentSignature(attributes);

                return Json(new { success = true, message = "Payment Verified Successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Payment Verification Failed: " + ex.Message });
            }
        }
    }
}