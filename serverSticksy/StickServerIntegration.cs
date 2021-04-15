using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SticksyProtocol;
using BaseProtocol;

namespace StickServerIntegration
{
     class StickServerIntegration
    {
        StiksyDataBase DBquery;
        StickServerIntegration(StiksyDataBase constr)
        {
            DBquery = constr;
        }
        public AnswerId CreateStick(int creatorId)
        {
            return new AnswerId(DBquery.CreateStick(creatorId),TypeId.IdStick);
        }
        public void EditStick(Stick stick)
        {
            DBquery.UpdateStick(stick);
        }
        public void DeleteStick(int stickId)
        {
            DBquery.DeleteStick(stickId);
        }
    }
}
