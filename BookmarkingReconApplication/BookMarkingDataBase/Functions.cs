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
using Opus.FileManagement.Common;

namespace BookMarkingDataBase
{
    class Functions
    {
        internal bool ImportNewBatch(formItems form)
        {
            dbResult result = new dbResult();

            if (form.importValidForm() == 1)
            {
                return false;
            }
            try
            {
                using (FileStream fstream = new FileStream(form.importPageCount, FileMode.Open))
                {
                    Workbook workbook = new Workbook(fstream);
                    Worksheet worksheet = workbook.Worksheets[1];
                    DataTable dataTable = worksheet.Cells.ExportDataTable(0, 0, (worksheet.Cells.MaxRow + 1), (worksheet.Cells.MaxColumn + 1), true);

                    result = enterNewIntoDB(form, dataTable);
                    fstream.Close();
                }

            }
            catch (Exception e)
            {
                var logger = LoggerObject.Instance;
                logger.GetLoggerMethods().Error(String.Format("Unhandled error: {0}", e.Message));
                form.statusMessage = "ERROR: " + e.Message;
                return false;
            }

            StringBuilder message = new StringBuilder();
            if (result.errorCount > 0)
            {
                message.Append(result.errorCount + " Errors.  Check log for more info.  ");
            }
            message.Append("Import Complete: ").Append(result.numEntered).Append(" entered");
            form.statusMessage = message.ToString();
            return true;
        }
        internal bool ImportBookmarked(formItems form)
        {
            dbResult result = new dbResult();
            if (form.importValidForm() == 1)
            {
                return false;
            }
            try
            {
                using (FileStream fstream = new FileStream(form.importPageCount, FileMode.Open))
                {
                    Workbook workbook = new Workbook(fstream);
                    Worksheet worksheet = workbook.Worksheets[1];
                    DataTable dataTable = worksheet.Cells.ExportDataTable(0, 0, (worksheet.Cells.MaxRow + 1), (worksheet.Cells.MaxColumn + 1), true);

                    result = enterBookIntoDB(form, dataTable);
                    fstream.Close();
                }
            }
            catch (Exception e)
            {
                var logger = LoggerObject.Instance;
                logger.GetLoggerMethods().Error(String.Format("Unhandled error: {0}", e.Message));
                form.statusMessage = "ERROR: " + e.Message;
                return false;
            }
            StringBuilder message = new StringBuilder();
            if (result.errorCount > 0)
            {
                message.Append(result.errorCount + " Errors.  Check log for more info.  ");
            }
            message.Append("Import Complete: ").Append(result.numEntered).Append(" bookmarks entered");
            form.statusMessage = message.ToString();
            return true;
        }
        internal bool Export(formItems form)
        {
            if(form.exportValidForm() == 1)
            {
                return false;
            }
            if (form.exportAll == true)
            {
                FindAll(form);
                return true;
            }
            else if (form.exportBatch == true)
            {
                FindBatch(form);
                return true;
            }
            else
            {
                ReconBatch(form);
                return true;
            }
        }
        private static dbResult enterNewIntoDB(formItems form, DataTable dataTable)
        {
            dbResult result = new dbResult();
            SqlActivity sqlActivity = new SqlActivity();
            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    ImageInfo newfile = new ImageInfo();
                    newfile.BatchName = form.importBatchName;
                    newfile.LoanNumber = row["Loan_Number"].ToString();
                    newfile.FileName = row["File_Name"].ToString().Replace("_Bookmarked", string.Empty);
                    newfile.OriginalPageCount = Convert.ToInt32(row["Page_Count"]);
                    newfile.DateUpload = DateTime.Now;
                    result.numEntered += sqlActivity.insertFile(newfile);
                }
                catch (Exception e)
                {
                    result.errorCount++;
                    var logger = LoggerObject.Instance;
                    logger.GetLoggerMethods().Error(String.Format("Unhandled error: {0}", e.Message));
                }

            }

            return result;
        }
        private static dbResult enterBookIntoDB(formItems form, DataTable dataTable)
        {
            dbResult result = new dbResult();
            SqlActivity sqlActivity = new SqlActivity();
            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    ImageInfo newfile = new ImageInfo();
                    newfile.BatchName = form.importBatchName;
                    newfile.LoanNumber = row["Loan_Number"].ToString();
                    newfile.FileName = row["File_Name"].ToString().Replace("_Bookmarked", string.Empty);
                    newfile.BookmarkedPageCount = Convert.ToInt32(row["Page_Count"]);
                    newfile.DateDownload = DateTime.Now;
                    result.numEntered += sqlActivity.insertBookmark(newfile);
                }
                catch (Exception e)
                {
                    result.errorCount++;
                    var logger = LoggerObject.Instance;
                    logger.GetLoggerMethods().Error(String.Format("Unhandled error: {0}", e.Message));

                }

            }

            return result;
        }
        private bool FindAll(formItems form)
        {
            try
            {
                SqlActivity sqlActivity = new SqlActivity();
                sqlActivity.findAll(form.foundData);

                if (form.exportExcel == true)
                {
                    Functions functions = new Functions();
                    functions.exportToExcel(form.foundData, form.reportFolder, "All Files");
                }
            }
            catch (Exception e)
            {
                var logger = LoggerObject.Instance;
                logger.GetLoggerMethods().Error(String.Format("Unhandled error: {0}", e.Message));
                form.statusMessage = "ERROR: " + e.Message;
                return false;
            }


            form.statusMessage = "Export Complete";
            return true;
        }
        private bool FindBatch(formItems form)
        {
            try
            {
                SqlActivity sqlActivity = new SqlActivity();
                sqlActivity.findBatch(form.foundData, form.exportBatchName);

                if (form.exportExcel == true)
                {
                    Functions functions = new Functions();
                    functions.exportToExcel(form.foundData, form.reportFolder, form.exportBatchName);
                }
            }
            catch (Exception e)
            {
                var logger = LoggerObject.Instance;
                logger.GetLoggerMethods().Error(String.Format("Unhandled error: {0}", e.Message));
                form.statusMessage = "ERROR: " + e.Message;
                return false;
            }
            form.statusMessage = "Export Complete";
            return true;
        }
        private bool ReconBatch(formItems form)
        {
            ReconResult reconResult;
            try
            {
                SqlActivity sqlActivity = new SqlActivity();
                sqlActivity.findBatch(form.foundData, form.exportBatchName);

                Functions function = new Functions();
                reconResult = function.reconBatch(form.foundData);

                if (form.exportExcel == true)
                {
                    function.ExportReconExcel(reconResult, form.reportFolder, form.exportBatchName);
                }
            }
            catch (Exception e)
            {
                var logger = LoggerObject.Instance;
                logger.GetLoggerMethods().Error(String.Format("Unhandled error: {0}", e.Message));
                form.statusMessage = "ERROR: " + e.Message;
                return false;
            }

            form.statusMessage = string.Format("Export Complete.  Matches: {0}, Failed: {1}, Not Received: {2}", reconResult.match.Count, reconResult.failedRecon.Count, reconResult.notReceived.Count);
            return true;
        }

        internal ReconResult reconBatch(List<ImageInfo> foundData)
        {
            ReconResult reconResult = new ReconResult();
            foreach (ImageInfo item in foundData)
            {
                if (item.BookmarkedPageCount == null)
                {
                    reconResult.notReceived.Add(item);
                }
                else if (item.OriginalPageCount != item.BookmarkedPageCount)
                {
                    reconResult.failedRecon.Add(item);
                }
                else
                {
                    reconResult.match.Add(item);
                }
            }
            return reconResult;
        }
        internal void exportToExcel(List<ImageInfo> foundData, string location, string batch)
        {
            Workbook book = new Workbook();
            book.Worksheets.Clear();
            Worksheet newsheet = book.Worksheets.Add(batch);
            addSheet(newsheet, foundData);
            StringBuilder folderFile = new StringBuilder();
            folderFile.Append(location).Append("\\").Append(batch).Append(".xlsx");
            book.Save(folderFile.ToString());
        }
        private void addSheet(Worksheet sheet, List<ImageInfo> foundData)
        {
            sheet.Cells.ImportCustomObjects(foundData,
                 new string[] { "BatchName", "LoanNumber", "FileName", "OriginalPageCount", "BookmarkedPageCount", "DateUpload", "DateDownload" },
                 true,
                 0,
                 0,
                 foundData.Count,
                 true,
                "mm/dd/yyyy hh:mm AM/PM",
                false);
        }
        internal void ExportReconExcel(ReconResult reconResult, string location, string batch)
        {
            Workbook book = new Workbook();
            book.Worksheets.Clear();
            Worksheet sheet1 = book.Worksheets.Add("Summary");
            addReconSheet(sheet1, reconResult);
            Worksheet sheet2 = book.Worksheets.Add("Success");
            addSheet(sheet2, reconResult.match);
            Worksheet sheet3 = book.Worksheets.Add("Failed");
            addSheet(sheet3, reconResult.failedRecon);
            Worksheet sheet4 = book.Worksheets.Add("Not Received");
            addSheet(sheet4, reconResult.notReceived);
            StringBuilder folderFile = new StringBuilder();
            folderFile.Append(location).Append("\\").Append(batch).Append("_Recon").Append(".xlsx");
            book.Save(folderFile.ToString());
        }
        private void addReconSheet(Worksheet sheet, ReconResult reconResult)
        {
            sheet.Cells["A1"].PutValue("Success Count");
            sheet.Cells["B1"].PutValue("Failed Count");
            sheet.Cells["C1"].PutValue("Not Received Count");
            sheet.Cells["A2"].PutValue(reconResult.match.Count);
            sheet.Cells["B2"].PutValue(reconResult.failedRecon.Count);
            sheet.Cells["C2"].PutValue(reconResult.notReceived.Count);
        }
    }

}