using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void ShowToolGreyRegionDelegate(ShowToolGreyRegionEventArgs e);

    //显示模板区域事件
    public class ShowToolGreyRegionEventArgs : EventArgs
    {
        internal String  tool;

        public String Tool
        {
            get
            {
                return tool;
            }
        }

        public ShowToolGreyRegionEventArgs(String pTool)
        {
            tool = pTool;
        }
    }

    public class ShowToolGreyRegion
    {
        //显示工具区域事件
        public static event ShowToolGreyRegionDelegate SendShowToolGreyRegionArgs;
        public static void OnSendShowPattrenGreyRegion(ShowToolGreyRegionEventArgs e)
        {
            if (SendShowToolGreyRegionArgs != null)
            {
                SendShowToolGreyRegionArgs(e);
            }
        }
    }

}




