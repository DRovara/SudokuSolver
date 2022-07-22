using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public partial class Solver
    {
        public Game ToGame()
        {
            var digits = new List<(int, int, int)>();
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    if (PossibleDigits[i][j].Count == 1)
                    {
                        digits.Add((i, j, PossibleDigits[i][j].First()));
                    }
                }
            }
            return new Game(digits.ToArray());
        }

        private int CoordinatesToBoxId(int row, int column)
        {
            return row / 3 * 3 + column / 3;
        }

        private bool SeenBy(int row, int column, int digit)
        {
            for (var i = 0; i < 9; i++)
            {
                if (PossibleDigits[row][i].Count == 1 && PossibleDigits[row][i].First() == digit)
                {
                    return true;
                }
                if (PossibleDigits[i][column].Count == 1 && PossibleDigits[i][column].First() == digit)
                {
                    return true;
                }
                var (cy, cx) = BoxCellCoordinates(CoordinatesToBoxId(row, column), i / 3, i % 3);
                if (PossibleDigits[cy][cx].Count == 1 && PossibleDigits[cy][cx].First() == digit)
                {
                    return true;
                }
            }
            return false;
        }

        private (int, int) BoxCellCoordinates(int box, int index)
        {
            return BoxCellCoordinates(box, index / 3, index % 3);
        }


        private (int, int) BoxCellCoordinates(int box, int row, int column)
        {
            return ((box / 3) * 3 + row, (box % 3) * 3 + column);
        }

        private void InitHiddenCandidates()
        {
            RowPossibilities = new HashSet<int>[9][];
            ColumnPossibilities = new HashSet<int>[9][];
            BoxPossibilities = new HashSet<int>[9][];
            for (var i = 0; i < 9; i++)
            {
                RowPossibilities[i] = new HashSet<int>[9];
                ColumnPossibilities[i] = new HashSet<int>[9];
                BoxPossibilities[i] = new HashSet<int>[9];
                for (var j = 0; j < 9; j++)
                {
                    RowPossibilities[i][j] = new HashSet<int>();
                    ColumnPossibilities[i][j] = new HashSet<int>();
                    BoxPossibilities[i][j] = new HashSet<int>();
                    for (var k = 0; k < 9; k++)
                    {
                        //if (PossibleDigits[i][k].Count > 1 && !SeenBy(i, k, j + 1) || PossibleDigits[i][k].Count == 1 && PossibleDigits[i][k].First() == j + 1)
                        //    RowPossibilities[i][j].Add(k);
                        //if (PossibleDigits[k][i].Count > 1 && !SeenBy(k, i, j + 1) || PossibleDigits[k][i].Count == 1 && PossibleDigits[k][i].First() == j + 1)
                        //    ColumnPossibilities[i][j].Add(k);
                        //var (cy, cx) = BoxCellCoordinates(i, k / 3, k % 3);
                        //if (PossibleDigits[cy][cx].Count > 1 && !SeenBy(cy, cx, j + 1) || PossibleDigits[cy][cx].Count == 1 && PossibleDigits[cy][cx].First() == j + 1)
                        //    BoxPossibilities[i][j].Add(k);
                        if (PossibleDigits[i][k].Contains(j + 1))
                            RowPossibilities[i][j].Add(k);
                        if (PossibleDigits[k][i].Contains(j + 1))
                            ColumnPossibilities[i][j].Add(k);
                        var (cy, cx) = BoxCellCoordinates(i, k / 3, k % 3);
                        if (PossibleDigits[cy][cx].Contains(j + 1))
                            BoxPossibilities[i][j].Add(k);
                    }
                }
            }
        }

        private List<int> SearchCandidateTwinsRow(HashSet<int> possibilities, int row)
        {
            var twins = new List<int>();
            for(int i = 0; i < 9; i++)
            {
                if(PossibleDigits[row][i].SetEquals(possibilities))
                {
                    twins.Add(i);
                }
            }
            return twins;
        }

        private List<int> SearchCandidateTwinsColumn(HashSet<int> possibilities, int column)
        {
            var twins = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (PossibleDigits[i][column].SetEquals(possibilities))
                {
                    twins.Add(i);
                }
            }
            return twins;
        }

        private List<int> SearchCandidateTwinsBox(HashSet<int> possibilities, int box)
        {
            var twins = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                var (cy, cx) = BoxCellCoordinates(box, i);
                if (PossibleDigits[cy][cx].SetEquals(possibilities))
                {
                    twins.Add(i);
                }
            }
            return twins;
        }

    }
}
