namespace Sitecore.XConnect.ServicePlugins.InteractionsTracker.Models
{
    public class WebVisitFacet
    {
        public string InteractionId { get; set; }
        public string BrowserMajorName { get; set; }
        public string BrowserMinorName { get; set; }
        public string BrowserVersion { get; set; }
        public string Language { get; set; }
        public string MajorVersion { get; set; }
        public string MinorVersion { get; set; }
        public string Name { get; set; }
        public string Referrer { get; set; }
        public string ReferringSite { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public string SearchKeywords { get; set; }
        public string SiteName { get; set; }
    }
}
