using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdateCalibrationMakeDelegate(UpdateCalibrationMakeEventArgs e);

    //显示模板区域事件
    public class UpdateCalibrationMakeEventArgs : EventArgs
    {

        public String output_image;




        public String OutputImage
        {
            get
            {
                return output_image;
            }
        }

        public UpdateCalibrationMakeEventArgs(String poutput_image)
        {
            output_image = poutput_image;

        }


    }

    public class UpdateCalibrationMake
    {
        //显示工具区域事件
        public static event UpdateCalibrationMakeDelegate SenUpdateCalibrationMakeArgs;
        public static void OnSendUpdateCalibrationMake(UpdateCalibrationMakeEventArgs e)
        {
            if (SenUpdateCalibrationMakeArgs != null)
            {
                SenUpdateCalibrationMakeArgs(e);
            }
        }
    }

}




