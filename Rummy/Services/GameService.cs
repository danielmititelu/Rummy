//using Microsoft.Extensions.Caching.Memory;
//using Rummy.Models;
//using System.Collections.Generic;

//namespace Rummy.Services
//{
//    public class GameService
//    {
//        private readonly IMemoryCache _memoryCache;
//        private readonly string roomsKey = "roomsKey";

//        public GameService(IMemoryCache memoryCache)
//        {
//            _memoryCache = memoryCache;
//        }

//        public void CreateRoom(string roomName)
//        {
//            if (!_memoryCache.TryGetValue<List<string>>(roomsKey, out var rooms))
//            {
//                rooms = new List<string>();
//            }
//            rooms.Add(roomName);

//            _memoryCache.Set(roomName,
//                new GameRoom
//                {
//                    RoomName = roomName,
//                    Players = new List<Player>()
//                });
//            _memoryCache.Set(roomsKey, rooms);
//        }

//        public void Draw14Pieces()
//        {

//        }
//    }
//}
