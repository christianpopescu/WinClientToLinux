using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinClientToLinuxConsole.Repository
{
    interface ILinuxServerDto
    {
        string ServerId { get; set; }
        string Host { get; set; }
        string User { get; set; }
        string Password { get; set; }
    }
}
