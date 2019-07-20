using NUnit.Framework;
using Rummy.Models;
using Rummy.Services;
using System.Collections.Generic;

namespace Tests
{
    public class PieceTypeCheckerTests
    {
        [TestCase(1, 2, 3, PieceModel.Colors.Black)]
        [TestCase(11, 12, 13, PieceModel.Colors.Blue)]
        [TestCase(9, 10, 11, PieceModel.Colors.Red)]
        [TestCase(5, 6, 7, PieceModel.Colors.Yellow)]
        [TestCase(12, 13, 1, PieceModel.Colors.Blue)]
        public void ReturnTrueGivenCorrectRunOfThree(int p1, int p2, int p3, PieceModel.Colors color)
        {
            var pieces = new List<PieceModel>
            {
                new PieceModel(p1, color),
                new PieceModel(p2, color),
                new PieceModel(p3, color)
            };

            var piecesService = new PieceTypeCheckerService();
            Assert.IsTrue(piecesService.IsSet(pieces));
        }

        [TestCase(1, 2, 4, PieceModel.Colors.Black)]
        [TestCase(9, 12, 13, PieceModel.Colors.Blue)]
        [TestCase(9, 10, 4, PieceModel.Colors.Red)]
        [TestCase(5, 6, 2, PieceModel.Colors.Yellow)]
        [TestCase(10, 11, 1, PieceModel.Colors.Yellow)]
        public void ReturnFalseGivenIncorrectRunOfThree(int p1, int p2, int p3, PieceModel.Colors color)
        {
            var pieces = new List<PieceModel>
            {
                new PieceModel(p1, color),
                new PieceModel(p2, color),
                new PieceModel(p3, color)
            };

            var piecesService = new PieceTypeCheckerService();
            Assert.IsFalse(piecesService.IsSet(pieces));
        }

        [TestCase(PieceModel.Colors.Blue, PieceModel.Colors.Yellow, PieceModel.Colors.Red, 7)]
        [TestCase(PieceModel.Colors.Yellow, PieceModel.Colors.Red, PieceModel.Colors.Black, 8)]
        [TestCase(PieceModel.Colors.Red, PieceModel.Colors.Black, PieceModel.Colors.Blue, 13)]
        [TestCase(PieceModel.Colors.Black, PieceModel.Colors.Blue, PieceModel.Colors.Yellow, 1)]
        public void ReturnTrueGivenCorrectGroupOfThree(PieceModel.Colors c1, PieceModel.Colors c2, PieceModel.Colors c3, int n)
        {
            var pieces = new List<PieceModel>
            {
                new PieceModel(n, c1),
                new PieceModel(n, c2),
                new PieceModel(n, c3)
            };

            var piecesService = new PieceTypeCheckerService();
            Assert.IsTrue(piecesService.IsSet(pieces));
        }

        [TestCase(PieceModel.Colors.Blue, PieceModel.Colors.Blue, PieceModel.Colors.Red, 7)]
        [TestCase(PieceModel.Colors.Black, PieceModel.Colors.Black, PieceModel.Colors.Black, 1)]
        public void ReturnTrueGivenIncorrectGroupOfThree(PieceModel.Colors c1, PieceModel.Colors c2, PieceModel.Colors c3, int n)
        {
            var pieces = new List<PieceModel>
            {
                new PieceModel(n, c1),
                new PieceModel(n, c2),
                new PieceModel(n, c3)
            };

            var piecesService = new PieceTypeCheckerService();
            Assert.IsFalse(piecesService.IsSet(pieces));
        }

        [Test]
        public void ReturnTrueGivenCorrectGroupOfThreeWithJoker()
        {
            var pieces = new List<PieceModel>
            {
                new PieceModel(PieceModel.Types.Joker),
                new PieceModel(6, PieceModel.Colors.Blue),
                new PieceModel(6, PieceModel.Colors.Red)
            };

            var piecesService = new PieceTypeCheckerService();
            Assert.IsTrue(piecesService.IsSet(pieces));
        }

        [Test]
        public void ReturnTrueGivenCorrectRunOfThreeWithJoker()
        {
            var pieces = new List<PieceModel>
            {
                new PieceModel(PieceModel.Types.Joker),
                new PieceModel(2, PieceModel.Colors.Red),
                new PieceModel(3, PieceModel.Colors.Red)
            };

            var piecesService = new PieceTypeCheckerService();
            Assert.IsTrue(piecesService.IsSet(pieces));
        }

        [Test]
        public void ReturnTrueGivenCorrectRunOfThreeWithJokerInTheMiddle()
        {
            var pieces = new List<PieceModel>
            {
                new PieceModel(2, PieceModel.Colors.Red),
                new PieceModel(PieceModel.Types.Joker),
                new PieceModel(4, PieceModel.Colors.Red)
            };

            var piecesService = new PieceTypeCheckerService();
            Assert.IsTrue(piecesService.IsSet(pieces));
        }

        [Test]
        public void ReturnTrueGivenCorrectRunOfThreeWithJokerEdgeCase()
        {
            var pieces = new List<PieceModel>
            {
                new PieceModel(12, PieceModel.Colors.Red),
                new PieceModel(PieceModel.Types.Joker),
                new PieceModel(1, PieceModel.Colors.Red)
            };

            var piecesService = new PieceTypeCheckerService();
            Assert.IsTrue(piecesService.IsSet(pieces));
        }
    }
}