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
    public partial class MathCalc : Form, ISerializable
    {
        public MathCalc(ExecuteBuffer buffer, bool addbuffer)
        {
            InitializeComponent();
            if (addbuffer)
            {
            //    if (buffer != null)

                 //   SetParaImage(buffer); 
            }
            this.cbb_CalcType.SelectedIndex = 0;
            this.cbb_CalcType1.SelectedIndex = 0;
            this.cbb_tri.SelectedIndex = 0;
            this.cbb_tri1.SelectedIndex = 0;
            this.cbb_Inputsource.SelectedIndex = 0;
            this.cbb_inputsource2.SelectedIndex = 0;
            this.panel_inno1.Visible = false;
           // this.panel_inno2.Visible = false;
            this.panel_input.Visible = false;

        }

        public MathCalc(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();

        this.cbb_CalcType.SelectedIndex = (Int32)(info.GetValue("TransType", typeof(Int32)));
        this.cbb_CalcType1.SelectedIndex = (Int32)(info.GetValue("TransType1", typeof(Int32)));

        this.cbb_Inputsource.SelectedIndex = (Int32)(info.GetValue("TransSource", typeof(Int32)));
        this.cbb_inputsource2.SelectedIndex = (Int32)(info.GetValue("TransSource2", typeof(Int32)));

            this.cbb_tri.SelectedIndex = (Int32)(info.GetValue("CbbType1", typeof(Int32)));
            this.cbb_tri1.SelectedIndex = (Int32)(info.GetValue("CbbType", typeof(Int32)));
            this.txt_number2.Text = (string)(info.GetValue("InitialNumber1", typeof(string)));
            this.tb_innumber.Text = (string)(info.GetValue("InitialNumber", typeof(string)));
            this.tb_outnumber.Text = (string)(info.GetValue("ListNumber", typeof(string)));
            this.tb_inputx.Text = (string)(info.GetValue("TbInputx", typeof(string)));
            this.tb_inputy.Text = (string)(info.GetValue("TbInputy", typeof(string)));
            this.tb_angle1.Text = (string)(info.GetValue("TbAngle1", typeof(string)));
            this.tb_angle.Text = (string)(info.GetValue("TbAngle", typeof(string)));
            if ((string)(info.GetValue("Angle_check", typeof(string))) == "True")
                this.check_angle.Checked = true;
            else
                this.check_angle.Checked = false;
} 
        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);





            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "TransType", this.cbb_CalcType.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "TransType1", this.cbb_CalcType1.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "TransSource", this.cbb_Inputsource.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "TransSource2", this.cbb_inputsource2.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "InitialNumber", this.tb_innumber.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "InitialNumber1", this.txt_number2.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "ListNumber", this.tb_outnumber.Text.ToString());

            IniFile.IniWriteValue(j.ToString(), "CbbType", this.cbb_tri.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "CbbType1", this.cbb_tri1.SelectedIndex.ToString());

            IniFile.IniWriteValue(j.ToString(), "TbAngle", this.tb_angle.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "TbAngle1", this.tb_angle1.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Angle_check", this.check_angle.Checked.ToString());
            IniFile.IniWriteValue(j.ToString(), "TbInputx", this.tb_inputx.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "TbInputy", this.tb_inputy.Text.ToString());
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("TransType", this.cbb_CalcType.SelectedIndex.ToString());
            info.AddValue("TransType1", this.cbb_CalcType1.SelectedIndex.ToString());
            info.AddValue("TransSource", this.cbb_Inputsource.SelectedIndex.ToString());
            info.AddValue("TransSource2", this.cbb_inputsource2.SelectedIndex.ToString());



            info.AddValue("InitialNumber", this.tb_innumber.Text.ToString());
            info.AddValue("InitialNumber1", this.txt_number2.Text.ToString());
            info.AddValue("ListNumber", this.tb_outnumber.Text.ToString());



            info.AddValue("CbbType", this.cbb_tri.SelectedIndex.ToString());
            info.AddValue("CbbType1", this.cbb_tri1.SelectedIndex.ToString());

            info.AddValue("TbInputx", this.tb_inputx.Text.ToString());
            info.AddValue("TbInputy", this.tb_inputy.Text.ToString());
            info.AddValue("TbAngle1", this.tb_angle1.Text.ToString());
            info.AddValue("TbAngle", this.tb_angle.Text.ToString());
            info.AddValue("Angle_check", this.check_angle.Checked.ToString());




        } 

        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            int Selectnumber = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransType"));
            this.cbb_CalcType.SelectedIndex = Selectnumber;
            int Selectnumber1 = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransType1"));
            this.cbb_CalcType1.SelectedIndex = Selectnumber1;
            int Selectnumber2 = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "CbbType"));
            this.cbb_tri.SelectedIndex = Selectnumber2;
            int Selectnumber3 = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "CbbType1"));
            this.cbb_tri1.SelectedIndex = Selectnumber3;

            if (IniFile.IniReadValue(j.ToString(), "Angle_check") == "True")
                this.check_angle.Checked = true;
            else
                this.check_angle.Checked = false;
            if (IniFile.IniReadValue(j.ToString(), "TransSource") != "")
                this.cbb_Inputsource.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransSource"));
            if (IniFile.IniReadValue(j.ToString(), "TransSource2") != "")
            this.cbb_inputsource2.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransSource2"));

            this.tb_innumber.Text = IniFile.IniReadValue(j.ToString(), "InitialNumber");
            this.txt_number2.Text = IniFile.IniReadValue(j.ToString(), "InitialNumber1");
            this.tb_outnumber.Text = IniFile.IniReadValue(j.ToString(), "ListNumber");
            this.tb_inputx.Text = IniFile.IniReadValue(j.ToString(), "TbInputx");
            this.tb_inputy.Text = IniFile.IniReadValue(j.ToString(), "TbInputy");


        }
        public void Run_transform(ExecuteBuffer _executeBuffer, 点GVName_halcon Point_result, 模板GVName_halcon Model_result, 圆GVName_halcon Circle_result, Dictionary<int, PointName> Point_temp_result, out Dictionary<int, PointName> Point_out_result)
        {
            int number_source = this.cbb_Inputsource.SelectedIndex;
            int number_get = Convert.ToInt32(this.tb_innumber.Text.ToString());
            HTuple x, y;
            HTuple modelangle =0, modelregionangle = 0;
            HTuple x2, y2;
            int inputlength1 = 0;

            Point_out_result = Point_temp_result;

            if (number_source == 0)
            {   if (Point_result == null )
                {
                    MessageBox.Show("算术计算：  输入为空，其中没有点位");
                    return;
                }

            if (Point_result.点X.TupleLength() < 1)
                {
                    MessageBox.Show("算术计算：  输入阵列长度不够，达不到需求");
                    return;

                }
                else
                {
                    x = Point_result.点X;
                    y = Point_result.点Y;
                    inputlength1 = Point_result.点X.TupleLength();

                }
            
            }
            else if (number_source == 1)
            {
                if (Model_result == null)
                {
                    MessageBox.Show("算术计算：  输入为空，其中没有点位");
                    return;
                }

                if (Model_result.点X.TupleLength() < 1)
                {
                    MessageBox.Show("算术计算：  输入阵列长度不够，达不到需求");
                    return;

                }
                else
                {
                    x = Model_result.点Y;
                    y = Model_result.点X;
                    modelangle = Model_result.角度Angle;
                    modelregionangle = Model_result.模板Angle;
                    inputlength1 = Model_result.点X.TupleLength();

                }

            }
            else if (number_source == 2)
            {
                if (Circle_result == null)
                {
                    MessageBox.Show("算术计算：  输入为空，其中没有点位");
                    return;
                }

                if (Circle_result.圆心X.TupleLength() < 1)
                {
                    MessageBox.Show("算术计算：  输入阵列长度不够，达不到需求");
                    return;

                }
                else
                {
                    x = Circle_result.圆心X;
                    y = Circle_result.圆心Y;
                    inputlength1 = Circle_result.圆心X.TupleLength();

                }

            }
            else
            {
                if (Point_temp_result == null)
                {
                    MessageBox.Show("算术计算：  输入为空，其中没有点位");
                    return;
                }

                if (Point_temp_result.ContainsKey(number_get))
                {

                    x = Point_temp_result[number_get].点X;
                    y = Point_temp_result[number_get].点Y;
                    inputlength1 = Point_temp_result[number_get].点X.TupleLength();

                }
                else
                {

                    MessageBox.Show("算术计算：  输入字典中没有该点位");
                    return;
                }

            }


            int number2 = this.cbb_inputsource2.SelectedIndex;
            int inputlength2 = 0;
            if (number2 == 0)
            {
                if (Point_temp_result == null)
                {
                    MessageBox.Show("算术计算：输入为空，其中没有点位");
                    return;
                }
                int number_get2 = Convert.ToInt32(this.txt_number2.Text.ToString());
                if (Point_temp_result.ContainsKey(number_get2))
                {

                    x2 = Point_temp_result[number_get2].点X;
                    y2 = Point_temp_result[number_get2].点Y;
                    inputlength2 = Point_temp_result[number_get2].点X.TupleLength();
                }
                else
                {

                    MessageBox.Show("算术计算：  输入字典中没有该点位");
                    return;
                }

            }
            else
            {
                x2 = Convert.ToDouble(this.tb_inputx.Text.ToString());
                y2 = Convert.ToDouble(this.tb_inputy.Text.ToString());
                inputlength2 = inputlength1;

            
            }


            HTuple output_x, output_y;
            int Trans_type = this.cbb_CalcType.SelectedIndex;
            int Calc_way = this.cbb_CalcType.SelectedIndex;
            int Calc_way1 = this.cbb_CalcType1.SelectedIndex;
            if (inputlength1 == inputlength2)
            {




                if (Calc_way == 0 && cbb_tri.SelectedIndex == 0)
                {
                    if (check_angle.Checked && number_source == 1)
                        output_y = x + x2 * Math.Cos(modelregionangle - modelangle);
                    else
                        output_y = x + x2 * Math.Cos(Convert.ToDouble(tb_angle.Text));

                }
                else if (Calc_way == 1 && cbb_tri.SelectedIndex == 0)
                {
                    if (check_angle.Checked && number_source == 1)
                        output_y = x - x2 * Math.Cos(modelregionangle - modelangle);
                    else
                        output_y = x - x2 * Math.Cos(Convert.ToDouble(tb_angle.Text));

                }
                else if (Calc_way == 2 && cbb_tri.SelectedIndex == 0)
                {
                    if (check_angle.Checked && number_source == 1)
                        output_y = x2 * Math.Cos(modelregionangle - modelangle) - x;
                    else
                        output_y = x2 * Math.Cos(Convert.ToDouble(tb_angle.Text)) - x;
                }
                else if (Calc_way == 0 && cbb_tri.SelectedIndex == 1)
                {
                    if (check_angle.Checked && number_source == 1)
                        output_y = x + x2 * Math.Sin(modelregionangle - modelangle);
                    else
                        output_y = x + x2 * Math.Sin(Convert.ToDouble(tb_angle.Text));

                }
                else if (Calc_way == 1 && cbb_tri.SelectedIndex == 1)
                {
                    if (check_angle.Checked && number_source == 1)
                        output_y = x - x2 * Math.Sin(modelregionangle - modelangle);
                    else
                        output_y = x - x2 * Math.Sin(Convert.ToDouble(tb_angle.Text));

                }
                else
                {
                    if (check_angle.Checked && number_source == 1)
                        output_y = x2 * Math.Sin(modelregionangle - modelangle) - x;
                    else
                        output_y = x2 * Math.Sin(Convert.ToDouble(tb_angle.Text)) - x;
                }



                if (Calc_way1 == 0 && cbb_tri1.SelectedIndex == 0)
                {
                    if (check_angle.Checked && number_source == 1)
                        output_x = y + y2 * Math.Cos(modelregionangle - modelangle  );
                    else
                        output_x = y + y2 * Math.Cos(Convert.ToDouble(tb_angle1.Text));

                }
                else if (Calc_way1 == 1 && cbb_tri1.SelectedIndex == 0)
                {
                    if (check_angle.Checked && number_source == 1)
                        output_x = y - y2 * Math.Cos(modelregionangle - modelangle);
                    else
                        output_x = y - y2 * Math.Cos(Convert.ToDouble(tb_angle1.Text));

                }
                else if (Calc_way1 == 2 && cbb_tri1.SelectedIndex == 0)
                {
                    if (check_angle.Checked && number_source == 1)
                        output_x = y2 * Math.Cos(modelregionangle - modelangle) - y;
                    else
                        output_x = y2 * Math.Cos(Convert.ToDouble(tb_angle1.Text)) - y;
                }
                else if (Calc_way1 == 0 && cbb_tri1.SelectedIndex == 1)
                {
                    if (check_angle.Checked && number_source == 1)
                        output_x = y + y2 * Math.Sin(modelregionangle - modelangle);
                    else
                        output_x = y + y2 * Math.Sin(Convert.ToDouble(tb_angle1.Text));

                }
                else if (Calc_way1 == 1 && cbb_tri1.SelectedIndex == 1)
                {
                    if (check_angle.Checked && number_source == 1)
                        output_x = y - y2 * Math.Sin(modelregionangle - modelangle);
                    else
                        output_x = y - y2 * Math.Sin(Convert.ToDouble(tb_angle1.Text));

                }
                else
                {
                    if (check_angle.Checked && number_source == 1)
                        output_x = y2 * Math.Sin(modelregionangle - modelangle) - y;
                    else
                        output_x = y2 * Math.Sin(Convert.ToDouble(tb_angle1.Text)) - y;
                }




                int out_number_list = Convert.ToInt32(this.tb_outnumber.Text.ToString());
                if (Point_temp_result.ContainsKey(out_number_list))
                {
                    Point_temp_result[out_number_list].点X = output_x;
                    Point_temp_result[out_number_list].点Y = output_y;
                }
                else
                    Point_temp_result.Add(out_number_list, new PointName(output_x, output_y));

                Point_out_result = Point_temp_result;
                this.label2.Text = "x 偏移：" + output_x.ToString() + "\n" + "y偏移是：" + output_y.ToString();
            }



            








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
            if (this.cbb_Inputsource.SelectedIndex == 3)
            {
                if (!IsNumber(this.tb_innumber.Text.ToString()))
                {
                    MessageBox.Show("算术计算：  输入点位编号1不是数字,请重新输入");
                    return;
                }
            }
            if (this.cbb_inputsource2.SelectedIndex == 1)
            {
                if (!IsNumber(this.tb_inputx.Text.ToString()))
                {
                    MessageBox.Show("算术计算：  X2 不是数字,请重新输入");
                    return;
                }
                if (!IsNumber(this.tb_inputy.Text.ToString()))
                {
                    MessageBox.Show("算术计算：  Y2 不是数字,请重新输入");
                    return;
                }

            }
            else
            {
                if (!IsNumber(this.txt_number2.Text.ToString()))
                {
                    MessageBox.Show("算术计算：  输入点位编号2不是数字,请重新输入");
                    return;
                }
            
            }

            if (!IsNumber(this.tb_outnumber.Text.ToString()))
            {
                MessageBox.Show("算术计算：  输出点位编号不是数字,请重新输入");
                return;
            }


            this.Visible = false;
        }





        public  bool Check_pal()
        {
            if (this.cbb_Inputsource.SelectedIndex == 3)
            {
                if (!IsNumber(this.tb_innumber.Text.ToString()))
                {
                    MessageBox.Show("算术计算：  输入点位编号1不是数字,请重新输入");
                    return false;
                }
            }
            if (this.cbb_inputsource2.SelectedIndex == 1)
            {
                if (!IsNumber(this.tb_inputx.Text.ToString()))
                {
                    MessageBox.Show("算术计算：  X2 不是数字,请重新输入");
                    return false;
                }
                if (!IsNumber(this.tb_inputy.Text.ToString()))
                {
                    MessageBox.Show("算术计算：  Y2 不是数字,请重新输入");
                    return false;
                }

            }
            else
            {
                if (!IsNumber(this.txt_number2.Text.ToString()))
                {
                    MessageBox.Show("算术计算：  输入点位编号2不是数字,请重新输入");
                    return false;
                }
            
            }

            if (!IsNumber(this.tb_outnumber.Text.ToString()))
            {
                MessageBox.Show("算术计算：  输出点位编号不是数字,请重新输入");
                return false;
            }
            return true;
        
        }



        private void cbb_Inputsource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbb_Inputsource.SelectedIndex == 3)
                this.panel_inno1.Visible = true;
            else
                this.panel_inno1.Visible = false;

            
        }

        private void cbb_inputsource2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbb_inputsource2.SelectedIndex == 0)
            {
                this.panel_inno2.Visible = true;
                this.panel_input.Visible = false;
            }
            else
            {
                this.panel_inno2.Visible = false;
                this.panel_input.Visible = true;
            
            }
        }







    }
}
