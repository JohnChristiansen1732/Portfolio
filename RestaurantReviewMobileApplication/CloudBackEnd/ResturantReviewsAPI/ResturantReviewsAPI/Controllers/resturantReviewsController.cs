using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ResturantReviewsAPI.Controllers
{
    public class resturantReviewsController : ApiController
    {
        public IQueryable<Models.restReviews> Get()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("restReviews");

            table.CreateIfNotExists();

            TableQuery<Models.restReviews> query = new TableQuery<Models.restReviews>();

            var reviews = table.ExecuteQuery(query).AsQueryable();

            return reviews;
        }
        public IQueryable<Models.restReviews> Get(string id)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("restReviews");

            TableQuery<Models.restReviews> query = new TableQuery<Models.restReviews>().Where
                (TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id));

            var reviews = table.ExecuteQuery(query).AsQueryable();

            return reviews;
        }
        public IQueryable<Models.restReviews> Get(string username, string restName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("restReviews");

            TableQuery<Models.restReviews> query = new TableQuery<Models.restReviews>().Where(TableQuery.CombineFilters(
                (TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, username)),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, restName)));

            var reviews = table.ExecuteQuery(query).AsQueryable();

            return reviews;
        }
        public HttpResponseMessage Post([FromBody] Models.restReviews newReview)
        {
            // Create new team from passed in info
            Models.restReviews review = new Models.restReviews(newReview.username, newReview.restName);
            review.username = newReview.username;
            review.address = newReview.address;
            review.restName = newReview.restName;
            review.rating = newReview.rating;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
               CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("restReviews");

            TableOperation insertOperation = TableOperation.Insert(review);

            // Check if new review is valid, if not, send error
            if (ModelState.IsValid)
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<Models.restReviews>(review.username, review.restName);

                TableResult retirevedResult = table.Execute(retrieveOperation);

                Models.restReviews reviews = (Models.restReviews)retirevedResult.Result;
                if (reviews == null)
                {
                    // If valid review and not in database, enter into db
                    table.Execute(insertOperation);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Review Exists");
                }

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Review");
            }

        }
        public HttpResponseMessage Put([FromBody] Models.restReviews newReview)
        {
            // Create new review from passed in info
            Models.restReviews review = new Models.restReviews(newReview.username, newReview.restName);
            review.username = newReview.username;
            review.address = newReview.address;
            review.restName = newReview.restName;
            review.rating = newReview.rating;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
               CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("restReviews");

            // Check if new review is valid, if not, send error
            if (ModelState.IsValid)
            {
                //Query database for new review, if in database, add as new
                TableOperation retrieveOperation = TableOperation.Retrieve<Models.restReviews>(review.PartitionKey, review.RowKey);

                TableResult retirevedResult = table.Execute(retrieveOperation);

                Models.restReviews updatedReview = (Models.restReviews)retirevedResult.Result;

                if (updatedReview == null)
                {
                    // Enter if no result
                    TableOperation insertOperation = TableOperation.Insert(review);
                    table.Execute(insertOperation);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    updatedReview.username = review.username;
                    updatedReview.address = review.address;
                    updatedReview.restName = review.restName;
                    updatedReview.rating = review.rating;

                    TableOperation updateOperation = TableOperation.Replace(updatedReview);

                    table.Execute(updateOperation);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Review");
            }

        }
        public HttpResponseMessage Delete([FromBody] Models.restReviews review)
        {


            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
              CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("RestReviews");

            TableOperation retrieveOperation = TableOperation.Retrieve<Models.restReviews>(review.username, review.restName);

            TableResult retirevedResult = table.Execute(retrieveOperation);

            Models.restReviews deletedReview = (Models.restReviews)retirevedResult.Result;

            if (deletedReview == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Review not in db");
            }
            else
            {
                TableOperation deleteOperation = TableOperation.Delete(deletedReview);

                table.Execute(deleteOperation);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
