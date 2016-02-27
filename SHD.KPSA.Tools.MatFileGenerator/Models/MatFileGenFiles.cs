namespace SHD.KPSA.Tools.MatFileGenerator.Models
{
    using System;

    /// <summary>
    /// The model class to fill a list with MatFileGenFiles.
    /// </summary>
    public class MatFileGenFiles
    {
        /// <summary>
        /// Gets and sets full path to the file.
        /// </summary>
        public string FullFilePath { get; set; }

        /// <summary>
        /// Gets and sets the filename of file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets and sets the <see cref="DateTime" /> of the last modification.
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets and sets the selection state in a ListBox or something like this.
        /// </summary>
        public bool IsSelected { get; set; }
    }
}