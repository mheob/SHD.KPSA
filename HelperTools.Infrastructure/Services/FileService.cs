namespace HelperTools.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>File handling class, that processes the files to text layer.</summary>
    public static class FileService
    {
        /// <summary>Reads a text document line by line.</summary>
        /// <param name="file">The path where the file is located.</param>
        /// <returns>A "List of strings", which is crapped of text document line by line.</returns>
        public static List<string> Read(string file)
        {
            var fileText = new List<string>();

            try
            {
                string line;
                var sr = new StreamReader(file);

                while ((line = sr.ReadLine()) != null)
                {
                    fileText.Add(line);
                }

                sr.Close();
            }
            catch (Exception ex)
            {
                DialogService.Exception(ex, DialogService.ExceptionType.FileAcces);
            }

            return fileText;
        }

        /// <summary>Writes a text document line by line.</summary>
        /// <param name="file">The file path where the file to write is located.</param>
        /// <param name="toWrite">A "List of strings", to write a text document line by line.</param>
        public static void Write(string file, List<string> toWrite)
        {
            try
            {
                var sw = File.CreateText(file);
                toWrite.ForEach(sw.WriteLine);
                sw.Close();
            }
            catch (Exception ex)
            {
                DialogService.Exception(ex, DialogService.ExceptionType.FileAcces);
            }
        }
    }
}