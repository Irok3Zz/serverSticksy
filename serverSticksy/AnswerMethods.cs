using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SticksyProtocol;
using DataBaseProtocol;

namespace AnswerMethods
{
    static class AnswerMethods
    {
        static public AnswerId SignUp(Sign newUser) // SignUp(Регистрация)
        {
            // Возвращает id нового User или -1, если такой пользователь уже занят
            return new AnswerId(SaveBase.SaveUserRegistration(new User(0, newUser.login, newUser.password)));
        }
        static public AnswerUser SignIn(Sign checkUser) // SignIn(Авторизация)
        {
            // Возвращает объект User или null, если нет совпадений в базе
            return new AnswerUser(QueryBase.CheckUserIn(new User(0, checkUser.login, checkUser.password)));
        }

    }
}
