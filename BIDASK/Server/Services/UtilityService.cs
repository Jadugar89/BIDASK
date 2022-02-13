using BIDASK.Server.Data;
using BIDASK.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using xAPI.Commands;
using xAPI.Sync;

namespace BIDASK.Server.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static xAPI.Sync.Server serverData = Servers.DEMO;

        public UtilityService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetUser()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<SyncAPIConnector> GetConnected()
        {
            var user = await GetUser();

            var UserData = await _context.userProfils.FindAsync(user.Id);

            
            SyncAPIConnector connector = new SyncAPIConnector(serverData);
            Credentials credentials = new Credentials(UserData.XTB_name, UserData.XTB_pass);
   
                APICommandFactory.ExecuteLoginCommand(connector, credentials);
                connector.Streaming.Connect();
            
            return connector;
        }


    }
}
