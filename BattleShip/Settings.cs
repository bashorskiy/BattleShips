using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Settings
    {
        public static ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

        public static ConsoleColor ColorSet(ConsoleColor currentcolor)
        {
            Console.WriteLine($"Сейчас цвет этих кораблей: {Console.ForegroundColor = currentcolor}");
            Console.ResetColor();
            Console.WriteLine("Вот список всех доступных цветов:");
            
            for (int m = 0; m < colors.Length; m++)
            {
                if (colors[m] == currentcolor)
                {
                    Console.Write($"Сейчас цвет этих кораблей: {Console.ForegroundColor = currentcolor}");
                    Console.ResetColor();
                    Console.WriteLine($" под номером {m}\n");
                }
                else
                    Console.WriteLine($"Нажмите [{m}], чтобы выбрать {Console.ForegroundColor = colors[m]}\n");
                    Console.ResetColor();
            }
            int numb = int.Parse(Console.ReadLine());
            ConsoleColor newcolor = colors[numb];
              return newcolor;
        }


        public static void Menu(ConsoleColor player1shipColor, ConsoleColor waterColor)
        {
            Console.Write("Что хотите выбрать? \n" 
                                          /*$"1. Размер поля (сейчас {defaultSize - 2}х{defaultSize - 2})\n"*/);
            Console.Write($"1. Цвет кораблей (сейчас {Console.ForegroundColor = player1shipColor})\n");
            Console.ResetColor();
            Console.Write($"2. Цвет воды (сейчас {Console.ForegroundColor = waterColor})\n");
            Console.ResetColor();
            Console.WriteLine("3. Всё устраивает, хочу вернуться в меню");
        }
    }
}


