using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdatePuzzlePictureDelegate(UpdatePuzzlePictureEventArgs e);

    //显示模板区域事件
    public class UpdatePuzzlePictureEventArgs : EventArgs
    {

        public String output_image;




        public String OutputImage
        {
            get
            {
                return output_image;
            }
        }

        public UpdatePuzzlePictureEventArgs(String poutput_image)
        {
            output_image = poutput_image;

        }


    }

    public class UpdatePuzzlePicture
    {
        //显示工具区域事件
        public static event UpdatePuzzlePictureDelegate SenUpdatePuzzlePictureArgs;
        public static void OnSendUpdatePuzzlePicture(UpdatePuzzlePictureEventArgs e)
        {
            if (SenUpdatePuzzlePictureArgs != null)
            {
                SenUpdatePuzzlePictureArgs(e);
            }
        }
    }

}




