﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class UserRegisterModel : UserLoginModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
