using System;

namespace EasyConsole
{
    public static class Input
    {
        public static int ReadInt(string prompt, int min, int max)
        {
            bool lessThan9 = (max - min) < 10;
            
            Output.DisplayPrompt(prompt + (lessThan9 ? ": " : " and then press enter: "));
            return ReadInt(min, max, lessThan9);
        }

        public static int ReadInt(int min, int max, bool canReadKey = false)
        {
            int value = ReadInt(canReadKey);

            while (value < min || value > max)
            {
                Output.DisplayPrompt("Please enter an integer between {0} and {1} (inclusive) then press enter: ", min, max);
                value = ReadInt(canReadKey);
            }

            return value;
        }

        public static int ReadInt(bool canReadKey = false)
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
                
                Output.DisplayPrompt("Please enter an integer" + (canReadKey ? " and then press enter: " : " "));
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

        public static bool ReadConfirmation(string option)
        {
            Output.WriteLine($"You selected {option}, is this correct (Y/n)?");

            var choice = Console.ReadKey().Key;
            Console.WriteLine();

            while (choice != ConsoleKey.Y && choice != ConsoleKey.N)
            {
                Console.Write("Please press Y or N: ");
                choice = Console.ReadKey().Key;
            }

            return choice == ConsoleKey.Y;
        }
    }
}
