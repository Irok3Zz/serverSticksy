using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SticksyProtocol
{
    [Serializable]
    public class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public List<Stick> sticks { get; set; }

        public User(int iD, string log, string pas)
        {
            id = iD;
            login = log;
            password = pas;
            sticks = new List<Stick>();
        }
    }

    [Serializable]
    public class Friend
    {
        public int id { get; set; }
        public string login { get; set; }

        public Friend(int iD, string log)
        {
            id = iD;
            login = log;
        }
    }
}
