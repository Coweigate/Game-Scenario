using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CollectTheLettersTestVersion
{
    class Highscore
    {
        public static void GetHighScore()
        {
            using (var highscore = new StreamReader("highscore.txt"))
            {
                string line = highscore.ReadLine();
                while(line != null)
                {
                    Console.WriteLine(line);
                    line = highscore.ReadLine();
                }
            }
        }
        public static void AddHighscore(int score, string name)
        {
            bool ifNotAddScore = true;
            List<string> updateHighscore = new List<string>();
            using (var highscore = new StreamReader("highscore.txt"))
            {
                string line = highscore.ReadLine();
                while(line != null)
                {
                    int scoreFromLine = int.Parse(line.Split(' ')[2]);
                    string nameFromLine = line.Split(' ')[1];
                    if(score >= scoreFromLine && ifNotAddScore)
                    {
                        updateHighscore.Add(name + " " + score);
                        ifNotAddScore = false;
                    }
                    updateHighscore.Add(nameFromLine + " " + scoreFromLine);
                    line = highscore.ReadLine();
                }
            }
            if(ifNotAddScore)
            {
                updateHighscore.Add(name + " " + score);
                ifNotAddScore = false;
            }
            if(!ifNotAddScore)
            {
                using (var writeScore = new StreamWriter("highscore.txt"))
                {
                    int resultsLength = 5;
                    if(updateHighscore.Count < 5)
                    {
                        resultsLength = updateHighscore.Count;
                    }
                    for (int i = 0; i < resultsLength; i++)
                    {
                        writeScore.WriteLine((i + 1) + ". " + updateHighscore[i]);
                    }
                }
            }
        }
    }
}
