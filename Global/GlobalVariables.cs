using BasicAuthHandler.Models;
using BasicAuthHandler.Properties;
using Newtonsoft.Json;
using System.Text;


namespace BasicAuthHandler.Global
{
    public static class GlobalVariables
    {
        private static string _stringJson = Encoding.UTF8.GetString(Resources.AcceptUserKeys);

        public static AcceptUserKeysModel[] AcceptUserKeys { get; set; } =
            JsonConvert.DeserializeObject<AcceptUserKeysModel[]>(_stringJson)!;
    }
}
