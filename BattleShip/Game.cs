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
		static bool win,game;
		static int x, y;
		static int playerFlag; //0 = пусто, 1 = корабль, 2=ранен, 3 = убит, 4 = закрыт обстрел,5 = нельзя ставить, 8 = symbols
		static bool player = true;
		static Letters buffer_x;
		




		public static void Play(ConsoleColor X_ColorMain, ConsoleColor O_ColorMain)
		{ 
			int[,] firstPlayerField = new int[fieldsize, fieldsize];
			int firstPlayerCount = 0;

			int[,] secondPlayerField = new int[fieldsize, fieldsize];
			int secondPlayerCount = 0;

			char control = '1';
			while (control.Equals("1"))
			{
				game = true;
				
				win = false;
				player = true; //player = true - 1-й игрок, player = false - 2-й игрок
				ResetArray(firstPlayerField);
				ResetArray(secondPlayerField);
				Console.Clear();
				
				Console.Write("Начинаем расстановку кораблей для ");
				Console.ForegroundColor = X_ColorMain;
				Console.Write("первого ");
				Console.ResetColor();
				Console.WriteLine("игрока!");
				ShipContructer(firstPlayerField, X_ColorMain, O_ColorMain);
				Console.Clear();
				Console.WriteLine("Расстановка окончена!");

				Console.WriteLine("Нажмите любую клавишу, для расстановки кораблей второго игрока!");
				Console.ReadKey();
											
				Console.Write("Начинаем расстановку кораблей для ");
				Console.ForegroundColor = O_ColorMain;
				Console.Write("второго ");
				Console.ResetColor();
				Console.WriteLine("игрока!");
				ShipContructer(secondPlayerField, X_ColorMain, O_ColorMain);
				Console.Clear();
				Console.WriteLine("Расстановка окончена!");

				while (game)
				{
					while (player)
                    {

                    }
                    while(!player)
                    {

                    }

				}
				
				win = WinCheck(firstPlayerField, x, y, playerFlag);
				player = !player;
				

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
				control = (char)Console.Read();
			}
		}

		public static int CoordX()
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
		public static int CoordY()
		{
			Console.Write($"Введите");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(" цифру ");
			Console.ResetColor();
			Console.WriteLine("в промежутке от 1 до 10");
			y = int.Parse(Console.ReadLine());
			return y;
		}

		public static void ShipContructer(int[,] playerField, ConsoleColor X_ColorMain, ConsoleColor O_ColorMain)
        {
			bool shipConstruct = true;
			byte fourship = 1;
			byte threeship = 2;
			byte twoship = 3;
			byte oneship = 4;
			int countConstruct = 0; 
			while (shipConstruct)
			{
				if (countConstruct == 10)
				{
					shipConstruct = false;
				}
				else
				{ 
					Printer.PrintBattleship(playerField, X_ColorMain, O_ColorMain);
					Console.WriteLine($"Вы можете поставить: " +
					$"\t\t\t\t {fourship} четырёхпалубников " +
					$"\n\t\t\t {threeship} трёхпалубников" +
					$"\n\t\t\t {twoship} двухпалубников" +
					$"\n\t\t\t {oneship} однопалубников \n" +
					"Какой корабль хотите поставить?");
					if (oneship != 0)
					{Console.WriteLine("1.Однопалубный"); }
					
					if (twoship != 0)
					{Console.WriteLine("2.Двухпалубный");}

					if (threeship != 0)
					{Console.WriteLine("3.Трёхпалубный");}

					if (fourship != 0)
					{Console.WriteLine("4.Четырёхпалубный");}

					int shipSize = int.Parse(Console.ReadLine());
					Console.WriteLine("Пожалуйста, введите координаты точки размещения корабля");
					x = CoordX();
					y = CoordY();
					if (shipSize > 1 & shipSize < 5)
					{
						switch (shipSize)
						{
							case 2: { twoship--; break; }
							case 3: { threeship--; break; }
							case 4: { fourship--; break; }
						}

						Console.WriteLine("Пожалуйста, выберите направление, в котором будет выставляться корабль");
						Console.WriteLine("1.Вверх\n" +
						"2.Вниз\n" +
						"3.Впрво\n" +
						"4.Влево");
						int direct = int.Parse(Console.ReadLine());
						Spawner.Spawn(playerField, x, y, shipSize, direct);
						countConstruct++;
					}
					else if (shipSize == 1)
					{
						oneship--;
						Spawner.Spawn(playerField, x, y, shipSize, 0);
						countConstruct++;
					}
					else
					{
						Console.WriteLine("Таких кораблей не существует!");
					}
				}
			}
			
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

		public static bool Shoot(int[,] playerTarget,int playerCount, int x, int y)
        {
			bool miss = false;
			while (!miss)
			{
				if (playerTarget[x, y] == (int)Map.Ship)
				{
					Console.WriteLine("Попадание!");
					playerTarget[x, y] = (int)Map.Wounded;
					playerCount++;
				}
                else if (playerTarget[x, y] == (int)Map.Wounded)
                {
					Console.WriteLine("Эта клетка уже подбита!");
					miss = true;
				}
				else if (playerTarget[x, y] == (int)Map.Oreol || x == 0 || y== 0)
                {
					Console.WriteLine("Стрелять в эту клетку не имеет смысла - тут не может быть корабля");
					miss = true;
				}
				else
                {
					Console.WriteLine("Мимо!");
					miss = true;
                }
			}
			return miss;
        }
	}
}



