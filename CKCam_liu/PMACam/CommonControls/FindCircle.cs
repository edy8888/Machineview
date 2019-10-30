using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.Runtime.Serialization; 
using System.Text.RegularExpressions;
using ViewROI;
namespace PMACam
{
    [Serializable] 
    public partial class FindCircle : Form, ISerializable
    {
        HTuple xrow= 0, xcol = 0, xradius = 0;

        public FindCircle(ExecuteBuffer buffer, bool addbuffer)
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
            this.cbb_measure_select.SelectedIndex = 0;
            this.cbb_transition.SelectedIndex = 0;
            this.cbb_source1.SelectedIndex = 0;

        }

        public FindCircle(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();
        this.cbb_measure_select.SelectedIndex = 0;
        this.cbb_transition.SelectedIndex = 0;

        this.cbb_source1.SelectedIndex = (Int32)(info.GetValue("Cbb_Source1", typeof(Int32)));
            this.comboBox1.SelectedIndex =(Int32)( info.GetValue("Circle_color", typeof(Int32)));
            this.cbb_measure_select.SelectedIndex = (Int32)(info.GetValue("Cbb_Measure_Select", typeof(Int32)));
            this.cbb_transition.SelectedIndex = (Int32)(info.GetValue("Cbb_Transition", typeof(Int32)));
            this.num_measure.Text = (string)(info.GetValue("Num_measure", typeof(string)));
            this.measure_length1.Text = (string)(info.GetValue("Measure_length1", typeof(string)));
            this.measure_length2.Text = (string)(info.GetValue("Measure_length2", typeof(string)));
            this.measure_threshold.Text = (string)(info.GetValue("Measure_threshold", typeof(string)));
            xrow = (double)(info.GetValue("Xrow", typeof(double)));
            xcol = (double)(info.GetValue("Xcol", typeof(double)));
            xradius = (double)(info.GetValue("Xradius", typeof(double)));

            string out_image_string = (string)(info.GetValue("Image_string", typeof(string)));
            string[] condition = { "," };
            string[] result = out_image_string.Split(condition, StringSplitOptions.None);
            this.tb_value1.Text = (string)(info.GetValue("Tb_Value1", typeof(string)));
            this.textBox_radius.Text = (string)(info.GetValue("TbRadius", typeof(string)));
            for (int m = 0; m < result.Count(); m++)
                Threshold_image.Items.Add(result[m]);
            Threshold_image.SelectedIndex = (Int32)(info.GetValue("Input_image", typeof(Int32)));
            if ((string)(info.GetValue("HandCircle", typeof(string))) == "True")
                this.checkBox_circle.Checked = true;
            else
                this.checkBox_circle.Checked = false;
} 
        public void Binary_ProImage(HImage Image, out HObject IImageReduced)
        {
            HOperatorSet.DualThreshold(Image, out IImageReduced, 17400, 200, 180);

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("Input_image", this.Threshold_image.SelectedIndex.ToString());
            info.AddValue("Circle_color", this.comboBox1.SelectedIndex.ToString());
            info.AddValue("Cbb_Measure_Select", this.cbb_measure_select.SelectedIndex.ToString());
            info.AddValue("Cbb_Transition", this.cbb_transition.SelectedIndex.ToString());
            info.AddValue("Num_measure", this.num_measure.Text.ToString());
            info.AddValue("Measure_length1", this.measure_length1.Text.ToString());
            info.AddValue("Measure_length2", this.measure_length2.Text.ToString());
            info.AddValue("Measure_threshold", this.measure_threshold.Text.ToString());
            info.AddValue("Xrow", (double)xrow);
            info.AddValue("Xcol", (double)xcol);
            info.AddValue("Xradius", (double)xradius);
            info.AddValue("HandCircle", this.checkBox_circle.Checked.ToString());
            string Inputimage_string = "";
            for (int m = 0; m < this.Threshold_image.Items.Count; m++)
            {
                if (m == this.Threshold_image.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Threshold_image.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Threshold_image.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string", Inputimage_string);
            info.AddValue("Tb_Value1", this.tb_value1.Text.ToString());
            info.AddValue("TbRadius", this.textBox_radius.Text.ToString());
            info.AddValue("Cbb_Source1", this.cbb_source1.SelectedIndex.ToString());
        } 


        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "Input_image", this.Threshold_image.Text);
            IniFile.IniWriteValue(j.ToString(), "Cbb_Source1", this.cbb_source1.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "Circle_color", this.comboBox1.SelectedIndex.ToString());
            if (xrow.D != 0)
            {
                IniFile.IniWriteValue(j.ToString(), "LineCheck", "true");
                IniFile.IniWriteValue(j.ToString(), "XRow", xrow.D.ToString());
            }
            else
                IniFile.IniWriteValue(j.ToString(), "LineCheck", "false");
            if (xcol.D != 0)
                IniFile.IniWriteValue(j.ToString(), "XCol", xcol.D.ToString());
            if (xradius.D != 0)
                IniFile.IniWriteValue(j.ToString(), "XRadius", xradius.D.ToString());

            IniFile.IniWriteValue(j.ToString(), "Cbb_Measure_Select", this.cbb_measure_select.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "Cbb_Transition", this.cbb_transition.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "Num_measure", num_measure.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Measure_length1", measure_length1.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Measure_length2", measure_length2.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Measure_threshold", measure_threshold.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "HandCircle", this.checkBox_circle.Checked.ToString());
            IniFile.IniWriteValue(j.ToString(), "Tb_Value1", this.tb_value1.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "TbRadius", this.textBox_radius.Text.ToString());
        }

        internal void ReadData(List<string> n_Path, int j)
        {
   
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            this.Threshold_image.Items.Clear();
            Threshold_image.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image"));
            Threshold_image.SelectedIndex = 0;
            string line_check = IniFile.IniReadValue(j.ToString(), "LineCheck");
            if (line_check == "true")
            {
                if (IniFile.IniReadValue(j.ToString(), "XRow") != "")
                    xrow = (HTuple)Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "XRow"));
                if (IniFile.IniReadValue(j.ToString(), "XCol") != "")
                    xcol = (HTuple)Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "XCol"));
                if (IniFile.IniReadValue(j.ToString(), "XRadius") != "")
                    xradius = (HTuple)Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "XRadius"));

            }
            if (IniFile.IniReadValue(j.ToString(), "Cbb_Source1") != "")
                this.cbb_source1.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "Cbb_Source1"));
            this.tb_value1.Text = IniFile.IniReadValue(j.ToString(), "Tb_Value1");
            this.textBox_radius.Text = IniFile.IniReadValue(j.ToString(), "TbRadius");
            if (IniFile.IniReadValue(j.ToString(), "Cbb_Measure_Select") != "")
                this.cbb_measure_select.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "Cbb_Measure_Select"));
            if (IniFile.IniReadValue(j.ToString(), "Cbb_Transition") != "")
                this.cbb_transition.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "Cbb_Transition"));
            if (IniFile.IniReadValue(j.ToString(), "Circle_color") != "")
                this.comboBox1.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "Circle_color"));

                this.num_measure.Text = IniFile.IniReadValue(j.ToString(), "Num_measure");
                this.measure_length1.Text = IniFile.IniReadValue(j.ToString(), "Measure_length1");
                this.measure_length2.Text = IniFile.IniReadValue(j.ToString(), "Measure_length2");
                this.measure_threshold.Text = IniFile.IniReadValue(j.ToString(), "Measure_threshold");
                if (IniFile.IniReadValue(j.ToString(), "HandCircle") == "True")
                    this.checkBox_circle.Checked = true;
                else
                    this.checkBox_circle.Checked = false;


        }
        internal void SetParaImage(ExecuteBuffer test)
        {
            int defaultnumber = 0;
            int get_number = 0;
            if (Threshold_image.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    if (keyc.Contains(".img"))
                    {
                        Threshold_image.Items.Add(keyc.Substring(0, keyc.Length - 4));
                        if (keyc.Contains("image"))
                        {
                            get_number = Convert.ToInt32(keyc.Substring(5, keyc.Length - 9));

                            if (get_number > defaultnumber)
                            {
                                Threshold_image.Text = keyc.Substring(0, keyc.Length - 4);
                                defaultnumber = get_number;
                            }
                        }
                    }

               //     if (keyc.Contains("image1"))
                    //    Threshold_image.Text = keyc;
                }

            }
            else
            {

                int m = 0;

                if (Threshold_image.SelectedItem != null)
                {
                    if (!test.imageBuffer.ContainsKey(Threshold_image.SelectedItem.ToString()+".img"))
                        Threshold_image.Items.Remove(Threshold_image.SelectedItem.ToString()+".img");
                }
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < Threshold_image.Items.Count; i++)
                    {
                        if (keyc == (Threshold_image.Items[i].ToString()+".img"))
                            break;
                        m = i;
                        if (m == Threshold_image.Items.Count - 1 && keyc.Contains(".img"))
                            Threshold_image.Items.Add(keyc.Substring(0,keyc.Length-4));

                    }
                }


            }




        }

        internal void SetParaRegion(ExecuteBuffer test)
        {




        }



        public string get_Imagename()
        {
            if (Threshold_image.SelectedItem != null)
                return this.Threshold_image.SelectedItem.ToString()+".img";
            else
                return null;

            
        }

        public void Binary_ProImage1(HImage Image, out HObject IImageReduced)
        {
            HObject xx;
            HOperatorSet.GenEmptyObj(out xx);
            HOperatorSet.GenEmptyObj(out IImageReduced);
            HOperatorSet.DualThreshold(Image, out xx, 1740, 200, 180);
            HOperatorSet.ReduceDomain(Image, xx, out IImageReduced);
            xx.Dispose();

            

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

            if (this.Threshold_image.Text == "")
            {
                MessageBox.Show("查找圆: 输入图像image in为空,请选择图像");
                return false;
            }

            if (!IsNumber(this.num_measure.Text.ToString()))
            {
                MessageBox.Show(" 输入参数卡尺数量不是数字,请重新输入");
                return false;
            }
            if (!IsNumber(this.measure_length1.Text.ToString()))
            {
                MessageBox.Show(" 输入参数卡尺长度不是数字,请重新输入");
                return false;
            }
            if (!IsNumber(this.measure_length2.Text.ToString()))
            {
                MessageBox.Show(" 输入参数卡尺长度不是数字,请重新输入");
                return false;
            }
            if (!IsNumber(this.measure_threshold.Text.ToString()))
            {
                MessageBox.Show(" 输入参数灰度阈值不是数字,请重新输入");
                return false;
            }
            return true;

        }











        public bool RunThresholdTest(HWndCtrl hWndCtrl,HObject imagein, out HObject hregion)
        {
            hregion = new HObject();
            return false;
        }



        public bool RunThresholdTest_new( ExecuteBuffer _executeBuffer, out ExecuteBuffer outexecutebuffer,out string result_info)
        {

            outexecutebuffer = _executeBuffer;
            result_info = "";
            HObject region;
            HObject outimagename;
            HOperatorSet.GenEmptyObj(out region);
            HOperatorSet.GenEmptyObj(out outimagename);

            result_info = " 二值化处理: 完成";
            return true;
        }


        public void Set_parameter(HTuple Row, HTuple Column, HTuple Radius)
        {
            xrow = Row;
            xcol = Column;
            xradius = Radius;

        }
        public bool Find_halcon_circle(ExecuteBuffer _executeBuffer,HWndCtrl hWndCtrl,模板GVName_halcon Model_result,Dictionary<int, PointName> Point_temp_result,out string result_info,out HTuple Row,out HTuple Column,out HTuple Radius,bool show_info)
        {
            HTuple MetrologyHandle;
            HTuple pic_wid,pic_height;
            Row = new HTuple();
            Column = new HTuple();
            Radius = new HTuple();
            result_info = "";
            HOperatorSet.CreateMetrologyModel(out MetrologyHandle);
                        if (!_executeBuffer.imageBuffer.ContainsKey(this.Threshold_image.SelectedItem.ToString()+".img"))
            {
                MessageBox.Show("查找圆: 输入图像已经不存在，请重置设置输入图像");
                result_info = " 查找圆: 输入图像已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Threshold_image.SelectedItem.ToString()+".img"] == null)
            {
                MessageBox.Show("查找圆: image参数为空或者未赋值");
                result_info = " 查找圆: 输入图像已经不存在，请重置设置输入图像";
                return false;
            }
            /*
            if (xrow == null || xcol == null || xradius == null)
            {
                MessageBox.Show("查找圆: 未确定圆位置");
                result_info = " 查找圆: 未确定圆位置";
                return false;
            }*/
            HObject imagein = _executeBuffer.imageBuffer[this.Threshold_image.SelectedItem.ToString()+".img"];
            HOperatorSet.GetImageSize(imagein,out pic_wid,out pic_height);
            HOperatorSet.SetMetrologyModelImageSize(MetrologyHandle,pic_wid,pic_height);
            HTuple Index;

            HTuple trow, tcol,tradius;
            if (this.checkBox_circle.Checked)
            {
                if (xrow == null || xcol == null || xradius == null)
                {
                    MessageBox.Show("查找直线:未确认线位置");
                    result_info = " 查找直线: 未确认线位置";
                    return false;
                }
                else if (xrow.D == 0 && xcol.D == 0 && xradius.D == 0)
                {
                    MessageBox.Show("查找直线:未确认线位置");
                    result_info = " 查找直线: 未确认线位置";
                    return false;
                }
                else
                {
                    trow = xrow;
                    tcol = xcol;
                    tradius = xradius;


                }
            }
            else
            {
                tradius = Convert.ToDouble(this.textBox_radius.Text);
                if (this.cbb_source1.SelectedIndex == 0)
                {
                    if (Model_result == null)
                    {
                        MessageBox.Show("查找圆:匹配列表为空，请设置");
                        result_info = " 查找圆: 匹配列表为空，请设置";
                        return false;
                    }
                    if (Model_result.点X.TupleLength() < 1)
                    {
                        MessageBox.Show("查找圆:匹配列表为空，请设置");
                        result_info = " 查找圆: 匹配列表为空，请设置";
                        return false;
                    }
                    trow = Model_result.点Y[0];
                    tcol = Model_result.点X[0];

                }
                else
                {
                    int number1 = Convert.ToInt32(this.tb_value1.Text);
                    if (!Point_temp_result.ContainsKey(number1))
                    {
                        MessageBox.Show("查找圆:全局列表点无此点，请设置");
                        result_info = " 查找圆: 全局列表无此点，请设置";
                        return false;
                    }
                    else
                    {
                        trow = Point_temp_result[number1].点Y;
                        tcol = Point_temp_result[number1].点X;

                    }
                }


            }













            HOperatorSet.AddMetrologyObjectCircleMeasure(MetrologyHandle, trow, tcol, tradius, Convert.ToInt32(this.measure_length1.Text.ToString()), Convert.ToInt32(this.measure_length2.Text.ToString()), 1, Convert.ToInt32(this.measure_threshold.Text.ToString()), new HTuple(), new HTuple(), out Index);
        //    HOperatorSet.SetMetrologyObjectParam(MetrologyHandle,"all","measure_transition","negative");
            HOperatorSet.SetMetrologyObjectParam(MetrologyHandle, "all", "measure_transition", this.cbb_transition.SelectedItem.ToString());
            HOperatorSet.SetMetrologyObjectParam(MetrologyHandle, "all", "measure_select", this.cbb_measure_select.SelectedItem.ToString());
            HOperatorSet.SetMetrologyObjectParam(MetrologyHandle,"all","num_measures",Convert.ToInt32(this.num_measure.Text.ToString()));
            HOperatorSet.SetMetrologyObjectParam(MetrologyHandle,"all","num_instances",40);
            HOperatorSet.SetMetrologyObjectParam(MetrologyHandle,"all","measure_sigma",1);
            HOperatorSet.SetMetrologyObjectParam(MetrologyHandle,"all","measure_interpolation","bicubic");
            //HOperatorSet.SetMetrologyObjectParam(MetrologyHandle,"all","measure_select","all");
            HOperatorSet.SetMetrologyObjectParam(MetrologyHandle,"all","min_score",0.4);
            HOperatorSet.ApplyMetrologyModel(imagein,MetrologyHandle);
            HObject Contours,Cross;

            HOperatorSet.GetMetrologyObjectMeasures(out Contours,MetrologyHandle,"all","all",out Row,out Column);
            HOperatorSet.GenCrossContourXld(out Cross,Row,Column,6,0.785398);
            HTuple hv_Parameter = null;
            //得到线的起点和终点坐标并显示出来
            HOperatorSet.GetMetrologyObjectResult(MetrologyHandle, "all", "all", "result_type",
                "all_param", out hv_Parameter);
            if (hv_Parameter.TupleLength() == 0)
            {
                result_info = " 查找圆: 在指定位置,找圆失败";
                MessageBox.Show("未找到圆");
                return false;

            }
            int Circle_number = hv_Parameter.TupleLength()/3;
            double[] Row_out = new double[Circle_number];
            double[] Column_out = new double[Circle_number];
            double[] Radius_out = new double[Circle_number];
            for (int i = 0; i < Circle_number; i++)
            {
                Row_out[i] = hv_Parameter[i*3+1].D;
                Column_out[i] = hv_Parameter[i * 3].D;
                Radius_out[i] = hv_Parameter[i * 3 + 2].D;
            }
            Row = (HTuple)Row_out;
            Column = (HTuple)Column_out;
            Radius = (HTuple)Radius_out;

            HObject ho_Contour;
            HOperatorSet.GenEmptyObj(out ho_Contour);
            ho_Contour.Dispose();
            HOperatorSet.GetMetrologyObjectResultContour(out ho_Contour, MetrologyHandle,
                "all", "all", 1.5);


            HOperatorSet.ClearMetrologyModel(MetrologyHandle);
            if (show_info)
            {
                hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, this.comboBox1.SelectedItem.ToString());
                hWndCtrl.addIconicVar(Cross);
                hWndCtrl.addIconicVar(Contours);
            }
            hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, this.comboBox1.SelectedItem.ToString());
            hWndCtrl.addIconicVar(ho_Contour);
            hWndCtrl.repaint();
            return true;


        
        }





        public bool Send_name()
        {
            if (Threshold_image.Items.Count == 0)
                return false;
            if (Threshold_image.SelectedItem == null)
                return false;
          //  UpdateThresholdTest.OnSendUpdateThresholdTest(new UpdateThresholdTestEventArgs(Threshold_image.SelectedItem.ToString()+".img", thresold_region.Text.ToString()+".region", comboBox_imageout.Text.ToString()+".img", trackBar_min.Value, trackBar_max.Value, false));
            return true;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.checkBox_circle.Checked)
                UpdateFindCircle.OnSendUpdateFindCircle(new UpdateFindCircleEventArgs("circle"));
            else
                MessageBox.Show("请先选择手动画圆");



        }

        private void btn_Sure_Click(object sender, EventArgs e)
        {

            if (Check_pal())
            {
                this.Visible = false;
            }
        }




    }




    











}
