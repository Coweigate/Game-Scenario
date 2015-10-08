using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FindTheLetters
{
    class Player
    {
        int x, y, points;
        string name = "Noname";
        public int playerTime = 1;

        public Player()
        {
            x = Console.WindowWidth / 2;
            y = Console.WindowHeight / 2;
            points = 0;
        }

        public string PlayerName
        {
            get { return name; }
            set { name = value; }
        }

        //getting the player points
        public int Points
        {
            get { return points; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        //adding point to the player
        public void GetPoint()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            points += 10;
        }
        //adding point to the player
        public void LosePoint()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (points > 0)
            {
                points -= 5;
            }
        }

        public int MoveLeft()
        {
            x--;
            return x;
        }

        public int MoveRight()
        {
            x++;
            return x;
        }

        public int MoveUp()
        {
            y--;
            return y;
        }

        public int MoveDown()
        {
            y++;
            return y;
        }

        public void DrawPlayer()
        {
            Console.SetCursorPosition(x, y);
            //Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("#");
            Console.ResetColor();
        }

        public void PlayerScore()
        {
            string scoreMessage = String.Format("Score: {0}", points);
            //setting the cursor at custom position for the score message
            //to clear the screen on each update
            Console.SetCursorPosition(Console.WindowWidth / 2 - 28, Console.WindowHeight - 3);
            Console.Write(new string(' ', scoreMessage.Length + 1));

            Console.SetCursorPosition(Console.WindowWidth / 2 - 28, Console.WindowHeight - 3);
            Console.Write(scoreMessage);
            Console.ResetColor();
        }
        //matrix.levelMatrix[(player.X - matrix.matrixOffsetX) + 1, (player.Y - matrix.matrixOffsetY) + 1] != " "
        public void ChangePlayerLocation(ConsoleKey key, Matrix currentMatrix)
        {
            Console.SetCursorPosition(x, y);
            switch (key)
            {
                case ConsoleKey.W:
                    Console.Write(" ");
                    if (y > currentMatrix.topBorder + 1
                        && currentMatrix.levelMatrix[(X - currentMatrix.matrixOffsetX), (Y - currentMatrix.matrixOffsetY) - 1] == " ")
                    {
                        y = MoveUp();
                    }
                    break;

                case ConsoleKey.D:
                    Console.Write(" ");
                    if (x < currentMatrix.rightBorder - 1
                        && currentMatrix.levelMatrix[(X - currentMatrix.matrixOffsetX) + 1, (Y - currentMatrix.matrixOffsetY)] == " ")
                    {
                        x = MoveRight();
                    }
                    break;

                case ConsoleKey.S:
                    Console.Write(" ");
                    if (y < currentMatrix.bottomBorder - 1
                        && currentMatrix.levelMatrix[(X - currentMatrix.matrixOffsetX), (Y - currentMatrix.matrixOffsetY) + 1] == " ")
                    {
                        y = MoveDown();
                    }
                    break;

                case ConsoleKey.A:
                    Console.Write(" ");
                    if (x > currentMatrix.leftBorder + 1
                        && currentMatrix.levelMatrix[(X - currentMatrix.matrixOffsetX) - 1, (Y - currentMatrix.matrixOffsetY)] == " ")
                    {
                        x = MoveLeft();
                    }
                    break;
            }
            DrawPlayer();
        }

        public void PrintPlayerTime(Stopwatch gameTime)
        {
            if (gameTime.ElapsedMilliseconds == 1000)
            {
                Console.SetCursorPosition(Console.WindowWidth - 32, Console.WindowHeight - 3);
                Console.Write("Time = {0}", playerTime);
                playerTime++;
                gameTime.Restart();
            }
        }
    }
}
