using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Marketplace.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            return json;
        }
    }
}