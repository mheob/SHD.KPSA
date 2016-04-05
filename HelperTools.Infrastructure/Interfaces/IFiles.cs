namespace HelperTools.Infrastructure.Interfaces
{
    using System;

    /// <summary>The IFiles.</summary>
    public interface IFiles
    {
        /// <summary>Gets or sets the full file path.</summary>
        /// <value>The full file path.</value>
        string FullFilePath { get; set; }

        /// <summary>Gets or sets the name of the file.</summary>
        /// <value>The name of the file.</value>
        string FileName { get; set; }

        /// <summary>Gets or sets the last time of modified.</summary>
        /// <value>The last time of modified.</value>
        DateTime CreatedTime { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is selected.</summary>
        /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
        bool IsSelected { get; set; }
    }
}