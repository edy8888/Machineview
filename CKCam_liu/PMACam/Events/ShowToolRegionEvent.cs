using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void ShowToolRegionDelegate(ShowToolRegionEventArgs e);

    //显示模板区域事件
    public class ShowToolRegionEventArgs : EventArgs
    {
        internal String  tool;

        public String Tool
        {
            get
            {
                return tool;
            }
        }

        public ShowToolRegionEventArgs(String pTool)
        {
            tool = pTool;
        }
    }

    public class ShowToolRegion
    {
        //显示工具区域事件
        public static event ShowToolRegionDelegate SendShowToolRegionArgs;
        public static void OnSendShowPattrenRegion(ShowToolRegionEventArgs e)
        {
            if (SendShowToolRegionArgs != null)
            {
                SendShowToolRegionArgs(e);
            }
        }
    }

}




