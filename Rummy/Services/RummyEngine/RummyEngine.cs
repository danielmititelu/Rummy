using Rummy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rummy.Services
{
    public class RummyEngine
    {
        public RummyModel InitilizeGame(List<string> playersNames)
        {
            var piecesPool = GeneratePieces();
            var players = playersNames.ToDictionary(p => p, p => new Player());
            foreach (var player in players)
            {
                var (pieces, remainingPieces) = Draw14PiecesFromPool(piecesPool);
                piecesPool = remainingPieces;
                player.Value.PiecesOnBoard = InitilizePiecesOnBoard(pieces);
            }

            var rummyModel = new RummyModel
            {
                PiecesPool = piecesPool,
                Players = players,
                CurrentPlayerTurn = RandomlyChoseFirstPlayer(players),
                PiecesOnTable = new List<PieceModel>() {
                    new PieceModel(PieceModel.Types.Empty,
                    PieceModel.Locations.Board, 0)}
            };
            return rummyModel;
        }
        public PieceModel DrawPieceFromPool(RummyModel model)
        {
            var random = new Random();
            var i = random.Next(model.PiecesPool.Count);
            var piece = model.PiecesPool[i];
            model.PiecesPool.RemoveAt(i); // ??
            return piece;
        }

        public RummyModel AddPieceOnTable(RummyModel game, PieceModel piece)
        {
            var tmp = piece.ShallowCopy();
            game.PiecesOnTable.Insert(game.PiecesOnTable.Count - 1, tmp);
            return game;
        }

        public bool IsPlayerTurn(RummyModel model, string playerName)
        {
            var player = model.Players[playerName];
            if (model.CurrentPlayerTurn == player)
            {
                return true;
            }
            return false;
        }

        private (List<PieceModel>, List<PieceModel>) Draw14PiecesFromPool(List<PieceModel> piecesPool)
        {
            var pieces = new List<PieceModel>();
            var remainingPieces = piecesPool;
            for (int i = 0; i <= 13; i++)
            {
                var random = new Random();
                var index = random.Next(remainingPieces.Count);
                var piece = remainingPieces[index];
                remainingPieces.RemoveAt(i);
                pieces.Add(piece);
            }

            return (pieces, piecesPool);
        }

        private PieceModel[,] InitilizePiecesOnBoard(List<PieceModel> pieces)
        {
            var piecesOnBoard = new PieceModel[3, 14];
            for (int i = 0; i < pieces.Count; i++)
            {
                var piece = pieces[i];
                piecesOnBoard[0, i] = new PieceModel(piece.Number, piece.Color,
                    PieceModel.Locations.Board, i, 0);
            }

            return piecesOnBoard;
        }

        private Player RandomlyChoseFirstPlayer(Dictionary<string, Player> players)
        {
            var random = new Random();
            var i = random.Next(players.Count);
            return players.ElementAt(i).Value;
        }

        private List<PieceModel> GeneratePieces()
        {
            var piecesPool = new List<PieceModel>();
            var colors = new List<PieceModel.Colors>
            {
                PieceModel.Colors.Black,
                PieceModel.Colors.Blue,
                PieceModel.Colors.Red,
                PieceModel.Colors.Yellow
            };

            for (int y = 0; y < 2; y++)
            {
                foreach (var color in colors)
                {
                    for (int i = 1; i <= 13; i++)
                    {
                        piecesPool.Add(new PieceModel(i, color));
                    }
                }
            }

            piecesPool.Add(new PieceModel(PieceModel.Types.Joker));
            piecesPool.Add(new PieceModel(PieceModel.Types.Joker));
            return piecesPool;
        }
    }
}
