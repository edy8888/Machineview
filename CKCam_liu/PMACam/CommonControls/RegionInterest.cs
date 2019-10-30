using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using System.Text.RegularExpressions;
using System.Runtime.Serialization; 
namespace PMACam
{
    [Serializable]
    public partial class RegionInterest : Form, ISerializable
    {




        public RegionInterest(ExecuteBuffer buffer, bool addbuffer)
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

        }
        public RegionInterest(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();
            this.textBox_chong.Text = (string)(info.GetValue("TextboxFill", typeof(string)));
            this.cbb_outregion.Text = (string)(info.GetValue("RegionOut", typeof(string)));
            this.cbb_regions.Text = (string)(info.GetValue("RegionIn", typeof(string)));
            if ((string)(info.GetValue("CheckFill", typeof(string))) == "True")
                this.checkBox_chong.Checked = true;
            else
                this.checkBox_chong.Checked = false;
            string out_image_string = (string)(info.GetValue("Image_string", typeof(string)));
            string[] condition = { "," };
            string[] result = out_image_string.Split(condition, StringSplitOptions.None);

            for (int m = 0; m < result.Count(); m++)
                cbb_image.Items.Add(result[m]);
            cbb_image.SelectedIndex = (Int32)(info.GetValue("Input_image", typeof(Int32)));
}
        public string Imagename()
        {
            return cbb_image.Text.ToString() + ".img";
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("TextboxFill", this.textBox_chong.Text.ToString());
            info.AddValue("RegionOut", this.cbb_outregion.Text.ToString());
            info.AddValue("RegionIn", this.cbb_regions.Text.ToString());
            info.AddValue("CheckFill", this.checkBox_chong.Checked.ToString());
            info.AddValue("Input_image", this.cbb_image.SelectedIndex.ToString());
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
            if (this.cbb_image.SelectedItem == null)
            { MessageBox.Show("请确认输入图像是否为空");
                return;
            }
            IniFile.IniWriteValue(j.ToString(), "ImageIn", this.cbb_image.SelectedItem.ToString());
            IniFile.IniWriteValue(j.ToString(), "RegionIn", this.cbb_regions.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "RegionOut", this.cbb_outregion.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "CheckFill", this.checkBox_chong.Checked.ToString());

            IniFile.IniWriteValue(j.ToString(), "TextboxFill", this.textBox_chong.Text.ToString());



        }


        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);



            this.cbb_outregion.Text = IniFile.IniReadValue(j.ToString(), "RegionOut");
            this.cbb_regions.Items.Clear();
            this.cbb_regions.Items.Add(IniFile.IniReadValue(j.ToString(), "RegionIn"));
            this.cbb_regions.SelectedIndex = 0;
            this.cbb_image.Items.Add(IniFile.IniReadValue(j.ToString(), "ImageIn"));
            this.cbb_image.SelectedIndex = 0;
            if (IniFile.IniReadValue(j.ToString(), "CheckFill") == "True")
                this.checkBox_chong.Checked = true;
            else
                this.checkBox_chong.Checked = false;
            this.textBox_chong.Text = IniFile.IniReadValue(j.ToString(), "TextboxFill");




        }

        public string get_Regionout()
        {
            return this.cbb_outregion.Text.ToString()+".img";
        }
        public string get_Regionin()
        {
            if (this.cbb_regions.SelectedItem != null)
                return this.cbb_regions.SelectedItem.ToString();
            else
                return null;


        }
        public string get_Imagein()
        {
            if (this.cbb_image.SelectedItem != null)
                return this.cbb_image.SelectedItem.ToString()+".img";
            else
                return null;


        }

        public bool Run_Region(ExecuteBuffer _executeBuffer, out ExecuteBuffer outexecutebuffer)
        {

            outexecutebuffer = _executeBuffer;
            HObject inRegion;
            HObject outRegion;
            HObject outImage;
            HOperatorSet.GenEmptyObj(out outImage);
            if (!_executeBuffer.imageBuffer.ContainsKey(this.cbb_image.SelectedItem.ToString()+".img"))
            {
                MessageBox.Show("感兴趣区域：无法找到输入图像");
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.cbb_regions.SelectedItem.ToString()+".region"))
            {
                MessageBox.Show("感兴趣区域： 无法找到输入区域");
                return false;
            }
            if (!_executeBuffer.imageBuffer.ContainsKey(this.cbb_outregion.Text.ToString() + ".img"))
            {
                MessageBox.Show("感兴趣区域： 无法找到输出图像，请先确认");
                return false;
            }
            if (_executeBuffer.imageBuffer[this.cbb_regions.SelectedItem.ToString() + ".region"] == null)
            {
                MessageBox.Show("感兴趣区域： 无法找到感兴趣区域，请先确认");
                return false;
            }



            if (this.checkBox_chong.Checked)
            {
                HOperatorSet.PaintRegion(_executeBuffer.imageBuffer[this.cbb_regions.SelectedItem.ToString() + ".region"], _executeBuffer.imageBuffer[this.cbb_image.SelectedItem.ToString() + ".img"], out outImage, Convert.ToDouble(this.textBox_chong.Text), "fill");
                _executeBuffer.imageBuffer[this.cbb_outregion.Text.ToString() + ".img"] = outImage; ;
            }
            else
            {
                HOperatorSet.ReduceDomain(_executeBuffer.imageBuffer[this.cbb_image.SelectedItem.ToString() + ".img"], _executeBuffer.imageBuffer[this.cbb_regions.SelectedItem.ToString() + ".region"], out outRegion);

                if (_executeBuffer.imageBuffer[this.cbb_outregion.Text.ToString() + ".img"] != null)
                {
                    if (_executeBuffer.imageBuffer[this.cbb_outregion.Text.ToString() + ".img"].IsInitialized())
                    {
                        _executeBuffer.imageBuffer[this.cbb_outregion.Text.ToString() + ".img"].Dispose();
                    }
                }
                _executeBuffer.imageBuffer[this.cbb_outregion.Text.ToString() + ".img"] = outRegion;
            }
            return true;

        }

        internal void SetParaImage(ExecuteBuffer test)
        {
            if (cbb_regions.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    if(keyc.Contains(".region"))
                        cbb_regions.Items.Add(keyc.Substring(0,keyc.Length-7));

            }
            else
            {

                int m = 0;
                if (cbb_regions.SelectedItem != null)
                {
                    if (!test.imageBuffer.ContainsKey(cbb_regions.SelectedItem.ToString()+".region"))
                        cbb_regions.Items.Remove(cbb_regions.SelectedItem.ToString()+".region");
                }
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < cbb_regions.Items.Count; i++)
                    {
                        if (keyc == (cbb_regions.Items[i].ToString()+ ".region"))
                            break;
                        m = i;
                        if (m == cbb_regions.Items.Count - 1 && keyc.Contains(".region"))
                            cbb_regions.Items.Add(keyc.Substring(0,keyc.Length-7));

                    }
                }


            }



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
                    if (!test.imageBuffer.ContainsKey(cbb_image.SelectedItem.ToString()+".img"))
                        cbb_image.Items.Remove(cbb_image.SelectedItem.ToString()+".img");
                }
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < cbb_image.Items.Count; i++)
                    {
                        if (keyc == (cbb_image.Items[i].ToString()+".img"))
                            break;
                        m = i;
                        if (m == cbb_image.Items.Count - 1 && keyc.Contains(".img"))
                            cbb_image.Items.Add(keyc.Substring(0,keyc.Length-4));

                    }
                }


            }

        }






        internal void SetParaRegion(ExecuteBuffer test)
        {
            if (this.cbb_outregion.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    cbb_outregion.Items.Add(keyc);

            }
            else
            {

                int m = 0;
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < cbb_outregion.Items.Count; i++)
                    {
                        if (keyc == cbb_outregion.Items[i].ToString())
                            break;
                        m = i;
                        if (m == cbb_outregion.Items.Count - 1)
                            cbb_outregion.Items.Add(keyc);

                    }
                }

            }

        }





        public bool Check_pal()
        {
            if (cbb_image.SelectedItem == null)
            { MessageBox.Show("感兴趣区域： 异常，输入图像为空，请设置");
            return false;
            }
            if (cbb_outregion.Text == null)
            {
                MessageBox.Show("感兴趣区域： 异常，输出图像为空，请设置");
                return false;
            }
            if (cbb_outregion.Text == "")
            {
                MessageBox.Show("感兴趣区域： 异常，输出图像未填，请设置");
                return false;
            }
            if (cbb_regions.SelectedItem == null)
            {
                MessageBox.Show("感兴趣区域： 异常，输入区域为空，请设置");
                return false;
            }
            if (cbb_regions.SelectedItem.ToString() == "")
            {
                MessageBox.Show("感兴趣区域： 异常，输入区域未填，请设置");
                return false;
            }
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
        private void btn_Sure_Click(object sender, EventArgs e)
        {
           // UpdateRegionProcess.OnSendUpdateRegionProcess(new UpdateRegionProcessEventArgs(this.cbb_outregion.Text.ToString()));
            UpdateRegionInterest.OnSendUpdateRegionInterest(new UpdateRegionInterestEventArgs(this.cbb_outregion.Text.ToString()+".img"));
            if (Check_pal())
                this.Visible = false;
        }













    }



    /*
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

    }*/
}
