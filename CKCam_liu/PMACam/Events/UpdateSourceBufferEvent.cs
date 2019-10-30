using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMACam
{


    public delegate void UpdateSourceBufferDelegate(UpdateSourceBufferEventArgs e);

    //显示模板区域事件
    public class UpdateSourceBufferEventArgs : EventArgs
    {


        internal List<SourceBuffer> sourceBuffer_update;
        internal List<ExecuteBuffer> executeBuffer_update;

        public List<SourceBuffer> Source_info
        {
            get
            {
                return sourceBuffer_update;
            }
        }

        public List<ExecuteBuffer> Execute_info
        {
            get
            {
                return executeBuffer_update;
            }
        }
        public UpdateSourceBufferEventArgs(List<SourceBuffer> pSource_infotest,List<ExecuteBuffer> pExecuter_infotest)
        {
            sourceBuffer_update = pSource_infotest;
            executeBuffer_update = pExecuter_infotest;

        }
    }

    public class UpdateSourceBuffer
    {
        //显示工具区域事件
        public static event UpdateSourceBufferDelegate SenUpdateSourceBufferArgs;
        public static void OnSendUpdateSourceBuffer(UpdateSourceBufferEventArgs e)
        {
            if (SenUpdateSourceBufferArgs != null)
            {
                SenUpdateSourceBufferArgs(e);
            }
        }
    }

}




