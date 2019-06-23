using System.Collections.Generic;
namespace Rummy.Models
{
    public class Player
    {
        public string ConnectionId { get; set; }
        public string RoomName { get; set; }
        public PieceModel[,] PiecesOnBoard { get; set; }

        public Player()
        {
            PiecesOnBoard = new PieceModel[3, 14];
        }

        public void AddPieces(List<PieceModel> pieces)
        {
            int i = 1;
            foreach (var piece in pieces)
            {
                PiecesOnBoard[0, i] = new PieceModel(piece.Number, piece.Color,
                    PieceModel.Locations.Board, i, 0);
                i++;
            }
        }
    }
}
