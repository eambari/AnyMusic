using AnyMusic.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Interface
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetAll();
        Artist Get(Guid id);
        void Insert(Artist entity);
        void Update(Artist entity);
        void Delete(Artist entity);
    }
}
