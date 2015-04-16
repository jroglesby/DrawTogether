using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using DrawTogether.Models;
using System.Collections.Generic;
using DrawTogether.Managers;

namespace DrawTogether.Hubs
{
    [HubName("DrawTogether")]
    public class DrawTogetherHub : Hub
    {
        private static GameManager _gameManager = new GameManager();
        private static UserManager _userManager = new UserManager();

        private string GetGameGuid()
        {
            return Context.Request.Cookies["GameGuid"];
        }
        private string GetUserName()
        {
            return Context.Request.Cookies["username"];
        }


        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public void AddObjectToCanvas(string serializedObj)
        {
            string gameGuid = GetGameGuid();
            _gameManager.AddObjectToCanvas(gameGuid, serializedObj);
            Clients.OthersInGroup(gameGuid).addObjectToCanvas(serializedObj);
        }

        public void GetCanvasObjects()
        {
            string gameGuid = GetGameGuid();
            Clients.Caller.receiveCanvasObjects(_gameManager.GetCanvasObjects(gameGuid));
        }

        public void ClearCanvas()
        {
            string gameGuid = GetGameGuid();
            _gameManager.ClearCanvas(gameGuid);
            Clients.OthersInGroup(gameGuid).clearCanvas();
        }

        public void GetOnlineUsers()
        {
            string gameGuid = GetGameGuid();
            List<User> users = _gameManager.GetGameUsers(gameGuid);
            Clients.Caller.receiveOnlineUsers(users);
        }

        public override Task OnConnected()
        {
            User user = _userManager.UserConnected(Context.ConnectionId, GetUserName());
            string gameGuid = _gameManager.GetGameGuids()[0];
            _gameManager.AddPlayerToGame(gameGuid, user);
            Groups.Add(user.ConnectionId, gameGuid);
            Clients.Caller.setGameGuid(gameGuid);
            Clients.OthersInGroup(user.Game.Guid).userConnected(GetUserName());
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            User user = _userManager.UserDisconnected(Context.ConnectionId);
            _gameManager.RemovePlayerFromGame(user.Game.Guid, user.ConnectionId);
            Clients.OthersInGroup(user.Game.Guid).userDisconnected(user.Username);
            return base.OnDisconnected(stopCalled);
        }
    }
}