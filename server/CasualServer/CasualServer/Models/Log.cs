using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasualServer.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public Action Action { get; set; }
    }
}
