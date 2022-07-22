using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Game
    {
        public int this[int row, int column]
        {
            get
            {
                return Digits[row][column];
            }
            set
            {
                if(value < 0 || value > 9)
                {
                    throw new ArgumentException("Sudoku Digits must be between 1 and 9.");
                }
                Digits[row][column] = value;
            }
        }

        private int[][] Digits;

        public Game((int, int, int)[] knownDigits)
        {
            Digits = new int[9][];
            for(var i = 0; i < 9; i++)
            {
                Digits[i] = new int[9];
            }
            foreach(var (row, column, digit) in knownDigits)
            {
                this[row, column] = digit;
            }
        }

        public Game() : this(new (int, int, int)[0])
        { }

        public List<(int, int, int)> GetDigits()
        {
            var d = new List<(int, int, int)>();
            for(var i = 0; i < 9; i++)
            {
                for(var j = 0; j < 9; j++)
                {
                    if(this[i, j] != 0)
                    {
                        d.Add((i, j, this[i, j]));
                    }
                }
            }
            return d;
        }

        public static Game Parse(string text)
        {
            var lines = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var digits = new List<(int, int, int)>();
            for(var i = 0; i < 9; i++)
            {
                var line = lines[i];
                for (var j = 0; j < 9; j++)
                {
                    if(line[j] != ' ')
                    {
                        digits.Add((i, j, int.Parse(line[j].ToString())));
                    }
                }
            }
            return new Game(digits.ToArray());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for(var i = 0; i < 9; i++)
            {
                for(var j = 0; j < 9; j++)
                {
                    if (this[i, j] > 0)
                        sb.Append(this[i, j]);
                    else
                        sb.Append(" ");
                }
                if (i != 8)
                    sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Game)
                return false;
            var g = (Game)obj;
            for(var i = 0; i < 9; i++)
            {
                for(var j = 0; j < 9; j++)
                {
                    if (this[i, j] != g[i, j])
                        return false;
                }
            }
            return true;
        }

        public bool Contains(Game game)
        {
            for(var i = 0; i < 9; i++)
            {
                for(var j = 0; j < 9; j++)
                {
                    if (game[i, j] != 0 && this[i, j] != game[i, j])
                        return false;
                }
            }
            return true;
        }
    }
}
