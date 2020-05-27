using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using Aspose.Cells;
using System.IO;


namespace BookMarkingDataBase
{
    public partial class bookmarkingPageCountsForm : Form
    {
        BindingList<ImageInfo> _foundData = new BindingList<ImageInfo>();
        BindingList<ReconResult> _foundRecon = new BindingList<ReconResult>();

        public bookmarkingPageCountsForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void importOriginals_onClick(object sender, EventArgs e)
        {
            StatusBar.Text = "Import in progress...";
            StatusBar.Refresh();
            formItems form = new formItems(importBatch.Text, importPageCount.Text, textBoxBatchName.Text, excelOutput.Text, Export.Checked, ExportAll.Checked, ExportBatch.Checked, ExportRecon.Checked);
            Functions functions = new Functions();
            functions.ImportNewBatch(form);
            StatusBar.Text = form.statusMessage;
            StatusBar.Refresh();
        }
        private void importBookmarked_onClick(object sender, EventArgs e)
        {
            StatusBar.Text = "Import in progress...";
            StatusBar.Refresh();
            formItems form = new formItems(importBatch.Text, importPageCount.Text, textBoxBatchName.Text, excelOutput.Text, Export.Checked, ExportAll.Checked, ExportBatch.Checked, ExportRecon.Checked);
            Functions functions = new Functions(); 
            functions.ImportBookmarked(form);
            StatusBar.Text = form.statusMessage;
            StatusBar.Refresh();
        }
        private void ExportData_OnClick(object sender, EventArgs e)
        {
            StatusBar.Text = "Export in progress...";
            StatusBar.Refresh();
            formItems form = new formItems(ImportBatchName.Text, importPageCount.Text, textBoxBatchName.Text, excelOutput.Text, Export.Checked, ExportAll.Checked, ExportBatch.Checked, ExportRecon.Checked);
            Functions functions = new Functions();
            if (functions.Export(form) == true)
            {
                fillDataGrid(form);
            }
            StatusBar.Text = form.statusMessage;
        }
        private void fillDataGrid(formItems form)
        {

            dataGrid.DataSource = null;
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
            _foundData.Clear();
            foreach (ImageInfo item in form.foundData)
            {
                _foundData.Add(item);
            }
            dataGrid.DataSource = _foundData;
        }
    }
}
