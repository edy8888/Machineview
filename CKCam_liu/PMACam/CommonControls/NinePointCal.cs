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

namespace PMACam
{
    public partial class NinePointCal : Form
    {
        List<double> x_point = new List<double>();
        List<double> y_point = new List<double>();
        HTuple PixToWorld, WorldToPixel;
        HTuple WorldX, WorldY;
        HTuple temp_row = new HTuple();
        HTuple temp_column = new HTuple();
        HTuple temp_row_last = new HTuple();
        HTuple temp_column_last = new HTuple();
        
        double m,n;



     
        public NinePointCal()
        {
            InitializeComponent();
            this.dataGridView1.Columns.Add("Columns1", "No");
            this.dataGridView1.Columns.Add("Columns2","X");
            this.dataGridView1.Columns.Add("Columns3", "Y");
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }

        public int getlabel()
        {
            return Convert.ToInt32(this.textBox1.Text.ToString());
        }



        public void SetLabelcom()
        {
            this.textBox1.Text = "0";
        }

        public void Setlabel(double x,double y)
        {
 
            int number = Convert.ToInt32(this.textBox1.Text.ToString());
            if (number == 0)
            {
                x_point.Clear();
                y_point.Clear();


            }


            if (number < 9)
            {
                x_point.Add(x);
                y_point.Add(y);
                this.textBox1.Text = (number + 1).ToString();

                this.dataGridView1.Rows.Add(number.ToString(), x.ToString(), y.ToString());


            }


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }




        public bool NinePointCalMake(SourceBuffer _sourceBuffer,ExecuteBuffer _executeBuffer,out SourceBuffer outsourcebuffer,out ExecuteBuffer outexecutebuffer)
        {
            outsourcebuffer = _sourceBuffer;
            outexecutebuffer = _executeBuffer;
            m = Convert.ToDouble(txt_m.Text.ToString());
            n = Convert.ToDouble(txt_n.Text.ToString());
            WorldX = new HTuple(0, m, m, 0, -m, -m, -m, 0, m);
            WorldY = new HTuple(0, 0, -n, -n, -n, 0, n, n, n);
            for (int i = 0; i < x_point.Count; i++)
            {
                HOperatorSet.TupleConcat(temp_row, (HTuple)x_point[i], out temp_row_last);
                temp_row = temp_row_last;
                HOperatorSet.TupleConcat(temp_column,(HTuple)y_point[i],out temp_column_last);
                temp_column = temp_column_last;

            } try
            {
                HOperatorSet.VectorToHomMat2d(temp_column, temp_row, WorldY, WorldX, out PixToWorld);
                HOperatorSet.VectorToHomMat2d(WorldY, WorldX, temp_column, temp_row, out WorldToPixel);
            }
            catch (Exception)
            {
                MessageBox.Show("点位错误，请确认匹配是否正确");
                return false;
            }
            //保存PixelToWorld到本地
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "PixelToWorld保存";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string strPath = sfd.FileName;
                if (!strPath.EndsWith(".tup"))
                {
                    strPath += ".tup";
                }
                Match match = Regex.Match(strPath, "(\\w+)(.tup)$");
                HOperatorSet.WriteTuple(PixToWorld, strPath);
                bool cPixToWorld_add = true;
                foreach (Info_Source keybuffcontrol in _sourceBuffer._s_ControlBuffer.Keys)
                {
                    if (keybuffcontrol.Name == match.Groups[1].ToString())
                    {
                        cPixToWorld_add = false;
                        break;
                    }
                }
                Info_Source control_add_key = new Info_Source();
                control_add_key.Name = match.Groups[1].ToString()+".tuple";
                control_add_key.Path = strPath;
                control_add_key.Type = "tuple";
                if (cPixToWorld_add)
                    _sourceBuffer._s_ControlBuffer.Add(control_add_key, PixToWorld);
                else
                    _sourceBuffer._s_ControlBuffer[control_add_key] = PixToWorld;



                if (_executeBuffer.controlBuffer.ContainsKey(match.Groups[1].ToString()))
                {
                    _executeBuffer.controlBuffer[match.Groups[1].ToString()] = PixToWorld;
                }
                else
                {
                    _executeBuffer.controlBuffer.Add(match.Groups[1].ToString(), PixToWorld);
                }



            }

            //保存WorldToPixel到本地
            SaveFileDialog sfd2 = new SaveFileDialog();
            sfd2.Title = "WorldToPixel保存";
            sfd2.RestoreDirectory = true;
            if (sfd2.ShowDialog() == DialogResult.OK)
            {
                string strPath = sfd2.FileName;
                if (!strPath.EndsWith(".tup"))
                {
                    strPath += ".tup";
                }
                Match match = Regex.Match(strPath, "(\\w+)(.tup)$");
                HOperatorSet.WriteTuple(WorldToPixel, strPath);
                bool WorldToPixel_add = true;
                foreach (Info_Source keybuffcontrol in _sourceBuffer._s_ControlBuffer.Keys)
                {
                    if (keybuffcontrol.Name == match.Groups[1].ToString())
                    {
                        WorldToPixel_add = false;
                        break;
                    }
                }
                Info_Source control_add_key = new Info_Source();
                control_add_key.Name = match.Groups[1].ToString()+".tuple";
                control_add_key.Path = strPath;
                control_add_key.Type = "tuple";
                if (WorldToPixel_add)
                    _sourceBuffer._s_ControlBuffer.Add(control_add_key, WorldToPixel);
                else
                    _sourceBuffer._s_ControlBuffer[control_add_key] = WorldToPixel;



                if (_executeBuffer.controlBuffer.ContainsKey(match.Groups[1].ToString()))
                {
                    _executeBuffer.controlBuffer[match.Groups[1].ToString()] = WorldToPixel;
                }
                else
                {
                    _executeBuffer.controlBuffer.Add(match.Groups[1].ToString(), WorldToPixel);
                }

            }
            outsourcebuffer = _sourceBuffer;
            outexecutebuffer = _executeBuffer;
            MessageBox.Show("9点标定成功");

            return true;



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "0";

            this.dataGridView1.Rows.Clear();

        }
    }
}
