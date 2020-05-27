using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.ComponentModel.DataAnnotations;

namespace ResturantReviewsAPI.Models
{
    public class restReviews : TableEntity
    {
        public restReviews(string userName, string restname)
        {
            this.PartitionKey = userName;
            this.RowKey = restname;
        }

        public restReviews() { }
        [Required]
        public string username { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string restName { get; set; }
        public int rating { get; set; }

    }
}