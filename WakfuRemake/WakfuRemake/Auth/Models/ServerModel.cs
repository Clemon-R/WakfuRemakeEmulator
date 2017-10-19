using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakfuRemake.Auth.Models
{
    public class ServerModel
    {
        public enum CommunityId
        {
            FRENCH
        }
        public int ID { get; private set; }
        public string Name { get; private set; }
        public CommunityId Community { get; private set; }
        public string AdressHost { get; private set; }
        public int[] Ports { get; private set; }
    }
}
