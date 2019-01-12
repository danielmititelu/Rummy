﻿using Blazor.Extensions;
using System;
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
        public async Task CreateRoom(string roomName)
        {
            _roomName = roomName;
            await connection.StartAsync();
            await connection.InvokeAsync("CreateRoom", roomName);
            await connection.InvokeAsync("JoinRoom", roomName);
        }

        public async Task JoinRoom(string roomName)
        {
            _roomName = roomName;
            await connection.StartAsync();
            await connection.InvokeAsync("JoinRoom", _roomName);
        }

        public void OnBroadcastMessage(Func<string,string,Task> onBroadcastMessage)
        {
            connection.On("ReceiveMessage", onBroadcastMessage);
        }

        public async Task SendMessage(string message)
        {
            _authService.TryGetUsername(out var username);
            await connection.InvokeAsync("SendMessageToRoom", _roomName, username, message);
        }
    }
}