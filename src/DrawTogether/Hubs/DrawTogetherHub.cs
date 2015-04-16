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
        private static List<User> Users = new List<User>();
        private static string SerializedCanvas = "";
        private static GameManager _gameManager = new GameManager();

        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public void handleMessage(string op, object[] parameters)
        {
            string connectionId = Context.ConnectionId;
            _gameManager.HandleMessage(connectionId, op, parameters);
        }

        public void SyncDraw(string serializedDrawing)
        {
            Clients.Others.syncDrawing(serializedDrawing);
            if(SerializedCanvas != "")
            {
                SerializedCanvas += ",";
            }
            SerializedCanvas += serializedDrawing;
        }

        public void GetCurrentCanvas()
        {
            var serializedCanvas = "[" + SerializedCanvas + "]";
            Clients.Caller.receiveCurrentCanvas(serializedCanvas);
        }

        public void ClearCanvas()
        {
            SerializedCanvas = "";
            Clients.Others.clearCanvas();
        }

        public void GetOnlineUsers()
        {
            Clients.Caller.receiveOnlineUsers(Users);
        }

        public override Task OnConnected()
        {
            Users.Add(new User() { ConnectionId = Context.ConnectionId, Username = Context.Request.Cookies["username"], Online = true });
            Clients.Others.userConnected(Context.Request.Cookies["username"]);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            User user = Users.Find(u => u.ConnectionId == Context.ConnectionId);
            if (user != null)
            {
                Users.Remove(user);
                Clients.Others.userDisconnected(user.Username);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}