using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

#region BackEnd
using SticksyProtocol;
using UserAnswers;
using BaseProtocol;
using StickServerIntegration;
#endregion

namespace serverSticksy
{
    class Server
    {
        TcpListener server { get; }

        #region BackEnd классы 
        StickServerIntegration.StickServerIntegration StickServer;
        UserAnswer userAnswer;
        #endregion

        public Server(IPEndPoint iP)
        {
            StiksyDataBase dataBase = new StiksyDataBase();
            server = new TcpListener(iP);
            userAnswer = new UserAnswer(dataBase);
            StickServer = new StickServerIntegration.StickServerIntegration(dataBase);
        }

        public void Listen()
        {
            Task<TcpClient> result;
            while (true)
            {
                try {
                    result = server.AcceptTcpClientAsync();
                    while (!result.IsCompleted)
                    {
                        Console.WriteLine("Waiting for connection");
                        Thread.Sleep(1000);
                    }
                    ListenClientAsync(result.Result);
                }
                catch { }
            }
        }
        private async void ListenClientAsync(TcpClient client)
        {
            
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        IData data = Transfer.ReceiveData(client);

                        #region Sticks
                        if (data is CreateStick) // Создание стика
                        {
                            Transfer.SendData(client, StickServer.CreateStick((data as CreateStick).idCreator));
                        }
                        if (data is DelStick) // Удаление стика
                        {
                            StickServer.DeleteStick((data as DelStick).idStick);
                        }
                        if (data is EditStick) // Редактирование стика
                        {
                            StickServer.EditStick(data as Stick);
                        }
                        #endregion
                        #region Sign 
                        if (data is Sign) 
                        {
                            switch ((data as Sign).command)
                            {
                                case CommandUser.SignUp: // Регистрация
                                    { Transfer.SendData(client, userAnswer.SignUp(data as Sign)); break; }
                                case CommandUser.SignIn: // Авторизация
                                    { Transfer.SendData(client, userAnswer.SignIn(data as Sign)); break; }
                                default:
                                    break;
                            }
                        }
                        #endregion
                        #region AddFriendsToStick(GetUSers) *не доработано*
                        //if (data is GetUsers)
                        //{
                        //    Transfer.SendData(client, StickServer.)
                        //}
                        #endregion
                    }
                    catch
                    { }
                }
            });
        }

    }
}
