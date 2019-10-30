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
    public partial class PuzzlePicture : Form, ISerializable
    {


        public PuzzlePicture(ExecuteBuffer buffer, bool addbuffer)
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

        }

        public void Binary_ProImage(HImage Image, out HObject IImageReduced)
        {
            HOperatorSet.DualThreshold(Image, out IImageReduced, 17400, 200, 180);

        }

        public PuzzlePicture(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();

            this.textBox_x.Text = (string)(info.GetValue("Txt_x", typeof(string)));
            this.textBox_y.Text = (string)(info.GetValue("Txt_y", typeof(string)));
            this.textBox_bi.Text = (string)(info.GetValue("Txt_bi", typeof(string)));
            this.Output_image.Text = (string)(info.GetValue("Imageout", typeof(string)));
            this.textBox_dian.Text = (string)(info.GetValue("PointNumber", typeof(string)));

            string out_image_string = (string)(info.GetValue("Image_string1", typeof(string)));
            string[] condition = { "," };
            string[] result = out_image_string.Split(condition, StringSplitOptions.None);

            for (int m = 0; m < result.Count(); m++)
                Input_image1.Items.Add(result[m]);

            
             out_image_string = (string)(info.GetValue("Image_string2", typeof(string)));
             string[] result1  = out_image_string.Split(condition, StringSplitOptions.None);
             for (int m = 0; m < result.Count(); m++)
                 Input_image2.Items.Add(result1[m]);

             out_image_string = (string)(info.GetValue("Image_string3", typeof(string)));
             string[] result2 = out_image_string.Split(condition, StringSplitOptions.None);
             for (int m = 0; m < result.Count(); m++)
                Input_image3.Items.Add(result2[m]);




             out_image_string = (string)(info.GetValue("Image_string4", typeof(string)));
             string[] result3 = out_image_string.Split(condition, StringSplitOptions.None);
             for (int m = 0; m < result.Count(); m++)
                 Input_image4.Items.Add(result3[m]);



             out_image_string = (string)(info.GetValue("Image_string5", typeof(string)));
             string[] result4 = out_image_string.Split(condition, StringSplitOptions.None);
             for (int m = 0; m < result.Count(); m++)
                 Input_image5.Items.Add(result4[m]);

             out_image_string = (string)(info.GetValue("Image_string6", typeof(string)));
             string[] result5 = out_image_string.Split(condition, StringSplitOptions.None);
             for (int m = 0; m < result.Count(); m++)
                 Input_image6.Items.Add(result5[m]);

             out_image_string = (string)(info.GetValue("Image_string7", typeof(string)));
             string[] result6 = out_image_string.Split(condition, StringSplitOptions.None);
             for (int m = 0; m < result.Count(); m++)
                 Input_image7.Items.Add(result6[m]);


             out_image_string = (string)(info.GetValue("Image_string8", typeof(string)));
             string[] result7 = out_image_string.Split(condition, StringSplitOptions.None);
             for (int m = 0; m < result.Count(); m++)
                 Input_image8.Items.Add(result7[m]);

             out_image_string = (string)(info.GetValue("Image_string9", typeof(string)));
             string[] result8 = out_image_string.Split(condition, StringSplitOptions.None);
             for (int m = 0; m < result.Count(); m++)
                 Input_image9.Items.Add(result8[m]);
            Input_image1.SelectedIndex = (Int32)(info.GetValue("InputImage_1", typeof(Int32)));
            Input_image2.SelectedIndex = (Int32)(info.GetValue("InputImage_2", typeof(Int32)));
            Input_image3.SelectedIndex = (Int32)(info.GetValue("InputImage_3", typeof(Int32)));
            Input_image4.SelectedIndex = (Int32)(info.GetValue("InputImage_4", typeof(Int32)));
            Input_image5.SelectedIndex = (Int32)(info.GetValue("InputImage_5", typeof(Int32)));
            Input_image6.SelectedIndex = (Int32)(info.GetValue("InputImage_6", typeof(Int32)));
            Input_image7.SelectedIndex = (Int32)(info.GetValue("InputImage_7", typeof(Int32)));
            Input_image8.SelectedIndex = (Int32)(info.GetValue("InputImage_8", typeof(Int32)));
            Input_image9.SelectedIndex = (Int32)(info.GetValue("InputImage_9", typeof(Int32)));


} 
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("Txt_x", this.textBox_x.Text.ToString());
            info.AddValue("Txt_y", this.textBox_y.Text.ToString());
            info.AddValue("Txt_bi", this.textBox_bi.Text.ToString());
            info.AddValue("Imageout", this.Output_image.Text.ToString());
            info.AddValue("PointNumber", this.textBox_dian.Text.ToString());
            info.AddValue("InputImage_1", this.Input_image1.SelectedIndex.ToString());
            info.AddValue("InputImage_2", this.Input_image2.SelectedIndex.ToString());
            info.AddValue("InputImage_3", this.Input_image3.SelectedIndex.ToString());
            info.AddValue("InputImage_4", this.Input_image4.SelectedIndex.ToString());
            info.AddValue("InputImage_5", this.Input_image5.SelectedIndex.ToString());
            info.AddValue("InputImage_6", this.Input_image6.SelectedIndex.ToString());
            info.AddValue("InputImage_7", this.Input_image7.SelectedIndex.ToString());
            info.AddValue("InputImage_8", this.Input_image8.SelectedIndex.ToString());
            info.AddValue("InputImage_9", this.Input_image9.SelectedIndex.ToString());

            string Inputimage_string = "";
            for (int m = 0; m < this.Input_image1.Items.Count; m++)
            {
                if (m == this.Input_image1.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Input_image1.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Input_image1.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string1", Inputimage_string);
             Inputimage_string = "";
            for (int m = 0; m < this.Input_image2.Items.Count; m++)
            {
                if (m == this.Input_image2.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Input_image2.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Input_image2.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string2", Inputimage_string);
            Inputimage_string = "";
            for (int m = 0; m < this.Input_image3.Items.Count; m++)
            {
                if (m == this.Input_image3.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Input_image3.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Input_image3.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string3", Inputimage_string);

            Inputimage_string = "";
            for (int m = 0; m < this.Input_image4.Items.Count; m++)
            {
                if (m == this.Input_image4.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Input_image4.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Input_image4.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string4", Inputimage_string);

            Inputimage_string = "";
            for (int m = 0; m < this.Input_image5.Items.Count; m++)
            {
                if (m == this.Input_image5.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Input_image5.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Input_image5.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string5", Inputimage_string);


            Inputimage_string = "";
            for (int m = 0; m < this.Input_image6.Items.Count; m++)
            {
                if (m == this.Input_image6.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Input_image6.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Input_image6.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string6", Inputimage_string);

            Inputimage_string = "";
            for (int m = 0; m < this.Input_image7.Items.Count; m++)
            {
                if (m == this.Input_image7.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Input_image7.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Input_image7.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string7", Inputimage_string);

            Inputimage_string = "";
            for (int m = 0; m < this.Input_image8.Items.Count; m++)
            {
                if (m == this.Input_image8.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Input_image8.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Input_image8.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string8", Inputimage_string);


            Inputimage_string = "";
            for (int m = 0; m < this.Input_image9.Items.Count; m++)
            {
                if (m == this.Input_image9.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Input_image9.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Input_image9.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string9", Inputimage_string);
        } 
        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "Input_image1", this.Input_image1.Text);
            IniFile.IniWriteValue(j.ToString(), "Input_image2", this.Input_image2.Text);
            IniFile.IniWriteValue(j.ToString(), "Input_image3", this.Input_image3.Text);
            IniFile.IniWriteValue(j.ToString(), "Input_image4", this.Input_image4.Text);
            IniFile.IniWriteValue(j.ToString(), "Input_image5", this.Input_image5.Text);
            IniFile.IniWriteValue(j.ToString(), "Input_image6", this.Input_image6.Text);
            IniFile.IniWriteValue(j.ToString(), "Input_image7", this.Input_image7.Text);
            IniFile.IniWriteValue(j.ToString(), "Input_image8", this.Input_image8.Text);
            IniFile.IniWriteValue(j.ToString(), "Input_image9", this.Input_image9.Text);
            IniFile.IniWriteValue(j.ToString(), "Txt_x", this.textBox_x.Text);
            IniFile.IniWriteValue(j.ToString(), "Txt_y", this.textBox_y.Text);
            IniFile.IniWriteValue(j.ToString(), "Txt_bi", this.textBox_bi.Text);
            IniFile.IniWriteValue(j.ToString(), "Imageout", this.Output_image.Text);
            IniFile.IniWriteValue(j.ToString(), "PointNumber", this.textBox_dian.Text);
        }

        internal void ReadData(List<string> n_Path, int j)
        {
   
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            this.Output_image.Text = IniFile.IniReadValue(j.ToString(), "Imageout");
            this.Input_image1.Items.Clear();
            Input_image1.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image1"));
            Input_image1.SelectedIndex = 0;

            this.Input_image2.Items.Clear();
            Input_image2.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image2"));
            Input_image2.SelectedIndex = 0;

            this.Input_image3.Items.Clear();
            Input_image3.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image3"));
            Input_image3.SelectedIndex = 0;

            this.Input_image4.Items.Clear();
            Input_image4.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image4"));
            Input_image4.SelectedIndex = 0;

            this.Input_image5.Items.Clear();
            Input_image5.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image5"));
            Input_image5.SelectedIndex = 0;

            this.Input_image6.Items.Clear();
            Input_image6.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image6"));
            Input_image6.SelectedIndex = 0;

            this.Input_image7.Items.Clear();
            Input_image7.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image7"));
            Input_image7.SelectedIndex = 0;

            this.Input_image8.Items.Clear();
            Input_image8.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image8"));
            Input_image8.SelectedIndex = 0;

            this.Input_image9.Items.Clear();
            Input_image9.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image9"));
            Input_image9.SelectedIndex = 0;
            this.textBox_x.Text = IniFile.IniReadValue(j.ToString(), "Txt_x");
            this.textBox_y.Text = IniFile.IniReadValue(j.ToString(), "Txt_y");
            this.textBox_bi.Text = IniFile.IniReadValue(j.ToString(), "Txt_bi");
            if (IniFile.IniReadValue(j.ToString(), "PointNumber") != "")
                this.textBox_dian.Text = IniFile.IniReadValue(j.ToString(), "PointNumber");
        }


        internal void SetParaImage(ExecuteBuffer test)
        {

            SetNewParaImage(test, Input_image1); 
            SetNewParaImage(test, Input_image2);
            SetNewParaImage(test, Input_image3);
            SetNewParaImage(test, Input_image4);
            SetNewParaImage(test, Input_image5);
            SetNewParaImage(test, Input_image6);
            SetNewParaImage(test, Input_image7);
            SetNewParaImage(test, Input_image8);
            SetNewParaImage(test, Input_image9);
        }

        internal void SetNewParaImage(ExecuteBuffer test, ComboBox x1)
        {
            int defaultnumber = 0;
            int get_number = 0;
            if (x1.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    if (keyc.Contains(".img"))
                    {
                        x1.Items.Add(keyc.Substring(0, keyc.Length - 4));
                        if (keyc.Contains("image"))
                        {
                            get_number = Convert.ToInt32(keyc.Substring(5, keyc.Length - 9));

                            if (get_number > defaultnumber)
                            {
                                x1.Text = keyc.Substring(0, keyc.Length - 4);
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

                if (x1.SelectedItem != null)
                {
                    if (!test.imageBuffer.ContainsKey(x1.SelectedItem.ToString() + ".img"))
                        x1.Items.Remove(x1.SelectedItem.ToString() + ".img");
                }
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < x1.Items.Count; i++)
                    {
                        if (keyc == (x1.Items[i].ToString() + ".img"))
                            break;
                        m = i;
                        if (m == x1.Items.Count - 1 && keyc.Contains(".img"))
                            x1.Items.Add(keyc.Substring(0, keyc.Length - 4));

                    }
                }


            }
        }

        internal void SetParaRegion(ExecuteBuffer test)
        {


            if (this.Output_image.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    if (keyc.Contains(".img"))
                        Output_image.Items.Add(keyc.Substring(0,keyc.Length-4));

            }
            else
            {

                int m = 0;

                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < Output_image.Items.Count; i++)
                    {
                        if (keyc == (Output_image.Items[i].ToString()+ ".img"))
                            break;
                        m = i;
                        if (m == Output_image.Items.Count - 1 && keyc.Contains(".img"))
                            Output_image.Items.Add(keyc.Substring(0,keyc.Length-4));

                    }
                }

            }

        }

        public bool getshow()
        {
            return true;
        }

        public string get_Region()
        {
            return "";
        }
        public string get_Imageout()
        {
            return this.Output_image.Text.ToString()+".img";
        }
        public string get_Imagename()
        {
            if (Input_image1.SelectedItem != null)
                return this.Input_image1.SelectedItem.ToString()+".img";
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



            return true;

        }







        private void btn_Sure_Click(object sender, EventArgs e)
        {
            UpdatePuzzlePicture.OnSendUpdatePuzzlePicture(new UpdatePuzzlePictureEventArgs(this.Output_image.Text.ToString() + ".img"));
            
            if (Check_pal())
                this.Visible = false;

                
        }

        private void txt_minGray_TextChanged(object sender, EventArgs e)
        {
            
        }

        public bool RunThresholdTest(HWndCtrl hWndCtrl,HObject imagein, out HObject hregion)
        {
            hregion = new HObject(); ;
            return false;
        }

        public bool Puzzle_tezheng(ExecuteBuffer _executeBuffer, out ExecuteBuffer outexecutebuffer, out string result_info, Dictionary<int, PointName> Pointlist)
        {
            outexecutebuffer = _executeBuffer;
            result_info = "";
            HObject image1, image2, image3, image4, image5, image6, image7, image8, image9;

            double X_shu = Convert.ToDouble(this.textBox_x.Text.ToString()) / Convert.ToDouble(this.textBox_bi.Text.ToString());
            double Y_shu = Convert.ToDouble(this.textBox_y.Text.ToString()) / Convert.ToDouble(this.textBox_bi.Text.ToString());
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image1.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像1已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像1已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image1.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image1参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像1已经不存在，请重置设置输入图像";
                return false;
            }

            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image2.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像2已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像2已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image2.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image2参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像2已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image3.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像3已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像3已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image3.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image3参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像3已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image4.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像4已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像4已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image4.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image4参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像4已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image5.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像5已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像5已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image5.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image5参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像5已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image6.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像6已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像6已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image6.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image6参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像6已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image7.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像7已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像7已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image7.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image7参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像7已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image8.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像8已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像8已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image8.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image8参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像8已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image9.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像9已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像9已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image9.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image9参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像9已经不存在，请重置设置输入图像";
                return false;
            }
            string comboBox_imageoutname = this.Output_image.Text.ToString() + ".img";
            if (_executeBuffer.imageBuffer[comboBox_imageoutname] != null)
            {
                if (_executeBuffer.imageBuffer[comboBox_imageoutname].IsInitialized())
                {
                    _executeBuffer.imageBuffer[comboBox_imageoutname].Dispose();
                }
            }
            HObject imagex;
            HOperatorSet.GenEmptyObj(out imagex);
        //    image1 = _executeBuffer.imageBuffer[this.Input_image1.SelectedItem.ToString() + ".img"];
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image1.SelectedItem.ToString() + ".img"], out image1);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image2.SelectedItem.ToString() + ".img"], out image2);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image3.SelectedItem.ToString() + ".img"], out image3);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image4.SelectedItem.ToString() + ".img"], out image4);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image5.SelectedItem.ToString() + ".img"], out image5);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image6.SelectedItem.ToString() + ".img"], out image6);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image7.SelectedItem.ToString() + ".img"], out image7);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image8.SelectedItem.ToString() + ".img"], out image8);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image9.SelectedItem.ToString() + ".img"], out image9);
            /*
            image2 = _executeBuffer.imageBuffer[this.Input_image2.SelectedItem.ToString() + ".img"];
            image3 = _executeBuffer.imageBuffer[this.Input_image3.SelectedItem.ToString() + ".img"];
            image4 = _executeBuffer.imageBuffer[this.Input_image4.SelectedItem.ToString() + ".img"];
            image5 = _executeBuffer.imageBuffer[this.Input_image5.SelectedItem.ToString() + ".img"];
            image6 = _executeBuffer.imageBuffer[this.Input_image6.SelectedItem.ToString() + ".img"];
            image7 = _executeBuffer.imageBuffer[this.Input_image7.SelectedItem.ToString() + ".img"];
            image8 = _executeBuffer.imageBuffer[this.Input_image8.SelectedItem.ToString() + ".img"];
            image9 = _executeBuffer.imageBuffer[this.Input_image9.SelectedItem.ToString() + ".img"];
             * */
            HOperatorSet.ConcatObj(imagex,image1, out imagex);
            HOperatorSet.ConcatObj(imagex, image2, out imagex);
            HOperatorSet.ConcatObj(imagex, image3, out imagex);
            HOperatorSet.ConcatObj(imagex, image4, out imagex);
            HOperatorSet.ConcatObj(imagex, image5, out imagex);
            HOperatorSet.ConcatObj(imagex, image6, out imagex);
            HOperatorSet.ConcatObj(imagex, image7, out imagex);
            HOperatorSet.ConcatObj(imagex, image8, out imagex);
            HOperatorSet.ConcatObj(imagex, image9, out imagex);
            HObject ho_TiledImage;
            HOperatorSet.GenEmptyObj(out ho_TiledImage);
            HTuple width, height;
            HOperatorSet.GetImageSize(image1, out width, out height);
            HTuple Pal_1= new HTuple(), Pal_2=new HTuple(), Pal_3=new HTuple();
            for (int i = 0; i < 9; i++)
            {
                Pal_3.Append(-1);
                if (i % 3 == 0)
                   Pal_2.Append(0);
                else if(i % 3 == 1)
                    Pal_2.Append(width + 20);
                else
                    Pal_2.Append(2*width + 40);
                if ((int)i / 3 < 1)
                    Pal_1.Append(0);
                else if ((int)i / 3 < 2)
                    Pal_1.Append(height + 20);
                else

                    Pal_1.Append(2*height + 40);
            
            }

            HOperatorSet.TileImagesOffset(imagex, out ho_TiledImage, Pal_1, Pal_2, Pal_3, Pal_3, Pal_3, Pal_3,(width*3+2*20), height*3+2*20);
            HTuple From = new HTuple(1,1,1,2,2,2,3,4,4,4,5,5,5,6);
            HTuple End = new HTuple(4, 5, 2, 5, 6, 3, 6,7,8,5,8,9,6,9);
                return true;
        
        }

        public bool RunThresholdTest_new( ExecuteBuffer _executeBuffer, out ExecuteBuffer outexecutebuffer,out string result_info,Dictionary<int, PointName> Pointlist)
        {
            outexecutebuffer = _executeBuffer;
            result_info = "";
            double x_bu = 0;
            double y_bu = 0;
            HObject image1, image2, image3, image4, image5, image6, image7, image8, image9;
            HOperatorSet.GenEmptyObj(out image1);
            HOperatorSet.GenEmptyObj(out image2);
            HOperatorSet.GenEmptyObj(out image3);
            HOperatorSet.GenEmptyObj(out image4);
            HOperatorSet.GenEmptyObj(out image5);
            HOperatorSet.GenEmptyObj(out image6);
            HOperatorSet.GenEmptyObj(out image7);
            HOperatorSet.GenEmptyObj(out image8);
            HOperatorSet.GenEmptyObj(out image9);
            int number_check = Convert.ToInt32(this.textBox_dian.Text.ToString());
            if (!Pointlist.ContainsKey(number_check))
            {
                MessageBox.Show("拼图数据：  输入为空，其中没有点位");
                result_info = "拼图数据：  输入为空，其中没有点位";

            }
            else 
            {
                
                if (Pointlist[number_check].点X.TupleLength() > 0)
            {
                x_bu = Pointlist[number_check].点X[0];
                y_bu = Pointlist[number_check].点Y[0];

            }
            }

            double X_shu = (Convert.ToDouble(this.textBox_x.Text.ToString())) / Convert.ToDouble(this.textBox_bi.Text.ToString()) + Convert.ToDouble(this.textBox_x.Text.ToString()) * x_bu;
            double Y_shu = (Convert.ToDouble(this.textBox_y.Text.ToString()) + y_bu) / Convert.ToDouble(this.textBox_bi.Text.ToString()) + Convert.ToDouble(this.textBox_y.Text.ToString()) * y_bu;
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image1.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像1已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像1已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image1.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image1参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像1已经不存在，请重置设置输入图像";
                return false;
            }

            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image2.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像2已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像2已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image2.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image2参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像2已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image3.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像3已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像3已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image3.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image3参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像3已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image4.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像4已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像4已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image4.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image4参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像4已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image5.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像5已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像5已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image5.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image5参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像5已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image6.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像6已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像6已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image6.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image6参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像6已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image7.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像7已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像7已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image7.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image7参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像7已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image8.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像8已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像8已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image8.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image8参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像8已经不存在，请重置设置输入图像";
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image9.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像9已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像9已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image9.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image9参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像9已经不存在，请重置设置输入图像";
                return false;
            }
            string comboBox_imageoutname = this.Output_image.Text.ToString() + ".img";
            if (_executeBuffer.imageBuffer[comboBox_imageoutname] != null)
            {
                if (_executeBuffer.imageBuffer[comboBox_imageoutname].IsInitialized())
                {
                    _executeBuffer.imageBuffer[comboBox_imageoutname].Dispose();
                }
            }
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image1.SelectedItem.ToString() + ".img"], out image1);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image2.SelectedItem.ToString() + ".img"], out image2);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image3.SelectedItem.ToString() + ".img"], out image3);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image4.SelectedItem.ToString() + ".img"], out image4);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image5.SelectedItem.ToString() + ".img"], out image5);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image6.SelectedItem.ToString() + ".img"], out image6);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image7.SelectedItem.ToString() + ".img"], out image7);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image8.SelectedItem.ToString() + ".img"], out image8);
            HOperatorSet.CopyImage(_executeBuffer.imageBuffer[this.Input_image9.SelectedItem.ToString() + ".img"], out image9);
            /*
            image1 = _executeBuffer.imageBuffer[this.Input_image1.SelectedItem.ToString() + ".img"];
            image2 = _executeBuffer.imageBuffer[this.Input_image2.SelectedItem.ToString() + ".img"];
            image3 = _executeBuffer.imageBuffer[this.Input_image3.SelectedItem.ToString() + ".img"];
            image4 = _executeBuffer.imageBuffer[this.Input_image4.SelectedItem.ToString() + ".img"];
            image5 = _executeBuffer.imageBuffer[this.Input_image5.SelectedItem.ToString() + ".img"];
            image6 = _executeBuffer.imageBuffer[this.Input_image6.SelectedItem.ToString() + ".img"];
            image7 = _executeBuffer.imageBuffer[this.Input_image7.SelectedItem.ToString() + ".img"];
            image8 = _executeBuffer.imageBuffer[this.Input_image8.SelectedItem.ToString() + ".img"];
            image9 = _executeBuffer.imageBuffer[this.Input_image9.SelectedItem.ToString() + ".img"];
             */
            HTuple width,height,width123,height123;
            HObject Rectangle1, Mask1, ImagePart1, Mask2, ImagePart2, imagex, imagey, imagez, imagea, ho_TiledImage;
            HOperatorSet.GenEmptyObj(out Rectangle1);
            HOperatorSet.GenEmptyObj(out Mask1);
            HOperatorSet.GenEmptyObj(out ImagePart1);
            HOperatorSet.GenEmptyObj(out Mask2);
            HOperatorSet.GenEmptyObj(out ImagePart2);
            HOperatorSet.GenEmptyObj(out imagex);
            HOperatorSet.GenEmptyObj(out imagey);
            HOperatorSet.GenEmptyObj(out imagez);
            HOperatorSet.GenEmptyObj(out imagea);
            HOperatorSet.GenEmptyObj(out ho_TiledImage);


            HOperatorSet.GetImageSize(image1, out width, out height);
            HOperatorSet.GenRectangle1(out Rectangle1,0,0,height,X_shu);


            HOperatorSet.ReduceDomain(image1, Rectangle1, out Mask1);
            HOperatorSet.CropDomain(Mask1, out ImagePart1);
            HOperatorSet.ReduceDomain(image2, Rectangle1, out Mask2);
            HOperatorSet.CropDomain(Mask2, out ImagePart2);
            HOperatorSet.GetImageSize(ImagePart1, out width123, out height123);
            HOperatorSet.GenEmptyObj(out imagex);
            HOperatorSet.ConcatObj(imagex, ImagePart1, out imagex);
            HOperatorSet.ConcatObj(imagex, ImagePart2, out imagex);
            HOperatorSet.ConcatObj(imagex, image3, out imagex);
            HOperatorSet.GenEmptyObj(out ho_TiledImage);
            HOperatorSet.TileImagesOffset(imagex, out ho_TiledImage, ((new HTuple(0)).TupleConcat(
    0)).TupleConcat(0), (((new HTuple(0)).TupleConcat(width123))).TupleConcat(
    width123 + width123), ((new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1),
    ((new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1), ((new HTuple(-1)).TupleConcat(
    -1)).TupleConcat(-1), ((new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1),
    (width123 + width123) + width, height);
          //  image1.Dispose();
         //   image2.Dispose();
            Mask1.Dispose();
            Mask2.Dispose();
          //  image3.Dispose();

            ImagePart2.Dispose();
            ImagePart1.Dispose();
            imagex.Dispose();

            HTuple  width456, height456;
            HObject  ho_TiledImage1,Mask4,Mask5,ImagePart4,ImagePart5;
            HOperatorSet.ReduceDomain(image4, Rectangle1, out Mask4);
            HOperatorSet.CropDomain(Mask4, out ImagePart4);
            HOperatorSet.ReduceDomain(image5, Rectangle1, out Mask5);
            HOperatorSet.CropDomain(Mask5, out ImagePart5);
            HOperatorSet.GetImageSize(ImagePart4, out width456, out height456);
            HOperatorSet.GenEmptyObj(out imagey);
            HOperatorSet.ConcatObj(imagey, ImagePart4, out imagey);
            HOperatorSet.ConcatObj(imagey, ImagePart5, out imagey);
            HOperatorSet.ConcatObj(imagey, image6, out imagey);
            HOperatorSet.GenEmptyObj(out ho_TiledImage1);
            HOperatorSet.TileImagesOffset(imagey, out ho_TiledImage1, ((new HTuple(0)).TupleConcat(
    0)).TupleConcat(0), (((new HTuple(0)).TupleConcat(width456))).TupleConcat(
    width456 + width456), ((new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1),
    ((new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1), ((new HTuple(-1)).TupleConcat(
    -1)).TupleConcat(-1), ((new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1),
    (width456 + width456) + width, height);
            Mask4.Dispose();
            Mask5.Dispose();
            ImagePart4.Dispose();
            ImagePart5.Dispose();
            imagey.Dispose();
          //  image4.Dispose();
          //  image5.Dispose();
          //  image6.Dispose();
            HTuple width789, height789;
            HObject ho_TiledImage2, Mask7, Mask8, ImagePart7, ImagePart8;
            HOperatorSet.ReduceDomain(image7, Rectangle1, out Mask7);
            HOperatorSet.CropDomain(Mask7, out ImagePart7);
            HOperatorSet.ReduceDomain(image8, Rectangle1, out Mask8);
            HOperatorSet.CropDomain(Mask8, out ImagePart8);
            HOperatorSet.GetImageSize(ImagePart8, out width789, out height789);
            HOperatorSet.GenEmptyObj(out imagez);
            HOperatorSet.ConcatObj(imagez, ImagePart7, out imagez);
            HOperatorSet.ConcatObj(imagez, ImagePart8, out imagez);
            HOperatorSet.ConcatObj(imagez, image9, out imagez);
            HOperatorSet.GenEmptyObj(out ho_TiledImage2);
            HOperatorSet.TileImagesOffset(imagez, out ho_TiledImage2, ((new HTuple(0)).TupleConcat(
    0)).TupleConcat(0), (((new HTuple(0)).TupleConcat(width789))).TupleConcat(
    width789 + width789), ((new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1),
    ((new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1), ((new HTuple(-1)).TupleConcat(
    -1)).TupleConcat(-1), ((new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1),
    (width789 + width789) + width, height);

            Mask8.Dispose();
            Mask7.Dispose();
            imagez.Dispose();
           // image7.Dispose();
            //image8.Dispose();
           // image9.Dispose();
            ImagePart7.Dispose();
            ImagePart8.Dispose();

            Rectangle1.Dispose();
            HTuple widtha3, heighta3,widthnew123,heightnew123;
            HObject Rectangle2, Mask11, ImagePart11, Mask22, ImagePart22, ho_TiledImage3;
            HOperatorSet.GenEmptyObj(out Rectangle2);
            HOperatorSet.GetImageSize(ho_TiledImage2, out widtha3, out heighta3);
            HOperatorSet.GenRectangle1(out Rectangle2, 0, 0, Y_shu, widtha3);

            HOperatorSet.ReduceDomain(ho_TiledImage, Rectangle2, out Mask11);
            HOperatorSet.CropDomain(Mask11, out ImagePart11);

            HOperatorSet.ReduceDomain(ho_TiledImage1, Rectangle2, out Mask22);
            HOperatorSet.CropDomain(Mask22, out ImagePart22);
            Rectangle2.Dispose();
            Mask11.Dispose();
            Mask22.Dispose();
            HOperatorSet.GetImageSize(ImagePart22, out widthnew123, out heightnew123);
            HOperatorSet.GenEmptyObj(out imagea);
            HOperatorSet.ConcatObj(imagea, ImagePart11, out imagea);
            HOperatorSet.ConcatObj(imagea, ImagePart22, out imagea);
            HOperatorSet.ConcatObj(imagea, ho_TiledImage2, out imagea);
            ImagePart11.Dispose();
            ImagePart22.Dispose();
            ho_TiledImage.Dispose();
            ho_TiledImage1.Dispose();
            ho_TiledImage2.Dispose();
            image1.Dispose();
            image2.Dispose();
            image3.Dispose();
            image4.Dispose();
            image5.Dispose();
            image6.Dispose();
            image7.Dispose();
            image8.Dispose();
            image9.Dispose();
            HOperatorSet.GenEmptyObj(out ho_TiledImage3);
            
            HOperatorSet.TileImagesOffset(imagea, out ho_TiledImage3, (((new HTuple(0)).TupleConcat(
                heightnew123))).TupleConcat(heightnew123 + heightnew123), ((new HTuple(0)).TupleConcat(
                0)).TupleConcat(0), ((new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1), (
                (new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1), ((new HTuple(-1)).TupleConcat(
                -1)).TupleConcat(-1), ((new HTuple(-1)).TupleConcat(-1)).TupleConcat(-1),
                widtha3, (heightnew123 + heightnew123) + height);


            _executeBuffer.imageBuffer[comboBox_imageoutname] = ho_TiledImage3;
            outexecutebuffer = _executeBuffer;

            
            outexecutebuffer = _executeBuffer;
            result_info = " 拼图: 完成";

            imagea.Dispose();

            return true;
        }





        public bool Send_name()
        {
            if (Input_image1.Items.Count == 0)
                return false;
            if (Input_image1.SelectedItem == null)
                return false;
           // UpdateThresholdTest.OnSendUpdateThresholdTest(new UpdateThresholdTestEventArgs(Threshold_image.SelectedItem.ToString()+".img", thresold_region.Text.ToString()+".region", comboBox_imageout.Text.ToString()+".img", trackBar_min.Value, trackBar_max.Value, false));
            return true;

        }






    }




    











}
