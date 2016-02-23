namespace SHD.KPSA.Tools.Utils
{
    public interface IPage
    {
        IPageViewModel PageViewModel { get; set; }
        string PageTitle { get; set; }
    }
}