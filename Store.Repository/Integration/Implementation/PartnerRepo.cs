using AnyMusic.Domain.Domain;
using AnyMusic.Domain.Domain.PartnerModels;
using AnyMusic.Repository.Integration.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Integration.Implementation
{
    public class PartnerRepo : IPartnerRepo
    {
        private readonly PartnerStoreDB _context;

        public PartnerRepo(PartnerStoreDB context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrackModel>> GetAllTracksWithDetailsAsync()
        {
            return await _context.TracksModel
                .Include(t => t.Album)
                .Include(t => t.Artists).ThenInclude(aot => aot.Artist)
                .Include(t => t.Genres).ThenInclude(got => got.Genre)
                .ToListAsync();
        }
    }
}
