namespace Sitecore.XConnect.ServicePlugins.InteractionsTracker.Models
{
    public class ProfileScoresFacet
    {
        public string InteractionId { get; set; }
        public string ScoreId { get; set; }
        public string MatchedPatternId { get; set; }
        public string ProfileDefinitionId { get; set; }
        public int ScoreCount { get; set; }
        public double Score { get; set; }
        public string ValueId { get; set; }
        public double Value { get; set; }
    }

}
