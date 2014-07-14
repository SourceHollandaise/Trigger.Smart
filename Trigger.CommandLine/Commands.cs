using System;

namespace Trigger.CommandLine
{
    struct Commands
    {
        public static string ADD = "ADD";
        public static string DEL = "DEL >";
        public static string LST = "LST >";
        public static string EXIT = "EXIT";
    }

    public static class ConsolePasswordMask
    {
        public static string Enter()
        {
            string input = "";
           
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace)
                {
                    input += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    input = input.Length > 0 ? input.Remove(input.Length - 1) : input.Remove(input.Length);

                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);



            var result = input.Replace("\r\n", "");

            return result;

        }
    }
}
