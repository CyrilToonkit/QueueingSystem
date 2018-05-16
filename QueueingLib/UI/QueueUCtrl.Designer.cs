namespace QueueingLib
{
    partial class QueueUCtrl
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
            this._queueStatusStrip = new System.Windows.Forms.StatusStrip();
            this._queueProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this._queueCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._queueStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CancelBT = new System.Windows.Forms.ToolStripStatusLabel();
            this.PauseBT = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.captionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._queueStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // _queueStatusStrip
            // 
            this._queueStatusStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this._queueStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._queueProgressBar,
            this._queueCountLabel,
            this._queueStatusLabel,
            this.CancelBT,
            this.PauseBT});
            this._queueStatusStrip.Location = new System.Drawing.Point(0, 0);
            this._queueStatusStrip.Name = "_queueStatusStrip";
            this._queueStatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._queueStatusStrip.Size = new System.Drawing.Size(603, 22);
            this._queueStatusStrip.SizingGrip = false;
            this._queueStatusStrip.TabIndex = 0;
            this._queueStatusStrip.Text = "statusStrip1";
            // 
            // _queueProgressBar
            // 
            this._queueProgressBar.Name = "_queueProgressBar";
            this._queueProgressBar.Size = new System.Drawing.Size(100, 16);
            this._queueProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // _queueCountLabel
            // 
            this._queueCountLabel.Name = "_queueCountLabel";
            this._queueCountLabel.Size = new System.Drawing.Size(411, 17);
            this._queueCountLabel.Spring = true;
            this._queueCountLabel.Text = "0 / 0 Jobs";
            // 
            // _queueStatusLabel
            // 
            this._queueStatusLabel.Name = "_queueStatusLabel";
            this._queueStatusLabel.Size = new System.Drawing.Size(43, 17);
            this._queueStatusLabel.Text = "Waiting";
            // 
            // CancelBT
            // 
            this.CancelBT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CancelBT.Enabled = false;
            this.CancelBT.Image = global::QueueingLib.Properties.Resources.CancelQueue;
            this.CancelBT.Name = "CancelBT";
            this.CancelBT.Size = new System.Drawing.Size(16, 17);
            this.CancelBT.Text = "CancelBT";
            this.CancelBT.Click += new System.EventHandler(this.CancelBT_Click);
            // 
            // PauseBT
            // 
            this.PauseBT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PauseBT.Enabled = false;
            this.PauseBT.Image = global::QueueingLib.Properties.Resources.PauseQueue;
            this.PauseBT.Name = "PauseBT";
            this.PauseBT.Size = new System.Drawing.Size(16, 17);
            this.PauseBT.Text = "PauseBT";
            this.PauseBT.Click += new System.EventHandler(this.PauseBT_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameColumn,
            this.captionColumn,
            this.statusColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 22);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.Size = new System.Drawing.Size(603, 196);
            this.dataGridView1.TabIndex = 2;
            // 
            // nameColumn
            // 
            this.nameColumn.DataPropertyName = "Name";
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            // 
            // captionColumn
            // 
            this.captionColumn.DataPropertyName = "Caption";
            this.captionColumn.HeaderText = "Caption";
            this.captionColumn.Name = "captionColumn";
            this.captionColumn.ReadOnly = true;
            // 
            // statusColumn
            // 
            this.statusColumn.DataPropertyName = "Status";
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            // 
            // QueueUCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this._queueStatusStrip);
            this.Name = "QueueUCtrl";
            this.Size = new System.Drawing.Size(603, 218);
            this._queueStatusStrip.ResumeLayout(false);
            this._queueStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip _queueStatusStrip;
        private System.Windows.Forms.ToolStripProgressBar _queueProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel _queueCountLabel;
        private System.Windows.Forms.ToolStripStatusLabel _queueStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel PauseBT;
        private System.Windows.Forms.ToolStripStatusLabel CancelBT;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn captionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
    }
}
