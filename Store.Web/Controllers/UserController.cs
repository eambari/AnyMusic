using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using AnyMusic.Domain.Identity;

namespace AnyMusic.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AnyMusicUser> _userManager;

        public UserController(UserManager<AnyMusicUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> PayOrder(string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51PzlpWCduKt5RefbbodPspEUWyLm1bfLVHWujug639WbHtjtunTaOn4s47ONnaBs0gXInhr9TlbdPBAip0J0ZKEh00CRjxNAKa";

            var customerService = new CustomerService();
            var chargeService = new ChargeService();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (long)(5.49 * 100), // Amount in cents
                Description = "AnyMusic Subscription Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    user.IsSubscribed = true;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("SuccessPayment");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to update user subscription.");
                    }
                }
            }

            return RedirectToAction("NotSuccessPayment");
        }
        public IActionResult SuccessPayment()
        {
            return View();
        }

        public IActionResult NotSuccessPayment()
        {
            return View();
        }
    }
}
