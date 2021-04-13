using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Configuration;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using SticksyProtocol;

namespace DataBaseProtocol
{
   
    public class SaveBase
    {
        public static DataBase dataBase = new DataBase(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

        //сохранение пользователя при регестрации
        public static int SaveUserRegistration(User user)
        {
            try
            {
                dataBase.Users.InsertOnSubmit(
                    new UserB
                    {
                        Login = user.login,
                        Password = user.password
                    });
                dataBase.SubmitChanges();
                return dataBase.Users.Where((x) => x.Login == user.login).Select((d) => d.Id).ToList()[0];
            }
            catch { return -1; }//если такой логин есть возвращает -1
        }

        //сохранение стика (обновление)
        public static void SaveStick(Stick stick)
        {
            try
            {
                List<StickB> st = (from S in dataBase.Sticks where S.Id == stick.id select S).ToList();
                foreach (var item in st)
                {
                    item.Title = stick.title;
                    item.Date = stick.date;
                    item.Color = stick.color.ToString();
                }
                dataBase.SubmitChanges();

                if (stick.content.Count != 0)
                    SaveTextCheck(stick.content, stick.id);
                if (stick.tags.Count != 0)
                    SaveTags(stick.tags, stick.id);
                if (stick.idVisiters.Count != 0)
                    SaveFriends(stick.idVisiters, stick.id);

            }
            catch { }
        }

        //сохранение нового или обновление
        public static void SaveTextCheck(List<TextCheck> textChecks, int id)
        {
            try
            {
                List<TextCheckB> query = (from T in dataBase.TextChecks where T.IdStick == id select T).ToList();
                if (query.Count == 0)
                {
                    foreach (var item in textChecks)
                    {
                        dataBase.TextChecks.InsertOnSubmit(
                            new TextCheckB
                            {
                                IdStick = id,
                                Text = item.text,
                                IsChecked = item.isChecked == true ? 1 : 0
                            });
                        dataBase.SubmitChanges();
                    }
                }
                else
                {
                    foreach (var item in query)
                    {
                        foreach (var it in textChecks)
                        {
                            if (item.Text == it.text)
                                item.IsChecked = (it.isChecked == true ? 1 : 0);
                        }
                    }
                }
            }
            catch { }
        }

        public static void SaveTags(List<string> tags, int id)
        {
            try
            {
                List<TagsB> query = (from T in dataBase.Tags where T.IdStick == id select T).ToList();
                if (query.Count == 0)
                {
                    foreach (var item in tags)
                    {
                        dataBase.Tags.InsertOnSubmit(
                            new TagsB
                            {
                                IdStick = id,
                                Tag = item
                            });
                        dataBase.SubmitChanges();
                    }
                }
                else if(query.Count < tags.Count)
                {
                    foreach (var item in tags)
                    {
                        if(query.Exists((x) => x.Tag != item))
                        {
                            dataBase.Tags.InsertOnSubmit(
                                    new TagsB
                                    {
                                        IdStick = id,
                                        Tag = item
                                    });
                            dataBase.SubmitChanges();
                        }
                    }
                }
            }
            catch { }
        }

        public static void SaveFriends(List<Friend> friends, int id)
        {
            try
            {
                foreach (var item in friends)
                {
                    dataBase.Friends.InsertOnSubmit(
                        new FriendB
                        {
                            IdStick = id,
                            Login = item.login
                        });
                    dataBase.SubmitChanges();
                }
            }
            catch { }
        }
    }

    public class QueryBase
    {
        public static DataBase dataBase = new DataBase(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

        //запрос Id нового стика
        public static int IdStick(int id)
        {
            dataBase.Sticks.InsertOnSubmit(
                new StickB
                {
                    Title = "",
                    IdCreator = id,
                    Date = DateTime.Now,
                    Color = KnownColor.White.ToString()
                });
            dataBase.SubmitChanges();
            var query = from S in dataBase.Sticks
                        select S;
            int count = query.ToList().Count;
            return count;
        }

        //проверка логина и пароля при входе уже зарегестрированного пользователя
        public static User CheckUserIn(User user)
        {
            try
            {
                var query = from U in dataBase.Users
                            where U.Login == user.login && U.Password == user.password
                            select U;
                if (query.ToList().Count == 1)
                {
                    UserB map = dataBase.Users.Where((x) => x.Login == user.login).Select((d) => d).ToList()[0]; //возвращает User пользователя
                    return new User(map.Id, map.Login, map.Password);
                }
                else
                    return null; //если логин или пароль введены не верно возвращает null
            }
            catch { return null; }
        }

        /*
                 public static int CheckUserIn(User user)
        {
            try
            {
                var query = from U in dataBase.Users
                            where U.Login == user.login && U.Password == user.password
                            select U;
                if (query.ToList().Count == 1)
                    return dataBase.Users.Where((x) => x.Login == user.login).Select((d) => d.Id).ToList()[0]; //возвращает Id пользователя
                else
                    return -1;//если логин или пароль введены не верно возвращает -1
            }
            catch { return -1; }
        }

         */
    }

    public class LoadBase
    {
        public static DataBase dataBase = new DataBase(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
    }

    public class DellBase
    {
        public static DataBase dataBase = new DataBase(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
    }

    [Table(Name = "User")]
    public class UserB
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "login")]
        public string Login { get; set; }
        [Column(Name = "password")]
        public string Password { get; set; }
    }

    [Table(Name = "Friend")]
    public class FriendB
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "id_stick")]
        public int IdStick { get; set; }
        [Column(Name = "login")]
        public string Login { get; set; }
    }

    [Table(Name = "Stick")]
    public class StickB
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "title")]
        public string Title { get; set; }
        [Column(Name = "id_creator")]
        public int IdCreator { get; set; }
        [Column(Name = "date")]
        public DateTime Date { get; set; }
        [Column(Name = "color")]
        public string Color { get; set; }
    }

    [Table(Name = "Tags")]
    public class TagsB
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "id_stick")]
        public int IdStick { get; set; }
        [Column(Name = "tag")]
        public string Tag { get; set; }
    }

    [Table(Name = "TextCheck")]
    public class TextCheckB
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "id_stick")]
        public int IdStick { get; set; }
        [Column(Name = "text")]
        public string Text { get; set; }
        [Column(Name = "is_checked")]
        public int IsChecked { get; set; }
    }

    public class DataBase : DataContext
    {
        public Table<UserB> Users;
        public Table<StickB> Sticks;
        public Table<TagsB> Tags;
        public Table<TextCheckB> TextChecks;
        public Table<FriendB> Friends;

        public DataBase(string conStr) : base(conStr)
        {
            Users = GetTable<UserB>();
            Sticks = GetTable<StickB>();
            Tags = GetTable<TagsB>();
            TextChecks = GetTable<TextCheckB>();
            Friends = GetTable<FriendB>();
        }
    }
}
