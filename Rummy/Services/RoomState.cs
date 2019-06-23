using System;
using System.Collections.Generic;

namespace Rummy.Services
{
    public static class RoomState
    {
        // To do: Isolate rooms to receive messages only from the players that are in that room 
        public static List<string> Rooms { get; set; } = new List<string>();
        public static List<string> Messages { get; set; } = new List<string>();

        public static event Action OnMessageReceive;
        public static event Action OnRoomAdded;

        public static void Message(string message)
        {
            Messages.Add(message);
            OnMessageReceive.Invoke();
        }

        public static void AddRoom(string roomName)
        {
            Rooms.Add(roomName);
            OnRoomAdded.Invoke();
        }
    }
}
