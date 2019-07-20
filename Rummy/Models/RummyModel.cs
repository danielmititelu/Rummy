using System.Collections.Generic;

namespace Rummy.Models
{
    public class RummyModel
    {
        public Dictionary<string, Player> Players { get; set; } =
            new Dictionary<string, Player>();
        public Player CurrentPlayerTurn { get; set; }
        public List<PieceModel> PiecesPool { get; set; }
        public List<PieceModel> PiecesOnTable { get; set; }
    }
}
