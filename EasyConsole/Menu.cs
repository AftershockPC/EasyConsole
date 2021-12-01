using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyConsole
{
    public class Menu
    {
        public Menu()
        {
            Options = new List<Option>();
        }

        private IList<Option> Options { get; }

        public void Display()
        {
            int choice;
            while (true)
            {
                for (var i = 0; i < Options.Count; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, Options[i].Name);
                }

                choice = Input.ReadInt("Choose an option", 1, Options.Count);

                if (Input.ReadConfirmation(Options[choice - 1].Name))
                {
                    break;
                }
            }

            Options[choice - 1].Callback();
        }

        public Menu Add(string option, Action callback)
        {
            return Add(new Option(option, callback));
        }

        public Menu Add(Option option)
        {
            Options.Add(option);
            return this;
        }

        public bool Contains(string option)
        {
            return Options.FirstOrDefault(op => op.Name.Equals(option)) != null;
        }
    }
}