using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameOfLife.Tests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void GetNeighbours_LeftDown()
        {
            var cell = new Cell(new Position(0, 0), false);

            var result = cell.GetNeighboursPositions(4);
            var output = new List<Position>
            {
                new Position(1, 0),
                new Position(1, 1),
                new Position(0, 1)
            };
            CollectionAssert.AreEquivalent(output, result);
        }

        [TestMethod]
        public void GetNeighbours_LeftUp()
        {
            var cell = new Cell(new Position(0, 3), false);

            var result = cell.GetNeighboursPositions(4);
            var output = new List<Position>
            {
                new Position(1, 3),
                new Position(1, 2),
                new Position(0, 2)
            };
            CollectionAssert.AreEquivalent(output, result);
        }

        [TestMethod]
        public void GetNeighbours_RightUpper()
        {
            var cell = new Cell(new Position(3, 3), false);

            var result = cell.GetNeighboursPositions(4);
            var output = new List<Position>
            {
                new Position(3, 2),
                new Position(2, 3),
                new Position(2, 2)
            };

            CollectionAssert.AreEquivalent(output, result);
        }

        [TestMethod]
        public void GetNeighbours_RightDown()
        {
            var cell = new Cell(new Position(3, 0), false);

            var result = cell.GetNeighboursPositions(4);
            var output = new List<Position>
            {
                new Position(3, 1),
                new Position(2, 1),
                new Position(2, 0)
            };

            CollectionAssert.AreEquivalent(output, result);
        }

        [TestMethod]
        public void GetNeighbours_Center()
        {
            var cell = new Cell(new Position(2, 2), false);

            var result = cell.GetNeighboursPositions(4);
            var output = new List<Position>
            {
                new Position(1, 2),
                new Position(1, 3),
                new Position(1, 1),
                new Position(2, 1),
                new Position(2, 3),
                new Position(3, 1),
                new Position(3, 2),
                new Position(3, 3)
            };

            CollectionAssert.AreEquivalent(output, result);
        }
    }
}