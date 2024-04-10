using BasicAuthHandler.Models;
using BasicAuthHandler.Properties;
using Newtonsoft.Json;
using System.Net;
using System.Text;


namespace BasicAuthHandler.Global
{
    public static class GlobalVariables
    {
        /// <summary>
        /// JSON string in the AcceptUserKeys file
        /// </summary>
        private static readonly string _stringJson = Encoding.UTF8.GetString(Resources.AcceptUserKeys);

        /// <summary>
        /// List of all users and keys authorized to access the API using basic auth.
        /// </summary>
        public static AcceptUserKeysModel[] AcceptUserKeys { get; set; } =
            JsonConvert.DeserializeObject<AcceptUserKeysModel[]>(_stringJson)!;

        /// <summary>
        /// <code xml:space="preserve">
        /// List of all clients authorized to access the API using basic auth.
        ///
        /// Please note that this dictionary exists solely to illustrate how you can retrieve information 
        /// about the client accessing the API. Such implementation is not recommended for handling a large 
        /// number of requests, for instance, it is purely illustrative.
        /// </code>
        /// </summary>
        public static Dictionary<IPAddress, ClientAcceptModel> ClientsAccept { get; set; } = [];
    }
}
