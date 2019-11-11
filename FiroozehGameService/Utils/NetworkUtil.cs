using System.Linq;
using System.Net.NetworkInformation;

namespace FiroozehGameService.Utils
{
    public class NetworkUtil
    {
        public static string GetMacAddress()
        {
           return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where( nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback )
                .Select( nic => nic.GetPhysicalAddress().ToString() )
                .FirstOrDefault();
        }
    }
}