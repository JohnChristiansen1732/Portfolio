namespace Bookworm
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DealNumber = new System.Windows.Forms.TextBox();
            this.LoanNumber = new System.Windows.Forms.TextBox();
            this.RStatusBar = new System.Windows.Forms.Label();
            this.RCheck = new System.Windows.Forms.CheckBox();
            this.KCheck = new System.Windows.Forms.CheckBox();
            this.KStatusBar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(206, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Find Images";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Bookworm_OnClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Deal Number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Loan Number:";
            // 
            // DealNumber
            // 
            this.DealNumber.Location = new System.Drawing.Point(90, 6);
            this.DealNumber.Name = "DealNumber";
            this.DealNumber.Size = new System.Drawing.Size(100, 20);
            this.DealNumber.TabIndex = 3;
            // 
            // LoanNumber
            // 
            this.LoanNumber.Location = new System.Drawing.Point(90, 32);
            this.LoanNumber.Name = "LoanNumber";
            this.LoanNumber.Size = new System.Drawing.Size(100, 20);
            this.LoanNumber.TabIndex = 4;
            // 
            // RStatusBar
            // 
            this.RStatusBar.AutoSize = true;
            this.RStatusBar.Location = new System.Drawing.Point(287, 9);
            this.RStatusBar.Name = "RStatusBar";
            this.RStatusBar.Size = new System.Drawing.Size(175, 13);
            this.RStatusBar.TabIndex = 5;
            this.RStatusBar.Text = "                                                        ";
            // 
            // RCheck
            // 
            this.RCheck.AutoSize = true;
            this.RCheck.Location = new System.Drawing.Point(219, 9);
            this.RCheck.Name = "RCheck";
            this.RCheck.Size = new System.Drawing.Size(62, 17);
            this.RCheck.TabIndex = 6;
            this.RCheck.Text = "R Drive";
            this.RCheck.UseVisualStyleBackColor = true;
            // 
            // KCheck
            // 
            this.KCheck.AutoSize = true;
            this.KCheck.Location = new System.Drawing.Point(219, 31);
            this.KCheck.Name = "KCheck";
            this.KCheck.Size = new System.Drawing.Size(61, 17);
            this.KCheck.TabIndex = 7;
            this.KCheck.Text = "K Drive";
            this.KCheck.UseVisualStyleBackColor = true;
            // 
            // KStatusBar
            // 
            this.KStatusBar.AutoSize = true;
            this.KStatusBar.Location = new System.Drawing.Point(287, 32);
            this.KStatusBar.Name = "KStatusBar";
            this.KStatusBar.Size = new System.Drawing.Size(172, 13);
            this.KStatusBar.TabIndex = 8;
            this.KStatusBar.Text = "                                                       ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 85);
            this.Controls.Add(this.KStatusBar);
            this.Controls.Add(this.KCheck);
            this.Controls.Add(this.RCheck);
            this.Controls.Add(this.RStatusBar);
            this.Controls.Add(this.LoanNumber);
            this.Controls.Add(this.DealNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Bookworm 2.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DealNumber;
        private System.Windows.Forms.TextBox LoanNumber;
        private System.Windows.Forms.Label RStatusBar;
        private System.Windows.Forms.CheckBox RCheck;
        private System.Windows.Forms.CheckBox KCheck;
        private System.Windows.Forms.Label KStatusBar;
    }
}

