using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectTheLettersTestVersion
{
    class Matrix
    {
        public string[,] matrix;
        public int leftBorder, rightBorder, topBorder, bottomBorder;
        int chosenLevel;
        int matrixPositionX, matrixPositionY;

        public Matrix(int level) {
            chosenLevel = level;
            CreateMatrix();
        } 

        public void CreateMatrix() {
            switch (chosenLevel) {
                case 1:
                    matrix = Levels.LevelOne();                    
                    break;
                default:
                    matrix = Levels.LevelOne();
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Error occured with the level choice.\nThe entered level is: {0}. Level 1 will be chosen by default.", chosenLevel);
                    break;
            }
            //to position the cursor
            matrixPositionX = (Console.WindowWidth - matrix.GetLength(0)) / 2;
            matrixPositionY = (Console.WindowHeight - matrix.GetLength(1)) / 2;

            //getting the matrix borders to limit the player movements(to not be able to move outside the matrix)
            leftBorder = matrixPositionX;            
            rightBorder = matrixPositionX + (matrix.GetLength(0) - 1);            
            topBorder = matrixPositionY;            
            bottomBorder = matrixPositionY + (matrix.GetLength(1) - 1);
        }
       
        public void DrawMatrix() {           
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.SetCursorPosition(matrixPositionX + i, matrixPositionY + j);
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
