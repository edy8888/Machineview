using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdateFindCircleDelegate(UpdateFindCircleEventArgs e);

    //显示模板区域事件
    public class UpdateFindCircleEventArgs : EventArgs
    {
        public String Tool_name;




        public String Shapename
        {
            get
            {
                return Tool_name;
            }
        }

        public UpdateFindCircleEventArgs(String pshape_name)
        {
            Tool_name = pshape_name;

        }


    }

    public class UpdateFindCircle
    {
        //显示工具区域事件
        public static event UpdateFindCircleDelegate SenUpdateFindCircleArgs;
        public static void OnSendUpdateFindCircle(UpdateFindCircleEventArgs e)
        {
            if (SenUpdateFindCircleArgs != null)
            {
                SenUpdateFindCircleArgs(e);
            }
        }
    }

}




