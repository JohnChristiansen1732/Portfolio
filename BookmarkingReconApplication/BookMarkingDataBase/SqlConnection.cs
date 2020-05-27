using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BookMarkingDataBase
{
    class SqlActivity
    {
        // Recieves list of ImageInfos.  Does * search on database and fills in foundDataList.  Returns Number of
        // images
        internal long findAll(List<ImageInfo> foundData)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                StringBuilder queryString = new StringBuilder();
                queryString.Append("SELECT ");
                queryString.Append("* ");
                queryString.Append("FROM ");
                queryString.Append("[dbo].[PageCount]");

                using (SqlCommand cmd = new SqlCommand(queryString.ToString(), conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                            while (reader.Read())
                            {
                                fillNewFile(foundData, reader);
                            }
                    }
                }
            }
            return foundData.LongCount();
        }
        internal long findBatch(List<ImageInfo> foundData, string batch)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("[dbo].[uspPageCountSelectByBatch]", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@batchName", batch));
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fillNewFile(foundData, reader);
                        }
                    }
                }
            }
            return foundData.LongCount();
        }
        internal void fillNewFile (List<ImageInfo> foundData, SqlDataReader reader)
        {
            ImageInfo newFile = new ImageInfo();
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("fileName"))]))
            {
                newFile.FileName = reader["fileName"].ToString();
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("batchName"))]))
            {
                newFile.BatchName = reader["batchName"].ToString();
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("originalPageCount"))]))
            {
                newFile.OriginalPageCount = Convert.ToInt32(reader["originalPageCount"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("bookMarkedPageCount"))]))
            {
                newFile.BookmarkedPageCount = Convert.ToInt32(reader["bookmarkedPageCount"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("loanNumber"))]))
            {
                newFile.LoanNumber = reader["loanNumber"].ToString();
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("dateUpload"))]))
            {
                newFile.DateUpload = Convert.ToDateTime(reader["dateUpload"]);
            }
            if (!DBNull.Value.Equals(reader[(reader.GetOrdinal("dateDownload"))]))
            {
                newFile.DateDownload = Convert.ToDateTime(reader["dateDownload"]);
            }
            foundData.Add(newFile);
        }
        internal int insertFile(ImageInfo newFile)
        {
            int rowsAffected;
            string connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("[dbo].[uspPageCountInsert]", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@batchName", newFile.BatchName));
                    cmd.Parameters.Add(new SqlParameter("@fileName", newFile.FileName));
                    cmd.Parameters.Add(new SqlParameter("@originalPageCount", newFile.OriginalPageCount));
                    cmd.Parameters.Add(new SqlParameter("@loanNumber", newFile.LoanNumber));
                    cmd.Parameters.Add(new SqlParameter("@dateUpload", newFile.DateUpload));
                    cmd.CommandType = CommandType.StoredProcedure;
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            if (rowsAffected < 0)
            {
                rowsAffected = 0;
            }
            return rowsAffected;
        }
        internal int insertBookmark(ImageInfo newFile)
        {
            int rowsAffected = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("[dbo].[uspPageCountUpdateBookmarkedPageCount]", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@batchName", newFile.BatchName));
                    cmd.Parameters.Add(new SqlParameter("@fileName", newFile.FileName));
                    cmd.Parameters.Add(new SqlParameter("@bookmarkedPageCount", newFile.BookmarkedPageCount));
                    cmd.Parameters.Add(new SqlParameter("@loanNumber", newFile.LoanNumber));
                    cmd.Parameters.Add(new SqlParameter("@dateDownload", newFile.DateDownload));
                    cmd.CommandType = CommandType.StoredProcedure;
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            if(rowsAffected < 0)
            {
                rowsAffected = 0;
            }
            return rowsAffected;
        }
    }


}
