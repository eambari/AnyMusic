using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Domain.Domain.ViewModels
{
    public class PartnerViewModel
    {
        public string TrackName { get; set; }
        public string ArtistNames { get; set; }
        public string AlbumName { get; set; }
        public string GenreNames { get; set; }
        public double DurationMinutes { get; set; }
    }
}
