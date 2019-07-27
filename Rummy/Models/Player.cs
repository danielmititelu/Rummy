using System.Collections.Generic;

namespace Rummy.Models
{
    public class Player
    {
        public List<PieceModel> PiecesOnBoard { get; set; }
        public List<List<PieceModel>> Sets { get; set; }
    }
}
