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
    public partial class FindScaledShapeModel : Form
    {



        HObject ShapeModel;
        HTuple hv_MovementOfObject;


        public FindScaledShapeModel(ExecuteBuffer buffer, bool addbuffer)
        {
            InitializeComponent();
            panel_参数设置1.Visible = false;
            panel_参数设置2.Visible = false;
            panel_参数设置3.Visible = false;
            model_subPixel.SelectedIndex = 0;
            if (addbuffer)
            {
                if (buffer != null)
                {
                    SetParawindow(buffer);
                    SetParaImage(buffer);

                }
            }
        }

        private void FindScaledShapeModel_Load(object sender, EventArgs e)
        {

        }

        internal void SetParaImage(ExecuteBuffer test)
        {
            int defaultnumber = 0;
             int get_number = 0;

            if (this.Model_image.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    if (keyc.Contains(".img"))
                    {

                        Model_image.Items.Add(keyc.Substring(0,keyc.Length-4));
                        if (keyc.Contains("image"))
                        {
                            get_number  = Convert.ToInt32(keyc.Substring(5, keyc.Length - 9));

                            if (get_number > defaultnumber)
                            {
                                Model_image.Text = keyc.Substring(0,keyc.Length-4);
                                defaultnumber = get_number;
                            }
                        }
                    }

                        
                }

            }
            else
            {
                int m = 0;
                if (Model_image.SelectedItem != null)
                {
                    if (!test.imageBuffer.ContainsKey(Model_image.SelectedItem.ToString()+".img"))
                        Model_image.Items.Remove(Model_image.SelectedItem.ToString());
                }
                for (int i = 0; i < Model_image.Items.Count; i++)
                {
                    if (!test.imageBuffer.ContainsKey(Model_image.Items[i].ToString()+".img"))
                        Model_image.Items.Remove(Model_image.Items[i].ToString());
                }

                    foreach (string keyc in test.imageBuffer.Keys)
                    {


                        for (int i = 0; i < Model_image.Items.Count; i++)
                        {

                            if (keyc == (Model_image.Items[i].ToString()+".img"))
                                break;
                            m = i;
                            if (m == Model_image.Items.Count - 1 && keyc.Contains(".img"))
                                Model_image.Items.Add(keyc.Substring(0,keyc.Length-4));

                        }
                    }

            }

        }



        public bool MatchingModel(HObject In_Image, HTuple ModelID, out List<模板GVName> pmatest_result,  out 模板GVName_halcon pmatest_result_halcon,out List<HObject> object_graphic_out,out string out_info)
        {
            HTuple hv_Rowzz, hv_Columnzz, hv_Anglezz, hv_Score, hv_scalezz;
            pmatest_result = new List<模板GVName>();
            pmatest_result_halcon = new 模板GVName_halcon();
            object_graphic_out = new List<HObject>();
            out_info = "";
            HObject ho_ModelAtNewPosition;
            HHomMat2D Matrix = new HHomMat2D();
            HTuple angleStart,angleExtent;
            double temp1 = double.Parse(txt_angleStart.Text.ToString());
            
            HOperatorSet.TupleRad(temp1, out angleStart);

            double temp2 = double.Parse(txt_angleExtent.Text.ToString());
            HOperatorSet.TupleRad(temp2, out angleExtent);
            if (In_Image == null)
            {
                MessageBox.Show("形状模板匹配：   请先选择图像");
                out_info = "形状模板匹配：    Please Choose Picture first";
                return false;
            }
            if (ModelID.Length == 0)
            {
                MessageBox.Show("形状模板匹配：  请先选择模板");
                out_info = "形状模板匹配： 请先选择模板";
                return false;
            }
            try
            {
                
                HOperatorSet.FindScaledShapeModel(In_Image, ModelID, (HTuple)Convert.ToDouble(txt_angleStart.Text.ToString()), (HTuple)Convert.ToDouble(txt_angleExtent.Text.ToString()), Convert.ToDouble(this.txt_scaleMin.Text.ToString()),
                    Convert.ToDouble(this.txt_scaleMax.Text.ToString()), Convert.ToDouble(this.txt_minScore.Text.ToString()), Convert.ToDouble(this.txt_numMatches.Text.ToString()), Convert.ToDouble(this.txt_maxOverlap.Text.ToString()), this.model_subPixel.SelectedItem.ToString(),
                     Convert.ToInt32(this.txt_numLevels.Text.ToString()), Convert.ToDouble(this.txt_greediness.Text.ToString()), out hv_Rowzz, out hv_Columnzz, out hv_Anglezz, out hv_scalezz, out hv_Score);
                 
    
             //   HOperatorSet.FindScaledShapeModel(In_Image, ModelID, (HTuple)Convert.ToDouble(txt_angleStart.Text.ToString()), angleExtent, 1, 1, 0.6, 0, 0.5, "least_squares", 0, 0.8, out hv_Rowzz, out hv_Columnzz, out hv_Anglezz, out hv_scalezz, out hv_Score);


            }

            catch (Exception)
            {
                out_info = "形状模板匹配：匹配时报错，请检查参数是否正确";
                return false;
            }

            HOperatorSet.GetShapeModelContours(out ShapeModel, ModelID, 1);
            pmatest_result_halcon.点X = hv_Columnzz;
            pmatest_result_halcon.点Y = hv_Rowzz;
            pmatest_result_halcon.角度Angle = hv_Anglezz;
            pmatest_result_halcon.分数Score = hv_Score;

            for (int i = 0; i < hv_Score.Length; i = i + 1)
            {

                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Rowzz[i], hv_Columnzz[i], hv_Anglezz[i], out hv_MovementOfObject);
                HOperatorSet.AffineTransContourXld(ShapeModel, out ho_ModelAtNewPosition, hv_MovementOfObject);
                object_graphic_out.Add(ho_ModelAtNewPosition);

                pmatest_result.Add(new 模板GVName(i.ToString(), hv_Columnzz[i].D.ToString("F3"), hv_Rowzz[i].D.ToString("F3"), hv_Anglezz[i].D.ToString("F3"), hv_Score[i].D.ToString("F3")));




            }


            return true;

        }

        internal void SetParawindow(ExecuteBuffer test)
        {

            if (this.Model_modelID.Items.Count == 0)
            {
                foreach (string keyc in test.controlBuffer.Keys)
                {   if (keyc.Contains(".shapemode_contour"))
                        Model_modelID.Items.Add(keyc.Substring(0,keyc.Length-18));
                }


            }
            else
            {
                int m = 0;
                if (Model_modelID.SelectedItem != null)
                {
                    if (!test.controlBuffer.ContainsKey(Model_modelID.SelectedItem.ToString()+".shapemode_contour"))
                        Model_modelID.Items.Remove(Model_modelID.SelectedItem.ToString());
                }
                foreach (string keyc in test.controlBuffer.Keys)
                {
                    for (int i = 0; i < Model_modelID.Items.Count; i++)
                    {
                        if (keyc == (Model_modelID.Items[i].ToString()+".shapemode_contour"))
                            break;
                        m = i;
                        if (m == Model_modelID.Items.Count - 1 && keyc.Contains(".shapemode_contour"))
                            Model_modelID.Items.Add(keyc.Substring(0,keyc.Length-18));

                    }
                }

            }

        }



        internal void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "InputImage", this.Model_image.SelectedItem.ToString());
            IniFile.IniWriteValue(j.ToString(), "InputModel", this.Model_modelID.SelectedItem.ToString());
            IniFile.IniWriteValue(j.ToString(), "Angle_start", this.txt_angleStart.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Angle_end", this.txt_angleExtent.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "NumMatches", this.txt_numMatches.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Score", this.txt_minScore.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "ScaleMin", this.txt_scaleMin.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "ScaleMax", this.txt_scaleMax.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "MaxOverlap", this.txt_maxOverlap.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "NumLevels", this.txt_numLevels.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Greediness", this.txt_greediness.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "SubPixel", this.model_subPixel.SelectedItem.ToString());



        }

        public string get_Modelname()
        {
            if (this.Model_modelID.SelectedItem != null)
                return this.Model_modelID.SelectedItem.ToString();
            else
                return null;
        }
        public string get_Imagename()
        {
            if (this.Model_image.SelectedItem != null)
                return this.Model_image.SelectedItem.ToString();
            else
                return null;


        }
        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);


            this.txt_angleStart.Text = IniFile.IniReadValue(j.ToString(), "Angle_start");
            this.txt_angleExtent.Text = IniFile.IniReadValue(j.ToString(), "Angle_end");
            this.txt_numMatches.Text = IniFile.IniReadValue(j.ToString(), "NumMatches");
            this.txt_minScore.Text = IniFile.IniReadValue(j.ToString(), "Score");
            this.txt_scaleMin.Text = IniFile.IniReadValue(j.ToString(), "ScaleMin");
            this.txt_scaleMax.Text = IniFile.IniReadValue(j.ToString(), "ScaleMax");
            this.txt_maxOverlap.Text = IniFile.IniReadValue(j.ToString(), "MaxOverlap");
            this.txt_numLevels.Text = IniFile.IniReadValue(j.ToString(), "NumLevels");
            this.txt_greediness.Text = IniFile.IniReadValue(j.ToString(), "Greediness");
            this.txt_angleStart.Text = IniFile.IniReadValue(j.ToString(), "Angle_start");
            Model_image.Items.Clear();
            Model_image.Items.Add(IniFile.IniReadValue(j.ToString(), "InputImage"));
            Model_image.SelectedIndex = 0;
            Model_modelID.Items.Clear();
            Model_modelID.Items.Add(IniFile.IniReadValue(j.ToString(), "InputModel"));
            Model_modelID.SelectedIndex = 0;
           // model_subPixel.Items.Clear();
           // model_subPixel.Items.Add(IniFile.IniReadValue(j.ToString(), "SubPixel"));
            model_subPixel.Text = IniFile.IniReadValue(j.ToString(), "SubPixel");

        }
        private void button_参数设置3_Click(object sender, EventArgs e)
        {
            if (panel_参数设置3.Visible)
            {
                panel_参数设置3.Visible = false;
                button_参数设置2.Top = button_参数设置3.Bottom;
                button_参数设置1.Top = button_参数设置2.Bottom;
            }
            else
            {
                panel_参数设置3.Visible = true;
                panel_参数设置3.Top = button_参数设置3.Bottom;
                panel_参数设置1.Visible = false;
                panel_参数设置2.Visible = false;
                button_参数设置2.Top = panel_参数设置3.Bottom;
                button_参数设置1.Top = button_参数设置2.Bottom;

            }
        }

        private void button_参数设置2_Click(object sender, EventArgs e)
        {
            if (panel_参数设置2.Visible)
            {
                panel_参数设置2.Visible = false;
                button_参数设置1.Top = button_参数设置2.Bottom;
            }
            else
            {
                panel_参数设置2.Visible = true;
                panel_参数设置1.Visible = false;


                if (panel_参数设置3.Visible)
                {
                    button_参数设置2.Top = button_参数设置3.Bottom;
                }
                panel_参数设置2.Top = button_参数设置2.Bottom;
                button_参数设置1.Top = panel_参数设置2.Bottom;
                panel_参数设置3.Visible = false;
                //  button2.Top = button2.Top - 300;
                //  button_参数设置2.Text = "》";
                // button2.Left = button2.Left - 100;
                // button3.Left = button3.Left - 100;
            }

        }

        private void button_参数设置1_Click(object sender, EventArgs e)
        {
            if (panel_参数设置1.Visible)
            {
                panel_参数设置1.Visible = false;

                //  button1.Top = button_参数设置1.Top + 300;
                //  button_参数设置1.Text = "《";
                //   button_参数设置2.Left = button_参数设置2.Left + 100;
                //   button3.Left = button3.Left + 100;
            }
            else
            {
                panel_参数设置1.Visible = true;



                if (panel_参数设置3.Visible)
                {
                    button_参数设置2.Top = button_参数设置3.Bottom;
                    button_参数设置1.Top = button_参数设置2.Bottom;
                }

                if (panel_参数设置2.Visible)
                {
                    button_参数设置1.Top = button_参数设置2.Bottom;

                }
                panel_参数设置2.Visible = false;
                panel_参数设置3.Visible = false;
                panel_参数设置1.Top = button_参数设置1.Bottom;

                // button1.Top = button1.Top - 300;
                //  button_参数设置1.Text = "》";
                // button2.Left = button2.Left - 100;
                // button3.Left = button3.Left - 100;
            }

        }

        private void button_参数设置3_Click_1(object sender, EventArgs e)
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

            if (this.Model_image.Text == "")
            {
                MessageBox.Show("输入图像为空,请输入");
                return false;
            }
            if (this.Model_modelID.Text == "")
            {
                MessageBox.Show("输入模板为空,请选择模板");
                return false;
            }
            if (!IsNumber(this.txt_maxOverlap.Text.ToString()))
            {
                MessageBox.Show(" 输入参数maxOverlap是数字,请重新输入");
                return false;
            }
            if (!IsNumber(this.txt_numLevels.Text.ToString()))
            {
                MessageBox.Show(" 输入参数numLevels不是数字,请重新输入");
                return false;
            }

            if (!IsNumber(this.txt_greediness.Text.ToString()))
            {
                MessageBox.Show(" 输入参数greediness不是数字,请重新输入");
                return false;
            }

            if (!IsNumber(this.txt_angleStart.Text.ToString()))
            {
                MessageBox.Show(" 输入参数angleStart不是数字,请重新输入");
                return false;
            }
            if (!IsNumber(this.txt_angleExtent.Text.ToString()))
            {
                MessageBox.Show(" 输入参数angleExtent不是数字,请重新输入");
                return false;
            }

            if (!IsNumber(this.txt_minScore.Text.ToString()))
            {
                MessageBox.Show(" 输入参数minScore不是数字,请重新输入");
                return false;
            }

            if (!IsNumber(this.txt_numMatches.Text.ToString()))
            {
                MessageBox.Show(" 输入参数numMatches不是数字,请重新输入");
                return false;
            }

            if (!IsNumber(this.txt_scaleMin.Text.ToString()))
            {
                MessageBox.Show(" 输入参数scalcMin不是数字,请重新输入");
                return false;
            }

            if (!IsNumber(this.txt_scaleMax.Text.ToString()))
            {
                MessageBox.Show(" 输入参数scaleMax不是数字,请重新输入");
                return false;
            }
            






            return true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Check_pal())
                this.Visible = false;
        }
        
    }
}