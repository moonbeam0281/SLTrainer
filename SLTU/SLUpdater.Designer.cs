
namespace SLTU
{
    partial class SLUpdater
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Logger = new System.Windows.Forms.TextBox();
            this.SLULabel = new System.Windows.Forms.Label();
            this.PleaseWaitLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 98);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(460, 33);
            this.progressBar1.TabIndex = 0;
            // 
            // Logger
            // 
            this.Logger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Logger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Logger.Enabled = false;
            this.Logger.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Logger.Location = new System.Drawing.Point(12, 137);
            this.Logger.Multiline = true;
            this.Logger.Name = "Logger";
            this.Logger.ReadOnly = true;
            this.Logger.Size = new System.Drawing.Size(460, 132);
            this.Logger.TabIndex = 1;
            // 
            // SLULabel
            // 
            this.SLULabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SLULabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SLULabel.Location = new System.Drawing.Point(12, 9);
            this.SLULabel.Name = "SLULabel";
            this.SLULabel.Size = new System.Drawing.Size(460, 31);
            this.SLULabel.TabIndex = 2;
            this.SLULabel.Text = "SL Updater";
            this.SLULabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PleaseWaitLabel
            // 
            this.PleaseWaitLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PleaseWaitLabel.Location = new System.Drawing.Point(12, 40);
            this.PleaseWaitLabel.Name = "PleaseWaitLabel";
            this.PleaseWaitLabel.Size = new System.Drawing.Size(460, 23);
            this.PleaseWaitLabel.TabIndex = 3;
            this.PleaseWaitLabel.Text = "Please Wait..";
            this.PleaseWaitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SLUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(484, 281);
            this.Controls.Add(this.PleaseWaitLabel);
            this.Controls.Add(this.SLULabel);
            this.Controls.Add(this.Logger);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(500, 320);
            this.Name = "SLUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SL Updater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox Logger;
        private System.Windows.Forms.Label SLULabel;
        private System.Windows.Forms.Label PleaseWaitLabel;
    }
}

