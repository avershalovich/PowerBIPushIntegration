using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Events;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.PowerBIIntegration.Models;
using Sitecore.Shell.Framework.Commands;

namespace Sitecore.PowerBIIntegration
{
    public class PowerBISyncCommand : Command
    {
        private static Database _database;

        protected static Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = Sitecore.Configuration.Factory.GetDatabase("master");
                }
                return _database;
            }
        }

        public override void Execute(CommandContext context)
        {
            try
            {
                // /sitecore/system/Marketing Control Panel/Campaigns
                var campaignsRoot = Database.GetItem(new ID("{EC095310-746F-4C1B-A73F-941863564DC2}"));
                var campaigns = campaignsRoot.Axes.GetDescendants()
                    .Where(x => x.TemplateID == new ID("{94FD1606-139E-46EE-86FF-BC5BF3C79804}"))
                    .Select(x => new Definition() {
                        Id = x.ID.Guid.ToString("D"),
                        Name = x.Name })
                    .ToList();

                // /sitecore/system/Marketing Control Panel/Taxonomies/Channel
                var channelsRoot = Database.GetItem(new ID("{F575D5E6-74DE-49B6-A866-E2256D213D83}"));
                var channels = channelsRoot.Axes.GetDescendants()
                    .Where(x => x.TemplateID == new ID("{3B4FDE65-16A8-491D-BF15-99CE83CF3506}"))
                    .Select(x => new Definition() {
                        Id = x.ID.Guid.ToString("D"),
                        Name = x.Name })
                    .ToList();

                // /sitecore/system/Settings/Analytics/Page Events
                var pageEventsRoot = Database.GetItem(new ID("{633273C1-02A5-4EBC-9B82-BD1A7C684FEA}"));
                var pageEvents = pageEventsRoot.Axes.GetDescendants()
                    .Where(x => x.TemplateID == new ID("{059CFBDF-49FC-4F14-A4E5-B63E1E1AFB1E}"))
                    .Select(x => new Definition()
                    {
                        Id = x.ID.Guid.ToString("D"),
                        Name = x.Name
                    })
                    .ToList();

                // /sitecore/system/Marketing Control Panel/Goals
                var goalsRoot = Database.GetItem(new ID("{0CB97A9F-CAFB-42A0-8BE1-89AB9AE32BD9}"));
                var goals = goalsRoot.Axes.GetDescendants()
                    .Where(x => x.TemplateID == new ID("{475E9026-333F-432D-A4DC-52E03B75CB6B}"))
                    .Select(x => new Definition()
                    {
                        Id = x.ID.Guid.ToString("D"),
                        Name = x.Name
                    })
                    .ToList();

                // /sitecore/system/Marketing Control Panel/Events
                var eventsRoot = Database.GetItem(new ID("{A65E2D0F-27E1-49AD-B332-076745A74ED6}"));
                var events = eventsRoot.Axes.GetDescendants()
                    .Where(x => x.TemplateID == new ID("{A386C5F2-F7F1-4980-AEDE-72A5B84B7F45}"))
                    .Select(x => new Definition()
                    {
                        Id = x.ID.Guid.ToString("D"),
                        Name = x.Name
                    })
                    .ToList();

                // /sitecore/system/Marketing Control Panel/Outcomes
                var outcomesRoot = Database.GetItem(new ID("{062A1E69-0BF6-4D6D-AC4F-C11D0F7DC1E1}"));
                var outcomes = outcomesRoot.Axes.GetDescendants()
                    .Where(x => x.TemplateID == new ID("{EE43C2F0-6277-4144-B144-8CA2CEFCCF12}"))
                    .Select(x => new Definition()
                    {
                        Id = x.ID.Guid.ToString("D"),
                        Name = x.Name
                    })
                    .ToList();

                // /sitecore/system/Marketing Control Panel/Profiles
                var profilesRoot = Database.GetItem(new ID("{12BD7E35-437B-449C-B931-23CFA12C03D8}"));

                var profiles = profilesRoot.Axes.GetDescendants()
                    .Where(x => x.TemplateID == new ID("{8E0C1738-3591-4C60-8151-54ABCC9807D1}"))
                    .Select(x => new Definition()
                    {
                        Id = x.ID.Guid.ToString("D"),
                        Name = x.Name
                    })
                    .ToList();

                var patternCards = profilesRoot.Axes.GetDescendants()
                    .Where(x => x.TemplateID == new ID("{4A6A7E36-2481-438F-A9BA-0453ECC638FA}"))
                    .Select(x => new Definition()
                    {
                        Id = x.ID.Guid.ToString("D"),
                        Name = x.Name
                    })
                    .ToList();

                //Deploying marketing definitions to PowerBI dataset
                using (var adapter = new PowerBIAdapter())
                {
                    adapter.ClearTable("ChannelDefinition");
                    adapter.PushRows(channels, "ChannelDefinition");
                    adapter.ClearTable("CampaignDefinition");
                    adapter.PushRows(campaigns, "CampaignDefinition");
                    adapter.ClearTable("GoalDefinition");
                    adapter.PushRows(pageEvents, "GoalDefinition");
                    adapter.PushRows(goals, "GoalDefinition");
                    adapter.PushRows(events, "GoalDefinition");
                    adapter.PushRows(outcomes, "GoalDefinition");
                    adapter.ClearTable("ProfileDefinition");
                    adapter.PushRows(profiles, "ProfileDefinition");
                    adapter.ClearTable("MatchedPatternDefinition");
                    adapter.PushRows(patternCards, "MatchedPatternDefinition");
                }

                Sitecore.Context.ClientPage.ClientResponse.Alert("Done");
            }
            catch (Exception e)
            {
                Sitecore.Diagnostics.Log.Error(e.Message, e, this);
            }
        }
    }
}
