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

            //CustomerEntity customer1 = new CustomerEntity("AP", "A01")
            //{
            //    FirstName = "Jonny",
            //    LastName = "Gaddar",
            //    Email = "jon@wer.com",
            //    PhoneNumber = "985412547"
            //};

            //TableOperation insertOPeration = TableOperation.Insert(customer1);
            //table.Execute(insertOPeration);

            //Bulk Insert
            TableBatchOperation batchOperation = new TableBatchOperation();
            CustomerEntity customer2 = new CustomerEntity("AP", "A02")
            {
                FirstName = "Tony",
                LastName = "Gaddar",
                Email = "jon@wer.com",
                PhoneNumber = "985412547"
            };

            CustomerEntity customer3 = new CustomerEntity("AP", "A03")
            {
                FirstName = "Golu",
                LastName = "Gaddar",
                Email = "jon@wer.com",
                PhoneNumber = "985412547"
            };

            CustomerEntity customer4 = new CustomerEntity("AP", "A04")
            {
                FirstName = "Molu",
                LastName = "Gaddar",
                Email = "jon@wer.com",
                PhoneNumber = "985412547"
            };

            batchOperation.Insert(customer2);
            batchOperation.Insert(customer3);
            batchOperation.Insert(customer4);

            table.ExecuteBatch(batchOperation);
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
