using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SticksyProtocol
{
    public interface Data { }

    [Serializable]
    public class SignUp:Data
    {
        public string login { get; set; }
        public string password { get; set; }
    }

    [Serializable]
    public class SignIn : Data
    {
        public string login { get; set; }
        public string password { get; set; }
    }

    [Serializable]
    public class CreateStick : Data
    {
        public int idCreator { get; set; }       
    }

    [Serializable]
    public class DelStick : Data
    {
        public int idStick { get; set; }
    }

    [Serializable]
    public class EditStick : Data
    {
        public Stick stick { get; set; }
    }

    [Serializable]
    public class GetUsers : Data { }

    [Serializable]
    public class AddFriend : Data
    {
        public int idCreator { get; set; }
        public int idFriend { get; set; }
        public int idStick { get; set; }
    }

    [Serializable]
    public class AnswerId : Data
    {
        public int id { get; set; }
    }

    [Serializable]
    public class AnswerUser : Data
    {
        public User user { get; set; }
    }

    [Serializable]
    public class AnswerListUser : Data
    {
        public List<Friend> users { get; set; }
    }
    public class Transfer
    {
        private static BinaryFormatter formatter = new BinaryFormatter();
        public static void SendData(TcpClient clientSocket, Data data)
        {
            formatter.Serialize(clientSocket.GetStream(), data);
        }

        public static async void SendDataAsync(TcpClient clientSocket, Data data)
        {
            await Task.Run(() => formatter.Serialize(clientSocket.GetStream(), data));
        }

        public static Data ReceiveData(TcpClient clientSocket)
        {
            return (Data)formatter.Deserialize(clientSocket.GetStream());
        }
    }
}
