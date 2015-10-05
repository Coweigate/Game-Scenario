using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectTheLettersTestVersion
{
    class Letters
    {
        public char letter;
        char[] letters = new char[26];
        int leftBorder, rightBorder, topBorder, bottomBorder, randomXPosition, randomYPosition;
        Random randomGenerator; 
        
        public int X
        {
            get { return randomXPosition; }
        }

        public int Y
        {
            get { return randomYPosition; }
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
            //getting the matrix borders
            leftBorder = matrix.leftBorder + 1;
            rightBorder = matrix.rightBorder;
            topBorder = matrix.topBorder + 1;
            bottomBorder = matrix.bottomBorder;

            GetRandomLetter();
            GetRandomPosition();
        }

        //get a random letter
        public void GetRandomLetter() {
            letter = letters[randomGenerator.Next(0, letters.Length)];            
        }
        public void GetRandomPosition() {
            randomXPosition = randomGenerator.Next(leftBorder, rightBorder);
            randomYPosition = randomGenerator.Next(topBorder, bottomBorder);
        }
        //draws a letter inside the matrix
        public void DrawLetter() {    
            //setting the cursor at random position and drawing a letter
            Console.SetCursorPosition(randomXPosition, randomYPosition);
            Console.ForegroundColor = ConsoleColor.Blue;            
            Console.Write(letter);
            Console.ResetColor();
        }
    }
}
