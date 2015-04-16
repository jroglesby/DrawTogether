using System;

namespace DrawTogether.Models
{
    public class User
    {
        public string ConnectionId { get; set; }
        public string Username { get; set; }

        public User(string connectionId, string username)
        {
            ConnectionId = ConnectionId;
            Username = username;
        }
    }
}