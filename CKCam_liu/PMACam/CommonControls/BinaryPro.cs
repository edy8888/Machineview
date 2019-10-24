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
    public partial class BinaryPro : Form,ISerializable
    {


        public BinaryPro(ExecuteBuffer buffer, bool addbuffer)
        {
            InitializeComponent();
            if (addbuffer)
            {
                if (buffer != null)
                {
                    SetParaImage(buffer);
                 //   SetParaImage(buffer);

                }
            }
            this.cbb_features.SelectedIndex = 0;
            this.cbb_operation.SelectedIndex = 0;
            this.panel_参数设置2.Visible = false;
            this.panel_参数设置3.Visible = false;
        }
        public BinaryPro(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();
        this.cbb_features.SelectedIndex = 0;
        this.cbb_operation.SelectedIndex = 0;
        this.panel_参数设置2.Visible = false;
        this.panel_参数设置3.Visible = false;


            this.comboBox_form.SelectedIndex = (Int32)(info.GetValue("ComboxForm", typeof(Int32)));
         //   this.comboBox1.SelectedIndex = (Int32)(info.GetValue("Circle_color", typeof(Int32)));
           // this.cbb_measure_select.SelectedIndex = (Int32)(info.GetValue("Cbb_Measure_Select", typeof(Int32)));
            this.cbb_features.SelectedIndex = (Int32)(info.GetValue("FeatureIn", typeof(Int32)));
            this.cbb_operation.SelectedIndex = (Int32)(info.GetValue("OperationIn", typeof(Int32)));
            this.thresold_region.Text = (string)(info.GetValue("Output_region", typeof(string)));
            this.comboBox_imageout.Text = (string)(info.GetValue("Output_image", typeof(string)));
            this.trackBar_min.Value = (int)(info.GetValue("Threshold_minvalue", typeof(int)));
            this.trackBar_max.Value = (int)(info.GetValue("Threshold_maxvalue", typeof(int)));
            this.txt_min.Text = (string)(info.GetValue("MinValue", typeof(string)));
            this.txt_max.Text = (string)(info.GetValue("MaxValue", typeof(string)));
            if ((string)(info.GetValue("Check_filter", typeof(string))) == "True")
                this.checkBox_过滤.Checked = true;
            else
                this.checkBox_过滤.Checked = false;
            if ((string)(info.GetValue("Check_pre", typeof(string))) == "True")
                this.checkBox_预处理.Checked = true;
            else
                this.checkBox_预处理.Checked = false;
            if ((string)(info.GetValue("Check_black", typeof(string))) == "True")
                this.checkBox_black.Checked = true;
            else
                this.checkBox_black.Checked = false;
            if ((string)(info.GetValue("Check_show", typeof(string))) == "True")
                this.checkBox_show.Checked = true;
            else
                this.checkBox_show.Checked = false;
            if ((string)(info.GetValue("Check_dya", typeof(string))) == "True")
                this.checkBox_dya.Checked = true;
            else
                this.checkBox_dya.Checked = false;
            string out_image_string = (string)(info.GetValue("Image_string", typeof(string)));
            string[] condition = { "," };
            string[] result = out_image_string.Split(condition, StringSplitOptions.None);

            for (int m = 0; m < result.Count(); m++)
                Threshold_image.Items.Add(result[m]);
            Threshold_image.SelectedIndex = (Int32)(info.GetValue("Input_image", typeof(Int32)));
}

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("Input_image", this.Threshold_image.SelectedIndex.ToString());
            info.AddValue("ComboxForm", this.comboBox_form.SelectedIndex.ToString());
            info.AddValue("Output_region", this.thresold_region.Text.ToString());
            info.AddValue("Output_image", this.comboBox_imageout.Text.ToString());
            info.AddValue("Threshold_minvalue", this.trackBar_min.Value);
            info.AddValue("Threshold_maxvalue", this.trackBar_max.Value);
            info.AddValue("MorIterations", this.txt_iterations.Text.ToString());
            info.AddValue("MorStruct", this.mor_structElement.Text.ToString());
            info.AddValue("MorOperation", this.comboBox_form.SelectedIndex.ToString());
            info.AddValue("FeatureIn", this.cbb_features.SelectedIndex.ToString());
            info.AddValue("OperationIn", this.cbb_operation.SelectedIndex.ToString());
            info.AddValue("MinValue", this.txt_min.Text.ToString());
            info.AddValue("MaxValue", this.txt_max.Text.ToString());
            info.AddValue("Check_filter", this.checkBox_过滤.Checked.ToString());
            info.AddValue("Check_pre", this.checkBox_预处理.Checked.ToString());
            info.AddValue("Check_black", this.checkBox_black.Checked.ToString());
            info.AddValue("Check_show", this.checkBox_show.Checked.ToString());
            info.AddValue("Check_dya", this.checkBox_dya.Checked.ToString());
            string Inputimage_string = "";
            for (int m = 0; m < this.Threshold_image.Items.Count; m++)
            {
                if (m == this.Threshold_image.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Threshold_image.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Threshold_image.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string", Inputimage_string);

        } 
        public void Binary_ProImage(HImage Image, out HObject IImageReduced)
        {
            HOperatorSet.DualThreshold(Image, out IImageReduced, 17400, 200, 180);

        }

        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "Input_image", this.Threshold_image.Text);
            IniFile.IniWriteValue(j.ToString(), "Output_region", this.thresold_region.Text);
            IniFile.IniWriteValue(j.ToString(), "Output_image", this.comboBox_imageout.Text);
            IniFile.IniWriteValue(j.ToString(), "Threshold_minvalue",this.trackBar_min.Value.ToString());
            IniFile.IniWriteValue(j.ToString(), "Threshold_maxvalue", this.trackBar_max.Value.ToString());

            IniFile.IniWriteValue(j.ToString(), "MorIterations", this.txt_iterations.Text);
            IniFile.IniWriteValue(j.ToString(), "MorStruct", this.mor_structElement.Text);
            IniFile.IniWriteValue(j.ToString(), "MorOperation", this.comboBox_form.SelectedIndex.ToString());

            IniFile.IniWriteValue(j.ToString(), "FeatureIn", this.cbb_features.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "OperationIn", this.cbb_operation.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "MinValue", this.txt_min.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "MaxValue", this.txt_max.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Check_filter", this.checkBox_过滤.Checked .ToString());
            IniFile.IniWriteValue(j.ToString(), "Check_pre", this.checkBox_预处理.Checked.ToString());
            IniFile.IniWriteValue(j.ToString(), "Check_black", this.checkBox_black.Checked.ToString());
            IniFile.IniWriteValue(j.ToString(), "Check_show", this.checkBox_show.Checked.ToString());
        }

        internal void ReadData(List<string> n_Path, int j)
        {
   
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            this.thresold_region.Text = IniFile.IniReadValue(j.ToString(), "Output_region");
            this.comboBox_imageout.Text = IniFile.IniReadValue(j.ToString(), "Output_image");
            this.Threshold_image.Items.Clear();
            Threshold_image.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image"));
            Threshold_image.SelectedIndex = 0;
            if (IniFile.IniReadValue(j.ToString(), "Threshold_minvalue") != "")
                this.trackBar_min.Value  = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "Threshold_minvalue"));
            if (IniFile.IniReadValue(j.ToString(), "Threshold_maxvalue") != "")
            this.trackBar_max.Value  = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "Threshold_maxvalue"));


            mor_structElement.Items.Add(IniFile.IniReadValue(j.ToString(), "MorStruct"));
            mor_structElement.SelectedIndex = 0;
            int Selectnumber = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "MorOperation"));
            this.comboBox_form.SelectedIndex = Selectnumber;
            this.txt_iterations.Text = IniFile.IniReadValue(j.ToString(), "MorIterations");
            if (IniFile.IniReadValue(j.ToString(), "FeatureIn") != "")
                this.cbb_features.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "FeatureIn"));
            if (IniFile.IniReadValue(j.ToString(), "OperationIn") != "")
                this.cbb_operation.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "OperationIn"));
            this.txt_min.Text = IniFile.IniReadValue(j.ToString(), "MinValue");
            this.txt_max.Text = IniFile.IniReadValue(j.ToString(), "MaxValue");

            if (IniFile.IniReadValue(j.ToString(), "Check_filter") == "True")
                this.checkBox_过滤.Checked = true;
            else
                this.checkBox_过滤.Checked = false;
            if (IniFile.IniReadValue(j.ToString(), "Check_pre") == "True")
                this.checkBox_预处理.Checked = true;
            else
                this.checkBox_预处理.Checked = false;
            if (IniFile.IniReadValue(j.ToString(), "Check_black") == "True")
                this.checkBox_black.Checked = true;
            else
                this.checkBox_black.Checked = false;
            if (IniFile.IniReadValue(j.ToString(), "Check_show") == "True")
                this.checkBox_show.Checked = true;
            else
                this.checkBox_show.Checked = false;
        }
        internal void SetParaImage(ExecuteBuffer test)
        {
            int defaultnumber = 0;
            int get_number = 0;
            if (Threshold_image.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    if (keyc.Contains(".img"))
                    {
                        Threshold_image.Items.Add(keyc.Substring(0, keyc.Length - 4));
                        if (keyc.Contains("image"))
                        {
                            get_number = Convert.ToInt32(keyc.Substring(5, keyc.Length - 9));

                            if (get_number > defaultnumber)
                            {
                                Threshold_image.Text = keyc.Substring(0, keyc.Length - 4);
                                defaultnumber = get_number;
                            }
                        }
                    }

               //     if (keyc.Contains("image1"))
                    //    Threshold_image.Text = keyc;
                }

            }
            else
            {

                int m = 0;

                if (Threshold_image.SelectedItem != null)
                {
                    if (!test.imageBuffer.ContainsKey(Threshold_image.SelectedItem.ToString()+".img"))
                        Threshold_image.Items.Remove(Threshold_image.SelectedItem.ToString()+".img");
                }
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < Threshold_image.Items.Count; i++)
                    {
                        if (keyc == (Threshold_image.Items[i].ToString()+".img"))
                            break;
                        m = i;
                        if (m == Threshold_image.Items.Count - 1 && keyc.Contains(".img"))
                            Threshold_image.Items.Add(keyc.Substring(0,keyc.Length-4));

                    }
                }


            }


            if (mor_structElement.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    if (keyc.Contains(".region"))
                    {
                        mor_structElement.Items.Add(keyc.Substring(0,keyc.Length-7));
                        if (keyc.Contains("ROI") && keyc.Contains(".region"))
                            mor_structElement.Text = keyc.Substring(0, keyc.Length - 7);

                    }


                }
            }
            else
            {

                int m = 0;
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < mor_structElement.Items.Count; i++)
                    {
                        if (keyc == mor_structElement.Items[i].ToString()+ ".region")
                            break;
                        m = i;
                        if (m == mor_structElement.Items.Count - 1 && keyc.Contains(".region"))
                            mor_structElement.Items.Add(keyc.Substring(0,keyc.Length-7));

                    }
                }


            }

        }

        internal void SetParaRegion(ExecuteBuffer test)
        {
            if (this.thresold_region.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    if (keyc.Contains(".region"))
                        thresold_region.Items.Add(keyc.Substring(0,keyc.Length-7));

            }
            else
            {

                int m = 0;

                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < thresold_region.Items.Count; i++)
                    {
                        if (keyc == (thresold_region.Items[i].ToString()+".region"))
                            break;
                        m = i;
                        if (m == thresold_region.Items.Count - 1 && keyc.Contains(".region"))
                            thresold_region.Items.Add(keyc.Substring(0,keyc.Length-7));

                    }
                }

            }

            if (this.comboBox_imageout.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    if (keyc.Contains(".img"))
                        comboBox_imageout.Items.Add(keyc.Substring(0,keyc.Length-4));

            }
            else
            {

                int m = 0;

                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < comboBox_imageout.Items.Count; i++)
                    {
                        if (keyc == (comboBox_imageout.Items[i].ToString()+ ".img"))
                            break;
                        m = i;
                        if (m == comboBox_imageout.Items.Count - 1 && keyc.Contains(".img"))
                            comboBox_imageout.Items.Add(keyc.Substring(0,keyc.Length-4));

                    }
                }

            }

        }

        public bool getshow()
        {
            if (this.checkBox_show.Checked)
                return true;
            else
                return false;
        }

        public string get_Region()
        {
            return this.thresold_region.Text.ToString()+".region";
        }
        public string get_Imageout()
        {
            return this.comboBox_imageout.Text.ToString()+".img";
        }
        public string get_Imagename()
        {
            if (Threshold_image.SelectedItem != null)
                return this.Threshold_image.SelectedItem.ToString()+".img";
            else
                return null;

            
        }

        public void Binary_ProImage1(HImage Image, out HObject IImageReduced)
        {
            HObject xx;
            HOperatorSet.GenEmptyObj(out xx);
            HOperatorSet.GenEmptyObj(out IImageReduced);
            HOperatorSet.DualThreshold(Image, out xx, 1740, 200, 180);
            HOperatorSet.ReduceDomain(Image, xx, out IImageReduced);
            xx.Dispose();

            

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

            if (this.Threshold_image.Text == "")
            {
                MessageBox.Show("二值化处理: 输入图像image in为空,请选择图像");
                return false;
            }
            if (this.thresold_region.Text == "")
            {
                MessageBox.Show("二值化处理: 输出图像区域region out为空,请输入");
                return false;
            }
            if (this.comboBox_imageout.Text == "")
            {
                MessageBox.Show("二值化处理: 输出图像image out为空,请输入");
                return false;
            }
            if (Threshold_image.SelectedItem.ToString() == thresold_region.Text.ToString())
            {
                MessageBox.Show("二值化处理: 输入图像与输出参数不能同名,请重新设置");
                return false;
            }

            if (this.checkBox_预处理.Checked)
            {
                if (this.mor_structElement.Text == "")
                {
                    MessageBox.Show("二值化处理: 输入结构structElement为空,请输入");
                    return false;
                }

                if (!IsNumber(this.txt_iterations.Text.ToString()))
                {
                    MessageBox.Show("二值化处理: 输入iterations不是数字,请重新输入");
                    return false;
                }
            }
            if (this.checkBox_过滤.Checked) 
            {

                if (!IsNumber(this.txt_min.Text.ToString()))
                {
                    MessageBox.Show("二值化处理: 输入最小值min不是数字,请重新输入");
                    return false;
                }
                if (!IsNumber(this.txt_max.Text.ToString()))
                {
                    MessageBox.Show("二值化处理: 输入最大值max不是数字,请重新输入");
                    return false;
                }
            }


            return true;

        }



        public void sendevent()
        {
            this.thresold_region.SelectedValue = this.thresold_region.Text;

            if (Check_pal())
            {
                UpdateThresholdTest.OnSendUpdateThresholdTest(new UpdateThresholdTestEventArgs(Threshold_image.SelectedItem.ToString(), thresold_region.Text.ToString() + ".region", comboBox_imageout.Text.ToString() + ".img", trackBar_min.Value, trackBar_max.Value, false));
            //    this.Visible = false;
            }
        }




        private void btn_Sure_Click(object sender, EventArgs e)
        {

            this.thresold_region.SelectedValue = this.thresold_region.Text;

            if (Check_pal())
            {
                UpdateThresholdTest.OnSendUpdateThresholdTest(new UpdateThresholdTestEventArgs(Threshold_image.SelectedItem.ToString(), thresold_region.Text.ToString() + ".region", comboBox_imageout.Text.ToString() + ".img", trackBar_min.Value, trackBar_max.Value, false));
                this.Visible = false;
            }
                
        }

        private void txt_minGray_TextChanged(object sender, EventArgs e)
        {
            
        }

        public bool RunThresholdTest(HWndCtrl hWndCtrl,HObject imagein, out HObject hregion)
        {
            HObject region;     
            HOperatorSet.GenEmptyObj(out region);
            hregion = null;
            if (imagein == null || !imagein.IsInitialized())
            {
                MessageBox.Show("二值化处理: image参数为空或者未赋值");
                return false;
            }
            int minvalue = Convert.ToInt32(this.trackBar_min.Value);
            int maxvalue = Convert.ToInt32(this.trackBar_max.Value);
            HOperatorSet.Threshold(imagein, out region, minvalue, maxvalue);
            if (hregion != null)
            {
                if (hregion.IsInitialized())
                {
                    hregion.Dispose();
                }
            }

          //  HObject xy;
          //  HOperatorSet.ReduceDomain(imagein, region, out xy);
            hregion = region;
            return false;
        }



        public bool RunThresholdTest_new(ExecuteBuffer _executeBuffer, out ExecuteBuffer outexecutebuffer,out 斑点GVName_halcon Blob_result, out string result_info)
        {

            outexecutebuffer = _executeBuffer;
            result_info = "";
            HObject region;
            HObject outimagename;
            HOperatorSet.GenEmptyObj(out region);
            HOperatorSet.GenEmptyObj(out outimagename);
            Blob_result = new 斑点GVName_halcon();
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Threshold_image.SelectedItem.ToString()+".img"))
            {
                MessageBox.Show("二值化处理: 输入图像已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Threshold_image.SelectedItem.ToString()+".img"] == null)
            {
                MessageBox.Show("二值化处理: image参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像已经不存在，请重置设置输入图像";
                return false;
            }
            HTuple width, height;

            String thresold_regionname = this.thresold_region.Text.ToString() + ".region";
            HOperatorSet.GetImageSize(_executeBuffer.imageBuffer[this.Threshold_image.SelectedItem.ToString()+".img"], out width, out height);
            int minvalue = Convert.ToInt32(this.trackBar_min.Value);
            int maxvalue = Convert.ToInt32(this.trackBar_max.Value);
            HOperatorSet.Threshold(_executeBuffer.imageBuffer[this.Threshold_image.SelectedItem.ToString()+".img"], out region, minvalue, maxvalue);

            if (_executeBuffer.imageBuffer[thresold_regionname] != null)
            {
                if (_executeBuffer.imageBuffer[thresold_regionname].IsInitialized())
                {
                    _executeBuffer.imageBuffer[thresold_regionname].Dispose();
                }
            }
            string comboBox_imageoutname = this.comboBox_imageout.Text.ToString() + ".img";
            if (_executeBuffer.imageBuffer[comboBox_imageoutname] != null)
            {
                if (_executeBuffer.imageBuffer[comboBox_imageoutname].IsInitialized())
                {
                    _executeBuffer.imageBuffer[comboBox_imageoutname].Dispose();
                }
            }
            HObject  regionOutresult = new HObject();
            if (this.checkBox_预处理.Checked) 
            {

                if (_executeBuffer.imageBuffer[this.mor_structElement.Text.ToString() + ".region"] == null)
                {
                    MessageBox.Show("二值化处理: 生成ROI区域为空，请先运行生成ROI");
                    result_info = " 二值化处理: 生成ROI区域为空，请先运行生成ROI";
                    return false;
                }
                HObject Form_regionout;
                HOperatorSet.GenEmptyObj(out Form_regionout);
                if (this.comboBox_form.SelectedIndex == 0)
                    HOperatorSet.Opening(region, _executeBuffer.imageBuffer[this.mor_structElement.Text.ToString()+".region"], out Form_regionout);
                else if (this.comboBox_form.SelectedIndex == 1)
                    HOperatorSet.Closing(region, _executeBuffer.imageBuffer[this.mor_structElement.Text.ToString()+".region"], out Form_regionout);
                else if (this.comboBox_form.SelectedIndex == 2)
                    HOperatorSet.Erosion1(region, _executeBuffer.imageBuffer[this.mor_structElement.Text.ToString()+".region"], out Form_regionout, Convert.ToInt32(this.txt_iterations.Text.ToString()));
                else
                    HOperatorSet.Dilation1(region, _executeBuffer.imageBuffer[this.mor_structElement.Text.ToString()+".region"], out Form_regionout, Convert.ToInt32(this.txt_iterations.Text.ToString()));

                
            region = Form_regionout;
            }


            if (this.checkBox_过滤.Checked)
            {
                HObject regionconOutresult;
                HOperatorSet.GenEmptyObj(out regionconOutresult);
                HOperatorSet.GenEmptyObj(out regionOutresult);
                HOperatorSet.Connection(region, out regionconOutresult);
                HOperatorSet.SelectShape(regionconOutresult, out regionOutresult, this.cbb_features.SelectedItem.ToString(), this.cbb_operation.SelectedItem.ToString(), Convert.ToInt32(this.txt_min.Text.ToString()), Convert.ToInt32(this.txt_max.Text.ToString()));
               // _executeBuffer.imageBuffer[this.thresold_region.Text.ToString()] = regionOutresult;
                

               // region = regionOutresult;
                if (regionOutresult.CountObj() != 0)
                    region = regionOutresult;
                else
                {
                    MessageBox.Show("二值化处理: 过滤了所有区域，请重新选择");
                    result_info = " 二值化处理: 过滤操作后滤完所有区域，请重新设置";
                    region.Dispose();
                    regionOutresult.Dispose();


                    return false;
                }

              //  regionconOutresult.Dispose();

            }

            _executeBuffer.imageBuffer[thresold_regionname] = region;


            HTuple area,Row1,Col1,Way;
            HOperatorSet.AreaCenter(region, out area, out Row1, out Col1);
            Blob_result.域中心X = Col1;
            Blob_result.域中心Y = Row1;
            Blob_result.域面积 = area;
            HOperatorSet.OrientationRegion(region, out Way);
            Blob_result.域角度 = Way;
            label_number.Text = region.CountObj().ToString();
          //  MessageBox.Show(region.CountObj().ToString());
            if(this.checkBox_black.Checked)
                HOperatorSet.RegionToBin(region, out outimagename, 0, 255, width, height);
            else
                HOperatorSet.RegionToBin(region, out outimagename, 255, 0, width, height);

         //   region.Dispose();
          //  regionOutresult.Dispose();
            _executeBuffer.imageBuffer[comboBox_imageoutname] = outimagename;
            






            outexecutebuffer = _executeBuffer;
            
            result_info = " 二值化处理: 完成";


            return true;
        }






        private void trackBar_min_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar_min_ValueChanged(object sender, EventArgs e)
        {
           if (checkBox_dya.Checked)
            {
                if (this.Threshold_image.SelectedItem.ToString() == "" || this.Threshold_image == null)
                {
                    MessageBox.Show("二值化处理： 请选择输入图像");
                    return;
                }

                if (this.thresold_region == null && this.thresold_region.Text == "")
                {
                    MessageBox.Show("二值化处理： 请选择输出图像");
                    return;
                }

                if (this.trackBar_min.Value > this.trackBar_max.Value)
                {
                    MessageBox.Show("二值化处理： MinValue 不能超过MaxValue");
                    return;
                }
                UpdateThresholdTest.OnSendUpdateThresholdTest(new UpdateThresholdTestEventArgs(Threshold_image.SelectedItem.ToString()+".img", thresold_region.Text.ToString()+".region",comboBox_imageout.Text.ToString()+".img", trackBar_min.Value, trackBar_max.Value, true));  
            
            }
        }

        private void trackBar_max_Validating(object sender, CancelEventArgs e)
        {

        }

        private void trackBar_max_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox_dya.Checked)
            {
                if (this.Threshold_image.SelectedItem.ToString() == "" || this.Threshold_image == null)
                {
                    MessageBox.Show("二值化处理： 请选择输入图像");
                    return;
                }

                if (this.thresold_region == null && this.thresold_region.Text == "")
                {
                    MessageBox.Show("二值化处理： 请选择输出图像");
                    return;
                }

                if (this.trackBar_min.Value > this.trackBar_max.Value)
                {
                    MessageBox.Show("二值化处理： MinValue 不能超过MaxValue");
                    return;
                }
                UpdateThresholdTest.OnSendUpdateThresholdTest(new UpdateThresholdTestEventArgs(Threshold_image.SelectedItem.ToString()+".img", thresold_region.Text.ToString()+".region",comboBox_imageout.Text.ToString()+".img", trackBar_min.Value, trackBar_max.Value, true));

            }
        }

        public bool Send_name()
        {
            if (Threshold_image.Items.Count == 0)
                return false;
            if (Threshold_image.SelectedItem == null)
                return false;
            UpdateThresholdTest.OnSendUpdateThresholdTest(new UpdateThresholdTestEventArgs(Threshold_image.SelectedItem.ToString()+".img", thresold_region.Text.ToString()+".region", comboBox_imageout.Text.ToString()+".img", trackBar_min.Value, trackBar_max.Value, false));
            return true;

        }




        private void button_参数设置3_Click(object sender, EventArgs e)
        {
            if (panel_参数设置3.Visible)
            {
                panel_参数设置3.Visible = false;
                button_参数设置2.Top = button_参数设置3.Bottom;

            }
            else
            {
                panel_参数设置3.Visible = true;
                panel_参数设置3.Top = button_参数设置3.Bottom;

                panel_参数设置2.Visible = false;
                button_参数设置2.Top = panel_参数设置3.Bottom;


            }
        }

        private void button_参数设置2_Click(object sender, EventArgs e)
        {
            if (panel_参数设置2.Visible)
            {
                panel_参数设置2.Visible = false;

            }
            else
            {
                panel_参数设置2.Visible = true;



                if (panel_参数设置3.Visible)
                {
                    button_参数设置2.Top = button_参数设置3.Bottom;
                }
                panel_参数设置2.Top = button_参数设置2.Bottom;

                panel_参数设置3.Visible = false;

            }
        }

        private void mor_structElement_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }




    










}
