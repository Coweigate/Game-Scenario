using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectTheLettersTestVersion
{
    class Levels
    {
        public static string[,] LevelOne()
        {
            string[,] matrix = new string[56, 24];

            //initializing the matrix
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = " ";
                }
            }

            int numOfRows = matrix.GetLength(0);
            int numOfCols = matrix.GetLength(1);
            //top wall
            for (int i = 0; i < numOfCols; i++)
            {
                matrix[0, i] = "+";
            }
            //left wall
            for (int i = 1; i < numOfRows - 1; i++)
            {
                matrix[i, 0] = "+";
            }
            ////bottom wall
            for (int i = 0; i < numOfCols; i++)
            {
                matrix[numOfRows - 1, i] = "+";
            }
            ////right wall
            for (int i = 1; i < numOfRows - 1; i++)
            {
                matrix[i, numOfCols - 1] = "+";
            }
            return matrix;
        }
    }
}
