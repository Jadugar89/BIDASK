using BIDASK.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xAPI.Sync;

namespace BIDASK.Server.Services
{
    public interface IUtilityService
    {
        Task<User> GetUser();
        Task<SyncAPIConnector> GetConnected();
    }
}
