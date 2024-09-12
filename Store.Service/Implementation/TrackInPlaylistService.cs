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
    public class TrackInPlaylistService : ITrackInPlaylistService
    {

        private readonly IRepository<TrackInPlaylist> _repository;

        public TrackInPlaylistService(IRepository<TrackInPlaylist> repository)
        {
            _repository = repository;
        }

        public void Delete(TrackInPlaylist a)
        {
            _repository.Delete(a);
        }

        public TrackInPlaylist Get(Guid id)
        {
           return _repository.Get(id);  
        }

        public void Insert(TrackInPlaylist a)
        {
          _repository.Insert(a);
        }
    }
}
