using AnyMusic.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Interface
{
    public interface IPlaylistRepository
    {
        IEnumerable<Playlist> GetAll();
        Playlist Get(Guid id);
        void Insert(Playlist entity);
        void Update(Playlist entity);
        void Delete(Playlist entity);
        public void SaveChanges();
    }
}
