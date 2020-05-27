using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Opus.FileManagement;
using System.Diagnostics;

namespace Bookworm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Bookworm_OnClick(object sender, EventArgs e)
        {
            Functions function = new Functions();
            clearStatusBars();

            if (RCheck.Checked == true)
            {
                RStatusBar.Text = "Finding Images...";
                RStatusBar.Refresh();                                                   
                RStatusBar.Text = function.rbookworm(DealNumber.Text, LoanNumber.Text);
                RStatusBar.Refresh();
            }
            if (KCheck.Checked == true)
            {
                KStatusBar.Text = "Finding Images...";
                KStatusBar.Refresh();
                KStatusBar.Text = function.kbookworm(DealNumber.Text, LoanNumber.Text);
                KStatusBar.Refresh();
            }
            if (RCheck.Checked == false && KCheck.Checked == false)
            {
                RStatusBar.Text = "Please select R and/or K drive";
            }
        }
        private void clearStatusBars()
        {
            RStatusBar.Text = "";
            RStatusBar.Refresh();
            KStatusBar.Text = "";
            KStatusBar.Refresh();
        }
    }
}
