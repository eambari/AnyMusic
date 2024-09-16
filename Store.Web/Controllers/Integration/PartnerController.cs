using AnyMusic.Service.Integration.Interface;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var tracks = await _partnerService.GetAllTracksAsync();
            return View(tracks);
        }


       
    }
}
