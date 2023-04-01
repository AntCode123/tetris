using System;

namespace Tetris
{
    class Program
    {

        static void Main(String[] args)
        {
            /*
             * The this keyword is only useful in constructors as there can be variables that share the same name
             * but there is basically no reason to use them outside of constructors
             *
             * 
             * For coordinates you can use something like a structure or a record structure instead of a list:
             *
             * Beginning with C# 9, you can use the record keyword to define a reference type that provides built-in functionality for encapsulating data.
             * C# 10 allows the record class syntax as a synonym to clarify a reference type, and record struct to define a value type with similar functionality
             * src: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record
             * 
             * Here is how to implement a coordinates record structure:
             * 
             * public record struct Coords(int X, int Y);
             *
             * or if you want it to be mutable:
             * public record struct Coords
             * {
             *      public int X { get; set; }
             *      public int Y { get; set; }
             * }
             * 
             *
             * For all the shapes, implement a shape class that have the shape's properties e.g. coordinates of pixels, rotation, etc. OOP!
             * 
             *
             * If you want to initialise a list with values, you can use Enumerable.Repeat()
             * Usage:
             * IEnumerable<TResult> Repeat(TResult element, int count)
             * 
             *
             * Use the All() method in linq to check if all the values in a list are equal to a certain value instead of whatever the fuck this is:
             * `j[0] == 1 && j[1] == 1 && j[2] == 1 && j[3] == 1 && j[4] == 1 && j[5] == 1 && j[6] == 1 && j[7] == 1 && j[8] == 1 && j[9] == 1 && j[10] == 1 && j[10] == 1)`
             *
             * Usage:
             * bool All<TSource>(Func<TSource, bool> predicate)
             * Example:
             * if (j.All(i => i == 1) // do something
             *
             *
             * If you have code that you want to use more than once or some repetitive tasks you want to perform, create a function to make your code shorter and easier on the eyes
             *
             *
             * Tips:
             * - you can use the Last() function to get the last element of a list, just remember to use System.Linq
             * - use Console.ReadLine() (or input() in python) at the end of the program to let the user end the program by pressing enter
             */
            
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
                // Use System.Timers.Timer instead as rendering takes time which can cause irregular frame time
                game.Update();
            }
            // gameover message
            Console.Write($"GAME OVER. YOUR SCORE WAS {game.score}.");
            Thread.Sleep(3000);
        }
    }
}