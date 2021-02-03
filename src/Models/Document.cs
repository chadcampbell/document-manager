#region Using Directives

using System;
using System.Text.Json.Serialization;

#endregion Using Directives

namespace DocumentManager.Models
{
    /// <summary>
    /// A file that has been uploaded into the Document Manager
    /// </summary>
    public class Document
    {
        #region Properties

        /// <summary>
        /// The unique identifier of the file.
        /// </summary>
        /// <remarks>
        /// This property is necessary to distinguish between multiple files with the same name.
        /// </remarks>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The file name as it was when it was uploaded.
        /// </summary>
        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// The number of bytes in the <c>Document</c> file.
        /// </summary>
        [JsonPropertyName("fileSize")]
        public long FileSize { get; set; }

        /// <summary>
        /// The extension of the <c>Document</c> file.
        /// </summary>
        [JsonPropertyName("extension")]
        public string FileExtension { get; set; }

        /// <summary>
        /// The date/time the file was uploaded.
        /// </summary>
        [JsonPropertyName("uploadDate")]
        public DateTime UploadDate { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// The default constructor for a <c>Document</c>
        /// </summary>
        public Document()
        {}

        #endregion Constructors

        #region Methods

        /// <summary>
        /// This method returns a user-friendly version of the object for use on the client-side
        /// </summary>
        public object ForClient()
        {
            return new
            {
                id = this.Id,
                extension = this.FileExtension,
                fileName = this.FileName,
                fileSize = FormatFileSize(this.FileSize),
                uploadDate = this.UploadDate.ToLocalTime().ToString("h:mm tt MM-dd-yyyy")
            };
        }

        /**
         * Formats the size of the <c>Document</c> in an easy-to-read format.
         **/
        private string FormatFileSize(long fileSize)
        {
            var result = "unknown";
            if (fileSize < 1000)                            // Lower limit for a kilobyte (rounded)
                result = $"{fileSize} B";
            else if (fileSize < 1000000)                    // Lower limit for a megabyte (rounded)
            {
                var rounded = (double)(fileSize) / 1000;
                result = $"{rounded.ToString("N1")} KB";
            }
            else if (fileSize < 1000000000)                 // Lower limit for a gigabyte (rounded)
            {
                var rounded = (double)(fileSize) / 1000000;
                result = $"{rounded.ToString("N1")} MB";
            }
            return result;
        }

        #endregion Methods
    }
}