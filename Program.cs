using System;
using Azure.Storage.Blobs;
using Microsoft.Azure.Storage.Blob;
using System.IO;
using Microsoft.Azure.Storage;
using System.Net;
using System.Threading.Tasks;

namespace BobContainerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=bobstoragetsg;AccountKey=GIWfXAYz0aQCiIE/IiNz9GkkROtJmz5o6aE7yjulr3NpgwjR9Vw+yM+HYFVPXU3f5QpDxgB8THjY1nE+8sjW2A==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference("first");
            cloudBlobContainer.CreateIfNotExists();
            CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference("myfist" + ".txt");
            Console.WriteLine("File Uploading...");
            UploadFile(blockBlob).Wait();
            Console.WriteLine("file uploaded sucessfully");
            Console.ReadLine();
        }

        private static async Task UploadFile(CloudBlockBlob blockBlob)
        {
            string filepath = @"C:\Users\tanis\Desktop\Blob.txt";
            string newcontent;
            
            if (!File.Exists(filepath))
            {
              File.Create(filepath);
                Console.WriteLine("enter the new text to be entered to the file");
                newcontent = Console.ReadLine();
               File.AppendAllText(filepath, newcontent);

            }
            else
            {
                var c = File.ReadAllText(filepath);
                Console.WriteLine("File Current text \n");
                Console.WriteLine(c);
                Console.WriteLine(" \n enter the new text to be appended at the end");
                newcontent = Console.ReadLine();
                File.AppendAllText(filepath, newcontent);


            }

            var filestream = File.OpenRead(filepath);

            await blockBlob.UploadFromStreamAsync(filestream);
        }
    }
}
