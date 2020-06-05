using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class ChatContext : IdentityDbContext<ChatUser>
    {
        public ChatContext(DbContextOptions options) : base(options)
        {

        }
    }
}
