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
        //private readonly IRepository<Playlist> _playlistRepository;
        private readonly IPlaylistRepository playlistRepository1;

        public PlaylistService(IPlaylistRepository playlistRepository1)
        {
            //_playlistRepository = playlistRepository;
            this.playlistRepository1 = playlistRepository1;
        }

        public void CreateNewUserPlaylist(Playlist a)
        {
            playlistRepository1.Insert(a);
        }

        public void DeleteUserPlaylist(Guid id)
        {
            playlistRepository1.Delete(playlistRepository1.Get(id));
        }

        public List<Playlist> GetAllUserPlaylists(string userId)
        {
             return playlistRepository1.GetAll().Where(p => p.UserId == userId).ToList();
        }

        public Playlist GetDetailsForUserPlaylist(Guid id)
        {
            return playlistRepository1.Get(id);
        }

        public void UpdateExistingUserPlaylist(Playlist a)
        {
            playlistRepository1.Update(a); 
        }
    }
}
