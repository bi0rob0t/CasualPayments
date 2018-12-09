

namespace CasualServer.Models
{
    public class Action
    {
        public int ActionId { get; set; }
        public string ActionType { get; set; }        
        public User User { get; set; }
        public Service Service { get; set; }
    }
}
