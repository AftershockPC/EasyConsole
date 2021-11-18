using System;

namespace EasyConsole
{
    public static class Input
    {
        public static int ReadInt(string prompt, int min, int max)
        {
            Output.DisplayPrompt(prompt);
            return ReadInt(min, max);
        }

        public static int ReadInt(int min, int max)
        {
            bool lessThan9 = (max - min) < 10;

            int value = ReadInt(lessThan9);

            while (value < min || value > max)
            {
                Output.DisplayPrompt("Please enter an integer between {0} and {1} (inclusive)", min, max);
                value = ReadInt(lessThan9);
            }

            return value;
        }

        public static int ReadInt(bool canReadKey = true)
        {
            string input;
            int value;

            while (true)
            {
                if (canReadKey)
                {
                    input = Console.ReadKey().KeyChar.ToString();
                    Console.WriteLine();
                }
                else
                {
                    input = Console.ReadLine();
                }

                if (int.TryParse(input, out value))
                {
                    break;
                }
                
                Output.DisplayPrompt("Please enter an integer");
            }

            return value;
        }

        public static string ReadString(string prompt)
        {
            Output.DisplayPrompt(prompt);
            return Console.ReadLine();
        }

        public static TEnum ReadEnum<TEnum>(string prompt) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            Type type = typeof(TEnum);

            if (!type.IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type");

            Output.WriteLine(prompt);
            Menu menu = new Menu();

            TEnum choice = default(TEnum);
            foreach (var value in Enum.GetValues(type))
                menu.Add(Enum.GetName(type, value), () => { choice = (TEnum)value; });
            menu.Display();

            return choice;
        }
    }
}
