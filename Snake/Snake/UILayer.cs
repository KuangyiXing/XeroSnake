﻿using System;
using BusinessLayer;
using System.Collections.Generic;

namespace Snake
{
    class Program
    {
        private const int GameStepMilliseconds = 100;
        private static KeyListner keyListner = new KeyListner();

        private static bool ExitGame = false;
        private static BusinessLayer.gameMode currentGameMode;
        static bool gameSelected = false;

        static void Main(string[] args)
        {
            initialMenuLoad();

            currentGameMode = gameMode.basic;

            int mazeMode = ChooseMazeMode();
            GameSound gameSound = new GameSound();
            Engine gameEngine = new Engine(gameMode.basic, gameSound,mazeMode);

            Elements[,] Maze = gameEngine.initializeGame();
            gameSound.SoundWhilePlaying();
            Elements[,] updateMaze = Maze;

            Draw(Maze);

            do
            {
                int score = Score.getScore();
                drawScore(score);

                ConsoleKeyInfo keyInfo = keyListner.ReadKey(GameStepMilliseconds);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        updateMaze = gameEngine.updateGame(Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        updateMaze = gameEngine.updateGame(Direction.Down);
                        break;
                    case ConsoleKey.RightArrow:
                        updateMaze = gameEngine.updateGame(Direction.Right);
                        break;
                    case ConsoleKey.LeftArrow:
                        updateMaze = gameEngine.updateGame(Direction.Left);
                        break;
                    case ConsoleKey.Q:
                        ExitGame = true;
                        break;
                    default:
                        updateMaze = gameEngine.updateGame(Direction.Unchanged);
                        break;
                }

                if (updateMaze[0, 0] == Elements.snakeDeath)
                {
                    ExitGame = true;
                }
                System.Console.Clear();
                Draw(updateMaze);
            }
            while (ExitGame == false);

            endGame();
        }

        public static void Draw(Elements[,] DynamicMaze)
        {
            int rowLength = DynamicMaze.GetLength(0);
            int colLength = DynamicMaze.GetLength(1);
            Style style = new Style();
            for (int i = 0; i < rowLength; i++)
            {
                string row = "";
                for (int j = 0; j < colLength; j++)
                {
                    row += style.StyleMazeElement(DynamicMaze[i, j]);

                }
                if (row.Contains(" "))
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write(row);
                Console.Write(Environment.NewLine);
            }
        }

        public static void drawScore(int score)
        {
            System.Console.WriteLine("Score: " + score);
        }

        static void endGame()
        {
            Console.WriteLine("Your final score is " + Score.getScore());
            Console.WriteLine("The high score is " + Score.getHighScore());
            Console.WriteLine("Enter r to replay. q key to quit.");

            ConsoleKeyInfo keyInfo = keyListner.ReadKey(Int32.MaxValue);
            if (keyInfo.KeyChar.ToString().Equals("r", StringComparison.OrdinalIgnoreCase))
            {
                ExitGame = false;
                Console.Clear();
                Main(null);
            }
            else if (keyInfo.KeyChar.ToString().Equals("q", StringComparison.OrdinalIgnoreCase))
            {
                Environment.Exit(0);
            }
            endGame();
        }

        static void initialMenuLoad()
        {
            do
            {

                Style.menuImage();
                try
                {
                    currentGameMode = (gameMode)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine();
                }

                if (currentGameMode == gameMode.basic)
                {
                    gameSelected = true;
                }
            }
            while (gameSelected == false);
        }




        public static int ChooseMazeMode()
        {
            MazeLevel mazeMode;

            Console.WriteLine("1. Easy Mode : Line Maze");
            Console.WriteLine("2. Medium Mode : Cross Maze");
            Console.WriteLine("3. Hard Mode : Grid Maze");

            do
            {
                Console.WriteLine();
                Console.Write("Please enter 1,2 or 3 : ");
                
            }
            while (!Enum.TryParse(keyListner.ReadKey(Int32.MaxValue).KeyChar.ToString(), out mazeMode) || !Enum.IsDefined(typeof(MazeLevel),mazeMode));


            Console.Clear();
            return (int)mazeMode;

        }
    }
}
