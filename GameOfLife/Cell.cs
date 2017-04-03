﻿using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public static class ExtensionsCell
    {
        public static List<List<bool>> Convert(this IEnumerable<Cell> cells)
        {
            var ordered = cells.OrderBy(x => x.Position.X).ThenBy(x => x.Position.Y);
            return ordered.GroupBy(x => x.Position.X).Select(a => a.Select(el => el.State).ToList()).ToList();
        }

        public static List<Cell> Convert(this IEnumerable<List<bool>> world)
        {
            return
                world.Select((c, x) => c.Select((l, y) => new Cell(new Position (x,y), l)))
                    .SelectMany(x => x)
                    .ToList();
        }
    }

    public class Cell
    {
        public Cell(Position position, bool state)
        {
            Position = position;
            State = state;
        }

        public bool State { get; }

        public Position Position { get; }

        public override int GetHashCode()
        {
            return Position.X.GetHashCode() + Position.Y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Cell;
            if (other == null)
                return false;
            return Position.X == other.Position.X && Position.Y == other.Position.Y && State == other.State;
        }
    }
}