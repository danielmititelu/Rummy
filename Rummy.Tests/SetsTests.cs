using NUnit.Framework;
using Rummy.Shared.Models;
using Rummy.Shared.Services;
using System.Collections.Generic;

namespace Rummy.Tests
{
    public class SetsTests
    {
        [TestCase(1, 2, 3, Piece.Colors.Black)]
        [TestCase(11, 12, 13, Piece.Colors.Blue)]
        [TestCase(9, 10, 11, Piece.Colors.Red)]
        [TestCase(5, 6, 7, Piece.Colors.Yellow)]
        [TestCase(12, 13, 1, Piece.Colors.Blue)]
        public void ReturnTrueGivenCorrectRunOfThree(int p1, int p2, int p3, Piece.Colors color)
        {
            var pieces = new List<Piece>
            {
                new Piece(p1, color),
                new Piece(p2, color),
                new Piece(p3, color)
            };

            var piecesService = new PiecesService();
            Assert.IsTrue(piecesService.IsSet(pieces));
        }

        [TestCase(1, 2, 4, Piece.Colors.Black)]
        [TestCase(9, 12, 13, Piece.Colors.Blue)]
        [TestCase(9, 10, 4, Piece.Colors.Red)]
        [TestCase(5, 6, 2, Piece.Colors.Yellow)]
        [TestCase(10, 11, 1, Piece.Colors.Yellow)]
        public void ReturnFalseGivenIncorrectRunOfThree(int p1, int p2, int p3, Piece.Colors color)
        {
            var pieces = new List<Piece>
            {
                new Piece(p1, color),
                new Piece(p2, color),
                new Piece(p3, color)
            };

            var piecesService = new PiecesService();
            Assert.IsFalse(piecesService.IsSet(pieces));
        }

        [TestCase(Piece.Colors.Blue, Piece.Colors.Yellow, Piece.Colors.Red, 7)]
        [TestCase(Piece.Colors.Yellow, Piece.Colors.Red, Piece.Colors.Black, 8)]
        [TestCase(Piece.Colors.Red, Piece.Colors.Black, Piece.Colors.Blue, 13)]
        [TestCase(Piece.Colors.Black, Piece.Colors.Blue, Piece.Colors.Yellow, 1)]
        public void ReturnTrueGivenCorrectGroupOfThree(Piece.Colors c1, Piece.Colors c2, Piece.Colors c3, int n)
        {
            var pieces = new List<Piece>
            {
                new Piece(n, c1),
                new Piece(n, c2),
                new Piece(n, c3)
            };

            var piecesService = new PiecesService();
            Assert.IsTrue(piecesService.IsSet(pieces));
        }

        [TestCase(Piece.Colors.Blue, Piece.Colors.Blue, Piece.Colors.Red, 7)]
        [TestCase(Piece.Colors.Black, Piece.Colors.Black, Piece.Colors.Black, 1)]
        public void ReturnTrueGivenIncorrectGroupOfThree(Piece.Colors c1, Piece.Colors c2, Piece.Colors c3, int n)
        {
            var pieces = new List<Piece>
            {
                new Piece(n, c1),
                new Piece(n, c2),
                new Piece(n, c3)
            };

            var piecesService = new PiecesService();
            Assert.IsFalse(piecesService.IsSet(pieces));
        }
    }
}
