using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MyRedditApp.Helpers
{
    public static class Utilities
    {
        /// <summary>
        /// Deserialize the specified jsonString.
        /// </summary>
        /// <returns>The deserialize.</returns>
        /// <param name="jsonString">Json string.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T Deserialize<T>(string jsonString)
        {

            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }

        }
    }
}
