using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdateRegionInterestDelegate(UpdateRegionInterestEventArgs e);

    //显示模板区域事件
    public class UpdateRegionInterestEventArgs : EventArgs
    {

        public String output_region;




        public String OutputRegion
        {
            get
            {
                return output_region;
            }
        }

        public UpdateRegionInterestEventArgs(String poutput_region)
        {
            output_region = poutput_region;

        }


    }

    public class UpdateRegionInterest
    {
        //显示工具区域事件
        public static event UpdateRegionInterestDelegate SenUpdateRegionInterestArgs;
        public static void OnSendUpdateRegionInterest(UpdateRegionInterestEventArgs e)
        {
            if (SenUpdateRegionInterestArgs != null)
            {
                SenUpdateRegionInterestArgs(e);
            }
        }
    }

}




