using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualServer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string Login { get; set; }
        public int Password { get; set; }
    }
}
