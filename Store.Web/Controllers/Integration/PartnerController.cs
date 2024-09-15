using AnyMusic.Service.Integration.Interface;
using AnyMusic.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AnyMusic.Web.Controllers.Integration
{
    public class PartnerController : Controller
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        public async Task<IActionResult> Index()
        {
            var tracks = await _partnerService.GetAllTracksAsync();
            return View(tracks);
        }
    }
}
