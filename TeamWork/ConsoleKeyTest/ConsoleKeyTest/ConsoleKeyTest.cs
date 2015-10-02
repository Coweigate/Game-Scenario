using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleKeyTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			const int INITIAL_POSITION = 5;
			int wallWidth = 40, wallHeight = 20;
			int x = wallWidth / 2, y = wallHeight / 2;

			Console.WriteLine("To stop the game press ESC.");
			Console.WriteLine("Windows Width: {0}, Windows Height: {0}", Console.WindowWidth, Console.WindowHeight);

			//Drawing example matrix
			DrawMatrix (INITIAL_POSITION, wallHeight, wallWidth);

			do {
				if (Console.ReadKey(true).Key == ConsoleKey.D && x < wallWidth- 1) {
					x++;
				}
				if (Console.ReadKey(true).Key == ConsoleKey.S && y < wallHeight - 1) {
					y++;
				}
				if (Console.ReadKey(true).Key == ConsoleKey.A && x > INITIAL_POSITION + 1) {
					x--;
				}
				if (Console.ReadKey(true).Key == ConsoleKey.W && y > INITIAL_POSITION + 1) {
					y--;
				}

				Drawing(x, y, "$");
				Thread.Sleep(100);
				DeletingLastDraw(x,y);     
			} while (Console.ReadKey(true).Key != ConsoleKey.Escape);
		}

		private static void Drawing(int x, int y, string symbol)
		{
			Console.SetCursorPosition(x, y);
			Console.Write(symbol);
		}

		private static void DeletingLastDraw(int x, int y)
		{
			Drawing(x, y, " ");
		}

		protected static void WriteAt(string s, int x, int y)
		{
			Console.SetCursorPosition (x, y);
			Console.Write(s);		
		}

		private	static void DrawMatrix(int startIndex, int wallHeight, int wallWidth) {
			//left wall
			for (int i = startIndex; i < wallHeight; i++) {
				WriteAt ("#", startIndex, i);
			}
			//top wall
			for (int i = startIndex; i < wallWidth; i++) {
				WriteAt ("#", i, startIndex);
			}
			//right wall
			for (int i = startIndex; i < wallHeight; i++) {
				WriteAt ("#", wallWidth, i);
			}
			//bottom wall
			for (int i = startIndex; i < wallWidth + 1; i++) {
				WriteAt ("#", i, wallHeight);
			}
		}
	}
}
