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
    public class TrackInUserPlaylistRepository : ITrackInUserPlaylistRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<TrackInPlaylist> entities;
        string errorMessage = string.Empty;

        public TrackInUserPlaylistRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<TrackInPlaylist>();
        }

        public void Delete(TrackInPlaylist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public TrackInPlaylist Get(Guid id)
        {
            return entities
                .Include("UserPlaylist")
                .First(t => t.Id == id);
        }

        public void Insert(TrackInPlaylist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }
    }
}
