using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdateFindLineDelegate(UpdateFindLineEventArgs e);

    //显示模板区域事件
    public class UpdateFindLineEventArgs : EventArgs
    {
        public String Tool_name;




        public String Shapename
        {
            get
            {
                return Tool_name;
            }
        }

        public UpdateFindLineEventArgs(String pshape_name)
        {
            Tool_name = pshape_name;

        }


    }

    public class UpdateFindLine
    {
        //显示工具区域事件
        public static event UpdateFindLineDelegate SenUpdateFindLineArgs;
        public static void OnSendUpdateFindLine(UpdateFindLineEventArgs e)
        {
            if (SenUpdateFindLineArgs != null)
            {
                SenUpdateFindLineArgs(e);
            }
        }
    }

}




