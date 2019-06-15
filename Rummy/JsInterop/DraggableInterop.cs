using Microsoft.JSInterop;

namespace Rummy.JsInterop
{
    public static class DraggableInterop
    {
        [JSInvokable]
        public static string EndTurnWithPiece(string pieceId)
        {
            AppState.EndTurnWithPiece(pieceId);
            return "Received";
        }
    }
}
