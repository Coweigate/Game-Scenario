using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CollectTheLettersTestVersion
{
    class Levels
    {
        static string[,] matrix = new string[56, 24];
        public static string[,] LevelOne()
        {
            var reader = new StreamReader("../../Levels/level1.txt");
            using (reader)
            {
                int j = 0;
                string line = reader.ReadLine();
                while (line != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        string matrixLine = line;

                        matrix[i, j] = matrixLine[i].ToString();
                    }
                    line = reader.ReadLine();
                    j++;
                }
            }
            return matrix;
        }

        public static string[,] LevelTwo()
        {
            var reader = new StreamReader("../../Levels/level2.txt");
            using (reader)
            {
                int j = 0;
                string line = reader.ReadLine();
                while (line != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        string matrixLine = line;

                        matrix[i, j] = matrixLine[i].ToString();
                    }
                    line = reader.ReadLine();
                    j++;
                }
            }
            return matrix;
        }
        public static string[,] LevelThree()
        {
            var reader = new StreamReader("../../Levels/level3.txt");
            using (reader)
            {
                int j = 0;
                string line = reader.ReadLine();
                while (line != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        string matrixLine = line;
                        //matrixLine = line.ToCharArray();
                        matrix[i, j] = matrixLine[i].ToString();
                    }
                    line = reader.ReadLine();
                    j++;
                }
            }
            return matrix;
        }
    }
}
