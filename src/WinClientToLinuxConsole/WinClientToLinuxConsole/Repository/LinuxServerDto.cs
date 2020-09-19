using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinClientToLinuxConsole.Repository
{
    public class LinuxServerDto : ILinuxServerDto
    {
        public string ServerId { get; set; }
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
