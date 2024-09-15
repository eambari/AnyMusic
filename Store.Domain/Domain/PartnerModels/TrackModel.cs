using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Domain.Domain.PartnerModels
{
    public class TrackModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AlbumId { get; set; }
        public AlbumModel Album { get; set; }
        public double DurationInMilliseconds { get; set; }
        public ICollection<ArtistOfTrack> Artists { get; set; }
        public ICollection<GenreOfTrack> Genres { get; set; }
    }
}
