using System;
using System.Collections.Generic;

namespace Rummy.Services
{
    public static class AllRoomsState
    {
        public static Dictionary<string, RoomState> Rooms { get; set; } =
            new Dictionary<string, RoomState>();

        public static event Action OnRoomAdded;

        public static void AddRoom(string roomName, string playerName)
        {
            Rooms.Add(roomName, new RoomState
            {
                Players = new List<string> { playerName },
            });
            OnRoomAdded.Invoke();
        }

        public static void JoinRoom(string roomName, string playerName)
        {
            Rooms[roomName].AddPlayer(playerName);
        }
    }
}
