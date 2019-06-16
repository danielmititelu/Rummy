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

        public int Id { get; set; }
        public int Number { get; set; }
        public Colors Color { get; set; }

        public PieceModel(int id, int number, Colors color)
        {
            Id = id;
            Number = number;
            Color = color;
        }
    }
}
