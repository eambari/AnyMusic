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
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Playlist> entities;
        string errorMessage = string.Empty;

        public PlaylistRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Playlist>();
        }

        public void Delete(Playlist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public Playlist Get(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return context.Playlists
                .Include(p => p.TracksInPlaylists)
                    .ThenInclude(tp => tp.Track)
                .SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Playlist> GetAll()
        {
            return context.Playlists
                 .Include(p => p.TracksInPlaylists)
                     .ThenInclude(tp => tp.Track)
                 .AsEnumerable();
        }

        public void Insert(Playlist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Playlist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
