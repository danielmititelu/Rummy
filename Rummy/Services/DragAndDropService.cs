using Rummy.Models;

namespace Rummy.Services
{
    public class DragAndDropService
    {
        public PieceModel Model { get; set; }
        public string Zone { get; set; }

        public void StartDrag(PieceModel model, string zone)
        {
            Model = model;
            Zone = zone;
        }

        public bool Accepts(string zone)
        {
            return Zone == zone;
        }
    }
}
