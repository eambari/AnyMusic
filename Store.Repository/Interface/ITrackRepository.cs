using AnyMusic.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Interface
{
    public interface ITrackRepository
    {
        IEnumerable<Track> GetAll();
        Track Get(Guid id);
        void Insert(Track entity);
        void Update(Track entity);
        void Delete(Track entity);
    }
}
