using Blazor.Extensions;
using Rummy.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rummy.Client.Services
{
    public class ChatService
    {
        private HubConnection connection;
        private readonly AuthService _authService;
        private string _roomName;

        public ChatService(AuthService authService)
        {
            _authService = authService;
            connection = new HubConnectionBuilder().WithUrl("/chathub").Build();
        }

        public async Task StartAsync()
        {
            await connection.StartAsync();
        }

        public async Task CreateRoom(string roomName)
        {
            _roomName = roomName;
            await connection.InvokeAsync("CreateRoom", roomName);
        }

        public async Task JoinRoom(string roomName)
        {
            _roomName = roomName;
            await connection.InvokeAsync("JoinRoom", _roomName);
        }

        public async Task<List<string>> GetRooms()
        {
            return await connection.InvokeAsync<List<string>>("GetRooms");
        }

        public async Task SendMessage(string message)
        {
            _authService.TryGetUsername(out var username);
            await connection.InvokeAsync("SendMessageToRoom", _roomName, username, message);
        }

        public async Task StartGame()
        {
            await connection.InvokeAsync("StartGame", _roomName);
        }

        public void OnBroadcastMessage(Func<string, string, Task> onBroadcastMessage)
        {
            connection.On("BroadcastMessage", onBroadcastMessage);
        }

        public void OnReceivePiecesToAddToBoard(Func<List<Piece>, Task> OnReceivePiecesToAddToBoard)
        {
            connection.On("PiecesToAddToBoard", OnReceivePiecesToAddToBoard);
        }

        public void OnReceivePiecesToAddToTable(Func<List<Piece>, Task> OnReceivePiecesToAddToTable)
        {
            connection.On("PiecesToAddToTable", OnReceivePiecesToAddToTable);
        }
    }
}
