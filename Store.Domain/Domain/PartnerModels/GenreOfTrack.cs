using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Domain.Domain.PartnerModels
{
    public class GenreOfTrack
    {
        public Guid Id { get; set; }

        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        public Guid TrackId { get; set; }
        public TrackModel Track { get; set; }
    }
}
