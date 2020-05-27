using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using ResturantReviewsAPI.Models;
using ResturantReviewsAPI.Providers;
using ResturantReviewsAPI.Results;

namespace ResturantReviewsAPI.Controllers
{
    public class AccountsController : ApiController
    {
        public IQueryable<Models.Accounts> Get()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("Accounts");

            table.CreateIfNotExists();

            TableQuery<Models.Accounts> query = new TableQuery<Models.Accounts>();

            var accounts = table.ExecuteQuery(query).AsQueryable();

            return accounts;
        }


        public IQueryable<Models.Accounts> Get(string id)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("Accounts");

            TableQuery<Models.Accounts> query = new TableQuery<Models.Accounts>().Where
                (TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));

            var accounts = table.ExecuteQuery(query).AsQueryable();

            return accounts;
        }


        public HttpResponseMessage Post([FromBody] Models.Accounts newAccount)
        {
            // Create new account from passed in info
            Models.Accounts account = new Models.Accounts(newAccount.username);
            account.username = newAccount.username;
            account.password = newAccount.password;


            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
               CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("Accounts");

            TableOperation insertOperation = TableOperation.Insert(account);

            // Check if new account is valid, if not, send error
            if (ModelState.IsValid)
            {
                //Query database for new event, if in database, add as new
                TableOperation retrieveOperation = TableOperation.Retrieve<Models.Accounts>(account.PartitionKey, account.RowKey);

                TableResult retirevedResult = table.Execute(retrieveOperation);

                Models.Accounts result = (Models.Accounts)retirevedResult.Result;
                // If valid account and not in database, enter into db. Else print error
                if (result == null)
                {
                    table.Execute(insertOperation);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Account exists");
                }

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Account");
            }

        }


        public HttpResponseMessage Put([FromBody] Models.Accounts newAccount)
        {
            // Create new account from passed in info
            Models.Accounts account = new Models.Accounts(newAccount.username);
            account.username = newAccount.username;
            account.password = newAccount.password;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
               CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("Accounts");

            // Check if account is valid, if not, send error
            if (ModelState.IsValid)
            {
                //Query database for new account, if in database, add as new
                TableOperation retrieveOperation = TableOperation.Retrieve<Models.Accounts>(account.PartitionKey, account.RowKey);

                TableResult retirevedResult = table.Execute(retrieveOperation);

                Models.Accounts result = (Models.Accounts)retirevedResult.Result;

                if (result == null)
                {
                    // Enter if no result
                    TableOperation insertOperation = TableOperation.Insert(account);
                    table.Execute(insertOperation);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else // update account
                {
                    result.username = account.username;
                    result.password = account.password;
                    result.RowKey = account.RowKey;
                    result.PartitionKey = account.PartitionKey;

                    TableOperation updateOperation = TableOperation.Replace(result);

                    table.Execute(updateOperation);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Event");
            }

        }

        public HttpResponseMessage Delete(string id)
        {


            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
              CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("Accounts");

            TableOperation retrieveOperation = TableOperation.Retrieve<Models.Accounts>("one", id);

            TableResult retirevedResult = table.Execute(retrieveOperation);

            Models.Accounts result = (Models.Accounts)retirevedResult.Result;
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "User not in db");
            }
            else
            {
                TableOperation deleteOperation = TableOperation.Delete(result);

                table.Execute(deleteOperation);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
