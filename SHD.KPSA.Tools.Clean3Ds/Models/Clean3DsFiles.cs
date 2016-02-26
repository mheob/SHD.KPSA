namespace SHD.KPSA.Tools.Clean3Ds.Models
{
    using System;

    /// <summary>
    /// The model class to fill a list with Tools3DsFiles.
    /// </summary>
    public class Clean3DsFiles
    {
        public string FullFilePath { get; set; }
        public string FileName { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsSelected { get; set; }
    }
}