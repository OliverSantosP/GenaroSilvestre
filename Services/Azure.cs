using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Web.Configuration;
using System.Configuration;

namespace GenaroSilvestre.Services
{
    public class Azure
    {
        public static string UploadImage(HttpPostedFileBase file)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(WebConfigurationManager.AppSettings["AzureContainer"]);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.FileName);
                blockBlob.UploadFromStream(file.InputStream);

                return blockBlob.Uri.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteImage(string ImageURL)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(WebConfigurationManager.AppSettings["AzureContainer"]);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(ImageURL.Split('/').Last());
                blockBlob.Delete();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}