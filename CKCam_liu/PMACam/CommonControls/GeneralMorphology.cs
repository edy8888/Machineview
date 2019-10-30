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
using System.Runtime.Serialization; 
using System.Text.RegularExpressions;
namespace PMACam
{
    [Serializable]
    public partial class GeneralMorphology : Form, ISerializable
    {
        public GeneralMorphology(ExecuteBuffer buffer, bool addbuffer)
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
        }
        public GeneralMorphology(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();

            this.txt_iterations.Text = (string)(info.GetValue("MorIterations", typeof(string)));
            this.mor_regionout.Text = (string)(info.GetValue("MorRegionout", typeof(string)));
            this.mor_structElement.Text = (string)(info.GetValue("MorStruct", typeof(string)));
            this.mor_region.Text = (string)(info.GetValue("MorRegionin", typeof(string)));
            this.comboBox1.SelectedIndex = (Int32)(info.GetValue("MorOperation", typeof(Int32)));

} 

        internal void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "MorOperation", this.comboBox1.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "MorRegionin", this.mor_region.Text);
            IniFile.IniWriteValue(j.ToString(), "MorStruct", this.mor_structElement.Text);
            IniFile.IniWriteValue(j.ToString(), "MorRegionout", this.mor_regionout.Text);
            IniFile.IniWriteValue(j.ToString(), "MorIterations", this.txt_iterations.Text);
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("MorOperation", this.comboBox1.SelectedIndex.ToString());

            info.AddValue("MorIterations", this.txt_iterations.Text.ToString());
            info.AddValue("MorRegionout", this.mor_regionout.Text.ToString());
            info.AddValue("MorStruct", this.mor_structElement.Text.ToString());
            info.AddValue("MorRegionin", this.mor_region.Text.ToString());







        } 
        public void Run_Mor(ExecuteBuffer _executeBuffer, out ExecuteBuffer outexecutebuffer)
        {

            outexecutebuffer = _executeBuffer;
            HObject regionOutresult;
            if (this.comboBox1.SelectedIndex == 0)
                HOperatorSet.Opening(_executeBuffer.imageBuffer[this.mor_region.Text.ToString()], _executeBuffer.imageBuffer[this.mor_structElement.Text.ToString()], out regionOutresult);
            else if (this.comboBox1.SelectedIndex == 1)
                HOperatorSet.Closing(_executeBuffer.imageBuffer[this.mor_region.Text.ToString()],  _executeBuffer.imageBuffer[this.mor_structElement.Text.ToString()], out regionOutresult);
            else if (this.comboBox1.SelectedIndex == 2)
                HOperatorSet.Erosion1(_executeBuffer.imageBuffer[this.mor_region.Text.ToString()], _executeBuffer.imageBuffer[this.mor_structElement.Text.ToString()], out regionOutresult, Convert.ToInt32(this.txt_iterations.Text.ToString()));
            else 
               HOperatorSet.Dilation1(_executeBuffer.imageBuffer[this.mor_region.Text.ToString()],_executeBuffer.imageBuffer[this.mor_structElement.Text.ToString()],out regionOutresult,Convert.ToInt32(this.txt_iterations.Text.ToString()));

            if (_executeBuffer.imageBuffer[this.mor_regionout.Text.ToString()] != null)
            {
                if (_executeBuffer.imageBuffer[this.mor_regionout.Text.ToString()].IsInitialized())
                {
                    _executeBuffer.imageBuffer[this.mor_regionout.Text.ToString()].Dispose();
                }
            }
            _executeBuffer.imageBuffer[this.mor_regionout.Text.ToString()] = regionOutresult;
            outexecutebuffer = _executeBuffer;

        }


        
        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            int Selectnumber = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "MorOperation"));
            this.mor_regionout.Text = IniFile.IniReadValue(j.ToString(), "MorRegionout");
            this.comboBox1.SelectedIndex = Selectnumber;
            this.mor_regionout.Items.Clear();
            mor_region.Items.Add(IniFile.IniReadValue(j.ToString(), "MorRegionin"));
            mor_region.SelectedIndex = 0;
            mor_structElement.Items.Add(IniFile.IniReadValue(j.ToString(), "MorStruct"));
            mor_structElement.SelectedIndex = 0;
            this.txt_iterations.Text = IniFile.IniReadValue(j.ToString(), "MorIterations");
        }

        

        public string getRegionoutname()
        {
            if (this.mor_regionout.Text.ToString() != null)
                return this.mor_regionout.Text.ToString();
            else
                return null;
        
        }

        internal void SetParaImage(ExecuteBuffer test)
        {
            if (mor_region.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    mor_region.Items.Add(keyc);

            }
            else
            {

                int m = 0;
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < mor_region.Items.Count; i++)
                    {
                        if (keyc == mor_region.Items[i].ToString())
                            break;
                        m = i;
                        if (m == mor_region.Items.Count - 1)
                            mor_region.Items.Add(keyc);

                    }
                }


            }



            if (mor_structElement.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    mor_structElement.Items.Add(keyc);

            }
            else
            {

                int m = 0;
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < mor_structElement.Items.Count; i++)
                    {
                        if (keyc == mor_structElement.Items[i].ToString())
                            break;
                        m = i;
                        if (m == mor_structElement.Items.Count - 1)
                            mor_structElement.Items.Add(keyc);

                    }
                }


            }


        }



        internal void SetParaRegion(ExecuteBuffer test)
        {
            
            if (this.mor_regionout.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    mor_regionout.Items.Add(keyc);

            }
            else
            {

                int m = 0;
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < mor_regionout.Items.Count; i++)
                    {
                        if (keyc == mor_regionout.Items[i].ToString())
                            break;
                        m = i;
                        if (m == mor_regionout.Items.Count - 1)
                            mor_regionout.Items.Add(keyc);

                    }
                }

            }


        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {


        }

        private void mor_regionout_SelectedIndexChanged(object sender, EventArgs e)
        {

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


        public bool Check_pal()
        {

            if (this.mor_region.Text == "")
            {
                MessageBox.Show("输入区域region为空,请选择区域");
                return false;
            }
            if (this.mor_structElement.Text == "")
            {
                MessageBox.Show("输入结构structElement为空,请输入");
                return false;
            }
            if (this.mor_regionout.Text == "")
            {
                MessageBox.Show("输出图像区域region为空,请输入");
                return false;
            }
            if (!IsNumber(this.txt_iterations.Text.ToString()))
           {
                MessageBox.Show(" 输入iterations不是数字,请重新输入");
                return false;
           }
            
            return true;

        }


        private void btn_Sure_Click(object sender, EventArgs e)
        {

            UpdateMorTest.OnSendUpdateMorTest(new UpdateMorTestEventArgs(this.mor_regionout.Text));
            if (Check_pal())
                this.Visible = false;
        }





    }
}
