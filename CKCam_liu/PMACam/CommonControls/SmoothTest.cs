using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.Text.RegularExpressions;
using ViewROI;
using System.Runtime.Serialization; 
namespace PMACam
{
    [Serializable]
    public partial class SmoothTest : Form, ISerializable
    {


        public SmoothTest(ExecuteBuffer buffer, bool addbuffer)
        {
            InitializeComponent();
            Panel_show();
            if (addbuffer)
            {
                if (buffer != null)
                {
                    SetParaImage(buffer);


                }
            }
            smooth_typemedian.SelectedIndex = 0;
            smooth_margin.SelectedIndex = 0;
            txt_sizegauss.SelectedIndex = 0;

        }


        public void Panel_show()
        {
            if (this.comboBox_type.SelectedIndex == 0)
            {
                panel_gauss.Visible = true;
                panel_median.Visible = false;
                panel_mean.Visible = false;

            }
            else if (comboBox_type.SelectedIndex == 1)
            {
                panel_gauss.Visible = false;
                panel_median.Visible = false;
                panel_mean.Visible = true;
            }
            else
            {
                panel_gauss.Visible = false;
                panel_median.Visible = true;
                panel_mean.Visible = false;
            }
        }


        public SmoothTest(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();
            smooth_typemedian.SelectedIndex = 0;
            smooth_margin.SelectedIndex = 0;
            txt_sizegauss.SelectedIndex = 0;

            this.smooth_margin.SelectedIndex = (Int32)(info.GetValue("MedianMargin", typeof(Int32)));

            this.smooth_typemedian.SelectedIndex = (Int32)(info.GetValue("MedianType", typeof(Int32)));
            this.comboBox_type.SelectedIndex = (Int32)(info.GetValue("SmoothModel", typeof(Int32)));
            this.smooth_image.Text = (string)(info.GetValue("SmoothImage", typeof(string)));
            this.smooth_imageMeanout.Text = (string)(info.GetValue("SmoothImageout", typeof(string)));
            this.txt_sizegauss.Text = (string)(info.GetValue("GaussSize", typeof(string)));
            this.txt_maskWidthmean.Text = (string)(info.GetValue("MeanWidth", typeof(string)));
            this.txt_maskHeightmean.Text = (string)(info.GetValue("MeanHeight", typeof(string)));
            this.txt_radiusmedian.Text = (string)(info.GetValue("MedianRadius", typeof(string)));
            string out_image_string = (string)(info.GetValue("Image_string", typeof(string)));
            string[] condition = { "," };
            string[] result = out_image_string.Split(condition, StringSplitOptions.None);

            for (int m = 0; m < result.Count(); m++)
                smooth_image.Items.Add(result[m]);
            smooth_image.SelectedIndex = (Int32)(info.GetValue("SmoothImage", typeof(Int32)));

} 

                public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("SmoothModel", this.comboBox_type.SelectedIndex.ToString());
            info.AddValue("SmoothImage", this.smooth_image.SelectedIndex.ToString());
            info.AddValue("SmoothImageout", this.smooth_imageMeanout.Text.ToString());

            info.AddValue("GaussSize", this.txt_sizegauss.Text.ToString());

            info.AddValue("MeanWidth", this.txt_maskWidthmean.Text.ToString());
            info.AddValue("MeanHeight", this.txt_maskHeightmean.Text.ToString());
            info.AddValue("MedianRadius", this.txt_radiusmedian.Text.ToString());
            info.AddValue("MedianType", this.smooth_typemedian.SelectedIndex.ToString());
            info.AddValue("MedianMargin", this.smooth_margin.SelectedIndex.ToString());
            string Inputimage_string = "";
            for (int m = 0; m < this.smooth_image.Items.Count; m++)
            {
                if (m == this.smooth_image.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, smooth_image.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, smooth_image.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string", Inputimage_string);
        } 
        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "SmoothModel", this.comboBox_type.SelectedIndex.ToString());

            IniFile.IniWriteValue(j.ToString(), "SmoothImage", this.smooth_image.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "SmoothImageout", this.smooth_imageMeanout.Text.ToString());


            if (this.comboBox_type.SelectedIndex == 0)
                IniFile.IniWriteValue(j.ToString(), "GaussSize", this.txt_sizegauss.Text.ToString());
            else if (this.comboBox_type.SelectedIndex == 1)
            {
                IniFile.IniWriteValue(j.ToString(), "MeanWidth", this.txt_maskWidthmean.Text.ToString());
                IniFile.IniWriteValue(j.ToString(), "MeanHeight", this.txt_maskHeightmean.Text.ToString());
            }
            else
            {
                IniFile.IniWriteValue(j.ToString(), "MedianRadius", this.txt_radiusmedian.Text.ToString());
                IniFile.IniWriteValue(j.ToString(), "MedianType", this.smooth_typemedian.SelectedIndex.ToString());
                IniFile.IniWriteValue(j.ToString(), "MedianMargin", this.smooth_margin.SelectedIndex.ToString());
            }


        }


        internal void ReadData(List<string> n_Path, int j)
        {
   
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            int Selectnumber = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "SmoothModel"));
            this.comboBox_type.SelectedIndex = Selectnumber;
            if (Selectnumber == 0)
            {
                this.txt_sizegauss.Text = IniFile.IniReadValue(j.ToString(), "GaussSize");



            }
            else if (Selectnumber == 1)
            {
                this.txt_maskWidthmean.Text = IniFile.IniReadValue(j.ToString(), "MeanWidth");
                this.txt_maskHeightmean.Text = IniFile.IniReadValue(j.ToString(), "MeanHeight");

            }
            else
            {
                this.txt_radiusmedian.Text = IniFile.IniReadValue(j.ToString(), "MedianRadius");
                if (IniFile.IniReadValue(j.ToString(), "MedianType") != "")
                    this.smooth_typemedian.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "MedianType"));
                if (IniFile.IniReadValue(j.ToString(), "MedianMargin") != "")
                    this.smooth_margin.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "MedianMargin"));


            }
           // this.smooth_image.Text = IniFile.IniReadValue(j.ToString(), "SmoothImage");
            this.smooth_imageMeanout.Text = IniFile.IniReadValue(j.ToString(), "SmoothImageout");
            this.smooth_image.Items.Clear();
            this.smooth_image.Items.Add(IniFile.IniReadValue(j.ToString(), "SmoothImage"));
            this.smooth_image.SelectedIndex = 0;
            Panel_show();


        }
        internal void SetParaImage(ExecuteBuffer test)
        {
            if (smooth_image.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    if(keyc.Contains(".img"))
                        smooth_image.Items.Add(keyc.Substring(0,keyc.Length-4));
                if (smooth_image.Items.Count > 0)
                    this.smooth_image.SelectedIndex = 0;

            }
            else
            {

                int m = 0;
                for (int i = 0; i < smooth_image.Items.Count;i++ )
                {
                    if (!test.imageBuffer.ContainsKey(smooth_image.Items[i].ToString()+".img"))
                        smooth_image.Items.Remove(smooth_image.Items[i].ToString());
                }
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < smooth_image.Items.Count; i++)
                    {
                        if (keyc == (smooth_image.Items[i].ToString()+".img"))
                            break;
                        m = i;
                        if (m == smooth_image.Items.Count - 1 && keyc.Contains(".img"))
                            smooth_image.Items.Add(keyc.Substring(0,keyc.Length-4));

                    }
                }


            }

        }




        public bool Run_Smooth(ExecuteBuffer _executeBuffer, out ExecuteBuffer outexecutebuffer)
        {

            outexecutebuffer = _executeBuffer;
            HObject regionOutresult;
            if (!_executeBuffer.imageBuffer.ContainsKey(this.smooth_image.Text.ToString()+".img"))
            {
                MessageBox.Show("平滑处理：  图像输入错误，请设置");
                return false;

            }

            if (!_executeBuffer.imageBuffer.ContainsKey(this.smooth_imageMeanout.Text.ToString() + ".img"))
            {
                MessageBox.Show("平滑处理：  输出图像未设置，请设置");
                return false;

            }
            if (this.comboBox_type.SelectedIndex == 0)
                HOperatorSet.GaussFilter(_executeBuffer.imageBuffer[this.smooth_image.Text.ToString()+".img"], out regionOutresult, Convert.ToInt32(this.txt_sizegauss.Text.ToString()));
            else  if (this.comboBox_type.SelectedIndex == 1)
                HOperatorSet.MeanImage(_executeBuffer.imageBuffer[this.smooth_image.Text.ToString()+".img"], out regionOutresult, Convert.ToInt32(this.txt_maskWidthmean.Text.ToString()), Convert.ToInt32(this.txt_maskHeightmean.Text.ToString()));
            else
              //  HOperatorSet.MedianImage(_executeBuffer.imageBuffer[this.smooth_image.Text.ToString()], out regionOutresult, "circle", 5, "mirrored");
                HOperatorSet.MedianImage(_executeBuffer.imageBuffer[this.smooth_image.Text.ToString()+".img"], out regionOutresult, this.smooth_typemedian.SelectedItem.ToString(), Convert.ToInt32(this.txt_radiusmedian.Text.ToString()), this.smooth_margin.SelectedItem.ToString());


            
            if (_executeBuffer.imageBuffer[this.smooth_imageMeanout.Text.ToString()+".img"] != null)
            {
                if (_executeBuffer.imageBuffer[this.smooth_imageMeanout.Text.ToString()+".img"].IsInitialized())
                {
                    _executeBuffer.imageBuffer[this.smooth_imageMeanout.Text.ToString()+".img"].Dispose();
                }
            }
            _executeBuffer.imageBuffer[this.smooth_imageMeanout.Text.ToString()+".img"] = regionOutresult;

            outexecutebuffer = _executeBuffer;
            return true;

        }

        internal void SetParaRegion(ExecuteBuffer test)
        {
            if (this.smooth_imageMeanout.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    if (keyc.Contains(".img"))
                        smooth_imageMeanout.Items.Add(keyc);

            }
            else
            {

                int m = 0;
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < smooth_imageMeanout.Items.Count; i++)
                    {
                        if (keyc == (smooth_imageMeanout.Items[i].ToString()+".img"))
                            break;
                        m = i;
                        if (m == smooth_imageMeanout.Items.Count - 1 && keyc.Contains(".img"))
                            smooth_imageMeanout.Items.Add(keyc.Substring(0,keyc.Length-4));

                    }
                }

            }

        }

        public string get_Region()
        {
            return this.smooth_imageMeanout.Text.ToString()+".img";
        }
        public string get_Imagename()
        {
            if (smooth_image.Text.ToString() != null)
                return this.smooth_image.Text.ToString()+".img";
            else
                return null;

            
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




        public bool Check_pal()
        {


             if (comboBox_type.SelectedIndex == 1)
            {
                if (!IsNumber(this.txt_maskWidthmean.Text.ToString()))
                {
                    MessageBox.Show("平滑处理：  maskWidth 输入不正确,请重新输入");
                    return false;
                }
                if (!IsNumber(this.txt_maskHeightmean.Text.ToString()))
                {
                    MessageBox.Show("平滑处理：  maskHeight 输入不正确,请重新输入");
                    return false;
                }

            }
             if (comboBox_type.SelectedIndex == 2)
            {
                if (!IsNumber(this.txt_radiusmedian.Text.ToString()))
                {
                    MessageBox.Show("平滑处理：  radius 输入不正确,请重新输入");
                    return false;
                }
            }
            if (this.smooth_image.Text == "")
            {
                MessageBox.Show("平滑处理：  输入图像image in为空,请选择图像");
                return false;
            }
            if (this.smooth_imageMeanout.Text == "")
            {
                MessageBox.Show("平滑处理：  输出图像image out为空,请输入");
                return false;
            }

            return true;

        }





        private void btn_Sure_Click(object sender, EventArgs e)
        {

            // UpdateThresholdTest.OnSendUpdateThresholdTest(new UpdateThresholdTestEventArgs(Threshold_image.SelectedItem.ToString(), thresold_region.Text.ToString(), trackBar_min.Value, trackBar_max.Value, false));
            UpdateSmoothTest.OnSendUpdateSmoothTest(new UpdateSmoothTestEventArgs(this.smooth_imageMeanout.Text+".img"));
            if (Check_pal())
                this.Visible = false;

        }









        private void comboBox_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel_show();
        }

    }




    









}
