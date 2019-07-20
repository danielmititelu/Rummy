using Rummy.Models;

namespace Rummy.Services
{
    public class DragAndDropService
    {
        public PieceModel Model { get; set; }

        public void StartDrag(PieceModel model)
        {
            Model = model;
        }
    }
}
