using WorkTelegramBot.BotConsole.Enums;
using WorkTelegramBot.BotConsole.Extensions;

namespace WorkTelegramBot.BotConsole.Services.Interfaces
{
    public class Functions
    {
        private readonly EnumFunction _function;

        public Functions(EnumFunction function)
        {
            _function = function;
        }
     
        /// <summary>Формирование ответного сообщения</summary>
        /// <param name="message">Принятое сообщение от пользователя</param>
        /// <returns></returns>
        public string GetResult(string message)
        {
            switch (_function)
            {
                case EnumFunction.CountSymbols:
                    var length = 0;
                    if (string.IsNullOrWhiteSpace(message))
                        length = 0;
                    else
                        length = message.Length;
                    return $"В Вашем сообщении: {length} символов";
                case EnumFunction.AddingNumbers:
                    var result = message.StringToArrayCounter();
                    if (result != null)
                        return $"Сумма чисел ровна {result}";
                    else
                        return "Входящий текст должен состоять только из чисел и разделенный пробелом";

                default:
                    return "";
            }
        }
    }
}
