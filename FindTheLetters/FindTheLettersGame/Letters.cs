using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FindTheLetters
{
    class Letters
    {
        public char letter;
        Matrix matrixBoard;
        public ConsoleColor letterColor;
        public bool hasBeenStepedOver = false;
        char[] letters = new char[26];
        int leftBorder, rightBorder, topBorder, bottomBorder, x, y, randomX, randomY, matrixXIndex, matrixYIndex;
        Random randomGenerator;
        Stopwatch letterUpdateTime = new Stopwatch();
        int timeToStartMoving;

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public Letters(Matrix matrix, Random random)
        {
            //filling the matrix with letters from [65]A to[90]Z
            int num = 65;
            for (int i = 0; i < letters.Length; i++)
            {
                letters[i] = (char)num;
                num++;
            }
            matrixBoard = matrix;
            randomGenerator = random;
            //setting the letter color
            letterColor = LetterColor(randomGenerator.Next(1, 9));
            //setting the letter moving time
            timeToStartMoving = randomGenerator.Next(150, 451);
            //getting the matrix borders
            leftBorder = matrix.leftBorder + 1;
            rightBorder = matrix.rightBorder;
            topBorder = matrix.topBorder + 1;
            bottomBorder = matrix.bottomBorder;

            GetRandomLetter();
            GetRandomPosition();

            matrixXIndex = x - matrixBoard.matrixOffsetX;
            matrixYIndex = y - matrixBoard.matrixOffsetY;
            //checking if letter spawned near wall and set its direction
            if (x == matrixBoard.leftBorder + 1)
            {
                randomX = -1;
            }
            if (x == matrixBoard.rightBorder - 1)
            {
                randomX = 1;
            }
            if (y == matrixBoard.topBorder + 1)
            {
                randomY = -1;
            }
            if (y == matrixBoard.bottomBorder - 1)
            {
                randomY = 1;
            }
            //if the letter is not near wall do it normaly
            if (x != matrixBoard.leftBorder + 1 && x != matrixBoard.rightBorder - 1)
            {
                randomX = GetRandomDirection();
            }
            if (y != matrixBoard.topBorder + 1 && y != matrixBoard.bottomBorder - 1)
            {
                randomY = GetRandomDirection();
            }
            //if it is spawned near a wall and the direction is agains the wall, the direction is changed to opposite
            int z = matrixXIndex + randomX;
            int q = matrixYIndex + randomY;
            while (matrixBoard.levelMatrix[z, q] != " ")
            {
                randomX *= GetRandomDirection();
                randomY *= GetRandomDirection();
                z = matrixXIndex + randomX;
                q = matrixYIndex + randomY;
            }
        }
        //get a random letter
        public void GetRandomLetter()
        {
            letter = letters[randomGenerator.Next(0, letters.Length)];
        }
        public void GetRandomPosition()
        {
            x = randomGenerator.Next(leftBorder, rightBorder);
            y = randomGenerator.Next(topBorder, bottomBorder);
            //to check for collision with the matrix board
            while (matrixBoard.levelMatrix[matrixXIndex, matrixYIndex] != " ")
            {
                x = randomGenerator.Next(leftBorder, rightBorder);
                y = randomGenerator.Next(topBorder, bottomBorder);
                matrixXIndex = x - matrixBoard.matrixOffsetX;
                matrixYIndex = y - matrixBoard.matrixOffsetY;
            }
        }
        //draws a letter inside the matrix
        public void DrawLetter()
        {
            //setting the cursor at random position and drawing a letter
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = letterColor;
            Console.Write(letter);
            Console.ResetColor();
        }
        public void Update()
        {
            //moving part
            letterUpdateTime.Start();
            if (letterUpdateTime.ElapsedMilliseconds == timeToStartMoving)
            {
                MoveLetter();
                DrawLetter();
                letterUpdateTime.Reset();
            }
        }

        private ConsoleColor LetterColor(int num)
        {
            switch (num)
            {
                case 1:
                    return ConsoleColor.Blue;
                case 2:
                    return ConsoleColor.Yellow;
                case 3:
                    return ConsoleColor.Green;
                case 4:
                    return ConsoleColor.Cyan;
                case 5:
                    return ConsoleColor.Magenta;
                case 6:
                    return ConsoleColor.DarkMagenta;
                case 7:
                    return ConsoleColor.DarkYellow;
                case 8:
                    return ConsoleColor.DarkGreen;
                default: return ConsoleColor.White;
            }
        }

        public void MoveLetter()
        {
            matrixXIndex = x - matrixBoard.matrixOffsetX;
            matrixYIndex = y - matrixBoard.matrixOffsetY;
            //to add a little dynamics untill the problem with letters bouncing from obstacles is fixed
            if (x <= (matrixBoard.leftBorder + 1))
            {
                randomX = 1;
            }
            if (x >= (matrixBoard.rightBorder - 1))
            {
                randomX = -1;
            }
            //if it is spawned near a wall and the direction is agains the wall, the direction is changed to opposite
            int z = matrixXIndex + randomX;
            int q = matrixYIndex + randomY;
            while (matrixBoard.levelMatrix[z, q] != " ")
            {
                randomX *= GetRandomDirection();
                randomY *= GetRandomDirection();
                z = matrixXIndex + randomX;
                q = matrixYIndex + randomY;
            }
            if (y <= (matrixBoard.topBorder + 1))
            {
                randomY = 1;
            }
            if (y >= (matrixBoard.bottomBorder - 1))
            {
                randomY = -1;
            }
            Console.SetCursorPosition(x, y);
            Console.Write(" ");

            x += randomX;
            y += randomY;
        }

        private int GetRandomDirection()
        {
            int[] direction = { -1, 1 };
            return direction[randomGenerator.Next(0, 2)];
        }

        public static void RemainingLetters(List<Letters> listOfLetters)
        {

            string remainingLetters = String.Format("Remaining letters: {0}", listOfLetters.Count);
            //to clear the screen on each update
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write(new string(' ', remainingLetters.Length + 1));

            Console.SetCursorPosition(Console.WindowWidth / 2 - 28, 8);
            Console.Write("Remaining letters: {0}", listOfLetters.Count);
            DrawRemainingLettersOrder(listOfLetters, Console.WindowWidth / 2 - 28 + (remainingLetters.Length + 1));
        }

        private static void DrawRemainingLettersOrder(List<Letters> listOfLetters, int position)
        {
            //clearing the screen
            Console.SetCursorPosition(position, 8);
            Console.Write(new string(' ', listOfLetters.Count + 2));

            for (int i = 0; i < listOfLetters.Count; i++)
            {
                Console.SetCursorPosition((position + i), 8);
                Console.ForegroundColor = listOfLetters[i].letterColor;
                Console.Write(listOfLetters[i].letter);
            }
            Console.ResetColor();
        }
    }
}
