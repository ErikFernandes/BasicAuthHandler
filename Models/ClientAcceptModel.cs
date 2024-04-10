using System.Net;

namespace BasicAuthHandler.Models
{
    public class ClientAcceptModel
    {
        public IPAddress? IpAddress { get; set; }
        public int HitsAccept { get; set; }
        public DateTime LastAccessDate { get; set; } = DateTime.Now;
        public string Username { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public bool IsLocalhost => IpAddress!.Equals(IPAddress.IPv6Loopback);

    }
}
