namespace MotionStrategyLab.UI
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
            btnLine = new Button();
            btnArc = new Button();
            btnAbort = new Button();
            PB_Canvas = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PB_Canvas).BeginInit();
            SuspendLayout();
            // 
            // btnLine
            // 
            btnLine.Location = new Point(12, 24);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(146, 58);
            btnLine.TabIndex = 0;
            btnLine.Text = "直线";
            btnLine.UseVisualStyleBackColor = true;
            // 
            // btnArc
            // 
            btnArc.Location = new Point(12, 88);
            btnArc.Name = "btnArc";
            btnArc.Size = new Size(146, 58);
            btnArc.TabIndex = 1;
            btnArc.Text = "弧线";
            btnArc.UseVisualStyleBackColor = true;
            // 
            // btnAbort
            // 
            btnAbort.Location = new Point(12, 152);
            btnAbort.Name = "btnAbort";
            btnAbort.Size = new Size(146, 58);
            btnAbort.TabIndex = 2;
            btnAbort.Text = "停止";
            btnAbort.UseVisualStyleBackColor = true;
            // 
            // PB_Canvas
            // 
            PB_Canvas.Location = new Point(164, 24);
            PB_Canvas.Name = "PB_Canvas";
            PB_Canvas.Size = new Size(761, 680);
            PB_Canvas.TabIndex = 3;
            PB_Canvas.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(937, 716);
            Controls.Add(PB_Canvas);
            Controls.Add(btnAbort);
            Controls.Add(btnArc);
            Controls.Add(btnLine);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)PB_Canvas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnLine;
        private Button btnArc;
        private Button btnAbort;
        private PictureBox PB_Canvas;
    }
}
