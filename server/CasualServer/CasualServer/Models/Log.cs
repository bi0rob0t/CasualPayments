using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualServer.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserAction Action { get; set; }
    }
}
