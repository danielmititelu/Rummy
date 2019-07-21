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

        public PieceModel(int number, Colors color, Locations location,
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

        public PieceModel ShallowCopy()
        {
            return (PieceModel)this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PieceModel item))
            {
                return false;
            }

            return Type.Equals(item.Type) &&
                Color.Equals(item.Color) &&
                Number.Equals(item.Color);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Type.GetHashCode();
                hash = (hash * 7) + Color.GetHashCode();
                hash = (hash * 7) + Number.GetHashCode();
                return hash;
            }
        }
    }
}
