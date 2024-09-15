using AnyMusic.Domain.Domain.PartnerModels;
using AnyMusic.Domain.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyMusic.Service.Integration.Interface
{
    public interface IPartnerService
    {
        Task<IEnumerable<PartnerViewModel>> GetAllTracksAsync();
    }
}
