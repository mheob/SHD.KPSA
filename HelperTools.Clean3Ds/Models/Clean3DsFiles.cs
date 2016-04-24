namespace HelperTools.Clean3Ds.Models
{
    using System;
    using Infrastructure.Interfaces;

    /// <summary>The model class to fill a list with Clean3DsFiles.</summary>
    /// <seealso cref="IFiles" />
    public class Clean3DsFiles : IFiles
    {
        /// <summary>Gets or sets the full file path.</summary>
        /// <value>The full file path.</value>
        public string FullFilePath { get; set; }

        /// <summary>Gets or sets the name of the file.</summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>Gets or sets the last time of modified.</summary>
        /// <value>The last time of modified.</value>
        public DateTime CreatedTime { get; set; }

        /// <summary>Gets or sets the size of the file.</summary>
        /// <value>The size of the file.</value>
        public long FileSize { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is selected.</summary>
        /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
        public bool IsSelected { get; set; }
    }
}