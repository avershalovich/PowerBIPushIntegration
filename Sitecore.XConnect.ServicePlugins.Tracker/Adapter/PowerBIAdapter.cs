using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;

namespace Sitecore.XConnect.ServicePlugins.InteractionsTracker
{
    public class PowerBIAdapter : IDisposable
    {
        private readonly string clientID;
        private readonly string groupID;
        private readonly string datasetID;
        private readonly string token;

        public PowerBIAdapter()
        {
            clientID = ConfigurationManager.AppSettings["PowerBIClientID"];
            groupID = ConfigurationManager.AppSettings["PowerBIGroupID"];
            datasetID = ConfigurationManager.AppSettings["PowerBIDatasetID"];

            token = GetToken();
        }

        private string GetToken()
        {
            // TODO: Install-Package Microsoft.IdentityModel.Clients.ActiveDirectory -Version 2.21.301221612
            // and add using Microsoft.IdentityModel.Clients.ActiveDirectory

            //Resource Uri for Power BI API
            string resourceUri = "https://analysis.windows.net/powerbi/api";

            //OAuth2 authority Uri
            string authorityUri = "https://login.microsoftonline.com/common/";

            //Get access token:
            // To call a Power BI REST operation, create an instance of AuthenticationContext and call AcquireToken
            // AuthenticationContext is part of the Active Directory Authentication Library NuGet package
            // To install the Active Directory Authentication Library NuGet package in Visual Studio,
            //  run "Install-Package Microsoft.IdentityModel.Clients.ActiveDirectory" from the nuget Package Manager Console.

            // AcquireToken will acquire an Azure access token
            // Call AcquireToken to get an Azure token from Azure Active Directory token issuance endpoint
            AuthenticationContext authContext = new AuthenticationContext(authorityUri);

            var userCredential = new UserPasswordCredential(ConfigurationManager.AppSettings["PowerBIUserEmail"], ConfigurationManager.AppSettings["PowerBIUserPassword"]);

            Task<AuthenticationResult> task = authContext.AcquireTokenAsync(resourceUri, clientID, userCredential);

            task.Wait();


            string token = task.Result.AccessToken;

            Console.WriteLine(token);
            Console.ReadLine();

            return token;
        }

        public void PushRow<T>(T data, string tableName)
        {
            var json = (JObject)JToken.FromObject(data);

            string rowsJson = "{\"rows\":[" + json + "]}";

            SendData(rowsJson, tableName);
        }

        public void PushRows<T>(IEnumerable<T> data, string tableName)
        {
            var json = (JArray)JToken.FromObject(data);

            string rowsJson = "{\"rows\":" + json + "}";

            SendData(rowsJson, tableName);
        }

        private void SendData(string json, string tableName)
        {

            string powerBIApiAddRowsUrl = String.Format("https://api.powerbi.com/v1.0/myorg/groups/{0}/datasets/{1}/tables/{2}/rows", groupID, datasetID, tableName);

            //POST web request to add rows.
            //Change request method to "POST"
            HttpWebRequest request = System.Net.WebRequest.Create(powerBIApiAddRowsUrl) as System.Net.HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentLength = 0;
            request.ContentType = "application/json";

            //Add token to the request header
            request.Headers.Add("Authorization", String.Format("Bearer {0}", token));

            //POST web request
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json.ToString());
            request.ContentLength = byteArray.Length;

            //Write JSON byte[] into a Stream
            using (Stream writer = request.GetRequestStream())
            {
                writer.Write(byteArray, 0, byteArray.Length);

                var response = (HttpWebResponse)request.GetResponse();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
