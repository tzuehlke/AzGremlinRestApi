using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using System.Net;
using System.Text;


/* hint 1: call this function
Call with POST and as body {query:"g.V().limit(3)"}
*/

/* hint 2: how to use gremlin console
start console:              ./gremlin.sh
connect to Azure DB:        :remote connect tinkerpop.server conf/remote-secure.yaml
send query:                 :> g.V()
exit console:               :exit
*/

namespace Company.Function
{
    public static class gremgraphapi
    {   
        private static string hostname = Environment.GetEnvironmentVariable("GRAPHDBHOSTNAME"); 
        private static int port = 443;
        private static string authKey = Environment.GetEnvironmentVariable("GRAPHDBKEY");
        private static string database = Environment.GetEnvironmentVariable("GRAPHDBNAME");
        private static string collection = Environment.GetEnvironmentVariable("GRAPHDBCOLL");

        [FunctionName("mgGremlinGraph")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string query="";
            if (req.Method.ToLower() == "post")
            {
               query = JObject.Parse(await new StreamReader(req.Body).ReadToEndAsync())?["query"]?.ToString();
            }

            if (String.IsNullOrEmpty(query) || (query.Contains("add")))
                return new BadRequestObjectResult("Please pass a read query in the POST body");

            log.LogInformation($"Query: {query}");
            log.LogInformation($"hostname: {hostname}");
            string connection = "/dbs/" + database + "/colls/" + collection;
            log.LogInformation($"username: {connection}");
            log.LogInformation($"key: {authKey.Substring(0, 4)}...{authKey.Substring(authKey.Length-4, 4)}");
            string jRes="";
            var gremlinServer = new GremlinServer(hostname, port, enableSsl: true,
                                                username: connection,
                                                password: authKey);
            using (var gremlinClient = new GremlinClient(gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                // Create async task to execute the Gremlin query.
                var resultSet = gremlinClient.SubmitAsync<dynamic>(query).Result;
                jRes = JsonConvert.SerializeObject(resultSet).ToString();
            }
            return new OkObjectResult(jRes);
        }
    }
}
