using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public partial class Solver
    {
        public Game Game { get; private set; }

        public HashSet<int>[][] PossibleDigits { get; private set; }
        public HashSet<int>[][] RowPossibilities { get; private set; }
        public HashSet<int>[][] ColumnPossibilities { get; private set; }
        public HashSet<int>[][] BoxPossibilities { get; private set; }
        public bool Unsolvable { get; private set; }

        public bool DoHiddenSingles { get; set; }
        public bool DoHiddenPairs { get; set; }
        public bool DoHiddenTriples { get; set; }
        public bool DoHiddenQuadruples { get; set; }
        public bool DoNakedPairs { get; set; }
        public bool DoNakedTriples { get; set; }
        public bool DoNakedQuadruples { get; set; }

        public bool DoPointingPairs { get; set; }
        public bool DoBoxReduction { get; set; }
        public bool DoXWings { get; set; }
        public bool DoYWings { get; set; }

        public bool DoXYZWings { get; set; }
        public bool DoSwordfish { get; set; }
        public bool DoSimpleColouring { get; set; }

        public HashSet<int> this[int row, int column]
        {
            get
            {
                return PossibleDigits[row][column];
            }
        }

        public HashSet<int> this[Location location]
        {
            get
            {
                return this[location.Row, location.Column];
            }
        }


        private bool noChange;

        public Solver(Game game)
        {
            Game = game;
            Init();
        }

        public HashSet<int> PossibleDigitsInBox(int boxId, int element)
        {
            var row = boxId / 3 * 3 + element / 3;
            var column = boxId % 3 * 3 + element % 3;
            return PossibleDigits[row][column];
        }

        public void Init()
        {
            PossibleDigits = new HashSet<int>[9][];
            for (var i = 0; i < 9; i++)
            {
                PossibleDigits[i] = new HashSet<int>[9];
                for (var j = 0; j < 9; j++)
                {
                    PossibleDigits[i][j] = new HashSet<int>();
                    if (Game[i, j] == 0)
                    {
                        for (var k = 1; k <= 9; k++)
                        {
                            PossibleDigits[i][j].Add(k);
                        }
                    }
                    else
                    {
                        PossibleDigits[i][j].Add(Game[i, j]);
                    }
                }
            }
        }

        public void RemovePossibility(int row, int column, int digit)
        {
            if(PossibleDigits[row][column].Remove(digit))
            {
                if(PossibleDigits[row][column].Count == 1)
                {
                    Debug.WriteLine("Setting value " + PossibleDigits[row][column].First() + " for cell R" + row + "C" + column + " through REMOVE " + digit + ".");
                    OnDigitSet(row, column, PossibleDigits[row][column].First());
                }
                else
                {
                    Debug.WriteLine("Removed possibility " + digit + " from cell R" + row + "C" + column + ".");
                }
                noChange = false;
            }
        }

        public void SetDigit(int row, int column, int digit)
        {
            if (PossibleDigits[row][column].Count == 1)
                return;
            PossibleDigits[row][column].Clear();
            PossibleDigits[row][column].Add(digit);
            Debug.WriteLine("Setting value " + digit + " for cell R" + row + "C" + column + " through SET.");
            noChange = false;
            OnDigitSet(row, column, digit);
        }

        private void OnDigitSet(int row, int column, int digit)
        {
            UpdatePossibilities(row, column);
        }

        private void UpdatePossibilities(int row, int column)
        {
            if(PossibleDigits[row][column].Count > 1)
            {
                return;
            }
            if(PossibleDigits[row][column].Count == 0)
            {
                Unsolvable = true;
                Debug.WriteLine("Cell R" + row + "C" + column + " does not have any valid digit.");
                return;
            }
            var digit = PossibleDigits[row][column].First();
            var boxX = column / 3;
            var boxY = row / 3;

            for(var i = 0; i < 9; i++)
            {
                if(i != row)
                {
                    RemovePossibility(i, column, digit);
                }
                if(i != column)
                {
                    RemovePossibility(row, i, digit);
                }

                var boxXI = boxX * 3 + i % 3;
                var boxYI = boxY * 3 + i / 3;
                if(boxXI != column || boxYI != row)
                {
                    RemovePossibility(boxYI, boxXI, digit);
                }
            }

        }

        private void UpdatePossibilities()
        {
            for(var i = 0; i < 9; i++)
            {
                for(var j = 0; j < 9; j++)
                {
                    UpdatePossibilities(i, j);
                    if (Unsolvable)
                        return;
                }
            }
        }

        private void UpdateHiddenSingles()
        {
            InitHiddenCandidates();
            for(var i = 0; i < 9; i++)
            {
                for(var digit = 0; digit < 9; digit++)
                {
                    if(RowPossibilities[i][digit].Count == 1)
                    {
                        SetDigit(i, RowPossibilities[i][digit].First(), digit + 1);
                    }
                    if (ColumnPossibilities[i][digit].Count == 1)
                    {
                        SetDigit(ColumnPossibilities[i][digit].First(), i, digit + 1);
                    }
                    if (BoxPossibilities[i][digit].Count == 1)
                    {
                        var (cy, cx) = BoxCellCoordinates(i, BoxPossibilities[i][digit].First());
                        SetDigit(cy, cx, digit + 1);
                    }
                }
            }
        }

        #region NAKED SETS
        private void NakedSetRows(int setSize)
        {
            for (int row = 0; row < 9; row++)
            {
                var setIndices = new HashSet<int>();
                var possibilities = new HashSet<int>();
                for (int i = 0; i < setSize; i++)
                {
                    setIndices.Add(i);
                    possibilities.UnionWith(PossibleDigits[row][i]);
                }
                var repeat = true;
                var next = false;
                while (repeat)
                {
                    if (possibilities.Count > setSize || next)
                    {
                        next = false;
                        for (var i = 0; i < 8; i++)
                        {
                            if (setIndices.Contains(i) && !setIndices.Contains(i + 1))
                            {
                                setIndices.Remove(i);
                                setIndices.Add(i + 1);
                                possibilities.Clear();
                                foreach (var j in setIndices)
                                {
                                    possibilities.UnionWith(PossibleDigits[row][j]);
                                }
                                break;
                            }
                            if (i == 7)
                                repeat = false;
                        }
                    }
                    else
                    {
                        for (var i = 0; i < 9; i++)
                        {
                            if (!setIndices.Contains(i))
                            {
                                foreach (var d in possibilities)
                                    PossibleDigits[row][i].Remove(d);
                            }
                        }
                        next = true;
                    }
                }
            }
        }

        private void NakedSetColumns(int setSize)
        {
            for (int column = 0; column < 9; column++)
            {
                var setIndices = new HashSet<int>();
                var possibilities = new HashSet<int>();
                for (int i = 0; i < setSize; i++)
                {
                    setIndices.Add(i);
                    possibilities.UnionWith(PossibleDigits[i][column]);
                }
                var repeat = true;
                var next = false;
                while (repeat)
                {
                    if (possibilities.Count > setSize || next)
                    {
                        next = false;
                        for (var i = 0; i < 8; i++)
                        {
                            if (setIndices.Contains(i) && !setIndices.Contains(i + 1))
                            {
                                setIndices.Remove(i);
                                setIndices.Add(i + 1);
                                possibilities.Clear();
                                foreach (var j in setIndices)
                                {
                                    possibilities.UnionWith(PossibleDigits[j][column]);
                                }
                                break;
                            }
                            if (i == 7)
                                repeat = false;
                        }
                    }
                    else
                    {
                        for (var i = 0; i < 9; i++)
                        {
                            if (!setIndices.Contains(i))
                            {
                                foreach (var d in possibilities)
                                    PossibleDigits[i][column].Remove(d);
                            }
                        }
                        next = true;
                    }
                }
            }
        }

        private void NakedSetBoxes(int setSize)
        {
            for (int box = 0; box < 9; box++)
            {
                var setIndices = new HashSet<int>();
                var possibilities = new HashSet<int>();
                for (int i = 0; i < setSize; i++)
                {
                    setIndices.Add(i);
                    possibilities.UnionWith(PossibleDigitsInBox(box, i));
                }
                var repeat = true;
                var next = false;
                while (repeat)
                {
                    if (possibilities.Count > setSize || next)
                    {
                        next = false;
                        for (var i = 0; i < 8; i++)
                        {
                            if (setIndices.Contains(i) && !setIndices.Contains(i + 1))
                            {
                                setIndices.Remove(i);
                                setIndices.Add(i + 1);
                                possibilities.Clear();
                                foreach (var j in setIndices)
                                {
                                    possibilities.UnionWith(PossibleDigitsInBox(box, j));
                                }
                                break;
                            }
                            if (i == 7)
                                repeat = false;
                        }
                    }
                    else
                    {
                        for (var i = 0; i < 9; i++)
                        {
                            if (!setIndices.Contains(i))
                            {
                                foreach (var d in possibilities)
                                    PossibleDigitsInBox(box, i).Remove(d);
                            }
                        }
                        next = true;
                    }
                }
            }
        }
        #endregion

        #region HIDDEN SETS
        private void HiddenSetRows(int setSize)
        {
            for(var row = 0; row < 9; row++)
            {
                var setPossibilities = new HashSet<int>();
                var indices = new HashSet<int>();
                for (int i = 0; i < setSize; i++)
                {
                    setPossibilities.Add(i);
                    indices.UnionWith(RowPossibilities[row][i]);
                }
                var repeat = true;
                while (repeat)
                {
                    if (indices.Count > setSize)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (setPossibilities.Contains(i) && !setPossibilities.Contains(i + 1))
                            {
                                setPossibilities.Remove(i);
                                setPossibilities.Add(i + 1);
                                indices.Clear();
                                foreach (var j in setPossibilities)
                                {
                                    indices.UnionWith(RowPossibilities[row][j]);
                                }
                                break;
                            }
                            if (i == 7)
                                repeat = false;
                        }
                    }
                    else
                    {
                        for (var i = 0; i < 9; i++)
                        {
                            if (indices.Contains(i))
                            {
                                for(var j = 0; j < 9; j++)
                                {
                                    if (!setPossibilities.Contains(j))
                                        RemovePossibility(row, i, j + 1);
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void HiddenSetColumns(int setSize)
        {
            for (var column = 0; column < 9; column++)
            {
                var setPossibilities = new HashSet<int>();
                var indices = new HashSet<int>();
                for (int i = 0; i < setSize; i++)
                {
                    setPossibilities.Add(i);
                    indices.UnionWith(ColumnPossibilities[column][i]);
                }
                var repeat = true;
                while (repeat)
                {
                    if (indices.Count > setSize)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (setPossibilities.Contains(i) && !setPossibilities.Contains(i + 1))
                            {
                                setPossibilities.Remove(i);
                                setPossibilities.Add(i + 1);
                                indices.Clear();
                                foreach (var j in setPossibilities)
                                {
                                    indices.UnionWith(ColumnPossibilities[column][j]);
                                }
                                break;
                            }
                            if (i == 7)
                                repeat = false;
                        }
                    }
                    else
                    {
                        for (var i = 0; i < 9; i++)
                        {
                            if (indices.Contains(i))
                            {
                                for (var j = 0; j < 9; j++)
                                {
                                    if (!setPossibilities.Contains(j))
                                        RemovePossibility(i, column, j + 1);
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void HiddenSetBoxes(int setSize)
        {
            for (var box = 0; box < 9; box++)
            {
                var setPossibilities = new HashSet<int>();
                var indices = new HashSet<int>();
                for (int i = 0; i < setSize; i++)
                {
                    setPossibilities.Add(i);
                    indices.UnionWith(BoxPossibilities[box][i]);
                }
                var repeat = true;
                while (repeat)
                {
                    if (indices.Count > setSize)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (setPossibilities.Contains(i) && !setPossibilities.Contains(i + 1))
                            {
                                setPossibilities.Remove(i);
                                setPossibilities.Add(i + 1);
                                indices.Clear();
                                foreach (var j in setPossibilities)
                                {
                                    indices.UnionWith(BoxPossibilities[box][j]);
                                }
                                break;
                            }
                            if (i == 7)
                                repeat = false;
                        }
                    }
                    else
                    {
                        for (var i = 0; i < 9; i++)
                        {
                            var (cy, cx) = BoxCellCoordinates(box, i / 3, i % 3);
                            if (indices.Contains(i))
                            {
                                for (var j = 0; j < 9; j++)
                                {
                                    if (!setPossibilities.Contains(j))
                                        RemovePossibility(cy, cx, j + 1);
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }
        #endregion

        #region INTERSECTION REDUCTION
        private void PointingPairs()
        {
            for(var box = 0; box < 9; box++)
            {
                for(var digit = 0; digit < 9; digit++)
                {
                    var possibilities = BoxPossibilities[box][digit];
                    if (possibilities.Count < 2 || possibilities.Count > 3)
                        continue;
                    var rowIndex = possibilities.First() / 3;
                    var colIndex = possibilities.First() % 3;
                    foreach(var p in possibilities)
                    {
                        if(p / 3 != rowIndex)
                        {
                            rowIndex = -1;
                        }
                        if(p % 3 != colIndex)
                        {
                            colIndex = -1;
                        }
                    }
                    if(rowIndex >= 0)
                    {
                        rowIndex = (box / 3) * 3 + rowIndex;
                        for(int i = 0; i < 9; i++)
                        {
                            if(i / 3 != box % 3 || !possibilities.Contains(i % 3 + rowIndex % 3 * 3))
                                RemovePossibility(rowIndex, i, digit + 1);
                        }
                    }
                    if (colIndex >= 0)
                    {
                        colIndex = (box % 3) * 3 + colIndex;
                        for (int i = 0; i < 9; i++)
                        {
                            if (i / 3 != box / 3 || !possibilities.Contains(colIndex % 3 + i % 3 * 3))
                                RemovePossibility(i, colIndex, digit + 1);
                        }
                    }
                }
            }
        }

        private void BoxRowReduction()
        {
            for (var row = 0; row < 9; row++)
            {
                for (var digit = 0; digit < 9; digit++)
                {
                    var possibilities = RowPossibilities[row][digit];
                    if (possibilities.Count < 2 || possibilities.Count > 3)
                        continue;
                    var boxIndex = possibilities.First() / 3;
                    foreach (var p in possibilities)
                    {
                        if (p / 3 != boxIndex)
                        {
                            boxIndex = -1;
                        }
                    }
                    if (boxIndex >= 0)
                    {
                        boxIndex = (row / 3) * 3 + boxIndex;
                        for (int i = 0; i < 9; i++)
                        {
                            var (cy, cx) = BoxCellCoordinates(boxIndex, i);
                            if (!(possibilities.Contains(cx) && cy == row))
                            {
                                RemovePossibility(cy, cx, digit + 1);
                            }
                        }
                    }
                }
            }
        }

        private void BoxColumnReduction()
        {
            for (var column = 0; column < 9; column++)
            {
                for (var digit = 0; digit < 9; digit++)
                {
                    var possibilities = ColumnPossibilities[column][digit];
                    if (possibilities.Count < 2 || possibilities.Count > 3)
                        continue;
                    var boxIndex = possibilities.First() / 3;
                    foreach (var p in possibilities)
                    {
                        if (p / 3 != boxIndex)
                        {
                            boxIndex = -1;
                        }
                    }
                    if (boxIndex >= 0)
                    {
                        boxIndex = boxIndex * 3 + column / 3;
                        for (int i = 0; i < 9; i++)
                        {
                            var (cy, cx) = BoxCellCoordinates(boxIndex, i);
                            if (!(possibilities.Contains(cy) && cx == column))
                            {
                                RemovePossibility(cy, cx, digit + 1);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        private void RowXWings()
        {
            for(int digit = 0; digit < 9; digit++)
            {
                for(int row = 0; row < 8; row++)
                {
                    var possibilities1 = RowPossibilities[row][digit];
                    if(possibilities1.Count == 2)
                    {
                        for(int row2 = row + 1; row2 < 9; row2++)
                        {
                            var possibilities2 = RowPossibilities[row2][digit];
                            if(possibilities1.SetEquals(possibilities2))
                            {
                                for(int i = 0; i < 9; i++)
                                {
                                    if (i == row || i == row2)
                                        continue;
                                    foreach(var column in possibilities1)
                                    {
                                        RemovePossibility(i, column, digit + 1);
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void ColumnXWings()
        {
            for (int digit = 0; digit < 9; digit++)
            {
                for (int column = 0; column < 8; column++)
                {
                    var possibilities1 = ColumnPossibilities[column][digit];
                    if (possibilities1.Count == 2)
                    {
                        for (int column2 = column + 1; column2 < 9; column2++)
                        {
                            var possibilities2 = ColumnPossibilities[column2][digit];
                            if (possibilities1.SetEquals(possibilities2))
                            {
                                for (int i = 0; i < 9; i++)
                                {
                                    if (i == column || i == column2)
                                        continue;
                                    foreach (var row in possibilities1)
                                    {
                                        RemovePossibility(row, i, digit + 1);
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void SimpleColouring()
        {
            for(int digit = 1; digit <= 9; digit++)
            {
                var chains = SinglesChain.Build(this, digit);

                //SHARED SEEING
                foreach(var chain in chains)
                {
                    var elements = chain.Get();
                    for(var i = 0; i < elements.Count - 1; i++)
                    {
                        for(var j = i + 1; j < elements.Count; j++)
                        {
                            if (elements[i].Colour == elements[j].Colour)
                                continue;
                            var locations = Location.SeenByBoth(elements[i].Location, elements[j].Location);
                            foreach(var loc in locations)
                            {
                                RemovePossibility(loc.Row, loc.Column, digit);
                            }
                        }
                    }
                }

                //RULE OF TWO
                foreach(var chain in chains)
                {
                    var elements = chain.Get();
                    HashSet<int>[] boxElements = new HashSet<int>[9];
                    HashSet<int>[] rowElements = new HashSet<int>[9];
                    HashSet<int>[] columnElements = new HashSet<int>[9];
                    for(var i = 0; i < 9; i++)
                    {
                        boxElements[i] = new HashSet<int>();
                        rowElements[i] = new HashSet<int>();
                        columnElements[i] = new HashSet<int>();
                    }
                    foreach(var element in elements)
                    {
                        if(!boxElements[element.Location.Box].Add(element.Colour))
                        {
                            SetFromColourChain(chain, (element.Colour + 1) % 2, digit);
                            break;
                        }
                        if (!rowElements[element.Location.Row].Add(element.Colour))
                        {
                            SetFromColourChain(chain, (element.Colour + 1) % 2, digit);
                            break;
                        }
                        if (!columnElements[element.Location.Column].Add(element.Colour))
                        {
                            SetFromColourChain(chain, (element.Colour + 1) % 2, digit);
                            break;
                        }
                    }
                }
            }
        }

        private void SetFromColourChain(SinglesChain chain, int onColour, int digit)
        {
            var list = chain.Get();
            foreach(var element in list)
            {
                if(element.Colour == onColour)
                    SetDigit(element.Location.Row, element.Location.Column, digit);
            }
        }

        private void YWings()
        {
            for(int row = 0; row < 9; row++)
            {
                for(int column = 0; column < 9; column++)
                {
                    var possibilities = PossibleDigits[row][column];
                    if (possibilities.Count != 2)
                        continue;
                    var d1 = possibilities.First();
                    var d2 = possibilities.Last();
                    for(var i = 0; i < 9; i++)
                    {
                        var p2 = PossibleDigits[row][i];
                        if (p2.Count != 2)
                            continue;
                        var otherDigit = 0;
                        var thirdDigit = 0;
                        if (p2.Contains(d1) && !p2.Contains(d2))
                        {
                            otherDigit = d2;
                            if (d1 == p2.First())
                                thirdDigit = p2.Last();
                            else
                                thirdDigit = p2.First();
                        }
                        else if (p2.Contains(d2) && !p2.Contains(d1))
                        {
                            otherDigit = d1;
                            if (d2 == p2.First())
                                thirdDigit = p2.Last();
                            else
                                thirdDigit = p2.First();
                        }
                        else
                            continue;
                        
                        var locA = new Location(row, i);
                        for(var j = 0; j < 9; j++)
                        {
                            var possibilities2 = PossibleDigits[j][column];
                            if (possibilities2.Count != 2|| (j, column) == (locA.Row, locA.Column) || (j, column) == (row, column))
                                continue;
                            if (!(possibilities2.Contains(otherDigit) && possibilities2.Contains(thirdDigit)))
                                continue;
                            ApplyYWing(new Location(row, column), locA, new Location(j, column), thirdDigit);
                        }
                        for (var j = 0; j < 9; j++)
                        {
                            var (cy, cx) = BoxCellCoordinates(CoordinatesToBoxId(row, column), j);
                            var possibilities2 = PossibleDigits[cy][cx];
                            if (possibilities2.Count != 2 || (cy, cx) == (locA.Row, locA.Column) || (cy, cx) == (row, column))
                                continue;
                            if (!(possibilities2.Contains(otherDigit) && possibilities2.Contains(thirdDigit)))
                                continue;
                            ApplyYWing(new Location(row, column), locA, new Location(cy, cx), thirdDigit);
                        }
                    }
                    for (var i = 0; i < 9; i++)
                    {
                        var p2 = PossibleDigits[i][column];
                        if (p2.Count != 2)
                            continue;
                        var otherDigit = 0;
                        var thirdDigit = 0;
                        if (p2.Contains(d1) && !p2.Contains(d2))
                        {
                            otherDigit = d2;
                            if (d1 == p2.First())
                                thirdDigit = p2.Last();
                            else
                                thirdDigit = p2.First();
                        }
                        else if (p2.Contains(d2) && !p2.Contains(d1))
                        {
                            otherDigit = d1;
                            if (d2 == p2.First())
                                thirdDigit = p2.Last();
                            else
                                thirdDigit = p2.First();
                        }
                        else
                            continue;

                        var locA = new Location(i, column);
                        for (var j = 0; j < 9; j++)
                        {
                            var (cy, cx) = BoxCellCoordinates(CoordinatesToBoxId(row, column), j);
                            var possibilities2 = PossibleDigits[cy][cx];
                            if (possibilities2.Count != 2 || (cy, cx) == (locA.Row, locA.Column) || (cy, cx) == (row, column))
                                continue;
                            if (!(possibilities2.Contains(otherDigit) && possibilities2.Contains(thirdDigit)))
                                continue;
                            ApplyYWing(new Location(row, column), locA, new Location(cy, cx), thirdDigit);
                        }
                    }
                }
            }
        }

        private void ApplyYWing(Location l1, Location l2, Location l3, int digit)
        {
            var spaces = Location.SeenByBoth(l2, l3);
            foreach(var space in spaces)
            {
                RemovePossibility(space.Row, space.Column, digit);
            }
        }

        public void RowSwordFish()
        {
            for (var digit = 0; digit < 9; digit++)
            { 
                for (var row = 0; row < 7; row++)
                {
                    var possibilities = RowPossibilities[row][digit];
                    var totalPossibilities = new HashSet<int>();
                    if(possibilities.Count > 1 && possibilities.Count < 4)
                    {
                        totalPossibilities.UnionWith(possibilities);
                        for(var row2 = row + 1; row2 < 8; row2++)
                        {
                            var possibilities2 = RowPossibilities[row2][digit];
                            var newTotalPossibilities = new HashSet<int>();
                            newTotalPossibilities.UnionWith(totalPossibilities);
                            newTotalPossibilities.UnionWith(possibilities2);
                            if (newTotalPossibilities.Count > 3)
                                continue;
                            for(var row3 = row2 + 1; row3 < 9; row3++)
                            {
                                var possibilities3 = RowPossibilities[row3][digit];
                                var finalPossibilities = new HashSet<int>();
                                finalPossibilities.UnionWith(newTotalPossibilities);
                                finalPossibilities.UnionWith(possibilities3);
                                if(finalPossibilities.Count == 3)
                                {
                                    ApplySwordFish(new int[] { row, row2, row3 }, finalPossibilities.ToArray(), digit + 1);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ColumnSwordFish()
        {
            for (var digit = 0; digit < 9; digit++)
            {
                for (var column = 0; column < 7; column++)
                {
                    var possibilities = ColumnPossibilities[column][digit];
                    var totalPossibilities = new HashSet<int>();
                    if (possibilities.Count > 1 && possibilities.Count < 4)
                    {
                        totalPossibilities.UnionWith(possibilities);
                        for (var column2 = column + 1; column2 < 8; column2++)
                        {
                            var possibilities2 = ColumnPossibilities[column2][digit];
                            var newTotalPossibilities = new HashSet<int>();
                            newTotalPossibilities.UnionWith(totalPossibilities);
                            newTotalPossibilities.UnionWith(possibilities2);
                            if (newTotalPossibilities.Count > 3)
                                continue;
                            for (var column3 = column2 + 1; column3 < 9; column3++)
                            {
                                var possibilities3 = ColumnPossibilities[column3][digit];
                                var finalPossibilities = new HashSet<int>();
                                finalPossibilities.UnionWith(newTotalPossibilities);
                                finalPossibilities.UnionWith(possibilities3);
                                if (finalPossibilities.Count == 3)
                                {
                                    ApplySwordFish(finalPossibilities.ToArray(), new int[] { column, column2, column3 }, digit + 1);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ApplySwordFish(int[] rows, int[] columns, int digit)
        {
            for(var row = 0; row < 9; row++)
            {
                for(var column = 0; column < 9; column++)
                {
                    if (rows.Contains(row) && !columns.Contains(column))
                    {
                        RemovePossibility(row, column, digit);
                    }
                    if (columns.Contains(column) && !rows.Contains(row))
                    {
                        RemovePossibility(row, column, digit);
                    }
                }
            }
        }

        public void XYZWing()
        {
            for(var row1 = 0; row1 < 8; row1++)
            {
                for(var col1 = 0; col1 < 9; col1++)
                {
                    var l1 = new Location(row1, col1);
                    if (PossibleDigits[l1.Row][l1.Column].Count != 2)
                        continue;
                    for(var row2 = row1 + 1; row2 < 9; row2++)
                    {
                        for(var col2 = 0; col2 < 9; col2++)
                        {
                            var l2 = new Location(row2, col2);
                            if (PossibleDigits[l2.Row][l2.Column].Count != 2)
                                continue;
                            if (l1.Sees(l2))
                                continue;
                            var sharedValues = new HashSet<int>();
                            sharedValues.UnionWith(PossibleDigits[l1.Row][l1.Column]);
                            sharedValues.UnionWith(PossibleDigits[l2.Row][l2.Column]);
                            if (sharedValues.Count != 3)
                                continue;
                            var seenCells = Location.SeenByBoth(l1, l2);
                            foreach(var loc in seenCells)
                            {
                                if (PossibleDigits[loc.Row][loc.Column].SetEquals(sharedValues))
                                    ApplyXYZWing(l1, l2, loc);
                            }
                        }
                    }
                }
            }
        }

        private void ApplyXYZWing(Location l1, Location l2, Location pivot)
        {
            var seen = Location.SeenByAll(l1, l2, pivot);
            var digitToRemove = -1;
            foreach(var digit in PossibleDigits[pivot.Row][pivot.Column])
            {
                if(PossibleDigits[l1.Row][l1.Column].Contains(digit) && PossibleDigits[l2.Row][l2.Column].Contains(digit))
                {
                    digitToRemove = digit;
                }
            }
            foreach(var cell in seen)
            {
                RemovePossibility(cell.Row, cell.Column, digitToRemove);
            }
        }


        public Game Solve()
        {
            noChange = false;
            while (!noChange)
            {
                noChange = true;

                //Naked Singles
                UpdatePossibilities();

                if(DoHiddenSingles)
                    UpdateHiddenSingles();

                for(var i = 2; i <= 4; i++)
                {
                    if ((i == 2 && !DoNakedPairs) || (i == 3 && !DoNakedTriples) || (i == 4 && !DoNakedQuadruples))
                        continue;
                    NakedSetRows(i);
                    NakedSetColumns(i);
                    NakedSetBoxes(i);
                }
                for (var i = 2; i <= 4; i++)
                {
                    if ((i == 2 && !DoHiddenPairs) || (i == 3 && !DoHiddenTriples) || (i == 4 && !DoHiddenQuadruples))
                        continue;
                    InitHiddenCandidates();
                    HiddenSetRows(i);
                    HiddenSetColumns(i);
                    HiddenSetBoxes(i);
                }

                if(DoPointingPairs)
                    PointingPairs();

                if(DoBoxReduction)
                {
                    BoxRowReduction();
                    BoxColumnReduction();
                }

                if(DoXWings)
                {
                    RowXWings();
                    ColumnXWings();
                }

                if(DoSimpleColouring)
                    SimpleColouring();

                if(DoYWings)
                    YWings();

                if(DoSwordfish)
                {
                    RowSwordFish();
                    ColumnSwordFish();
                }

                if(DoXWings)
                    XYZWing();

                if (Unsolvable)
                    return null;
            }
            return ToGame();
        }
    }
}
