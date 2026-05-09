using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace CRM.WebApp.Localization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly IDistributedCache _cache;
        private readonly JsonSerializer _serializer = new();

        public JsonStringLocalizer(IDistributedCache cache)
        {
            _cache = cache;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actualValue = this[name];
                return !actualValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualValue.Value, arguments))
                    : actualValue;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
            //var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName}/{filename}.json";

            //using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            //using StreamReader streamReader = new(stream);
            //using JsonTextReader reader = new(streamReader);

            //while (reader.Read())
            //{
            //    if (reader.TokenType != JsonToken.PropertyName)
            //        continue;

            //    var key = reader.Value as string;
            //    reader.Read();
            //    var value = _serializer.Deserialize<string>(reader);
            //    yield return new LocalizedString(key, value);
            //}
        }

        private string GetString(string key)
        {
            var filename = "Main";
            if (key.Contains("."))
            {
                filename = key.Substring(0, key.IndexOf("."));
                key = key.Substring(key.IndexOf(".") + 1);
            }

            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName}/{filename}.json";
            var fullFilePath = Path.GetFullPath(filePath);

            if (File.Exists(fullFilePath))
            {
                var cacheKey = $"locale_{Thread.CurrentThread.CurrentCulture.Name}_{filename}_{key}";
                var cacheValue = _cache.GetString(cacheKey);

                if (!string.IsNullOrEmpty(cacheValue))
                    return cacheValue;

                var result = GetValueFromJSON(key, fullFilePath);

                if (!string.IsNullOrEmpty(result))
                    _cache.SetString(cacheKey, result);

                return result;
            }

            return key;
        }

        private string GetValueFromJSON(string propertyName, string filePath)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filePath))
                return string.Empty;

            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(stream);
            using JsonTextReader reader = new(streamReader);

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName
                    && reader.Value as string == propertyName)
                {
                    reader.Read();
                    return _serializer.Deserialize<string>(reader);
                }
            }

            return propertyName;
        }
    }
}
