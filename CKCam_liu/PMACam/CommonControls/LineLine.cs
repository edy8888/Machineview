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
    public partial class LineLine : Form, ISerializable
    {
        public LineLine(ExecuteBuffer buffer, bool addbuffer)
        {
            InitializeComponent();
            if (addbuffer)
            {
                if (buffer != null)

                    SetParaImage(buffer); 
            }

            this.cbb_Inputsource.SelectedIndex = 0;
            this.cbb_inputsource2.SelectedIndex = 0;
            this.panel_inno1.Visible = true;
           // this.panel_inno2.Visible = false;


        }

        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);

            IniFile.IniWriteValue(j.ToString(), "TransSource", this.cbb_Inputsource.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "TransSource2", this.cbb_inputsource2.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "InitialNumber", this.tb_innumber.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "ListNumber", this.tb_outnumber.Text.ToString());

            IniFile.IniWriteValue(j.ToString(), "Txtnumber", this.txt_number2.Text.ToString());

        }

        public LineLine(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();
        this.cbb_Inputsource.SelectedIndex = 0;
        this.cbb_inputsource2.SelectedIndex = 0;
        this.panel_inno1.Visible = false;


        this.cbb_Inputsource.SelectedIndex = (Int32)(info.GetValue("TransSource", typeof(Int32)));
        this.cbb_inputsource2.SelectedIndex = (Int32)(info.GetValue("TransSource2", typeof(Int32)));
        this.tb_innumber.Text = (string)(info.GetValue("InitialNumber", typeof(string)));
        this.tb_outnumber.Text = (string)(info.GetValue("ListNumber", typeof(string)));

        this.txt_number2.Text = (string)(info.GetValue("Txtnumber", typeof(string)));

}
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("InitialNumber", this.tb_innumber.Text.ToString());
            info.AddValue("TransSource", this.cbb_Inputsource.SelectedIndex.ToString());
            info.AddValue("TransSource2", this.cbb_inputsource2.SelectedIndex.ToString());
            info.AddValue("ListNumber", this.tb_outnumber.Text.ToString());

            info.AddValue("Txtnumber", this.txt_number2.Text.ToString());





        } 
        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
          //  int Selectnumber = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransType"));

        //    int Selectnumber1 = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransType1"));


            if (IniFile.IniReadValue(j.ToString(), "TransSource") != "")
            this.cbb_Inputsource.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransSource"));
            if (IniFile.IniReadValue(j.ToString(), "TransSource2") != "")
            this.cbb_inputsource2.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransSource2"));

            this.tb_innumber.Text = IniFile.IniReadValue(j.ToString(), "InitialNumber");
            this.tb_outnumber.Text = IniFile.IniReadValue(j.ToString(), "ListNumber");
            this.txt_number2.Text = IniFile.IniReadValue(j.ToString(), "Txtnumber");



        }
        public bool Run_transform(ExecuteBuffer _executeBuffer, 模板GVName_halcon Model_result, 圆GVName_halcon Circle_result, Dictionary<int, PointName> Point_temp_result, out Dictionary<int, PointName> Point_out_result, List<直线GVName> newline)
        {
            int number_source = this.cbb_Inputsource.SelectedIndex;
            int number_get = Convert.ToInt32(this.tb_innumber.Text.ToString());
            HTuple x, y;
            HTuple modelangle =0, modelregionangle = 0;
            int inputlength1 = 0;
            int number_line = Convert.ToInt32(this.txt_number2.Text);
            Point_out_result = Point_temp_result;






             if (newline == null)
             {
              //   MessageBox.Show("线线交点:直线为空，请设置");
                 return false;

             }
             if (newline.Count < 2)
             {
             //    MessageBox.Show("线线交点:直线为空，请设置");
                 return false;
             }
             if (newline.Count <= number_line)
             {
                 //MessageBox.Show("线线交点:直线编号过大，请修改");
                 return false;
             }
            if (newline.Count <= number_get)
            {
              //  MessageBox.Show("线线交点:直线编号过大，请修改");
                return false;
            }

            double xrow1a = Convert.ToDouble(newline[number_get].点1X);
            double xcol1a = Convert.ToDouble(newline[number_get].点1Y);
            double xrow2a = Convert.ToDouble(newline[number_get].点2X);
            double xcol2a = Convert.ToDouble(newline[number_get].点2Y);
            double xrow1 = Convert.ToDouble(newline[number_line].点1X);
             double xcol1 = Convert.ToDouble(newline[number_line].点1Y);
             double xrow2 = Convert.ToDouble(newline[number_line].点2X);
             double xcol2 = Convert.ToDouble(newline[number_line].点2Y);
            HTuple output_x,output_y,distance;
            HOperatorSet.IntersectionLines(xrow1, xcol1, xrow2, xcol2, xrow1a, xcol1a, xrow2a, xcol2a, out output_x, out output_y,out distance);
           
 

                int out_number_list = Convert.ToInt32(this.tb_outnumber.Text.ToString());
                if (Point_temp_result.ContainsKey(out_number_list))
                {
                    Point_temp_result[out_number_list].点X = output_x;
                    Point_temp_result[out_number_list].点Y = output_y;
                }
                else
                   Point_temp_result.Add(out_number_list, new PointName(output_x, output_y));

                Point_out_result = Point_temp_result;

                this.label_show.Text = "点线距离：" + distance.ToString() +"\n" +"焦点X：" + output_x.ToString() + "\n" + "焦点y：" + output_y.ToString();
            return true;
            
            }






        public string get_outnumber()
        {
            return tb_outnumber.Text;

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

        internal void SetParaImage(ExecuteBuffer test)
        {





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













    }
}
