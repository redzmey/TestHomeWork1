using System;
using System.Text;

namespace SuperDict
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("Welcome to Super-Duper translator!");
            string input = Console.ReadLine();

            try
            {
                while (input != "exit")
                {
                    string[] command = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (command.Length > 0)
                    {
                        switch (command[0].ToLower())
                        {
                            case "add":
                            {
                                if (command.Length != 5)
                                    throw new ArgumentException("Invalid arguments count");

                                TranslationHandler.AddEntry(command[1], command[2], command[3], command[4]);
                                Console.WriteLine($"Translation of {command[1]} added.");
                            }

                                break;
                            case "remove":
                            {
                                if (command.Length != 3)
                                    throw new ArgumentException("Invalid arguments count");

                                TranslationHandler.Remove(command[1], command[2]);
                                Console.WriteLine($"Translation of {command[1]} removed.");
                            }

                                break;
                            case "clear":
                            {
                                TranslationHandler.Clear();
                                Console.WriteLine($"Dictionary wiped out.");
                            }
                                break;

                            case "translate":
                            {
                                if (command.Length != 4)
                                    throw new ArgumentException("Invalid arguments count");

                                string result = TranslationHandler.Translate(command[1], command[2], command[3]);
                                Console.WriteLine($"Translation of {command[1]} to {command[3]} is {result}");
                            }

                                break;
                        }

                        input = Console.ReadLine();
                    }
                }
            }
            finally
            {
                input = Console.ReadLine();
            }
        }
    }
}