using AnyMusic.Domain.Domain;
using AnyMusic.Domain.Domain.PartnerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Repository.Integration.Interface
{
    public interface IPartnerRepo
    {
        Task<IEnumerable<TrackModel>> GetAllTracksWithDetailsAsync();
    }
}
