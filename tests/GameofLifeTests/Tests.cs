using System;
using Xunit;

namespace GameOfLife.Tests
{
    public class Tests
    {
        private readonly GameService _service;

        public Tests()
        {
            _service = new GameService();
        }

        [Fact]
        public void Initialises_Game_Correctly()
        {
            var grid = _service.Initialise(20, 20);

            var next = _service.NextGeneration(grid);

            Assert.NotNull(grid);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Rule_1_Test(int numberOfNeighbours)
        {
            var cell = new Cell { State = State.Alive };

            var result = _service.ExecuteRuleOfLife(cell, numberOfNeighbours);

            Assert.True(result.State == State.Alive);
        }

        [Fact]
        public void Rule_2_Test()
        {
            var cell = new Cell { State = State.Dead };

            var result = _service.ExecuteRuleOfLife(cell, 3);

            Assert.True(result.State == State.Alive);
        }

        [Fact]
        public void Rule_3_DeadCell_Stays_Dead()
        {
            var cell = new Cell { State = State.Dead };

            var result = _service.ExecuteRuleOfLife(cell, 1);

            Assert.True(result.State == State.Dead);
        }

        [Fact]
        public void Rule_3_LiveCell_Dies()
        {
            var cell = new Cell { State = State.Alive };

            var result = _service.ExecuteRuleOfLife(cell, 4);

            Assert.True(result.State == State.Dead);
        }
    }
}
