using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CRM.WebApp.Extensions
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<string> GetAllErrors(this ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        }
    }
}
