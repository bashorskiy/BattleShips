using System;

namespace BattleShip //0 = пусто, 1 = корабль, 2=ранен, 3 = убит, 4 = закрыт обстрел,5 = ободок, 8 = symbols
{
    enum Map
    {
		Empty = 0,
		Ship = 1,
		Wounded = 2,
		Killed = 3,
		DontShoot = 4,
        Oreol = 5,
		Miss =6,
		Symbols = 8
    }

	/*
	 * Если вверх: sign_x: 1, sign_y: -1, sign_vect_x: -1, sign_vect_y:1
	 * Если вниз: sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y:1
	 * Если вправо: sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y:1
	 * Если влево: sign_x: -1, sign_y: 1, sign_vect_x: 1, sign_vect_y:-1
	 */
	class Spawner
	{
		public static void Spawn(int[,] field, int x, int y, int shipSize, int direct, ref byte oneship, ref byte twoship, ref byte threeship, ref byte fourship, ref int countConstruct)
		{
			if (shipSize == 1)
			{
				if (Vert.FreespaceCheck(field, x, y, shipSize,
				sign_x: 1, sign_y: -1, sign_vect_x: -1, sign_vect_y: 1))
				{

					Vert.MakeShip(field, x, y, shipSize,
					sign_x: 1, sign_y: -1, sign_vect_x: -1, sign_vect_y: 1);
					oneship--;
					countConstruct++;
				}
				else
				{
					Printer.PrintPlaceError();
				}
			}
			else
			{
				switch (direct)
				{
					case 1:    // вверх
						{
							if (Vert.FreespaceCheck(field, x, y, shipSize,
							sign_x: 1, sign_y: -1, sign_vect_x: -1, sign_vect_y: 1))
							{
								Vert.MakeShip(field, x, y, shipSize,
								sign_x: 1, sign_y: -1, sign_vect_x: -1, sign_vect_y: 1);
								switch (shipSize)
								{
									case 2: { twoship--; break; }
									case 3: { threeship--; break; }
									case 4: { fourship--; break; }
								}
								countConstruct++;
							}
							else
							{
								Printer.PrintPlaceError();
							}
							break;
						}
					case 2: // вниз
						{
							if (Vert.FreespaceCheck(field, x, y, shipSize,
							sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y: 1))
							{
								Vert.MakeShip(field, x, y, shipSize,
								sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y: 1);
								switch (shipSize)
								{
									case 2: { twoship--; break; }
									case 3: { threeship--; break; }
									case 4: { fourship--; break; }
								}
								countConstruct++;
							}
							else
							{
								Printer.PrintPlaceError();
							}
							break;
						}
					case 3:    // вправо
						{
							if (Horiz.FreespaceCheck(field, x, y, shipSize,
								sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y: 1))
							{
								Horiz.MakeShip(field, x, y, shipSize,
								sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y: 1);
								switch (shipSize)
								{
									case 2: { twoship--; break; }
									case 3: { threeship--; break; }
									case 4: { fourship--; break; }
								}
								countConstruct++;
							}
							else
							{
								Printer.PrintPlaceError();
							}
							break;
						}
					case 4: // влево
						{
							if (Horiz.FreespaceCheck(field, x, y, shipSize,
								sign_x: -1, sign_y: 1, sign_vect_x: 1, sign_vect_y: -1))
							{
								Horiz.MakeShip(field, x, y, shipSize,
								sign_x: -1, sign_y: 1, sign_vect_x: 1, sign_vect_y: -1);
								switch (shipSize)
								{
									case 2: { twoship--; break; }
									case 3: { threeship--; break; }
									case 4: { fourship--; break; }
								}
								countConstruct++;
							}
							else
							{
								Printer.PrintPlaceError();
							}
							break;
						}
				}
			}
		}


		private class Vert
        {
			/// <summary>
			/// Для вертикальных кораблей 
			/// <para> Проверка на отсутствие корабля в заданной ячейке и области (х - направлен вниз, у - направлен вправо) </para>
			/// </summary>
			/// <param name="field">поле</param>
			/// <param name="x">х-координата корабля</param>
			/// <param name="y">у-координата корабля</param>
			/// <param name="shipSize">размер корабля (одно/двух/трёх/четырёх-палубный)</param>
			/// <param name="sign_x">знак сдвига по х-координате от начальной точки для универсализации метода</param>
			/// <param name="sign_y">знак сдвига по у-координате от начальной точки для универсализации метода</param>
			/// <param name="sign_vect_x">знак направления обхода в х-координате </param>
			/// <param name="sign_vect_y">знак направления обхода в у-координате </param>
			/// <returns>Возвращает true если поле чистое или false, если в указанной области есть корабль</returns>
			internal static bool FreespaceCheck(int[,] field, int x, int y, int shipSize, int sign_x, int sign_y, int sign_vect_x, int sign_vect_y)
			{
				bool freespace = false;

				for (int vect_x = 0; vect_x < (shipSize + 2); vect_x++)
				{
					for (int vect_y = 0; vect_y < 3; vect_y++)
					{
						if (field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] != (int)Map.Ship)
						{
							freespace = true;
						}
						else 
						{
							freespace = false;
							return freespace;
						}
					}
				}
				return freespace;
			}

			internal static void MakeShip(int[,] field, int x, int y, int shipSize, int sign_x, int sign_y, int sign_vect_x, int sign_vect_y)
			{
				for (int vect_x = 0; vect_x < (shipSize+2); vect_x++)
				{
					for (int vect_y = 0; vect_y < 3; vect_y++)
					{
						if ((vect_x > 0 & vect_x < shipSize + 1) & vect_y == 1)
						{
							field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] = (int)Map.Ship;
						}
						else if (field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)]!=(int)Map.Symbols)
						{
							field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] = (int)Map.Oreol;
						}
					}
				}
			}
		}
		private class Horiz
        {
			/// <summary>
			/// Для горизонтальных кораблей 
			/// <para> Проверка на отсутствие корабля в заданной ячейке и области (х - направлен вниз, у - направлен вправо) </para>
			/// </summary>
			/// <param name="field">поле</param>
			/// <param name="x">х-координата корабля</param>
			/// <param name="y">у-координата корабля</param>
			/// <param name="shipSize">размер корабля (одно/двух/трёх/четырёх-палубный)</param>
			/// <param name="sign_x">знак сдвига по х-координате от начальной точки для универсализации метода</param>
			/// <param name="sign_y">знак сдвига по у-координате от начальной точки для универсализации метода</param>
			/// <param name="sign_vect_x">знак направления обхода в х-координате </param>
			/// <param name="sign_vect_y">знак направления обхода в у-координате </param>
			/// <returns>Возвращает true если поле чистое или false, если в указанной области есть корабль</returns>
			internal static bool FreespaceCheck(int[,] field, int x, int y, int shipSize, int sign_x, int sign_y, int sign_vect_x, int sign_vect_y)
			{
				bool freespace = false;
				for (int vect_x = 0; vect_x < 3; vect_x++)
				{
					for (int vect_y = 0; vect_y < (shipSize + 2); vect_y++)
					{
						if (field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] != (int)Map.Ship)							
						{
							freespace = true;
						}
						else
						{
							freespace = false;
							return freespace;
						}
					}
				}
				return freespace;
			}

			internal static void MakeShip(int[,] field, int x, int y, int shipSize, int sign_x, int sign_y, int sign_vect_x, int sign_vect_y)
			{
				for (int vect_x = 0; vect_x < 3; vect_x++)
				{
					for (int vect_y = 0; vect_y < shipSize + 2; vect_y++)
					{
						if ((vect_y > 0 & vect_y < shipSize + 1) & vect_x == 1)
						{
							field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] = (int)Map.Ship;
						}
						else if (field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] != (int)Map.Symbols)
						{
							field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] = (int)Map.Oreol;
						}
					}
				}
			}
		}
	}
}



