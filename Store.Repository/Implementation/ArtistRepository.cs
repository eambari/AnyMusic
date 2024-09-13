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
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Artist> entities;
        string errorMessage = string.Empty;

        public ArtistRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Artist>();
        }

        public void Delete(Artist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public Artist Get(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return context.Artists
                .Include(a => a.Albums)
                    .ThenInclude(ai => ai.Album)
                .Include(a => a.Tracks)
                    .ThenInclude(at => at.Track)
                .SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Artist> GetAll()
        {
            return context.Artists
               .Include(a => a.Albums)
                   .ThenInclude(ai => ai.Album)
               .Include(a => a.Tracks)
                   .ThenInclude(at => at.Track)
               .AsEnumerable();
        }

        public void Insert(Artist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Artist entity)
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
