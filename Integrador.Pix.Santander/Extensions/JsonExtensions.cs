using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Pix.Santander.Extensions
{
    public static class JsonExtensions
    {
        public static TResult ToStringDeserialize<TResult>(this HttpResponseMessage response) where TResult : new()
        {
            var jsonResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var jsonData = jsonResult.ToDeserialize<TResult>();
            return jsonData;
        }

        public static TResult ToDeserialize<TResult>(this string value) where TResult : new()
        {
            var jsonData = Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(value);
            return jsonData;
        }

        public static string ToDateTimeToUTC(this string value)
        {
            var dateTime = Convert.ToDateTime(value);
            dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
            dateTime = System.DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            var utcValue = ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();
            return utcValue.ToString();
        }
    }
}
