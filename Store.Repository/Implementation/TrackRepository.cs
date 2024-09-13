using AnyMusic.Domain.Domain;
using AnyMusic.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Implementation
{
    public class TrackRepository : ITrackRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<Track> entities;
        string errorMessage = string.Empty;

        public TrackRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Track>();
        }

        public void Delete(Track entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public Track Get(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return context.Tracks
                .Include(t => t.Album)
                .Include(t => t.Artists)
                    .ThenInclude(at => at.Artist)
                .Include(t => t.TracksInPlaylists)
                    .ThenInclude(tp => tp.Playlist)
                .SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<Track> GetAll()
        {
            return context.Tracks
              .Include(t => t.Album)
              .Include(t => t.Artists)
                  .ThenInclude(at => at.Artist)
              .Include(t => t.TracksInPlaylists)
                  .ThenInclude(tp => tp.Playlist)
              .AsEnumerable();
        }

        public void Insert(Track entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Track entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
