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
		
		static readonly uint fieldsize = 12;
		static readonly int limit = 20;
		
		static int x, y;
		 //0 = пусто, 1 = корабль, 2=ранен, 3 = убит, 4 = закрыт обстрел,5 = нельзя ставить, 8 = symbols
		
		
		public static void Play(ConsoleColor PlayerColor, ConsoleColor waterColor)
		{ 
			int[,] firstPlayerField = new int[fieldsize, fieldsize];
			int firstPlayerCount = 0;

			int[,] secondPlayerField = new int[fieldsize, fieldsize];
			int secondPlayerCount = 0;

			string control = "1";
			while (control.Equals("1"))
			{
				
				bool player = true; //player = true - 1-й игрок, player = false - 2-й игрок
				ResetArray(firstPlayerField);
				ResetArray(secondPlayerField);
				Console.Clear();
				
				Console.Write("Начинаем расстановку кораблей для ");
				Console.ForegroundColor = PlayerColor;
				Console.Write("первого ");
				Console.ResetColor();
				Console.WriteLine("игрока!");

				ShipContructer(firstPlayerField, PlayerColor, waterColor);

				Console.Clear();
				Console.WriteLine("Расстановка окончена!");

				Console.WriteLine("Нажмите Enter, чтобы перейти к расстановке кораблей второго игрока!");
				Console.ReadLine();
			
				Console.Write("\n\n\n\n\nНачинаем расстановку кораблей для ");
				Console.ForegroundColor = PlayerColor;
				Console.Write("второго ");
				Console.ResetColor();
				Console.WriteLine("игрока!");

				ShipContructer(secondPlayerField, PlayerColor, waterColor);

				Console.Clear();
				Console.WriteLine("Расстановка окончена!");
				bool game = true;
				bool missing;
				while (game)
				{
					if (player)
                    {
						missing = false;
						while(!missing)
						{
							Console.WriteLine("Ходит первый игрок!");
							missing = Shoot(secondPlayerField, ref firstPlayerCount,  PlayerColor, waterColor);
						}
						if (firstPlayerCount == limit)
						{
							game = false;
						}
						else
						{ 
							player = !player;
						}
                        
                    }
                    else
                    {
						missing = false;
						while (!missing)
						{
							Console.WriteLine("Ходит второй игрок!");
							missing = Shoot(firstPlayerField, ref secondPlayerCount, PlayerColor, waterColor);
						}
						if (secondPlayerCount == limit)
						{
							game = false;
						}
						else
						{
							player = !player;
						}				
					}
				}	
				
                if (player)
				{
					Console.ForegroundColor = PlayerColor;
					Console.WriteLine("\n\t\tПервый игрок победил!\n");
					Console.ResetColor();
				}
				else
				{
					Console.ForegroundColor = PlayerColor;
					Console.WriteLine("\n\t\tВторой игрок победил!\n");
					Console.ResetColor();
                }
				
				Console.WriteLine("Сыграете ещё раз или выйдете в меню?\n\r 1. Сыграть ещё раз \n 2. Выйти в меню");
				control = Console.ReadLine();
			}
		}

		public static int CoordY()
		{	
			again:
			Console.Write($"Введите");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(" букву ");
			Console.ResetColor();
			Console.WriteLine("в промежутке от А до К");

			bool correctEnter = Enum.TryParse<Letters>(Console.ReadLine(), out Letters buffer_y);
			if (correctEnter)
			{ 
				int coord_y = (int)buffer_y;
                if (coord_y<11)
                {
					return coord_y;
				}
                else
                {
					Printer.PrintPlaceError();
					goto again;
				}
			}
			else
            {
				Printer.PrintPlaceError();
				goto again;
            }
			
		}
		public static int CoordX()
		{
			again:
			Console.Write($"Введите");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(" цифру ");
			Console.ResetColor();
			Console.WriteLine("в промежутке от 1 до 10");
			bool correctcoord_x = int.TryParse(Console.ReadLine(),out int coord_x);
            if (correctcoord_x&coord_x<11)
            {
				return coord_x;
			}
			else
            {
				Printer.PrintPlaceError();
				goto again;
            }
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
					//Printer.PrintTest(playerField);
					Console.WriteLine($"Вы можете поставить: " +
					$"\n\t\t\t {fourship} четырёхпалубников " +
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

					bool corshipSize = int.TryParse(Console.ReadLine(),out int shipSize);
					if (corshipSize)
					{
						Console.WriteLine("Пожалуйста, введите координаты точки размещения корабля");
						y = CoordY();
						x = CoordX();
						if (shipSize > 1 & shipSize < 5)
						{
							if ((shipSize == 2 & twoship != 0) |
								(shipSize == 3 & threeship != 0)| 
								(shipSize == 4 & fourship != 0))
							{
								Console.WriteLine("Пожалуйста, выберите направление, в котором будет выставляться корабль");
								Console.WriteLine("1.Вверх\n" +
								"2.Вниз\n" +
								"3.Вправо\n" +
								"4.Влево");
								bool cordirect = int.TryParse(Console.ReadLine(), out int direct);
								if (cordirect)
								{
									Spawner.Spawn(playerField, x, y, shipSize, direct, ref oneship, ref twoship, ref threeship, ref fourship, ref countConstruct);
								}
								else
								{ Console.WriteLine("Такого направления не предусмотрено!"); }
							}
                            else
                            {Printer.PrintAmountError();}
						}
						else if (shipSize == 1 & oneship != 0)
						{
							Spawner.Spawn(playerField, x, y, shipSize, 0, ref oneship, ref twoship, ref threeship, ref fourship, ref countConstruct);
						}
						else
						{Printer.PrintAmountError();}
					}
					else
					{ Printer.PrintAmountError();}
				}
			}
			
		}


		public static void ResetArray(int[,] field)
		{
			for (int i = 0; i < field.GetLength(0); i++)
                
				for (int j = 0; j < field.GetLength(1); j++)
				{
					if ((i == 0 & j<11)|(j==0&i<11))
					{
						field[i, j] = (int)Map.Symbols;
					}
					else
					field[i, j] = (int)Map.Empty;
				}
		}

		public static bool Shoot(int[,] playerTarget,ref int playerCount, ConsoleColor shipColor, ConsoleColor waterColor)
        {
			bool miss = false;
			while (!miss)
			{
				Printer.PrintBattleField(playerTarget, shipColor, waterColor);
				if (playerCount == limit)
				{
					miss = true;
				}
				else
				{
					y = CoordY();
					x = CoordX();
					if (playerTarget[x, y] == (int)Map.Ship)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("\n\t\t\tПопадание!\n");
						Console.ResetColor();
						playerTarget[x, y] = (int)Map.Wounded;
						playerCount++;
					}
					else if (playerTarget[x, y] == (int)Map.Wounded)
					{
						Console.ForegroundColor = ConsoleColor.DarkYellow;
						Console.WriteLine("\n\t\t\tЭта клетка уже подбита!\n");
						Console.ResetColor();
					}
					else if (playerTarget[x, y] == (int)Map.Miss | playerTarget[x, y] == (int)Map.Symbols)
					{
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.WriteLine("\n\t\t\tСтрелять в эту клетку не имеет смысла - тут не может быть корабля\n");
						Console.ResetColor();
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\n\t\t\tМимо!\n");
						Console.ResetColor();
						playerTarget[x, y] = (int)Map.Miss;
						miss = true;
					}
				}
				
			}
			return miss;
        }
	}
}



