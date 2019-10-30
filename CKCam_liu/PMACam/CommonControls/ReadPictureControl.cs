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
using System.Drawing.Imaging;
using System.Runtime.Serialization; 
namespace PMACam
{
    [Serializable]
    public partial class ReadPictureControl : Form, ISerializable
    {
        public ReadPictureControl(ExecuteBuffer buffer,bool addbuffer)
        {
            InitializeComponent();
            if (addbuffer)
            {
                if (buffer != null)
                {

                    SetParaImage(buffer);

                }
            }

        }

        Parameters Read_parameters = new Parameters();
        internal void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "Camera_checkd", this.checkBox_相机.Checked.ToString());
            IniFile.IniWriteValue(j.ToString(), "ImageBuffer", this.read_ImageName.Text);
            IniFile.IniWriteValue(j.ToString(), "File_choose", this.textBox_file.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Exptime", this.label2.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Exp_checked", this.checkBox_exp.Checked.ToString());

        }
        internal void ReadData(List<string> n_Path, int j)
        {
            string camera_check,exp_check;
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            string path;
            camera_check = IniFile.IniReadValue(j.ToString(), "Camera_checkd");
            if (camera_check == "True")
                checkBox_相机.Checked = true;
            else
                checkBox_相机.Checked = false;
            exp_check = IniFile.IniReadValue(j.ToString(), "Exp_checked");
            if (exp_check == "True")
                checkBox_exp.Checked = true;
            else
                checkBox_exp.Checked = false;
            path = IniFile.IniReadValue(j.ToString(), "File_choose");
            textBox_file.Text = path;
            this.read_ImageName.Text = IniFile.IniReadValue(j.ToString(), "ImageBuffer");
            this.label2.Text = IniFile.IniReadValue(j.ToString(), "Exptime");




        }
        public ReadPictureControl(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();



            this.read_ImageName.Text = (string)(info.GetValue("Exptime", typeof(string)));
            this.label2.Text = (string)(info.GetValue("ImageBuffer", typeof(string)));

            this.textBox_file.Text = (string)(info.GetValue("File_choose", typeof(string)));
            if ((string)(info.GetValue("Camera_checkd", typeof(string))) == "True")
                this.checkBox_相机.Checked = true;
            else
                this.checkBox_相机.Checked = false;
            if ((string)(info.GetValue("Exp_checkd", typeof(string))) == "True")
                this.checkBox_exp.Checked = true;
            else
                this.checkBox_exp.Checked = false;


        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("ImageBuffer", this.read_ImageName.Text.ToString());
            info.AddValue("Exptime", this.label2.Text.ToString());
            info.AddValue("File_choose", this.textBox_file.Text.ToString());
            info.AddValue("Camera_checkd", this.checkBox_相机.Checked.ToString());
            info.AddValue("Exp_checkd", this.checkBox_exp .Checked.ToString());




        } 

        internal void SetParaImage(ExecuteBuffer test)
        {
            if (this.read_ImageName.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    if(keyc.Contains(".img"))
                        read_ImageName.Items.Add(keyc.Substring(0,keyc.Length-4));

            }
            else
            {
                int m = 0;
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < read_ImageName.Items.Count; i++)
                    {    
                        if (keyc == (read_ImageName.Items[i].ToString()+".img"))
                            break;
                        m = i;
                        if (m == read_ImageName.Items.Count-1 && keyc.Contains(".img"))
                            read_ImageName.Items.Add(keyc.Substring(0,keyc.Length-4));
                        
                    }
                }

            }

        }





        internal Parameters_buffer ReadData_buf(List<string> n_Path, int j)
        {
            Parameters_buffer out_para = new Parameters_buffer();
            out_para.Imagebuffer_val = this.read_ImageName.Text+".img";
            return out_para ;



        }

        public bool Set_Pictureway()
        {
            if (checkBox_相机.Checked)
                return true;
            else
                return false;
        }
        public bool Set_Exp()
        {
            if (this.checkBox_exp.Checked)
                return true;
            else
                return false;
        }



        public bool Cam_OpenImage(out HObject ho_Image)
        {

                HOperatorSet.GenEmptyObj(out ho_Image);
                if (textBox_file.Text == "")
                {
                    MessageBox.Show("请确认文件路径是否正确");
                    return false;
                }
                try
                {
                    HOperatorSet.ReadImage(out ho_Image, textBox_file.Text);
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("请确认文件格式是否正确");
                    return false;
                }
        }
        public void Cam_OpenImageout()
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "bmp files (*.bmp)|*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strPathName = openFileDialog1.FileName;
                textBox_file.Text = strPathName;
            } 

        }
        private void button_文件位置_Click(object sender, EventArgs e)
        {
            Cam_OpenImageout();
        }

        public void Modifytime(string timevalue)
        {
            this.label2.Text = timevalue;
        }


        public Parameters Get_parameters()
        {
            Read_parameters.Camera_checkedpal = checkBox_相机.Checked;
            Read_parameters.File_pathpal = textBox_file.Text;
            Read_parameters.Image_out = this.read_ImageName.Text;

            return Read_parameters;
        }

        public void Set_parameters(Parameters read_parameters)
        {
            checkBox_相机.Checked = read_parameters.Camera_checkedpal;
            textBox_file.Text = read_parameters.File_pathpal;
            this.read_ImageName.Text = read_parameters.Image_out;

        }

        public string  get_imagebuf()
        {
            return this.read_ImageName.Text;
        }
        public int get_time()
        {
            if (this.label2.Text != "")
                return Convert.ToInt32(this.label2.Text);
            else
                return 0;
        }
        private void btn_Sure_Click(object sender, EventArgs e)
        {

            string Shape = this.read_ImageName.Text;
            if (Shape == "")
            {
                MessageBox.Show("请填写输出图像名");
                return;
            }
            UpdateBufferReadImage.OnSendUpdateReadImageBuffer(new UpdateBufferReadImageEventArgs(Shape+".img", 0));
            this.read_ImageName.SelectedValue = this.read_ImageName.Text;
            this.Visible = false;
        }

        public void Send_name()
        {
            string Shape = this.read_ImageName.Text;
            if (Shape == "")
            {
                MessageBox.Show("请填写输出图像名");
                return;
            }
            UpdateBufferReadImage.OnSendUpdateReadImageBuffer(new UpdateBufferReadImageEventArgs(Shape+".img", 0));
        
        }

        private void ReadPictureControl_Load(object sender, EventArgs e)
        {

        }

        private void button_exp_Click(object sender, EventArgs e)
        {
            UpdateBufferReadImage.OnSendUpdateReadImageBuffer(new UpdateBufferReadImageEventArgs("TIMEMODIFY", 0));
        }
    }
    public class Parameters
    {

        public bool Camera_checkedpal;
        public string File_pathpal;
        public string Image_out;
        public string window_in;


        public Parameters()
        {


        }

    }


        public class Parameters_buffer
    {

        public String  Imagebuffer_val;
        public string Controlbuffer_val;


        public Parameters_buffer()
        {


        }

    }
}
