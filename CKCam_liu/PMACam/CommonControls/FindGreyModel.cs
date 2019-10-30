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
    public partial class FindGreyModel : Form
    {




        HTuple hv_MovementOfObject;


        public FindGreyModel(ExecuteBuffer buffer, bool addbuffer)
        {
            InitializeComponent();

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
            HTuple hv_Rowzz, hv_Columnzz, hv_Anglezz, hv_Error;
            pmatest_result = new List<模板GVName>();
            pmatest_result_halcon = new 模板GVName_halcon();
            object_graphic_out = new List<HObject>();
            out_info = "";
            HHomMat2D Matrix = new HHomMat2D();
            HTuple angleStart,angleExtent;
            double temp1 = double.Parse(txt_angleStart.Text.ToString());
            
            HOperatorSet.TupleRad(temp1, out angleStart);

            double temp2 = double.Parse(txt_angleExtent.Text.ToString());
            HOperatorSet.TupleRad(temp2, out angleExtent);
            if (In_Image == null)
            {
                MessageBox.Show("形状模板匹配：    请选择输入图像");
                out_info = "形状模板匹配：    请选择输入图像";
                return false;
            }
            if (ModelID.Length == 0)
            {
                MessageBox.Show("形状模板匹配：    请选择输入模板");
                out_info = "形状模板匹配：    请选择输入模板";
                return false;
            }
            try
            {
                


                HOperatorSet.BestMatchRot(In_Image,ModelID,(HTuple)Convert.ToDouble(txt_angleStart.Text.ToString()), (HTuple)Convert.ToDouble(txt_angleExtent.Text.ToString()),
                    (HTuple)Convert.ToDouble(txt_greydis.Text.ToString()), this.model_subPixel.SelectedItem.ToString(), out hv_Rowzz, out hv_Columnzz, out hv_Anglezz, out hv_Error);
           


            }

            catch (Exception)
            {

                return false;
            }
            double Error_number = Convert.ToDouble(this.txt_maxerror.Text.ToString());
            if (hv_Error < Error_number)
            { 
         //   HOperatorSet.GetShapeModelContours(out ShapeModel, ModelID, 1);
            pmatest_result_halcon.点X = hv_Columnzz;
            pmatest_result_halcon.点Y = hv_Rowzz;
            pmatest_result_halcon.角度Angle = hv_Anglezz;
           // pmatest_result_halcon.分数Score = hv_Score;
        }
            for (int i = 0; i < hv_Rowzz.Length; i = i + 1)
            {
                if (hv_Error < Error_number)
                { 
                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Rowzz[i], hv_Columnzz[i], hv_Anglezz[i], out hv_MovementOfObject);
            //    HOperatorSet.AffineTransContourXld(ShapeModel, out ho_ModelAtNewPosition, hv_MovementOfObject);
            //    object_graphic_out.Add(ho_ModelAtNewPosition);

                pmatest_result.Add(new 模板GVName(i.ToString(), hv_Columnzz[i].D.ToString("F3"), hv_Rowzz[i].D.ToString("F3"), hv_Anglezz[i].D.ToString("F3"), "1"));


            }

            }


            return true;

        }

        internal void SetParawindow(ExecuteBuffer test)
        {

            if (this.Model_modelID.Items.Count == 0)
            {
                foreach (string keyc in test.controlBuffer.Keys)
                {   if (keyc.Contains(".shapemode_grey"))
                        Model_modelID.Items.Add(keyc.Substring(0,keyc.Length-15));
                }


            }
            else
            {
                int m = 0;
                if (Model_modelID.SelectedItem != null)
                {
                    if (!test.controlBuffer.ContainsKey(Model_modelID.SelectedItem.ToString()+".shapemode_grey"))
                        Model_modelID.Items.Remove(Model_modelID.SelectedItem.ToString());
                }
                foreach (string keyc in test.controlBuffer.Keys)
                {
                    for (int i = 0; i < Model_modelID.Items.Count; i++)
                    {
                        if (keyc == (Model_modelID.Items[i].ToString()+".shapemode_grey"))
                            break;
                        m = i;
                        if (m == Model_modelID.Items.Count - 1 && keyc.Contains(".shapemode_grey"))
                            Model_modelID.Items.Add(keyc.Substring(0,keyc.Length-15));

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
            IniFile.IniWriteValue(j.ToString(), "NumMatches", this.txt_greydis.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Score", this.txt_maxerror.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "SubPixel", this.model_subPixel.SelectedItem.ToString());
            IniFile.IniWriteValue(j.ToString(), "Grey_dis", this.txt_greydis.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Max_error", this.txt_maxerror.Text.ToString());


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
            this.txt_greydis.Text = IniFile.IniReadValue(j.ToString(), "Grey_dis");
            this.txt_maxerror.Text = IniFile.IniReadValue(j.ToString(), "Max_error");

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

            if (!IsNumber(this.txt_maxerror.Text.ToString()))
            {
                MessageBox.Show(" 输入参数minScore不是数字,请重新输入");
                return false;
            }

            if (!IsNumber(this.txt_greydis.Text.ToString()))
            {
                MessageBox.Show(" 输入参数numMatches不是数字,请重新输入");
                return false;
            }


            if (!IsNumber(this.txt_maxerror.Text.ToString()))
            {
                MessageBox.Show(" 输入参数最大冗错不是数字,请重新输入");
                return false;
            }

            if (!IsNumber(this.txt_greydis.Text.ToString()))
            {
                MessageBox.Show(" 输入参数灰度差值不是数字,请重新输入");
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