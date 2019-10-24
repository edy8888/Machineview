using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace PMACam
{
    public class SourceBuffer
    {
        public Dictionary<Info_Source, HObject> _s_ObjectBuffer;
        public Dictionary<Info_Source, Object> _s_ControlBuffer;
    }

    public class Info_Source
    {
        public String Name;
        public String Type;
        public String Path;
        public Info_Source(String name, String type, String path)
        {
            Name = name;
            Type = type;
            Path = path;
        }
        public Info_Source()
        { 
        }

    }
}
