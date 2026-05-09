namespace CRM.WebApp.ViewModels.Automation
{
    public class AutomationRunHistoryViewModel
    {
        public List<RunRecord> RecentRuns { get; set; }

        public class RunRecord
        {
            public DateTime RunDate { get; set; }
            public int ContactsProcessed { get; set; }
            public int EmailsSent { get; set; }
            public int StatusChanges { get; set; }
            public int CampaignAdditions { get; set; }
        }
    }
}
