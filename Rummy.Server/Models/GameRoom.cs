using Rummy.Shared.Models;
using System;
using System.Collections.Generic;

namespace Rummy.Server.Models
{
    public class GameRoom
    {
        public List<Piece> Pieces { get; set; }
        public List<Player> Players { get; set; }
        public Player CurrentPlayerTurn { get; set; }
        public string RoomName { get; set; }

        public void InitilizeGame()
        {
            GeneratePieces();
            SharePieces();
            RandomlyChoseFirstPlayer();
        }

        public Piece DrawPiece()
        {
            var random = new Random();

            var i = random.Next(Pieces.Count);
            var piece = Pieces[i];
            Pieces.RemoveAt(i);

            return piece;
        }

        private IEnumerable<Piece> Draw14Pieces()
        {
            for (int i = 0; i <= 13; i++)
            {
                yield return DrawPiece();
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
                player.Pieces = Draw14Pieces();
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
