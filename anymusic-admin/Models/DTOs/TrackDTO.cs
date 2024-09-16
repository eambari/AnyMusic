namespace anymusic_admin.Models.DTOs
{
    public class TrackDTO
    {
        public string TrackName { get; set; }
        public AlbumDTO Album { get; set; }
        public TimeSpan Duration { get; set; }
        public double Rating { get; set; }
        public List<ArtistDTO> Artists { get; set; }
        public List<PlaylistDTO> Playlists { get; set; }
    }
}
