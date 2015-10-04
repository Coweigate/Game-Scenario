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
            Console.Clear();
            Console.Write(" Please enter a valid name: \n\n  - between 3 and 10 characters long\n  - containing only letters (A to Z) and numbers (0 to 9)");
            StringBuilder name = new StringBuilder();
            while (true)
            {
                Console.CursorVisible = true;
                name = new StringBuilder();
                Console.SetCursorPosition(28, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                name.Append(Console.ReadLine());
                if (Regex.Match(name.ToString(), @"[-!$%^&*()_+|~=`{}\[\]:; '<>?,.\/]").Success || name.Length < 3 || name.Length > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(1, 1);
                    Console.WriteLine("Invalid name, try again!");
                    Console.SetCursorPosition(28, 0);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("                          ");
                }
                else
                {
                    Console.Clear();
                    return name.ToString();
                }
            }
        }
    }
}
