﻿using Rummy.Models;
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

        public Response DropPieceOnTable(PieceModel source, string playerName)
        {
            var (response, game) = _rummyEngine.AddPieceOnTable(Game, source, playerName);
            Game = game;
            OnDropPieceOnTable.Invoke();
            return response;
        }

        public ResponseWithPiece DrawPiece(string playerName)
        {
            var (response, game) = _rummyEngine.DrawPieceFromPool(Game, playerName);
            Game = game;
            return response;
        }

        public List<PieceModel> GetPlayerBoardPieces(string playerName)
        {
            return Game.Players[playerName].PiecesOnBoard;
        }

        public string GetCurrentPlayer()
        {
            return Game.CurrentPlayerTurn;
        }
    }
}