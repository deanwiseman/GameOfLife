using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GameService
    {
        private const int Height = 20;
        private const int Width = 20;

        public Cell[,] Initialise(int width, int height) 
        {
            var grid = new Cell[width, height];

            for (var row = 0; row < height; row++)
            {
                for (var column = 0; column < width; column++)
                {
                    var rng = new Random().Next(0, 2);
                    var cell = new Cell((State) rng);
                    grid[row, column] = cell;
                }
            }

            return grid;
        }

        public Cell[,] NextGeneration(Cell[,] currentGeneration) 
        {
            var nextGeneration = new Cell[Width, Height];

            for (var row = 1; row < Height - 1; row++)
            {
                for (var column = 1; column < Width - 1; column++)
                {
                    var neighbours = CountNeighbours(currentGeneration, row, column);

                    var currentCell = currentGeneration[row, column];

                    if (currentCell.State == State.Alive)
                    {
                        neighbours--;
                    }

                    nextGeneration[row, column] = ExecuteRuleOfLife(currentGeneration[row, column], neighbours);

                }
            }

            return nextGeneration;
        }

        private int CountNeighbours(Cell[,] currentGeneration, int row, int column)
        {
            var result = 0;

            for (var i = -1; i <= 1; i++)
            {
                for (var k = -1; k <= 1; k++)
                {
                    Console.WriteLine($"i {i}, row {row}");
                    Console.WriteLine($"i {k}, column {column}");
                    if (currentGeneration[row + i, column + k]?.State == State.Alive)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        public Cell ExecuteRuleOfLife(Cell currentCell, int numberOfNeighbours)
        {
            // Rule 1. Any live cell with 2 or 3 live neighbours lives
            // Rule 2. Any dead cell with 3 live neighbours becomes a live cell
            // Rule 3. All other live cells die. Dead cells stay dead

            var cell = new Cell();

            if (currentCell.State == State.Alive)
            {
                if (numberOfNeighbours == 2 || numberOfNeighbours == 3)
                {
                    cell.State = State.Alive;
                }
                else
                {
                    cell.State = State.Dead;
                }
                 
            }
            else
            {
                if (numberOfNeighbours == 3)
                {
                    cell.State = State.Alive;
                }
                else
                {
                    cell.State = State.Dead;
                }
            }

            return cell;
        }
    }

    public class Cell
    {
        public State State { get; set; }

        public Cell() { }

        public Cell(State state)
        {
            State = state;
        }
    }

    public enum State
    {
        Alive,
        Dead
    }
}
