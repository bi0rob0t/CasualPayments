using System;

namespace CasualServer.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public Action Action { get; set; }
    }
}
