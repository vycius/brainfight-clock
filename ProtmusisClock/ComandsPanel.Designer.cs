namespace ProtmusisClock
{
    partial class ComandsPanel
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
            this.cPoints = new System.Windows.Forms.Label();
            this.cName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cPoints
            // 
            this.cPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.cPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.cPoints.Location = new System.Drawing.Point(200, 3);
            this.cPoints.Name = "cPoints";
            this.cPoints.Size = new System.Drawing.Size(36, 36);
            this.cPoints.TabIndex = 0;
            this.cPoints.Text = "0";
            this.cPoints.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cName
            // 
            this.cName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.cName.Location = new System.Drawing.Point(3, 3);
            this.cName.Name = "cName";
            this.cName.AutoSize = false;
            this.cName.Size = new System.Drawing.Size(184, 33);
            this.cName.TabIndex = 1;
            this.cName.Text = "Komanda ";
            // 
            // ComandsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cName);
            this.Controls.Add(this.cPoints);
            this.Name = "ComandsPanel";
            this.Size = new System.Drawing.Size(251, 38);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label cPoints;
        private System.Windows.Forms.TextBox cName;
    }
}
