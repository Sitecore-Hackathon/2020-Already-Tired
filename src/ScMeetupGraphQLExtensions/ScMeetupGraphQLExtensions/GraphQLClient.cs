using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScMeetupGraphQLExtensions.Models;
using Sitecore.Configuration;

namespace ScMeetupGraphQLExtensions
{
    public class GraphQLClient
    {
        public T Execute<T>(string queryName, dynamic variables)
        {
            string query = ReadQueryFromResource(queryName);

            dynamic request = new
            {
                query,
                variables
            };

            string serialized = JsonConvert.SerializeObject(request);

            using (var wc = new WebClient())
            {
                string hasuraAdminSecret = Settings.GetSetting("ScMeetup.HasuraSecret");
                string hasuraEndpoint = Settings.GetSetting("ScMeetup.HasuraEndpoint");

                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                wc.Headers.Add("x-hasura-admin-secret", hasuraAdminSecret);
				
                string response = wc.UploadString(hasuraEndpoint, serialized);

                T responseObject = JsonConvert.DeserializeObject<T>(response);

                return responseObject;
            }
        }

        private string ReadQueryFromResource(string queryName)
        {
            Assembly queryAssembly = typeof(GraphQLClient).Assembly;

            string resourceName = queryAssembly.GetManifestResourceNames()
                .FirstOrDefault(resource =>
                    resource.IndexOf(queryName, StringComparison.InvariantCultureIgnoreCase) > -1);

            if (string.IsNullOrEmpty(resourceName))
            {
                throw new ArgumentException("Unable to locate query.");
            }

            string query;

            using (Stream stream = queryAssembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new ArgumentException("Unable to stream resource.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    query = reader.ReadToEnd();
                }
            }

            return query;
        }
    }
}
