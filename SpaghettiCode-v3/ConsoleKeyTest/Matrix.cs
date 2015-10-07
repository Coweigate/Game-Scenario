using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectTheLettersTestVersion
{
    class Matrix
    {
        public string[,] levelMatrix;
        public int leftBorder, rightBorder, topBorder, bottomBorder;
        int chosenLevel;
        public int matrixOffsetX, matrixOffsetY;

        public Matrix(int level) {
            chosenLevel = level;
            CreateMatrix();
        } 

        public void CreateMatrix() {
            switch (chosenLevel) {
                case 1:
                    levelMatrix = Levels.LevelOne();                    
                    break;
                default:
                    levelMatrix = Levels.LevelOne();
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Error occured with the level choice.\nThe entered level is: {0}. Level 1 will be chosen by default.", chosenLevel);
                    break;
            }
            //to position the cursor
            matrixOffsetX = (Console.WindowWidth - levelMatrix.GetLength(0)) / 2;
            matrixOffsetY = (Console.WindowHeight - levelMatrix.GetLength(1)) / 2;

            //getting the matrix borders to limit the player movements(to not be able to move outside the matrix)
            leftBorder = matrixOffsetX;            
            rightBorder = matrixOffsetX + (levelMatrix.GetLength(0) - 1);            
            topBorder = matrixOffsetY;            
            bottomBorder = matrixOffsetY + (levelMatrix.GetLength(1) - 1);
        }
       
        public void DrawMatrix() {           
            for (int i = 0; i < levelMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < levelMatrix.GetLength(1); j++)
                {
                    Console.SetCursorPosition(matrixOffsetX + i, matrixOffsetY + j);
                    Console.Write(levelMatrix[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
