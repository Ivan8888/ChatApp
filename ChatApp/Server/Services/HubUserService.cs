using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Hubs;

namespace Server.Services
{
    public class HubUserService
    {
        public List<HubUserInfo> HubUsers { get; }
        public HubUserService()
        {
            HubUsers = new List<HubUserInfo>();
        }
    }
}
