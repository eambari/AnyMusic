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
    public class AlbumRepository : IAlbumRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<Album> entities;
        string errorMessage = string.Empty;

        public AlbumRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Album>();
        }

        public void Delete(Album entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public Album Get(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return context.Albums
               .Include(a => a.Tracks)
               .ThenInclude(t => t.Artists)
                .ThenInclude(at => at.Artist) 
                .SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Album> GetAll()
        {
            return context.Albums
                  .Include(a => a.Tracks)
                  .Include(a => a.Artists)
                      .ThenInclude(ai => ai.Artist)
                  .AsEnumerable();
        }

        public void Insert(Album entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Album entity)
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
