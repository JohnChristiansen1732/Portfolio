using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BookMarkingDataBase
{
    class ImageInfo
    {
        public string BatchName { get; set; }
        public string LoanNumber { get; set; }
        public string FileName { get; set; }
        public int OriginalPageCount { get; set; }
        public int? BookmarkedPageCount { get; set; }
        public DateTime? DateUpload { get; set; }
        public DateTime? DateDownload { get; set; }
    }
    class ReconResult
    {
        public ReconResult()
        {
            notReceived = new List<ImageInfo>();
            failedRecon = new List<ImageInfo>();
            match = new List<ImageInfo>();
        }
        public List<ImageInfo> match { get; set; }
        public List<ImageInfo> failedRecon { get; set; }
        public List<ImageInfo> notReceived { get; set; }

    }
    class formItems
    {
        public formItems(string ibatch, string ipageCount, string ebatch, string folder, bool excel, bool all, bool batch, bool recon)
        {
            importBatchName = ibatch;
            importPageCount = ipageCount;
            exportBatchName = ebatch;
            reportFolder = folder;
            exportExcel = excel;
            exportAll = all;
            exportBatch = batch;
            exportRecon = recon;
            foundData = new List<ImageInfo>();
            statusMessage = string.Empty;
        }
        public string importBatchName { get; set; }
        public string importPageCount { get; set; }
        public string exportBatchName { get; set; }
        public string reportFolder { get; set; }
        public bool exportExcel { get; set; }
        public bool exportAll { get; set; }
        public bool exportBatch { get; set; }
        public bool exportRecon { get; set; }
        public List<ImageInfo> foundData { get; set; }
        public string statusMessage { get; set; }

        public int importValidForm()
        {
            if (!File.Exists(importPageCount))
            {
                statusMessage = "ERROR: Check Page Count Location";
                return 1;
            }
            if (importBatchName == string.Empty)
            {
                statusMessage = "ERROR: Please Enter a Batch Name";
                return 1;
            }
            return 0;
        }
        public int exportValidForm()
        {
            if (exportAll == false && exportBatch == false && exportRecon == false)
            {
                statusMessage = "ERROR: Check export all, batch, or recon";
                return 1;
            }
            if (exportExcel == true && (!Directory.Exists(reportFolder)))
            {

                statusMessage = "ERROR: Check report folder";
                return 1;
            }
            return 0;
        }
    }
    class dbResult
    {
        public int errorCount;
        public int numEntered;
    }
}
