using Microsoft.AspNetCore.Identity;
using AnyMusic.Domain.Domain;
using System.Collections.Generic;

namespace AnyMusic.Domain.Identity
{
    public class AnyMusicUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public bool? IsSubscribed { get; set; } = false;
        public virtual ICollection<Playlist>? MyPlaylists { get; set; }
    }
}