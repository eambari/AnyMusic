using AnyMusic.Domain.Domain;
using AnyMusic.Domain.Domain.BaseClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Implementation
{
    public class TrackFetchRepo : Repository<Track>
    {

        private readonly ApplicationDbContext _context;
        

        public TrackFetchRepo(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public override IEnumerable<Track> GetAll()
        {
            return _context.Tracks
                .Include(t => t.Album)
                .Include(t => t.Artists)
                    .ThenInclude(at => at.Artist)
                .Include(t => t.TracksInPlaylists)
                    .ThenInclude(tp => tp.Playlist)
                .AsEnumerable();
        }

        public override Track Get(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return _context.Tracks
                .Include(t => t.Album)
                .Include(t => t.Artists)
                    .ThenInclude(at => at.Artist)
                .Include(t => t.TracksInPlaylists)
                    .ThenInclude(tp => tp.Playlist)
                .SingleOrDefault(t => t.Id == id);
        }
    }

    
}
