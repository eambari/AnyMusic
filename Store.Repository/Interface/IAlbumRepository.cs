using AnyMusic.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Interface
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> GetAll();
        Album Get(Guid id);
        void Insert(Album entity);
        void Update(Album entity);
        void Delete(Album entity);
    }
}
