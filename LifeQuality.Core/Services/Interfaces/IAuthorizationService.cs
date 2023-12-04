using LifeQuality.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuality.Core.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<LoginResultDto> LogIn(string login, string password);
    }
}
