using AnyMusic.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Service.Interface
{
    public interface IPlaylistService
    {
        List<Playlist> GetAllUserPlaylists(string userId);
        Playlist GetDetailsForUserPlaylist(Guid id);
        void CreateNewUserPlaylist(Playlist a);
        void UpdateExistingUserPlaylist(Playlist a);
        void DeleteUserPlaylist(Guid id);
    }
}
