using NUnit.Framework;
using Rummy.Models;
using Rummy.Services;
using System.Collections.Generic;
using System.Linq;

namespace Rummy.Tests
{
    class RummyEngineTests
    {
        RummyEngine _rummyEngine;

        [SetUp]
        public void BaseSetUp()
        {
            _rummyEngine = new RummyEngine();
        }

        [Test]
        public void GameIsCorrectlyInitializedForTwoPlayers()
        {
            var players = new List<string> { "Player1", "Player2" };
            var rummyModel = _rummyEngine.InitilizeGame(players);
            Assert.AreEqual(2, rummyModel.Players.Count);
            // there are 106 pieces in a game of Rummy and each player draws 14 ->
            // 106 - 14 * 2 = 78
            Assert.AreEqual(78, rummyModel.PiecesPool.Count);
        }

        [Test]
        public void GameIsCorrectlyInitializedForFourPlayers()
        {
            var players = new List<string> { "Player1", "Player2", "Player3", "Player4" };
            var rummyModel = _rummyEngine.InitilizeGame(players);
            Assert.AreEqual(4, rummyModel.Players.Count);
            // there are 106 pieces in a game of Rummy and each player draws 14 ->
            // 106 - 14 * 4 = 50
            Assert.AreEqual(50, rummyModel.PiecesPool.Count);
        }

        [Test]
        public void PlayerCannotDrawWhenItIsNotHisTurn()
        {
            var players = new List<string> { "Player1", "Player2" };
            var rummyModel = _rummyEngine.InitilizeGame(players);
            rummyModel.CurrentPlayerTurn = players[0];
            var (response, _) = _rummyEngine.DrawPieceFromPool(rummyModel, players[1]);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void PlayerCannotPutPieceOnTableIfHeHasNotDrawFirst()
        {
            var players = new List<string> { "Player1", "Player2" };
            var rummyModel = _rummyEngine.InitilizeGame(players);
            rummyModel.CurrentPlayerTurn = players[0];
            rummyModel.HasDrawnPiece = false;
            var piece = rummyModel.Players[players[0]].PiecesOnBoard.First();
            var (response, _) = _rummyEngine.AddPieceOnTable(rummyModel, piece, players[1]);
            Assert.IsFalse(response.Success);
        }
    }
}
