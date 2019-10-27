using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace WinClientToLinuxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void TestSftp()
        {
            SftpClient sftp = new SftpClient("localhost", "user", "password");
            sftp.Connect();
            using (Stream fileStream = File.OpenWrite(@"c:\temp\testenv.txt"))
            {
                sftp.DownloadFile("/data/directory/config/file.ini", fileStream);
            }
        }


        static void TestSshStream()
        {
            SshClient sshclient = new SshClient("localhost", "user", "password");
            sshclient.Connect();

            ShellStream ss = sshclient.CreateShellStream("dumb", 0, 0, 0, 0, 0);
            ss.WriteLine("pwd");
            ss.WriteLine("cat  test.ini");

            string line;
            while ((line = ss.ReadLine(TimeSpan.FromSeconds(2))) != null)
            {
                Console.WriteLine(line);
                // if a termination pattern is known, check it here and break to exit immediately
            }
            // ...
            ss.Close();

            sshclient.Disconnect();

        }

    }
}
