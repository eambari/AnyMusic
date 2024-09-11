using AnyMusic.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<AnyMusicUser> GetAll();
        AnyMusicUser Get(string id);
        void Insert(AnyMusicUser entity);
        void Update(AnyMusicUser entity);
        void Delete(AnyMusicUser entity);
    }
}
