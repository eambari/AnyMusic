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
            var stripeSecretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");

            if (string.IsNullOrEmpty(stripeSecretKey))
            {
                throw new InvalidOperationException("Stripe secret key is not set.");
            }

            StripeConfiguration.ApiKey = stripeSecretKey;

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
