using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SticksyProtocol;

namespace AnswerMethods
{
    static class AnswerMethods
    {
        static public AnswerId SignUp() // Регистрация
        {
            AnswerId answerId = new AnswerId();

            /* To Do
             answerId = GetIdFormDB(); // Возвращает id нового User или -1, если такой пользователь уже занят
            */

            return answerId;
        }
        static public AnswerUser SignIn() // Авторизация
        {
            AnswerUser answerUser = new AnswerUser();

            /* To Do 
            answerUser.User = GetUserFromDB(); // Возвращает объект User или null, если нет совпадений в базе
            */

            return answerUser;
        }

    }
}
