using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindTheLettersGame
{
    class FindTheLettersGame
    {


        //declare struct to save player's coordinates and to move him
        struct PlayerCoordinates
        {
            public int x;
            public int y;

            public void PutCoordinates(int put, int put2)
            {
                x = put;
                y = put2;
            }
            public void moveLeft()
            {
                x--;
            }
            public void moveRight()
            {
                x++;
            }
            public void moveUp()
            {
                y--;
            }
            public void moveDown()
            {
                y++;
            }
        }
        /*static variables*/
        //Menu variables

        static char uncheckedField = '\u25A1'; //unchecked symbol
        static char checkedField = '\u25A0'; //checked symbol


        //declare level by default 1
        static int level = 1;
        //declare timer
        static Stopwatch timer = new Stopwatch();
        /*---------------------*/
        static void Main()
        {
            //MENU PART
            RemoveScrollBars();
            Console.OutputEncoding = Encoding.Unicode;
            string wholeUncheckedString = new string(uncheckedField, 1); //creating the unchecked field /w tabulation
            string wholeCheckedString = new string(checkedField, 1); //creating the checked field /w tabulation
            string[] wholeField = new[] // init the menu /w 4 fields
            {wholeUncheckedString, wholeUncheckedString, wholeUncheckedString, wholeUncheckedString};

            PrintField(wholeField);
            wholeField[0] = wholeCheckedString; //mark the first field as checked
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

            PrintField(wholeField); //calling the printing method
            ModifyFields(keyInfo, wholeField, 0);
            //END OF MENU PART

            //RemoveScrollBars();
            /*intro part*/

            //shows the title of the game

            //menu with short instructions
            //- you have 20 seconds to find and collect alphabetically a number of letters
            //- arrow up/down/left/right for movements
            //-'h' for hint(optional)
            //- press ENTER to start

            /*end of intro part*/
            MainLoop();
        }
        //MENU METHODS
        static void PrintField(string[] wholeField) //printing method
        {

            Console.Clear();
            printingTheTitle();
            Console.WriteLine();
            centerText(wholeField[0] + " Play ");
            centerText(wholeField[1] + " Instructions ");
            centerText(wholeField[2] + " High Scores ");
            centerText(wholeField[3] + " Quit ");
        }
        private static void centerText(String text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }
        static void printingTheTitle()
        {
            string title =
                @"    ______ _           _   _   _            _          _   _                
    |  ___(_)         | | | | | |          | |        | | | |               
    | |_   _ _ __   __| | | |_| |__   ___  | |     ___| |_| |_ ___ _ __ ___ 
    |  _| | | '_ \ / _` | | __| '_ \ / _ \ | |    / _ \ __| __/ _ \ '__/ __|
    | |   | | | | | (_| | | |_| | | |  __/ | |___|  __/ |_| ||  __/ |  \__ \
    \_|   |_|_| |_|\__,_|  \__|_| |_|\___| \_____/\___|\__|\__\___|_|  |___/";
            Console.WriteLine(title);
        }
        static void ModifyFields(ConsoleKeyInfo keyInfo, string[] field, int index)
        {
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        {
                            if (index == 0)
                            {
                                Console.Clear();
                                RemoveScrollBars();
                                MainLoop();
                                break;
                            }
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        if (index != 0)
                        {
                            field[index] = new string(uncheckedField, 1);
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
                }
            }
        }

        //static void QuitMenu()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Are you sure you want to quit? (Y/N)");
        //    char quitCheck = new char();
        //    do
        //    {
        //        quitCheck = char.Parse(Console.ReadLine());
        //        if (quitCheck=='Y'||quitCheck=='y')
        //        {
        //            Console.WriteLine("Thank you for playing!");
        //            break;
        //            return;
        //        }
        //        else if (quitCheck == 'N' || quitCheck == 'n')
        //        {

        //        }
        //    } while ((quitCheck=='Y'||quitCheck=='y')||(quitCheck == 'N' || quitCheck == 'n'));
        //}
        //END OF MENU METHODS


        //MAtRIX GENERATOR METHODS
        static void GenerateMatrix(int level)
        {
            int boardSize = 30;
            char[][] matrix = new char[boardSize][];

            if (level == 1)
            {
                var reader = new StreamReader("../../Levels/level1.txt");
                using (reader)
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            char[] matrixLine = new char[line.Length];
                            matrixLine = line.ToCharArray();
                            matrix[i] = matrixLine;
                            line = reader.ReadLine();
                        }
                    }
                }
            }
            else if (level == 2)
            {
                var reader = new StreamReader("../../Levels/level2.txt");
                using (reader)
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            char[] matrixLine = new char[line.Length];
                            matrixLine = line.ToCharArray();
                            matrix[i] = matrixLine;
                            line = reader.ReadLine();
                        }
                    }
                }
            }
            else if (level == 3)
            {
                var reader = new StreamReader("../../Levels/level3.txt");
                using (reader)
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            char[] matrixLine = new char[line.Length];
                            matrixLine = line.ToCharArray();
                            matrix[i] = matrixLine;
                            line = reader.ReadLine();
                        }
                    }
                }
            }
            PrintMatrix(matrix, boardSize);
        }


        static void PrintMatrix(char[][] matrix, int boardSize)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 30, 4);
            
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 30, 4 + row);
                for (int col = 0; col < 60; col++)
                {
                    Console.Write("{0}", matrix[row][col]);
                }
                Console.WriteLine();
            }
        }

        //END OF MATRIX METHODS

        /* MAIN LOOP METHOD */

        static void MainLoop()
        {
            //declare pause loop interval
            const int INTERVAL = 100;
            //declare time to find the letters
            int timeToFindTheLetters = 20;

            //reset and start timer
            timer = new Stopwatch();
            timer.Start();

            GenerateMatrix(level);

            //check if time is up --> stop loop
            while (timeToFindTheLetters - timer.Elapsed.Seconds > 0)
            {
                // Sleep for a short period
                Thread.Sleep(INTERVAL);

                /*** Update the screen and handle input here! ***/
                Console.SetCursorPosition(0, 0);
                Console.Write("Timeleft: {0} sec", timeToFindTheLetters - timer.Elapsed.Seconds);

            }
            //stop timer
            timer.Stop();
            //print time is up text
            Console.WriteLine();
            Console.WriteLine("Time is up!");
        }

        /*---------------------*/




        //remove scrollbars of the console
        static void RemoveScrollBars()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }
    }
}
