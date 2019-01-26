﻿using Rummy.Shared.Models;
using System.Collections.Generic;

namespace Rummy.Server.Models
{
    public class Player
    {
        public string ConnectionId { get; set; }
        public string RoomName { get; set; }
        public IList<Piece> Pieces { get; set; } 

        public Player()
        {
            Pieces = new List<Piece>();
        }
    }
}
