using AnyMusic.Domain.Identity;
using System.ComponentModel.DataAnnotations;


namespace AnyMusic.Domain.Domain
{
    public class Playlist : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public string? UserId { get; set; }
        public virtual AnyMusicUser? User { get; set; }
        public virtual ICollection<TrackInPlaylist>? TracksInPlaylists { get; set; }
    }
}
