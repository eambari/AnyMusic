﻿using AnyMusic.Domain.Domain.BaseClass;

namespace AnyMusic.Domain.Domain
{
    public class ArtistInAlbum : BaseEntity
    {
        public Guid ArtistId { get; set; }
        public virtual Artist? Artist { get; set; }

        public Guid AlbumId { get; set; }
        public virtual Album? Album { get; set; }
    }
}
