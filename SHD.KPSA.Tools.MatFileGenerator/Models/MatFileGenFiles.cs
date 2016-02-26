namespace SHD.KPSA.Tools.MatFileGenerator.Models
{
    using System;

    /// <summary>
    /// The model class to fill a list with MatFileGenFiles.
    /// </summary>
    public class MatFileGenFiles
    {
        public string FullFilePath { get; set; }
        public string FileName { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsSelected { get; set; }
    }
}