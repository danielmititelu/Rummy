﻿using System;
using System.Collections.Generic;

namespace Rummy.Models
{
    public class RummyModel
    {
        public Dictionary<string, Player> Players { get; set; }
        public List<string> PlayerOrder { get; set; }
        public string CurrentPlayerTurn { get; set; }
        public bool HasDrawnPiece { get; set; }
        public List<PieceModel> PiecesPool { get; set; }
        public List<PieceModel> PiecesOnTable { get; set; }
        public bool GameEnded { get; set; }
    }
}
