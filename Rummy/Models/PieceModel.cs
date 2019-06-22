namespace Rummy.Models
{
    public class PieceModel
    {
        public enum Colors
        {
            Black,
            Red,
            Blue,
            Yellow
        }

        public enum Types
        {
            Empty,
            Normal,
            Joker
        }

        public enum Locations
        {
            Board,
            PiecesToDraw,
            Table
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Locations Location { get; set; }
        public int Number { get; set; }
        public Colors Color { get; set; }
        public Types Type { get; set; }

        public PieceModel(int number, Colors color,  Locations location,
             int x, int y = 0)
        {
            X = x;
            Y = y;
            Location = location;
            Number = number;
            Color = color;
            Type = Types.Normal;
        }

        public PieceModel(Types type, Locations location,
            int x, int y = 0)
        {
            X = x;
            Y = y;
            Location = location;
            Type = type;
        }

        public PieceModel(int number, Colors color)
        {
            Number = number;
            Color = color;
            Type = Types.Normal;
        }

        public PieceModel(Types type)
        {
            Type = type;
        }
    }
}
