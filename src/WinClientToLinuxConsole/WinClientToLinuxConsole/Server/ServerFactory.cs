using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinClientToLinuxConsole.Server
{
    public class ServerFactory
    {
        protected Dictionary<string, LinuxServer> AvailableServers = new Dictionary<string, LinuxServer>();

        public LinuxServer GetServer(string serverId)
        {
            return AvailableServers[serverId];
        }
    }
}
