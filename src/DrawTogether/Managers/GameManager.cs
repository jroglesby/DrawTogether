using System.Collections.Generic;
using DrawTogether.Models;
using System.Linq;

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

        public List<string> GetGameGuids()
        {
            List<string> guids = Games.Select(g => g.Value.Guid).ToList();
            return guids;
        }

        public List<User> GetGameUsers(string gameGuid)
        {
            return Games[gameGuid].GetUsers();
        }

        public void CreateGame(string gameName)
        {
            Game game = new Game(gameName);
            Games.Add(game.Guid, game);
        }

        public void AddPlayerToGame(string gameGuid, User user)
        {
            Games[gameGuid].AddUserToGame(user);
        }

        public void RemovePlayerFromGame(string gameGuid, string connectionId)
        {
            Games[gameGuid].RemoveUserFromGame(connectionId);
        }

        public string GetCanvasObjects(string gameGuid)
        {
            return Games[gameGuid].GetCanvasObjects();
        }

        public void AddObjectToCanvas(string gameGuid, string obj)
        {
            Games[gameGuid].AddObjectToCanvas(obj);
        }

        public void ClearCanvas(string gameGuid)
        {
            Games[gameGuid].ClearCanvas();
        }
    }
}