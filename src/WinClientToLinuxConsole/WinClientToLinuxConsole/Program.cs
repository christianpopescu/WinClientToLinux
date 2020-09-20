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
            // TestMultipleCommandOnSsh();

            //Console.WriteLine(TestRunWithResult("ls -la"));
            /*          Console.WriteLine(ShowFileContent(@"/home/christian/ccp_main/testfile.txt"));
                      GetFileBySftpSync(@"/home/christian/ccp_main/testfile.txt", @"E:\Temp\ToDelete\file1.txt");
                      GetFileBySftpSync(@"/home/christian/ccp_main/anotherfile.txt", @"E:\Temp\ToDelete\file2.txt");
            */

             //TestXmlRepository();
//             TestServerFactory();
             FinalCommandTest();
            Console.ReadKey();
        }

        static void FinalCommandTest()
        {
            LinuxServerService lss =
                new LinuxServerService(@"E:\ccp_vhdd_main\workspace\WinClientToLinux\wrkspace\LinuxServerList.xml");
            Console.WriteLine(lss.RunCommandWithResultSync("Server01", "ls -la"));
        }
        
        static void TestXmlRepository()
        {
            ILinuxServerRepository ServerRepository = 
                new XmlFileRepository(@"E:\ccp_vhdd_main\workspace\WinClientToLinux\wrkspace\LinuxServerList.xml");
            LinuxServerDto lsDto = ServerRepository.GetServerById("Server01");

            LinuxServer ls = new LinuxServer(lsDto.ServerId, lsDto.Host, new ConnectionParameters(lsDto.User, lsDto.Password));
        }

        static void TestServerFactory()
        {
            ServerFactory sf =
                new ServerFactory(
                    new XmlFileRepository(
                        @"E:\ccp_vhdd_main\workspace\WinClientToLinux\wrkspace\LinuxServerList.xml"));
            LinuxServer ls = sf.GetServer("Server01");
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
            LinuxServer ls = new LinuxServer("server1", "192.168.5.128" ,
                new ConnectionParameters() {User = "christian", Password = "password"});
            string answer = ls.RunCommandSync("source ~/.profile 2> /dev/null;" + command);
            return answer;
        }

        static string ShowFileContent(string filename)
        {
            LinuxServer ls =  new LinuxServer("server1", "192.168.5.128",
                new ConnectionParameters() { User = "christian", Password = "password" });
            string answer = ls.GetFileContentSync(filename);
            return answer;
        }

        static void GetFileBySftpSync(string source, string destination)
        {
            LinuxServer ls = new LinuxServer("server1", "192.168.5.128",
                new ConnectionParameters() { User = "christian", Password = "password" });
            ls.GetFileBySftpSync(source,destination);
            
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
