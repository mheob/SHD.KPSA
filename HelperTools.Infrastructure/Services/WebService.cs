namespace HelperTools.Infrastructure.Services
{
    using System;
    using System.Net;

    /// <summary>The WebService.</summary>
    public static class WebService
    {
        /// <summary>Check the exists of the file on server.</summary>
        /// <param name="location">The location.</param>
        /// <returns><c>true</c> if the file exists on server; otherwise, <c>false</c>.</returns>
        public static bool ExistsOnServer(Uri location)
        {
            try
            {
                HttpWebResponse resp = (HttpWebResponse) ((HttpWebRequest) WebRequest.Create(location.AbsoluteUri)).GetResponse();
                resp.Close();

                return resp.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}