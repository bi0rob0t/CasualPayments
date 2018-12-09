using System;

namespace CasualServer.Models
{
    public class Log
    {
        public int logId { get; set; }
        public DateTime Time { get; set; }
        public string actionName { get; set; }
        public string serviceName { get; set; }
        public string userName { get; set; }
    }
}
