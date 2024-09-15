using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Domain.Domain.PartnerModels
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<GenreOfTrack> GenreTracks { get; set; }
    }
}
