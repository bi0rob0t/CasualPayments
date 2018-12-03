using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualServer.Models
{
    public class UserService
    {
        public int UserServiceId { get; set; }
        public int User { get; set; }
        public int[] Services { get; set; }
    }
}
