using System;

namespace Tetris
{
    class Figure
    {
        public char letter;
        public List<List<int>> shape = new();
        public List<List<int>> ghost = new();
        public List<int> centreRotation = new();
        public List<char> letters = new List<char> { 'I', 'L', 'J', 'S', 'Z', 'T', 'O' };
        public void Add(Game game)
        { 
            // adding a shape to the field
            this.shape.Clear();
            Random rand = new();
            this.letter = this.letters[rand.Next(0, this.letters.Count)];
            this.letters.Remove(this.letter);
            game.timeInterval = 150;
            bool containsBlock = false;

            switch (this.letter)
            {
                case 'I':
                    this.shape = new List<List<int>> { new List<int> { 4, 0 }, new List<int> { 5, 0 }, new List<int> { 6, 0 }, new List<int> { 7, 0 } };
                    this.centreRotation = new List<int> { 5, 0 };
                    break;
                case 'J':
                    this.shape = new List<List<int>> { new List<int> { 4, 1 }, new List<int> { 5, 1 }, new List<int> { 6, 1 }, new List<int> { 6, 0 } };
                    this.centreRotation = new List<int> { 5, 1 };
                    break;
                case 'L':
                    this.shape = new List<List<int>> { new List<int> { 4, 0 }, new List<int> { 4, 1 }, new List<int> { 5, 1 }, new List<int> { 6, 1 } };
                    this.centreRotation = new List<int> { 5, 1 };
                    break;
                case 'S':
                    this.shape = new List<List<int>> { new List<int> { 4, 1 }, new List<int> { 5, 1 }, new List<int> { 5, 0 }, new List<int> { 6, 0 } };
                    this.centreRotation = new List<int> { 5, 1 };
                    break;
                case 'Z':
                    this.shape = new List<List<int>> { new List<int> { 4, 0 }, new List<int> { 5, 0 }, new List<int> { 5, 1 }, new List<int> { 6, 1 } };
                    this.centreRotation = new List<int> { 5, 1 };
                    break;
                case 'T':
                    this.shape = new List<List<int>> { new List<int> { 4, 1 }, new List<int> { 5, 1 }, new List<int> { 6, 1 }, new List<int> { 5, 0 } };
                    this.centreRotation = new List<int> { 5, 1 };
                    break;
                case 'O':
                    this.shape = new List<List<int>> { new List<int> { 5, 0 }, new List<int> { 6, 0 }, new List<int> { 5, 1 }, new List<int> { 6, 1 } };
                    this.centreRotation = new List<int> { 5, 1 };
                    break;
            }
            // refreshing the letters list
            if (this.letters.Count == 0)
            {
                this.letters = new List<char> { 'I', 'L', 'J', 'S', 'Z', 'T', 'O' };
            }
            // checking if the figures piles up to the top
            for (int i = 0; i < game.posTaken.Count; i++)
            {
                if (this.shape[0][0] == game.posTaken[i][0] && this.shape[0][1] == game.posTaken[i][1])
                {
                    game.running = false;
                    break;
                }
            }
        }
        public void Fall(Game game, Field field)
        {
            //making the figure fall
            List<bool> ableToFall = new();
            for (int i = 0; i < 4; i++)
            {
                List<int> block = this.shape[i];
                List<int> nextPos = new List<int> { block[0], block[1] + 1 };
                bool containsBlock = false;
                for (int j = 0; j < game.posTaken.Count; j++)
                {
                    if (nextPos[0] == game.posTaken[j][0] && nextPos[1] == game.posTaken[j][1])
                    {
                        containsBlock = true;
                        break;
                    }
                }
                if (containsBlock)
                {
                    ableToFall.Add(false);
                }
                else
                {
                    ableToFall.Add(true);
                }
            }
            bool containsFalse = false;
            for (int i = 0; i < 4; i++)
            {
                if (ableToFall[i] == false)
                {
                    containsFalse = true;
                }
            }
            if (!containsFalse)
            {
                for (int i = 0; i < 4; i++)
                {
                    List<int> block = this.shape[i];
                    this.shape[i] = new List<int> { block[0], block[1] + 1 };
                }
                this.centreRotation = new List<int> { this.centreRotation[0], this.centreRotation[1] + 1 };
            }
            else
            {
                field.Update(game, this);
                field.DropRow(game);
                this.Add(game);
            }
        }
        public void Move(Game game, int offset)
        {
            // moving the figure left or right
            List<bool> ableToMove = new();
            for (int i = 0; i < 4; i++)
            {
                List<int> block = this.shape[i];
                List<int> nextPos = new List<int> { block[0] + offset, block[1] };
                bool containsBlock = false;
                for (int j = 0; j < game.posTaken.Count; j++)
                {
                    if (nextPos[0] == game.posTaken[j][0] && nextPos[1] == game.posTaken[j][1])
                    {
                        containsBlock = true;
                        break;
                    }
                }
                if (containsBlock)
                {
                    ableToMove.Add(false);
                }
                else
                {
                    ableToMove.Add(true);
                }
            }
            bool containsFalse = false;
            for (int i = 0; i < 4; i++)
            {
                if (ableToMove[i] == false)
                {
                    containsFalse = true;
                }
            }
            if (!containsFalse)
            {
                for (int i = 0; i < 4; i++)
                {
                    List<int> block = this.shape[i];
                    this.shape[i] = new List<int> { block[0] + offset, block[1] };
                }
                this.centreRotation = new List<int> { this.centreRotation[0] + offset, this.centreRotation[1] };

            }
        }
        public void Rotate(Game game)
        {
            List<bool> ableToRotate = new();
            for (int i = 0; i < 4; i++)
            { 
                // rotating the figure
                List<int> block = this.shape[i];
                List<int> rotationFactor = new List<int> { -(block[1] - this.centreRotation[1]), block[0] - this.centreRotation[0] };
                List<int> nextPos = new List<int> { this.centreRotation[0] + rotationFactor[0], this.centreRotation[1] + rotationFactor[1] };
                bool containsBlock = false;
                for (int j = 0; j < game.posTaken.Count; j++)
                {
                    if (nextPos[0] == game.posTaken[j][0] && nextPos[1] == game.posTaken[j][1])
                    {
                        containsBlock = true;
                        break;
                    }
                }
                if (containsBlock)
                {
                    ableToRotate.Add(false);
                }
                else
                {
                    ableToRotate.Add(true);
                }
            }
            bool containsFalse = false;
            for (int i = 0; i < 4; i++)
            {
                if (ableToRotate[i] == false)
                {
                    containsFalse = true;
                }
            }
            if (!containsFalse)
            {
                for (int i = 0; i < 4; i++)
                {
                    List<int> block = this.shape[i];
                    List<int> rotationFactor = new List<int> { -(block[1] - this.centreRotation[1]), block[0] - this.centreRotation[0] };
                    this.shape[i] = new List<int> { this.centreRotation[0] + rotationFactor[0], this.centreRotation[1] + rotationFactor[1] };
                }
            }
        }
        public void Drop(Game game)
        { 
            // dropping the figure
            game.timeInterval = 10;
        }
    }
}
