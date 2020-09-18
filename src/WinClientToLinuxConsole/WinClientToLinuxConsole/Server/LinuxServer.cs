﻿using System;
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
        public string ServerId { get; set; }

        public string Host { get; set; }

        public  ConnectionParameters connectionParameters { get; set; }

        public string RunCommandSync(string command)
        {
            SshClient sshclient = new SshClient(Host, connectionParameters.User, connectionParameters.Password);
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
            SftpClient sftp = new SftpClient(Host, connectionParameters.User, connectionParameters.Password);
            sftp.Connect();
            using (Stream fileStream = File.OpenWrite(destination))
            {
                sftp.DownloadFile(source, fileStream);
            }
        }
    }
}