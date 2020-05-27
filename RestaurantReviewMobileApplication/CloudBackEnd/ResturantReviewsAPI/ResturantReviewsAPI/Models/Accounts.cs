using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.ComponentModel.DataAnnotations;

namespace ResturantReviewsAPI.Models
{
    public class Accounts : TableEntity
    {
        public Accounts(string name)
        {
            this.PartitionKey = "one";
            this.RowKey = name;
        }

        public Accounts() { }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }

    }
}