using AnyMusic.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Interface
{
    public interface ITrackInUserPlaylistRepository
    {
        void Insert(TrackInPlaylist entity);
        TrackInPlaylist Get(Guid id);
        void Delete(TrackInPlaylist entity);
    }
}
