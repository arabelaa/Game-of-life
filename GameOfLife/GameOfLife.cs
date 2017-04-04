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
            var nextState = GetNextState(_length, cells);
            var newWorld = nextState.Convert();
            return newWorld;
        }

        public List<Cell> GetNeighbours(Cell cell, List<Cell> worldCells)
        {
            var positions = cell.GetNeighboursPositions(_length);
            return worldCells.Where(x => positions.Contains(x.Position)).ToList();
        }

        public List<Cell> GetNextState(int max, List<Cell> currentCells)
        {
            var rules = GetRules(currentCells);
            return
                currentCells.Select(cell => new {cell, rule = rules.FirstOrDefault(r => r.Key(cell))})
                    .Select(arg => arg.rule.Equals(default(KeyValuePair<Func<Cell, bool>, bool>))
                        ? arg.cell
                        : new Cell(arg.cell.Position, arg.rule.Value)).ToList();
        }

        private IReadOnlyDictionary<Func<Cell, bool>, bool> GetRules(List<Cell> currentCells)
        {
            Func<Cell, bool> aliveCellFewNeighbours =
                c => c.State && GetNeighbours(c, currentCells).Count(x => x.State) < 2;
            Func<Cell, bool> aliveCellAllowedNeighbours =
                c =>
                    c.State &&
                    (GetNeighbours(c, currentCells).Count(x => x.State) == 2 ||
                     GetNeighbours(c, currentCells).Count(x => x.State) == 3);
            Func<Cell, bool> aliveCellMoreNeigbours =
                c => c.State && GetNeighbours(c, currentCells).Count(x => x.State) > 3;
            Func<Cell, bool> deadCellAllowedNeighbours =
                c => c.State == false && GetNeighbours(c, currentCells).Count(x => x.State) == 3;

            return new Dictionary<Func<Cell, bool>, bool>
            {
                {aliveCellFewNeighbours, false},
                {aliveCellAllowedNeighbours, true},
                {aliveCellMoreNeigbours, true},
                {deadCellAllowedNeighbours, true}
            };
        }
    }
}