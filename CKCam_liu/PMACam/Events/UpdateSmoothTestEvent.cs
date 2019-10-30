 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdateSmoothTestDelegate(UpdateSmoothTestEventArgs e);

    //显示模板区域事件
    public class UpdateSmoothTestEventArgs : EventArgs
    {

        public String output_region;




        public String OutputRegion
        {
            get
            {
                return output_region;
            }
        }

        public UpdateSmoothTestEventArgs(String poutput_region)
        {
            output_region = poutput_region;

        }


    }

    public class UpdateSmoothTest
    {
        //显示工具区域事件
        public static event UpdateSmoothTestDelegate SenUpdateSmoothTestArgs;
        public static void OnSendUpdateSmoothTest(UpdateSmoothTestEventArgs e)
        {
            if (SenUpdateSmoothTestArgs != null)
            {
                SenUpdateSmoothTestArgs(e);
            }
        }
    }

}




