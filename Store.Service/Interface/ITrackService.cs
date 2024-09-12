using AnyMusic.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Service.Interface
{
    public interface ITrackService
    {
        void AddTrackToUserPlaylist(Playlist playlist, Track track);
        void DeleteTrackFromUserPlaylist(Guid id);
        List<Track> GetAllTracks();
        Track GetDetailsForTrack(Guid id);
        void CreateNewTrack(Track a);
        void UpdateExistingTrack(Track a);
        void DeleteTrack(Guid id);
       
    }
}
