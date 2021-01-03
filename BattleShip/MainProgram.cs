using System;

namespace BattleShip
{
    class MainProgram
    {
        static readonly Letters[] letters = (Letters[])Enum.GetValues(typeof(Letters));
        static void Main()
        {
            int[,] arr = new int[12, 12];
            Game.ResetArray(arr);
            Spawner.Spawn_1(arr, 4, 4);
            Spawner.Spawn_1(arr, 4, 6);
            Printer.PrintTest(arr);
            //Printer.PrintBattleship(arr,ConsoleColor.Red,ConsoleColor.DarkCyan, letters);
           // Printer.PrintTicFieldNext(arr,ConsoleColor.Red,ConsoleColor.Blue);
        }
    }
}
