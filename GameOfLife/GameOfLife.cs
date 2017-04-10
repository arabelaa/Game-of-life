using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class GameOfLife
    {
        private readonly int _length;

        public GameOfLife(int length)
        {
            _length = length;
        }

        public List<List<bool>> GetNext(List<List<bool>> world)
        {
            var cells = world.Convert();
            var nextState = GetNextState(cells);
            var newWorld = nextState.Convert();
            return newWorld;
        }

        public List<Cell> GetNeighbours(Cell cell, List<Cell> worldCells)
        {
            var positions = cell.GetNeighboursPositions(_length);
            return worldCells.Where(x => positions.Contains(x.Position)).ToList();
        }

        public List<Cell> GetNextState(List<Cell> currentCells)
        {
            var rules = GetRules(currentCells);
            return
                currentCells.Select(cell => new { cell, rule = rules.FirstOrDefault(r => r.Key(cell)) })
                    .Select(arg => arg.rule.Equals(default(KeyValuePair<Func<Cell, bool>, bool>))
                        ? arg.cell
                        : new Cell(arg.cell.Position, arg.rule.Value)).ToList();
        }

        private IReadOnlyDictionary<Func<Cell, bool>, bool> GetRules(List<Cell> currentCells)
        {
            var aliveCellDiesBecauseUnderpopulation =
                new Tuple<Func<Cell, bool>, bool>(c => c.State && GetNeighbours(c, currentCells).Count(x => x.State) < 2, false);
            var aliveCellLivesBecauseSufficientNeighbours = new Tuple<Func<Cell, bool>, bool>(c => c.State &&
                    (GetNeighbours(c, currentCells).Count(x => x.State) == 2 ||
                     GetNeighbours(c, currentCells).Count(x => x.State) == 3), true);
            var aliveCellDiesBecauseOverPopulation = new Tuple<Func<Cell, bool>, bool>(
                c => c.State && GetNeighbours(c, currentCells).Count(x => x.State) > 3, false);
            var deadCellResurectsBecauseReproduction = new Tuple<Func<Cell, bool>, bool>(
                c => c.State == false && GetNeighbours(c, currentCells).Count(x => x.State) == 3, true);

            return new Dictionary<Func<Cell, bool>, bool>()
            {
                {aliveCellDiesBecauseUnderpopulation.Item1, aliveCellDiesBecauseUnderpopulation.Item2},
                {aliveCellLivesBecauseSufficientNeighbours.Item1, aliveCellLivesBecauseSufficientNeighbours.Item2},
                {aliveCellDiesBecauseOverPopulation.Item1, aliveCellDiesBecauseOverPopulation.Item2},
                {deadCellResurectsBecauseReproduction.Item1, deadCellResurectsBecauseReproduction.Item2}
            };
        }
    }
}