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
            server = new TcpListener(iP);
            userAnswer = new UserAnswer(new StiksyDataBase());
            StickServer = new StickServerIntegration.StickServerIntegration(new StiksyDataBase());

        }

        public void Listen()
        {
            while (true)
            {
                try {
                    TcpClient client = server.AcceptTcpClient();
                    ListenClientAsync(client);
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
                        //if (data is Sign)
                        //{
                        //    answerMethods.getUsers(data as GetUsers);
                        //}
                        #region Sticks
                        if (data is CreateStick)
                        {
                            Transfer.SendData(client, StickServer.CreateStick((data as CreateStick).idCreator));
                        }
                        if (data is DelStick)
                        {
                            StickServer.DeleteStick((data as DelStick).idStick);
                        }
                        if (data is EditStick)
                        {
                            StickServer.EditStick(data as Stick);
                        }
                        #endregion
                        #region Sign
                        if (data is Sign)
                        {
                            switch ((data as Sign).command)
                            {
                                case CommandUser.SignUp:
                                    { Transfer.SendData(client,userAnswer.SignUp(data as Sign)); break; }
                                case CommandUser.SignIn:
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
                    {}
                }
            });
        }

    }
}
