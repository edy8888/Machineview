 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdateMorTestDelegate(UpdateMorTestEventArgs e);

    //显示模板区域事件
    public class UpdateMorTestEventArgs : EventArgs
    {

        public String output_region;




        public String OutputRegion
        {
            get
            {
                return output_region;
            }
        }

        public UpdateMorTestEventArgs(String poutput_region)
        {
            output_region = poutput_region;

        }


    }

    public class UpdateMorTest
    {
        //显示工具区域事件
        public static event UpdateMorTestDelegate SenUpdateMorTestArgs;
        public static void OnSendUpdateMorTest(UpdateMorTestEventArgs e)
        {
            if (SenUpdateMorTestArgs != null)
            {
                SenUpdateMorTestArgs(e);
            }
        }
    }

}




