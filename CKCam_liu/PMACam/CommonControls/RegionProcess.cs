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
    public partial class RegionProcess : Form, ISerializable
    {
        public RegionProcess(ExecuteBuffer buffer, bool addbuffer)
        {


            InitializeComponent();

            if (addbuffer)
            {
                if (buffer != null)
                {
                    SetParaImage(buffer);
                    SetParaImage(buffer);

                }
            }
            this.cbb_features.SelectedIndex = 0;
            this.cbb_operation.SelectedIndex = 0;
        }

        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "TbNumber", this.tb_outnumber.Text.ToString());

            IniFile.IniWriteValue(j.ToString(), "RegionIn", this.cbb_regions.Text.ToString());
                IniFile.IniWriteValue(j.ToString(), "FeatureIn", this.cbb_features.SelectedIndex.ToString());
                IniFile.IniWriteValue(j.ToString(), "OperationIn", this.cbb_operation.SelectedIndex.ToString());
                IniFile.IniWriteValue(j.ToString(), "MinValue", this.txt_min.Text.ToString());
                IniFile.IniWriteValue(j.ToString(), "MaxValue", this.txt_max.Text.ToString());
            
        }

        public RegionProcess(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();
        this.cbb_features.SelectedIndex = 0;
        this.cbb_operation.SelectedIndex = 0;

            this.cbb_operation.SelectedIndex = (Int32)(info.GetValue("OperationIn", typeof(Int32)));
            this.cbb_features.SelectedIndex = (Int32)(info.GetValue("FeatureIn", typeof(Int32)));
            this.cbb_regions.Text = (string)(info.GetValue("RegionIn", typeof(string)));
            this.txt_min.Text = (string)(info.GetValue("MinValue", typeof(string)));
            this.txt_max.Text = (string)(info.GetValue("MaxValue", typeof(string)));
            this.tb_outnumber.Text = (string)(info.GetValue("TbNumber", typeof(string)));


}

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("RegionIn", this.cbb_regions.Text.ToString());
            info.AddValue("MinValue", this.txt_min.Text.ToString());
            info.AddValue("OperationIn", this.cbb_operation.SelectedIndex.ToString());
            info.AddValue("FeatureIn", this.cbb_features.SelectedIndex.ToString());
            info.AddValue("MaxValue", this.txt_max.Text.ToString());
            info.AddValue("TbNumber", this.tb_outnumber.Text.ToString());





        } 
        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
           // int Selectnumber = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "RegionType"));
            this.cbb_regions.Items.Clear();
            this.cbb_regions.Items.Add(IniFile.IniReadValue(j.ToString(), "RegionIn"));
            this.tb_outnumber.Text = IniFile.IniReadValue(j.ToString(), "TbNumber");
            this.cbb_regions.SelectedIndex = 0;

            if (IniFile.IniReadValue(j.ToString(), "FeatureIn") != "")
               this.cbb_features.SelectedIndex =  Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "FeatureIn"));
            if (IniFile.IniReadValue(j.ToString(), "OperationIn") != "")
               this.cbb_operation.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "OperationIn"));
               this.txt_min.Text = IniFile.IniReadValue(j.ToString(), "MinValue");
               this.txt_max.Text = IniFile.IniReadValue(j.ToString(), "MaxValue");
            


        }





        public string get_Regionout()
        {
           // return this.cbb_selectedRegions.Text.ToString();
            return "";
        }
        public string get_Regionin()
        {
            if (this.cbb_regions.SelectedItem != null)
                return this.cbb_regions.SelectedItem.ToString();
            else
                return null;


        }

        public bool Run_Region(ExecuteBuffer _executeBuffer, out ExecuteBuffer outexecutebuffer, out string result_info,  Dictionary<int, PointName> Point_temp_result,out Dictionary<int, PointName> Point_out_result)
        {

            outexecutebuffer = _executeBuffer;
            result_info = "";
            Point_out_result = Point_temp_result;
            HObject regionconOutresult, regionOutresult;
            if (!_executeBuffer.imageBuffer.ContainsKey(this.cbb_regions.SelectedItem.ToString() + ".region"))
            {
                MessageBox.Show("二值化处理: 输入图像已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.cbb_regions.SelectedItem.ToString() + ".region"] == null)
            {
                MessageBox.Show("二值化处理: image参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像已经不存在，请重置设置输入图像";
                return false;
            }
            HTuple area1, row1, col1;
            HOperatorSet.Connection(_executeBuffer.imageBuffer[this.cbb_regions.SelectedItem.ToString() + ".region"], out regionconOutresult);
            HOperatorSet.SelectShape(regionconOutresult, out regionOutresult, this.cbb_features.SelectedItem.ToString(), this.cbb_operation.SelectedItem.ToString(), Convert.ToInt32(this.txt_min.Text.ToString()), Convert.ToInt32(this.txt_max.Text.ToString()));
            HOperatorSet.AreaCenter(regionOutresult,out area1,out row1,out col1);


            int out_number_list = Convert.ToInt32(this.tb_outnumber.Text.ToString());
            if (Point_temp_result.ContainsKey(out_number_list))
            {
                Point_temp_result[out_number_list].点X = col1;
                Point_temp_result[out_number_list].点Y = row1;
            }
            else
                Point_temp_result.Add(out_number_list, new PointName(col1, row1));
            outexecutebuffer = _executeBuffer;
            Point_out_result = Point_temp_result;
            return true;
        }

        internal void SetParaImage(ExecuteBuffer test)
        {
            if (cbb_regions.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    if (keyc.Contains(".region"))
                    {
                        cbb_regions.Items.Add(keyc.Substring(0, keyc.Length - 7));
                        if (keyc.Contains("ROI") && keyc.Contains(".region"))
                            cbb_regions.Text = keyc.Substring(0, keyc.Length - 7);

                    }


                }
            }
            else
            {

                int m = 0;
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < cbb_regions.Items.Count; i++)
                    {
                        if (keyc == cbb_regions.Items[i].ToString() + ".region")
                            break;
                        m = i;
                        if (m == cbb_regions.Items.Count - 1 && keyc.Contains(".region"))
                            cbb_regions.Items.Add(keyc.Substring(0, keyc.Length - 7));

                    }
                }


            }


        }










        public bool Check_pal()
        {


            if (this.cbb_regions.Text == "")
            {
                MessageBox.Show("输入区域为空,请输入");
                return false;
            }

            if (!IsNumber(this.txt_min.Text.ToString()))
            {
                MessageBox.Show(" 输入最小值min不是数字,请重新输入");
                return false;
            }
            if (!IsNumber(this.txt_max.Text.ToString()))
            {
                MessageBox.Show(" 输入最大值max不是数字,请重新输入");
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
            //UpdateRegionProcess.OnSendUpdateRegionProcess(new UpdateRegionProcessEventArgs(this.cbb_selectedRegions.Text.ToString()));
            if (Check_pal())
                this.Visible = false;
        }









    }
}
