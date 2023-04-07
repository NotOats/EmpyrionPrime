using System.Diagnostics;

namespace EmpyrionPrime.RemoteClient.Epm
{
    public class EpmClientSettings
    {
        public string IPAddress { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 12345;
        public int ClientId { get; set; } = Process.GetCurrentProcess().Id;
    }
}
