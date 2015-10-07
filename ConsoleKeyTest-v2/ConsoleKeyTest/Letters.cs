using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CollectTheLettersTestVersion
{
    class Letters
    {
        public char letter;        
        Matrix matrixBoard = new Matrix(1);
        public ConsoleColor letterColor;
        public bool hasBeenStepedOver = false;        
        char[] letters = new char[26];
        int leftBorder, rightBorder, topBorder, bottomBorder, x, y, randomX, randomY;
        Random randomGenerator;
        Stopwatch timer = new Stopwatch();
        int timeToStartMoving;

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public Letters(Matrix matrix, Random random) {
            //filling the matrix with letters from [65]A to[90]Z
            int num = 65;
            for (int i = 0; i < letters.Length; i++)
            {
                letters[i] = (char)num;
                num++;
            }
            randomGenerator = random;
            //setting the letter color
            letterColor = LetterColor(randomGenerator.Next(1, 9));
            //setting the letter moving time
            timeToStartMoving = randomGenerator.Next(150, 251);
            //getting the matrix borders
            leftBorder = matrix.leftBorder + 1;
            rightBorder = matrix.rightBorder;
            topBorder = matrix.topBorder + 1;
            bottomBorder = matrix.bottomBorder;

            GetRandomLetter();
            GetRandomPosition();
            randomX = GetRandomDirection();
            randomY = GetRandomDirection();
        }

        //get a random letter
        public void GetRandomLetter() {
            letter = letters[randomGenerator.Next(0, letters.Length)];            
        }
        public void GetRandomPosition() {
            x = randomGenerator.Next(leftBorder, rightBorder);
            y = randomGenerator.Next(topBorder, bottomBorder);
            //to check for collision with the matrix board
            //while (matrixBoard.matrix[randomXPosition, randomYPosition] != " ")
            //{
            //    randomXPosition = randomGenerator.Next(leftBorder, rightBorder);
            //    randomYPosition = randomGenerator.Next(topBorder, bottomBorder);
            //}
        }
        //draws a letter inside the matrix
        public void DrawLetter() {    
            //setting the cursor at random position and drawing a letter
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = letterColor;
            Console.Write(letter);
            Console.ResetColor();
        }
        public void Update() {
            //moving part
            timer.Start();
            if (timer.ElapsedMilliseconds == timeToStartMoving)
            {
                MoveLetter();
                DrawLetter();
                timer.Reset();
            }
        }

        private ConsoleColor LetterColor(int num)
        {
            switch (num) {
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
        
        public void MoveLetter() {    
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
            x += randomX;
            y += randomY;
            if (x <= (matrixBoard.leftBorder + 1) || x >= (matrixBoard.rightBorder  - 1)) {
                randomX *= -1;
            }
            if (y <= (matrixBoard.topBorder + 1) || y >= (matrixBoard.bottomBorder - 1)) {

                randomY *= -1;
            }
        }

        private int GetRandomDirection() {
            int[] direction = { -1, 1 };
            return direction[randomGenerator.Next(0, 2)];
        }

        public static void RemainingLetters(List<Letters> listOfLetters)
        {
            string remainingLetters = String.Format("Remaining letters: {0}", listOfLetters.Count);
            //to clear the screen on each update
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write(new string(' ', remainingLetters.Length + 1));

            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write("Remaining letters: {0}", listOfLetters.Count);
            DrawRemainingLettersOrder(listOfLetters, (remainingLetters.Length + 1));
        }

        private static void DrawRemainingLettersOrder(List<Letters> listOfLetters, int position)
        {
            //clearing the screen
            Console.SetCursorPosition(position, Console.WindowHeight - 3);
            Console.Write(new string(' ', listOfLetters.Count + 2));

            for (int i = 0; i < listOfLetters.Count; i++)
            {
                Console.SetCursorPosition((position + i), Console.WindowHeight - 3);
                Console.ForegroundColor = listOfLetters[i].letterColor;
                Console.Write(listOfLetters[i].letter);
            }
            Console.ResetColor();
        }
    }
}
