using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindTheLettersGame
{
    class FindTheLettersGame
    {
        //declare struct to save player's coordinates and to move him
        struct PlayerCoordinates
        {
            public int x;
            public int y;

            public void PutCoordinates(int put, int put2)
            {
                x = put;
                y = put2;
            }

            public void moveLeft()
            {
                x--;
            }

            public void moveRight()
            {
                x++;
            }

            public void moveUp()
            {
                y--;
            }

            public void moveDown()
            {
                y++;
            }

        }
        /*static variables*/

        //declare level by default 1
        static int level = 1;
        //declare timer
        static Stopwatch timer = new Stopwatch();

        /*---------------------*/

        /*main method*/
        static void Main(string[] args)
        {
            /*intro part*/

            /*end of intro part*/
            Console.ReadKey();
            Console.Clear();
            MainLoop();


        }
        /*----------------------*/
        /*methods for intro part*/


        /*----------------------*/
        /*matrix generator*/






        /*----------------------*/

        /* MAIN LOOP METHOD */

        static void MainLoop()
        {
            //declare pause loop interval
            const int INTERVAL = 100;
            //declare time to find the letters
            int timeToFindTheLetters = 20;

            //reset and start timer
            timer = new Stopwatch();
            timer.Start();

            //check if time is up --> stop loop
            while (timeToFindTheLetters - timer.Elapsed.Seconds > 0)
            {
                // Sleep for a short period
                Thread.Sleep(INTERVAL);

                /*** Update the screen and handle input here! ***/
                Console.SetCursorPosition(0, 0);
                Console.Write("Timeleft: {0} sec", timeToFindTheLetters - timer.Elapsed.Seconds);

            }
            //stop timer
            timer.Stop();
            //print time is up text
            Console.WriteLine();
            Console.WriteLine("Time is up!");
        }

        /*---------------------*/

    }
}
