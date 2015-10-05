using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CollectTheLettersTestVersion
{
    class MainClass
    {
        public static bool menu = true;
        public static int width = Console.WindowWidth;
        public static int height = Console.WindowHeight;

        //Menu variables

        public static char uncheckedField = '\u25A1'; //unchecked symbol
        public static char checkedField = '\u25A0'; //checked symbol
        public static string wholeUncheckedString = new string(uncheckedField, 1); //creating the unchecked field
        public static string wholeCheckedString = new string(checkedField, 1); //creating the checked field
        public static string[] mainMenuField = new[] // init the main menu /w 4 fields
        {wholeUncheckedString, wholeUncheckedString, wholeUncheckedString, wholeUncheckedString};
        public static string[] subMenuField = new[] // init the submenu /w 4 fields
        {wholeUncheckedString, wholeUncheckedString, wholeUncheckedString, wholeUncheckedString};


        public static void Main()
        {

            Console.CursorVisible = false;
            int levelChoice = 1;

            //we can use random level option in the menu and a specific level option

            //MENU PART

            if (menu)
            {

                //mainMenuField[0] = wholeCheckedString; //mark the first field as checked
                //subMenuField[0] = wholeCheckedString;
                Console.OutputEncoding = Encoding.Unicode;
                string wholeUncheckedString = new string(uncheckedField, 1); //creating the unchecked field /w tabulation
                string wholeCheckedString = new string(checkedField, 1); //creating the checked field /w tabulation
                string[] wholeField = new[] // init the menu /w 4 fields
                {wholeUncheckedString, wholeUncheckedString, wholeUncheckedString, wholeUncheckedString};

                GameMenuAndMessages.PrintMainMenu(wholeField);
                wholeField[0] = wholeCheckedString; //mark the first field as checked
                ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

                GameMenuAndMessages.PrintMainMenu(wholeField); //calling the printing method
                GameMenuAndMessages.ModifyMainMenu(keyInfo, mainMenuField, 0);
                //  GameMenuAndMessages.ModifyFields(keyInfo, wholeField, 0);

            }
            //creating a player
            Player player = new Player();

            //enter name
            player.PlayerName = GameMenuAndMessages.EnterName();

            //creating initial matrix
            Matrix matrix = new Matrix(levelChoice);
            //drawing the matrix to the console
            matrix.DrawMatrix();

            //get 10 random letters
            int letterToCollect = 6;
            List<Letters> lettersToWrite = new List<Letters>();
            for (int i = 0; i < letterToCollect; i++)
            {
                lettersToWrite.Add(new Letters(matrix));
                Thread.Sleep(40);
            }
            //drawing the player to the console inside the matrix
            for (int i = 0; i < lettersToWrite.Count; i++)
            {
                lettersToWrite[i].DrawLetter();
            }

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

            //drawing the player to the console
            player.DrawPlayer();
            player.DrawPlayerScore();

            //getting user input(pressed key)
            ConsoleKey pressedKey = Console.ReadKey(true).Key;
            while (pressedKey != ConsoleKey.Escape)
            {
                //looping only if key is pressed
                while (Console.KeyAvailable)
                {
                    if (player.Points == letterToCollect)
                    {
                        //highscore
                        menu = true;
                        Highscore.AddHighscore(player.Points, player.PlayerName);
                        Main();
                        return;
                    }
                    pressedKey = Console.ReadKey(true).Key;
                    //updating player location based on the pressedKey, also printing him to the console.
                    player.ChangePlayerLocation(pressedKey, matrix);
                    //check if the player steped on a letter
                    for (int i = 0; i < lettersToWrite.Count; i++)
                    {
                        if (player.X == (lettersToWrite[i].X) && player.Y == (lettersToWrite[i].Y) && !lettersToWrite[i].isCollected)
                        {
                            player.GetPoint();
                            player.DrawPlayerScore();
                            lettersToWrite[i].letter = ' ';
                            lettersToWrite[i].isCollected = true;
                        }
                    }
                }
            }

            //press any key to continue message
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine();
        }
    }
}
