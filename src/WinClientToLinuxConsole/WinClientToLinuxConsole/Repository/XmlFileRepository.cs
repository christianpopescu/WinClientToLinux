using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WinClientToLinuxConsole.Repository
{
    public class XmlFileRepository<TDto> : ILinuxServerRepository where TDto :ILinuxServerDto, new()
    {
        protected List<TDto> listOfLinuxServers = new List<TDto>();
        protected Dictionary<string, ILinuxServerDto> dictLinuxServer = new Dictionary<string, ILinuxServerDto>();

        public XmlFileRepository(string pathToFile)
        {
            XDocument repositoryDocument = XDocument.Load(pathToFile);
            if (repositoryDocument.Root.Elements().Count() > 0)
            {
 /*               foreach (XElement xe in repositoryDocument.Root.Elements())
                {
                    Console.WriteLine(xe.Element("ServerId").Value);
                }
 */
                listOfLinuxServers = repositoryDocument.Root.Elements().Select(xelem =>
                    new TDto()
                    {
                        ServerId = xelem.Element("ServerId").Value,
                        Host = xelem.Element("Host").Value,
                        User = xelem.Element("User").Value,
                        Password = xelem.Element("Password").Value
                    }).ToList();

                foreach (var lls in listOfLinuxServers)
                {
                    dictLinuxServer.Add(lls.ServerId, lls);
                }
            }

        }
        
        public ILinuxServerDto GetServerById(string serverId)
        {
            return dictLinuxServer [serverId];
        }

        public IList<ILinuxServerDto> GetServerList()
        {
            return listOfLinuxServers as IList<ILinuxServerDto>;
        }
    }
}
