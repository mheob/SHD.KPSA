namespace HelperTools.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>The type of hash to create</summary>
    public enum HashType
    {
        /// <summary>The hash type MD5.</summary>
        Md5,

        /// <summary>The hash type SHA1.</summary>
        Sha1,

        /// <summary>The hash type SHA512.</summary>
        Sha512
    }

    /// <summary>Class used to generate hash sums of files.</summary>
    public static class HashService
    {
        /// <summary>Generate a hash sum of a file.</summary>
        /// <param name="filePath">The file to hash.</param>
        /// <param name="hashType">The Type of hash.</param>
        /// <returns>The computed hash.</returns>
        internal static string HashFile(string filePath, HashType hashType)
        {
            switch (hashType)
            {
                case HashType.Md5:
                    return MakeHashString(MD5.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                case HashType.Sha1:
                    return MakeHashString(SHA1.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                case HashType.Sha512:
                    return MakeHashString(SHA512.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(hashType), hashType, null);
            }
        }

        /// <summary>Converts "IEnumerable of byte" to string.</summary>
        /// <param name="hash">The hash to convert.</param>
        /// <returns>The hash as a string.</returns>
        private static string MakeHashString(IEnumerable<byte> hash)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2").ToLower());
            }

            return sb.ToString();
        }
    }
}