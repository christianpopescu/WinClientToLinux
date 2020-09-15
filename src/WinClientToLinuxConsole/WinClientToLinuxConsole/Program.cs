using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using WinClientToLinuxConsole.Server;

namespace WinClientToLinuxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // TestMultipleCommandOnSsh();

            Console.WriteLine(TestRunWithResult("ls -la"));

            Console.ReadKey();
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

        static void TestMultipleCommandOnSsh()
        {
            SshClient sshclient = new SshClient("192.168.1.52", "christian", "password");
            sshclient.Connect();
            SshCommand sc = sshclient.CreateCommand("source ~/.bash_profile 2> /dev/null; ls");
            sc.Execute();
            string answer = sc.Result;
            Console.WriteLine(answer);
        }

        static string TestRunWithResult(string command)
        {
            LinuxServer ls = new LinuxServer() {Host = "192.168.5.128" ,
                connectionParameters = new ConnectionParameters() {User = "christian", Password = "Christian1967"}};
            string answer = ls.RunCommandSync("source ~/.profile 2> /dev/null;" + command);
            return answer;
        }

        static string StartRunningWithResult(string command)
        {
            SshClient sshclient = new SshClient("server", "user", "passs");
            sshclient.Connect();
            SshCommand sc = sshclient.CreateCommand("source ~/.profile 2> /dev/null;" + command);
            sc.Execute();
            string answer = sc.Result;
            sshclient.Disconnect();
            return answer;
        }



    }
}
