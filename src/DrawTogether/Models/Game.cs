using System;
using System.Collections.Generic;

namespace DrawTogether.Models
{
    public class Game
    {
        public string GameName { get; set; }
        public Dictionary<string, User> Users { get; set; }
        public string CanvasObjects { get; set; }

        public Game(string gamename)
        {
            GameName = gamename;
            Users = new Dictionary<string, User>();
        }

        public void AddUserToGame(string connectionId, string username)
        {
            Users.Add(connectionId, new User(connectionId, username));
        }

        public void RemoveUserFromGame(string connectionId)
        {
            Users.Remove(connectionId);
        }
    }
}