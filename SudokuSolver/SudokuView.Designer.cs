
namespace SudokuSolver
{
    partial class SudokuView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SudokuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "SudokuView";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SudokuView_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SudokuView_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SudokuView_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
