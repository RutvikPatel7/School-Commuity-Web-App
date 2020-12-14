using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Assignment1.Data;
using Assignment1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Controllers
{
    public class AdvertisementsController : Controller
    {

        private readonly SchoolCommunityContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string containerName = "advertisements";

        public AdvertisementsController(SchoolCommunityContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Advertisements.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile files)
        {

            BlobContainerClient containerClient;
            // Create the container and return a container client object
            try
            {
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }
            catch (RequestFailedException)
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }

            try
            {
                // create the blob to hold the data
                var blockBlob = containerClient.GetBlobClient(files.FileName);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                using (var memoryStream = new MemoryStream())
                {
                    // copy the file data into memory
                    await files.CopyToAsync(memoryStream);

                    // navigate back to the beginning of the memory stream
                    memoryStream.Position = 0;

                    // send the file to the cloud
                    await blockBlob.UploadAsync(memoryStream);
                    memoryStream.Close();
                }

                // add the photo to the database if it uploaded successfully
                var image = new Advertisement();
                image.Url = blockBlob.Uri.AbsoluteUri;
                image.FileName = files.FileName;

                _context.Advertisements.Add(image);
                _context.SaveChanges();
            }
            catch (RequestFailedException)
            {
                View("Error");
            }

            return RedirectToAction("Index");
        }

        // For multiple files, use this
        /*public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {

            BlobContainerClient containerClient;
        
            // Create the container and return a container client object
            try
             {
                 containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
             }
             catch (RequestFailedException e)
             {
                  containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }

            foreach (var file in files)
              {
                  try{
                     
                  // create the blob to hold the data
                  var blockBlob = containerClient.GetBlobClient(file.FileName);
                     if (await blockBlob.ExistsAsync())
                    {
                        await blockBlob.DeleteAsync();
                    }

                     using (var memoryStream = new MemoryStream())
                     {
                         // copy the file data into memory
                         await file.CopyToAsync(memoryStream);

                         // navigate back to the beginning of the memory stream
                         memoryStream.Position = 0;

                         // send the file to the cloud
                         await blockBlob.UploadAsync(memoryStream);
                         memoryStream.Close();
                     }

                     // add the photo to the database if it uploaded successfully
                     var image = new Advertisement();
                     image.Url = blockBlob.Uri.AbsoluteUri;
                     image.FileName = file.FileName;

                     _context.Advertisements.Add(image);
                     await _context.SaveChangesAsync();
                 }
                 catch (RequestFailedException e)
                 {
                    return RedirectToAction("Index");
                 }
             }
            return RedirectToAction("Index");
    }*/

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.AdvertisementId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Advertisements.FindAsync(id);


            BlobContainerClient containerClient;
            // Get the container and return a container client object
            try
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }
            catch (RequestFailedException)
            {
                return View("Error");
            }

            try
            {
                // Get the blob that holds the data
                var blockBlob = containerClient.GetBlobClient(image.FileName);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                _context.Advertisements.Remove(image);
                await _context.SaveChangesAsync();

            }
            catch (RequestFailedException)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }
    }
}
