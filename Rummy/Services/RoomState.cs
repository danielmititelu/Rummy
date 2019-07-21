using Rummy.Models;
using System;
using System.Collections.Generic;

namespace Rummy.Services
{
    public class RoomState
    {
        private readonly RummyEngine _rummyEngine;

        public RoomState()
        {
            _rummyEngine = new RummyEngine();
        }

        public List<string> Messages { get; set; } = new List<string>();
        public List<string> Players { get; set; } = new List<string>();
        public RummyModel Game { get; set; }

        public event Action OnMessageReceive;
        public event Action OnPlayerJoin;
        public event Action OnStartGame;
        public event Action OnDropPieceOnTable;

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

        public void StartGame()
        {
            // todo: save this to memory cache/redis
            Game = _rummyEngine.InitilizeGame(Players);
            OnStartGame.Invoke();
        }

        public void DropPieceOnTable(PieceModel source, string playerName)
        {
            Game = _rummyEngine.AddPieceOnTable(Game, source);
            OnDropPieceOnTable.Invoke();
        }
    }
}
