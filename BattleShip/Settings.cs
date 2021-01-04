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
                    Console.WriteLine($"под номером {m}\n");
                }
                else
                    Console.WriteLine($"Нажмите [{m}], чтобы выбрать {Console.ForegroundColor = colors[m]}\n");
                    Console.ResetColor();
            }
            int numb = int.Parse(Console.ReadLine());
            ConsoleColor newcolor = colors[numb];
              return newcolor;
        }

        //public static uint Size(uint defSize)
        //{
        //    bool setSize = true;
        //    while (setSize)
        //    {            
        //        Console.WriteLine("\n1.Изменить размер поля" + $"\t \t\t(сейчас размер поля {defSize-2}х{defSize-2})" );
        //        Console.WriteLine("2.Выйти в главное меню ");
        //        string settingsPoint = Console.ReadLine();
        //        if (settingsPoint == "1")
        //        {
        //            Console.WriteLine("\n\nВведите новый размер поля(минимальное значение: 5): \t ");
        //            bool correctSize = uint.TryParse(Console.ReadLine(), out uint newsize);
        //            if (correctSize&newsize>=5)

        //            {
        //                defSize = newsize+2;
        //                setSize = false;
        //            }
        //            else
        //            {
        //                Console.ForegroundColor = ConsoleColor.Red;
        //                Console.WriteLine("Такой размер, к сожалению, недопустим\n");
        //                Console.ResetColor();
        //            }
        //        }
        //        if (settingsPoint =="2")
        //        {
        //            setSize = false;
        //        }
        //    }
        //    return defSize;
        //}

        public static void Menu(uint defaultSize, ConsoleColor X_currentForeground, ConsoleColor O_currentForeground)
        {
            Console.Write("Что хотите выбрать? \n" 
                                          /*$"1. Размер поля (сейчас {defaultSize - 2}х{defaultSize - 2})\n"*/);
            Console.Write($"2. Цвет кораблей первого игрока (сейчас {Console.ForegroundColor = X_currentForeground})\n");
            Console.ResetColor();
            Console.Write($"3. Цвет кораблей второго игрока (сейчас {Console.ForegroundColor = O_currentForeground})\n");
            Console.ResetColor();
            Console.WriteLine("4. Всё устраивает, хочу вернуться в меню");
        }
    }
}


