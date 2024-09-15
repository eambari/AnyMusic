using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Domain.Domain.PartnerModels
{
    public class AlbumModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<TrackModel> Tracks { get; set; }
    }
}
