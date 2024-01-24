namespace WorkTelegramBot.BotConsole.Extensions
{
    public static class StringExtension
    {
        public static int? StringToArrayCounter(this string value) 
        {
            try
            {
                string[] splitted = value.Split(' ');
                int[] nums = new int[splitted.Length];
                for (int i = 0; i < splitted.Length; i++)
                {
                    if(int.TryParse(splitted[i],out int result))
                        nums[i]=result;
                    else
                        nums[i] = 0;
                }
                return nums.Sum();

                //int[] array = value.Split(' ').Select(int.Parse).ToArray();
                //return array.Sum();
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
