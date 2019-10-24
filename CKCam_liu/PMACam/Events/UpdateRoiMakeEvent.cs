 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdateRoiMakeDelegate(UpdateRoiMakeEventArgs e);

    //显示模板区域事件
    public class UpdateRoiMakeEventArgs : EventArgs
    {
        public String shape_name;




        public String Shapename
        {
            get
            {
                return shape_name;
            }
        }

        public UpdateRoiMakeEventArgs(String pshape_name)
        {
            shape_name = pshape_name;

        }


    }

    public class UpdateRoiMake
    {
        //显示工具区域事件
        public static event UpdateRoiMakeDelegate SenUpdateRoiMakeArgs;
        public static void OnSendUpdateRoiMake(UpdateRoiMakeEventArgs e)
        {
            if (SenUpdateRoiMakeArgs != null)
            {
                SenUpdateRoiMakeArgs(e);
            }
        }
    }

}




