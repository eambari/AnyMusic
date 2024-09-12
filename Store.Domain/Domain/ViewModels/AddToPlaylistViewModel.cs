using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Domain.Domain.ViewModels
{
    public class AddToPlaylistViewModel
    {
        public Guid TrackId { get; set; }
        public string TrackName { get; set; }
        public Guid PlaylistId { get; set; }
    }

}
