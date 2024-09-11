using AnyMusic.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace AnyMusic.Domain.Domain
{
    public class Album : BaseEntity
    {
        [Required]
        public string? AlbumName { get; set; }

        [Required]
        public string? AlbumDescription { get; set; }

        [Required]
        public string? AlbumCoverImage { get; set; }

        [Required]
        public string? Genre { get; set; }
        public virtual ICollection<Track>? Tracks { get; set; }
        public virtual ICollection<ArtistInAlbum>? Artists { get; set; }
    }
}
