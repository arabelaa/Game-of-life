using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests
{
    [TestClass]
    public class GameOfLifeTests
    {
        [TestMethod]
        public void GameOfLife_Still_Block_life()
        {
            var world = new List<List<bool>>
            {
                new List<bool> {false, false, false, false},
                new List<bool> {false, true, true, false},
                new List<bool> {false, true, true, false},
                new List<bool> {false, false, false, false}
            };

            var result = new GameOfLife(4).GetNext(world);

            Assert.IsTrue(world.SequenceEqual(result, new EnumerableComparer<bool>()));
        }

        [TestMethod]
        public void GameOfLife_Oscilator_Blinker_life()
        {
            var world = new List<List<bool>>
            {
                new List<bool> {false, false, false, false, false},
                new List<bool> {false, false, true, false, false},
                new List<bool> {false, false, true, false, false},
                new List<bool> {false, false, true, false, false},
                new List<bool> {false, false, false, false, false}
            };

            var result = new GameOfLife(4).GetNext(world);

            var expected = new List<List<bool>>
            {
                new List<bool> {false, false, false, false, false},
                new List<bool> {false, false, false, false, false},
                new List<bool> {false, true, true, true, false},
                new List<bool> {false, false, false, false, false},
                new List<bool> {false, false, false, false, false}
            };
            Assert.IsTrue(expected.SequenceEqual(result, new EnumerableComparer<bool>()));
        }

        [TestMethod]
        public void GameOfLife_Oscilator_Beacon_life()
        {
            var world = new List<List<bool>>
            {
                new List<bool> {false, false, false, false, false, false},
                new List<bool> {false, false, false, true, true, false},
                new List<bool> {false, false, false, false, true, false},
                new List<bool> {false, true, false, false, false, false},
                new List<bool> {false, true, true, false, false, false},
                new List<bool> {false, false, false, false, false, false}
            };

            var result = new GameOfLife(5).GetNext(world);

            var expected = new List<List<bool>>
            {
                new List<bool> {false, false, false, false, false, false},
                new List<bool> {false, false, false, true, true, false},
                new List<bool> {false, false, false, true, true, false},
                new List<bool> {false, true, true, false, false, false},
                new List<bool> {false, true, true, false, false, false},
                new List<bool> {false, false, false, false, false, false}
            };
            Assert.IsTrue(expected.SequenceEqual(result, new EnumerableComparer<bool>()));
        }

        [TestMethod]
        public void GameOfLife_Oscilator_Beacon_Reverse_life()
        {
            var world = new List<List<bool>>
            {
               new List<bool> {false, false, false, false, false, false},
                new List<bool> {false, false, false, true, true, false},
                new List<bool> {false, false, false, true, true, false},
                new List<bool> {false, true, true, false, false, false},
                new List<bool> {false, true, true, false, false, false},
                new List<bool> {false, false, false, false, false, false}
            };

            var result = new GameOfLife(5).GetNext(world);

            var expected = new List<List<bool>>
            {
                new List<bool> {false, false, false, false, false, false},
                new List<bool> {false, false, false, true, true, false},
                new List<bool> {false, false, false, false, true, false},
                new List<bool> {false, true, false, false, false, false},
                new List<bool> {false, true, true, false, false, false},
                new List<bool> {false, false, false, false, false, false}
            };
            Assert.IsTrue(expected.SequenceEqual(result, new EnumerableComparer<bool>()));
        }

        [TestMethod]
        public void ConvertWorldToCells()
        {
            var world = new List<List<bool>>
            {
                new List<bool> {false, false, false, false},
                new List<bool> {false, true, true, false},
                new List<bool> {false, true, true, false},
                new List<bool> {false, false, false, false}
            };

            var result = world.Convert();

            var output = new List<Cell>
            {
                new Cell(new Position(0, 0), false),
                new Cell(new Position(0, 1), false),
                new Cell(new Position(0, 2), false),
                new Cell(new Position(0, 3), false),
                new Cell(new Position(1, 0), false),
                new Cell(new Position(1, 1), true),
                new Cell(new Position(1, 2), true),
                new Cell(new Position(1, 3), false),
                new Cell(new Position(2, 0), false),
                new Cell(new Position(2, 1), true),
                new Cell(new Position(2, 2), true),
                new Cell(new Position(2, 3), false),
                new Cell(new Position(3, 0), false),
                new Cell(new Position(3, 1), false),
                new Cell(new Position(3, 2), false),
                new Cell(new Position(3, 3), false)
            };
            CollectionAssert.AreEqual(output, result);
        }

        [TestMethod]
        public void GetNextState_ForStillLife_ReturnStillLife()
        {
            var world = new List<List<bool>>
            {
                new List<bool> {false, false, false, false},
                new List<bool> {false, true, true, false},
                new List<bool> {false, true, true, false},
                new List<bool> {false, false, false, false}
            };
            var cells = world.Convert();

            var nextState = new GameOfLife(4).GetNextState(cells);

            var expected = new List<Cell>
            {
                new Cell(new Position(0, 0), false),
                new Cell(new Position(0, 1), false),
                new Cell(new Position(0, 2), false),
                new Cell(new Position(0, 3), false),
                new Cell(new Position(1, 0), false),
                new Cell(new Position(1, 1), true),
                new Cell(new Position(1, 2), true),
                new Cell(new Position(1, 3), false),
                new Cell(new Position(2, 0), false),
                new Cell(new Position(2, 1), true),
                new Cell(new Position(2, 2), true),
                new Cell(new Position(2, 3), false),
                new Cell(new Position(3, 0), false),
                new Cell(new Position(3, 1), false),
                new Cell(new Position(3, 2), false),
                new Cell(new Position(3, 3), false)
            };
            CollectionAssert.AreEqual(expected, nextState);
        }

        [TestMethod]
        public void ConvertCellsToWorld()
        {
            var result = new List<Cell>
            {
                new Cell(new Position(0, 0), false),
                new Cell(new Position(0, 1), false),
                new Cell(new Position(0, 2), false),
                new Cell(new Position(0, 3), false),
                new Cell(new Position(1, 0), false),
                new Cell(new Position(1, 1), true),
                new Cell(new Position(1, 2), true),
                new Cell(new Position(1, 3), false),
                new Cell(new Position(2, 0), false),
                new Cell(new Position(2, 1), true),
                new Cell(new Position(2, 2), true),
                new Cell(new Position(2, 3), false),
                new Cell(new Position(3, 0), false),
                new Cell(new Position(3, 1), false),
                new Cell(new Position(3, 2), false),
                new Cell(new Position(3, 3), false)
            }.Convert();

            var expected = new List<List<bool>>
            {
                new List<bool> {false, false, false, false},
                new List<bool> {false, true, true, false},
                new List<bool> {false, true, true, false},
                new List<bool> {false, false, false, false}
            };
            Assert.IsTrue(expected.SequenceEqual(result, new EnumerableComparer<bool>()));
        }
    }
}