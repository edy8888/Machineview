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
using ViewROI;
using System.Text.RegularExpressions;
using System.Runtime.Serialization; 
namespace PMACam
{
            [Serializable]
    public partial class ReadQRcode : Form, ISerializable
    {
        public ReadQRcode(ExecuteBuffer buffer, bool addbuffer)
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
            this.comboBox_type.SelectedIndex = 0;
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
            IniFile.IniWriteValue(j.ToString(), "QR_number", this.textBox1.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "QR_type", this.comboBox_type.SelectedIndex.ToString());




        }


        public ReadQRcode(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();
        this.comboBox_type.SelectedIndex = 0;

            this.comboBox_type.SelectedIndex = (Int32)(info.GetValue("QR_type", typeof(Int32)));

            this.textBox1.Text = (string)(info.GetValue("QR_number", typeof(string)));
            string out_image_string = (string)(info.GetValue("Image_string", typeof(string)));
            string[] condition = { "," };
            string[] result = out_image_string.Split(condition, StringSplitOptions.None);

            for (int m = 0; m < result.Count(); m++)
                cbb_image.Items.Add(result[m]);
            cbb_image.SelectedIndex = (Int32)(info.GetValue("Input_image", typeof(Int32)));
} 
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("Input_image", this.cbb_image.SelectedIndex.ToString());
            info.AddValue("QR_type", this.comboBox_type.SelectedIndex.ToString());
            info.AddValue("QR_number", this.textBox1.Text.ToString());


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
        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
          //  int Selectnumber = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "RegionType"));



            this.cbb_image.Items.Add(IniFile.IniReadValue(j.ToString(), "ImageIn"));
            this.cbb_image.SelectedIndex = 0;
            this.textBox1.Text = IniFile.IniReadValue(j.ToString(), "QR_number");
            if (IniFile.IniReadValue(j.ToString(), "QR_type") != "")
                this.comboBox_type.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "QR_type"));





        }



        public string get_Imagein()
        {
            if (this.cbb_image.SelectedItem != null)
                return this.cbb_image.SelectedItem.ToString()+".img";
            else
                return null;


        }

        public bool Run_Region(ExecuteBuffer _executeBuffer, out 字符串GVName_halcon outResult,HWndCtrl hWndCtrl)
        {

         //   outexecutebuffer = _executeBuffer;

            HObject outImage;
            outResult = new 字符串GVName_halcon();
            HOperatorSet.GenEmptyObj(out outImage);
            if (!_executeBuffer.imageBuffer.ContainsKey(this.cbb_image.SelectedItem.ToString()+".img"))
            {
                MessageBox.Show("感兴趣区域：无法找到输入图像");
                return false;
            }
            if (_executeBuffer.imageBuffer[this.cbb_image.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("感兴趣区域：无法找到输入图像");
                return false;
            }
             HTuple DataCodeHandle,DecodedDataStrings,ResultHandles;
            HObject SymbolXLDs;
            HOperatorSet.GenEmptyObj(out SymbolXLDs);

            HOperatorSet.CreateDataCode2dModel(this.comboBox_type.SelectedItem.ToString(),new HTuple(),new HTuple(),out DataCodeHandle);
            if (this.comboBox_type.SelectedIndex == 0)
                HOperatorSet.SetDataCode2dParam(DataCodeHandle, "default_parameters", "maximum_recognition");

            HOperatorSet.FindDataCode2d(_executeBuffer.imageBuffer[this.cbb_image.SelectedItem.ToString() + ".img"], out SymbolXLDs, DataCodeHandle, "stop_after_result_num", this.textBox1.Text.ToString(), out ResultHandles, out DecodedDataStrings);
            outResult.字符串 = DecodedDataStrings;
            hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, "green");
            hWndCtrl.addIconicVar(SymbolXLDs);
            hWndCtrl.repaint();
            return true;

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












        public bool Check_pal()
        {
            if (cbb_image.SelectedItem == null)
            { MessageBox.Show("感兴趣区域： 异常，输入图像为空，请设置");
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
          //  UpdateRegionInterest.OnSendUpdateRegionInterest(new UpdateRegionInterestEventArgs(this.cbb_outregion.Text.ToString()+".img"));
            if (Check_pal())
                this.Visible = false;
        }











    }
}
