using AnyMusic.Domain.Domain.BaseClass;
using AnyMusic.Domain.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnyMusic.Domain.Domain
{
    public class Album : BaseEntity
    {
        [Required]
        [DisplayName("Album Name")]
        public string? AlbumName { get; set; }

        [Required]
        [DisplayName("Album Description")]
        public string? AlbumDescription { get; set; }

        [Required]
        [DisplayName("Album Cover Image")]
        public string? AlbumCoverImage { get; set; }


        [Required]
        public GENRE Genre { get; set; }
        public virtual ICollection<Track>? Tracks { get; set; }
        public virtual ICollection<ArtistInAlbum>? Artists { get; set; }
    }
}
