using System;

/* 
 *          Game.ResetArray(firstPlayerField);
            Spawner.Spawn(firstPlayerField, 8, 8, shipSize:4, 1);
            Printer.PrintTest(arr);
            Settings.Menu((uint)firstPlayerField.GetLength(0),ConsoleColor.Red,ConsoleColor.DarkCyan);
            Printer.PrintBattleship(firstPlayerField,firstColor,secondColor);
            Printer.PrintTicFieldNext(arr,ConsoleColor.Red,ConsoleColor.Blue);
            int[,] firstPlayerField = new int[12, 12];
            int[,] secondPlayerField = new int[12, 12];
 */

namespace BattleShip
{
    class MainProgram
    {
        static readonly uint defaultSize = 12;
        

        static void Main()
        {


            ConsoleColor firstColor = ConsoleColor.Red;
            ConsoleColor secondColor = ConsoleColor.DarkCyan;          
            bool game = true;
            bool settings;
            bool menu = true;

            while (menu)
            {

                Printer.PrintMenu(firstColor, secondColor);
                bool choiceNum = short.TryParse(Console.ReadLine(), out short choice);
                if (choice == 1 & choiceNum)
                {
                    while (game)
                    {
                        Game.Play(firstColor, secondColor);
                        game = false;
                    }
                }
                else if (choice == 2 & choiceNum)
                {
                    settings = true;
                    while (settings)
                    {
                        Settings.Menu(defaultSize, firstColor, secondColor);
                        string point = Console.ReadLine();

                        if (point == "1")
                        {

                            break;
                        }
                        if (point == "2")
                        {
                            firstColor = Settings.ColorSet(firstColor);
                            break;
                        }
                        if (point == "3")
                        {
                            secondColor = Settings.ColorSet(secondColor);
                            break;
                        }
                        if (point == "4")
                        {
                            break;
                        }
                        else
                        {
                            Printer.PrintMenuError();
                        }
                    }
                }

                else if (choice == 3 & choiceNum)
                {
                    Console.WriteLine("Спасибо за игру! Пока!");
                    Console.ReadKey();
                    menu = false;
                }
                else
                {
                    Printer.PrintMenuError();
                }
            }
               

        }
    }
}
