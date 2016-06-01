namespace HelperTools.Infrastructure.Constants
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>The AssemblyInfo.</summary>
    public static class AssemblyInfo
    {
        /// <summary>The build date of the assembly.</summary>
        public static DateTime BuildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
    }
}