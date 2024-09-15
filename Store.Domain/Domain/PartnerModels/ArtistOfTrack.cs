using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Domain.Domain.PartnerModels
{
    public class ArtistOfTrack
    {
        public Guid Id { get; set; }
        public Guid ArtistId { get; set; }
        public ArtistModel Artist { get; set; }
        public Guid TrackId { get; set; }
        public TrackModel Track { get; set; }
    }
}
