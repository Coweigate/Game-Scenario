using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CollectTheLettersTestVersion
{
    class MainClass
    {
        public static void Main()
        {
            Stopwatch gameTime = new Stopwatch();
            Console.WriteLine("To stop the game press ESC.\t\tFor movement use 'W', 'S', 'A' and 'D'");

            Console.CursorVisible = false;
            int levelChoice = 1;//we can use random level option in the menu and a specific level option

            //creating initial matrix
            Matrix matrix = new Matrix(levelChoice);
            //drawing the matrix to the console
            matrix.DrawMatrix();

            //creating a player
            Player player = new Player();
            //drawing the player to the console
            player.DrawPlayer();
            player.PlayerScore();

            Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - 3);
            Console.Write("Time = 0");

            //get 10 random letters
            Random randomNum = new Random();
            int letterToCollect = 10;
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
            //draw the remaining lettes to collect
            Letters.RemainingLetters(lettersToWrite);

            //getting user input(pressed key)
            ConsoleKey pressedKey = Console.ReadKey(true).Key;
            int letterOrder;

            gameTime.Start();
            while (pressedKey != ConsoleKey.Escape)
            {
                //moving only if key is pressed
                while (Console.KeyAvailable)
                {
                    pressedKey = Console.ReadKey(true).Key;
                    if (lettersToWrite.Count > 0) {
                        //updating player location based on pressedKey, also printing him to the console.                        
                        player.ChangePlayerLocation(pressedKey, matrix);
                    }                    
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
                            if (!lettersToWrite[i].hasBeenStepedOver) {
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
            ////press any key to continue message
            //Console.SetCursorPosition(0, Console.WindowHeight - 2);
            //Console.WriteLine();
            player = null;
            matrix = null;
            gameTime.Stop();
            Console.Clear();
            Main();            
        }
    }
}
