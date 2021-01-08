using System;

namespace BattleShip
{
    public class Printer
    {
        public static readonly Letters[] letters = (Letters[])Enum.GetValues(typeof(Letters));

        public static void PrintMenu(ConsoleColor friendColor, ConsoleColor waterColor)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t \t________________________________________________________\n");
            Console.Write("\t \t|         Добро пожаловать в Морской бой!              | \n");
            Console.Write("\t \t________________________________________________________\n\n\n");
            Console.ResetColor();
            Console.Write("1. Начать игру \n" +
                         $"2. Выбрать цвет кораблей \t");
            Console.Write($"\n\nСейчас: \t |");
            Console.Write("цвет своих кораблей:  " + (Console.ForegroundColor = friendColor));
            Console.ResetColor();
            Console.Write("| \n\t\t |");
            Console.Write("цвет воды: " + (Console.ForegroundColor = waterColor));
            Console.ResetColor();
            Console.Write("|\n\n3. Выйти \n");
        }
        public static void PrintTicFieldNext(int[,] arr, ConsoleColor X_Color, ConsoleColor O_Color)
        {
            for (int i = 0; i < arr.GetUpperBound(0); i++)
            {
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("N ");
                    Console.ResetColor();
                }
                else if (i < 10)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{i} ");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{i}");
                    Console.ResetColor();
                }
                for (int j = 0; j < arr.GetUpperBound(1); j++)
                {
                    if (i == 0 & j > 0 & j < 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($" {j} |");
                        Console.ResetColor();
                    }
                    else if (i == 0 & j >= 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{j} |");
                        Console.ResetColor();
                    }
                    //if ((i < minField) | (j < minField) | i > maxField | j > maxField)
                    //{
                    //    Console.Write("-|");
                    //}
                    else if (arr[i, j] == 1) // вывод красных крестиков
                    {
                        Console.ForegroundColor = X_Color;
                        Console.Write(" X");
                        Console.ResetColor();
                        Console.Write(" |");
                    }
                    else if (arr[i, j] == 2) // вывод синих ноликов
                    {
                        Console.ForegroundColor = O_Color;
                        Console.Write(" O");
                        Console.ResetColor();
                        Console.Write(" |");
                    }
                    else if (j == 0)
                    {
                        Console.Write("|");
                    }
                    else
                        Console.Write("   |");
                }
                Console.WriteLine();
            }
        }
        public static void PrintMenuError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\tНеверный пункт меню!\n");
            Console.ResetColor();
        }
        public static void PrintAmountError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\tТакие корабли недоступны!\n");
            Console.ResetColor();
        }
        public static void PrintPlaceError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\tКораблю здесь не развернуться!\n");
            Console.ResetColor();
        }
        public static void PrintTest(int[,] arr)
        {
            for (int i = 0; i < arr.GetUpperBound(0); i++)
            {
                for (int j = 0; j < arr.GetUpperBound(1); j++)
                {
                    Console.Write($"{arr[i, j]} |");
                }
                Console.WriteLine();
            }
        }


        public static void PrintBattleship(int[,] arr, ConsoleColor shipColor, ConsoleColor waterColor)
        {
            for (int i = 0; i < arr.GetUpperBound(0); i++)
            {
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" N");
                    Console.ResetColor();
                }
                else if (i < 10)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($" {i}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{i}");
                    Console.ResetColor();
                }
                for (int j = 0; j < arr.GetUpperBound(1); j++)
                {
                    if (i == 0 & j > 0 & j < 11)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{letters[j]}|");
                        Console.ResetColor();
                    }
                    else if (arr[i, j] == (int)Map.Wounded) // вывод ранений
                    {
                        Console.ForegroundColor = shipColor;
                        Console.Write("Х");
                        Console.ResetColor();
                        Console.Write("|");
                    }
                    else if (arr[i, j] == (int)Map.Ship) // вывод кораблей 
                    {
                        Console.ForegroundColor = shipColor;
                        Console.Write("=");
                        Console.ResetColor();
                        Console.Write("|");
                    }
                    else if ((arr[i, j] == (int)Map.Empty) & j != 0) // вывод моря
                    {
                        Console.ForegroundColor = waterColor;
                        Console.Write("~");
                        Console.ResetColor();
                        Console.Write("|");
                    }
                    else if (arr[i, j] == (int)Map.Oreol)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("%");
                        Console.ResetColor();
                        Console.Write("|");
                    }
                    else if (j == 0)
                    {
                        Console.Write("|");
                    }
                    else
                        Console.Write(" |");
                }
                Console.WriteLine();
            }
        }

        public static void PrintBattleField(int[,] arr, ConsoleColor shipColor, ConsoleColor waterColor)
        {
            for (int i = 0; i < arr.GetUpperBound(0); i++)
            {
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" N");
                    Console.ResetColor();
                }
                else if (i < 10)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($" {i}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{i}");
                    Console.ResetColor();
                }
                for (int j = 0; j < arr.GetUpperBound(1); j++)
                {
                    if (i == 0 & j > 0 & j < 11)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{letters[j]}|");
                        Console.ResetColor();
                    }
                    else if (arr[i, j] == (int)Map.Wounded) // вывод ранений
                    {
                        Console.ForegroundColor = shipColor;
                        Console.Write("Х");
                        Console.ResetColor();
                        Console.Write("|");
                    }
                    else if ((arr[i, j] == (int)Map.Empty || arr[i,j] == (int)Map.Ship)|| arr[i, j] == (int)Map.Oreol & j != 0) // вывод моря
                    {
                        Console.ForegroundColor = waterColor;
                        Console.Write("~");
                        Console.ResetColor();
                        Console.Write("|");
                    }

                    else if (arr[i, j] == (int)Map.Miss) // вывод промахов
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("*");
                        Console.ResetColor();
                        Console.Write("|");
                    }
                    else if (j == 0)
                    {
                        Console.Write("|");
                    }
                    else
                        Console.Write(" |");
                }
                Console.WriteLine();
            }
        }
    }
}



