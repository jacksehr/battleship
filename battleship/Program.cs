using System;
using System.Threading;

namespace battleship
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            StateManager testState = new StateManager();
            Console.WriteLine("Initial empty board");
            testState.InitBoard();
            testState.PrintBoard();
            Thread.Sleep(1000);
            
            Console.WriteLine("Adding ships");
            testState.StartGame();
            testState.AddShip(0, 0, 0, 5);
            testState.AddShip(2, 0, 6, 0);
            testState.AddShip(5, 5, 8, 5);
            testState.AddShip(2, 8, 6, 8);
            PrintAndCheckLoss(testState);
            Thread.Sleep(1000);

            testState = new StateManager();
            testState.InitBoard();
            testState.AddShip(0, 2, 0, 6);
            testState.StartGame();
            Console.WriteLine("Starting game with one ship");
            Thread.Sleep(1000);
            PrintAndCheckLoss(testState);
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"Attacking ({0}, {i})");
                if (testState.ReceiveAttack(0, i))
                {
                    Console.WriteLine("Hit!");
                }
                else
                {
                    Console.WriteLine("Miss!");
                }
                PrintAndCheckLoss(testState);
            }
        }

        private static void PrintAndCheckLoss(StateManager state)
        {
            state.PrintBoard();
            Thread.Sleep(1000);
            if (state.hasLost())
            {
                Console.WriteLine("Game lost!");
            }

        }
    }
}
