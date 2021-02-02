#region Using Directives

using System;
using System.Diagnostics;
using System.IO;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;

using DocumentManager.Models;

#endregion Using Directives

namespace DocumentManager.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : Controller
    {
        // The logger used to report application usage.
        private readonly ILogger<ApiController> _logger;

        // The directory where the documents are stored.
        private readonly string _documentsDirectory;

        // A proxy to map file extensions with mime types
        private FileExtensionContentTypeProvider mimeTypeProvider = new FileExtensionContentTypeProvider();

        /// <summary>
        /// The default constructors
        /// </summary>
        public ApiController(IWebHostEnvironment hostEnvironment, ILogger<ApiController> logger)
        {
            _documentsDirectory = Path.Combine(hostEnvironment.WebRootPath, "documents");
            _logger = logger;
        }

        /// <summary>
        /// The action associated with downloading the document.
        /// </summary>
        [HttpGet("download-document")]
        public IActionResult DownloadDocument(string f)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation($"Document download requested. (fileName: '{f}')");

                // Ensure the file exists
                var filePath = Path.Combine(_documentsDirectory, f);
                if (System.IO.File.Exists(filePath) == false)
                {
                    stopwatch.Stop();

                    _logger.LogInformation($"The document requested for download was not found.");
                    return NotFound();
                }

                // Identity the mime type of the file.
                var mimeType = string.Empty;
                if (mimeTypeProvider.TryGetContentType(filePath, out mimeType) == false)
                    mimeType = "application/octet-stream";
                var fileContent = System.IO.File.ReadAllBytes(filePath);

                stopwatch.Stop();
                 _logger.LogInformation($"The document requested for download has been retrieved. (duration:{stopwatch.ElapsedMilliseconds} ms.)");
                return new FileContentResult(fileContent, mimeType);
            }
            catch (Exception ex1)
            {
                _logger.LogError(ex1, $"There was a problem downloading the document. {ex1.Message} (duration:{stopwatch.ElapsedMilliseconds} ms.)");
                return Problem();
            }
        }

        /// <summary>
        /// The action associated with uploading a document.
        /// </summary>
        [HttpPost("upload-document")]
        public IActionResult UploadDocument(IFormCollection data, IFormFile document)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                // Ensure that a document was uploaded.
                var documentId = Guid.NewGuid().ToString().ToLower();
                if ((document == null) || (document.Length == 0))
                    return BadRequest(); 
                _logger.LogInformation($"Document received for uploading. (ContentType: '{document.ContentType}', FileName: '{document.FileName}', Length: {document.Length}, Name: '{document.Name}')");

                // Ensure the "wwwroot/documents" directory exists. This is where documents are stored.
                if (Directory.Exists(_documentsDirectory) == false)
                {
                    Directory.CreateDirectory(_documentsDirectory);
                }
                Document result = null;

                // Load the index of documents and add another.
                var index = Documents.GetIndex(_documentsDirectory);
                using (var documentStream = document.OpenReadStream())
                {
                    result = index.AddDocument(documentId, document.FileName, documentStream);
                }

                stopwatch.Stop();
                _logger.LogInformation($"The document was successfully uploaded. (duration:{stopwatch.ElapsedMilliseconds} ms.");

                return Ok(result.ForClient());
            }
            catch (Exception ex1)
            {
                stopwatch.Stop();
                _logger.LogError(ex1, $"There was a problem uploading the document. {ex1.Message} (duration:{stopwatch.ElapsedMilliseconds} ms.)");

                // Log that there was a problem uploading the file
                return Problem();
            }
        }

        /// <summary>
        /// The action associated with deleting a document.
        /// </summary>
        [HttpDelete("delete-document")]
        public IActionResult RemoveDocument(string id)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation($"Document removal requested. (id: '{id}')");

                // Load the index of documents and add another.
                var index = Documents.GetIndex(_documentsDirectory);
                var removed = index.RemoveDocument(id);

                stopwatch.Stop();
                var message = (removed) 
                    ? "The document was successfully removed."
                    : "The document was not removed.";
                _logger.LogInformation($"{message} (duration: {stopwatch.ElapsedMilliseconds} ms.)");

                return Ok();
            }
            catch (Exception ex1)
            {
                _logger.LogError(ex1, $"There was a problem removing the document. {ex1.Message} (duration:{stopwatch.ElapsedMilliseconds} ms.)");
                return Problem();
            }
        }
    }
}