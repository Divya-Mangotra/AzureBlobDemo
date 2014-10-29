using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

//Following code accesses blob storage programmatically, creates a public and private container and lists the blobs stored in the container along with their length.

namespace AzureStorageDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            //Creates a Connection string.
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=blobeg;AccountKey=4d5iWHQidQDYeF9o+sW7N8vCZLR7rzsibT5B6vXL7l1674CPpXnz3c01vHPaRtz4yFRC1zF9dHQYDeOtl2fDMg==");

            //Creates a CloudBlobClient object using the storage account object we retrieved above.
           CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

           //Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("publiccontainer");

            //Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            //Setting container type to public so that files within the container available to everyone.
            container.SetPermissions(
            new BlobContainerPermissions
            {
                PublicAccess =
                BlobContainerPublicAccessType.Blob
             });

            //Retrieve reference to a blob named "blob".
            CloudBlockBlob blob = container.GetBlockBlobReference("public.jpg");


            //Create or overwrite the "blob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(@"C:\public.jpg"))
            {
                blob.UploadFromStream(fileStream);
            }

            Console.WriteLine("Files successfully uploaded at " + blob.Uri);

            //Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blockblob = (CloudBlockBlob)item;

                        Console.WriteLine("Block blob of length {0}: {1}", blockblob.Properties.Length, blockblob.Uri);

                    }

                    else if (item.GetType() == typeof(CloudPageBlob))
                    {
                        CloudPageBlob pageblob = (CloudPageBlob)item;
                        Console.WriteLine("Page blob of length {0}: {1}", pageblob.Properties.Length, pageblob.Uri);

                    }

                    else if (item.GetType() == typeof(CloudBlobDirectory))
                    {

                        CloudBlobDirectory directory = (CloudBlobDirectory)item;
                        Console.WriteLine("Directory: {0}", directory.Uri);
                    }

                }
            }




            //Retrieve a reference to a container.
            
            CloudBlobContainer con1 = blobClient.GetContainerReference("privatecontainer");

            //Create the container if it doesn't already exist.
            con1.CreateIfNotExists();



            // Retrieve reference to a blob named "blob1".
            CloudBlockBlob blob1 = con1.GetBlockBlobReference("private.jpg");

            // Create or overwrite the "blob1" blob with contents from a local file.
            using (var fileStream1 = System.IO.File.OpenRead(@"C:\private.jpg"))
            {
                blob1.UploadFromStream(fileStream1);
            }


            // Create a permission policy to set the public access setting for the container. 
            BlobContainerPermissions containerPermissions = new BlobContainerPermissions();

            // The public access setting explicitly specifies that the container is private, so that it can't be accessed anonymously.
            containerPermissions.PublicAccess = BlobContainerPublicAccessType.Off;

            //Set the permission policy on the container.
            con1.SetPermissions(containerPermissions);


            Console.WriteLine("Files successfully uploaded at " + blob1.Uri);


            // Loop over items within the container and output the length and URI.

            foreach (IListBlobItem item1 in con1.ListBlobs(null, false))
            {
                {
                    if (item1.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blockblob1 = (CloudBlockBlob)item1;

                        Console.WriteLine("Block blob of length {0}: {1}", blockblob1.Properties.Length, blockblob1.Uri);

                    }

                    else if (item1.GetType() == typeof(CloudPageBlob))
                    {
                        CloudPageBlob pageblob1 = (CloudPageBlob)item1;
                        Console.WriteLine("Page blob of length {0}: {1}", pageblob1.Properties.Length, pageblob1.Uri);

                    }

                    else if (item1.GetType() == typeof(CloudBlobDirectory))
                    {

                        CloudBlobDirectory directory1 = (CloudBlobDirectory)item1;
                        Console.WriteLine("Directory: {0}", directory1.Uri);
                    }

                }
            }

            
        }
    }
}

  
