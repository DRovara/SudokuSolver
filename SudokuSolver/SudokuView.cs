using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokuView : UserControl
    {
        public Game Game { get; set; }
        public Solver Solver { get; set; }
        public Font FixedDigitFont { get; set; }
        public Font CandidatesFont { get; set; }
        public (int, int) SelectedCell
        {
            get
            {
                return (_selectedCellY, _selectedCellX);
            }
            set
            {
                (_selectedCellY, _selectedCellX) = value;
                Refresh();
            }
        }

        private int _selectedCellX = -1;
        private int _selectedCellY = -1;
        public SudokuView()
        {
            FixedDigitFont = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
            CandidatesFont = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular);
            InitializeComponent();
            LostFocus += (sender, e) => _selectedCellX = -1;
        }

        private void SudokuView_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var boxWidth = Width / 9;
            var boxHeight = Height / 9;
            g.FillRectangle(Brushes.White, 0, 0, Width, Height);
            g.DrawRectangle(new Pen(Brushes.Black, 8), 0, 0, Width, Height);
            for(var i = 0; i < 8; i++)
            {
                var lineWidth = 1;
                if ((i + 1) % 3 == 0)
                    lineWidth = 2;
                g.DrawLine(new Pen(Brushes.Black, lineWidth), (Width / 9) * (i + 1), 0, (Width / 9) * (i + 1), Height);
                g.DrawLine(new Pen(Brushes.Black, lineWidth), 0, (Height / 9) * (i + 1), Width, (Height / 9) * (i + 1));
            }
            foreach(var (row, column, digit) in Game.GetDigits())
            {
                var text = digit.ToString();
                var w = g.MeasureString(text, FixedDigitFont);
                g.DrawString(text, FixedDigitFont, Brushes.Black, column * boxWidth + boxWidth / 2 - w.Width / 2, row * boxHeight + boxHeight / 2 - w.Height / 2);
            }
            if(_selectedCellX != -1)
                g.DrawRectangle(new Pen(Brushes.LightBlue, 6), boxWidth * _selectedCellX, boxHeight * _selectedCellY, boxWidth, boxHeight);

            if(Solver != null)
            {
                for(var row = 0; row < 9; row++)
                {
                    for(var column = 0; column < 9; column++)
                    {
                        if (Game[row, column] != 0 || Solver[row, column].Count == 9)
                            continue;
                        var cnt = 0;
                        var positions = GetPositions(Solver[row, column], g);
                        for(var digit = 1; digit <= 9; digit++)
                        {
                            if(Solver[row, column].Contains(digit))
                            {
                                g.DrawString(digit.ToString(), CandidatesFont, Brushes.Blue, column * boxWidth + positions[cnt].Item1, row * boxHeight + positions[cnt].Item2);
                                cnt++;
                            }
                                
                        }
                    }
                }
            }
        }

        private (float, float)[] GetPositions(HashSet<int> possibilities, Graphics graphics)
        {
            var cellWidth = Width / 9;
            var cellHeight = Height / 9;
            var textSize = graphics.MeasureString("6", CandidatesFont);
            var textWidth = textSize.Width;
            var textHeight = textSize.Height;
            var list = new List<(float, float)>();
            list.Add((2, 2));
            list.Add((cellWidth - textWidth - 2, 2));
            list.Add((2, cellHeight - textHeight - 2));
            list.Add((cellWidth - textWidth - 2, cellHeight - textHeight - 2));
            if (possibilities.Count <= 4)
                return list.ToArray();
            list.Insert(1, (cellWidth / 2 - textWidth / 2, 2));
            if (possibilities.Count <= 5)
                return list.ToArray();
            list.Insert(4, (cellWidth / 2 - textWidth / 2, cellHeight - textHeight - 2));
            if (possibilities.Count <= 6)
                return list.ToArray();
            list.Insert(3, (2, cellHeight / 2 - textHeight / 2));
            if (possibilities.Count <= 7)
                return list.ToArray();
            list.Insert(3, (cellWidth - textWidth - 2, cellHeight / 2 - textHeight / 2));
            return list.ToArray();

        }

        private void SudokuView_MouseClick(object sender, MouseEventArgs e)
        {
            var x = e.X;
            var y = e.Y;
            var cellWidth = Width / 9;
            var cellHeight = Height / 9;
            var cx = x / cellWidth;
            var cy = y / cellHeight;
            SelectedCell = (cy, cx);
        }

        private void SudokuView_KeyDown(object sender, KeyEventArgs e)
        {
            if (_selectedCellX == -1)
                return;
            if(e.KeyCode == Keys.Back)
            {
                Game[_selectedCellY, _selectedCellX] = 0;
                Refresh();
            }
            var v = e.KeyValue;
            v -= 48;
            if(v >= 1 && v <= 9 && _selectedCellX != -1)
            {
                Game[_selectedCellY, _selectedCellX] = v;
                Refresh();
            }
        }
    }
}
