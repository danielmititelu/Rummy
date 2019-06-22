using System;
using System.Collections.Generic;
using System.Linq;

namespace Rummy.Models
{
    public class GameRoom
    {
        public List<PieceModel> PiecesPool { get; set; }
        public List<Player> Players { get; set; }
        public Player CurrentPlayerTurn { get; set; }
        public string RoomName { get; set; }
        public List<PieceModel> PiecesOnTable { get; set; }

        public void InitilizeGame()
        {
            PiecesOnTable = new List<PieceModel>();
            GeneratePieces();
            SharePieces();
            RandomlyChoseFirstPlayer();
        }

        public PieceModel DrawPiece(string connectionId)
        {
            var player = Players.First(p => p.ConnectionId == connectionId);
            var random = new Random();
            var i = random.Next(PiecesPool.Count);
            var piece = PiecesPool[i];
            PiecesPool.RemoveAt(i);
            return piece;
        }

        public bool IsPlayerTurn(string connectionId)
        {
            var player = Players.First(p => p.ConnectionId == connectionId);
            if (CurrentPlayerTurn.ConnectionId == player.ConnectionId)
            {
                return true;
            }
            return false;
        }

        private List<PieceModel> Draw14Pieces(string connectionId)
        {
            var pieces = new List<PieceModel>();
            for (int i = 0; i <= 13; i++)
            {
                var piece = DrawPiece(connectionId);
                pieces.Add(piece);
            }

            return pieces;
        }

        private void RandomlyChoseFirstPlayer()
        {
            var random = new Random();
            var i = random.Next(Players.Count);
            CurrentPlayerTurn = Players[i];
        }

        private void SharePieces()
        {
            foreach (var player in Players)
            {
                var pieces = Draw14Pieces(player.ConnectionId);
                player.AddPieces(pieces);
            }
        }

        private void GeneratePieces()
        {
            PiecesPool = new List<PieceModel>();
            var colors = new List<PieceModel.Colors>
            {
                PieceModel.Colors.Black,
                PieceModel.Colors.Blue,
                PieceModel.Colors.Red,
                PieceModel.Colors.Yellow
            };

            foreach (var color in colors)
            {
                for (int i = 1; i <= 13; i++)
                {
                    PiecesPool.Add(new PieceModel(i, color));
                }
            }

            PiecesPool.Add(new PieceModel(PieceModel.Types.Joker));
            PiecesPool.Add(new PieceModel(PieceModel.Types.Joker));
        }
    }
}
