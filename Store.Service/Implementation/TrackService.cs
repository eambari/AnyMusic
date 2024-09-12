using AnyMusic.Domain.Domain;
using AnyMusic.Repository.Interface;
using AnyMusic.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Service.Implementation
{
    public class TrackService : ITrackService
    {
        private readonly IRepository<Track> _trackRepo;
        private readonly PlaylistService _playlistService;
        private readonly TrackInPlaylistService _trackInPlaylistService;

        public TrackService(IRepository<Track> trackRepo, PlaylistService playlistService, TrackInPlaylistService trackInPlaylistService)
        {
            _trackRepo = trackRepo;
            _playlistService = playlistService;
            _trackInPlaylistService = trackInPlaylistService;
        }

        public void AddTrackToUserPlaylist(Playlist selectedPlaylist, Track selectedTrack)
        {
            if (selectedPlaylist != null && selectedTrack != null)
            {
                if (selectedPlaylist.TracksInPlaylists == null)
                {
                    selectedPlaylist.TracksInPlaylists = new List<TrackInPlaylist>();
                }
                if (!selectedPlaylist.TracksInPlaylists.Any(t => t.TrackId == selectedTrack.Id))
                {
                    var trackInUserPlaylist = new TrackInPlaylist
                    {
                        TrackId = selectedTrack.Id,
                        PlaylistId = selectedPlaylist.Id,
                        Track = selectedTrack,
                        Playlist = selectedPlaylist,
                        Id = Guid.NewGuid()
                    };
                    _trackInPlaylistService.Insert(trackInUserPlaylist);
                    _playlistService.UpdateExistingUserPlaylist(selectedPlaylist);
                }
            }
        }

        public void CreateNewTrack(Track a)
        {
         _trackRepo.Insert(a);
        }

        public void DeleteTrack(Guid id)
        {
          _trackRepo.Delete(_trackRepo.Get(id));
        }

        public void DeleteTrackFromUserPlaylist(Guid id)
        {
            var track = _trackInPlaylistService.Get(id);
            var playlist = track.Playlist;
            if (track != null)
            {
                _trackInPlaylistService.Delete(track);
                _playlistService.UpdateExistingUserPlaylist(playlist);
            }
        }

        public List<Track> GetAllTracks()
        {
          return _trackRepo.GetAll().ToList();
        }

        public Track GetDetailsForTrack(Guid id)
        {
           return _trackRepo.Get(id);
        }

        public void UpdateExistingTrack(Track a)
        {
            _trackRepo.Update(a); 
        }

      
    }
}
