namespace SHD.KPSA.Tools.Application.Models
{
    using Utils;

    /// <summary>
    /// The model class to fill the menu button for the separate pages.
    /// </summary>
    public class Pages : IPage
    {
        public IPageViewModel PageViewModel { get; set; }
        public string PageTitle { get; set; }
    }
}