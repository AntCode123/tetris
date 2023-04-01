using System;

namespace Tetris
{
    class Field
    {
        public List<List<int>> grid = new();
        public void Configure(Game game)
        {
            //constructing the field as a 2d list
            for (int i = 0; i < game.height; i++)
            {
                this.grid.Add(new List<int> { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
            }
            this.grid.Add(new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
        }
        public void Update(Game game, Figure figure)
        {
            // adding the figures to the field
            for (int i = 0; i < 4; i++)
            {
                List<int> block = figure.shape[i];
                switch (figure.letter)
                {
                    case 'I':
                        this.grid[block[1]][block[0]] = 2;
                        break;
                    case 'L':
                        this.grid[block[1]][block[0]] = 3;
                        break;
                    case 'J':
                        this.grid[block[1]][block[0]] = 4;
                        break;
                    case 'S':
                        this.grid[block[1]][block[0]] = 5;
                        break;
                    case 'Z':
                        this.grid[block[1]][block[0]] = 6;
                        break;
                    case 'T':
                        this.grid[block[1]][block[0]] = 7;
                        break;
                    case 'O':
                        this.grid[block[1]][block[0]] = 8;
                        break;
                }
                game.posTaken.Add(block);
            }
        }
        public void Refresh(Game game)
        {
            // refreshing the positions taken list
            for (int i = 0; i < game.height + 1; i++)
            {
                for (int j = 0; j < game.width + 2; j++)
                {
                    if (this.grid[i][j] == 1)
                    {
                        game.posTaken.Add(new List<int> { j, i });
                    }
                }
            }
        }
        public void DropRow(Game game)
        {
            // clearing a completed row and dropping everything above down one row
            for (int i = 0; i < game.height; i++)
            {
                List<int> j = this.grid[i];
                if (j[0] == 1 && j[1] == 1 && j[2] == 1 && j[3] == 1 && j[4] == 1 && j[5] == 1 && j[6] == 1 && j[7] == 1 && j[8] == 1 && j[9] == 1 && j[10] == 1 && j[10] == 1)
                {
                    this.grid.Remove(j);
                    this.grid.Insert(0, new List<int> { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
                    game.posTaken.Clear();
                    game.score++;
                    this.Refresh(game);
                }
            }
        }
    }
}
