using AnyMusic.Domain.Domain;
using AnyMusic.Domain.Domain.ViewModels;
using AnyMusic.Domain.DTO;
using AnyMusic.Repository.Interface;
using AnyMusic.Service.Integration.Interface;
using AnyMusic.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnyMusic.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ITrackService _trackService;
        private readonly IPartnerService _partnerService;
        private readonly ITrackRepository trackRepository;

        public AdminController(ITrackService trackService, IPartnerService partnerService, ITrackRepository trackRepository)
        {
            _trackService = trackService;
            _partnerService = partnerService;
            this.trackRepository = trackRepository;
        }

        [HttpGet("[action]")]
        public List<TrackDTO> GetAllTracks()
        {
            var tracks = trackRepository.GetAll().ToList();

            var trackDtos = tracks.Select(t => new TrackDTO
            {
                TrackName = t.TrackName,
                Album = t.Album != null ? new AlbumDTO { AlbumName = t.Album.AlbumName } : null,
                Duration = t.Duration,
                Rating = t.Rating,
                Artists = t.Artists?.Select(a => new ArtistDTO { ArtistName = a.Artist.ArtistName }).ToList(),
                Playlists = t.TracksInPlaylists?.Select(tp => new PlaylistDTO {
                    Name = tp.Playlist?.Name, 
                    UserEmail = tp.Playlist?.User?.Email
                }).ToList()
            }).ToList();

            return trackDtos;
        }


        [HttpGet("[action]")]
        public async Task<IEnumerable<PartnerViewModel>> GetAllPartnerTracks()
        {
            return await _partnerService.GetAllTracksAsync();
        }

    }
}
