using Rummy.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rummy.Server.Models
{
    public class GameRoom
    {
        public List<Piece> Pieces { get; set; }
        public List<Player> Players { get; set; }
        public Player CurrentPlayerTurn { get; set; }
        public string RoomName { get; set; }
        public List<Piece> PiecesOnTable { get; set; }

        public void InitilizeGame()
        {
            PiecesOnTable = new List<Piece>();
            GeneratePieces();
            SharePieces();
            RandomlyChoseFirstPlayer();
        }

        public Piece DrawPiece(string connectionId)
        {
            var player = Players.First(p => p.ConnectionId == connectionId);
            var random = new Random();
            var i = random.Next(Pieces.Count);
            var piece = Pieces[i];
            Pieces.RemoveAt(i);
            player.Pieces.Append(piece);
            return piece;
        }

        public void PutPieceOnTable(string connectionId, Piece piece)
        {
            var player = Players.First(p => p.ConnectionId == connectionId);
            PiecesOnTable.Add(piece);
            player.Pieces.Remove(piece);
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

        private void Draw14Pieces(string connectionId)
        {
            for (int i = 0; i <= 13; i++)
            {
                DrawPiece(connectionId);
            }
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
                Draw14Pieces(player.ConnectionId);
            }
        }

        private void GeneratePieces()
        {
            Pieces = new List<Piece>();
            var colors = new List<Piece.Colors>
            {
                Piece.Colors.Black,
                Piece.Colors.Blue,
                Piece.Colors.Red,
                Piece.Colors.Yellow
            };

            foreach (var color in colors)
            {
                for (int i = 1; i <= 13; i++)
                {
                    Pieces.Add(new Piece(i, color));
                }
            }

            Pieces.Add(new Piece(0, Piece.Colors.Joker));
            Pieces.Add(new Piece(0, Piece.Colors.Joker));
        }
    }
}
