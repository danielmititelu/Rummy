using System;
using System.Collections.Generic;

namespace Rummy.JsInterop
{
    public static class AppState
    {
        public static event Action OnPiecesOnTableChange;
        public static List<string> PiecesOnTable { get; set; } = new List<string>();
        public static double Y { get; set; }

        public static void EndTurnWithPiece(string pieceId)
        {
            PiecesOnTable.Add(pieceId);
            OnPiecesOnTableChange.Invoke();
        }
    }
}
