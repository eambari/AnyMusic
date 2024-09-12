using AnyMusic.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Implementation
{
    public class AlbumFetchRepo : Repository<Album>
    {
        private readonly ApplicationDbContext _context;
        public AlbumFetchRepo(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public override IEnumerable<Album> GetAll()
        {
            return _context.Albums
                .Include(a => a.Tracks)
                .Include(a => a.Artists)
                    .ThenInclude(ai => ai.Artist)
                .AsEnumerable();
        }

        public override Album Get(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return _context.Albums
                .Include(a => a.Tracks)
                .Include(a => a.Artists)
                    .ThenInclude(ai => ai.Artist)
                .SingleOrDefault(a => a.Id == id);
        }
    }
}
