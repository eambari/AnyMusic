namespace AnyMusic.Domain.Domain
{
    public class TrackInPlaylist : BaseEntity
    {
        public Guid TrackId { get; set; }
        public virtual Track? Track { get; set; }

        public Guid PlaylistId { get; set; }
        public virtual Playlist? Playlist { get; set; }
    }
}
