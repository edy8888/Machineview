using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void ShowToolResultDelegate(ShowToolResultEventArgs e);

    //显示模板区域事件
    public class ShowToolResultEventArgs : EventArgs
    {
        internal String  tool;

        public String Tool
        {
            get
            {
                return tool;
            }
        }

        public ShowToolResultEventArgs(String pTool)
        {
            tool = pTool;
        }
    }

    public class ShowToolResult
    {
        //显示工具区域事件
        public static event ShowToolResultDelegate SendShowToolResultArgs;
        public static void OnSendShowPattrenResult(ShowToolResultEventArgs e)
        {
            if (SendShowToolResultArgs != null)
            {
                SendShowToolResultArgs(e);
            }
        }
    }

}




