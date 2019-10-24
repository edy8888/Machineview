using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdateBufferReadImageDelegate(UpdateBufferReadImageEventArgs e);

    //显示模板区域事件
    public class UpdateBufferReadImageEventArgs : EventArgs
    {
        internal String name;
        internal int type;

        public String Name
        {
            get
            {
                return name;
            }
        }
        public int Type
        {
            get
            {
                return type;
            }
        }
        public UpdateBufferReadImageEventArgs(String pName,int pType)
        {
            name = pName;
            type = pType;
        }
    }

    public class UpdateBufferReadImage
    {
        //显示工具区域事件
        public static event UpdateBufferReadImageDelegate SenUpdateReadImageBufferArgs;
        public static void OnSendUpdateReadImageBuffer(UpdateBufferReadImageEventArgs e)
        {
            if (SenUpdateReadImageBufferArgs != null)
            {
                SenUpdateReadImageBufferArgs(e);
            }
        }
    }

}




