using System;

namespace Trigger.CRM.Persistent
{
    //SOURCE: http://schroedman.wordpress.com/2012/01/19/short-unique-id-in-c-without-using-guid/
    public static class Schroeder
    {
        public static string UniqueId()
        {
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#";
            string ticks = DateTime.UtcNow.Ticks.ToString();

            var id = "";

            for (var i = 0; i < characters.Length; i += 2)
            {
                if ((i + 2) <= ticks.Length)
                {
                    var number = int.Parse(ticks.Substring(i, 2));
                    if (number > characters.Length - 1)
                    {
                        var one = double.Parse(number.ToString().Substring(0, 1));
                        var two = double.Parse(number.ToString().Substring(1, 1));
                        id += characters[Convert.ToInt32(one)];
                        id += characters[Convert.ToInt32(two)];
                    }
                    else
                        id += characters[number];
                }
            }
            return id;
        }
    }
}