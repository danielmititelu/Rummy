namespace Rummy.Shared.Models
{
    public class Piece
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

        public Piece(int number, Colors color)
        {
            Number = number;
            Color = color;
        }

        public Piece()
        {
            //Error said this class needs a public parameterless 
            //constructor, so here it is
        }
    }
}
