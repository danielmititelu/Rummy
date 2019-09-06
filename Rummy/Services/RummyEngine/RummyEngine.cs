using Rummy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rummy.Services
{
    public class RummyEngine
    {
        private readonly PieceTypeCheckerService _pieceTypeChecker;

        public RummyEngine()
        {
            _pieceTypeChecker = new PieceTypeCheckerService();
        }

        public RummyModel InitilizeGame(List<string> playersNames)
        {
            var piecesPool = GeneratePieces();
            var players = playersNames.ToDictionary(p => p, p => new Player());
            foreach (var player in players)
            {
                var (pieces, remainingPieces) = Draw14PiecesFromPool(piecesPool);
                piecesPool = remainingPieces;
                player.Value.PiecesOnBoard = pieces;
                player.Value.Sets = new List<List<PieceModel>>();
            }

            var rummyModel = new RummyModel
            {
                PiecesPool = piecesPool,
                Players = players,
                PlayerOrder = playersNames,
                CurrentPlayerTurn = RandomlyChoseFirstPlayer(players),
                PiecesOnTable = new List<PieceModel>() {
                    new PieceModel(PieceModel.Types.Empty,
                    PieceModel.Locations.Board)}
            };
            return rummyModel;
        }

        public (ResponseWithPiece, RummyModel) DrawPieceFromPool(RummyModel game, string playerName)
        {
            if (!IsPlayerTurn(game, playerName))
            {
                var errorResponse = new ResponseWithPiece { Success = false, Message = "It is not your turn" };
                return (errorResponse, game);
            }

            if (game.HasDrawnPiece)
            {
                var errorResponse = new ResponseWithPiece
                {
                    Success = false,
                    Message = "You have already drawn a piece this turn"
                };
                return (errorResponse, game);
            }

            var random = new Random();
            var i = random.Next(game.PiecesPool.Count);
            var piece = game.PiecesPool[i];
            game.PiecesPool.RemoveAt(i);
            game.Players[playerName].PiecesOnBoard.Add(piece);
            game.HasDrawnPiece = true;
            var response = new ResponseWithPiece { Success = true, Piece = piece };
            return (response, game);
        }

        public (Response, RummyModel) AddPieceOnTable(RummyModel game, PieceModel piece, string playerName)
        {
            if (!IsPlayerTurn(game, playerName))
            {
                var errorResponse = new ResponseWithPiece { Success = false, Message = "It is not your turn" };
                return (errorResponse, game);
            }

            if (!game.HasDrawnPiece)
            {
                var errorResponse = new ResponseWithPiece
                {
                    Success = false,
                    Message = "You must draw a piece first"
                };
                return (errorResponse, game);
            }

            var tmp = piece.ShallowCopy();
            tmp.Location = PieceModel.Locations.Table;
            game.PiecesOnTable.Insert(game.PiecesOnTable.Count - 1, tmp);
            game.Players[playerName].PiecesOnBoard.Remove(piece);
            game.CurrentPlayerTurn = PassTurn(game.PlayerOrder, game.CurrentPlayerTurn);
            game.HasDrawnPiece = false;
            var response = new Response { Success = true };
            return (response, game);
        }

        public (Response, RummyModel) AddSet(RummyModel game, List<PieceModel> set, string playerName)
        {
            if (!IsPlayerTurn(game, playerName))
            {
                var errorTurnResponse = new ResponseWithPiece { Success = false, Message = "It is not your turn" };
                return (errorTurnResponse, game);
            }

            if (_pieceTypeChecker.IsSet(set))
            {
                var newSet = set.Select(p => {
                    var piece = p.ShallowCopy();
                    piece.Location = PieceModel.Locations.piecesSetOnTable;
                    return piece;
                    }).ToList();
                game.Players[playerName].Sets.Add(newSet);
                game.Players[playerName].Score += CalculateScore(set);
                var response = new Response { Success = true };
                return (response, game);
            }

            var errorResponse = new Response
            {
                Success = false,
                Message = "Selected pieces do not form a set"
            };
            return (errorResponse, game);
        }

        private int CalculateScore(List<PieceModel> set)
        {
            var score = 0;
            foreach (var piece in set)
            {
                if (piece.Type == PieceModel.Types.Joker)
                {
                    score += 50;
                }
                else if (piece.Number == 1)
                {
                    score += 25;
                }
                else if (piece.Number >= 10)
                {
                    score += 10;
                }
                else if (piece.Number < 10)
                {
                    score += 5;
                }
            }
            return score;
        }

        public (Response, RummyModel) AddPieceToSet(RummyModel game, int setIndex,
            string setPlayerName, PieceModel piece, string playerName)
        {
            if (!IsPlayerTurn(game, playerName))
            {
                var errorTurnResponse = new ResponseWithPiece { Success = false, Message = "It is not your turn" };
                return (errorTurnResponse, game);
            }

            var set = new List<PieceModel>(game.Players[setPlayerName].Sets[setIndex])
            {
                piece
            };

            if (_pieceTypeChecker.IsSet(set))
            {
                game.Players[playerName].Sets[setIndex].Add(piece);
                var response = new Response { Success = true };
                return (response, game);
            }

            var errorResponse = new Response
            {
                Success = false,
                Message = "Piece does add to set"
            };
            return (errorResponse, game);
        }

        public bool IsPlayerTurn(RummyModel model, string playerName)
        {
            return model.CurrentPlayerTurn == playerName;
        }

        private string PassTurn(List<string> playerOrder, string currentPlayerTurn)
        {
            var index = playerOrder.IndexOf(currentPlayerTurn);
            index++;
            index %= playerOrder.Count;
            return playerOrder[index];
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
                while (pieces.Any(p => p.Color == piece.Color
                    && p.Number == piece.Number))
                {
                    index = random.Next(remainingPieces.Count);
                    piece = remainingPieces[index];
                }

                remainingPieces.Remove(piece);
                pieces.Add(piece);
            }

            return (pieces, piecesPool);
        }

        private string RandomlyChoseFirstPlayer(Dictionary<string, Player> players)
        {
            var random = new Random();
            var i = random.Next(players.Count);
            return players.ElementAt(i).Key;
        }

        private List<PieceModel> GeneratePieces()
        {
            var piecesPool = new List<PieceModel>();
            var colors = new List<PieceModel.Colors>
            {
                PieceModel.Colors.Black,
                PieceModel.Colors.Blue,
                PieceModel.Colors.Red,
                PieceModel.Colors.Yellow,
            };

            var id = 0;
            for (int y = 0; y < 2; y++)
            {
                foreach (var color in colors)
                {
                    for (int i = 1; i <= 13; i++)
                    {
                        id++;
                        piecesPool.Add(new PieceModel(i, color, id));
                    }
                }
            }

            piecesPool.Add(new PieceModel(PieceModel.Types.Joker));
            piecesPool.Add(new PieceModel(PieceModel.Types.Joker));
            return piecesPool;
        }
    }
}
