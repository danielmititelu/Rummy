using System.Collections.Generic;

namespace Rummy.Models
{
    public class Player
    {
        public string RoomName { get; set; }
        public List<PieceModel> PiecesOnBoard { get; set; }
    }
}
