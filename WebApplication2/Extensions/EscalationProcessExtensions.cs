using CRM.Domain.Enums.CustomerService;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CRM.WebApp.Extensions
{
    public static class EscalationProcessExtensions
    {
        public static string GetDisplayName(this EscalationProcess value)
        {
            var field = value.GetType().GetField(value.ToString());
            return field?.GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
        }

        public static string GetDescription(this EscalationProcess value)
        {
            var field = value.GetType().GetField(value.ToString());
            return field?.GetCustomAttribute<DisplayAttribute>()?.Description ?? string.Empty;
        }

        public static string GetShortName(this EscalationProcess value)
        {
            var field = value.GetType().GetField(value.ToString());
            return field?.GetCustomAttribute<DisplayAttribute>()?.ShortName ?? value.ToString();
        }

        public static int GetOrder(this EscalationProcess value)
        {
            var field = value.GetType().GetField(value.ToString());
            return field?.GetCustomAttribute<DisplayAttribute>()?.GetOrder() ?? 99;
        }

        public static EscalationProcess GetDefault()
        {
            return EscalationProcess.Standard;
        }

        public static List<EscalationProcess> GetPrioritizedList()
        {
            return Enum.GetValues(typeof(EscalationProcess))
                      .Cast<EscalationProcess>()
                      .OrderBy(e => e.GetOrder())
                      .ToList();
        }
    }
}
