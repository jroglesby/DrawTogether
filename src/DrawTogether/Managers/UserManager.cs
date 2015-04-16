using System;
using System.Collections.Generic;
using DrawTogether.Models;

namespace DrawTogether.Managers
{
    public class UserManager
    {
        private Dictionary<string, User> Users { get; set; }
        public UserManager()
        {
            Users = new Dictionary<string, User>();
        }

        public User GetUser(string connectionId)
        {
            return Users[connectionId];
        }

        public User UserConnected(string connectionId, string username)
        {
            Users.Add(connectionId, new User(connectionId, username));
            return GetUser(connectionId);
        }

        public User UserDisconnected(string connectionId)
        {
            if(Users.ContainsKey(connectionId))
            {
                User user = Users[connectionId];
                Users.Remove(connectionId);
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}