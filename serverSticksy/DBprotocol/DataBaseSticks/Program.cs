using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SticksyProtocol;
using System.Drawing;
using DataBaseProtocol;


namespace DataBaseSticksy
{
    class Program
    {
        static void Main(string[] args)
        {
            User user1 = new User(1, "Nick", "123");
            int id = QueryBase.CheckUserIn(user1);//{ sticks = new List<Stick>() };
            User user2 = new User(2, "Lola", "12345");
            //SaveBase.SaveUser(user2);//{ sticks = new List<Stick>() };
            User user3 = new User(3, "Bella", "1234567");
            //SaveBase.SaveUser(user3);//{ sticks = new List<Stick>() };
            //int idS = QueryBase.IdStick(user1);
           Console.WriteLine(id);
            //List<Stick> st1 = new List<Stick>();
            //List<Stick> st2 = new List<Stick>();
            //List<Stick> st3 = new List<Stick>();
            Stick stick1 = new Stick(1, 1)
            {
                title = "home work1",
                //idVisiters = new List<Friend>(),
                date = DateTime.Now,
                color = KnownColor.Brown
               // content = new List<TextCheck>(),
               // tags = new List<string>()
            };
            //SaveBase.SaveStick(stick1);
            //Stick stick2 = new Stick(2, 2)
            //{
            //    title = "home work2",
            //    idVisiters = new List<Friend>(),
            //    date = DateTime.Now,
            //    color = KnownColor.Aquamarine,
            //    content = new List<TextCheck>(),
            //    tags = new List<string>()
            //};
            //Stick stick3 = new Stick(3, 3)
            //{
            //    title = "home work3",
            //    idVisiters = new List<Friend>(),
            //    date = DateTime.Now,
            //    color = KnownColor.DarkGray,
            //    content = new List<TextCheck>(),
            //    tags = new List<string>()
            //};
            //List<string> tags1 = new List<string>();
            //tags1.Add("work1");
            //tags1.Add("home1");
            //tags1.Add("sleep1");
            //tags1.Add("morning1");
            //List<string> tags2 = new List<string>();
            //tags2.Add("work2");
            //tags2.Add("home2");
            //tags2.Add("sleep2");
            //tags2.Add("morning2");
            //List<string> tags3 = new List<string>();
            //tags3.Add("work3");
            //tags3.Add("home3");
            //tags3.Add("sleep3");
            //tags3.Add("morning3");
            //TextCheck textCheck1 = new TextCheck()
            //{
            //    text = "I need money",
            //    isChecked = false
            //};
            //TextCheck textCheck2 = new TextCheck()
            //{
            //    text = "I need help",
            //    isChecked = false
            //};
            //List<TextCheck> texts = new List<TextCheck>();
            //texts.Add(textCheck1);
            //texts.Add(textCheck2);
            //List<Friend> friends = new List<Friend>();
            //Friend friend1 = new Friend(2, "Lola");
            //Friend friend2 = new Friend(3, "Bella");
            //friends.Add(friend1);
            //friends.Add(friend2);
            //stick1.idVisiters = friends;
            //stick1.tags = tags1;
            //stick1.content = texts;
            //stick2.idVisiters = friends;
            //stick2.tags = tags2;
            //stick2.content = texts;
            //stick3.idVisiters = friends;
            //stick3.tags = tags3;
            //stick3.content = texts;
            //st1.Add(stick1);
            //st1.Add(stick2);
            //st2.Add(stick2);
            //st2.Add(stick3);
            //st3.Add(stick3);
            //st3.Add(stick1);
            //user1.sticks = st1;
            //user2.sticks = st2;
            //user3.sticks = st3;

            //SaveBase.SaveUser(user2);
            //SaveBase.SaveUser(user3);

            Console.ReadLine();
        }
    }
}
