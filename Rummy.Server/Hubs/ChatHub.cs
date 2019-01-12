using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rummy.Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string roomKey = "roomKey";

        public ChatHub(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public List<string> GetRooms()
        {
            if (_memoryCache.TryGetValue<List<string>>(roomKey, out var rooms))
            {
                return rooms;
            }
            return new List<string>();
        }

        public void CreateRoom(string roomName)
        {
            if (!_memoryCache.TryGetValue<List<string>>(roomKey, out var rooms))
            {
                rooms = new List<string>();
            }
            rooms.Add(roomName);
            _memoryCache.Set(roomKey, rooms);
        }

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendMessageToRoom(string roomName, string user, string message)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
