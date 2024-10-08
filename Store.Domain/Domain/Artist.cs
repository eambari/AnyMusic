﻿using AnyMusic.Domain.Domain.BaseClass;
using AnyMusic.Domain.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnyMusic.Domain.Domain
{
    public class Artist : BaseEntity
    {
        [Required]
        [DisplayName("Artist Name")]
        public string? ArtistName { get; set; }
        [DisplayName("Artist Description")]
        public string? ArtistDescription { get; set; }

        public virtual ICollection<ArtistInAlbum>? Albums { get; set; }
        public virtual ICollection<ArtistInTrack>? Tracks { get; set; }
    }
}
