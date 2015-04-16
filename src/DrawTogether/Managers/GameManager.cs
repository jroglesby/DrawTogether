using System;
using System.Collections.Generic;
using DrawTogether.Models;
namespace DrawTogether.Managers
{
    public class GameManager
    {
        private const string OP_GETCANVAS = "GETCANVAS";
        private const string OP_CLEARCANVAS = "CLEARCANVAS";
        private const string OP_GETUSERSINGAME = "GETUSERSINGAME";
        private const string OP_SYNCDRAW = "SYNCDRAW";

        private Dictionary<string,Game> Games { get; set; }

        public GameManager()
        {
            Games = new Dictionary<string, Game>();
            CreateGame("DefaultGame");
        }

        public void CreateGame(string gameName)
        {
            Game game = new Game(gameName);
            Games.Add(gameName, game);
        }

        public void HandleMessage(string connectionId, string op, object[] parameters)
        {
            var userGame = GetGameByConnectionId(connectionId);
            if(userGame == null)
            {
                return;
            }

            switch(op)
            {
                case OP_GETUSERSINGAME:
                    break;
                case OP_GETCANVAS:
                    break;
                case OP_CLEARCANVAS:
                    break;
                case OP_SYNCDRAW:
                    break;
            }
        }

        public Game GetGameByConnectionId(string connectionId)
        {
            return Games["DefaultGame"];
        }

        public void AddPlayerToGame(string gameName, string connectionId, string username)
        {
            Games[gameName].AddUserToGame(connectionId, username);
        }

        public void RemovePlayerFromGame(string gameName, string connectionId)
        {
            Games[gameName].RemoveUserFromGame(connectionId);
        }
    }
}