using Rummy.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace Rummy.Shared.Services
{
    public class PiecesService
    {
        public bool IsSet(IList<Piece> pieces)
        {
            if (pieces.Count < 3)
            {
                return false;
            }

            if (IsRun(pieces))
            {
                return true;
            }

            if (IsGroup(pieces))
            {
                return true;
            }

            return false;
        }

        private bool IsGroup(IList<Piece> pieces)
        {
            if (pieces.Any(p => p.Color == Piece.Colors.Joker))
            {
                var piecesWithoutJoker = pieces.Where(p => p.Color != Piece.Colors.Joker).ToList();
                return AreSameNumber(piecesWithoutJoker) && AreDifferentColors(piecesWithoutJoker);
            }
            return AreSameNumber(pieces) && AreDifferentColors(pieces);
        }

        private bool IsRun(IList<Piece> pieces)
        {
            if (pieces.Any(p => p.Color == Piece.Colors.Joker))
            {
                var piecesWithoutJoker = pieces.Where(p => p.Color != Piece.Colors.Joker).ToList();
                if (AreConsecutive(piecesWithoutJoker) && AreSameColor(piecesWithoutJoker))
                {
                    return true;
                }
                else if (AreConsecutive(piecesWithoutJoker, 2) && AreSameColor(piecesWithoutJoker))
                {
                    return true;
                }
                return piecesWithoutJoker.First().Number == 12 &&
                       piecesWithoutJoker.Last().Number == 1 &&
                       AreSameColor(piecesWithoutJoker);
            }

            var count = pieces.Count();
            if (AreSameColor(pieces))
            {
                if (pieces.Last().Number == 1 &&
                    pieces.ElementAt(count - 2).Number == 13)
                {
                    return AreConsecutive(pieces.Take(count - 1).ToList());
                }
                else
                {
                    return AreConsecutive(pieces);
                }
            }
            else
            {
                return false;
            }
        }

        private bool AreDifferentColors(IList<Piece> pieces)
        {
            return pieces.Select(p => p.Color).Distinct().Count() == pieces.Count();
        }

        private bool AreSameNumber(IList<Piece> pieces)
        {
            return pieces.Select(p => p.Number).Distinct().Count() == 1;
        }

        private bool AreSameColor(IList<Piece> pieces)
        {
            return pieces.Select(p => p.Color).Distinct().Count() == 1;
        }

        private bool AreConsecutive(IList<Piece> pieces)
        {
            return !pieces.Select((p, i) => p.Number - i).Distinct().Skip(1).Any();
        }

        private bool AreConsecutive(IList<Piece> pieces, int step)
        {
            var temp = pieces.Select((p, j) => p.Number - j).ToList();
            for (int i = 0; i < step - 1; i++)
            {
                temp = temp.Select((p, j) => p - j).ToList();
            }

            return !temp.Distinct().Skip(1).Any();
        }
    }
}
