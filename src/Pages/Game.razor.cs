using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.Pages
{
    public partial class Game
    {
        public const int Width = 20;
        public const int Height = 20;
        public bool RunSimulation { get; set; } = false;
        [Inject] public GameService GameService { get; set; }
        public Cell[,] Grid { get; set; }

        protected override void OnInitialized()
        {
            Grid = GameService.Initialise(Width, Height);
        }

        public async Task Play()
        {
            RunSimulation = true;

            while (RunSimulation)
            {
                Grid = GameService.NextGeneration(Grid);

                StateHasChanged();

                await Task.Delay(500);
            }
        }

        public void ResetGame()
        {
            Grid = GameService.Initialise(Width, Height);
        }

        public void StopGame()
        {
            RunSimulation = false;
        }
    }
}
