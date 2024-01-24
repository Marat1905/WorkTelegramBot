﻿namespace WorkTelegramBot.BotConsole.Extensions
{
    public static class EnumExtension
    {
        public static String ConvertToString(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }

        public static EnumType ConvertToEnum<EnumType>(this string enumValue)
        {
            return (EnumType)Enum.Parse(typeof(EnumType), enumValue);
        }
    }
}
