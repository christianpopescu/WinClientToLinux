using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinClientToLinuxConsole.Repository
{
    public interface ILinuxServerRepository
    {

        LinuxServerDto GetServerById(string serverId);
        IList<LinuxServerDto> GetServerList();
    }
}
