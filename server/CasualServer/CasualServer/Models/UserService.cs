﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasualServer.Models
{
    public class UserService
    {
        public int UserServiceId { get; set; }
        public User User { get; set; }
        public Service Service { get; set; }



    }
}
