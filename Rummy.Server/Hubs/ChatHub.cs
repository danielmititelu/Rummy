using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Rummy.Server.Models;
using Rummy.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rummy.Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string roomsKey = "roomsKey";

        public ChatHub(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public List<string> GetRooms()
        {
            if (_memoryCache.TryGetValue<List<string>>(roomsKey, out var rooms))
            {
                return rooms;
            }
            return new List<string>();
        }

        public void CreateRoom(string roomName)
        {
            if (!_memoryCache.TryGetValue<List<string>>(roomsKey, out var rooms))
            {
                rooms = new List<string>();
            }
            rooms.Add(roomName);

            _memoryCache.Set(roomName,
                new GameRoom
                {
                    RoomName = roomName,
                    Players = new List<Player>()
                });
            _memoryCache.Set(roomsKey, rooms);
        }

        public async Task JoinRoom(string roomName)
        {
            var gameRoom = _memoryCache.Get<GameRoom>(roomName);
            gameRoom.Players.Add(new Player { ConnectionId = Context.ConnectionId });
            _memoryCache.Set(roomName, gameRoom);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task LeaveRoom(string roomName)
        {
            var gameRoom = _memoryCache.Get<GameRoom>(roomName);
            gameRoom.Players.RemoveAll(p => p.ConnectionId == Context.ConnectionId);
            _memoryCache.Set(roomName, gameRoom);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendMessageToRoom(string roomName, string user, string message)
        {
            await Clients.Group(roomName).SendAsync("BroadcastMessage", user, message);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("BroadcastMessage", user, message);
        }

        public async Task StartGame(string roomName)
        {
            var gameRoom = _memoryCache.Get<GameRoom>(roomName);
            gameRoom.InitilizeGame();

            foreach (var player in gameRoom.Players)
            {
                await Clients.Client(player.ConnectionId).SendAsync("PiecesToAddToBoard", player.Pieces);
            }
            _memoryCache.Set(roomName, gameRoom);
        }

        public async Task Draw(string roomName)
        {
            var gameRoom = _memoryCache.Get<GameRoom>(roomName);
            if (!gameRoom.IsPlayerTurn(Context.ConnectionId))
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ErrorMessage", "It's not your turn.");
            }

            var piece = gameRoom.DrawPiece(Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).SendAsync("PiecesToAddToBoard", new List<Piece> { piece });
            _memoryCache.Set(roomName, gameRoom);
        }

        public async Task PutPieceOnTable(string roomName, Piece piece)
        {
            var gameRoom = _memoryCache.Get<GameRoom>(roomName);
            if (!gameRoom.IsPlayerTurn(Context.ConnectionId))
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ErrorMessage", "It's not your turn.");
            }

            gameRoom.PutPieceOnTable(Context.ConnectionId, piece);
            await Clients.Group(roomName).SendAsync("PiecesToAddToTable", piece);
            _memoryCache.Set(roomName, gameRoom);
        }
    }
}
