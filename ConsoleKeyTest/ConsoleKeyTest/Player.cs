using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectTheLettersTestVersion
{
    class Player
    {
        int x, y, points;
        string name = "Noname";

        public Player() {
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
            points += 1;
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

        public void DrawPlayer() {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("O");
            Console.ResetColor();
        }

        public void DrawPlayerScore()
        {
            Console.SetCursorPosition((Console.WindowWidth) / 2 - 4, Console.WindowHeight - 3);
            Console.Write("Score: {0}", points);
        }

        public void ChangePlayerLocation(ConsoleKey key, Matrix currentMatrix)
        {
            Console.SetCursorPosition(x, y);
            switch (key)
            {
                case ConsoleKey.W:
                    Console.Write(" ");
                    if (y > currentMatrix.topBorder + 1)
                    {
                        y = MoveUp();
                    }
                    break;

                case ConsoleKey.D:
                    Console.Write(" ");
                    if (x < currentMatrix.rightBorder - 1)
                    {
                        x = MoveRight();
                    }
                    break;

                case ConsoleKey.S:
                    Console.Write(" ");
                    if (y < currentMatrix.bottomBorder - 1)
                    {
                        y = MoveDown();
                    }
                    break;

                case ConsoleKey.A:
                    Console.Write(" ");
                    if (x > currentMatrix.leftBorder + 1)
                    {
                        x = MoveLeft();
                    }
                    break;
            }
            DrawPlayer();
        }
    }
}
