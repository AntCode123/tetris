using System;

namespace Tetris
{
    class Game
    {
        public bool running = true;
        public int score = 0;
        public int width = 10;
        public int height = 20;
        public int timeInterval = 150;
        public List<List<int>> posTaken = new();

        public void ConfigurePosTaken(Field field)
        {
            // adding the wall coordinates to the positions taken list
            for (int i = 0; i < this.height + 1; i++)
            {
                for (int j = 0; j < this.width + 2; j++)
                {
                    if (field.grid[i][j] == 1)
                    {
                        this.posTaken.Add(new List<int> { j, i });
                    }
                }
            }
        }
        public void Display(Figure figure, Field field, Game game)
        {
            // displaying the field
            for (int i = 0; i < game.height + 1; i++)
            {
                for (int j = 0; j < game.width + 2; j++)
                {
                    switch (field.grid[i][j])
                    {
                        case 0:
                            Console.SetCursorPosition(j, i);
                            Console.Write(" ");
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.White;
                            this.Render(j, i);
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            this.Render(j, i);
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            this.Render(j, i);
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            this.Render(j, i);
                            break;
                        case 5:
                            Console.ForegroundColor = ConsoleColor.Green;
                            this.Render(j, i);
                            break;
                        case 6:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            this.Render(j, i);
                            break;
                        case 7:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            this.Render(j, i);
                            break;
                        case 8:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            this.Render(j, i);
                            break;
                    }
                }
            }
            // displaying the figure
            switch(figure.letter)
            {
                case 'I':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 'L':
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 'J':
                    Console.ForegroundColor = ConsoleColor.Blue;                    
                    break;
                case 'S':
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 'Z':
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case 'T':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 'O':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                List<int> block = figure.shape[i];
                this.Render(block[0], block[1]);
            }
            // displaying score
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 21);
            Console.Write($"SCORE {this.score}");
        }
        public void Render(int col, int row)
        {
            //rendering all blocks as a '#'
            Console.SetCursorPosition(col, row);
            Console.Write("o");
        }
        public void Input(Game game, Figure figure)
        {
            // handling user input to move, rotate and drop
            if (Console.KeyAvailable)
            {
                int key = Console.ReadKey(true).KeyChar;

                switch (key)
                {
                    case 97:
                        figure.Move(game, -1);
                        break;
                    case 100:
                        figure.Move(game, 1);
                        break;
                    case 114:
                        if (figure.letter != 'O')
                        {
                            figure.Rotate(game);
                        }
                        break;
                    case 115:
                        figure.Drop(game);
                        break;
                }
            }
        }
        public void Update()
        { 
            // updating the screen
            Thread.Sleep(this.timeInterval);
            Console.Clear();
        }

    }
}
