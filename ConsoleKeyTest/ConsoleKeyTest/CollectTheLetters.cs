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

        public static void Main()
        {
            

            Console.CursorVisible = false;
            int levelChoice = 1;//we can use random level option in the menu and a specific level option

            //MENU PART

            if (menu)
            {
                char uncheckedField = '\u25A1'; //unchecked symbol
                char checkedField = '\u25A0'; //checked symbol
                GameMenuAndMessages.RemoveScrollBars();
                Console.OutputEncoding = Encoding.Unicode;
                string wholeUncheckedString = new string(uncheckedField, 1); //creating the unchecked field /w tabulation
                string wholeCheckedString = new string(checkedField, 1); //creating the checked field /w tabulation
                string[] wholeField = new[] // init the menu /w 4 fields
                {wholeUncheckedString, wholeUncheckedString, wholeUncheckedString, wholeUncheckedString};

                GameMenuAndMessages.PrintField(wholeField);
                wholeField[0] = wholeCheckedString; //mark the first field as checked
                ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

                GameMenuAndMessages.PrintField(wholeField); //calling the printing method
                GameMenuAndMessages.ModifyFields(keyInfo, wholeField, 0);

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
            int letterToCollect = 10;
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

            //drawing the player to the console
            player.DrawPlayer();
            player.DrawPlayerScore();

            //getting user input(pressed key)
            ConsoleKey pressedKey = Console.ReadKey(true).Key;
            while (pressedKey != ConsoleKey.Escape) {
                //looping only if key is pressed
                while (Console.KeyAvailable)
                {
                    if(player.Points == letterToCollect)
                    {
                        //highscore
                        Highscore.AddHighscore(player.Points, player.PlayerName);
                        Highscore.GetHighScore();

                        break;
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
