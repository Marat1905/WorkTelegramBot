using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WorkTelegramBot.BotConsole.Enums;
using WorkTelegramBot.BotConsole.Extensions;
using WorkTelegramBot.BotConsole.Services.Interfaces;

namespace WorkTelegramBot.BotConsole.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).SelectedFunction = callbackQuery.Data.ConvertToEnum<EnumFunction>();

            // Генерим информационное сообщение
            string functionText = callbackQuery.Data.ConvertToEnum<EnumFunction>() switch
            {
                EnumFunction.CountSymbols => "Подсчет количества символов",
                EnumFunction.AddingNumbers => "Вычисление суммы чисел",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбрано - {functionText}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
