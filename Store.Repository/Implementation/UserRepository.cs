using AnyMusic.Domain.Identity;
using AnyMusic.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyMusic.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<AnyMusicUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<AnyMusicUser>();
        }

        // Get all users
        public IEnumerable<AnyMusicUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        // Get a user by ID with their playlists
        public AnyMusicUser Get(string id)
        {
            return entities
                .Include(u => u.MyPlaylists)
                .Include("MyPlaylists.TracksInPlaylists.Track")
                .FirstOrDefault(u => u.Id == id);
        }

        // Insert a new user
        public void Insert(AnyMusicUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        // Update an existing user
        public void Update(AnyMusicUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        // Delete a user
        public void Delete(AnyMusicUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
