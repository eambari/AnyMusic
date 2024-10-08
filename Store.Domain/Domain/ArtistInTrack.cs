﻿using AnyMusic.Domain.Domain.BaseClass;

namespace AnyMusic.Domain.Domain
{
    public class ArtistInTrack : BaseEntity
    {
        public Guid ArtistId { get; set; }
        public virtual Artist? Artist { get; set; }

        public Guid TrackId { get; set; }
        public virtual Track? Track { get; set; }
    }
}
