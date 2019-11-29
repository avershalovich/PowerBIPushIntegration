using System;

namespace Sitecore.XConnect.ServicePlugins.InteractionsTracker.Models
{
    public class Interaction
    {
        public string Id { get; set; }
        public string ContactId { get; set; }
        public string CampaignId { get; set; }
        public string ChannelId { get; set; }
        public string VenueId { get; set; }
        public string ConcurrencyToken { get; set; }
        public int Duration { get; set; }
        public DateTime StartDataTime { get; set; }
        public DateTime EndDataTime { get; set; }
        public int EngagementValue { get; set; }
    }
}
