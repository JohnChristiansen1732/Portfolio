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
    class Functions
    {
        internal string rbookworm(string deal, string loanNumber)
        {
            deal = deal.Replace("-", string.Empty);
            int folderCount = 0;
            if (deal.Length < 7 || loanNumber == "")
            {
                return "Please enter valid deal and loan number";
            }

            StringBuilder path = new StringBuilder();
            path.Append(@"\\****\corp\Images\").Append(deal);
            if(!Directory.Exists(path.ToString()))
            {
                return "Please check deal number";
            }
            string[] subfolders = Directory.GetDirectories(path.ToString());
            foreach (string sub in subfolders)
            {
                StringBuilder fullpath = new StringBuilder();
                fullpath.Append(sub).Append("\\").Append(loanNumber);
                if(Directory.Exists(fullpath.ToString()))
                {
                    Process.Start(fullpath.ToString());
                    folderCount++;
                }
            }
            return folderCount + " R drive folder(s) found";
        }
        internal string kbookworm(string deal, string loanNumber)
        {
            deal = deal.Replace("-", string.Empty);
            if (deal.Length < 7 || loanNumber == "")
            {
                return "Please enter valid deal and loan number";
            }
            string job = deal.Remove(4, deal.Length-4);
            int folderCount = 0;

            StringBuilder path = new StringBuilder();
            path.Append(@"\\****\corp\SupportingDocs\Servdata\").Append(job).Append("\\").Append(deal);
            if (!Directory.Exists(path.ToString()))
            {
                return "Please check deal number";
            }
            path.Append("\\").Append(loanNumber);
            if (Directory.Exists(path.ToString()))
            {
                Process.Start(path.ToString());
                folderCount++;
            }
            return folderCount + " K drive folder(s) found";
        }
    }
}
