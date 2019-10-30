using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
namespace PMACam
{
    [Serializable]
    public partial class FocusPoint : Form, ISerializable
    {

        double x_get = 0;
        double y_get = 0;
        public FocusPoint(ExecuteBuffer buffer, bool addbuffer)
        {
            InitializeComponent();

            if (addbuffer)
            {
                if (buffer != null)
                {
                    SetParaImage(buffer);
                    //  SetParaImage(buffer);

                }
            }


           // this.panel_inno2.Visible = false;


        }



        internal void SetParaImage(ExecuteBuffer test)
        {
            if (this.cbb_image.Items.Count == 0)
            {
                int defaultnumber = 0;
                int get_number = 0;
                foreach (string keyc in test.imageBuffer.Keys)
                    if (keyc.Contains(".img"))
                    {
                        cbb_image.Items.Add(keyc.Substring(0, keyc.Length - 4));
                        if (keyc.Contains("image"))
                        {
                            get_number = Convert.ToInt32(keyc.Substring(5, keyc.Length - 9));

                            if (get_number > defaultnumber)
                            {
                                cbb_image.Text = keyc.Substring(0, keyc.Length - 4);
                                defaultnumber = get_number;
                            }
                        }
                    }


            }
            else
            {

                int m = 0;
                if (cbb_image.SelectedItem != null)
                {
                    if (!test.imageBuffer.ContainsKey(cbb_image.SelectedItem.ToString() + ".img"))
                        cbb_image.Items.Remove(cbb_image.SelectedItem.ToString() + ".img");
                }
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < cbb_image.Items.Count; i++)
                    {
                        if (keyc == (cbb_image.Items[i].ToString() + ".img"))
                            break;
                        m = i;
                        if (m == cbb_image.Items.Count - 1 && keyc.Contains(".img"))
                            cbb_image.Items.Add(keyc.Substring(0, keyc.Length - 4));

                    }
                }


            }

        }
        public FocusPoint(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();



        this.label_show.Text = "x 偏移：" + x_get.ToString() + "\n" + "y偏移是：" + y_get.ToString();





            this.tb_outnumber.Text = (string)(info.GetValue("PointNumber", typeof(string)));

            x_get = (double)(info.GetValue("X_value", typeof(double)));
            y_get = (double)(info.GetValue("Y_value", typeof(double)));



            string out_image_string = (string)(info.GetValue("Image_string", typeof(string)));
            string[] condition = { "," };
            string[] result = out_image_string.Split(condition, StringSplitOptions.None);

            for (int m = 0; m < result.Count(); m++)
                cbb_image.Items.Add(result[m]);
            cbb_image.SelectedIndex = (Int32)(info.GetValue("ImageSource", typeof(Int32)));
} 
          public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);

            info.AddValue("ImageSource", this.cbb_image.SelectedIndex.ToString());
            info.AddValue("PointNumber", this.tb_outnumber.Text.ToString());
            info.AddValue("X_value", (double)x_get);
            info.AddValue("Y_value", (double)y_get);

            string Inputimage_string = "";
            for (int m = 0; m < this.cbb_image.Items.Count; m++)
            {
                if (m == this.cbb_image.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, cbb_image.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, cbb_image.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string", Inputimage_string);


        } 
        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);





            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);

            IniFile.IniWriteValue(j.ToString(), "Input_image", this.cbb_image.Text);
  
            IniFile.IniWriteValue(j.ToString(), "X_value", x_get.ToString());
            IniFile.IniWriteValue(j.ToString(), "PointNumber", this.tb_outnumber.Text.ToString());



            
        }

        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);



            this.cbb_image.Items.Clear();
            cbb_image.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image"));
            cbb_image.SelectedIndex = 0;


            if (IniFile.IniReadValue(j.ToString(), "PointNumber") != "")
                this.tb_outnumber.Text = IniFile.IniReadValue(j.ToString(), "PointNumber");
            this.label_show.Text = "x 偏移：" + x_get.ToString() +"\n" + "y偏移是：" + y_get.ToString();






        }
        public bool Run_transform(ExecuteBuffer _executeBuffer, 模板GVName_halcon Model_result, Dictionary<int, PointName> Point_temp_result, out Dictionary<int, PointName> Point_out_result, out string result_info)
        {


            HTuple x, y;
            HTuple modelangle =0, modelregionangle = 0;
            result_info = "";
            HTuple output_x = 0, output_y = 0;

            Point_out_result = Point_temp_result;
            if (!_executeBuffer.imageBuffer.ContainsKey(this.cbb_image.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像1已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像1已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.cbb_image.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image1参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像1已经不存在，请重置设置输入图像";
                return false;
            }
            HObject EdgeAmplitude,EdgeAmplitude1,region,BinImage,ImageResult3,ImageResult4;
            HTuple Min,Max,Range,Width,Height,Value,Deviation;
            HOperatorSet.GenEmptyObj(out EdgeAmplitude);
            HOperatorSet.GenEmptyObj(out EdgeAmplitude1);
            HOperatorSet.GenEmptyObj(out region);
            HOperatorSet.GenEmptyObj(out BinImage);
            HOperatorSet.GetImageSize(_executeBuffer.imageBuffer[this.cbb_image.SelectedItem.ToString() + ".img"],out Width,out Height);
            HOperatorSet.SobelAmp(_executeBuffer.imageBuffer[this.cbb_image.SelectedItem.ToString() + ".img"],out EdgeAmplitude,"sum_sqrt", 3);
            HOperatorSet.MinMaxGray(EdgeAmplitude,EdgeAmplitude,0,out Min,out Max,out Range );
            HOperatorSet.Threshold(EdgeAmplitude,out region,50,255);
            HOperatorSet.RegionToBin(region,out BinImage,1,0,Width,Height);
            HOperatorSet.MultImage(BinImage, BinImage, out ImageResult3, 1, 0);
            HOperatorSet.MultImage(ImageResult3, ImageResult3, out ImageResult4, 1, 0);
            HOperatorSet.Intensity(ImageResult4, ImageResult4, out Value, out Deviation);
            x_get = Value;
            y_get = Deviation;
                int out_number_list = Convert.ToInt32(this.tb_outnumber.Text.ToString());
                if (Point_temp_result.ContainsKey(out_number_list))
                {
                    Point_temp_result[out_number_list].点X = x_get;
                    Point_temp_result[out_number_list].点Y = y_get;
                }
                else
                    Point_temp_result.Add(out_number_list, new PointName(output_x, output_y));

                Point_out_result = Point_temp_result;


                this.label_show.Text = "x 偏移：" + x_get.ToString() + "\n" + "y偏移是：" + y_get.ToString(); 
            return true;
            }



            








        

        public bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(strNumber) &&
                   !objTwoDotPattern.IsMatch(strNumber) &&
                   !objTwoMinusPattern.IsMatch(strNumber) &&
                   objNumberPattern.IsMatch(strNumber);
        }

        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?/d*[.]?/d*$");
        }
        private void button1_Click(object sender, EventArgs e)
        {




            if (!IsNumber(this.tb_outnumber.Text.ToString()))
            {
                MessageBox.Show("算术计算：  输出点位编号不是数字,请重新输入");
                return;
            }


            this.Visible = false;
        }





        public  bool Check_pal()
        {



            if (!IsNumber(this.tb_outnumber.Text.ToString()))
            {
                MessageBox.Show("算术计算：  输出点位编号不是数字,请重新输入");
                return false;
            }
            return true;
        
        }









    }
}
