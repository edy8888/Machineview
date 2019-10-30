 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdateThresholdTestDelegate(UpdateThresholdTestEventArgs e);

    //显示模板区域事件
    public class UpdateThresholdTestEventArgs : EventArgs
    {
        public String input_image;
        public String output_region;
        public String output_image;
        public int minvalue;
        public int maxvalue;
        public bool dyn_show;


        public String InputImage
        {
            get
            {
                return input_image;
            }
        }
        public String OutputRegion
        {
            get
            {
                return output_region;
            }
        }
        public String OutputRImage
        {
            get
            {
                return output_image;
            }
        }
        public int  MinValue
        {
            get
            {
                return minvalue;
            }
        }

        public int MaxValue
        {
            get
            {
                return maxvalue;
            }
        }
        public bool Dyn_showtest
        {
            get
            {
                return dyn_show;
            }
        }
        public UpdateThresholdTestEventArgs(String pinput_image, String poutput_region,String poutput_image,int pmin_value, int pmax_value,bool pdyn_show)
        {
            input_image = pinput_image;
            output_region = poutput_region;
            output_image = poutput_image;
            minvalue = pmin_value;
            maxvalue = pmax_value;
            dyn_show = pdyn_show;
        }


    }

    public class UpdateThresholdTest
    {
        //显示工具区域事件
        public static event UpdateThresholdTestDelegate SenUpdateThresholdTestArgs;
        public static void OnSendUpdateThresholdTest(UpdateThresholdTestEventArgs e)
        {
            if (SenUpdateThresholdTestArgs != null)
            {
                SenUpdateThresholdTestArgs(e);
            }
        }
    }

}




