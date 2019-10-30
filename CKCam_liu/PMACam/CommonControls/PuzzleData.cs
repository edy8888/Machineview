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
    public partial class PuzzleData : Form, ISerializable
    {
        int number = 0;
        double x_get = 0;
        double y_get = 0;
        public PuzzleData()
        {
            InitializeComponent();
            this.dataGridView1.Columns.Add("Columns1", "No");
            this.dataGridView1.Columns.Add("Columns2", "振镜");
            this.dataGridView1.Columns.Add("Columns3", "轴");
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.cbb_Inputsource.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 2;
            this.label_show.Text = "x 偏移：" + x_get.ToString() + "\n" + "y偏移是：" + y_get.ToString();
           // this.panel_inno2.Visible = false;


        }

        public PuzzleData(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();
        this.dataGridView1.Columns.Add("Columns1", "No");
        this.dataGridView1.Columns.Add("Columns2", "振镜");
        this.dataGridView1.Columns.Add("Columns3", "轴");
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        this.cbb_Inputsource.SelectedIndex = 0;
        this.comboBox2.SelectedIndex = 2;
        this.label_show.Text = "x 偏移：" + x_get.ToString() + "\n" + "y偏移是：" + y_get.ToString();


        this.comboBox2.SelectedIndex = (Int32)(info.GetValue("Combox_check", typeof(Int32)));
        this.cbb_Inputsource.SelectedIndex = (Int32)(info.GetValue("TransSource", typeof(Int32)));
            this.textBox_xstart.Text = (string)(info.GetValue("XStart", typeof(string)));
            this.textBox_ystart.Text = (string)(info.GetValue("YStart", typeof(string)));
            this.textBox_xstep.Text = (string)(info.GetValue("XStep", typeof(string)));
            this.textBox_ystep.Text = (string)(info.GetValue("YStep", typeof(string)));
            this.tb_outnumber.Text = (string)(info.GetValue("PointNumber", typeof(string)));

            x_get = (double)(info.GetValue("X_value", typeof(double)));
            y_get = (double)(info.GetValue("Y_value", typeof(double)));
} 
                public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("Combox_check", this.comboBox2.SelectedIndex.ToString());
            info.AddValue("TransSource", this.cbb_Inputsource.SelectedIndex.ToString());
            info.AddValue("XStart", this.textBox_xstart.Text.ToString());
            info.AddValue("YStart", this.textBox_ystart.Text.ToString());
            info.AddValue("XStep", this.textBox_xstep.Text.ToString());
            info.AddValue("YStep", this.textBox_ystep.Text.ToString());
            info.AddValue("PointNumber", this.tb_outnumber.Text.ToString());
            info.AddValue("X_value", (double)x_get);
            info.AddValue("Y_value", (double)y_get);




        } 
        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);





            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);

            IniFile.IniWriteValue(j.ToString(), "TransSource", this.cbb_Inputsource.SelectedIndex.ToString());

  
            IniFile.IniWriteValue(j.ToString(), "X_value", x_get.ToString());
            IniFile.IniWriteValue(j.ToString(), "Y_value", y_get.ToString());
            IniFile.IniWriteValue(j.ToString(), "Combox_check", this.comboBox2.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "XStart", this.textBox_xstart.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "YStart", this.textBox_ystart.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "XStep", this.textBox_xstep.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "YStep", this.textBox_ystep.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "PointNumber", this.tb_outnumber.Text.ToString());



            
        }

        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);



            if (IniFile.IniReadValue(j.ToString(), "TransSource") != "")
                this.cbb_Inputsource.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "TransSource"));
            if (IniFile.IniReadValue(j.ToString(), "Combox_check") != "")
                this.comboBox2.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "Combox_check"));
            if (IniFile.IniReadValue(j.ToString(), "X_value") != "")
                x_get = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "X_value"));
            if (IniFile.IniReadValue(j.ToString(), "Y_value") != "")
                y_get = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Y_value"));

            if (IniFile.IniReadValue(j.ToString(), "XStart") != "")
                this.textBox_xstart.Text = IniFile.IniReadValue(j.ToString(), "XStart");
            if (IniFile.IniReadValue(j.ToString(), "YStart") != "")
                this.textBox_ystart.Text = IniFile.IniReadValue(j.ToString(), "YStart");
            if (IniFile.IniReadValue(j.ToString(), "XStep") != "")
                this.textBox_xstep.Text = IniFile.IniReadValue(j.ToString(), "XStep");
            if (IniFile.IniReadValue(j.ToString(), "YStep") != "")
                this.textBox_ystep.Text = IniFile.IniReadValue(j.ToString(), "YStep");
            if (IniFile.IniReadValue(j.ToString(), "PointNumber") != "")
                this.tb_outnumber.Text = IniFile.IniReadValue(j.ToString(), "PointNumber");
            this.label_show.Text = "x 偏移：" + x_get.ToString() +"\n" + "y偏移是：" + y_get.ToString();






        }
        public void Run_transform(模板GVName_halcon Model_result,  Dictionary<int, PointName> Point_temp_result, out Dictionary<int, PointName> Point_out_result)
        {
            int number_source = this.cbb_Inputsource.SelectedIndex;

            HTuple x, y;
            HTuple modelangle =0, modelregionangle = 0;

            HTuple output_x = 0, output_y = 0;
            double middle_value = 0;
            double num_all = 0;
            Point_out_result = Point_temp_result;

            if (this.comboBox2.SelectedIndex == 0)
            {
                if (number < 10)
                {
                    this.dataGridView1.Rows.Add(number.ToString(), Model_result.点X[0].D.ToString(), 0);
                    number++;
                }
                else if (number < 20 && number >= 10)
                {
                    this.dataGridView1.Rows[number - 10].Cells[2].Value = Model_result.点X[0].D.ToString();
                    number++;
                }
                else
                {
                    number = 0;


                    this.comboBox2.SelectedIndex = 1;
                    for (int i = 0; i < 10; i++)
                    {
                        middle_value = Math.Abs(Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) - Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value)) / (Convert.ToDouble(textBox_xstart.Text) + i * Convert.ToDouble(textBox_xstep.Text));
                        num_all = middle_value + num_all;
                    }
                    x_get = num_all / 10;
                    num_all = 0;

                }
                middle_value = 0;
 
            }

            if (this.comboBox2.SelectedIndex == 1)
            {

                if (number < 10)
                    this.dataGridView1.Rows[number].Cells[1].Value = Model_result.点Y[0].D.ToString();
                else if (number < 20 && number >= 10)
                {
                    this.dataGridView1.Rows[number-10].Cells[2].Value = Model_result.点Y[0].D.ToString();
                }
                else
                {
                    number = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        middle_value = Math.Abs(Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) - Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value)) / (Convert.ToDouble(textBox_ystart.Text) + i * Convert.ToDouble(textBox_ystep.Text));
                        num_all = middle_value + num_all;
                    }
                    y_get = num_all / 10;
                    this.comboBox2.SelectedIndex = 2;

                }
                number++;
            }
            if (this.comboBox2.SelectedIndex == 2)
            {
                int out_number_list = Convert.ToInt32(this.tb_outnumber.Text.ToString());
                if (Point_temp_result.ContainsKey(out_number_list))
                {
                    Point_temp_result[out_number_list].点X = x_get;
                    Point_temp_result[out_number_list].点Y = y_get;
                }
                else
                    Point_temp_result.Add(out_number_list, new PointName(output_x, output_y));

                Point_out_result = Point_temp_result;
            }

            this.label_show.Text = "x 偏移：" + x_get.ToString() + "\n" +"y偏移是：" + y_get.ToString();
            
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




            if (!IsNumber(this.tb_outnumber.Text.ToString()))
            {
                MessageBox.Show("算术计算：  输出点位编号不是数字,请重新输入");
                return;
            }


            this.Visible = false;
        }





        public  bool Check_pal()
        {



            if (!IsNumber(this.tb_outnumber.Text.ToString()))
            {
                MessageBox.Show("算术计算：  输出点位编号不是数字,请重新输入");
                return false;
            }
            return true;
        
        }









    }
}
