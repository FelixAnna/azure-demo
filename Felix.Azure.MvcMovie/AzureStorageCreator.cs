using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Felix.Azure.MvcMovie
{
    public class AzureStorageCreator
    {
        public AzureStorageCreator()
        {

        }

        // Retrieve the connection string for use with the application. The storage connection string is stored
        // in an environment variable on the machine running the application called storageconnectionstring.
        // If the environment variable is created after the application is launched in a console or with Visual
        // Studio, the shell needs to be closed and reloaded to take the environment variable into account.
        private const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=felix2018;AccountKey=jInjPDx6l2AjUP+Ii0SdWutcQijgw3faNZsj8Y3iD2hTp5nbU4/FMa52jDCSQtc+vCCflgNL5b/7kjKPcHRJcA==;EndpointSuffix=core.windows.net";
        private const string myBlobName = "felix-blob-2018";

        public async Task<string> CreateLogFileAsync()
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            string sourceFile = null;
            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    cloudBlobContainer = cloudBlobClient.GetContainerReference(myBlobName);

                    // Create a file in your local MyDocuments folder to upload to a blob.
                    string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string localFileName = "HelloWorld_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString() + ".txt";
                    sourceFile = Path.Combine(localPath, localFileName);
                    // Write text to the file.
                    File.WriteAllText(sourceFile, $"{Environment.MachineName} - {DateTime.Now}");

                    // Get a reference to the blob address, then upload the file to the blob.
                    // Use the value of localFileName for the blob name.
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
                    await cloudBlockBlob.UploadFromFileAsync(sourceFile);

                    return sourceFile;
                }
                catch (StorageException ex)
                {
                    Console.WriteLine("Error returned from the service: {0}", ex.Message);
                }
                finally
                {
                    // Clean up resources. This includes the container and the two temp files.
                    Console.WriteLine("Deleting the local source file and local downloaded files");
                    Console.WriteLine();
                    File.Delete(sourceFile);
                }
            }
            else
            {
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add a environment variable named 'storageconnectionstring' with your storage " +
                    "connection string as a value.");
            }

            return "failed";
        }
    }
}
