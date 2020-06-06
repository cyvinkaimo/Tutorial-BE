namespace Yousource.Infrastructure.Extensions
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Newtonsoft.Json;

    public static class DataReaderExtensions
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public static T AsArray<T>(this SqlDataReader reader) where T : IEnumerable
        {
            var dictionary = Serialize(reader);
            var json = JsonConvert.SerializeObject(dictionary);
            var result = JsonConvert.DeserializeObject<T>(json, Settings);
            return result;
        }

        public static T As<T>(this SqlDataReader reader) where T : class
        {
            var dictionary = Serialize(reader).FirstOrDefault();
            var json = JsonConvert.SerializeObject(dictionary);
            var result = JsonConvert.DeserializeObject<T>(json, Settings);
            return result;
        }

        private static IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                cols.Add(reader.GetName(i));
            }

            while (reader.Read())
            {
                results.Add(SerializeRow(cols, reader));
            }

            return results;
        }

        private static Dictionary<string, object> SerializeRow(IEnumerable<string> cols, SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
            {
                result.Add(col, reader[col]);
            }

            return result;
        }
    }
}
