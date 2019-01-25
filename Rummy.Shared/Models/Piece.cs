namespace Rummy.Shared.Models
{
    public class Piece
    {
        public enum Colors
        {
            Black,
            Red,
            Blue,
            Yellow
        }

        public int Number { get; set; }
        public Colors Color { get; set; }

        public Piece(int number, Colors color)
        {
            Number = number;
            Color = color;
        }
    }
}
