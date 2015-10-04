using System;
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
        static char checkedField = '\u2612'; //checked symbol


        //declare level by default 1
        static int level = 3;
        //declare timer
        static Stopwatch timer = new Stopwatch();
        /*---------------------*/
        static void Main()
        {
            //MENU PART
            Console.OutputEncoding = System.Text.Encoding.Unicode;
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

            RemoveScrollBars();
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

        static char[,] CreateEmptyMatrix(int boardSize)
        {
            char[,] matrix = new char[boardSize, boardSize];

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    matrix[row, col] = ' ';
                }
            }
            return matrix;
        }

        static int pickRandomRow(Random randomPick, int boardSize)
        {
            int randomRow = randomPick.Next(0, boardSize);
            return randomRow;
        }

        static int pickRandomCol(Random randomPick, int boardSize)
        {
            int randomCol = randomPick.Next(0, boardSize);
            return randomCol;
        }


        static void PrintMatrix(char[,] matrix, int boardSize)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, 3);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            //Console.WriteLine("Result: {0}", result);

            for (int row = 0; row < boardSize; row++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - boardSize, 5 + row);

                for (int col = 0; col < boardSize; col++)
                {
                    if (col == boardSize - 1)
                    {
                        Console.WriteLine("|{0:3}|", matrix[row, col]);
                    }
                    else
                    {
                        Console.Write("|{0:3}", matrix[row, col]);
                    }
                }
                Console.WriteLine();
            }
        }

        static void GenerateMatrix(int level)
        {
            int boardSize = 0; //default board size
            int letters = 0; //default letter count

            //customizing boardsize and number of letters depending on the level  
            switch (level)
            {
                case 1: boardSize = 8; letters = 5; break; //number of letters = 5; random (65, 70)
                case 2: boardSize = 10; letters = 8; break; //number of letters = 8; random (65, 73)
                case 3: boardSize = 12; letters = 11; break; // number of letters = 11; random (65, 76)
            }

            char[,] matrix = CreateEmptyMatrix(boardSize);

            Random randomPick = new Random();
            List<char> charList = new List<char>
            { 'A', 'B', 'C', 'D', 'E', 'F',
              'G', 'H', 'I', 'J', 'K', 'L',
              'M', 'N', 'O', 'P', 'Q', 'R',
              'S', 'T', 'U', 'V', 'W', 'X',
              'Y', 'Z'};

            for (int i = 0; i < letters; i++)
            {
                int randomRow = pickRandomRow(randomPick, boardSize);
                int randomCol = pickRandomCol(randomPick, boardSize);

                while (matrix[randomRow, randomCol] == ' ')
                {
                    while (randomRow == 0 && randomCol == 0)
                    {
                        randomRow = pickRandomRow(randomPick, boardSize);
                        randomCol = pickRandomCol(randomPick, boardSize);
                    }
                    matrix[randomRow, randomCol] = charList[i];

                }
            }
            PrintMatrix(matrix, boardSize);
        }

        /*----------------------*/

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
