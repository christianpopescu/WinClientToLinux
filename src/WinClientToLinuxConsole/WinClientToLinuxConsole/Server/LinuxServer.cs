using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace WinClientToLinuxConsole.Server
{
    public class LinuxServer
    {
        public string ServerId {  get; private set; }

        protected string Host { get; set; }

        protected  ConnectionParameters TheConnectionParameters { get; set; }

        public LinuxServer(string serverId, string host, ConnectionParameters theConnectionParameters)
        {
            this.ServerId = serverId;
            this.Host = host;
            this.TheConnectionParameters = theConnectionParameters;
        }

        public string RunCommandSync(string command)
        {
            SshClient sshclient = new SshClient(Host, TheConnectionParameters.User, TheConnectionParameters.Password);
            sshclient.Connect();
            SshCommand sc = sshclient.CreateCommand(command);
            sc.Execute();
            string answer = sc.Result;
            sshclient.Disconnect();
            return answer;
        }

        public string GetFileContentSync(string file)
        {
            return RunCommandSync("cat " + file);
        }

        public void GetFileBySftpSync(string source, string destination)
        {
            SftpClient sftp = new SftpClient(Host, TheConnectionParameters.User, TheConnectionParameters.Password);
            sftp.Connect();
            using (Stream fileStream = File.OpenWrite(destination))
            {
                sftp.DownloadFile(source, fileStream);
            }
        }
    }
}
