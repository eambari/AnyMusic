using AnyMusic.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Implementation
{
    public class ArtistFetchRepo : Repository<Artist>
    {

        private readonly ApplicationDbContext _context;
        public ArtistFetchRepo(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public override IEnumerable<Artist> GetAll()
        {
            return _context.Artists
                .Include(a => a.Albums)
                    .ThenInclude(ai => ai.Album)
                .Include(a => a.Tracks)
                    .ThenInclude(at => at.Track)
                .AsEnumerable();
        }

        public override Artist Get(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return _context.Artists
                .Include(a => a.Albums)
                    .ThenInclude(ai => ai.Album)
                .Include(a => a.Tracks)
                    .ThenInclude(at => at.Track)
                .SingleOrDefault(a => a.Id == id);
        }
    }
}
