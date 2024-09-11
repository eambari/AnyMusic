using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Domain.Domain.ViewModels
{
    public class TrackViewModel
    {
        public Track Track { get; set; }
        public List<Guid> SelectedArtistIds { get; set; } = new List<Guid>();
        public IEnumerable<SelectListItem> Artists { get; set; } = new List<SelectListItem>();
    }

}
