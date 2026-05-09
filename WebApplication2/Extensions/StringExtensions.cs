namespace CRM.WebApp.Extensions
{
    public static class StringExtensions
    {
        public static string Reverse(this string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string RemoveSpecialCharacters(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Trim()
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace("\t", "").Replace(" ", "");
        }

        public static int WordCount(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;
            return input.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static string CapitalizeFirstLetter(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                             .FirstOrDefault() as System.ComponentModel.DescriptionAttribute;
            return attr?.Description ?? value.ToString();
        }

        public static TResult? IfNotNull<T, TResult>(this T? obj, Func<T, TResult> func) where T : class
    => obj != null ? func(obj) : default;

        public static bool IsBetween(this DateTime date, DateTime start, DateTime end) => date >= start && date <= end;

    }
}
