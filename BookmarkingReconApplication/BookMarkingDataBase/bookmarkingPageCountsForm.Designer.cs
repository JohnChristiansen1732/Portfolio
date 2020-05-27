namespace BookMarkingDataBase
{
    partial class bookmarkingPageCountsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(bookmarkingPageCountsForm));
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.StatusBar = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ImportBatchName = new System.Windows.Forms.Button();
            this.importBookmarked = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.importPageCount = new System.Windows.Forms.TextBox();
            this.importBatch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ExportButton = new System.Windows.Forms.Button();
            this.ExportRecon = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBatchName = new System.Windows.Forms.TextBox();
            this.ExportBatch = new System.Windows.Forms.RadioButton();
            this.excelOutput = new System.Windows.Forms.TextBox();
            this.Export = new System.Windows.Forms.CheckBox();
            this.ExportAll = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gridPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(1, 6);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(993, 196);
            this.dataGrid.TabIndex = 3;
            // 
            // StatusBar
            // 
            this.StatusBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusBar.AutoSize = true;
            this.StatusBar.Location = new System.Drawing.Point(3, 148);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(766, 13);
            this.StatusBar.TabIndex = 16;
            this.StatusBar.Text = resources.GetString("StatusBar.Text");
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ImportBatchName);
            this.groupBox2.Controls.Add(this.importBookmarked);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.importPageCount);
            this.groupBox2.Controls.Add(this.importBatch);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(992, 80);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Import";
            // 
            // ImportBatchName
            // 
            this.ImportBatchName.Location = new System.Drawing.Point(870, 18);
            this.ImportBatchName.Name = "ImportBatchName";
            this.ImportBatchName.Size = new System.Drawing.Size(116, 23);
            this.ImportBatchName.TabIndex = 5;
            this.ImportBatchName.Text = "Import Originals";
            this.ImportBatchName.UseVisualStyleBackColor = true;
            this.ImportBatchName.Click += new System.EventHandler(this.importOriginals_onClick);
            // 
            // importBookmarked
            // 
            this.importBookmarked.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.importBookmarked.Location = new System.Drawing.Point(871, 49);
            this.importBookmarked.Name = "importBookmarked";
            this.importBookmarked.Size = new System.Drawing.Size(115, 23);
            this.importBookmarked.TabIndex = 4;
            this.importBookmarked.Text = "Import Bookmarked";
            this.importBookmarked.UseVisualStyleBackColor = true;
            this.importBookmarked.Click += new System.EventHandler(this.importBookmarked_onClick);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Page Count:";
            // 
            // importPageCount
            // 
            this.importPageCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.importPageCount.Location = new System.Drawing.Point(335, 35);
            this.importPageCount.Name = "importPageCount";
            this.importPageCount.Size = new System.Drawing.Size(530, 20);
            this.importPageCount.TabIndex = 2;
            // 
            // importBatch
            // 
            this.importBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.importBatch.Location = new System.Drawing.Point(80, 35);
            this.importBatch.Name = "importBatch";
            this.importBatch.Size = new System.Drawing.Size(181, 20);
            this.importBatch.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Batch Name:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.ExportButton);
            this.groupBox3.Controls.Add(this.ExportRecon);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.textBoxBatchName);
            this.groupBox3.Controls.Add(this.ExportBatch);
            this.groupBox3.Controls.Add(this.excelOutput);
            this.groupBox3.Controls.Add(this.Export);
            this.groupBox3.Controls.Add(this.ExportAll);
            this.groupBox3.Location = new System.Drawing.Point(3, 89);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(992, 54);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Export";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Report Folder:";
            // 
            // ExportButton
            // 
            this.ExportButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ExportButton.Location = new System.Drawing.Point(911, 21);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(75, 23);
            this.ExportButton.TabIndex = 14;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportData_OnClick);
            // 
            // ExportRecon
            // 
            this.ExportRecon.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ExportRecon.AutoSize = true;
            this.ExportRecon.Location = new System.Drawing.Point(848, 24);
            this.ExportRecon.Name = "ExportRecon";
            this.ExportRecon.Size = new System.Drawing.Size(57, 17);
            this.ExportRecon.TabIndex = 12;
            this.ExportRecon.TabStop = true;
            this.ExportRecon.Text = "Recon";
            this.ExportRecon.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(552, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Batch Name:";
            // 
            // textBoxBatchName
            // 
            this.textBoxBatchName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxBatchName.Location = new System.Drawing.Point(627, 23);
            this.textBoxBatchName.Name = "textBoxBatchName";
            this.textBoxBatchName.Size = new System.Drawing.Size(114, 20);
            this.textBoxBatchName.TabIndex = 2;
            // 
            // ExportBatch
            // 
            this.ExportBatch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ExportBatch.AutoSize = true;
            this.ExportBatch.Location = new System.Drawing.Point(789, 24);
            this.ExportBatch.Name = "ExportBatch";
            this.ExportBatch.Size = new System.Drawing.Size(53, 17);
            this.ExportBatch.TabIndex = 11;
            this.ExportBatch.TabStop = true;
            this.ExportBatch.Text = "Batch";
            this.ExportBatch.UseVisualStyleBackColor = true;
            // 
            // excelOutput
            // 
            this.excelOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.excelOutput.Location = new System.Drawing.Point(196, 23);
            this.excelOutput.Name = "excelOutput";
            this.excelOutput.Size = new System.Drawing.Size(350, 20);
            this.excelOutput.TabIndex = 9;
            // 
            // Export
            // 
            this.Export.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Export.AutoSize = true;
            this.Export.Location = new System.Drawing.Point(9, 25);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(101, 17);
            this.Export.TabIndex = 7;
            this.Export.Text = "Export To Excel";
            this.Export.UseVisualStyleBackColor = true;
            // 
            // ExportAll
            // 
            this.ExportAll.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ExportAll.AutoSize = true;
            this.ExportAll.Location = new System.Drawing.Point(747, 24);
            this.ExportAll.Name = "ExportAll";
            this.ExportAll.Size = new System.Drawing.Size(36, 17);
            this.ExportAll.TabIndex = 10;
            this.ExportAll.TabStop = true;
            this.ExportAll.Text = "All";
            this.ExportAll.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.StatusBar);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(11, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(999, 167);
            this.panel1.TabIndex = 19;
            // 
            // gridPanel
            // 
            this.gridPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPanel.BackColor = System.Drawing.SystemColors.Control;
            this.gridPanel.Controls.Add(this.dataGrid);
            this.gridPanel.Location = new System.Drawing.Point(11, 6);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Size = new System.Drawing.Size(1000, 205);
            this.gridPanel.TabIndex = 20;
            // 
            // bookmarkingPageCountsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 387);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.panel1);
            this.Name = "bookmarkingPageCountsForm";
            this.Text = "Bookmarking PageCounts";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gridPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label StatusBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button importBookmarked;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox importPageCount;
        private System.Windows.Forms.TextBox importBatch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.RadioButton ExportRecon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxBatchName;
        private System.Windows.Forms.RadioButton ExportBatch;
        private System.Windows.Forms.TextBox excelOutput;
        private System.Windows.Forms.CheckBox Export;
        private System.Windows.Forms.RadioButton ExportAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel gridPanel;
        private System.Windows.Forms.Button ImportBatchName;
    }
}

