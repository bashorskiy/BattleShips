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
		
		static uint fieldsize = 12;
		static readonly int limit = 20;
		static bool win;
		static int x, y;
		static int playerFlag; //0 = пусто, 1 = корабль, 2=ранен, 3 = убит, 4 = закрыт обстрел,5 = нельзя ставить, 8 = symbols
		static bool player = true;
		static bool tie;
		static Letters buffer_x;
		




		public static void Play(ConsoleColor X_ColorMain, ConsoleColor O_ColorMain)
		{ 
			int[,] firstPlayerField = new int[fieldsize, fieldsize];
			int[,] secondPlayerField = new int[fieldsize, fieldsize];
			

			string control = "1";
			while (control.Equals("1"))
			{
				tie = false;
				win = false;
				player = true; //player = true - 1-й игрок, player = false - 2-й игрок
				ResetArray(firstPlayerField);
				ResetArray(secondPlayerField);
				Console.Clear();
				while (!win)
				{
					if (player)
					{
						byte fourship = 1;
						byte threeship = 2;
						byte twoship = 3;
						byte oneship = 4;
						while (player) 
						{
							bool shipConstruct = true;
							Printer.PrintBattleship(firstPlayerField, X_ColorMain, O_ColorMain);
							Console.Write("Начинаем расстановку кораблей для ");
							Console.ForegroundColor = X_ColorMain;
							Console.Write("первого ");
							Console.ResetColor();
							Console.WriteLine("игрока!");
							Console.WriteLine($"Вы можете поставить: " +
								$"\t\t\t {fourship} четырёхпалубников " +
								$"\n\t\t\t {threeship} трёхпалубников" +
								$"\n\t\t\t {twoship} двухпалубников" +
								$"\n\t\t\t {oneship} однопалубников \n"+
								"Какой корабль хотите поставить?");
							Console.WriteLine("1.Однопалубный\n" +
								"2.Двухпалубный\n"+
								"3.Трёхпалубный\n"+
								"4.Четырёхпалубный");

							int shipSize = int.Parse(Console.ReadLine());

							while (shipConstruct)
							{
								Console.WriteLine("Пожалуйста, выберите точку, от которой будет отсчитываться выставление корабля");
								try
								{
									x = CoordX(firstPlayerField);
									y = CoordY(firstPlayerField);
								}
								catch (Exception)
								{
									Console.WriteLine("Поле не предусмотрено для таких значений");
									continue;
								}

								if (shipSize > 1 & shipSize < 5)
								{
									Console.WriteLine("Пожалуйста, выберите направление, в котором будет выставляться корабль");
									Console.WriteLine("1.Вверх\n" +
									"2.Вниз\n" +
									"3.Впрво\n" +
									"4.Влево");
									int direct = int.Parse(Console.ReadLine());
									Spawner.Spawn(firstPlayerField, x, y, shipSize, direct);
								}
								else if (shipSize == 1)
								{
									Spawner.Spawn(firstPlayerField, x, y, shipSize, 0);
								}
								else
								{
									Console.WriteLine("Таких кораблей не существует!");
								}
							}
							

						}
					}
					else
					{
						Printer.PrintBattleship(secondPlayerField, X_ColorMain, O_ColorMain);
						Console.Write("Начинаем расстановку кораблей для ");
						Console.ForegroundColor = O_ColorMain;
						Console.Write("второго ");
						Console.ResetColor();
						Console.WriteLine("игрока!");
					}
					while (true)
					{
						
					}
					Printer.PrintTicFieldNext(firstPlayerField, X_ColorMain, O_ColorMain);
					
					win = WinCheck(firstPlayerField, x, y, playerFlag);
					player = !player;
				}

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
				
				Console.WriteLine("Сыграете ещё раз или выйдете в меню?\n\r 1. Сыграть ещё раз \n 2. Выйти в меню");
				control = Console.ReadLine();
			}
		}

		public static int CoordX(int[,] userfield)
		{
			Console.Write($"Введите");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(" букву ");
			Console.ResetColor();
			Console.WriteLine("в промежутке от А до К");
			buffer_x = (Letters)Enum.Parse(typeof(Letters), Console.ReadLine());
			x = (int)buffer_x;
			return x;
		}
		public static int CoordY(int[,] userfield)
		{
			Console.Write($"Введите");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(" цифру ");
			Console.ResetColor();
			Console.WriteLine("в промежутке от 1 до 10");
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

							
						}
					}
				}
			}
			return winFlag;
		}

		
	}
}



