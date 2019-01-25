using Rummy.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rummy.Shared.Services
{
    public class PiecesService
    {
        public bool IsSet(IList<Piece> pieces)
        {
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
            return AreSameNumber(pieces) && AreDifferentColors(pieces);
        }

        private static bool IsRun(IList<Piece> pieces)
        {
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

        private static bool AreSameColor(IList<Piece> pieces)
        {
            return pieces.Select(p => p.Color).Distinct().Count() == 1;
        }

        private static bool AreConsecutive(IList<Piece> pieces)
        {
            return !pieces.Select((p, i) => p.Number - i).Distinct().Skip(1).Any();
        }
    }
}
