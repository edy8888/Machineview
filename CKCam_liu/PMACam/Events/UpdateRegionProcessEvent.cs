using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdateRegionProcessDelegate(UpdateRegionProcessEventArgs e);

    //显示模板区域事件
    public class UpdateRegionProcessEventArgs : EventArgs
    {

        public String output_region;




        public String OutputRegion
        {
            get
            {
                return output_region;
            }
        }

        public UpdateRegionProcessEventArgs(String poutput_region)
        {
            output_region = poutput_region;

        }


    }

    public class UpdateRegionProcess
    {
        //显示工具区域事件
        public static event UpdateRegionProcessDelegate SenUpdateRegionProcessArgs;
        public static void OnSendUpdateRegionProcess(UpdateRegionProcessEventArgs e)
        {
            if (SenUpdateRegionProcessArgs != null)
            {
                SenUpdateRegionProcessArgs(e);
            }
        }
    }

}




