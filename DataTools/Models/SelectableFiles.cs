using System;

namespace DataTools.Models
{
    public class SelectableFiles
    {
        public string FileName { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsSelected { get; set; }
    }
}