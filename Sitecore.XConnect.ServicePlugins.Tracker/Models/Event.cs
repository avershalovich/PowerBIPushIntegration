using System;

namespace Sitecore.XConnect.ServicePlugins.InteractionsTracker.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string InteractionId { get; set; }
        public string ItemId { get; set; }
        public string Text { get; set; }
        public string DefenitionId { get; set; }
        public int Duration { get; set; }
        public DateTime Timestamp { get; set; }
        public int EngagementValue { get; set; }
    }
}
