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
using ViewROI;
using System.Runtime.Serialization; 
namespace PMACam
{
    [Serializable]
    public partial class PointShow : Form, ISerializable
    {
        public PointShow(ExecuteBuffer buffer, bool addbuffer)
        {
            InitializeComponent();
            if (addbuffer)
            {
                if (buffer != null)

                    SetParaImage(buffer); 
            }

            this.cbb_Inputsource.SelectedIndex = 0;

        }

        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);

            IniFile.IniWriteValue(j.ToString(), "TransSource", this.cbb_Inputsource.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "ShowContour", this.linecheck.Checked.ToString());
            IniFile.IniWriteValue(j.ToString(), "Show_color", this.comboBox1.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "Show_no", this.textBox1.Text.ToString());
            
        }

        public PointShow(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();
        this.cbb_Inputsource.SelectedIndex = 0;


        this.cbb_Inputsource.SelectedIndex = (Int32)(info.GetValue("TransSource", typeof(Int32)));
        this.comboBox1.SelectedIndex = (Int32)(info.GetValue("Show_color", typeof(Int32)));
            this.textBox1.Text = (string)(info.GetValue("Show_no", typeof(string)));

            if ((string)(info.GetValue("ShowContour", typeof(string))) == "True")
                this.linecheck.Checked = true;
            else
                this.linecheck.Checked = false;
}

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("TransSource", this.cbb_Inputsource.SelectedIndex.ToString());
            info.AddValue("Show_color", this.comboBox1.SelectedIndex.ToString());
            info.AddValue("Show_no", this.textBox1.Text.ToString());
            info.AddValue("ShowContour", this.linecheck.Checked.ToString());




        } 
        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
           // int Selectnumber = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransType"));
            if (IniFile.IniReadValue(j.ToString(), "TransSource") != "")
            this.cbb_Inputsource.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransSource"));
            if (IniFile.IniReadValue(j.ToString(), "ShowContour") == "True")
                this.linecheck.Checked = true;
            else
                this.linecheck.Checked = false;
            if (IniFile.IniReadValue(j.ToString(), "Show_color") != "")
                this.comboBox1.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "Show_color"));
            this.textBox1.Text = IniFile.IniReadValue(j.ToString(), "Show_no");



        }
        public bool Run_show(ExecuteBuffer _executeBuffer, 模板GVName_halcon Model_result,Dictionary<int, PointName> Pointlist, HWndCtrl hWndCtrl,out string out_info)
        {
            int number_source = this.cbb_Inputsource.SelectedIndex;

            out_info = "";
            if (number_source == 0)
            {
                if (Model_result == null)
                {
                    MessageBox.Show("点位显示：  输入为空，其中没有点位");
                    out_info = "点位显示：  输入为空，其中没有点位";
                    return false;
                }
                HTuple size = 10;

                if (Model_result.点X.TupleLength() > 0)
                {
                    if (linecheck.Checked)
                    {

                        HObject line_match;
                        HOperatorSet.GenEmptyObj(out line_match);
                       // HOperatorSet.GenRegionLine(out line_match, (double)Model_result.点Y[0], (double)Model_result.点X[0], (double)Model_result.点Y[1], (double)Model_result.点X[1]);
                        HOperatorSet.GenContourPolygonXld(out line_match, Model_result.点X, Model_result.点Y);
                        hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, this.comboBox1.SelectedItem.ToString());
                        hWndCtrl.addIconicVar(line_match);
                        hWndCtrl.repaint();
                        out_info = "点位显示：  完成";
                        return true;

                    }
                    else
                    {


                        HObject cross;
                        HOperatorSet.GenEmptyObj(out cross);
                        HOperatorSet.GenCrossContourXld(out cross, Model_result.点Y, Model_result.点X, size, Model_result.角度Angle);

                        hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, this.comboBox1.SelectedItem.ToString());
                        hWndCtrl.addIconicVar(cross);
                        hWndCtrl.repaint();
                        out_info = "点位显示：  完成";
                        return true;
                    }

                }
                else
                    out_info = "点位显示：  没有可以显示的点位";

            }
            else
            {
                int number_check = Convert.ToInt32(this.textBox1.Text.ToString());
                if (!Pointlist.ContainsKey(number_check))
                {
                    MessageBox.Show("点位显示：  输入为空，其中没有点位");
                    out_info = "点位显示：  输入为空，其中没有点位";
                    return false;
                }
                HTuple size1 = 20;

                if (Pointlist[number_check].点X.TupleLength() > 0)
                {
                    if (linecheck.Checked)
                    {

                        HObject line;
                        HOperatorSet.GenEmptyObj(out line);
                       // HOperatorSet.GenRegionLine(out line, (double)Pointlist[number_check].点Y[0], (double)Pointlist[number_check].点X[0], (double)Pointlist[number_check].点Y[1], (double)Pointlist[number_check].点X[1]);
                        HOperatorSet.GenContourPolygonXld(out line, Pointlist[number_check].点X, Pointlist[number_check].点Y);
                        hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, this.comboBox1.SelectedItem.ToString());
                        hWndCtrl.addIconicVar(line);
                        hWndCtrl.repaint();
                        out_info = "点位显示：  完成";
                        return true;

                    }
                    else
                    {
                        HObject cross;
                        HOperatorSet.GenEmptyObj(out cross);
                        HOperatorSet.GenCrossContourXld(out cross, Pointlist[number_check].点Y, Pointlist[number_check].点X, size1, 0);

                        hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, this.comboBox1.SelectedItem.ToString());
                        hWndCtrl.addIconicVar(cross);
                        hWndCtrl.repaint();
                        out_info = "点位显示：  完成";
                        return true;
                    }


                }
                else
                    out_info = "点位显示：  没有可以显示的点位";

            
            }







            return false;
            








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
        private void button1_Click(object sender, EventArgs e)
        {
            if (Check_pal())
                this.Visible = false;
        }
        public bool Check_pal()
        {
            return true;

        }
        internal void SetParaImage(ExecuteBuffer test)
        {





        }







    }
}
