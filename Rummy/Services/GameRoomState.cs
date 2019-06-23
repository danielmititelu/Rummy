using System;
using System.Collections.Generic;

namespace Rummy.Services
{
    public class GameRoomState
    {
        public List<string> Messages { get; set; } = new List<string>();
        public List<string> Players { get; set; } = new List<string>();

        public event Action OnMessageReceive;
        public event Action OnPlayerJoin;

        public void Message(string message)
        {
            Messages.Add(message);
            OnMessageReceive.Invoke();
        }

        public void AddPlayer(string playerName)
        {
            Players.Add(playerName);
            OnPlayerJoin.Invoke();
        }
    }
}
