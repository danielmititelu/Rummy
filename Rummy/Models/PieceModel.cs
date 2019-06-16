namespace Rummy.Models
{
    public class PieceModel
    {
        public enum Colors
        {
            Black,
            Red,
            Blue,
            Yellow,
            Joker
        }

        public int Number { get; set; }
        public Colors Color { get; set; }

        public PieceModel(int number, Colors color)
        {
            Number = number;
            Color = color;
        }
    }
}
