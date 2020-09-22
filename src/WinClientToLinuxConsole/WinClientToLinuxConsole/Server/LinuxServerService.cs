using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinClientToLinuxConsole.Repository;

namespace WinClientToLinuxConsole.Server
{
    public class LinuxServerService
    {
        private ServerFactory serverFactory;
        private LinuxServer aServer;

        public LinuxServerService(string pathToFile)
        {
            serverFactory = new ServerFactory(new XmlFileRepository(pathToFile));
        }
        #region simple commands

        public string RunCommandWithResultSync(string serverId, string command)
        {
            aServer = serverFactory.GetServer(serverId);
            return aServer.RunCommandSync(command);
        }

        public void GetFileBySftpSync(string serverId, string source, string destination)
        {
            aServer = serverFactory.GetServer(serverId);
            aServer.GetFileBySftpSync(source, destination);
        }
        #endregion

    }
}
