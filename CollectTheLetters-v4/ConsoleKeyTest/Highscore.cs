using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace CollectTheLettersTestVersion
{
    class Highscore
    {
        public static void GetHighScore()
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            GameMenuAndMessages.ClearText();
            //drawing game name
            GameMenuAndMessages.printingTheTitle();
            //drawing lines
            GameMenuAndMessages.printFrame();
            //Positioning header
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition((width / 2) - 6, 9);
            Console.WriteLine("| HIGHSCORE |");
            Console.SetCursorPosition((width / 2) - 14, 12);
            Console.WriteLine("№     NAME     Score     Time");
            //get all the Lines from file and print them
            StringBuilder outputLine = new StringBuilder();
            int count = 0;
            using (var highscore = new StreamReader("highscore.txt"))
            {
                string line = highscore.ReadLine();
                while (line != null)
                {
                    count++;
                    if (count == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    }
                    Console.SetCursorPosition((width / 2) - 14, (height / 2) - 6 + count * 2);
                    outputLine = new StringBuilder();
                    outputLine.Append(line.Split(' ')[0].PadRight(5) + line.Split(' ')[1].PadRight(12) + line.Split(' ')[2].PadRight(9) + line.Split(' ')[3].PadRight(4));
                    Console.WriteLine(outputLine);
                    line = highscore.ReadLine();
                }
                Console.ForegroundColor = ConsoleColor.White;
                //info text
                Console.SetCursorPosition((width / 2) - 13, 26);
                Console.WriteLine("PRESS [BACKSPACE] TO BACK");
            }
        }

        public static void AddHighscore(int score, int time, string name)
        {
            bool ifNotAddScore = true;
            List<string> updateHighscore = new List<string>();
            //compare results
            using (var highscore = new StreamReader("highscore.txt"))
            {
                string line = highscore.ReadLine();
                while (line != null)
                {
                    int scoreFromLine = int.Parse(line.Split(' ')[2]);
                    string nameFromLine = line.Split(' ')[1];
                    int timeFromLine = int.Parse(line.Split(' ')[3]);
                    if (score > scoreFromLine && ifNotAddScore)
                    {
                        updateHighscore.Add(name + " " + score + " " + time);
                        ifNotAddScore = false;
                    }
                    else if (score == scoreFromLine && ifNotAddScore)
                    {
                        if (time < timeFromLine)
                        {
                            updateHighscore.Add(name + " " + score + " " + time);
                            ifNotAddScore = false;
                        }
                    }
                    updateHighscore.Add(nameFromLine + " " + scoreFromLine + " " + timeFromLine);
                    if(score == scoreFromLine && ifNotAddScore && time >= timeFromLine)
                    {
                        updateHighscore.Add(name + " " + score + " " + time);
                        ifNotAddScore = false;
                    }
                    line = highscore.ReadLine();
                }
            }
            //if no highscore
            if (ifNotAddScore)
            {
                updateHighscore.Add(name + " " + score + " " + time);
                ifNotAddScore = false;
            }
            //if compare highscore is the lowest
            if (!ifNotAddScore)
            {
                using (var writeScore = new StreamWriter("highscore.txt"))
                {
                    int resultsLength = 5;
                    if (updateHighscore.Count < 5)
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
