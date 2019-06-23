using System;
using System.Collections.Generic;

namespace Rummy.Services
{
    public static class RoomState
    {
        public static Dictionary<string, GameRoomState> Rooms { get; set; } =
            new Dictionary<string, GameRoomState>();

        public static event Action OnRoomAdded;

        public static void AddRoom(string roomName, string playerName)
        {
            Rooms.Add(roomName, new GameRoomState
            {
                Players = new List<string> { playerName }
            });
            OnRoomAdded.Invoke();
        }

        public static void JoinRoom(string roomName, string playerName)
        {
            Rooms[roomName].AddPlayer(playerName);
        }
    }
}
