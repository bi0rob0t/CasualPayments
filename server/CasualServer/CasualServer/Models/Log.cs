using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualServer.Models
{
    public class Log
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Time { get; set; }
        public Action Action { get; set; }
    }
}
