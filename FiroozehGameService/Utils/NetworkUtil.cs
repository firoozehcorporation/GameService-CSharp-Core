using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace FiroozehGameService.Utils
{
    internal static class NetworkUtil
    {
        
        internal static bool IsConnected()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204")) 
                    return true; 
            }
            catch
            {
                return false;
            }
        }
     
        internal static string GetMacAddress()
        {
           return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where( nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback )
                .Select( nic => nic.GetPhysicalAddress().ToString() )
                .FirstOrDefault();
        }
    }
}