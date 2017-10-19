using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakfuRemake.Auth.Models
{
    public class Server
    {
        public enum CommunityID
        {
            FRENCH
        }
        public enum PropertyID
        {
            SERVER_ID = 420
        }
        //Server Configuration
        public int ID { get; private set; }
        public string Name { get; private set; }
        public CommunityID Community { get; private set; }
        public bool Locked { get; set; }
        public Dictionary<PropertyID, string> Propertys { get; private set; }

        //Proxy Configuration
        public string Ip { get; private set; }
        public List<int> Ports { get; private set; }

        public Server(int id, string name, CommunityID com, string ip, int port)
        {
            this.ID = id;
            this.Name = name;
            this.Community = com;
            this.Ip = ip;
            (this.Ports = new List<int>()).Add(port);
            this.Ip = ip;

            this.Propertys = new Dictionary<PropertyID, string>();
            this.Propertys.Add(PropertyID.SERVER_ID, id.ToString());
        }
    }
}
