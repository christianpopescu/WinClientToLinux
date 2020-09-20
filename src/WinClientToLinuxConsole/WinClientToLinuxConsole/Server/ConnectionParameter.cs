namespace WinClientToLinuxConsole
{
    public class ConnectionParameters
    {
        public string User { get; set; }
        public string Password { get; set; }

        public ConnectionParameters() { }

        public ConnectionParameters(string user, string password)
        {
            User = user;
            Password = password;
        }
    }
}