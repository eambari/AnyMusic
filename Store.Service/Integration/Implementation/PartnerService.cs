using AnyMusic.Domain.Domain.PartnerModels;
using Newtonsoft.Json;
using AnyMusic.Service.Integration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyMusic.Domain.Domain.PartnerModels;
using AnyMusic.Domain.Domain;
using AnyMusic.Repository.Interface;
using AnyMusic.Service.Interface;
using AnyMusic.Repository.Integration.Interface;
using AnyMusic.Domain.Domain.ViewModels;
using AnyMusic.Repository.Implementation;

namespace AnyMusic.Service.Integration.Implementation
{
    public class PartnerService : IPartnerService
    {
        private readonly IPartnerRepo _partnerRepo;

        public PartnerService(IPartnerRepo partnerRepo)
        {
            _partnerRepo = partnerRepo;
        }

        public async Task<IEnumerable<PartnerViewModel>> GetAllTracksAsync()
        {
            var tracks = await _partnerRepo.GetAllTracksWithDetailsAsync();

            return tracks.Select(t => new PartnerViewModel
            {
                TrackName = t.Name,
                AlbumName = t.Album.Name,
                ArtistNames = string.Join(", ", t.Artists.Select(aot => aot.Artist.Name)),
                GenreNames = string.Join(", ", t.Genres.Select(got => got.Genre.Name)),
                DurationMinutes = TimeSpan.FromMilliseconds(t.DurationInMilliseconds).TotalMinutes
            });
        }
    }
}
