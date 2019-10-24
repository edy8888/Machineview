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
using ViewROI;
using System.Runtime.Serialization; 
using System.IO;
namespace PMACam
{
    [Serializable]
    public partial class MakeCalibration : Form, ISerializable
    {
        List<double> x_point = new List<double>();
        List<double> y_point = new List<double>();


        HTuple temp_row = new HTuple();
        HTuple temp_column = new HTuple();
        HTuple temp_row_last = new HTuple();
        HTuple temp_column_last = new HTuple();
        bool  Get_Calibration = false;
        HTuple CalibDataID;
        HTuple  hv_StartCamPar = new HTuple();
        HTuple hv_CalTabDescrFile = "F:/caltab2.descr";
        int hv_NumImages = 15;
        HObject ho_MapSingle1;
        bool Get_Cal_pixel = false;
        double Pixel_number = 0;
        HTuple hv_CamParam = null;
        HTuple hv_Pose = new HTuple();
        double Pixel_Trans = 0;
        bool Read_file = false;





        public MakeCalibration()
        {
            InitializeComponent();



        }

        public MakeCalibration(ExecuteBuffer buffer, bool addbuffer)
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
        public int getlabel()
        {
            return Convert.ToInt32(this.textBox1.Text.ToString());
        }


        public MakeCalibration(SerializationInfo info, StreamingContext context) 
    {
        InitializeComponent();

      //  this.Input_image.Text = (string)(info.GetValue("Input_image", typeof(string)));
        this.Output_image.Text = (string)(info.GetValue("Output_image", typeof(string)));
        this.label_pixel.Text = (string)(info.GetValue("Pixel", typeof(string)));
        this.textBox_pixel.Text = (string)(info.GetValue("Cal_Pixel", typeof(string)));
        this.textBox_width.Text = (string)(info.GetValue("Cal_width", typeof(string)));
        this.textBox_height.Text = (string)(info.GetValue("Cal_height", typeof(string)));
        this.textbox_lens.Text = (string)(info.GetValue("Cal_lens", typeof(string)));
        this.textBox_file.Text = (string)(info.GetValue("Cal_file", typeof(string)));
        this.textBox2.Text = (string)(info.GetValue("Cal_File_Path", typeof(string)));
        Pixel_Trans = (double)(info.GetValue("Cal_Pixel_Trans", typeof(double)));
            if ((string)(info.GetValue("Cal_foculize", typeof(string))) == "True")
                this.checkBox_focalize.Checked = true;
            else
                this.checkBox_focalize.Checked = false;
            if ((string)(info.GetValue("Cal_Check", typeof(string))) == "True")
                this.checkBox1.Checked = true;
            else
                this.checkBox1.Checked = false;
            if ((string)(info.GetValue("Cal_Get_Cal", typeof(string))) == "True")
                Get_Calibration = true;
            else
                Get_Calibration = false;
            string out_image_string = (string)(info.GetValue("Image_string", typeof(string)));
            string[] condition = { "," };
            string[] result = out_image_string.Split(condition, StringSplitOptions.None);

            for (int m = 0; m < result.Count(); m++)
                Input_image.Items.Add(result[m]);
            Input_image.SelectedIndex = (Int32)(info.GetValue("Input_image", typeof(Int32)));
}


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tool_Name ", this.GetType().Name);
            info.AddValue("Input_image", this.Input_image.SelectedIndex.ToString());
            info.AddValue("Output_image", this.Output_image.Text.ToString());
            info.AddValue("Pixel", this.label_pixel.Text.ToString());
            info.AddValue("Cal_Pixel", this.textBox_pixel.Text.ToString());
            info.AddValue("Cal_Get_Cal", this.Get_Calibration.ToString());
            info.AddValue("Cal_width", this.textBox_width.Text.ToString());
            info.AddValue("Cal_height", this.textBox_height.Text.ToString());
            info.AddValue("Cal_lens", this.textbox_lens.Text.ToString());
            info.AddValue("Cal_file", this.textBox_file.Text.ToString());
            info.AddValue("Cal_File_Path", this.textBox2.Text.ToString());
            info.AddValue("Cal_Check", this.checkBox1.Checked.ToString());
            info.AddValue("Cal_foculize", this.checkBox_focalize.Checked.ToString());
            info.AddValue("Cal_Pixel_Trans", (double)Pixel_Trans);
            string Inputimage_string = "";
            for (int m = 0; m < this.Input_image.Items.Count; m++)
            {
                if (m == this.Input_image.Items.Count - 1)
                    Inputimage_string = string.Concat(Inputimage_string, Input_image.Items[m].ToString());
                else
                    Inputimage_string = string.Concat(Inputimage_string, Input_image.Items[m].ToString() + ",");

            }

            info.AddValue("Image_string", Inputimage_string);

        } 
        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);
            IniFile.IniWriteValue(j.ToString(), "Input_image", this.Input_image.Text);
            IniFile.IniWriteValue(j.ToString(), "Output_image", this.Output_image.Text);
            IniFile.IniWriteValue(j.ToString(), "Pixel", this.label_pixel.Text);
            IniFile.IniWriteValue(j.ToString(), "Cal_Pixel", this.textBox_pixel.Text);
            IniFile.IniWriteValue(j.ToString(), "Cal_Get_Cal", Get_Calibration.ToString());
            IniFile.IniWriteValue(j.ToString(), "Cal_width", this.textBox_width.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Cal_height", this.textBox_height.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Cal_lens", this.textbox_lens.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Cal_file", this.textBox_file.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Cal_Pixel_Trans", this.Pixel_Trans.ToString());
            IniFile.IniWriteValue(j.ToString(), "Cal_File_Path", this.textBox2.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Cal_Check", this.checkBox1.Checked.ToString());
            IniFile.IniWriteValue(j.ToString(), "Cal_foculize", this.checkBox_focalize.Checked.ToString());

        }


        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            this.Output_image.Text = IniFile.IniReadValue(j.ToString(), "Output_image");
            this.Input_image.Items.Clear();
            Input_image.Items.Add(IniFile.IniReadValue(j.ToString(), "Input_image"));
            this.Input_image.SelectedIndex = 0;
            this.label_pixel.Text = IniFile.IniReadValue(j.ToString(), "Pixel");

            this.textBox_pixel.Text = IniFile.IniReadValue(j.ToString(), "Cal_Pixel");
            this.textBox_width.Text = IniFile.IniReadValue(j.ToString(), "Cal_width");
            this.textBox_height.Text = IniFile.IniReadValue(j.ToString(), "Cal_height");
            this.textbox_lens.Text = IniFile.IniReadValue(j.ToString(), "Cal_lens");
            this.textBox_file.Text = IniFile.IniReadValue(j.ToString(), "Cal_file");
            this.textBox2.Text = IniFile.IniReadValue(j.ToString(), "Cal_File_Path");
            if (IniFile.IniReadValue(j.ToString(), "Cal_Get_Cal") == "True")
            if (IniFile.IniReadValue(j.ToString(), "Cal_foculize") == "True")
            {
                this.checkBox_focalize.Checked = true;
            }
            else
                this.checkBox_focalize.Checked = false;

            if (IniFile.IniReadValue(j.ToString(), "Cal_Check") == "True")
            {
                this.checkBox1.Checked = true;
                Get_Cal_pixel = true;
            }
            else
            {
                this.checkBox1.Checked = false; 
                Get_Cal_pixel = false;
            }
            string get_result_pixel = IniFile.IniReadValue(j.ToString(), "Cal_Pixel_Trans");
            string[] sArray = Regex.Split(get_result_pixel, ",", RegexOptions.IgnoreCase);
            
                if (Convert.ToDouble(get_result_pixel) != 0)
                {
                    Pixel_Trans = Convert.ToDouble(get_result_pixel);
                  //  HOperatorSet.ReadObject(out ho_MapSingle1, n_Path[0] + "\\Map_" + j);
                    Read_file = true;

                }
            
            
            




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




            }



        }
        internal void SetParaImage(ExecuteBuffer test)
        {
            int defaultnumber = 0;
            int get_number = 0;
            if (Input_image.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    if (keyc.Contains(".img"))
                    {
                        Input_image.Items.Add(keyc.Substring(0, keyc.Length - 4));
                        if (keyc.Contains("image"))
                        {
                            get_number = Convert.ToInt32(keyc.Substring(5, keyc.Length - 9));

                            if (get_number > defaultnumber)
                            {
                                Input_image.Text = keyc.Substring(0, keyc.Length - 4);
                                defaultnumber = get_number;
                            }
                        }
                    }


                }

            }
            else
            {

                int m = 0;

                if (Input_image.SelectedItem != null)
                {
                    if (!test.imageBuffer.ContainsKey(Input_image.SelectedItem.ToString() + ".img"))
                        Input_image.Items.Remove(Input_image.SelectedItem.ToString() + ".img");
                }
                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < Input_image.Items.Count; i++)
                    {
                        if (keyc == (Input_image.Items[i].ToString() + ".img"))
                            break;
                        m = i;
                        if (m == Input_image.Items.Count - 1 && keyc.Contains(".img"))
                            Input_image.Items.Add(keyc.Substring(0, keyc.Length - 4));

                    }
                }


            }



        }




        public string GetOutputimage()
        {            
            return this.Output_image.Text.ToString()+".img";
        
        }
        internal void SetParaRegion(ExecuteBuffer test)
        {


            if (this.Output_image.Items.Count == 0)
            {
                foreach (string keyc in test.imageBuffer.Keys)
                    if (keyc.Contains(".img"))
                        Output_image.Items.Add(keyc.Substring(0, keyc.Length - 4));

            }
            else
            {

                int m = 0;

                foreach (string keyc in test.imageBuffer.Keys)
                {
                    for (int i = 0; i < Output_image.Items.Count; i++)
                    {
                        if (keyc == (Output_image.Items[i].ToString() + ".img"))
                            break;
                        m = i;
                        if (m == Output_image.Items.Count - 1 && keyc.Contains(".img"))
                            Output_image.Items.Add(keyc.Substring(0, keyc.Length - 4));

                    }
                }

            }

        }



        public bool CalibrationMake(SourceBuffer _sourceBuffer, ExecuteBuffer _executeBuffer, out SourceBuffer outsourcebuffer, out ExecuteBuffer outexecutebuffer,HWndCtrl hWndCtrl,out string result_info)
        {
            outsourcebuffer = _sourceBuffer;
            outexecutebuffer = _executeBuffer;
             result_info = "";
             HTuple hv_PixelSize = 0.00001;
             HTuple  hv_Message = new HTuple(), hv_Row = new HTuple();
             HTuple hv_Column = new HTuple(), hv_Index = new HTuple();
             HTuple hv_Error = null;
             HObject ho_Contours = null, ho_Cross = null,hv_outimage = null;
             HTuple hv_HomMat3DIdentity = null;
             HTuple hv_cp1Hur1 = null, hv_cam2Hcp2 = null, hv_cam2Hul2 = null;
             HTuple hv_PoseNewOrigin2 = null, hv_ULX = null, hv_ULY = null, hv_PoseNewOrigin = null;
             double[] Get_actual_pixel = new double[7];
             HTuple WorldPixelx,WorldPixely;
             HTuple Heightx = new HTuple(), Widthx= new HTuple();
             Heightx.Append(Height / 2);
             Heightx.Append(Height / 2);
             Heightx.Append(Height / 2 + 1);
             Widthx.Append(Width / 2);
             Widthx.Append(Width / 2 + 1);
             Widthx.Append(Width / 2);
             HTuple WorldLength1, WorldLength2;
             HTuple ScaleForSimilarPixelSize = 0;
             HOperatorSet.GenEmptyObj(out ho_Contours);
             HOperatorSet.GenEmptyObj(out ho_Cross);
             HOperatorSet.GenEmptyObj(out hv_outimage);

            if (!_executeBuffer.imageBuffer.ContainsKey(this.Input_image.SelectedItem.ToString() + ".img"))
            {
                MessageBox.Show("二值化处理: 输入图像已经不存在，请重置设置输入图像");
                result_info = " 二值化处理: 输入图像已经不存在，请重置设置输入图像";
                return false;
            }
            if (_executeBuffer.imageBuffer[this.Input_image.SelectedItem.ToString() + ".img"] == null)
            {
                MessageBox.Show("二值化处理: image参数为空或者未赋值");
                result_info = " 二值化处理: 输入图像已经不存在，请重置设置输入图像";
                return false;
            }
            string comboBox_imageoutname = this.Output_image.Text.ToString() + ".img";
            if (_executeBuffer.imageBuffer[comboBox_imageoutname] != null)
            {
                if (_executeBuffer.imageBuffer[comboBox_imageoutname].IsInitialized())
                {
                    _executeBuffer.imageBuffer[comboBox_imageoutname].Dispose();
                }
            }
            HTuple width, height;
            int m = _executeBuffer.imageBuffer[this.Input_image.SelectedItem.ToString() + ".img"].CountObj();
            HOperatorSet.GetImageSize(_executeBuffer.imageBuffer[this.Input_image.SelectedItem.ToString() + ".img"], out width, out height);
            int number = Convert.ToInt32(this.textBox1.Text.ToString());

            if (!this.checkBox1.Checked)
            {
                if (Get_Cal_pixel)
                    Get_Cal_pixel = false;
                if (number == 0)
                {
                    hv_StartCamPar = new HTuple();
                    if (this.checkBox_focalize.Checked)
                        hv_StartCamPar.Append(0);
                    else
                        hv_StartCamPar.Append(Convert.ToDouble(this.textbox_lens.Text.ToString()) / 100);
                    hv_StartCamPar.Append(0);
                    hv_StartCamPar.Append(Convert.ToDouble(this.textBox_width.Text.ToString()) / 10000000);
                    hv_StartCamPar.Append(Convert.ToDouble(this.textBox_height.Text.ToString()) / 10000000);
                    hv_StartCamPar.Append(width / 2);
                    hv_StartCamPar.Append(height / 2);
                    hv_StartCamPar.Append(width);
                    hv_StartCamPar.Append(height);
                    hv_CalTabDescrFile = this.textBox_file.Text;
                    HOperatorSet.CreateCalibData("calibration_object", 1, 1, out CalibDataID);
                    if (this.checkBox_focalize.Checked)
                        HOperatorSet.SetCalibDataCamParam(CalibDataID, 0, "area_scan_telecentric_division", hv_StartCamPar);
                    else
                        HOperatorSet.SetCalibDataCamParam(CalibDataID, 0, "area_scan_division", hv_StartCamPar);
                    HOperatorSet.SetCalibDataCalibObject(CalibDataID, 0, hv_CalTabDescrFile);

                }
                try
                {
                    HOperatorSet.FindCalibObject(_executeBuffer.imageBuffer[this.Input_image.SelectedItem.ToString() + ".img"], CalibDataID, 0, 0, number, new HTuple(), new HTuple());
                    HOperatorSet.GetCalibData(CalibDataID, "camera", 0, "init_params", out hv_StartCamPar);
                    HOperatorSet.GetCalibDataObservPoints(CalibDataID, 0, 0, number, out hv_Row, out hv_Column, out hv_Index, out hv_Pose);
                    HOperatorSet.GetCalibDataObservContours(out ho_Contours, CalibDataID, "caltab", 0, 0, number);
                    HOperatorSet.GenCrossContourXld(out ho_Cross, hv_Row, hv_Column, 6, 0.785398);
                    hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, "orange");
                    hWndCtrl.addIconicVar(ho_Cross);
                    hWndCtrl.addIconicVar(ho_Contours);
                    hWndCtrl.repaint();
                }
                catch
                {
                    MessageBox.Show("找不到标定板");
                    ho_Contours.Dispose();
                    ho_Cross.Dispose();
                    
                    return false;
                }

                if (number == hv_NumImages-1)
                {
                    Read_file = false;
                    HOperatorSet.CalibrateCameras(CalibDataID, out hv_Error);
                    HOperatorSet.GetCalibData(CalibDataID, "camera", 0, "params", out hv_CamParam);
                    HOperatorSet.GetCalibData(CalibDataID, "calib_obj_pose", (new HTuple(0)).TupleConcat(6), "pose", out hv_Pose);
                    HOperatorSet.HomMat3dIdentity(out hv_HomMat3DIdentity);
                    HOperatorSet.HomMat3dRotateLocal(hv_HomMat3DIdentity, ((-(hv_Pose.TupleSelect(5)))).TupleRad(), "z", out hv_cp1Hur1);
                    HOperatorSet.PoseToHomMat3d(hv_Pose, out hv_cam2Hcp2);
                    HOperatorSet.HomMat3dCompose(hv_cam2Hcp2, hv_cp1Hur1, out hv_cam2Hul2);
                    HOperatorSet.HomMat3dToPose(hv_cam2Hul2, out hv_PoseNewOrigin2);
                    HOperatorSet.ImagePointsToWorldPlane(hv_CamParam, hv_PoseNewOrigin2, 0, 0, "m", out hv_ULX, out hv_ULY);
                    HOperatorSet.SetOriginPose(hv_PoseNewOrigin2, hv_ULX, hv_ULY, 0, out hv_PoseNewOrigin);

                    HOperatorSet.ImagePointsToWorldPlane(hv_CamParam, hv_Pose, Heightx, Widthx, 1, out WorldPixelx, out  WorldPixely);
                    HOperatorSet.DistancePp(WorldPixely[0], WorldPixelx[0], WorldPixely[1], WorldPixelx[1], out WorldLength1);
                    HOperatorSet.DistancePp(WorldPixely[0], WorldPixelx[0], WorldPixely[2], WorldPixelx[2], out WorldLength2);
                    ScaleForSimilarPixelSize = (WorldLength1 + WorldLength2) / 2;
                    HOperatorSet.GenImageToWorldPlaneMap(out ho_MapSingle1, hv_CamParam,
hv_PoseNewOrigin, width, height, width, height, ScaleForSimilarPixelSize, "bilinear");
                    HOperatorSet.WriteObject(ho_MapSingle1, this.textBox2.Text.ToString());
                    Read_file = true;
                    Get_Calibration = true;
                    this.checkBox1.Checked = true;
                    result_info = " 校正参数: 设置完成";
                }

                this.textBox1.Text = (number + 1).ToString();
            }

            if (this.checkBox1.Checked)
             {
                 if (Read_file)
                 {
                     if (File.Exists(this.textBox2.Text))
                     {

                         HOperatorSet.ReadObject(out ho_MapSingle1, this.textBox2.Text);
                     }
                     else
                     {
                         MessageBox.Show("校正文件不存在 ，请检查");
                         return false;
                     }

                 }

                 HOperatorSet.MapImage(_executeBuffer.imageBuffer[this.Input_image.SelectedItem.ToString() + ".img"], ho_MapSingle1, out hv_outimage);
                 _executeBuffer.imageBuffer[comboBox_imageoutname] = hv_outimage;
                 outexecutebuffer = _executeBuffer;
                 result_info = " 校正: 完成";
                 HTuple Distance;
                 if (!Get_Cal_pixel)
                 {
                     try
                     {
                         double all_number = 0;
                         HOperatorSet.FindCalibObject(hv_outimage, CalibDataID, 0, 0, 0, new HTuple(), new HTuple());
                         HOperatorSet.GetCalibDataObservPoints(CalibDataID, 0, 0, 0, out hv_Row, out hv_Column, out hv_Index, out hv_Pose);
                         for (int i = 0; i < 7; i++)
                         {

                             HOperatorSet.DistancePp(hv_Row[i * 7], hv_Column[i * 7], hv_Row[i * 7 + 6], hv_Column[i * 7 + 6], out Distance);
                             Get_actual_pixel[i] = Distance.D;
                             all_number = all_number + Get_actual_pixel[i];
                         }
                         Pixel_number = (Convert.ToDouble(this.textBox_pixel.Text)) / (all_number / 7);
                         Get_Cal_pixel = true;
                         this.label_pixel.Text = Pixel_number.ToString();
                         Pixel_Trans = Convert.ToDouble(Pixel_number.ToString());
                     }
                     catch
                     {
                         MessageBox.Show("请再次添加图像寻找计算像素比");
                     }
                 }
                 if (Read_file)
                     ho_MapSingle1.Dispose();

             }



            return true;
        }



        public string get_Imageout()
        {
            return this.Output_image.Text.ToString() + ".img";
        }

        public bool get_result()
        {
            return this.checkBox1.Checked;
        }
         public bool NinePointCalMake(SourceBuffer _sourceBuffer,ExecuteBuffer _executeBuffer,out SourceBuffer outsourcebuffer,out ExecuteBuffer outexecutebuffer)
        {
            outsourcebuffer = _sourceBuffer;
            outexecutebuffer = _executeBuffer;



            return true;



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "0";


        }

        private void MakeCalibration_Load(object sender, EventArgs e)
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
            if (this.Input_image.SelectedItem == null)
            {
                MessageBox.Show("输入图像为空,请输入");
                return false;
            }

            if (this.Input_image.SelectedItem.ToString() == "")
            {
                MessageBox.Show("输入图像为空,请输入");
                return false;
            }


            if (!IsNumber(this.textBox_pixel.Text.ToString()))
            {
                MessageBox.Show(" 输入参数【两圆距离】不是数字,请重新输入");
                return false;
            }
            if (Convert.ToDouble(this.textBox_pixel.Text.ToString()) < 0 || Convert.ToDouble(this.textBox_pixel.Text.ToString()) > 50)
            {
                MessageBox.Show(" 输入参数【两圆距离】数字需大于0,小于50");
                return false;
            }
            if (!IsNumber(this.textBox_width.Text.ToString()))
            {
                MessageBox.Show(" 输入参数【单像素宽】不是数字,请重新输入");
                return false;
            }
            if (Convert.ToDouble(this.textBox_width.Text.ToString()) <= 0 || Convert.ToDouble(this.textBox_width.Text.ToString()) > 10)
            {
                MessageBox.Show(" 输入参数【单像素宽】数字需大于0,小于10");
                return false;
            }
            if (!IsNumber(this.textBox_height.Text.ToString()))
            {
                MessageBox.Show(" 输入参数【单像素高】不是数字,请重新输入");
                return false;
            }
            if (Convert.ToDouble(this.textBox_height.Text.ToString()) <= 0 || Convert.ToDouble(this.textBox_height.Text.ToString()) > 10)
            {
                MessageBox.Show(" 输入参数【单像素高】数字需大于0,小于10");
                return false;
            }
            if (!IsNumber(this.textbox_lens.Text.ToString()))
            {
                MessageBox.Show(" 输入参数【镜头焦距】不是数字,请重新输入");
                return false;
            }
            if (Convert.ToDouble(this.textbox_lens.Text.ToString()) <= 0 || Convert.ToDouble(this.textbox_lens.Text.ToString()) > 100)
            {
                MessageBox.Show(" 输入参数【镜头焦距】数字需大于0,小于100");
                return false;
            }
            if (this.textBox_file.Text == "")
            {
                MessageBox.Show("输入标定文件为空,请输入");
                return false;
            }
            if (this.textBox2.Text == "")
            {
                MessageBox.Show("输出标定文件路径为空,请输入");
                return false;
            }
            return true;
        }
        private void btn_Sure_Click(object sender, EventArgs e)
        {
            UpdateCalibrationMake.OnSendUpdateCalibrationMake(new UpdateCalibrationMakeEventArgs(this.Output_image.Text.ToString() + ".img"));
            if (Check_pal())
                this.Visible = false;
        }

        private void button_文件位置_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "bmp files (*.descr)|*.descr";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strPathName = openFileDialog1.FileName;
                textBox_file.Text = strPathName;
            } 
        }
        private void save_imge()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "校正文件|*.hobj|所有文件|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (String.IsNullOrEmpty(sfd.FileName))
                {
                    return;
                }
                this.textBox2.Text = sfd.FileName;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            save_imge();
        }


    }
}
