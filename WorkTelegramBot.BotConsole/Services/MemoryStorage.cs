using System.Collections.Concurrent;
using WorkTelegramBot.BotConsole.Enums;
using WorkTelegramBot.BotConsole.Models;
using WorkTelegramBot.BotConsole.Services.Interfaces;

namespace WorkTelegramBot.BotConsole.Services
{
    public class MemoryStorage : IStorage
    {
        /// <summary>Хранилище сессий</summary>
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            // Возвращаем сессию по ключу, если она существует
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            // Создаем и возвращаем новую, если такой не было
            var newSession = new Session();
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
