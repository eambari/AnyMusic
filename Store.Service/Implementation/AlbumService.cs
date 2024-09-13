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
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> _albumRepo;
        private readonly IAlbumRepository _albumRepository;
     

        public AlbumService(IRepository<Album> albumRepo, IAlbumRepository albumRepository)
        {
            _albumRepo = albumRepo;
            _albumRepository = albumRepository;
        }


        public void CreateNewAlbum(Album a)
        {
           _albumRepo.Insert(a);
        }

        public void DeleteAlbum(Guid id)
        {
            _albumRepo.Delete(_albumRepo.Get(id));
        }

        public List<Album> GetAllAlbums()
        {
            return _albumRepo.GetAll().ToList();
        }

        public Album GetDetailsForAlbum(Guid id)
        {
           return _albumRepository.Get(id);
        }

        public void UpdateExistingAlbum(Album a)
        {
           _albumRepo.Update(a);
        }
    }
}
