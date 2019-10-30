using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace PMACam
{
    public class ExecuteBuffer
    {
        public Dictionary<string, HObject> imageBuffer;
        public Dictionary<string, Object> controlBuffer;
        public Dictionary<int, All_buffer> all_test_buffer;

    }
    public class All_buffer
    {
        public List<String> imagebuffer = new List<string>();
        public List<String> controlbuffer = new List<string>();

    }
    public class ROI_Model
    {
        public string Roi_model;
        public double Roi_x;
        public double Roi_y;
        public double Roi_Rec2length1;
        public double Roi_Rec2length2;
        public double Roi_Rec2phi;
        public double Roi_Cir1radius;
        public ROI_Model()
        {
            //  Roi_model = "none";
        }

    }
}
