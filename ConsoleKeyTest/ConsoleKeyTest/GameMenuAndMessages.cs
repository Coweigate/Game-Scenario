using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CollectTheLettersTestVersion
{
    class GameMenuAndMessages
    {
        public static string EnterName()
        {
            //name validation
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            Console.Clear();
            Console.SetCursorPosition((width / 2) - 13, (height / 2) - 5);
            Console.Write("Please enter a valid name:");
            Console.SetCursorPosition((width / 2) - 18, (height / 2) - 1);
            Console.Write("[between 3 and 10 characters long]");
            Console.SetCursorPosition((width / 2) - 27, (height / 2) + 1);
            Console.Write("[containing only letters (A to Z) and numbers (0 to 9]");
            StringBuilder name = new StringBuilder();
            while (true)
            {
                Console.CursorVisible = true;
                name = new StringBuilder();
                Console.SetCursorPosition((width / 2) - 4, (height / 2) - 3);
                Console.ForegroundColor = ConsoleColor.Green;
                name.Append(Console.ReadLine());
                if (Regex.Match(name.ToString(), @"[-!$%^&*()_+|~=`{}\[\]:; '<>?,.\/]").Success || name.Length < 3 || name.Length > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition((width / 2) - 12, (height / 2) - 7);
                    Console.WriteLine("Invalid name, try again!");
                    Console.SetCursorPosition((width / 2) - 4, (height / 2) - 3);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("                                  ");
                }
                else
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    return name.ToString();
                }
            }
        }
    }
}
