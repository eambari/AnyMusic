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
    public class ArtistService : IArtistService
    {

        private readonly IRepository<Artist> _artistRepository;

        public ArtistService(IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public void CreateNewArtist(Artist a)
        {
           _artistRepository.Insert(a);
        }

        public void DeleteArtist(Guid id)
        {
            _artistRepository.Delete(_artistRepository.Get(id));
        }

        public List<Artist> GetAllArtists()
        {
           return _artistRepository.GetAll().ToList();
        }

        public Artist GetDetailsForArtist(Guid id)
        {
            return _artistRepository.Get(id);
        }

        public void UpdateExistingArtist(Artist a)
        {
            _artistRepository.Update(a);
        }
    }
}
