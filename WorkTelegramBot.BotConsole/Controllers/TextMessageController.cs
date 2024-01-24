using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WorkTelegramBot.BotConsole.Enums;
using WorkTelegramBot.BotConsole.Extensions;
using WorkTelegramBot.BotConsole.Services.Interfaces;

namespace WorkTelegramBot.BotConsole.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Вычисление кол-во символов" , EnumFunction.CountSymbols.ConvertToString()),
                    });
                    buttons.Add(new[]
                   {
                        InlineKeyboardButton.WithCallbackData($"Вычисление суммы чисел" , EnumFunction.AddingNumbers.ConvertToString())
                    });
                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b> Наш бот считает количество символов в тексте или вычисляет суммы чисел</b> {Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    Functions functions = new Functions(_memoryStorage.GetSession(message.Chat.Id).SelectedFunction);

                    var result = functions.GetResult(message.Text);
                    
                    if(string.IsNullOrWhiteSpace(result))
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Выберите в главном меню функцию", cancellationToken: ct);
                    else
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: ct);
                    break;
            }
        }
    }
}
