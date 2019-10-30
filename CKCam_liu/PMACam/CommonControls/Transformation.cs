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
    public partial class Transformation : Form, ISerializable
    {
        public Transformation(ExecuteBuffer buffer, bool addbuffer)
        {
            InitializeComponent();
            if (addbuffer)
            {
                if (buffer != null)

                    SetParaImage(buffer); 
            }
            this.cbb_TransType.SelectedIndex = 0;
            this.cbb_Inputsource.SelectedIndex = 0;
            panel_input.Visible = false;
            panel_outnumber.Visible = false;
            panel_inpnumber.Visible = false;

        }

        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            if (this.cbb_HM_PTW.SelectedItem != null)
                IniFile.IniWriteValue(j.ToString(), "TransPTW", this.cbb_HM_PTW.SelectedItem.ToString());
            else
            {
                MessageBox.Show("HM_PTW is null,请设置");
                return;
            }

            if (this.cbb_HM_WTP.SelectedItem != null)
                IniFile.IniWriteValue(j.ToString(), "TransWTP", this.cbb_HM_WTP.SelectedItem.ToString());

            else
            {
                MessageBox.Show("HM_WTP is null,请设置");
                return;
            }

            IniFile.IniWriteValue(j.ToString(), "TransWTP", this.cbb_HM_WTP.SelectedItem.ToString());

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "TransType", this.cbb_TransType.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "TransSource", this.cbb_Inputsource.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "InitialNumber", this.tb_innumber.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "ListNumber", this.tb_outnumber.Text.ToString());
        }

        public Transformation(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();
        this.cbb_TransType.SelectedIndex = 0;
        this.cbb_Inputsource.SelectedIndex = 0;
        panel_input.Visible = false;
        panel_outnumber.Visible = false;
        panel_inpnumber.Visible = false;


            this.cbb_HM_WTP.SelectedIndex = (Int32)(info.GetValue("TransWTP", typeof(Int32)));

            this.cbb_HM_PTW.SelectedIndex = (Int32)(info.GetValue("TransPTW", typeof(Int32)));

            this.cbb_TransType.SelectedIndex = (Int32)(info.GetValue("TransType", typeof(Int32)));
            this.cbb_Inputsource.SelectedIndex = (Int32)(info.GetValue("TransSource", typeof(Int32)));
            this.tb_innumber.Text = (string)(info.GetValue("InitialNumber", typeof(string)));
            this.tb_outnumber.Text = (string)(info.GetValue("ListNumber", typeof(string)));

} 

                        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("TransType", this.cbb_TransType.SelectedIndex.ToString());
            info.AddValue("TransSource", this.cbb_Inputsource.SelectedIndex.ToString());
            info.AddValue("TransWTP", this.cbb_HM_WTP.SelectedIndex.ToString());
            info.AddValue("TransPTW", this.cbb_HM_PTW.SelectedIndex.ToString());
            info.AddValue("InitialNumber", this.tb_innumber.Text.ToString());
            info.AddValue("ListNumber", this.tb_outnumber.Text.ToString());
  




        } 
        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            int Selectnumber = 0;
            if (IniFile.IniReadValue(j.ToString(), "TransType") != "")
                 Selectnumber = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransType"));
            this.cbb_TransType.SelectedIndex = Selectnumber;
            if (IniFile.IniReadValue(j.ToString(), "TransSource") != "")
                this.cbb_Inputsource.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransSource"));
            cbb_HM_WTP.Text = IniFile.IniReadValue(j.ToString(), "TransWTP");
            this.cbb_HM_PTW.Items.Clear();
            this.cbb_HM_WTP.Items.Clear();
            this.cbb_HM_WTP.Items.Add(IniFile.IniReadValue(j.ToString(), "TransWTP"));
            this.cbb_HM_WTP.SelectedIndex = 0;
            this.cbb_HM_PTW.Items.Add(IniFile.IniReadValue(j.ToString(), "TransPTW"));
            this.cbb_HM_PTW.SelectedIndex = 0;
            this.tb_innumber.Text = IniFile.IniReadValue(j.ToString(), "InitialNumber");
            this.tb_outnumber.Text = IniFile.IniReadValue(j.ToString(), "ListNumber");


        }





        public void Run_transform_backup(ExecuteBuffer _executeBuffer, List<点GVName> Point_result, List<模板GVName> Model_result, List<圆GVName> Circle_result, Dictionary<int, PointName> Point_temp_result, out Dictionary<int, PointName> Point_out_result)
        {
            int number_source = this.cbb_Inputsource.SelectedIndex;
            int number_get = Convert.ToInt32(this.tb_innumber.Text.ToString());
            double x = 0;
            double y = 0;

            Point_out_result = Point_temp_result;
            if (number_source == 0)
            {
                if (Point_result == null)
                {
                    MessageBox.Show("坐标转换：  输入为空，其中没有点位");
                    return;
                }

                if (Point_result.Count < number_get || Point_result.Count == 0)
                {
                    MessageBox.Show("坐标转换：  输入阵列长度不够，达不到需求");
                    return;

                }
                else
                {
                    x = Convert.ToDouble(Point_result[number_get].点X);
                    y = Convert.ToDouble(Point_result[number_get].点Y);

                }

            }
            else if (number_source == 1)
            {
                if (Model_result == null)
                {
                    MessageBox.Show("坐标转换：  输入为空，其中没有点位");
                    return;
                }

                if (Model_result.Count < number_get || Model_result.Count == 0)
                {
                    MessageBox.Show("坐标转换：  输入阵列长度不够，达不到需求");
                    return;

                }
                else
                {
                    x = Convert.ToDouble(Model_result[number_get].点X);
                    y = Convert.ToDouble(Model_result[number_get].点Y);

                }

            }
            else if (number_source == 2)
            {
                if (Circle_result == null)
                {
                    MessageBox.Show("坐标转换：  输入为空，其中没有点位");
                    return;
                }

                if (Circle_result.Count < number_get || Circle_result.Count == 0)
                {
                    MessageBox.Show("坐标转换：  输入阵列长度不够，达不到需求");
                    return;

                }
                else
                {
                    x = Convert.ToDouble(Circle_result[number_get].圆心X);
                    y = Convert.ToDouble(Circle_result[number_get].圆心Y);

                }

            }
            else if (number_source == 3)
            {
                if (Point_temp_result == null)
                {
                    MessageBox.Show("坐标转换：  输入为空，其中没有点位");
                    return;
                }

                if (Point_temp_result.ContainsKey(number_get))
                {

                    x = Point_temp_result[number_get].点X;
                    y = Point_temp_result[number_get].点Y;

                }
                else
                {

                    MessageBox.Show("坐标转换：  输入字典中没有该点位");
                    return;
                }

            }
            else
            {
                x = Convert.ToDouble(this.tb_inputx.Text.ToString());
                y = Convert.ToDouble(this.tb_inputy.Text.ToString());
            }

            if (this.cbb_HM_PTW.SelectedItem == null)
            {
                MessageBox.Show("坐标转换：  世界转图像 为空,请设置");
                return;
            }
            if (this.cbb_HM_WTP.SelectedItem == null)
            {
                MessageBox.Show("坐标转换：  世界转图像 为空,请设置");
                return;
            }
            HTuple output_x, output_y;
            int Trans_type = this.cbb_TransType.SelectedIndex;
            if (Trans_type == 0)
                HOperatorSet.AffineTransPoint2d((HTuple)_executeBuffer.controlBuffer[this.cbb_HM_PTW.SelectedItem.ToString()+ ".tuple"], x, y, out output_x, out output_y);
            else
                HOperatorSet.AffineTransPoint2d((HTuple)_executeBuffer.controlBuffer[this.cbb_HM_WTP.SelectedItem.ToString()+ ".tuple"], x, y, out output_x, out output_y);

            int out_number_list = Convert.ToInt32(this.tb_outnumber.Text.ToString());
            //MessageBox.Show(""+_executeBuffer.controlBuffer[this.cbb_HM_WTP.SelectedItem.ToString()].GetType());
            if (Point_temp_result.ContainsKey(out_number_list))
            {
                Point_temp_result[out_number_list].点X = output_x;
                Point_temp_result[out_number_list].点Y = output_y;
            }
            else
                Point_temp_result.Add(out_number_list, new PointName(output_x, output_y));
            this.tb_outputx.Text = output_x.ToString();
            this.tb_outputy.Text = output_y.ToString();
            Point_out_result = Point_temp_result;
            //MessageBox.Show("" + _executeBuffer.controlBuffer["123"].GetType());
            // MessageBox.Show("" + _executeBuffer.controlBuffer["windowHandle"].GetType());




        }















        public void Run_transform(ExecuteBuffer _executeBuffer, 点GVName_halcon Point_result_halcon, 模板GVName_halcon Model_result_halcon, 圆GVName_halcon Circle_result_halcon, Dictionary<int, PointName> Point_temp_result, out Dictionary<int, PointName> Point_out_result)
        {
            int number_source = this.cbb_Inputsource.SelectedIndex;

            HTuple x, y;
            Point_out_result = Point_temp_result;
            if (number_source == 0)
            {
                if (Point_result_halcon == null)
                {
                    MessageBox.Show("坐标转换：  输入为空，其中没有点位");
                    return;
                }

                if (Point_result_halcon.点X.TupleLength()  == 0)
                {
                    MessageBox.Show("坐标转换：  输入阵列长度不够，达不到需求");
                    return;

                }
                else
                {
                    x = Point_result_halcon.点X;
                    y = Point_result_halcon.点Y;

                }
            
            }
            else if (number_source == 1)
            {
                if (Model_result_halcon == null)
                {
                    MessageBox.Show("坐标转换：  输入为空，其中没有点位");
                    return;
                }

                if (Model_result_halcon.点X.TupleLength() == 0)
                {
                    MessageBox.Show("坐标转换：  输入阵列长度不够，达不到需求");
                    return;

                }
                else
                {
                    x = Model_result_halcon.点X;
                    y = Model_result_halcon.点Y;

                }

            }
            else if (number_source == 2)
            {
                if (Circle_result_halcon == null)
                {
                    MessageBox.Show("坐标转换：  输入为空，其中没有点位");
                    return;
                }

                if (Circle_result_halcon.圆心X.TupleLength() == 0)
                {
                    MessageBox.Show("坐标转换：  输入阵列长度不够，达不到需求");
                    return;

                }
                else
                {
                    x = Circle_result_halcon.圆心X;
                    y = Circle_result_halcon.圆心Y;

                }

            }
            else if (number_source == 3)
            {
                int number_get = Convert.ToInt32(this.tb_innumber.Text.ToString());
                if (Point_temp_result == null)
                {
                    MessageBox.Show("坐标转换：  输入为空，其中没有点位");
                    return;
                }

                if (Point_temp_result.ContainsKey(number_get))
                {

                    x = Point_temp_result[number_get].点X;
                    y = Point_temp_result[number_get].点Y;

                }
                else
                {

                    MessageBox.Show("坐标转换：  输入字典中没有该点位");
                    return;
                }

            }
            else
            {
                x = Convert.ToDouble(this.tb_inputx.Text.ToString());
                y = Convert.ToDouble(this.tb_inputy.Text.ToString());
            }

            if (this.cbb_HM_PTW.SelectedItem == null)
            {
                MessageBox.Show("坐标转换：  图像转世界为空,请设置");
                return;
            }
            if (this.cbb_HM_WTP.SelectedItem == null)
            {
                MessageBox.Show("坐标转换：  世界转图像为空,请设置");
                return;
            }
            HTuple output_x, output_y;
            int Trans_type = this.cbb_TransType.SelectedIndex;
            if (Trans_type == 0)
               HOperatorSet.AffineTransPoint2d( (HTuple)_executeBuffer.controlBuffer[this.cbb_HM_PTW.SelectedItem.ToString()+ ".tuple"], x,y,out output_x,out output_y);
            else
                HOperatorSet.AffineTransPoint2d((HTuple)_executeBuffer.controlBuffer[this.cbb_HM_WTP.SelectedItem.ToString()+".tuple"], x, y, out output_x, out output_y);

            int out_number_list = Convert.ToInt32(this.tb_outnumber.Text.ToString());
            //MessageBox.Show(""+_executeBuffer.controlBuffer[this.cbb_HM_WTP.SelectedItem.ToString()].GetType());
                if (Point_temp_result.ContainsKey(out_number_list))
                {    Point_temp_result[out_number_list].点X = output_x;
                     Point_temp_result[out_number_list].点Y = output_y;
                }
                else
                    Point_temp_result.Add(out_number_list,new PointName(output_x,output_y));
                //this.tb_outputx.Text = output_x.ToString();
               // this.tb_outputy.Text = output_y.ToString();
                Point_out_result = Point_temp_result;





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
        {/*
            if (!IsNumber(this.tb_innumber.Text.ToString()))
            {
                MessageBox.Show("输入点位编号1不是数字,请重新输入");
                return false;
            }*/
            if (this.cbb_Inputsource.SelectedIndex == 4)
            {
                if (!IsNumber(this.tb_inputx.Text.ToString()))
                {
                    MessageBox.Show("坐标转换：  X2 不是数字,请重新输入");
                    return false;
                }
                if (!IsNumber(this.tb_inputy.Text.ToString()))
                {
                    MessageBox.Show("坐标转换：  Y2 不是数字,请重新输入");
                    return false;
                }

            }/*
            else
            {
                if (!IsNumber(this.tb_innumber.Text.ToString()))
                {
                    MessageBox.Show("输入点位编号2不是数字,请重新输入");
                    return false;
                }

            }*/

            if (!IsNumber(this.tb_outnumber.Text.ToString()))
            {
                MessageBox.Show("坐标转换：  出点位编号不是数字,请重新输入");
                return false;
            }
            return true;

        }
        internal void SetParaImage(ExecuteBuffer test)
        {
            if (cbb_HM_WTP.Items.Count == 0)
            {
                foreach (string keyc in test.controlBuffer.Keys)
                    if (keyc.Contains(".tuple"))
                            cbb_HM_WTP.Items.Add(keyc.Substring(0,keyc.Length-6));

            }
            else
            {

                int m = 0;
                foreach (string keyc in test.controlBuffer.Keys)
                {
                    for (int i = 0; i < cbb_HM_WTP.Items.Count; i++)
                    {
                        if (keyc == (cbb_HM_WTP.Items[i].ToString()+ ".tuple"))
                            break;
                        m = i;
                        if (m == cbb_HM_WTP.Items.Count - 1 && keyc.Contains(".tuple"))
                            cbb_HM_WTP.Items.Add(keyc.Substring(0,keyc.Length-6));

                    }
                }


            }


            if (cbb_HM_PTW.Items.Count == 0)
            {
                foreach (string keyc in test.controlBuffer.Keys)
                    if (keyc.Contains(".tuple"))
                        cbb_HM_PTW.Items.Add(keyc.Substring(0,keyc.Length-6));

            }
            else
            {

                int m = 0;
                foreach (string keyc in test.controlBuffer.Keys)
                {
                    for (int i = 0; i < cbb_HM_PTW.Items.Count; i++)
                    {
                        if (keyc == (cbb_HM_PTW.Items[i].ToString()+".tuple"))
                            break;
                        m = i;
                        if (m == cbb_HM_PTW.Items.Count - 1 && keyc.Contains(".tuple"))
                            cbb_HM_PTW.Items.Add(keyc.Substring(0,keyc.Length-6));

                    }
                }


            }

        }

        private void cbb_Inputsource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbb_Inputsource.SelectedIndex == 3)
            {
                this.panel_inpnumber.Visible = true;
                this.panel_input.Visible = false;
                this.panel_outnumber.Visible = false;
            }
            else if (this.cbb_Inputsource.SelectedIndex == 4)
            {
                this.panel_input.Visible = true;
                this.panel_inpnumber.Visible = false;
                this.panel_outnumber.Visible = true;
            }
        }









    }
}
