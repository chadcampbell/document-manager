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
    /// <summary>
    /// The index of <c>Documents</c> in the Document Manager.
    /// </summary>
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

        /// <summary>
        /// Constructs an index of <c>Documents</c>.
        /// </summary>
        public Documents(string directory)
        {
            this.List = new List<Document>();
            this.directory = directory;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Retrieves the <c>Document</c> entities from a specific directory.
        /// </summary>
        public static Documents GetIndex(string directory)
        {
            // Ensure the document index location has been provided
            if (String.IsNullOrWhiteSpace(directory))
                throw new ArgumentException("The directory where the index is located is expected. However, a directory was not provided.");

            // Load the document index if it exists. It's expected that the document index may not have been created yet.
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
        /// Adds a <c>Document</c> to the index (i.e. collection of <c>Documents</c>).
        /// </summary>
        public Document AddDocument(string documentId, string fileName, Stream sourceFileStream)
        {
            // Ensure the expected properties have been provided
            if (String.IsNullOrWhiteSpace(documentId))
                throw new ArgumentException("A unique identifier for the document to be added to the index was expected. However, an identifer was not provided.");
            if (String.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("A user-friendly name for the document to be added to the index was expected. However, a user-friendly file name was not provided.");
            if (sourceFileStream == null)
                throw new ArgumentException("A stream for the document to be added to the index was expected. However, a stream was not provided.");

            // Ensure the directory for the documents exists.
            if (Directory.Exists(this.directory) == false)
            {
                Directory.CreateDirectory(this.directory);
            }

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
        public bool RemoveDocument(Document document)
        {
            // Ensure the document to be removed has been provided.
            if (document == null)
                throw new ArgumentException("A document to be removed from the index was expected. However, a document was not provided.");

            // Remove the file from the file system
            var fileNameToRemove = $"{document.Id}.{document.FileExtension}";
            var fileToRemove = Path.Combine(this.directory, fileNameToRemove);
            File.Delete(fileToRemove);
        
            // Remove the document from the index.
            this.List.Remove(document);
            this.Persist();

            return true;
        }

        /// <summary>
        /// Searches for a document with the given id.
        /// </summary>
        /// <returns>
        /// The <c>Document</c> with the provided id. If a <c>Document</c> with the provided id does not exist, <c>null</c> will be returned.
        /// </returns>
        public Document FindDocumentById(string documentId)
        {
            return this.List.Find(x => x.Id == documentId);
        }

        /// <summary>
        /// Searches for a document with the given filename.
        /// </summary>
        /// <returns>
        /// The <c>Document</c> with the given filename. If a <c>Document</c> with the given filename does not exist, <c>null</c> will be returned.
        /// </returns>
        public Document FindDocumentByFileName(string fileName)
        {
            return this.List.Find(x => x.FileName == fileName);
        }

        /**
         * Saves the <c>Document</c> index to the "index.json" file.
         **/
        private void Persist()
        {
            // Ensure that only one document can be written to the index at a time.
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