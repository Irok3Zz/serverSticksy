using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SticksyProtocol
{
    
   [Serializable]
    public class TextCheck
    {
        public string text { get; set; }
        public bool isChecked { get; set; }
        public TextCheck() 
        {
            text = "";
            isChecked = false;
        }       
    }

   

    [Serializable]
    public class Stick
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<TextCheck> content { get; set; }
        public List<string> tags { get; set; }
        public int idCreator { get; set; }
        public List<Friend> idVisiters { get; set; }
        public DateTime date { get; set; }
        public KnownColor color { get; set; }
        public Stick(int iD, int iDCreator)
        {
            id = iD;
            idCreator = iDCreator;
            idVisiters = new List<Friend>();
            tags = new List<string>();
            date = DateTime.Now;
            color = KnownColor.White;
        }
    }
}
