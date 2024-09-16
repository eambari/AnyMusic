using anymusic_admin.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace anymusic_admin.Controllers
{
    public class TrackController : Controller
    {
        public IActionResult MyStore()
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44317/api/Admin/GetAllTracks";

            HttpResponseMessage response = client.GetAsync(URL).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<TrackDTO>>(responseData);
                return View(data);
            }
            else
            {
             
                return View("Error");
            }
        }
        public IActionResult PartnerStore()
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44317/api/Admin/GetAllPartnerTracks";

            HttpResponseMessage response = client.GetAsync(URL).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<PartnerDTO>>(responseData);
                return View(data);
            }
            else
            {
                
                return View("Error");
            }
        }
    }
}
