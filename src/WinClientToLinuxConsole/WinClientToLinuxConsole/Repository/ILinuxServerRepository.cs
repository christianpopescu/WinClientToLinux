using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinClientToLinuxConsole.Repository
{
    public interface ILinuxServerRepository
    {

        ILinuxServerDto GetServerById(string serverId);
        IList<ILinuxServerDto> GetServerList();
    }
}
