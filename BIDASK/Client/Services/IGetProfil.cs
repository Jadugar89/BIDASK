using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BIDASK.Shared;

namespace BIDASK.Client.Services
{
    public interface IGetProfil
    {
        UserProfil _UserProfil { get; set; }
        Task LoadUserProfilAsync();
        Task UpdateUserProfilAsync();
    }
}
