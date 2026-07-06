using System.Net;
using System.Net.Sockets;

namespace LrwLib.Server
{
    public delegate void ServerPacket(int id, string data);
    public static class ServerHelper
    {
        
        public static string GetCurrentIP()
        {
            foreach (IPAddress address in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (address.AddressFamily == AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(address))
                {
                    return $"{address}";
                }
            }
            return "";
        }

        public static bool CheckPortRange(int port)
        {
             return 2000 < port && port < 40000;
        }
        
    }
}