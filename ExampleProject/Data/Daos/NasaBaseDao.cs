using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ExampleProject.Data.Models;
using ExampleProject.Handlers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExampleProject.Data.Daos
{
    // Having a base class may seem like overkill with only one thing inheriting
    // from it. If we wanted to play with more of the NASA APIs we could put
    // other potential core methods here. We'd also ensure an accurate base-url,
    // and consistent usage methods. Implementing at least one basic NASA query
    // is as simple as passing the correct path off of the root URL.
    public class NasaBaseDao<T1>
        where T1 : NasaBaseModel
    {
        // This should really never change under any circumstances.
        private static readonly string NasaApiBaseUrl = "https://api.nasa.gov/";
        private string ContentPath { get; set; }
        // TODO: Move to environment variable:
        private string ApiKey = "bWf8XmfjrSrBghkmWjmMuZRNPkt5DO9PCxynEMKM";
        protected string Url => $"{ NasaApiBaseUrl}{ContentPath}?api_key={ApiKey}";

        public NasaBaseDao(string contentPath)
        {
            ContentPath = contentPath;
        }

        // I like the idea of being able to construct complex query strings, so
        // this consumes a dictionary. It is the responsibility of the
        // controller to ensure all query parameters are strings.
        public IEnumerable<T1> Get(Dictionary<string, string> queryParameters)
        {
            // Default to returning null, we'll populate this after the web
            // request has been deserialized.
            IEnumerable<T1> results = null;

            // Construct the query string
            // start with a builder as it's mutable and strings are not.
            StringBuilder queryBuilder = new StringBuilder();
            // Now loop through the parameters
            foreach (var parameter in queryParameters)
            {
                // Thought about this one quite a bit, decided that I'd rather
                // not search on blank parameters. Several attempts sending
                // empty strings to NASA's APIs resulted in empty result-sets.
                if (!String.IsNullOrWhiteSpace(parameter.Value))
                    queryBuilder.Append($"&{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value)}");
            }

            // Try/catch to allow us to log any errors that occur during the 
            // request or data stream.
            try
            {
                // Unpleasant .NET WebRequest code
                string queryString = $"{Url}{queryBuilder}";
                WebRequest request = WebRequest.Create(queryString);

                // Using blocks to make sure all these IDisposables are handled
                using (WebResponse response = request.GetResponse())
                    using (Stream responseStream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(responseStream))
                {
                    string rawResults = reader.ReadToEnd();
                    results = JsonConvert.DeserializeObject<JsonResultSetModel<T1>>(rawResults, new JsonSerializerSettings
                    {
                        // Public APIs aren't always consistent, sometimes they
                        // Send back weird stuff. In this case, I'm going to
                        // simply log the error and ignore the fields that fail
                        // this way we can still examine as much of the data
                        // that's valid
                        Error = JsonErrorHandler.HandleDeserializationError
                    }).Results;
                    reader.Close();
                    responseStream.Close();
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                // I was taught in the past that we should never do any routing
                // based on exceptions. I would output exceptions to a log here
                // or rethrow them as more "developer" friendly, categorized
                // exceptions. In this case, I'll just write it to console, and
                // allow the program's execution to continue as there is null
                // result set handling in the controller.
                // TODO: Implement logging
                Console.WriteLine(ex.Message);
            }

            return results;
        }
    }
}
