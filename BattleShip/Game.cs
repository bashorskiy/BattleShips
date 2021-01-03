using System;

namespace BattleShip //0 = пусто, 1 = корабль, 2=ранен, 3 = убит, 4 = закрыт обстрел,5 = нельзя ставить, 8 = symbols
{
    public enum Letters
    {
		N = 0,
		А,
		Б,
		В,
		Г,
		Д,
		Е,
		Ж,
		З,
		И,
		К	
    }

	class Game
	{
		
		static uint fieldsize = 10;
		static readonly int limit = 5;
		static bool win;
		static int x, y;
		static int currentSymbolCode; //0 = пусто, 1 = корабль, 2=ранен, 3 = убит, 4 = закрыт обстрел,5 = нельзя ставить, 8 = symbols
		static bool player = true;
		static bool tie;
	   



		public static void Play(ConsoleColor X_ColorMain, ConsoleColor O_ColorMain)
		{ 
			int[,] player1field = new int[fieldsize, fieldsize];
			string control = "1";
			while (control.Equals("1"))
			{
				tie = false;
				win = false;
				player = true;
				ResetArray(player1field);
				Console.Clear();
				Printer.PrintTicFieldNext(player1field, X_ColorMain, O_ColorMain);
				while (!win)
				{
					if (player)
					{

						Console.Write("Ходит ");
						Console.ForegroundColor = X_ColorMain;
						Console.Write("первый ");
						Console.ResetColor();
						Console.WriteLine("игрок!");
						currentSymbolCode = 1;
					}
					else
					{
						Console.Write("Ходит ");
						Console.ForegroundColor = O_ColorMain;
						Console.Write("второй ");
						Console.ResetColor();
						Console.WriteLine("игрок!");
						currentSymbolCode = 2;
					}
					while (true)
					{
						try
						{
							x = CoordX(player1field);
							y = CoordY(player1field);

							if (player1field[x, y] == 3 &
								x >= (player1field.GetLowerBound(0) + 1) &
								x <= (player1field.GetUpperBound(0) - 1) &
								y >= (player1field.GetLowerBound(0) + 1) &
								y <= (player1field.GetUpperBound(1) - 1))
							{
								player1field[x, y] = currentSymbolCode;
								
								break;
							}
							else
							{
								Console.WriteLine("Извините, но сюда нельзя поставить ваш символ");
							}
						}
						catch (Exception)
						{
							Console.WriteLine("Поле не предусмотрено для таких значений");
							continue;
						}
					}
					Printer.PrintTicFieldNext(player1field, X_ColorMain, O_ColorMain);
					
					win = WinCheck(player1field, x, y, currentSymbolCode);
					player = !player;
				}
				if (tie)
				{
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.WriteLine("\t\t\tНичья!");
					Console.ResetColor();
				}
				else
				{
					if (!player)
					{
						Console.ForegroundColor = X_ColorMain;
						Console.WriteLine("Первый игрок победил!");
						Console.ResetColor();
					}
					else
					{
						Console.ForegroundColor = O_ColorMain;
						Console.WriteLine("Второй игрок победил!");
						Console.ResetColor();
					}
				}
				Console.WriteLine("Сыграете ещё раз или выйдете в меню?\n\r 1. Сыграть ещё раз \n 2. Выйти в меню");
				control = Console.ReadLine();
			}
		}

		public static int CoordX(int[,] userfield)
		{
			Console.Write($"Введите координату");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(" X ");
			Console.ResetColor();
			Console.WriteLine($"в промежутке от {(userfield.GetLowerBound(0) + 1)}" + " до " + $"{(userfield.GetUpperBound(0) - 1)}");
			x = int.Parse(Console.ReadLine());
			return x;
		}
		public static int CoordY(int[,] userfield)
		{
			Console.Write($"Введите координату");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(" Y ");
			Console.ResetColor();
			Console.WriteLine($"в промежутке от {(userfield.GetLowerBound(0) + 1)}" + " до " + $"{(userfield.GetUpperBound(0) - 1)}");
			y = int.Parse(Console.ReadLine());
			return y;
		}

		public static void ResetArray(int[,] field)
		{
			for (int i = 0; i < field.GetLength(0); i++)
				for (int j = 0; j < field.GetLength(1); j++)
				{
					field[i, j] = 0;
				}
		}

		

		public static bool WinCheck(int[,] field, int x, int y, int symbolCode)
		{
			bool winFlag = false;
			int vector_x, vector_y, innerCounter = 1;
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					if ((i != 0) | (j != 0))
					{
						if (field[x + i, y + j] == symbolCode)
						{
							innerCounter += 1;

							vector_x = i;

							vector_y = j;

							if (VectorWinCheck(field, x, y, vector_x, vector_y, innerCounter, symbolCode))
							{
								winFlag = true;
								break;
							}
							else
							{
								innerCounter = 1;
							}
						}
					}
				}
			}
			return winFlag;
		}

		public static bool VectorWinCheck(int[,] field, int x, int y, int vect_x, int vect_y, int count, int symbolNumber)
		{
			bool enough = false;
			int vectModule = 2;
			while (count < limit)
			{
				if (field[x + vect_x * vectModule, y + vect_y * vectModule] == symbolNumber)
				{
					count++;
					vectModule++;
				}
				else
				{
					break;
				}
			}

			vectModule = 1; // сбрасываем модуль

			while (count < limit)
			{
				if (field[x + (vect_x * (-vectModule)), y + (vect_y * (-vectModule))] == symbolNumber)
				{
					count++;
					vectModule++;
				}
				else
				{
					break;
				}
			}
			if (count == limit)
			{
				enough = true;
			}

			return enough;
		}
	}
}



