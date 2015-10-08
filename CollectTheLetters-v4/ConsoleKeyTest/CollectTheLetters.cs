using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //level
        public static int levelChoice;

        public static void Main()
        {
            Stopwatch gameTime = new Stopwatch();
            GameMenuAndMessages.RemoveScrollBars();
            Console.CursorVisible = false;

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
                wholeField[0] = wholeCheckedString;
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
            GameMenuAndMessages.ClearFrame();
            //creating initial matrix
            Matrix matrix = new Matrix(levelChoice);
            //drawing the matrix to the console
            matrix.DrawMatrix();
            //get 10 random letters
            Random randomNum = new Random();
            int letterToCollect = 5;
            List<Letters> lettersToWrite = new List<Letters>();
            for (int i = 0; i < letterToCollect; i++)
            {
                lettersToWrite.Add(new Letters(matrix, randomNum));
            }
            //drawing the player to the console inside the matrix & check if it didn't spawn on top of the player
            while (player.X == lettersToWrite[0].X && player.Y == lettersToWrite[0].Y)
            {
                lettersToWrite[0].GetRandomPosition();
            }
            lettersToWrite[0].DrawLetter();
            //avoid letters spawning on top of each other or on top of the player
            for (int i = 0; i < lettersToWrite.Count - 1; i++)
            {
                while ((lettersToWrite[i].X == lettersToWrite[i + 1].X
                    && lettersToWrite[i].Y == lettersToWrite[i + 1].Y)
                    || (player.X == lettersToWrite[i + 1].X && player.Y == lettersToWrite[i + 1].Y))
                {
                    lettersToWrite[i + 1].GetRandomPosition();
                }
                lettersToWrite[i + 1].DrawLetter();
            }

            //drawing game name
            Console.ForegroundColor = ConsoleColor.Cyan;
            GameMenuAndMessages.printingTheTitle();
            Console.ForegroundColor = ConsoleColor.White;

            //drawing the player to the console
            player.DrawPlayer();
            player.PlayerScore();

            //draw timer to the screen
            Console.SetCursorPosition(Console.WindowWidth - 32, Console.WindowHeight - 3);
            Console.Write("Time = 0");

            //update the remaining lettes
            Letters.RemainingLetters(lettersToWrite);

            //getting user input(pressed key)
            ConsoleKey pressedKey = Console.ReadKey(true).Key;
            int letterOrder;

            gameTime.Start();
            while (lettersToWrite.Count > 0)
            {
                //looping only if key is pressed
                while (Console.KeyAvailable)
                {                   
                    pressedKey = Console.ReadKey(true).Key;
                    //updating player location based on the pressedKey, also printing him to the console.
                    player.ChangePlayerLocation(pressedKey, matrix);
                }

                letterOrder = 0;
                if (lettersToWrite.Count > 0)
                {
                    //print player time
                    player.PrintPlayerTime(gameTime);
                    //check if the player steped on a letter                        
                    for (int i = 0; i < lettersToWrite.Count; i++)
                    {
                        if (player.X == (lettersToWrite[i].X) && player.Y == (lettersToWrite[i].Y) && lettersToWrite[i] == lettersToWrite[letterOrder])
                        {
                            player.GetPoint();
                            player.PlayerScore();
                            player.DrawPlayer();
                            lettersToWrite.RemoveAt(i);
                            letterOrder++;
                            //update the remaining lettes
                            Letters.RemainingLetters(lettersToWrite);
                            break;
                        }
                        else if (player.X == (lettersToWrite[i].X) && player.Y == (lettersToWrite[i].Y) && lettersToWrite[i] != lettersToWrite[letterOrder])
                        {
                            if (!lettersToWrite[i].hasBeenStepedOver)
                            {
                                player.LosePoint();
                                player.PlayerScore();
                            }
                            player.DrawPlayer();
                            lettersToWrite[i].hasBeenStepedOver = true;
                        }
                        else if (lettersToWrite[i].hasBeenStepedOver)
                        {
                            lettersToWrite[i].DrawLetter();
                            lettersToWrite[i].hasBeenStepedOver = false;
                            player.DrawPlayer();
                        }
                    }
                }

                //update the letters;
                for (int i = 0; i < lettersToWrite.Count; i++)
                {
                    lettersToWrite[i].Update();
                }
            }
            
            //highscore
            menu = true;
            Highscore.AddHighscore(player.Points, player.playerTime, player.PlayerName);
            string exitToMainMenuMessage = "To exit to Main Menu press ESC.";
            Console.SetCursorPosition((Console.WindowWidth - exitToMainMenuMessage.Length) / 2, (Console.WindowHeight - exitToMainMenuMessage.Length) / 2);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(exitToMainMenuMessage);
            Console.ResetColor();
            while (pressedKey != ConsoleKey.Escape)
            {
                pressedKey = Console.ReadKey(true).Key;
            }

            GameMenuAndMessages.ClearAction();
            Main();
        }
    }
}
