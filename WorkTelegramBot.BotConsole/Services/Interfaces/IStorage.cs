using WorkTelegramBot.BotConsole.Models;

namespace WorkTelegramBot.BotConsole.Services.Interfaces
{
    public interface IStorage
    {
        /// <summary>
        /// Получение сессии пользователя по идентификатору
        /// </summary>
        Session GetSession(long chatId);
    }
}
