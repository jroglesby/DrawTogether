using System;
using System.Collections.Generic;
using System.Linq;

namespace DrawTogether.Models
{
    public class Game
    {
        public string GameName { get; set; }
        public string Guid { get; set; }
        public Dictionary<string, User> Users { get; set; }
        public string CanvasObjects { get; set; }

        public Game(string gamename)
        {
            GameName = gamename;
            Users = new Dictionary<string, User>();
            Guid = new Guid().ToString();
            CanvasObjects = "";
        }

        public List<User> GetUsers()
        {
            return Users.Select(u => u.Value).ToList();
        }

        public void AddUserToGame(User user)
        {
            Users.Add(user.ConnectionId, user);
            user.Game = this;
        }

        public void RemoveUserFromGame(string connectionId)
        {
            if (Users.ContainsKey(connectionId))
            {
                Users.Remove(connectionId);
            }
        }

        public string GetCanvasObjects()
        {
            return "["+CanvasObjects+"]";
        }

        public void AddObjectToCanvas(string obj)
        {
            if (CanvasObjects != "")
            {
                CanvasObjects += ",";
            }
            CanvasObjects += obj;
        }

        public void ClearCanvas()
        {
            CanvasObjects = "";
        }
    }
}