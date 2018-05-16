namespace QueueingLib
{
    partial class JobUCtrl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._jobCaptionLabel = new System.Windows.Forms.Label();
            this._jobProgressBar = new System.Windows.Forms.ProgressBar();
            this._jobLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.Controls.Add(this._jobCaptionLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._jobProgressBar, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this._jobLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(507, 19);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _jobCaptionLabel
            // 
            this._jobCaptionLabel.AutoSize = true;
            this._jobCaptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._jobCaptionLabel.Location = new System.Drawing.Point(206, 0);
            this._jobCaptionLabel.Name = "_jobCaptionLabel";
            this._jobCaptionLabel.Size = new System.Drawing.Size(197, 19);
            this._jobCaptionLabel.TabIndex = 2;
            this._jobCaptionLabel.Text = "jobCaption";
            this._jobCaptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _jobProgressBar
            // 
            this._jobProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._jobProgressBar.Location = new System.Drawing.Point(406, 0);
            this._jobProgressBar.Margin = new System.Windows.Forms.Padding(0);
            this._jobProgressBar.Name = "_jobProgressBar";
            this._jobProgressBar.Size = new System.Drawing.Size(101, 19);
            this._jobProgressBar.TabIndex = 0;
            // 
            // _jobLabel
            // 
            this._jobLabel.AutoSize = true;
            this._jobLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._jobLabel.Location = new System.Drawing.Point(3, 0);
            this._jobLabel.Name = "_jobLabel";
            this._jobLabel.Size = new System.Drawing.Size(197, 19);
            this._jobLabel.TabIndex = 1;
            this._jobLabel.Text = "jobName";
            this._jobLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // JobUCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "JobUCtrl";
            this.Size = new System.Drawing.Size(507, 19);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _jobCaptionLabel;
        private System.Windows.Forms.ProgressBar _jobProgressBar;
        private System.Windows.Forms.Label _jobLabel;
    }
}
