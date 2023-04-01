using System;

namespace Tetris
{
    class Program
    {

        static void Main(String[] args)
        {
            Game game = new();
            Field field = new();
            Figure figure = new();
            figure.Add(game);
            field.Configure(game);
            game.ConfigurePosTaken(field);

            // main game loop
            while (game.running)
            {
                game.Input(game, figure);
                game.Display(figure, field, game);
                figure.Fall(game, field);
                game.Update();
            }
            // gameover message
            Console.Write($"GAME OVER. YOUR SCORE WAS {game.score}.");
            Thread.Sleep(3000);
        }
    }
}