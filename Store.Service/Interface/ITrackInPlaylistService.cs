using AnyMusic.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Service.Interface
{
    public interface ITrackInPlaylistService
    {
        TrackInPlaylist Get(Guid id);
        void Insert(TrackInPlaylist a);
        void Delete(TrackInPlaylist a);
    }
}
