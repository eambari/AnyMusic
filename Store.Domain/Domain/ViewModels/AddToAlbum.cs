using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Domain.Domain.ViewModels
{
    public class AddToAlbum
    {
        public Guid AlbumId { get; set; }
        public Guid TrackId { get; set; }

        public List<SelectListItem>? Tracks { get; set; }
    }
}
