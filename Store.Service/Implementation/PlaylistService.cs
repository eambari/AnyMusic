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
    public class PlaylistService : IPlaylistService
    {


        private readonly IRepository<Playlist> _playlistRepository;

        public PlaylistService(IRepository<Playlist> playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public void CreateNewUserPlaylist(Playlist a)
        {
          _playlistRepository.Insert(a);
        }

        public void DeleteUserPlaylist(Guid id)
        {
         _playlistRepository.Delete(_playlistRepository.Get(id));
        }

        public List<Playlist> GetAllUserPlaylists(string userId)
        {
             return _playlistRepository.GetAll().Where(p => p.UserId == userId).ToList();
        }

        public Playlist GetDetailsForUserPlaylist(Guid id)
        {
            return _playlistRepository.Get(id);
        }

        public void UpdateExistingUserPlaylist(Playlist a)
        {
         _playlistRepository.Update(a); 
        }
    }
}
