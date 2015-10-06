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
        public static void PrintMainMenu(string[] wholeField) //printing method
        {

            ClearText();
            printingTheTitle();
            Console.SetCursorPosition(0, 13);
            centerText("MENU ");
            Console.SetCursorPosition(0, 15);
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
        static void PrintSubmenu(string[] wholeField)
        {
            ClearText();
            printingTheTitle();
            Console.SetCursorPosition((MainClass.width / 2) - 34, (MainClass.height / 2) - 7);
            Console.WriteLine("===================================================================");
            Console.SetCursorPosition((MainClass.width / 2) - 34, (MainClass.height / 2) + 8);
            Console.WriteLine("===================================================================");
            Console.SetCursorPosition(0, 13);
            centerText("Select LEVEL");
            Console.SetCursorPosition(0, 15);
            centerText(wholeField[0] + " Level 1 ");
            centerText(wholeField[1] + " Level 2 ");
            centerText(wholeField[2] + " Level 3 ");
            Console.WriteLine();
            centerText(wholeField[3] + " Back to main menu ");
        }

        private static void centerText(String text)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
            Console.ResetColor();
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
        //remove scrollbars of the console
        static void RemoveScrollBars()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        public static void ModifyMainMenu(ConsoleKeyInfo keyInfo, string[] field, int index = 0)
        {
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        {
                            if (index == 0)
                            {
                                string wholeUncheckedString = new string(MainClass.uncheckedField, 1); //creating the unchecked field /w tabulation
                                string wholeCheckedString = new string(MainClass.checkedField, 1); //creating the checked field /w tabulation
                                string[] wholeField = new[] // init the menu /w 4 fields
                                {wholeUncheckedString, wholeUncheckedString, wholeUncheckedString, wholeUncheckedString};
                                wholeField[0] = wholeCheckedString;
                                ClearText();
                                PrintSubmenu(wholeField);
                                ModifySubmenu(keyInfo, MainClass.subMenuField, 0);
                                return;
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
                                ClearText();
                                Console.SetCursorPosition(0, Console.WindowHeight / 2);
                                centerText("Thank you for playing!\n");
                                Console.SetCursorPosition(Console.WindowWidth / 2 - 16, Console.WindowHeight / 2 + 2);
                                Environment.Exit(0);
                            }
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        if (index != 0)
                        {
                            field[index] = new string(MainClass.uncheckedField, 1);
                            index--;
                            field[index] = new string(MainClass.checkedField, 1);
                            PrintMainMenu(field);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (index != field.Length - 1)
                        {
                            field[index] = new string(MainClass.uncheckedField, 1);
                            index++;
                            field[index] = new string(MainClass.checkedField, 1);
                            PrintMainMenu(field);
                        }
                        break;
                    default:
                        string wholeUncheckedString2 = new string(MainClass.uncheckedField, 1); //creating the unchecked field /w tabulation
                        string wholeCheckedString2 = new string(MainClass.checkedField, 1); //creating the checked field /w tabulation
                        string[] wholeField2 = new[] // init the menu /w 4 fields
                        {wholeUncheckedString2, wholeUncheckedString2, wholeUncheckedString2, wholeUncheckedString2};
                        wholeField2[index] = wholeCheckedString2;
                        PrintMainMenu(wholeField2);
                        break;
                }
            }
        }

        public static void ModifySubmenu(ConsoleKeyInfo keyInfo, string[] field, int index = 0)
        {
            string wholeUncheckedString = new string(MainClass.uncheckedField, 1); //creating the unchecked field /w tabulation
            string wholeCheckedString = new string(MainClass.checkedField, 1); //creating the checked field /w tabulation
            string[] wholeField = new[] // init the menu /w 4 fields
            {wholeUncheckedString, wholeUncheckedString, wholeUncheckedString, wholeUncheckedString};
            wholeField[0] = wholeCheckedString;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        {
                            if (index == 0)
                            {
                                MainClass.menu = false;
                                MainClass.Main();
                                //open level 1
                                break;
                            }
                            else if (index == 3)
                            {
                                MainClass.subMenuField = new[] // reset the fields
                                {MainClass.wholeUncheckedString, MainClass.wholeUncheckedString, MainClass.wholeUncheckedString, MainClass.wholeUncheckedString};
                                //Console.Clear();
                                wholeField[0] = wholeCheckedString;
                                PrintMainMenu(wholeField);
                                ModifyMainMenu(keyInfo, MainClass.mainMenuField, 0);
                            }
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        if (index != 0)
                        {
                            field[index] = new string(MainClass.uncheckedField, 1);
                            index--;
                            field[index] = new string(MainClass.checkedField, 1);
                            PrintSubmenu(field);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (index != field.Length - 1)
                        {
                            field[index] = new string(MainClass.uncheckedField, 1);
                            index++;
                            field[index] = new string(MainClass.checkedField, 1);

                            PrintSubmenu(field);
                        }
                        break;
                }
            }
        }
        
        public static void ClearText()
        {
            for (int i = -6; i <= 7; i++)
            {
                Console.SetCursorPosition(0, i + Console.WindowHeight / 2);
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, Console.WindowHeight / 2 + 11);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.WriteLine(new string(' ', Console.WindowWidth));
        }

        public static string EnterName()
        {

            //name validation
            ClearText();
            //drawing game name

            printingTheTitle();

            //drawing lines
            Console.SetCursorPosition((MainClass.width / 2) - 34, (MainClass.height / 2) - 7);
            Console.WriteLine("===================================================================");
            Console.SetCursorPosition((MainClass.width / 2) - 34, (MainClass.height / 2) + 8);
            Console.WriteLine("===================================================================");
            //drawing text
            Console.SetCursorPosition((MainClass.width / 2) - 13, (MainClass.height / 2) - 5);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Please enter a valid name:");
            Console.SetCursorPosition((MainClass.width / 2) - 18, (MainClass.height / 2) + 4);
            Console.Write("[between 3 and 10 characters long]");
            Console.SetCursorPosition((MainClass.width / 2) - 27, (MainClass.height / 2) + 6);
            Console.Write("[containing only letters (A to Z) and numbers (0 to 9]");
            StringBuilder name = new StringBuilder();
            //validate player name
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.CursorVisible = true;
                name = new StringBuilder();
                Console.SetCursorPosition((MainClass.width / 2) - 4, (MainClass.height / 2) - 1);
                name.Append(Console.ReadLine());
                if (Regex.Match(name.ToString(), @"[-!$%^&*()_+|~=`{}\[\]:; '<>?,.\/]").Success || name.Length < 3 || name.Length > 10)
                {
                    Console.SetCursorPosition((MainClass.width / 2) - 12, (MainClass.height / 2) - 3);
                    Console.WriteLine("Invalid name, try again!");
                    Console.SetCursorPosition(0, (MainClass.height / 2) - 1);
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, (MainClass.height / 2));
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }
                else
                {
                    ClearText();
                    Console.CursorVisible = false;
                    return name.ToString();
                }
            }
        }
    }
}
