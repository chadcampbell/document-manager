#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

#endregion Using Directives

namespace DocumentManager.Models
{
    public class Documents
    {
        #region Properties

        /// <summary>
        /// A collection of <c>Document</c> entities.
        /// </summary>
        [JsonPropertyName("documents")]
        public List<Document> List { get; set; }

        // The path to the directory where the documents are stored.
        private string directory = string.Empty;

        // A lock to prevent a race condition on the index.json file.
        private readonly object indexLock = new object();

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <remarks>This is needed for serialization / deserialization</remarks>
        public Documents()
        {}

        public Documents(string directory)
        {
            this.List = new List<Document>();
            this.directory = directory;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Retrieves the <c>Document</c> entities in a specific directory.
        /// </summary>
        public static Documents GetIndex(string directory)
        {
            var documents = new Documents(directory);
            if (Directory.Exists(directory))
            {
                var documentIndexPath = Path.Combine(directory, "index.json");
                if (File.Exists(documentIndexPath))
                {
                    var index = File.ReadAllBytes(documentIndexPath);
                    documents = JsonSerializer.Deserialize<Documents>(index);
                    documents.SetDirectory(directory);
                }            
            }
            return documents;
        }

        /// <summary>
        /// Adds a <c>Document</c> to the collection of <c>Documents</c>.
        /// </summary>
        public Document AddDocument(string documentId, string fileName, Stream sourceFileStream)
        {
            // Generate a filename for the uploaded file.
            var fileExtension = Path.GetExtension(fileName);
            var filePath = Path.Combine(this.directory, documentId);
            var newFileName = $"{filePath}{fileExtension}";

            // Upload the file to the documents directory
            long fileSize = 0;
            using (var targetFileStream = new FileStream(newFileName, FileMode.Create))
            {
                sourceFileStream.CopyTo(targetFileStream);
                fileSize = targetFileStream.Length;
            }

            // Add the Document entity to the collection
            var document = new Document()
            {
                Id = documentId,
                FileName = fileName,
                FileExtension = fileExtension.Substring(1),                     // The file extension originally includees the ".". That is undesired.
                FileSize = fileSize,
                UploadDate = DateTime.UtcNow
            };

            this.List.Add(document);
            this.Persist();

            return document;
        }

        /// <summary>
        /// Removes a <c>Document</c> from the collection of <c>Documents</c>.
        /// </summary>
        public bool RemoveDocument(string documentId)
        {
            // Find the document in the index
            var documentToRemove = this.List.Find(x => x.Id == documentId);
            if (documentToRemove == null)
                return false;

            // Remove the file from the file system
            var fileNameToRemove = $"{documentToRemove.Id}.{documentToRemove.FileExtension}";
            var fileToRemove = Path.Combine(this.directory, fileNameToRemove);
            File.Delete(fileToRemove);
        
            // Remove the document from the index.
            this.List.Remove(documentToRemove);
            this.Persist();

            return true;
        }

        /**
         * Saves the <c>Document</c> index to the "index.json" file.
         **/
        private void Persist()
        {
            lock (indexLock)
            {
                var json = JsonSerializer.Serialize(this);
                var bytes = Encoding.ASCII.GetBytes(json);

                // Save the index
                var documentIndexPath = Path.Combine(directory, "index.json");
                File.WriteAllBytes(documentIndexPath, bytes);
            }
        }

        /**
         * This method is a setter for the "directory" property.
         * This setter method exists because the "directory" property is not set during deserialization from the .json file.
         * We do not want to put the "directory" value in the .json file.
         **/
        private void SetDirectory(string directory)
        {
            this.directory = directory;
        }

        #endregion Methods
    }
}