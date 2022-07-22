
namespace SudokuSolver
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SudokuSolver.Game game2 = new SudokuSolver.Game();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.GameView = new SudokuSolver.SudokuView();
            this.SolveButton = new System.Windows.Forms.Button();
            this.cbHiddenSingles = new System.Windows.Forms.CheckBox();
            this.cbNakedPairs = new System.Windows.Forms.CheckBox();
            this.cbNakedTriples = new System.Windows.Forms.CheckBox();
            this.cbNakedQuadruples = new System.Windows.Forms.CheckBox();
            this.cbHiddenQuadruples = new System.Windows.Forms.CheckBox();
            this.cbHiddenTriples = new System.Windows.Forms.CheckBox();
            this.cbHiddenPairs = new System.Windows.Forms.CheckBox();
            this.cbPointingPairs = new System.Windows.Forms.CheckBox();
            this.cbBoxReduction = new System.Windows.Forms.CheckBox();
            this.cbXWings = new System.Windows.Forms.CheckBox();
            this.cbYWings = new System.Windows.Forms.CheckBox();
            this.cbSwordfish = new System.Windows.Forms.CheckBox();
            this.cbSimpleColouring = new System.Windows.Forms.CheckBox();
            this.cbXYZWing = new System.Windows.Forms.CheckBox();
            this.importButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GameView
            // 
            this.GameView.CandidatesFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GameView.FixedDigitFont = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GameView.Game = game2;
            this.GameView.Location = new System.Drawing.Point(12, 12);
            this.GameView.Name = "GameView";
            this.GameView.SelectedCell = ((System.ValueTuple<int, int>)(resources.GetObject("GameView.SelectedCell")));
            this.GameView.Size = new System.Drawing.Size(500, 500);
            this.GameView.Solver = null;
            this.GameView.TabIndex = 0;
            // 
            // SolveButton
            // 
            this.SolveButton.Location = new System.Drawing.Point(518, 484);
            this.SolveButton.Name = "SolveButton";
            this.SolveButton.Size = new System.Drawing.Size(270, 27);
            this.SolveButton.TabIndex = 1;
            this.SolveButton.Text = "Solve";
            this.SolveButton.UseVisualStyleBackColor = true;
            this.SolveButton.Click += new System.EventHandler(this.SolveButton_Click);
            // 
            // cbHiddenSingles
            // 
            this.cbHiddenSingles.AutoSize = true;
            this.cbHiddenSingles.Checked = true;
            this.cbHiddenSingles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHiddenSingles.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbHiddenSingles.Location = new System.Drawing.Point(518, 21);
            this.cbHiddenSingles.Name = "cbHiddenSingles";
            this.cbHiddenSingles.Size = new System.Drawing.Size(144, 25);
            this.cbHiddenSingles.TabIndex = 2;
            this.cbHiddenSingles.Text = "Hidden Singles";
            this.cbHiddenSingles.UseVisualStyleBackColor = true;
            // 
            // cbNakedPairs
            // 
            this.cbNakedPairs.AutoSize = true;
            this.cbNakedPairs.Checked = true;
            this.cbNakedPairs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNakedPairs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbNakedPairs.Location = new System.Drawing.Point(518, 52);
            this.cbNakedPairs.Name = "cbNakedPairs";
            this.cbNakedPairs.Size = new System.Drawing.Size(120, 25);
            this.cbNakedPairs.TabIndex = 3;
            this.cbNakedPairs.Text = "Naked Pairs";
            this.cbNakedPairs.UseVisualStyleBackColor = true;
            this.cbNakedPairs.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // cbNakedTriples
            // 
            this.cbNakedTriples.AutoSize = true;
            this.cbNakedTriples.Checked = true;
            this.cbNakedTriples.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNakedTriples.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbNakedTriples.Location = new System.Drawing.Point(518, 83);
            this.cbNakedTriples.Name = "cbNakedTriples";
            this.cbNakedTriples.Size = new System.Drawing.Size(133, 25);
            this.cbNakedTriples.TabIndex = 4;
            this.cbNakedTriples.Text = "Naked Triples";
            this.cbNakedTriples.UseVisualStyleBackColor = true;
            // 
            // cbNakedQuadruples
            // 
            this.cbNakedQuadruples.AutoSize = true;
            this.cbNakedQuadruples.Checked = true;
            this.cbNakedQuadruples.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNakedQuadruples.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbNakedQuadruples.Location = new System.Drawing.Point(518, 114);
            this.cbNakedQuadruples.Name = "cbNakedQuadruples";
            this.cbNakedQuadruples.Size = new System.Drawing.Size(171, 25);
            this.cbNakedQuadruples.TabIndex = 5;
            this.cbNakedQuadruples.Text = "Naked Quadruples";
            this.cbNakedQuadruples.UseVisualStyleBackColor = true;
            // 
            // cbHiddenQuadruples
            // 
            this.cbHiddenQuadruples.AutoSize = true;
            this.cbHiddenQuadruples.Checked = true;
            this.cbHiddenQuadruples.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHiddenQuadruples.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbHiddenQuadruples.Location = new System.Drawing.Point(518, 207);
            this.cbHiddenQuadruples.Name = "cbHiddenQuadruples";
            this.cbHiddenQuadruples.Size = new System.Drawing.Size(177, 25);
            this.cbHiddenQuadruples.TabIndex = 8;
            this.cbHiddenQuadruples.Text = "Hidden Quadruples";
            this.cbHiddenQuadruples.UseVisualStyleBackColor = true;
            // 
            // cbHiddenTriples
            // 
            this.cbHiddenTriples.AutoSize = true;
            this.cbHiddenTriples.Checked = true;
            this.cbHiddenTriples.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHiddenTriples.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbHiddenTriples.Location = new System.Drawing.Point(518, 176);
            this.cbHiddenTriples.Name = "cbHiddenTriples";
            this.cbHiddenTriples.Size = new System.Drawing.Size(139, 25);
            this.cbHiddenTriples.TabIndex = 7;
            this.cbHiddenTriples.Text = "Hidden Triples";
            this.cbHiddenTriples.UseVisualStyleBackColor = true;
            // 
            // cbHiddenPairs
            // 
            this.cbHiddenPairs.AutoSize = true;
            this.cbHiddenPairs.Checked = true;
            this.cbHiddenPairs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHiddenPairs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbHiddenPairs.Location = new System.Drawing.Point(518, 145);
            this.cbHiddenPairs.Name = "cbHiddenPairs";
            this.cbHiddenPairs.Size = new System.Drawing.Size(126, 25);
            this.cbHiddenPairs.TabIndex = 6;
            this.cbHiddenPairs.Text = "Hidden Pairs";
            this.cbHiddenPairs.UseVisualStyleBackColor = true;
            // 
            // cbPointingPairs
            // 
            this.cbPointingPairs.AutoSize = true;
            this.cbPointingPairs.Checked = true;
            this.cbPointingPairs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPointingPairs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbPointingPairs.Location = new System.Drawing.Point(518, 238);
            this.cbPointingPairs.Name = "cbPointingPairs";
            this.cbPointingPairs.Size = new System.Drawing.Size(136, 25);
            this.cbPointingPairs.TabIndex = 9;
            this.cbPointingPairs.Text = "Pointing Pairs";
            this.cbPointingPairs.UseVisualStyleBackColor = true;
            // 
            // cbBoxReduction
            // 
            this.cbBoxReduction.AutoSize = true;
            this.cbBoxReduction.Checked = true;
            this.cbBoxReduction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBoxReduction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbBoxReduction.Location = new System.Drawing.Point(518, 269);
            this.cbBoxReduction.Name = "cbBoxReduction";
            this.cbBoxReduction.Size = new System.Drawing.Size(140, 25);
            this.cbBoxReduction.TabIndex = 10;
            this.cbBoxReduction.Text = "Box Reduction";
            this.cbBoxReduction.UseVisualStyleBackColor = true;
            // 
            // cbXWings
            // 
            this.cbXWings.AutoSize = true;
            this.cbXWings.Checked = true;
            this.cbXWings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbXWings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbXWings.Location = new System.Drawing.Point(518, 300);
            this.cbXWings.Name = "cbXWings";
            this.cbXWings.Size = new System.Drawing.Size(93, 25);
            this.cbXWings.TabIndex = 12;
            this.cbXWings.Text = "X-Wings";
            this.cbXWings.UseVisualStyleBackColor = true;
            // 
            // cbYWings
            // 
            this.cbYWings.AutoSize = true;
            this.cbYWings.Checked = true;
            this.cbYWings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbYWings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbYWings.Location = new System.Drawing.Point(518, 331);
            this.cbYWings.Name = "cbYWings";
            this.cbYWings.Size = new System.Drawing.Size(93, 25);
            this.cbYWings.TabIndex = 13;
            this.cbYWings.Text = "Y-Wings";
            this.cbYWings.UseVisualStyleBackColor = true;
            // 
            // cbSwordfish
            // 
            this.cbSwordfish.AutoSize = true;
            this.cbSwordfish.Checked = true;
            this.cbSwordfish.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSwordfish.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbSwordfish.Location = new System.Drawing.Point(518, 391);
            this.cbSwordfish.Name = "cbSwordfish";
            this.cbSwordfish.Size = new System.Drawing.Size(105, 25);
            this.cbSwordfish.TabIndex = 14;
            this.cbSwordfish.Text = "Swordfish";
            this.cbSwordfish.UseVisualStyleBackColor = true;
            // 
            // cbSimpleColouring
            // 
            this.cbSimpleColouring.AutoSize = true;
            this.cbSimpleColouring.Checked = true;
            this.cbSimpleColouring.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSimpleColouring.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbSimpleColouring.Location = new System.Drawing.Point(518, 422);
            this.cbSimpleColouring.Name = "cbSimpleColouring";
            this.cbSimpleColouring.Size = new System.Drawing.Size(162, 25);
            this.cbSimpleColouring.TabIndex = 15;
            this.cbSimpleColouring.Text = "Simple Colouring";
            this.cbSimpleColouring.UseVisualStyleBackColor = true;
            // 
            // cbXYZWing
            // 
            this.cbXYZWing.AutoSize = true;
            this.cbXYZWing.Checked = true;
            this.cbXYZWing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbXYZWing.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbXYZWing.Location = new System.Drawing.Point(518, 360);
            this.cbXYZWing.Name = "cbXYZWing";
            this.cbXYZWing.Size = new System.Drawing.Size(106, 25);
            this.cbXYZWing.TabIndex = 16;
            this.cbXYZWing.Text = "XYZ-Wing";
            this.cbXYZWing.UseVisualStyleBackColor = true;
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(518, 451);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(270, 27);
            this.importButton.TabIndex = 17;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 522);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.cbXYZWing);
            this.Controls.Add(this.cbSimpleColouring);
            this.Controls.Add(this.cbSwordfish);
            this.Controls.Add(this.cbYWings);
            this.Controls.Add(this.cbXWings);
            this.Controls.Add(this.cbBoxReduction);
            this.Controls.Add(this.cbPointingPairs);
            this.Controls.Add(this.cbHiddenQuadruples);
            this.Controls.Add(this.cbHiddenTriples);
            this.Controls.Add(this.cbHiddenPairs);
            this.Controls.Add(this.cbNakedQuadruples);
            this.Controls.Add(this.cbNakedTriples);
            this.Controls.Add(this.cbNakedPairs);
            this.Controls.Add(this.cbHiddenSingles);
            this.Controls.Add(this.SolveButton);
            this.Controls.Add(this.GameView);
            this.Name = "Form1";
            this.Text = "Sudoku Solver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SudokuView GameView;
        private System.Windows.Forms.Button SolveButton;
        private System.Windows.Forms.CheckBox cbHiddenSingles;
        private System.Windows.Forms.CheckBox cbNakedPairs;
        private System.Windows.Forms.CheckBox cbNakedTriples;
        private System.Windows.Forms.CheckBox cbNakedQuadruples;
        private System.Windows.Forms.CheckBox cbHiddenQuadruples;
        private System.Windows.Forms.CheckBox cbHiddenTriples;
        private System.Windows.Forms.CheckBox cbHiddenPairs;
        private System.Windows.Forms.CheckBox cbPointingPairs;
        private System.Windows.Forms.CheckBox cbBoxReduction;
        private System.Windows.Forms.CheckBox cbXWings;
        private System.Windows.Forms.CheckBox cbYWings;
        private System.Windows.Forms.CheckBox cbSwordfish;
        private System.Windows.Forms.CheckBox cbSimpleColouring;
        private System.Windows.Forms.CheckBox cbXYZWing;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.CheckBox H;
        private System.Windows.Forms.CheckBox Y;
    }
}

