using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;

namespace AzureStorageTableDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string conf=ConfigurationManager.AppSettings[0].ToString();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(conf);
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("customers");
            table.CreateIfNotExists();

            CustomerEntity customer1 = new CustomerEntity("AP", "A01")
            {
                FirstName = "Jonny",
                LastName = "Gaddar",
                Email = "jon@wer.com",
                PhoneNumber = "985412547"
            };

            TableOperation insertOPeration = TableOperation.Insert(customer1);
            table.Execute(insertOPeration);
        }
    }

    public class CustomerEntity:TableEntity
    {
        public CustomerEntity()
        {

        }
        public CustomerEntity(string state,string customerId)
        {
            PartitionKey = state;
            RowKey = customerId;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
