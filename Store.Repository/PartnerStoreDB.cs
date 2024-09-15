using AnyMusic.Domain.Domain.PartnerModels;
using AnyMusic.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository
{
    public class PartnerStoreDB : DbContext
    {
        public PartnerStoreDB(DbContextOptions<PartnerStoreDB> options)
            : base(options) { }


        public DbSet<TrackModel> TracksModel { get; set; }
        public DbSet<ArtistModel> ArtistsModel { get; set; }
        public DbSet<AlbumModel> AlbumsModel { get; set; }
        public DbSet<ArtistOfTrack> ArtistOfTracksModel { get; set; }
        public DbSet<Genre> GenresModel { get; set; }
        public DbSet<GenreOfTrack> GenreOfTracksModel { get; set; }

        // Optionally, override OnModelCreating if you need to customize the model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumModel>().ToTable("Albums", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<ArtistModel>().ToTable("Artist", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<TrackModel>().ToTable("Tracks", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Genre>().ToTable("Genre", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<ArtistOfTrack>().ToTable("ArtistOfTrack", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<GenreOfTrack>().ToTable("GenreOfTrack", t => t.ExcludeFromMigrations());


           // modelBuilder.Entity<ArtistOfTrack>()
           //.HasKey(aot => new { aot.ArtistId, aot.TrackId });

           // modelBuilder.Entity<GenreOfTrack>()
           //     .HasKey(got => new { got.GenreId, got.TrackId });

            // Configure your entity mappings here if needed
        }
    }
}
