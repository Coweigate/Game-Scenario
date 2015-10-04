using System;
using System.Collections.Generic;
using System.Threading;

namespace CollectTheLettersTestVersion
{
	class MainClass
	{
        public static void Main()
        {
            Console.WriteLine("To stop the game press ESC.\t\tFor movement use 'W', 'S', 'A' and 'D'");

            Console.CursorVisible = false;
            int levelChoice = 1;//we can use random level option in the menu and a specific level option

            //creating a player
            Player player = new Player();
            //enter name
            player.PlayerName = GameMenuAndMessages.EnterName();
            //highscore test
            Highscore.AddHighscore(player.Points, player.PlayerName);
            Highscore.GetHighScore();
            //drawing the player to the console
            player.DrawPlayer();
            player.DrawPlayerScore();

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
            //getting user input(pressed key)
            ConsoleKey pressedKey = Console.ReadKey(true).Key;
            while (pressedKey != ConsoleKey.Escape) {
                //looping only if key is pressed
                while (Console.KeyAvailable)
                {
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
