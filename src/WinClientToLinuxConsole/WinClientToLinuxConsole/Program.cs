using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using WinClientToLinuxConsole.Repository;
using WinClientToLinuxConsole.Server;

namespace WinClientToLinuxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
 
             FinalCommandTest();
            Console.ReadKey();
        }

        static void FinalCommandTest()
        {
            LinuxServerService lss =
                new LinuxServerService(@"E:\ccp_vhdd_main\workspace\WinClientToLinux\wrkspace\LinuxServerList.xml");
            Console.WriteLine(lss.RunCommandWithResultSync("Server01", "ls -la"));
        }
        
    }
}
