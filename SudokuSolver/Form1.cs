using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            try
            {
                GameView.Game = Game.Parse(System.IO.File.ReadAllText("field.txt"));
            }
            catch (Exception)
            {
                GameView.Game = new Game();
            }
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            var solver = new Solver(GameView.Game);

            solver.DoBoxReduction = cbBoxReduction.Checked;
            solver.DoHiddenPairs = cbHiddenPairs.Checked;
            solver.DoHiddenQuadruples = cbHiddenQuadruples.Checked;
            solver.DoHiddenSingles = cbHiddenSingles.Checked;
            solver.DoHiddenTriples = cbHiddenTriples.Checked;
            solver.DoNakedPairs = cbNakedPairs.Checked;
            solver.DoNakedQuadruples = cbNakedQuadruples.Checked;
            solver.DoNakedTriples = cbNakedTriples.Checked;
            solver.DoPointingPairs = cbPointingPairs.Checked;
            solver.DoSimpleColouring = cbSimpleColouring.Checked;
            solver.DoSwordfish = cbSwordfish.Checked;
            solver.DoXWings = cbXWings.Checked;
            solver.DoXYZWings = cbXYZWing.Checked;
            solver.DoYWings = cbYWings.Checked;

            this.Cursor = Cursors.WaitCursor;
            var g = solver.Solve();
            this.Cursor = Cursors.Default;
            if (g != null)
            {
                GameView.Game = g;
                GameView.Solver = solver;
            }
            else
                MessageBox.Show("Sudoku does not have a solution!");
            GameView.Refresh();
            if (g != null)
                Debug.WriteLine(g.ToString());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("" + Tests.TestAll());
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void importButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                GameView.Game = Game.Parse(System.IO.File.ReadAllText(dialog.FileName));
                Refresh();
            }
        }
    }
}
