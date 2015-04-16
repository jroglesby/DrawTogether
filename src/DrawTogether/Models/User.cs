using System.Runtime.Serialization;

namespace DrawTogether.Models
{
    public class User
    {
        public string ConnectionId { get; set; }
        public string Username { get; set; }
        [IgnoreDataMember]
        public Game Game { get; set; }

        public User(string connectionId, string username)
        {
            ConnectionId = connectionId;
            Username = username;
            Game = null;
        }
    }
}