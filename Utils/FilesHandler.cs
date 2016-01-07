using System;
using System.Collections.Generic;
using System.IO;

namespace Utils
{
    public static class FilesHandler
    {
        /// <summary>
        /// Liest ein Textdokument Zeile für Zeile.
        /// </summary>
        /// <param name="file">Der Dateipfad.</param>
        /// <returns>Gibt eine Array-Liste des Textdokumentes Zeilenweise zurück.</returns>
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
                Dialogs.Exception(ex, Dialogs.ExceptionType.FileAcces);
            }

            return fileText;
        }

        /// <summary>
        /// Schreibt ein Textdokument Zeile für Zeile.
        /// </summary>
        /// <param name="file">Der Dateipfad, de neu zu schreibenden Datei.</param>
        /// <param name="toWrite">Array-Liste des Textdokumentes Zeilenweise.</param>
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
                Dialogs.Exception(ex, Dialogs.ExceptionType.FileAcces);
            }
        }
    }
}