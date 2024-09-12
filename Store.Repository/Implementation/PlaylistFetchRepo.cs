using AnyMusic.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Implementation
{
    public class PlaylistFetchRepo : Repository<Playlist>
    {

        private readonly ApplicationDbContext _context;
        public PlaylistFetchRepo(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public override IEnumerable<Playlist> GetAll()
        {
            return _context.Playlists
                .Include(p => p.TracksInPlaylists)
                    .ThenInclude(tp => tp.Track)
                .AsEnumerable();
        }

        public override Playlist Get(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return _context.Playlists
                .Include(p => p.TracksInPlaylists)
                    .ThenInclude(tp => tp.Track)
                .SingleOrDefault(p => p.Id == id);
        }
    }
}
