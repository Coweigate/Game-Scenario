﻿using System;
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

            Console.Clear();
            //drawing game name
            GameMenuAndMessages.printingTheTitle();
            //drawing lines
            Console.SetCursorPosition((MainClass.width / 2) - 34, (MainClass.height / 2) - 7);
            Console.WriteLine("===================================================================");
            Console.SetCursorPosition((width / 2) - 34, (height / 2) + 8);
            Console.WriteLine("===================================================================");
            //Positioning header
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition((width / 2) - 8, (height / 2) - 5);
            Console.WriteLine("-=| Highscore |=-");

            //get all the Lines from file and print them
            int count = 0;
            using (var highscore = new StreamReader("highscore.txt"))
            {
                string line = highscore.ReadLine();
                while(line != null)
                {
                    count++;
                    if(count == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    }
                    Console.SetCursorPosition((width / 2) - 6, (height / 2) - 4 + count*2);
                    Console.WriteLine(line);
                    line = highscore.ReadLine();
                }
                Console.ForegroundColor = ConsoleColor.White;
                //info text
                Console.SetCursorPosition((width / 2) - 10, (height / 2) + 11);
                Console.WriteLine("PRESS ANY KAY TO BACK");
            }
        }

        public static void AddHighscore(int score, string name)
        {
            bool ifNotAddScore = true;
            List<string> updateHighscore = new List<string>();
            //compare results
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
            //if no highscore
            if(ifNotAddScore)
            {
                updateHighscore.Add(name + " " + score);
                ifNotAddScore = false;
            }
            //if compare highscore is the lowest
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