namespace CRM.WebApp.DTOs.CustomerService
{
    public class SLAMetricSummaryDto
    {
        public double AverageHours { get; set; }
        public double MedianHours { get; set; }
        public double MinimumHours { get; set; }
        public double MaximumHours { get; set; }
        public double Percentile90 { get; set; }
        public int TotalCases { get; set; }

        // Formatted versions for display
        public string AverageDisplay => $"{AverageHours:0.0} hours";
        public string MedianDisplay => $"{MedianHours:0.0} hours";
        public string MinimumDisplay => $"{MinimumHours:0.0} hours";
        public string MaximumDisplay => $"{MaximumHours:0.0} hours";
        public string Percentile90Display => $"{Percentile90:0.0} hours";
    }
}
