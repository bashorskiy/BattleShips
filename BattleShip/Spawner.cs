using System;

namespace BattleShip //0 = пусто, 1 = корабль, 2=ранен, 3 = убит, 4 = закрыт обстрел,5 = ободок, 8 = symbols
{
    enum Map
    {
		Empty,
		Ship,
		Wounded,
		Killed,
		DontShoot,
        Oreol
    }

	/*
	 * Если вверх: sign_x: 1, sign_y: -1, sign_vect_x: -1, sign_vect_y:1
	 * Если вниз: sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y:1
	 * Если вправо: sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y:1
	 * Если влево: sign_x: -1, sign_y: 1, sign_vect_x: 1, sign_vect_y:-1
	 */
	class Spawner
	{
        class Vert
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
			public static bool FreespaceCheck(int[,] field, int x, int y, int shipSize, int sign_x, int sign_y, int sign_vect_x, int sign_vect_y)
			{
				bool freespace = false;
				for (int vect_x = 0; vect_x < shipSize + 2; vect_x++)
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
							vect_x = shipSize + 2;
						}
					}
				}
				return freespace;
			}

			public static void Shipper(int[,] field, int x, int y, int shipSize, int sign_x, int sign_y, int sign_vect_x, int sign_vect_y)
			{
				for (int vect_x = 0; vect_x < shipSize+2; vect_x++)
				{
					for (int vect_y = 0; vect_y < 3; vect_y++)
					{
						if ((vect_x > 0 & vect_x < shipSize + 1) & vect_y == 1)
						{
							field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] = (int)Map.Ship;
						}
						else
						{
							field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] = (int)Map.Oreol;
						}
					}
				}
			}
		}
		class Horiz
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
			public static bool FreespaceCheck(int[,] field, int x, int y, int shipSize, int sign_x, int sign_y, int sign_vect_x, int sign_vect_y)
			{
				bool freespace = false;
				for (int vect_x = 0; vect_x < 3; vect_x++)
				{
					for (int vect_y = 0; vect_y < shipSize + 2; vect_y++)
					{
						if (field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] != (int)Map.Ship)
						{
							freespace = true;
						}
						else
						{
							freespace = false;
							vect_y = shipSize + 2;
						}
					}
				}
				return freespace;
			}

			public static void Shipper(int[,] field, int x, int y, int shipSize, int sign_x, int sign_y, int sign_vect_x, int sign_vect_y)
			{
				for (int vect_x = 0; vect_x < 3; vect_x++)
				{
					for (int vect_y = 0; vect_y < shipSize + 2; vect_y++)
					{
						if ((vect_y > 0 & vect_y < shipSize + 1) & vect_x == 1)
						{
							field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] = (int)Map.Ship;
						}
						else
						{
							field[(x + sign_x) + (sign_vect_x * vect_x), (y + sign_y) + (sign_vect_y * vect_y)] = (int)Map.Oreol;
						}
					}
				}
			}
		}
		

		


		public static void Spawn_1(int[,] field, int x, int y)
		{			
			if (Vert.FreespaceCheck(field, x, y, shipSize: 1, 
			sign_x: 1, sign_y: -1, sign_vect_x: -1, sign_vect_y:1))
			{
				Vert.Shipper(field, x, y, shipSize: 1, sign_x: 1, sign_y: -1, sign_vect_x: -1, sign_vect_y: 1);
			}		
			else
			{
				Printer.PrintPlaceError();
			}
		}
	

		public static void Spawn_2(int[,] field, int x, int y, short direct)
		{
			switch (direct)
			{
				case 1:    // вверх
					{
						if (Vert.FreespaceCheck(field, x, y, shipSize: 2,
						sign_x: 1, sign_y: -1, sign_vect_x: -1, sign_vect_y: 1))
						{
							Vert.Shipper(field, x, y, shipSize: 1, 
							sign_x: 1, sign_y: -1, sign_vect_x: -1, sign_vect_y: 1);
						}
						else
						{
							Printer.PrintPlaceError();
						}
						break;
					}
				case 2: // вниз
					{
						if (Vert.FreespaceCheck(field, x, y, shipSize: 2,
						sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y: 1))
						{
							Vert.Shipper(field, x, y, shipSize: 1,
							sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y: 1);
						}
						else
						{
							Printer.PrintPlaceError();
						}
						break;
					}
				case 3:    // вправо
					{
						if (Horiz.FreespaceCheck(field, x, y, shipSize: 2,
							sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y: 1))
						{
							Horiz.Shipper(field, x, y, shipSize: 1,
							sign_x: -1, sign_y: -1, sign_vect_x: 1, sign_vect_y: 1);
						}
						else
						{
							Printer.PrintPlaceError();
						}
						break;
					}
				case 4: // влево
					{
						if (Horiz.FreespaceCheck(field, x, y, shipSize: 2,
							sign_x: -1, sign_y: 1, sign_vect_x: 1, sign_vect_y: -1))
						{
							Horiz.Shipper(field, x, y, shipSize: 1,
							sign_x: -1, sign_y: 1, sign_vect_x: 1, sign_vect_y: -1);
						}
						else
						{
							Printer.PrintPlaceError();
						}
						break;
					}
			}
		}

		public static void Spawn_3(int[,] field, int x, int y, short direct)
		{
			switch (direct)
			{
				case 1:    // вверх
					{
						for (int i = 0; i < 5; i++)
						{
							for (int j = 0; j < 3; j++)
							{
								if (field[(x + 1) - i, (y - 1) + j] == 0)
								{
									field[(x + 1) - i, (y - 1) + j] = (int)Map.Oreol;
									if (i > 0 & i < 4 & j == 1)
									{
										field[(x + 1) - i, (y - 1) + j] = (int)Map.Ship;
									}
								}
								else
								{
									Printer.PrintPlaceError();
									break;
								}
							}
						}
						break;
					}
				case 2: // вниз
					{
						for (int i = 0; i < 5; i++)
						{
							for (int j = 0; j < 3; j++)
							{
								if (field[(x - 1) + i, (y - 1) + j] == 0)
								{
									field[(x - 1) + i, (y - 1) + j] = (int)Map.Oreol;
									if (i > 0 & i < 4 & j == 1)
									{
										field[(x - 1) + i, (y - 1) + j] = (int)Map.Ship;
									}
								}
								else
								{
									Printer.PrintPlaceError();
									break;
								}
							}
						}
						break;
					}
				case 3:    // вправо
					{
						for (int i = 0; i < 3; i++)
						{
							for (int j = 0; j < 5; j++)
							{
								if (field[(x - 1) + i, (y - 1) + j] == 0)
								{
									field[(x - 1) + i, (y - 1) + j] = (int)Map.Oreol;
									if ((j > 0 & j < 4) & i == 1)
									{
										field[(x - 1) + i, (y - 1) + j] = (int)Map.Ship;
									}
								}
								else
								{
									Printer.PrintPlaceError();
									break;
								}
							}
						}
						break;
					}
				case 4: // влево
					{
						for (int i = 0; i < 3; i++)
						{
							for (int j = 0; j < 5; j++)
							{
								if (field[(x - 1) + i, (y + 1) - j] == 0)
								{
									field[(x - 1) + i, (y + 1) - j] = (int)Map.Oreol;
									if ((j > 0 & j < 4) & i == 1)
									{
										field[(x - 1) + i, (y + 1) - j] = (int)Map.Ship;
									}
								}
								else
								{
									Printer.PrintPlaceError();
									break;
								}
							}
						}
						break;
					}
			}
		}

		public static void Spawn_4(int[,] field, int x, int y, short direct) // координата Х сопряжена с изменением i, Y сопряжена с изменением j
		{
			switch (direct)
			{
                case 1:    // вверх
                    {
						for (int i = 0; i < 6; i++)
						{
							for (int j = 0; j < 3; j++)
							{
								if (field[(x + 1) - i, (y - 1) + j] == 0)
								{
									field[(x + 1) - i, (y - 1) + j] = (int)Map.Oreol;
									if (i > 0 & i < 5 & j == 1)
									{
										field[(x + 1) - i, (y - 1) + j] = (int)Map.Ship;
									}
								}
								else
								{
									Printer.PrintPlaceError();
									break;
								}
							}
						}
						break;
					}
				case 2: // вниз
					{
						for (int i = 0; i < 6; i++)
						{
							for (int j = 0; j < 3; j++)
							{
								if (field[(x - 1) + i, (y - 1) + j] == 0)
								{
									field[(x - 1) + i, (y - 1) + j] = (int)Map.Oreol;
									if (i > 0 & i < 5 & j == 1)
									{
										field[(x - 1) + i, (y - 1) + j] = (int)Map.Ship;
									}
								}
								else
								{
									Printer.PrintPlaceError();
									break;
								}
							}
						}
						break;
					}
				case 3:    // вправо
					{
						for (int i = 0; i < 3; i++)
						{
							for (int j = 0; j < 6; j++)
							{
								if (field[(x - 1) + i, (y - 1) + j] == 0)
								{
									field[(x - 1) + i, (y - 1) + j] = (int)Map.Oreol;
									if ((j > 0 & j < 5) & i == 1)
									{
										field[(x - 1) + i, (y - 1) + j] = (int)Map.Ship;
									}
								}
								else
								{
									Printer.PrintPlaceError();
									break;
								}
							}
						}
						break;
					}
				case 4: // влево
					{
						for (int i = 0; i < 3; i++)
						{
							for (int j = 0; j < 6; j++)
							{
								if (field[(x - 1) + i, (y + 1) - j] == 0)
								{
									field[(x - 1) + i, (y + 1) - j] = (int)Map.Oreol;
									if ((j > 0 & j < 5) & i == 1)
									{
										field[(x - 1) + i, (y + 1) - j] = (int)Map.Ship;
									}
								}
								else
								{
									Printer.PrintPlaceError();
									break;
								}
							}
						}
						break;
					}
			}
		}
	}
}



