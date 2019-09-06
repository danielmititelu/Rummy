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
        }

        public enum Types
        {
            Empty,
            Normal,
            Joker,
            FaceDown
        }

        public enum Locations
        {
            Board,
            PiecesToDraw,
            Table,
            piecesSetOnTable,
            SetToDropOnTable
        }

        public Locations Location { get; set; }
        public int Number { get; set; }
        public Colors Color { get; set; }
        public Types Type { get; set; }
        public int Id { get; set; }
        public string SetPlayerName { get; set; }
        public int SetIndex { get; set; }

        public PieceModel(int number, Colors color, Locations location)
        {
            Location = location;
            Number = number;
            Color = color;
            Type = Types.Normal;
         
        }

        public PieceModel(Types type, Locations location)
        {
            Location = location;
            Type = type;
        }

        public PieceModel(int number, Colors color, int id)
        {
            Number = number;
            Color = color;
            Type = Types.Normal;
            Id = id;
        }

        public PieceModel(Types type)
        {
            Type = type;
        }

        public PieceModel ShallowCopy()
        {
            return (PieceModel)MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PieceModel item))
            {
                return false;
            }

            return Type.Equals(item.Type) &&
                Color.Equals(item.Color) &&
                Number.Equals(item.Number) &&
                Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Type.GetHashCode();
                hash = (hash * 7) + Color.GetHashCode();
                hash = (hash * 7) + Number.GetHashCode();
                hash = (hash * 7) + Id.GetHashCode();
                return hash;
            }
        }
    }
}
