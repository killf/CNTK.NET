namespace CNTK.Demo
{
    partial class CoordinatesDemo
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.coordinates1 = new CNTK.Controls.Coordinates();
            this.SuspendLayout();
            // 
            // coordinates1
            // 
            this.coordinates1.AxisColor = System.Drawing.Color.DimGray;
            this.coordinates1.AxisTextColor = System.Drawing.Color.DimGray;
            this.coordinates1.AxisTextFont = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.coordinates1.BackColor = System.Drawing.Color.White;
            this.coordinates1.DistanceX = 4;
            this.coordinates1.DistanceY = 1;
            this.coordinates1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coordinates1.Location = new System.Drawing.Point(0, 0);
            this.coordinates1.MaxX = 60;
            this.coordinates1.MaxY = 1;
            this.coordinates1.MinX = 0;
            this.coordinates1.MinY = 0;
            this.coordinates1.Name = "coordinates1";
            this.coordinates1.Padding = new System.Windows.Forms.Padding(10);
            this.coordinates1.Size = new System.Drawing.Size(496, 400);
            this.coordinates1.TabIndex = 0;
            this.coordinates1.Text = "coordinates1";
            // 
            // CoordinatesDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 400);
            this.Controls.Add(this.coordinates1);
            this.Name = "CoordinatesDemo";
            this.Text = "Coordinates Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Coordinates coordinates1;
    }
}