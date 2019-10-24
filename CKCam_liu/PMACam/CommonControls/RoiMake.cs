using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using ViewROI;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using System.Text.RegularExpressions;
using System.Runtime.Serialization; 
namespace PMACam
{
    [Serializable]
    public partial class RoiMake : Form, ISerializable
    {
        public List<ViewROI.ROI> sou_regions;
        ROI_Model Sousuo_region_get = new ROI_Model();
        public bool SetROi = false;
        bool Button_press = false;
        public RoiMake(ExecuteBuffer buffer, bool addbuffer)
        {
            InitializeComponent();
             if (addbuffer)
            {
                if (buffer != null)
                {
                    SetParaImage(buffer);
                  //  SetParaImage(buffer);

                }
            }

            panel_rectangle2.Visible = false;
            panel_circle.Visible = true;
            this.cbb_angle.SelectedIndex = 0;
            this.cbb_point.SelectedIndex = 0;
            this.panel_circle.Visible = true;


        }



        internal void SetParaImage(ExecuteBuffer test)
        {




            if (this.cbb_image.Items.Count == 0)
            {
                int defaultnumber = 0;
                int get_number = 0;
                foreach (string keyc in test.imageBuffer.Keys)
                    if (keyc.Contains(".img"))
                    {
                        cbb_image.Items.Add(keyc.Substring(0, keyc.Length - 4));
                        if (keyc.Contains("image"))
                        {
                            get_number = Convert.ToInt32(keyc.Substring(5, keyc.Length - 9));

                            if (get_number > defaultnumber)
                            {
                                cbb_image.Text = keyc.Substring(0, keyc.Length - 4);
                                defaultnumber = get_number;
                            }
                        }
                    }


            }
            else
            {

                int m = 0;
                if (cbb_image.SelectedItem != null)
                {
                    if (!test.imageBuffer.ContainsKey(cbb_image.SelectedItem.ToString() + ".img"))
                        cbb_image.Items.Remove(cbb_image.SelectedItem.ToString() + ".img");
                }
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < cbb_image.Items.Count; i++)
                    {
                        if (keyc == (cbb_image.Items[i].ToString() + ".img"))
                            break;
                        m = i;
                        if (m == cbb_image.Items.Count - 1 && keyc.Contains(".img"))
                            cbb_image.Items.Add(keyc.Substring(0, keyc.Length - 4));

                    }
                }


            }

        }




        public string Imagename()
        {
            return cbb_image.Text.ToString() + ".img";
        }

        public void Panel_show()
        {

             if (comboBox1.SelectedIndex == 1)
            {

                panel_rectangle2.Visible = true;
                panel_circle.Visible = false;
            }
            else
            {

                panel_rectangle2.Visible = false;
                panel_circle.Visible = true;
            }
        }

        public double Getangle1(HTuple x1, HTuple y1, HTuple x2, HTuple y2)
        {
            
            HTuple angle = 0;
          double   x3 = x2 - x1;
          double   y3 = y2 - y1;
          if (x3 == 0 && y3 > 0)
              angle = Math.PI / 2;
          else if (x3 == 0 && y3 < 0)
              angle = -Math.PI / 2;
          else
              HOperatorSet.TupleAtan(y3 / x3, out angle);

          return angle;
        }
        public bool Genshape(ExecuteBuffer _executeBuffer, out ExecuteBuffer outexecutebuffer,HWndCtrl hWndCtrl, 模板GVName_halcon Model_result, Dictionary<int, PointName> Point_temp_result, List<直线GVName> newline)
        {

            outexecutebuffer = _executeBuffer;
            int x_get=0, y_get=0;
            double angle_get = 0;
            bool Model_check = false;

            HObject new_type;
            if (!_executeBuffer.imageBuffer.ContainsKey(this.cbb_image.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("感兴趣区域：无法找到输入图像");
                return false;
            }
            if (this.checkBox1.Checked)
            {
                if (Button_press)
                {
                    new_type = sou_regions[0].getRegion();
                }
                else
                    new_type = _executeBuffer.imageBuffer[this.cbb_image.SelectedItem.ToString() + ".img"];


            }
            else
            {
                if (this.checkBox_get.Checked)
                {
                    if (this.cbb_point.SelectedIndex == 0)
                    {
                        if (Model_result == null)
                        {
                            MessageBox.Show("生成ROI:匹配列表为空，请设置");
                            return false;
                        }
                        if (Model_result.点X.TupleLength() < 1)
                        {
                            MessageBox.Show("生成ROI:匹配列表为空，请设置");
                            // result_info = " 查找直线: 匹配列表为空，请设置";
                            return false;
                        }
                        x_get = Convert.ToInt32(Model_result.点Y[0].D);
                        y_get = Convert.ToInt32(Model_result.点X[0].D);
                        Model_check = true;
                    }
                    else
                    {

                        int number1 = Convert.ToInt32(this.tb_no.Text);
                        if (!Point_temp_result.ContainsKey(number1))
                        {
                            MessageBox.Show("生成ROI:全局列表点无此点，请设置");

                            return false;
                        }
                        else
                        {
                            x_get = Convert.ToInt32(Point_temp_result[number1].点Y.D);
                            y_get = Convert.ToInt32(Point_temp_result[number1].点X.D);

                        }
                    }



                    if (this.cbb_angle.SelectedIndex == 0)
                    {

                        if (Model_check)
                        {
                            angle_get = Model_result.模板Angle[0] - Model_result.角度Angle[0];

                        }
                        else
                        {
                            if (Model_result == null)
                            {
                                MessageBox.Show("生成ROI:匹配列表为空，请设置");
                                return false;
                            }
                            if (Model_result.点X.TupleLength() < 1)
                            {
                                MessageBox.Show("生成ROI:匹配列表为空，请设置");
                                // result_info = " 查找直线: 匹配列表为空，请设置";
                                return false;
                            }
                            angle_get = Model_result.模板Angle[0] - Model_result.角度Angle[0];

                        }

                    }
                    else if (this.cbb_angle.SelectedIndex == 1)
                    {
                        if (newline == null)
                        {
                            MessageBox.Show("生成ROI:直线为空，请设置");
                            return false;

                        }
                        if (newline.Count < 1)
                        {
                            MessageBox.Show("生成ROI:直线为空，请设置");
                            return false;
                        }
                        double xrow1 = Convert.ToDouble(newline[0].点1X);
                        double xcol1 = Convert.ToDouble(newline[0].点1Y);
                        double xrow2 = Convert.ToDouble(newline[0].点2X);
                        double xcol2 = Convert.ToDouble(newline[0].点2Y);
                        angle_get = Getangle1(xrow1, xcol1, xrow2, xcol2);
                    }
                    else
                        angle_get = Convert.ToDouble(this.txt_rec2phi.Text.ToString());
                }


                outexecutebuffer = _executeBuffer;


                if (this.comboBox1.SelectedIndex == 1)
                {
                    if (!this.checkBox_get.Checked)
                    {
                        x_get = Convert.ToInt32(this.textBox_rec2row1.Text.ToString());
                        y_get = Convert.ToInt32(this.textBox_rec2col1.Text.ToString());
                    }
                    HOperatorSet.GenRectangle2(out new_type, x_get, y_get, -angle_get, Convert.ToInt32(this.textBox_rec2len1.Text.ToString()), Convert.ToInt32(this.textBox_rec2len2.Text.ToString()));
                }
                else
                {
                    if (!this.checkBox_get.Checked)
                    {
                        x_get = Convert.ToInt32(this.textBox_circlerow.Text.ToString());
                        y_get = Convert.ToInt32(this.textBox_circlecolumn.Text.ToString());
                    }
                    HOperatorSet.GenCircle(out new_type, x_get, y_get, Convert.ToInt32(this.textBox_circleradius.Text.ToString()));
                }


                if (_executeBuffer.imageBuffer[this.shape_name.Text + ".region"] != null)
                {
                    if (_executeBuffer.imageBuffer[this.shape_name.Text + ".region"].IsInitialized())
                    {
                        _executeBuffer.imageBuffer[this.shape_name.Text + ".region"].Dispose();
                    }
                }
            
            }

            hWndCtrl.addIconicVar(new_type);
            hWndCtrl.repaint();
            _executeBuffer.imageBuffer[this.shape_name.Text+".region"] = new_type;
            outexecutebuffer = _executeBuffer;

            return true;
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
            if (this.shape_name.Text.ToString() == "")
            {
                    MessageBox.Show("生成ROI： 输出名字为空,请设置");
                    return false;
                
            }

             if(comboBox1.SelectedIndex == 1)
            {
                if (!IsNumber(this.textBox_rec2row1.Text.ToString()))
                {
                    MessageBox.Show("生成ROI： Row1不是数字,请重新输入");
                    return false;
                }
                if (!IsNumber(this.textBox_rec2col1.Text.ToString()))
                {
                    MessageBox.Show("生成ROI： Column1不是数字,请重新输入");
                    return false;
                }
                if (!IsNumber(this.textBox_rec2len1.Text.ToString()))
                {
                    MessageBox.Show("生成ROI： Row2不是数字,请重新输入");
                    return false;
                }
                if (!IsNumber(this.textBox_rec2len2.Text.ToString()))
                {
                    MessageBox.Show("生成ROI： Column2不是数字,请重新输入");
                    return false;
                }
                if (!IsNumber(this.txt_rec2phi.Text.ToString()))
                {
                    MessageBox.Show("生成ROI： Phi不是数字,请重新输入");
                    return false;
                }
            }
            else
            {
                if (!IsNumber(this.textBox_circlerow.Text.ToString()))
                {
                    MessageBox.Show("生成ROI： Row不是数字,请重新输入");
                    return false;
                }
                if (!IsNumber(this.textBox_circlecolumn.Text.ToString()))
                {
                    MessageBox.Show("生成ROI： Column不是数字,请重新输入");
                    return false;
                }
                if (!IsNumber(this.textBox_circleradius.Text.ToString()))
                {
                    MessageBox.Show("生成ROI： Radius不是数字,请重新输入");
                    return false;
               }
            }
            return true;

        }

        public void MakeRoi_model(ROIController mx)
        {


            if (Sousuo_region_get.Roi_model == "ROICircle")
            {
                mx.genCircle(Sousuo_region_get.Roi_x, Sousuo_region_get.Roi_y, Sousuo_region_get.Roi_Cir1radius, ref sou_regions);
                mx.reset();
            }
            else if (Sousuo_region_get.Roi_model == "ROIRectangle2")
            {
                mx.genRect2(Sousuo_region_get.Roi_x, Sousuo_region_get.Roi_y, Sousuo_region_get.Roi_Rec2phi, Sousuo_region_get.Roi_Rec2length1, Sousuo_region_get.Roi_Rec2length2, ref sou_regions);
                mx.reset();
            }
        }
        public RoiMake(SerializationInfo info, StreamingContext context)
        {
            InitializeComponent();
            panel_rectangle2.Visible = false;
            panel_circle.Visible = true;
            this.cbb_angle.SelectedIndex = 0;
            this.cbb_point.SelectedIndex = 0;

            this.comboBox1.SelectedIndex = (Int32)(info.GetValue("ShapeModel", typeof(Int32)));
            this.cbb_point.SelectedIndex = (Int32)(info.GetValue("CbbPoint", typeof(Int32)));
            this.cbb_angle.SelectedIndex = (Int32)(info.GetValue("CbbAngle", typeof(Int32)));
            if ((string)(info.GetValue("Check_Roi", typeof(string))) == "True")
                this.checkBox_get.Checked = true;
            else
                this.checkBox_get.Checked = false;
            this.shape_name.Text = (string)(info.GetValue("Modelname", typeof(string)));
            this.tb_no.Text = (string)(info.GetValue("TbNo", typeof(string)));

            this.textBox_rec2row1.Text = (string)(info.GetValue("Rec2row1", typeof(string)));
            this.textBox_rec2col1.Text = (string)(info.GetValue("Rec2col1", typeof(string)));
            this.textBox_rec2len1.Text = (string)(info.GetValue("Rec2len1", typeof(string)));
            this.textBox_rec2len2.Text = (string)(info.GetValue("Rec2len2", typeof(string)));
            this.txt_rec2phi.Text = (string)(info.GetValue("Rec2phi", typeof(string)));
            this.textBox_circlerow.Text = (string)(info.GetValue("Cirrow", typeof(string)));
            this.textBox_circlecolumn.Text = (string)(info.GetValue("Circol", typeof(string)));
            this.textBox_circleradius.Text = (string)(info.GetValue("Cirradius", typeof(string)));
            string Regionnull_check = (string)(info.GetValue("Region_Not_null", typeof(string)));
            if ((string)(info.GetValue("ButtonPress", typeof(string))) == "True")
            {
                Button_press = true;
                this.button_搜索形状.BackColor = Color.Yellow;
            }
            else
                Button_press = false;
            if ((string)(info.GetValue("Check_handle", typeof(string))) == "True")
                this.checkBox1.Checked = true;
            else
                this.checkBox1.Checked = false;
            if (Regionnull_check == "ROIRectangle2")
            {

                Sousuo_region_get.Roi_model = "ROIRectangle2";
                Sousuo_region_get.Roi_Rec2length1 = Convert.ToDouble((string)(info.GetValue("Rec2length1", typeof(string))));
                Sousuo_region_get.Roi_Rec2length2 = Convert.ToDouble((string)(info.GetValue("Rec2length2", typeof(string))));
                Sousuo_region_get.Roi_Rec2phi = Convert.ToDouble((string)(info.GetValue("Rec2phix", typeof(string))));
                Sousuo_region_get.Roi_y = Convert.ToDouble((string)(info.GetValue("Rec2midc", typeof(string))));
                Sousuo_region_get.Roi_x = Convert.ToDouble((string)(info.GetValue("Rec2midr", typeof(string))));

                //this.comboBox2.SelectedIndex = 1;
                //  mx.genRect2(rec2midr, rec2midc, rec2phi, rec2length1, rec2length2, ref sou_regions);
                //   mx.reset();
            }
            else if (Regionnull_check == "ROICircle")
            {

                Sousuo_region_get.Roi_model = "ROICircle";
                Sousuo_region_get.Roi_Cir1radius = Convert.ToDouble((string)(info.GetValue("Cir1radius", typeof(string))));
                Sousuo_region_get.Roi_y = Convert.ToDouble((string)(info.GetValue("Cir1midc", typeof(string))));
                Sousuo_region_get.Roi_x = Convert.ToDouble((string)(info.GetValue("Cir1midr", typeof(string))));
                //       mx.genCircle(cir1midr, cir1midc, cir1radius, ref sou_regions);
                //      mx.reset();
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

            ViewROI.ROIRectangle2 Rec21;
            ViewROI.ROICircle Cir11;
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("ShapeModel", this.comboBox1.SelectedIndex.ToString());
            info.AddValue("CbbPoint", this.cbb_point.SelectedIndex.ToString());
            info.AddValue("CbbAngle", this.cbb_angle.SelectedIndex.ToString());
            info.AddValue("Modelname", this.shape_name.Text.ToString());
            info.AddValue("TbNo", this.tb_no.Text.ToString());
            info.AddValue("Check_Roi", this.checkBox_get.Checked.ToString());
            info.AddValue("Check_handle", this.checkBox1.Checked.ToString());
            info.AddValue("ButtonPress", Button_press.ToString());
            info.AddValue("Rec2row1", this.textBox_rec2row1.Text.ToString());
            info.AddValue("Rec2col1", this.textBox_rec2col1.Text.ToString());
            info.AddValue("Rec2len1", this.textBox_rec2len1.Text.ToString());
            info.AddValue("Rec2len2", this.textBox_rec2len2.Text.ToString());
            info.AddValue("Rec2phi", this.txt_rec2phi.Text.ToString());
            info.AddValue("Cirrow", this.textBox_circlerow.Text.ToString());
            info.AddValue("Circol", this.textBox_circlecolumn.Text.ToString());
            info.AddValue("Cirradius", this.textBox_circleradius.Text.ToString());
            if (this.sou_regions != null)
            {
                if (this.sou_regions.Count != 0)
                {

                    if (sou_regions[0].GetType().ToString() == "ViewROI.ROIRectangle2")
                    {

                        info.AddValue("Region_Not_null", "ROIRectangle2");
                        Rec21 = sou_regions[0] as ViewROI.ROIRectangle2;
                        info.AddValue("Rec2length1", Rec21.length1.ToString());
                        info.AddValue("Rec2length2", Rec21.length2.ToString());
                        info.AddValue("Rec2phix", Rec21.phi.ToString());
                        info.AddValue("Rec2midc", Rec21.midC.ToString());
                        info.AddValue("Rec2midr", Rec21.midR.ToString());


                    }
                    if (sou_regions[0].GetType().ToString() == "ViewROI.ROICircle")
                    {
                        
                            info.AddValue("Region_Not_null", "ROICircle");
                            Cir11 = sou_regions[0] as ViewROI.ROICircle;
                            info.AddValue("Cir1midC", Cir11.midC.ToString());
                            info.AddValue("Cir1midR", Cir11.midR.ToString());
                            info.AddValue("Cir1Radius", Cir11.radius.ToString());

                        

                    }
                }
                    else
                        info.AddValue("Region_Not_null", "None");
                }
                else
                    info.AddValue("Region_Not_null", "NoneNone");

                string Inputimage_string = "";
                for (int m = 0; m < this.cbb_image.Items.Count; m++)
                {
                    if (m == this.cbb_image.Items.Count - 1)
                        Inputimage_string = string.Concat(Inputimage_string, cbb_image.Items[m].ToString());
                    else
                        Inputimage_string = string.Concat(Inputimage_string, cbb_image.Items[m].ToString() + ",");

                }

                info.AddValue("Image_string", Inputimage_string);

            

        }
        internal void WriteData(List<string> n_Path, int j)
        {
            ViewROI.ROIRectangle2 Rec2;
            ViewROI.ROICircle Cir1;
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            IniFile.IniWriteValue(j.ToString(), "ImageIn", this.cbb_image.SelectedItem.ToString());

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "ShapeModel", this.comboBox1.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "Modelname", this.shape_name.Text);
            IniFile.IniWriteValue(j.ToString(), "CbbPoint", this.cbb_point.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "CbbAngle", this.cbb_angle.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "TbNo", this.tb_no.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Check_Roi", this.checkBox_get.Checked.ToString());
            IniFile.IniWriteValue(j.ToString(), "Check_handle", this.checkBox1.Checked.ToString());

            IniFile.IniWriteValue(j.ToString(), "ButtonPress", Button_press.ToString());
             if (this.comboBox1.SelectedIndex == 1)
            {
                IniFile.IniWriteValue(j.ToString(), "Rec2row1", this.textBox_rec2row1.Text.ToString());
                IniFile.IniWriteValue(j.ToString(), "Rec2col1", this.textBox_rec2col1.Text.ToString());
                IniFile.IniWriteValue(j.ToString(), "Rec2len1", this.textBox_rec2len1.Text.ToString());
                IniFile.IniWriteValue(j.ToString(), "Rec2len2", this.textBox_rec2len2.Text.ToString());
                IniFile.IniWriteValue(j.ToString(), "Rec2phi", this.txt_rec2phi.Text.ToString());
            
            
            }
            else 
            {
                IniFile.IniWriteValue(j.ToString(), "Cirrow", this.textBox_circlerow.Text.ToString());
                IniFile.IniWriteValue(j.ToString(), "Circol", this.textBox_circlecolumn.Text.ToString());
                IniFile.IniWriteValue(j.ToString(), "Cirradius", this.textBox_circleradius.Text.ToString());
            
            }

             if (this.sou_regions != null)
             {
                 if (this.sou_regions.Count != 0)
                 {

                     if (sou_regions[0].GetType().ToString() == "ViewROI.ROIRectangle2")
                     {

                         IniFile.IniWriteValue(j.ToString(), "Region_Not_null", "ROIRectangle2");
                         Rec2 = sou_regions[0] as ViewROI.ROIRectangle2;
                         IniFile.IniWriteValue(j.ToString(), "Rec2length1", Rec2.length1.ToString());
                         IniFile.IniWriteValue(j.ToString(), "Rec2length2", Rec2.length2.ToString());
                         IniFile.IniWriteValue(j.ToString(), "Rec2phi", Rec2.phi.ToString());
                         IniFile.IniWriteValue(j.ToString(), "Rec2midc", Rec2.midC.ToString());
                         IniFile.IniWriteValue(j.ToString(), "Rec2midr", Rec2.midR.ToString());

                     }
                     if (sou_regions[0].GetType().ToString() == "ViewROI.ROICircle")
                     {

                         IniFile.IniWriteValue(j.ToString(), "Region_Not_null", "ROICircle");
                         Cir1 = sou_regions[0] as ViewROI.ROICircle;
                         IniFile.IniWriteValue(j.ToString(), "Cir1midc", Cir1.midC.ToString());
                         IniFile.IniWriteValue(j.ToString(), "Cir1midr", Cir1.midR.ToString());
                         IniFile.IniWriteValue(j.ToString(), "Cir1radius", Cir1.radius.ToString());

                     }

                 }
                 else
                     IniFile.IniWriteValue(j.ToString(), "Region_Not_null", "None");
             }


        }

        public string getshapename()
        {
            if (this.shape_name.Text != null)
                return this.shape_name.Text+".region";
            else
                return null;

        }




        internal void ReadData(List<string> n_Path, int j, ROIController mm)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            int Selectnumber = 0;
            if (IniFile.IniReadValue(j.ToString(), "ShapeModel") != "")
                Selectnumber = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "ShapeModel"));
            this.comboBox1.SelectedIndex = Selectnumber;
            if (IniFile.IniReadValue(j.ToString(), "CbbAngle") != "")
                this.cbb_angle.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "CbbAngle"));
            if (IniFile.IniReadValue(j.ToString(), "CbbPoint") != "")
                this.cbb_point.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "CbbPoint"));
            this.tb_no.Text = IniFile.IniReadValue(j.ToString(), "TbNo");
            if (IniFile.IniReadValue(j.ToString(), "Check_Roi") == "True")
                this.checkBox_get.Checked = true;
            else
                this.checkBox_get.Checked = false;
            if (IniFile.IniReadValue(j.ToString(), "Check_handle") == "True")
                this.checkBox1.Checked = true;
            else
                this.checkBox1.Checked = false; 
            Panel_show();
            this.cbb_image.Items.Add(IniFile.IniReadValue(j.ToString(), "ImageIn"));
            this.cbb_image.SelectedIndex = 0;

             if (Selectnumber == 1)
            {
                this.textBox_rec2row1.Text = IniFile.IniReadValue(j.ToString(), "Rec2row1");
                this.textBox_rec2col1.Text = IniFile.IniReadValue(j.ToString(), "Rec2col1");
                this.textBox_rec2len1.Text = IniFile.IniReadValue(j.ToString(), "Rec2len1");
                this.textBox_rec2len2.Text = IniFile.IniReadValue(j.ToString(), "Rec2len2");
                this.txt_rec2phi.Text = IniFile.IniReadValue(j.ToString(), "Rec2phi");
            }
            else
            {
                this.textBox_circlerow.Text = IniFile.IniReadValue(j.ToString(), "Cirrow");
                this.textBox_circlecolumn.Text = IniFile.IniReadValue(j.ToString(), "Circol");
                this.textBox_circleradius.Text = IniFile.IniReadValue(j.ToString(), "Cirradius");

            
            }

            this.shape_name.Text = IniFile.IniReadValue(j.ToString(), "Modelname");

            string Regionnull_check = IniFile.IniReadValue(j.ToString(), "Region_Not_null");

            if (Regionnull_check == "ROIRectangle2")
            {
                double rec2length1 = 0, rec2length2 = 0, rec2phi = 0, rec2midc = 0, rec2midr = 0;
                if (IniFile.IniReadValue(j.ToString(), "Rec2length1") != "")
                    rec2length1 = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec2length1"));
                if (IniFile.IniReadValue(j.ToString(), "Rec2length2") != "")
                    rec2length2 = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec2length2"));
                if (IniFile.IniReadValue(j.ToString(), "Rec2phi") != "")
                    rec2phi = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec2phi"));
                if (IniFile.IniReadValue(j.ToString(), "Rec2midc") != "")
                    rec2midc = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec2midc"));
                if (IniFile.IniReadValue(j.ToString(), "Rec2midr") != "")
                    rec2midr = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec2midr"));

                //this.comboBox2.SelectedIndex = 1;
                mm.genRect2(rec2midr, rec2midc, rec2phi, rec2length1, rec2length2, ref sou_regions);
                mm.reset();
            }
            else if (Regionnull_check == "ROICircle")
            {

                double cir1midc = 0, cir1midr = 0, cir1radius = 0;
                if (IniFile.IniReadValue(j.ToString(), "Cir1midc") != "")
                    cir1midc = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Cir1midc"));
                if (IniFile.IniReadValue(j.ToString(), "Cir1midr") != "")
                    cir1midr = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Cir1midr"));
                if (IniFile.IniReadValue(j.ToString(), "Cir1radius") != "")
                    cir1radius = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Cir1radius"));
                //  this.comboBox2.SelectedIndex = 2;
                mm.genCircle(cir1midr, cir1midc, cir1radius, ref sou_regions);
                mm.reset();
            }

            if (IniFile.IniReadValue(j.ToString(), "ButtonPress") == "True")
            {
                Button_press = true;
                this.button_搜索形状.BackColor = Color.Yellow;
            }
            else
                Button_press = false;

        }







        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           

             if (comboBox1.SelectedIndex == 0)
            {

                panel_rectangle2.Visible = false;
                panel_circle.Visible = true;
            }
            else
            {

                panel_rectangle2.Visible = true;
                panel_circle.Visible = false;
            }
            
        }


        public void Send_name()
        {
            UpdateRoiMake.OnSendUpdateRoiMake(new UpdateRoiMakeEventArgs(this.shape_name.Text+".region"));

        }
        private void button1_Click(object sender, EventArgs e)
        {

            UpdateRoiMake.OnSendUpdateRoiMake(new UpdateRoiMakeEventArgs(this.shape_name.Text+".region"));
             if(Check_pal())
                this.Visible = false;
        }

        private void RoiMake_Load(object sender, EventArgs e)
        {

        }

        private void button_搜索形状_Click(object sender, EventArgs e)
        {

            if (this.checkBox1.Checked)
            {
                String Shape1 = comboBox1.SelectedItem.ToString();
                Button_press = !Button_press;

                if (Button_press)
                {
                    this.button_搜索形状.BackColor = Color.Yellow;
                    SetROi = true;
                    UpdateRoiMake.OnSendUpdateRoiMake(new UpdateRoiMakeEventArgs(Shape1));
                }
                else
                    this.button_搜索形状.BackColor = Color.Empty;

            }
            else
                MessageBox.Show("请先选中手选区域");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateRoiMake.OnSendUpdateRoiMake(new UpdateRoiMakeEventArgs("MakeRegion"));
            this.button3.Enabled = false;

            if (Button_press)
                this.button_搜索形状.BackColor = Color.Yellow;
        }
    }
}
