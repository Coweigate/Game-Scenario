using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace CollectTheLettersTestVersion
{
    class GameMenuAndMessages
    {
        //MENU METHODS
        public static void PrintField(string[] wholeField) //printing method
        {

            Console.Clear();
            printingTheTitle();
            Console.SetCursorPosition(0, 13);
            centerText(wholeField[0] + " Play ");
            centerText(wholeField[1] + " Instructions ");
            centerText(wholeField[2] + " High Scores ");
            centerText(wholeField[3] + " Quit ");
            //drawing lines
            Console.SetCursorPosition((MainClass.width / 2) - 34, (MainClass.height / 2) - 7);
            Console.WriteLine("===================================================================");
            Console.SetCursorPosition((MainClass.width / 2) - 34, (MainClass.height / 2) + 8);
            Console.WriteLine("===================================================================");
        }
        private static void centerText(String text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }
        public static void printingTheTitle()
        {

            //drawing game name
            Console.SetCursorPosition((MainClass.width / 2) - 36, 0);
            Console.Write(@"______ _           _   _   _            _          _   _                 ");
            Console.SetCursorPosition((MainClass.width / 2) - 36, 1);
            Console.Write(@"|  ___(_)         | | | | | |          | |        | | | |                ");
            Console.SetCursorPosition((MainClass.width / 2) - 36, 2);
            Console.Write(@"| |_   _ _ __   __| | | |_| |__   ___  | |     ___| |_| |_ ___ _ __ ___  ");
            Console.SetCursorPosition((MainClass.width / 2) - 36, 3);
            Console.Write(@"|  _| | | '_ \ / _` | | __| '_ \ / _ \ | |    / _ \ __| __/ _ \ '__/ __| ");
            Console.SetCursorPosition((MainClass.width / 2) - 36, 4);
            Console.Write(@"| |   | | | | | (_| | | |_| | | |  __/ | |___|  __/ |_| ||  __/ |  \__ \ ");
            Console.SetCursorPosition((MainClass.width / 2) - 36, 5);
            Console.Write(@"\_|   |_|_| |_|\__,_|  \__|_| |_|\___| \_____/\___|\__|\__\___|_|  |___/ ");
        }
        public static void RemoveScrollBars()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }
        public static void ModifyFields(ConsoleKeyInfo keyInfo, string[] field, int index)
        {
            char uncheckedField = '\u25A1'; //unchecked symbol
            char checkedField = '\u25A0'; //checked symbol

            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        {
                            if (index == 0)
                            {
                                MainClass.menu = false;
                                Console.Clear();
                                RemoveScrollBars();
                                MainClass.Main();
                                break;
                            }
                            if (index == 1)
                            {

                            }
                            if (index == 2)
                            {
                                Highscore.GetHighScore();
                                break;
                            }
                            if (index == 3)
                            {
                                Console.Write(Environment.NewLine + Environment.NewLine);
                                Environment.Exit(0);
                                break;
                            }
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        if (index != 0)
                        {
                            field[index] = new string( uncheckedField, 1);
                            index--;
                            field[index] = new string(checkedField, 1);
                            PrintField(field);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (index != field.Length - 1)
                        {
                            field[index] = new string(uncheckedField, 1);
                            index++;
                            field[index] = new string(checkedField, 1);
                            PrintField(field);
                        }
                        break;
                    default:
                        PrintField(field);
                        break;
                }
            }
        }

        public static string EnterName()
        {

            //name validation
            
            Console.Clear();
            //drawing game name
            Console.SetCursorPosition((MainClass.width / 2) - 36, 0);
            Console.Write(@"______ _           _   _   _            _          _   _                 ");
            Console.SetCursorPosition((MainClass.width / 2) - 36, 1);
            Console.Write(@"|  ___(_)         | | | | | |          | |        | | | |                ");
            Console.SetCursorPosition((MainClass.width / 2) - 36, 2);
            Console.Write(@"| |_   _ _ __   __| | | |_| |__   ___  | |     ___| |_| |_ ___ _ __ ___  ");
            Console.SetCursorPosition((MainClass.width / 2) - 36, 3);
            Console.Write(@"|  _| | | '_ \ / _` | | __| '_ \ / _ \ | |    / _ \ __| __/ _ \ '__/ __| ");
            Console.SetCursorPosition((MainClass.width / 2) - 36, 4);
            Console.Write(@"| |   | | | | | (_| | | |_| | | |  __/ | |___|  __/ |_| ||  __/ |  \__ \ ");
            Console.SetCursorPosition((MainClass.width / 2) - 36, 5);
            Console.Write(@"\_|   |_|_| |_|\__,_|  \__|_| |_|\___| \_____/\___|\__|\__\___|_|  |___/ ");
            //drawing lines
            Console.SetCursorPosition((MainClass.width / 2) - 34, (MainClass.height / 2) - 7);
            Console.WriteLine("===================================================================");
            Console.SetCursorPosition((MainClass.width / 2) - 34, (MainClass.height / 2) + 8);
            Console.WriteLine("===================================================================");
            //drawing text
            Console.SetCursorPosition((MainClass.width / 2) - 13, (MainClass.height / 2) - 5);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write("Please enter a valid name:");
            Console.SetCursorPosition((MainClass.width / 2) - 18, (MainClass.height / 2) + 4);
            Console.Write("[between 3 and 10 characters long]");
            Console.SetCursorPosition((MainClass.width / 2) - 27, (MainClass.height / 2) + 6);
            Console.Write("[containing only letters (A to Z) and numbers (0 to 9]");
            StringBuilder name = new StringBuilder();
            //validate player name
            while (true)
            {
                Console.CursorVisible = true;
                name = new StringBuilder();
                Console.SetCursorPosition((MainClass.width / 2) - 4, (MainClass.height / 2) - 1);
                Console.BackgroundColor = ConsoleColor.Black;
                name.Append(Console.ReadLine());
                if (Regex.Match(name.ToString(), @"[-!$%^&*()_+|~=`{}\[\]:; '<>?,.\/]").Success || name.Length < 3 || name.Length > 10)
                {
                    Console.SetCursorPosition((MainClass.width / 2) - 12, (MainClass.height / 2) - 3);
                    Console.WriteLine("Invalid name, try again!");
                    Console.SetCursorPosition((MainClass.width / 2) - 4, (MainClass.height / 2) - 1);
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
