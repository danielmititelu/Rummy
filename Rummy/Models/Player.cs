using System.Collections.Generic;
namespace Rummy.Models
{
    public class Player
    {
        public string RoomName { get; set; }
        public PieceModel[,] PiecesOnBoard { get; set; }

        public Player()
        {
            PiecesOnBoard = new PieceModel[3, 14];
        }
    }
}
