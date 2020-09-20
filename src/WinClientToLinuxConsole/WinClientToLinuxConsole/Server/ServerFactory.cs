using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinClientToLinuxConsole.Repository;

namespace WinClientToLinuxConsole.Server
{
    public class ServerFactory
    {
        protected Dictionary<string, LinuxServer> AvailableServers = new Dictionary<string, LinuxServer>();

        public LinuxServer GetServer(string serverId)
        {
            return AvailableServers[serverId];
        }

        public ServerFactory(ILinuxServerRepository serverRepository)
        {

            Console.WriteLine(serverRepository.GetServerList().Count());
            IList<LinuxServerDto> serverDtoList = serverRepository.GetServerList();
            foreach (var srv in serverDtoList)
            {
                AvailableServers.Add(srv.ServerId,
                    new LinuxServer(srv.ServerId, srv.Host, new ConnectionParameters(srv.User, srv.Password)));
            }
        }
    }
}
