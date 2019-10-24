using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basler.Pylon;
using System.IO;
using System.Threading;
using HalconDotNet;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using ViewROI;



namespace PMACam
{
    public partial class PMAControl : UserControl
    {

        /// <summary>
        /// 鼠标中键事件
        /// </summary>
        /// <param name="imgx"></param>
        /// <param name="imgy"></param>
        /// 
        private static string rootPath = System.Windows.Forms.Application.StartupPath;
        public delegate void MouseMidleClick(double imgx, double imgy);

        public static string CamModelName;
        public static bool CurCam_IniStatus;
        public static string theFirstCamIp = "";
        public static string theSecondCamIp = "";
        public static uint theFirstCamIndex = 0;
        public static uint theSecondCamIndex = 0;

        public double Cam_RoiLeft = 0;
        public double Cam_RoiTop = 0;
        public double Cam_RoiRight = 0;
        public double Cam_RoiBottom = 0;
        public double Cam_AoiLeft = 0;
        public double Cam_AoiTop = 0;
        public double Cam_AoiRight = 0;
        public double Cam_AoiBottom = 0;
        private bool Line_find = false;
        private bool Circle_find = false;
        private bool Ensure_step = true;
        public string Result_Type = "";
        public double ResultAngle = 0, ResultPosX = 0, ResultPosY = 0, ResultScore = 0;
        public int X = 0, Y = 0;
        public bool Img_show = false;
        public PMAControl instance = null;
        private static readonly object padlock = new object();
        double[] Y_list = new double[20];

        public Form ListTool_backup = new Form();
        public bool ListTool_exist = false;

        int comboBox1_SelectedIndex = 0;
        public bool Panel_zhedie = false;


        private bool Realtime_display = false;
        List<DataGVName> ListDataGV = new List<DataGVName>();
        List<点GVName> Pointlist = new List<点GVName>();
        List<直线GVName> Linelist = new List<直线GVName>();
        List<圆GVName> Circlelist = new List<圆GVName>();
        List<模板GVName> Modellist = new List<模板GVName>();
        List<斑点GVName> Bloblist = new List<斑点GVName>();
        List<角度GVName> Anglelist = new List<角度GVName>();
        List<长度GVName> Lengthlist = new List<长度GVName>();
        List<字符串GVName> Charlist = new List<字符串GVName>();
        模板GVName_halcon Modellist_halcon = new 模板GVName_halcon();
        斑点GVName_halcon Bloblist_halcon = new 斑点GVName_halcon();
        字符串GVName_halcon String_halcon = new 字符串GVName_halcon();
        
        点GVName_halcon Pointlist_halcon = new 点GVName_halcon();
        圆GVName_halcon Circlelist_halcon = new 圆GVName_halcon();
        直线GVName_halcon Linelist_halcon = new 直线GVName_halcon();
        List<string> File_solution = new List<string>();
        private List<ViewROI.ROI> regions1;
        private List<ViewROI.ROI> sou_regions1;
        private List<ViewROI.ROI> regions2;
        private List<ViewROI.ROI> sou_regions2;
        private List<ViewROI.ROI> sou_regions_interest;
        private List<List<Form>> ListTool = new List<List<Form>>();
        private List<SourceBuffer> _sourceBuffer;
        private List<ExecuteBuffer> _executeBuffer;
        List<string> N_Path = new List<string>();
        private List<All_buffer> all_test_buffer_save = new List<All_buffer>();
        private Dictionary<int, PointName> Tem_Pointlist = new Dictionary<int, PointName>();
        public 圆GVName_halcon Tem_Circlelist = new 圆GVName_halcon();
        public 圆弧GVName_halcon Tem_CircleArclist = new 圆弧GVName_halcon();
        public 直线GVName_halcon Tem_Linelist = new 直线GVName_halcon();
        HObject hv_image;
        HTuple ModelID = new HTuple(); //模板
        bool return_region_shape_type = false;
        bool return_region_grey_type = false;
        public event EventHandler accept;
        public bool Camerashow_setting = false;
        public bool Resultshow_setting = false;
        
        public PMAControl Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PMAControl();
                    }
                    return instance;
                }
            }
        }

        private int ErrorCode;
        AutoSizeFormClass asc = new AutoSizeFormClass();

        private void MakeDir()
        {
            CommonData.Path = rootPath + _DirKeep.ToString();

            CommonData.Real_pixel = new List<string>();

            for (int number = 0; number < File_solution.Count; number++)
                CommonData.Real_pixel.Add(File_solution[number].ToString());

            檔案路徑創建資料夾();
        }
        
        public PMAControl()
        {

            InitializeComponent();


            Buffer_initial();
            File_solution.Add("模板");
            List初始化(File_solution);
            資料表初始化(); //ui
            Initial_Datagridview_Result();
            //檔案路徑創建資料夾();
            this.button_相机连接.Enabled = true;
            this.button_断开.Enabled = false;
            label_location.BackColor = System.Drawing.Color.Transparent;
            ShowToolRegion.SendShowToolRegionArgs += ShowToolRegion_SendShowToolRegionArgs;
            ShowToolGreyRegion.SendShowToolGreyRegionArgs += ShowToolGreyRegion_SendShowToolRegionArgs;
            ShowToolResult.SendShowToolResultArgs += ShowToolResult_SendShowToolResultArgs;
            UpdateBufferReadImage.SenUpdateReadImageBufferArgs += UpdateBuffer_ReadImageArgs;
            UpdateSourceBuffer.SenUpdateSourceBufferArgs += UpdateSource_BufferArgs;
            UpdateThresholdTest.SenUpdateThresholdTestArgs += UpdateThreshold_testArgs;
            UpdateRoiMake.SenUpdateRoiMakeArgs += UpdateRoi_makeArgs;
            UpdateMorTest.SenUpdateMorTestArgs += UpdateMor_testArgs;
            UpdateSmoothTest.SenUpdateSmoothTestArgs += UpdateSmooth_testArgs;
            UpdateRegionProcess.SenUpdateRegionProcessArgs += UpdateRegionProcess_testArgs;
            UpdateRegionInterest.SenUpdateRegionInterestArgs += UpdateRegionInterest_testArgs;
            UpdateFindLine.SenUpdateFindLineArgs += UpdateFindLine_testArgs;
            UpdateFindCircle.SenUpdateFindCircleArgs += UpdateFindCircle_testArgs;
            UpdateCalibrationMake.SenUpdateCalibrationMakeArgs += UpdateCalibration_testArgs;
            UpdatePuzzlePicture.SenUpdatePuzzlePictureArgs += UpdatePuzzle_testArgs;
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            hWindowControl1.MouseUp += Hwindow_MouseUp;
            this.dataGridView2.DataError += delegate(object sender, DataGridViewDataErrorEventArgs e) {
                //MessageBox.Show("error");
            };


        }



        private void Hwindow_MouseUp(object sender, MouseEventArgs e)
        {
            int index;
            int s = 0;
            if (this.dataGridView1.RowCount == 0)
                return;

            s = this.dataGridView1.CurrentRow.Index; //取得選取位置
            if (ListTool[comboBox1_SelectedIndex][s].GetType().Name == "SaveShapeTem")
            {

                SaveShapeTem Saveshapetem_control = (SaveShapeTem)ListTool[comboBox1_SelectedIndex][s];
                if (!Saveshapetem_control.GetROI())
                    return;
                List<double> data;
                ViewROI.ROI roi = roiController.smallestActiveROI(out data, out index);

                if (index > -1)
                {
                    string name = roi.GetType().Name;
                    if (return_region_shape_type)
                    {
                        regions2 = Saveshapetem_control.regions;
                        Saveshapetem_control.regions[index] = roi;
                        regions2[index] = Saveshapetem_control.regions[index];

                    }
                    else
                    {
                        sou_regions2 = Saveshapetem_control.sou_regions;
                        Saveshapetem_control.sou_regions[index] = roi;
                        sou_regions2[index] = Saveshapetem_control.sou_regions[index];


                    }
                }

            }
            if (ListTool[comboBox1_SelectedIndex][s].GetType().Name == "SaveGreyTem")
            {
                SaveGreyTem Savegreytem_control = (SaveGreyTem)ListTool[comboBox1_SelectedIndex][s];
                List<double> data;
                if (!Savegreytem_control.getROI())
                    return;
                ViewROI.ROI roi = roiController.smallestActiveROI(out data, out index);

                if (index > -1)
                {
                    string name = roi.GetType().Name;
                    if (return_region_grey_type)
                    {
                        Savegreytem_control.regions[index] = roi;
                        regions1[index] = roi;
                    }
                    else
                    {
                        Savegreytem_control.sou_regions[index] = roi;
                        sou_regions1[index] = roi;
                    }
                }

            }
            if (ListTool[comboBox1_SelectedIndex][s].GetType().Name == "RoiMake")
            {
                RoiMake Roimake1 = (RoiMake)ListTool[comboBox1_SelectedIndex][s];
                List<double> data;
                ViewROI.ROI roi = roiController.smallestActiveROI(out data, out index);

                if (index > -1)
                {
                    string name = roi.GetType().Name;
                    Roimake1.sou_regions[index] = roi;
                        sou_regions_interest[index] = roi;
                    
                    
                }

            }
            else
                return;



        }
        private void UpdateFindLine_testArgs(UpdateFindLineEventArgs e)
        {
            HTuple row1, column1, row2, column2,Row1,Column1,Row2,Column2;
            HOperatorSet.SetColor(hWindowControl1.HalconWindow, "red");
            this.hWindowControl1.Focus();
            Line_find = true;
            HOperatorSet.DrawLine(hWindowControl1.HalconWindow, out row1, out column1, out row2, out column2);
            Line_find = false;
            int m = this.dataGridView1.CurrentRow.Index;
            int Thread_number = comboBox1_SelectedIndex;
            string result_info  = "";
            if (ListTool[comboBox1_SelectedIndex][m].GetType().Name != "FindLine")
            {
                MessageBox.Show("请在左边选中FindLine");  
                return; 
            }
            FindLine F_findline = (FindLine)ListTool[comboBox1_SelectedIndex][m];
            int count = Linelist.Count;
            直线GVName newline;
            F_findline.Set_parameter(row1, column1, row2, column2);

            if (F_findline.Find_halcon_line(_executeBuffer[Thread_number], hWndCtrl,Modellist_halcon,Tem_Pointlist, out  result_info, out Row1, out Column1,out Row2,out Column2,true))
            {
                for (int i = 0; i < Row1.TupleLength(); i++)
                {
                    newline = new 直线GVName((count+i).ToString(), Row1[i].D.ToString(), Column1[i].D.ToString(), Row2[i].D.ToString(), Column2[i].D.ToString());
                    Linelist.Add(newline);
                }
                show_result("show_直线");
            }

          //  String Image_name = F_binarypro.get_Imagename();


        }

        private void UpdateFindCircle_testArgs(UpdateFindCircleEventArgs e)
        {
            HTuple rownew, columnnew, radiusnew, Row, Column,Radius;
            HOperatorSet.SetColor(hWindowControl1.HalconWindow, "red");
            this.hWindowControl1.Focus();
            Circle_find = true;

            HOperatorSet.DrawCircle(hWindowControl1.HalconWindow, out rownew, out columnnew, out radiusnew);
            Circle_find = false;
            int m = this.dataGridView1.CurrentRow.Index;
            int Thread_number = comboBox1_SelectedIndex;
            string result_info = "";
            圆GVName newcircle;
            if (ListTool[comboBox1_SelectedIndex][m].GetType().Name != "FindCircle")
            {
                MessageBox.Show("请在左边选中FindCircle");
                return;
            }
            FindCircle F_findcircle = (FindCircle)ListTool[comboBox1_SelectedIndex][m];
            int count = Circlelist.Count;
            F_findcircle.Set_parameter(rownew, columnnew, radiusnew);

            if (F_findcircle.Find_halcon_circle(_executeBuffer[Thread_number], hWndCtrl,Modellist_halcon,Tem_Pointlist, out  result_info, out Row, out Column,out Radius,true))
            {
                Circlelist_halcon.圆心X = Row;
                Circlelist_halcon.圆心Y = Column;
                Circlelist_halcon.半径R = Radius;

                for (int i = 0; i < Row.TupleLength(); i++)
                {
                    newcircle = new 圆GVName((count+i).ToString(), Row[i].D.ToString(), Column[i].D.ToString(), Radius[i].D.ToString());
                    Circlelist.Add(newcircle);
                }
                show_result("show_圆");
            }
            //  String Image_name = F_binarypro.get_Imagename();


        }



        private void UpdateRoi_makeArgs(UpdateRoiMakeEventArgs e)
        {

            int number = comboBox1_SelectedIndex;

            int s = 0;
            s = this.dataGridView1.CurrentRow.Index; //取得選取位置
            int mx = this.comboBox1.SelectedIndex;


            if (ListTool[comboBox1_SelectedIndex][s].GetType().Name != "RoiMake")
                return;
            RoiMake Roimake12 = (RoiMake)ListTool[comboBox1_SelectedIndex][s];


            hWindowControl1.HalconWindow.ClearWindow();
            this.roiController.reset();
            Display_Graphic(_executeBuffer[mx].imageBuffer[Roimake12.Imagename()]);
            HTuple width_image, height_image;
            HOperatorSet.GetImageSize(hv_image, out width_image, out height_image);
            if (_executeBuffer[mx].imageBuffer[Roimake12.Imagename()] == null)
                return;


            if (!_executeBuffer[mx].imageBuffer.ContainsKey(Roimake12.Imagename()))
            {
                MessageBox.Show("请确认输入图像参数是否正确");
                return;
            }
            if (_executeBuffer[mx].imageBuffer[Roimake12.Imagename()] == null)
            {
                MessageBox.Show("输入图像为空");
                return;
            }

            if (e.shape_name == "Rectangle2")
            {
                //   Display_Graphic(_executeBuffer[mx].imageBuffer[Saveshapetem_control.Imagename()]);

                return_region_shape_type = false;
                if (Roimake12.sou_regions != null)
                {
                    if (Roimake12.sou_regions[0].GetType().ToString() == "ViewROI.ROIRectangle2")
                        roiController.displayROI(Roimake12.sou_regions);

                    else
                    {
                        roiController.removeROI(ref sou_regions_interest);
                        Roimake12.sou_regions = sou_regions_interest;
                    }

                }
                if (Roimake12.sou_regions == null || Roimake12.sou_regions.Count == 0)
                {
                    if (sou_regions_interest != null)
                    {
                        if (sou_regions_interest.Count != 0)
                            roiController.removeROI(ref sou_regions_interest);
                    }
                    roiController.genRect2(height_image / 2, width_image / 2, 0.0, 800.0, 400.0, ref sou_regions_interest);
                    Roimake12.sou_regions = sou_regions_interest;
                }


            }
            else if (e.shape_name == "Circle")
            {

                Display_Graphic(_executeBuffer[mx].imageBuffer[Roimake12.Imagename()]);
                return_region_shape_type = false;
                if (Roimake12.sou_regions != null)
                {
                    if (Roimake12.sou_regions[0].GetType().ToString() == "ViewROI.ROICircle")
                        roiController.displayROI(Roimake12.sou_regions);

                    else
                    {
                        roiController.removeROI(ref sou_regions_interest);
                        Roimake12.sou_regions = sou_regions_interest;
                    }

                }
                if (Roimake12.sou_regions == null || Roimake12.sou_regions.Count == 0)
                {

                    if (sou_regions_interest != null)
                    {
                        if (sou_regions_interest.Count != 0)
                            roiController.removeROI(ref sou_regions_interest);
                    }
                    roiController.genCircle(height_image / 2, width_image / 2, 200.0, ref sou_regions_interest);
                    Roimake12.sou_regions = sou_regions_interest;
                }


            }
            else if (e.shape_name == "MakeRegion")
            {

                Display_Graphic(_executeBuffer[mx].imageBuffer[Roimake12.Imagename()]);
                Roimake12.MakeRoi_model(roiController);
            }
            else
            {
                if (_executeBuffer[number].imageBuffer.ContainsKey(e.shape_name))
                {
                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                }
                else
                {
                    _executeBuffer[number].imageBuffer.Add(e.shape_name, null);

                }
                int m = this.dataGridView1.CurrentRow.Index;

                // _executeBuffer[number].all_test_buffer[m].image_buffer[0] = e.Name;
                _executeBuffer[number].all_test_buffer[m].imagebuffer[0] = e.shape_name;
            }




        }



        private void UpdateRegionInterest_testArgs(UpdateRegionInterestEventArgs e)
        {
            int number = comboBox1_SelectedIndex;
                if (_executeBuffer[number].imageBuffer.ContainsKey(e.output_region))
                {
                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                }
                else
                {
                    _executeBuffer[number].imageBuffer.Add(e.output_region, null);

                }
                int m = this.dataGridView1.CurrentRow.Index;

                // _executeBuffer[number].all_test_buffer[m].image_buffer[0] = e.Name;
                _executeBuffer[number].all_test_buffer[m].imagebuffer[0] = e.output_region;
            





        }
        private void UpdateCalibration_testArgs(UpdateCalibrationMakeEventArgs e)
        {
            int number = comboBox1_SelectedIndex;
            if (_executeBuffer[number].imageBuffer.ContainsKey(e.output_image))
            {
                // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
            }
            else
            {
                _executeBuffer[number].imageBuffer.Add(e.output_image, null);

            }
            int m = this.dataGridView1.CurrentRow.Index;

            // _executeBuffer[number].all_test_buffer[m].image_buffer[0] = e.Name;
            _executeBuffer[number].all_test_buffer[m].imagebuffer[0] = e.output_image;


        }

        private void UpdatePuzzle_testArgs(UpdatePuzzlePictureEventArgs e)
        {
            int number = comboBox1_SelectedIndex;
            if (_executeBuffer[number].imageBuffer.ContainsKey(e.output_image))
            {
                // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
            }
            else
            {
                _executeBuffer[number].imageBuffer.Add(e.output_image, null);

            }
            int m = this.dataGridView1.CurrentRow.Index;

            // _executeBuffer[number].all_test_buffer[m].image_buffer[0] = e.Name;
            _executeBuffer[number].all_test_buffer[m].imagebuffer[0] = e.output_image;


        }
        private void UpdateRegionProcess_testArgs(UpdateRegionProcessEventArgs e)
        {
            int number = comboBox1_SelectedIndex;
            if (_executeBuffer[number].imageBuffer.ContainsKey(e.output_region))
            {
                // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
            }
            else
            {
                _executeBuffer[number].imageBuffer.Add(e.output_region, null);

            }
            int m = this.dataGridView1.CurrentRow.Index;

            // _executeBuffer[number].all_test_buffer[m].image_buffer[0] = e.Name;
            _executeBuffer[number].all_test_buffer[m].imagebuffer[0] = e.output_region;


        }
        private void UpdateMor_testArgs(UpdateMorTestEventArgs e)
        {
            int number = comboBox1_SelectedIndex;
            if (_executeBuffer[number].imageBuffer.ContainsKey(e.output_region))
            {
                // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
            }
            else
            {
                _executeBuffer[number].imageBuffer.Add(e.output_region, null);

            }
            int m = this.dataGridView1.CurrentRow.Index;

            // _executeBuffer[number].all_test_buffer[m].image_buffer[0] = e.Name;
            _executeBuffer[number].all_test_buffer[m].imagebuffer[0] = e.output_region;


        }

        private void UpdateSmooth_testArgs(UpdateSmoothTestEventArgs e)
        {
            int number = comboBox1_SelectedIndex;
            
            if (_executeBuffer[number].imageBuffer.ContainsKey(e.output_region))
            {
                // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
            }
            else
            {
                _executeBuffer[number].imageBuffer.Add(e.output_region, null);

            }
            int m = this.dataGridView1.CurrentRow.Index;

            // _executeBuffer[number].all_test_buffer[m].image_buffer[0] = e.Name;
            _executeBuffer[number].all_test_buffer[m].imagebuffer[0] = e.output_region;


        }

        private void UpdateSource_BufferArgs(UpdateSourceBufferEventArgs e)
        {
            //MessageBox.Show("now list is update");
            if (e.sourceBuffer_update != null)
                _sourceBuffer = e.Source_info;
            if (e.executeBuffer_update != null)
                _executeBuffer = e.Execute_info;
            int m = this.comboBox1_SelectedIndex;
            CleearBufferRedundance(m);

        }

        private void UpdateThreshold_testArgs(UpdateThresholdTestEventArgs e)
        {
            int m = this.dataGridView1.CurrentRow.Index;
            int Thread_number = comboBox1_SelectedIndex;
            if (ListTool[Thread_number][m].GetType().Name != "BinaryPro")
            {
                MessageBox.Show("请在左边选中BinaryPro项");
                return;
            }
            string out_info = "";
            if (!e.dyn_show)
            {

                int number = comboBox1_SelectedIndex;
                string oldregion = _executeBuffer[number].all_test_buffer[m].imagebuffer[0];
                string oldimagename = _executeBuffer[number].all_test_buffer[m].imagebuffer[1];
                if (!_executeBuffer[number].imageBuffer.ContainsKey(e.output_region))
                {
                    _executeBuffer[number].imageBuffer.Add(e.output_region, null);

                }



                _executeBuffer[number].all_test_buffer[m].imagebuffer[0] = e.output_region;


                if (!_executeBuffer[number].imageBuffer.ContainsKey(e.output_image))
                {
                    _executeBuffer[number].imageBuffer.Add(e.output_image, null);

                }
                _executeBuffer[number].all_test_buffer[m].imagebuffer[1] = e.output_image;
                bool region_remove = true;
                foreach (int keyc in _executeBuffer[number].all_test_buffer.Keys)
                {
                    for (int mnumber = 0; mnumber < _executeBuffer[number].all_test_buffer[keyc].imagebuffer.Count; mnumber++)
                    {
                        if (oldregion == _executeBuffer[number].all_test_buffer[keyc].imagebuffer[mnumber])
                        {
                            region_remove = false;
                            break;
                        }

                    }
                }
                if (region_remove)
                    _executeBuffer[number].imageBuffer.Remove(oldregion);

                bool image_remove = true;
                foreach (int keyc in _executeBuffer[number].all_test_buffer.Keys)
                {
                    for (int mnumber = 0; mnumber < _executeBuffer[number].all_test_buffer[keyc].imagebuffer.Count; mnumber++)
                    {
                        if (oldimagename == _executeBuffer[number].all_test_buffer[keyc].imagebuffer[mnumber])
                        {
                            image_remove = false;
                            break;
                        }

                    }
                }
                if (image_remove)
                    _executeBuffer[number].imageBuffer.Remove(oldimagename);



            }
            else
            {


                BinaryPro F_binarypro = (BinaryPro)ListTool[comboBox1_SelectedIndex][m];
                String Image_name = F_binarypro.get_Imagename();
                ExecuteBuffer executeBuffer_updatebinary= new ExecuteBuffer();
                Display_Graphic(_executeBuffer[Thread_number].imageBuffer[F_binarypro.get_Imagename()]);
                hWindowControl1.HalconWindow.ClearWindow();
                斑点GVName_halcon Blob_result;
                F_binarypro.RunThresholdTest_new(_executeBuffer[Thread_number], out executeBuffer_updatebinary, out Blob_result, out out_info);
                for (int imx = 0; imx < Blob_result.域面积.TupleLength(); imx++)
                {
                    斑点GVName newblob = new 斑点GVName((imx).ToString(), Blob_result.域中心X[imx].D.ToString("F3"), Blob_result.域中心Y[imx].D.ToString("F3"), Blob_result.域面积[imx].D.ToString("F3"), Blob_result.域角度[imx].D.ToString("F3"));
                    Bloblist.Add(newblob);
                }
                show_result("show_圆");
                Display_Graphic(_executeBuffer[Thread_number].imageBuffer[F_binarypro.get_Region()]);
            
            }


        }




        private HWndCtrl hWndCtrl;
        private ROIController roiController;

        private void ShowToolResult_SendShowToolResultArgs(ShowToolResultEventArgs e)
        {
        }


        private void UpdateBuffer_ReadImageArgs(UpdateBufferReadImageEventArgs e)
        {
            int m = this.dataGridView1.CurrentRow.Index;
            int number = comboBox1_SelectedIndex;





            if (ListTool[number][m].GetType().Name != "ReadPictureControl")
            {
                MessageBox.Show("请在左边选中ReadPictureControl项");
                return;
            }
            if (e.Name == "TIMEMODIFY")
            {
                ReadPictureControl F_readpic = (ReadPictureControl)ListTool[comboBox1_SelectedIndex][m];
                if (BaslerCam.camera != null)
                    F_readpic.Modifytime(BaslerCam.camera.Parameters[PLCamera.ExposureTimeAbs].GetValue().ToString());
                return;

            }




            String oldname = _executeBuffer[number].all_test_buffer[m].imagebuffer[0];

            if (_executeBuffer[number].imageBuffer.ContainsKey(e.Name))
            {
                // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
            }
            else
            {
                _executeBuffer[number].imageBuffer.Add(e.Name, null);

            }


           _executeBuffer[number].all_test_buffer[m].imagebuffer[0] = e.Name;
           bool buffer_remove = true;
           foreach (int keyc in _executeBuffer[number].all_test_buffer.Keys)
           {
                for (int mnumber = 0; mnumber < _executeBuffer[number].all_test_buffer[keyc].imagebuffer.Count; mnumber++)
               {
                   if (oldname == _executeBuffer[number].all_test_buffer[keyc].imagebuffer[mnumber])
                        {
                            buffer_remove = false;
                            break;
                        }

                    }
            }
           if (buffer_remove)
               _executeBuffer[number].imageBuffer.Remove(oldname); 
        }


        private void ShowToolGreyRegion_SendShowToolRegionArgs(ShowToolGreyRegionEventArgs e)
        {
            int s = 0;
            s = this.dataGridView1.CurrentRow.Index; //取得選取位置
            int mx = this.comboBox1.SelectedIndex;
            List<模板GVName> match_test;
            List<HObject> Graphic_out;
            模板GVName_halcon match_test_halcon;
            string out_result_info = "";
            if (ListTool[comboBox1_SelectedIndex][s].GetType().Name != "SaveGreyTem")
                return;
            SaveGreyTem Savegreytem_control = (SaveGreyTem)ListTool[comboBox1_SelectedIndex][s];
            string Imagename = Savegreytem_control.get_Imagename() + ".img";
            hWindowControl1.HalconWindow.ClearWindow();
            this.roiController.reset();
            if (!_executeBuffer[mx].imageBuffer.ContainsKey(Savegreytem_control.Imagename()))
            {
                MessageBox.Show("请确认输入图像参数是否正确");
                return;
            }
            if (_executeBuffer[mx].imageBuffer[Savegreytem_control.Imagename()] == null)
            {
                MessageBox.Show("输入图像为空");
                return;
            }


            Display_Graphic(_executeBuffer[mx].imageBuffer[Savegreytem_control.Imagename()]);
            HTuple width_image, height_image;
            HOperatorSet.GetImageSize(hv_image, out width_image, out height_image);
            if (_executeBuffer[mx].imageBuffer[Savegreytem_control.Imagename()] == null)
                return;
             if (e.Tool == "Rectangle2")
            {
                if (Savegreytem_control.Get_image_result())
                    Display_Graphic(Savegreytem_control.Gettrain());
                else
                {
                    Savegreytem_control.setImageIn(_executeBuffer[mx].imageBuffer[Imagename]);
                    Display_Graphic(Savegreytem_control.Gettrain());
                }

                return_region_grey_type = true;
                if (Savegreytem_control.regions != null)
                {
                    if (Savegreytem_control.regions[0].GetType().ToString() == "ViewROI.ROIRectangle2")
                        roiController.displayROI(regions1);

                    else
                    {
                        roiController.removeROI(ref regions1);
                        Savegreytem_control.regions = regions1;

                    }

                }
                if (Savegreytem_control.regions == null || Savegreytem_control.regions.Count == 0)
                {
                    if (regions1 != null)
                    {
                        if (regions1.Count != 0)
                            roiController.removeROI(ref regions1);
                    }
                    roiController.genRect2(height_image / 2 - 100, width_image / 2 - 100, 0.0, 400.0, 200.0, ref regions1);
                    Savegreytem_control.regions = regions1;


                }


            }
            else if (e.Tool == "Circle")
            {
                if (Savegreytem_control.Get_image_result())
                    Display_Graphic(Savegreytem_control.Gettrain());
                else
                {
                    Savegreytem_control.setImageIn(_executeBuffer[mx].imageBuffer[Imagename]);
                    Display_Graphic(Savegreytem_control.Gettrain());
                }
                return_region_grey_type = true;

                //  Display_Graphic(_executeBuffer[mx].imageBuffer[Saveshapetem_control.Imagename()]);
                if (Savegreytem_control.regions != null)
                {
                    if (Savegreytem_control.regions[0].GetType().ToString() == "ViewROI.ROICircle")
                        roiController.displayROI(Savegreytem_control.regions);

                    else
                    {
                        roiController.removeROI(ref regions1);
                        Savegreytem_control.regions = regions1;
                    }

                }
                if (Savegreytem_control.regions == null || Savegreytem_control.regions.Count == 0)
                {

                    if (regions1 != null)
                    {
                        if (regions1.Count != 0)
                            roiController.removeROI(ref regions1);
                    }

                    roiController.genCircle(height_image/2,width_image/2, 50.0, ref regions1);
                    Savegreytem_control.regions = regions1;


                }


            }
            else if (e.Tool == "SRectangle2")
            {
                Display_Graphic(_executeBuffer[mx].imageBuffer[Savegreytem_control.Imagename()]);

                return_region_grey_type = false;
                //  Display_Graphic(_executeBuffer[mx].imageBuffer[Saveshapetem_control.Imagename()]);
                if (Savegreytem_control.sou_regions != null)
                {
                    if (Savegreytem_control.sou_regions[0].GetType().ToString() == "ViewROI.ROIRectangle2")
                        roiController.displayROI(Savegreytem_control.sou_regions);

                    else
                    {
                        roiController.removeROI(ref sou_regions1);
                        Savegreytem_control.sou_regions = sou_regions1;
                    }

                }
                if (Savegreytem_control.sou_regions == null || Savegreytem_control.sou_regions.Count == 0)
                {

                    roiController.genRect2(height_image / 2 - 200, width_image / 2 - 100, 0.0, 400.0, 200.0, ref sou_regions1);
                    Savegreytem_control.sou_regions = sou_regions1;
                }


            }
            else if (e.Tool == "SCircle")
            {
                Display_Graphic(_executeBuffer[mx].imageBuffer[Savegreytem_control.Imagename()]);

                return_region_grey_type = false;
                if (Savegreytem_control.sou_regions != null)
                {
                    if (Savegreytem_control.sou_regions[0].GetType().ToString() == "ViewROI.ROICircle")
                        roiController.displayROI(Savegreytem_control.sou_regions);

                    else
                    {
                        roiController.removeROI(ref sou_regions1);
                        Savegreytem_control.sou_regions = sou_regions1;
                    }

                }
                if (Savegreytem_control.sou_regions == null || Savegreytem_control.sou_regions.Count == 0)
                {

                   // hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, "red");
                    roiController.genCircle(height_image / 2, width_image / 2, 200.0, ref sou_regions1);
                    Savegreytem_control.sou_regions = sou_regions1;


                }


            }
            else if (e.Tool == "LearnModel")
            {

                if (Savegreytem_control.Get_image_result())
                    Display_Graphic(Savegreytem_control.Gettrain());
                Savegreytem_control.MakeModel(Savegreytem_control.Gettrain());
            }
             else if (e.Tool == "TrainModel")
             {
                 Savegreytem_control.setImageIn(_executeBuffer[mx].imageBuffer[Imagename]);
                 Display_Graphic(_executeBuffer[mx].imageBuffer[Savegreytem_control.Imagename()]);

             }
             else if (e.Tool == "MakeRegion")
             {

                 Display_Graphic(_executeBuffer[mx].imageBuffer[Savegreytem_control.Imagename()]);
                 Savegreytem_control.MakeRoi_model(roiController);
             }
            else if (e.Tool == "ModelMatching")
            {
                Display_Graphic(_executeBuffer[mx].imageBuffer[Savegreytem_control.Imagename()]);

                if (!Savegreytem_control.MatchingModel(hWndCtrl, _executeBuffer[mx].imageBuffer[Imagename], out match_test, out match_test_halcon, out Graphic_out, out out_result_info, roiController))
                {
                    MessageBox.Show("形状模板匹配：    异常，请检查");


                }
                for (int modelnumber = 0; modelnumber < Graphic_out.Count; modelnumber++)
                    hWndCtrl.addIconicVar(Graphic_out[modelnumber]);

                hWndCtrl.repaint();
                Modellist = match_test;
                Modellist_halcon = match_test_halcon;
                show_result("show_模板");

            }
            else
                return;

        }


        








        private void ShowToolRegion_SendShowToolRegionArgs(ShowToolRegionEventArgs e)
        {
            int s = 0;
            s = this.dataGridView1.CurrentRow.Index; //取得選取位置
            int mx = this.comboBox1.SelectedIndex;
            List<模板GVName> match_test;
            List<HObject> Graphic_out;
            模板GVName_halcon match_test_halcon;
            string out_result_info = "";
            if (ListTool[comboBox1_SelectedIndex][s].GetType().Name != "SaveShapeTem")
                return;
            SaveShapeTem Saveshapetem_control = (SaveShapeTem)ListTool[comboBox1_SelectedIndex][s];
            string Imagename = Saveshapetem_control.get_Imagename() + ".img";
            hWindowControl1.HalconWindow.ClearWindow();
            hWndCtrl.clearList();
            this.roiController.reset();
            if (!_executeBuffer[mx].imageBuffer.ContainsKey(Saveshapetem_control.Imagename()))
            {
                MessageBox.Show("输入图像不存在");
                return;
            }
            if (_executeBuffer[mx].imageBuffer[Saveshapetem_control.Imagename()] == null)
            {
                MessageBox.Show("输入图像为空");
                return;
            }

           // Display_Graphic(_executeBuffer[mx].imageBuffer[Saveshapetem_control.Imagename()]);
            HTuple width_image, height_image;
            HOperatorSet.GetImageSize(_executeBuffer[mx].imageBuffer[Saveshapetem_control.Imagename()], out width_image, out height_image);
            if (e.Tool == "Rectangle2")
            {

                if (Saveshapetem_control.Get_image_result())
                    Display_Graphic(Saveshapetem_control.Gettrain());
                else
                {
                    Saveshapetem_control.setImageIn(_executeBuffer[mx].imageBuffer[Imagename]);
                    Display_Graphic(Saveshapetem_control.Gettrain());
                }
                return_region_shape_type = true;
                if (Saveshapetem_control.regions != null)
                {
                    if (Saveshapetem_control.regions[0].GetType().ToString() == "ViewROI.ROIRectangle2")
                        roiController.displayROI(Saveshapetem_control.regions);

                    else
                    {
                        roiController.removeROI(ref regions2);
                        Saveshapetem_control.regions = regions2;

                    }

                }
                if (Saveshapetem_control.regions == null || Saveshapetem_control.regions.Count == 0)
                {
                    if (regions2 != null)
                    {
                        if (regions2.Count != 0)
                            roiController.removeROI(ref regions2);
                    }

                    roiController.genRect2(height_image / 2, width_image / 2, 0.0, 400.0, 200.0, ref regions2);
                    Saveshapetem_control.regions = regions2;

                }


            }
            else if (e.Tool == "Circle")
            {

                if (Saveshapetem_control.Get_image_result())
                    Display_Graphic(Saveshapetem_control.Gettrain());
                {
                    Saveshapetem_control.setImageIn(_executeBuffer[mx].imageBuffer[Imagename]);
                    Display_Graphic(Saveshapetem_control.Gettrain());
                }
                return_region_shape_type = true;

                if (Saveshapetem_control.regions != null)
                {
                    if (Saveshapetem_control.regions[0].GetType().ToString() == "ViewROI.ROICircle")
                        roiController.displayROI(Saveshapetem_control.regions);

                    else
                    {
                        roiController.removeROI(ref regions2);
                        Saveshapetem_control.regions = regions2;
                    }

                }
                if (Saveshapetem_control.regions == null || Saveshapetem_control.regions.Count == 0)
                {
                    if (regions2 != null)
                    {
                        if (regions2.Count != 0)
                            roiController.removeROI(ref regions2);
                    }
                    roiController.genCircle(height_image / 2, width_image / 2, 50.0, ref regions2);
                    Saveshapetem_control.regions = regions2;


                }


            }
            else if (e.Tool == "SRectangle2")
            {
                Display_Graphic(_executeBuffer[mx].imageBuffer[Saveshapetem_control.Imagename()]);

                return_region_shape_type = false;
                if (Saveshapetem_control.sou_regions != null)
                {
                    if (Saveshapetem_control.sou_regions[0].GetType().ToString() == "ViewROI.ROIRectangle2")
                        roiController.displayROI(Saveshapetem_control.sou_regions);

                    else
                    {
                        roiController.removeROI(ref sou_regions2);
                        Saveshapetem_control.sou_regions = sou_regions2;
                    }

                }
                if (Saveshapetem_control.sou_regions == null || Saveshapetem_control.sou_regions.Count == 0)
                {
                    roiController.genRect2(height_image / 2, width_image / 2, 0.0, 400, 200, ref sou_regions2);
                    Saveshapetem_control.sou_regions = sou_regions2;
                }


            }
            else if (e.Tool == "SCircle")
            {

                Display_Graphic(_executeBuffer[mx].imageBuffer[Saveshapetem_control.Imagename()]);
                return_region_shape_type = false;
                if (Saveshapetem_control.sou_regions != null)
                {
                    if (Saveshapetem_control.sou_regions[0].GetType().ToString() == "ViewROI.ROICircle")
                        roiController.displayROI(Saveshapetem_control.sou_regions);

                    else
                    {
                        roiController.removeROI(ref sou_regions2);
                        Saveshapetem_control.sou_regions = sou_regions2;
                    }

                }
                if (Saveshapetem_control.sou_regions == null || Saveshapetem_control.sou_regions.Count == 0)
                {
                    roiController.genCircle(height_image / 2, width_image / 2, 200.0, ref sou_regions2);
                    Saveshapetem_control.sou_regions = sou_regions2;
                }


            }
            else if (e.Tool == "LearnModel")
            {
                if (Saveshapetem_control.Get_image_result())
                    Display_Graphic(Saveshapetem_control.Gettrain());
                Saveshapetem_control.MakeModel(Saveshapetem_control.Gettrain());
            }
            else if (e.Tool == "TrainModel")
            {
                Saveshapetem_control.setImageIn(_executeBuffer[mx].imageBuffer[Imagename]);
                Display_Graphic(_executeBuffer[mx].imageBuffer[Saveshapetem_control.Imagename()]);

            }
            else if (e.Tool == "MakeRegion")
            {

                                Display_Graphic(_executeBuffer[mx].imageBuffer[Saveshapetem_control.Imagename()]);
                                Saveshapetem_control.MakeRoi_model(roiController);
            }

            else if (e.Tool == "ModelMatching")
            {
                Display_Graphic(_executeBuffer[mx].imageBuffer[Saveshapetem_control.Imagename()]);

                if (!Saveshapetem_control.MatchingModel(hWndCtrl, _executeBuffer[mx].imageBuffer[Imagename], out match_test, out match_test_halcon, out Graphic_out, out out_result_info, roiController))
                {
                    MessageBox.Show("形状模板匹配：    异常，请检查");
                }
                for (int modelnumber = 0; modelnumber < Graphic_out.Count; modelnumber++)
                    hWndCtrl.addIconicVar(Graphic_out[modelnumber]);

                hWndCtrl.repaint();
                Modellist = match_test;
                Modellist_halcon = match_test_halcon;
                show_result("show_模板");
            }
            else
                return;
            
        }

        /// <summary>
        /// 流程动作
        /// </summary>
        /// <param name="number">指定需要动作的流程</param>
        /// <param name="start">指定动作的流程的起始步骤</param>
        /// <param name="end">指定动作的流程的结束步骤</param>

        public string get_testtime()
        {
            return label_时间.Text.ToString();
        }

        public bool Runtest(String Test_name, out double Cameratime, out double alltime)
        {
            Cameratime = 0;
            alltime = 0;
            int number = 0;
            bool combox_get = false;
            bool return_value;
            for (int mtestnumber = 0; mtestnumber < File_solution.Count; mtestnumber++)
            {
                if (File_solution[mtestnumber] == Test_name)
                {
                    number = mtestnumber;
                    combox_get = true;
                    break;
                }
            }
            if (combox_get)
            {
                int s2 = this.ListTool[number].Count;
                return_value =  Runtest(Test_name, 0, s2, out Cameratime, out alltime);
            }
            else
            {
                int m2 = this.ListTool[this.comboBox1.SelectedIndex].Count;
                return_value =  Runtest(Test_name, 0, m2, out Cameratime, out alltime);
            }
            return return_value;
        
        }


        public bool Runtest(String Test_name, out double Cameratime, out double alltime,out string resulttype)
        {
            Cameratime = 0;
            alltime = 0;
            int number = 0;
            bool combox_get = false;
            resulttype = "";
            bool return_value;
            for (int mtestnumber = 0; mtestnumber < File_solution.Count; mtestnumber++)
            {
                if (File_solution[mtestnumber] == Test_name)
                {
                    number = mtestnumber;
                    combox_get = true;
                    break;
                }
            }
            if (combox_get)
            {
                int s2 = this.ListTool[number].Count;
                return_value =  Runtest(Test_name, 0, s2, out Cameratime, out alltime);
            }
            else
            {
                int m2 = this.ListTool[this.comboBox1.SelectedIndex].Count;
                return_value =  Runtest(Test_name, 0, m2, out Cameratime, out alltime);
            }
            resulttype = Result_Type;
            return return_value;


        }

        private void List初始化(List<string> Solution)
        {
            for (int i = 0; i < Solution.Count; i++)
            {
                comboBox1.Items.Add(Solution[i].ToString());
                ListTool.Add(new List<Form>());
                _executeBuffer.Add(new ExecuteBuffer());
                _sourceBuffer.Add(new SourceBuffer());
              //  all_test_buffer_save.Add(new All_buffer());
                _sourceBuffer[i]._s_ObjectBuffer = new Dictionary<Info_Source, HObject>();
                _sourceBuffer[i]._s_ControlBuffer = new Dictionary<Info_Source, object>();

                _executeBuffer[i].controlBuffer = new Dictionary<string, object>();
                _executeBuffer[i].imageBuffer = new Dictionary<string, HObject>();
                _executeBuffer[i].all_test_buffer = new Dictionary<int, All_buffer>();
            }
            comboBox1.SelectedIndex = 0;

        }






        private void List创建(int number)
        {


                ListTool.Add(new List<Form>());
                _executeBuffer.Add(new ExecuteBuffer());
                _sourceBuffer.Add(new SourceBuffer());

                _sourceBuffer[number]._s_ObjectBuffer = new Dictionary<Info_Source, HObject>();
                _sourceBuffer[number]._s_ControlBuffer = new Dictionary<Info_Source, object>();

                _executeBuffer[number].controlBuffer = new Dictionary<string, object>();
                _executeBuffer[number].imageBuffer = new Dictionary<string, HObject>();
                _executeBuffer[number].all_test_buffer = new Dictionary<int, All_buffer>();
            
                comboBox1.SelectedIndex = 0;

        }

        private void List复制(int number,int newnumber)
        {


            ListTool[newnumber] = ListTool[number];
            _executeBuffer[newnumber] = _executeBuffer[number];
            _sourceBuffer[newnumber] = _sourceBuffer[number];

        }

        #region 相机对外事件

        private string _CamID = "";
        public string CamID
        {[Browsable(true)]
            get { return _CamID; }
            set { _CamID = value; }
        }



        private string _DirKeep = "";
        public string DirKeep
        {
            [Browsable(true)]
            get { return _DirKeep; }
            set { _DirKeep = value; }
        }

        private string _CameraBrand = "";
        public string CameraBrand
        {
            [Browsable(true)]
            get { return _CameraBrand; }
            set { _CameraBrand = value; }
        }


        public string get_Solution_name()
        {
            int number = this.comboBox1_SelectedIndex;
            return File_solution[number];

            
        }

        public bool get_match_point(out double[] x, out double[] y,out double[] angle,out double[] score)
        {
            int number = Modellist_halcon.点X.TupleLength();
            if (number == 0)
            {
                x = new double[1];
                y = new double[1];

                angle = new double[1];
                score = new double[1];
                x[0] = 0;
                y[0] = 0;
                angle[0] = 0;
                score[0] = 0;
                return false;
            }
            x = new double[number];
            y = new double[number];
            
            angle = new double[number];
            score = new double[number];
            if (Modellist != null)
            {
                if (Modellist.Count == 0)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < number; i++)
                    {
                        x[i] = Modellist_halcon.点X[i].D;
                        y[i] = Modellist_halcon.点Y[i].D;
                        angle[i] = Modellist_halcon.角度Angle[i].D;
                        score[i] = Modellist_halcon.分数Score[i].D;

                    }
                    return true;
                }
            }
            else
                return false;
        }


        public bool get_blob_point(out double[] x, out double[] y, out double[] area)
        {
            int number = Bloblist.Count;
            if (number == 0)
            {
                x = new double[1];
                y = new double[1];

                area = new double[1];

                x[0] = 0;
                y[0] = 0;
                area[0] = 0;

                return false;
            }
            x = new double[number];
            y = new double[number];

            area = new double[number];

            if (Bloblist != null)
            {
                if (Bloblist.Count == 0)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < number; i++)
                    {
                        x[i] = Convert.ToDouble(Bloblist[i].域中心X);
                        y[i] = Convert.ToDouble(Bloblist[i].域中心Y);
                        area[i] = Convert.ToDouble(Bloblist[i].域面积);


                    }
                    return true;
                }
            }
            else
                return false;
        }


        public bool get_circle_point(out double[] x, out double[] y, out double[] radius)
        {
            int number = Circlelist.Count;
            if (number == 0)
            {
                x = new double[1];
                y = new double[1];

                radius = new double[1];

                x[0] = 0;
                y[0] = 0;
                radius[0] = 0;

                return false;
            }
            x = new double[number];
            y = new double[number];

            radius = new double[number];

            if (Circlelist != null)
            {
                if (Circlelist.Count == 0)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < number; i++)
                    {
                        x[i] = Convert.ToDouble(Circlelist[i].圆心X);
                        y[i] = Convert.ToDouble(Circlelist[i].圆心Y);
                        radius[i] = Convert.ToDouble(Circlelist[i].半径R);


                    }
                    return true;
                }
            }
            else
                return false;
        }

        public bool get_line_point(out double[] x1, out double[] y1, out double[] x2, out double[] y2)
        {
            int number = Linelist.Count;
            if (number == 0)
            {
                x1 = new double[1];
                y1 = new double[1];
                x2 = new double[1];
                y2 = new double[1];

                x1[0] = 0;
                y1[0] = 0;
                x2[0] = 0;
                y2[0] = 0;

                return false;
            }
            x1 = new double[number];
            y1 = new double[number];
            x2 = new double[number];
            y2 = new double[number];



            if (Linelist != null)
            {
                if (Linelist.Count == 0)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < number; i++)
                    {
                        x1[i] = Convert.ToDouble(Linelist[i].点1X);
                        y1[i] = Convert.ToDouble(Linelist[i].点1Y);
                        x2[i] = Convert.ToDouble(Linelist[i].点2X);
                        y2[i] = Convert.ToDouble(Linelist[i].点2Y);



                    }
                    return true;
                }
            }
            else
                return false;
        }

        public bool get_charlist_result(out string[] charname)
        {
            int char_count = Charlist.Count;

            if (char_count == 0)
            {
                charname = new string[1];
                return false;
            }
            charname = new string[char_count];
            for (int i = 0; i < char_count; i++)
                charname[i] = Charlist[i].字符串.ToString();
                return true;
        }

        public bool get_list_point(int Point_number,out double[] x, out double[] y)
        {

            if (!Tem_Pointlist.ContainsKey(Point_number))
            {
                x = new double[1];
                y = new double[1];
                MessageBox.Show("点位不存在");
                return  false;
            }
            int number = Tem_Pointlist[Point_number].点X.TupleLength();
            x = new double[number];
            y = new double[number];
            if (number == 0)
            {
                x = new double[1];
                y = new double[1];
                x[0] = 0;
                y[0] = 0;
                
                return false;
            }

                    for (int i = 0; i < number; i++)
                    {
                        x[i] = Tem_Pointlist[Point_number].点X[i].D;
                        y[i] = Tem_Pointlist[Point_number].点Y[i].D;


                    }
                    return true;


        }


        public void add_point(int no, double[]  rowx, double[] coly)
        {
            if (Tem_Pointlist.ContainsKey(no))
            {
                Tem_Pointlist[no].点X = (HTuple)rowx;

                Tem_Pointlist[no].点Y = (HTuple)coly;
            }
            else
                Tem_Pointlist.Add(no, new PointName(rowx, coly));
        }

        public void add_Result_show(double rowx, double coly, string model,double info1,double info2,double info3)
        {
            if (model == "Circle")
            {

                Tem_Circlelist.圆心X.Append(rowx);
                Tem_Circlelist.圆心Y.Append(coly);
                Tem_Circlelist.半径R.Append(info1);

                
            }
            else if (model == "CircleArc")
            {
                //Tem_CircleArclist.圆心X = (HTuple)rowx;
                //Tem_CircleArclist.圆心Y = (HTuple)coly;
                //Tem_CircleArclist.圆弧Start = (HTuple)info2;
                //Tem_CircleArclist.圆弧End = (HTuple)info3;
                //Tem_CircleArclist.半径R = (HTuple)info1;
                Tem_CircleArclist.圆心X.Append(rowx);
                Tem_CircleArclist.圆心Y.Append(coly);
                Tem_CircleArclist.圆弧Start.Append(info2);
                Tem_CircleArclist.圆弧End.Append(info3);
                Tem_CircleArclist.半径R.Append(info1);
            }
            else
            { 
                //Tem_Linelist.点1X = (HTuple)rowx;
                //Tem_Linelist.点1Y = (HTuple)coly;
                //Tem_Linelist.点2X = (HTuple)info1;
                //Tem_Linelist.点2Y = (HTuple)info2;
                Tem_Linelist.点1X.Append(rowx);
                Tem_Linelist.点1Y.Append(coly);
                Tem_Linelist.点2X.Append(info1);
                Tem_Linelist.点2Y.Append(info2);
            
            }

        }



        public void add_Result_show(double rowx, double coly, string model, double info1, double info2)
        {
            add_Result_show(rowx, coly, model, info1, info2, 0);
        }
        public void add_Result_show(double rowx, double coly, string model, double info1)
        {
            add_Result_show(rowx, coly, model, info1, 0, 0);
        }
        public void clear_result_show()
        {

            Tem_Circlelist = new 圆GVName_halcon();
            Tem_CircleArclist = new 圆弧GVName_halcon();
            Tem_Linelist = new 直线GVName_halcon();


        }

        public void Ini_combox()
        {
            if (CameraBrand == "BASLER")
                comboBox_相机品牌.SelectedIndex = 0;
            else if (CameraBrand == "HIKVISION")
                comboBox_相机品牌.SelectedIndex = 1;
            else
                comboBox_相机品牌.SelectedIndex = 2;
            listBox_相机编号.Items.Clear();
            listBox_相机编号.Items.Add(CamID);



        }

        private static HTuple Clone(HTuple source)
        {
            if (!typeof(HTuple).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return default(HTuple);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (HTuple)formatter.Deserialize(stream);
            }
        }







        private void ChangeList(int s)
        {
            int after = 0;
            List<DataGVName> ListDataGV1 = new List<DataGVName>();
            int Thread_Number = comboBox1_SelectedIndex;
            if (s == 0)
            {
                MessageBox.Show("现在已经是顶行");
                return;
            }
            for (int m = 0; m < s-1; m++)
                ListDataGV1.Insert(m, ListDataGV[m]);
            ListDataGV1.Insert(s-1, ListDataGV[s]);
            ListDataGV1.Insert(s, ListDataGV[s-1]);
            for (int m = s+1; m < ListDataGV.Count; m++)
                ListDataGV1.Insert(m, ListDataGV[m]);
            ListDataGV = ListDataGV1;
            Form xx = new Form();
            xx = ListTool[Thread_Number][s - 1];
            ListTool[Thread_Number][s - 1] = ListTool[Thread_Number][s];
            ListTool[Thread_Number][s] = xx;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ListDataGV1;
            Change_test_buffer(s, Thread_Number);
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.RowHeadersWidth = 30;

            dataGridView1.CurrentCell = dataGridView1.Rows[after].Cells[0];

        }




        /// <summary>
        /// 添加工具流程
        /// </summary>
        /// <param name="ToolStripMenuItem">添加的工具名称选项</param>
        /// <param name="Action_copy">确认工具是新增还是复制粘贴选项</param>


        private void ListToolADD(string ToolStripMenuItem,bool Action_copy)
        {
            int after = 0;
           List<DataGVName> ListDataGV1 = new List<DataGVName>();
           for (int m = 0; m < ListDataGV.Count;m++ )
               ListDataGV1.Insert(m, ListDataGV[m]);

               if (ListTool[comboBox1_SelectedIndex].Count == 0)
               {
                   dataGridView1.DataSource = null;

                   ADDSwitch(ListDataGV, ListTool, ToolStripMenuItem, 0, Action_copy);
                   ListDataGV1.Insert(0, ListDataGV[0]);
                   dataGridView1.DataSource = ListDataGV1;
               }
               else
               {
                   if (this.dataGridView1.RowCount == 0)
                       MessageBox.Show("now datagridview is wrong");
                   int s = this.dataGridView1.CurrentRow.Index;


                   switch (s)
                   {
                       case 0:
                           if (Ensure_step)
                           {
                               DialogResult myResult = MessageBox.Show("加入首行", "新增欄位位置", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                               if (myResult == DialogResult.Yes)
                               {
                                   dataGridView1.DataSource = null;

                                   ADDSwitch(ListDataGV, ListTool, ToolStripMenuItem, 0, Action_copy);
                                   ListDataGV1.Insert(0, ListDataGV[0]);
                                   dataGridView1.DataSource = ListDataGV1;

                                   after = 0;
                               }
                               else
                               {
                                   dataGridView1.DataSource = null;

                                   ADDSwitch(ListDataGV, ListTool, ToolStripMenuItem, s + 1, Action_copy);
                                   ListDataGV1.Insert(s + 1, ListDataGV[s + 1]);
                                   dataGridView1.DataSource = ListDataGV1;

                                   after = s + 1;
                               }
                           }
                           else
                           {
                               dataGridView1.DataSource = null;

                               ADDSwitch(ListDataGV, ListTool, ToolStripMenuItem, s + 1, Action_copy);
                               ListDataGV1.Insert(s + 1, ListDataGV[s + 1]);
                               dataGridView1.DataSource = ListDataGV1;

                               after = s + 1;
                           }

                           break;

                       default:
                           dataGridView1.DataSource = null;

                           ADDSwitch(ListDataGV, ListTool, ToolStripMenuItem, s + 1, Action_copy);
                           ListDataGV1.Insert(s + 1, ListDataGV[s + 1]);
                           dataGridView1.DataSource = ListDataGV1;

                           after = s + 1;

                           break;
                   }
               }

            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.RowHeadersWidth = 30;
            
            dataGridView1.CurrentCell = dataGridView1.Rows[after].Cells[0];
        }
  
        /// <summary>
        /// 响应工具添加按键
        /// </summary>
        /// <param name="sender">添加工具事件</param>
        /// <param name="e">工具事件</param>
        private void Splitcontainerbut_Click(object sender, EventArgs e)
        {
            Button split_but = sender as Button;
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            Panel_zhedie = false;
            ListToolADD(split_but.Name,false);
        }


        private void Change_test_buffer(int s, int threadnumber)
        {
            // _executeBuffer.all_test_buffer.Add(s, new All_buffer());
            int count = _executeBuffer[threadnumber].all_test_buffer.Count;
            _executeBuffer[threadnumber].all_test_buffer.Add(count, new All_buffer());
            for (int i = 0; i < _executeBuffer[threadnumber].all_test_buffer[s-1].imagebuffer.Count(); i++)
            {
                _executeBuffer[threadnumber].all_test_buffer[count].imagebuffer.Add(_executeBuffer[threadnumber].all_test_buffer[s - 1].imagebuffer[i]);
            }
            for (int j = 0; j < _executeBuffer[threadnumber].all_test_buffer[s - 1].controlbuffer.Count(); j++)
            {
                _executeBuffer[threadnumber].all_test_buffer[count].controlbuffer.Add(_executeBuffer[threadnumber].all_test_buffer[s - 1].controlbuffer[j]);
            }
            _executeBuffer[threadnumber].all_test_buffer[s-1].imagebuffer.Clear();
            _executeBuffer[threadnumber].all_test_buffer[s-1].controlbuffer.Clear();
            for (int i = 0; i < _executeBuffer[threadnumber].all_test_buffer[s].imagebuffer.Count(); i++)
            {
                _executeBuffer[threadnumber].all_test_buffer[s-1].imagebuffer.Add(_executeBuffer[threadnumber].all_test_buffer[s].imagebuffer[i]);
            }
            for (int j = 0; j < _executeBuffer[threadnumber].all_test_buffer[s].controlbuffer.Count(); j++)
            {
                _executeBuffer[threadnumber].all_test_buffer[s-1].controlbuffer.Add(_executeBuffer[threadnumber].all_test_buffer[s].controlbuffer[j]);
            }
            _executeBuffer[threadnumber].all_test_buffer[s].imagebuffer.Clear();
            _executeBuffer[threadnumber].all_test_buffer[s].controlbuffer.Clear();
            for (int i = 0; i < _executeBuffer[threadnumber].all_test_buffer[count].imagebuffer.Count(); i++)
            {
                _executeBuffer[threadnumber].all_test_buffer[s].imagebuffer.Add(_executeBuffer[threadnumber].all_test_buffer[count].imagebuffer[i]);
            }
            for (int j = 0; j < _executeBuffer[threadnumber].all_test_buffer[count].controlbuffer.Count(); j++)
            {
                _executeBuffer[threadnumber].all_test_buffer[s].controlbuffer.Add(_executeBuffer[threadnumber].all_test_buffer[count].controlbuffer[j]);
            }
            _executeBuffer[threadnumber].all_test_buffer.Remove(count);
        }

        private void Add_test_buffer(int s, int threadnumber, int numberimage, int numbercontrol)
        {
           // _executeBuffer.all_test_buffer.Add(s, new All_buffer());
            int count = _executeBuffer[threadnumber].all_test_buffer.Count;
            if (s < count)
            {
                _executeBuffer[threadnumber].all_test_buffer.Add(count, new All_buffer());
                for (int snumber = 0; snumber < numberimage; snumber++)
                    _executeBuffer[threadnumber].all_test_buffer[s].imagebuffer.Add("");
                for (int snumber = 0; snumber < numbercontrol; snumber++)
                    _executeBuffer[threadnumber].all_test_buffer[s].controlbuffer.Add("");

                for (int m = count; m > s; m--)
                {
                  //  _executeBuffer[threadnumber].all_test_buffer[m] = _executeBuffer[threadnumber].all_test_buffer[m - 1];
                    _executeBuffer[threadnumber].all_test_buffer[m].imagebuffer.Clear();
                    _executeBuffer[threadnumber].all_test_buffer[m].controlbuffer.Clear();
                    for (int i = 0; i < _executeBuffer[threadnumber].all_test_buffer[m-1].imagebuffer.Count(); i++)
                    {
                        _executeBuffer[threadnumber].all_test_buffer[m].imagebuffer.Add(_executeBuffer[threadnumber].all_test_buffer[m - 1].imagebuffer[i]);
                    }
                    for (int j = 0; j < _executeBuffer[threadnumber].all_test_buffer[m-1].controlbuffer.Count(); j++)
                    {
                        _executeBuffer[threadnumber].all_test_buffer[m].controlbuffer.Add(_executeBuffer[threadnumber].all_test_buffer[m - 1].controlbuffer[j]);
                    }



                }
                for (int snumber = 0; snumber < numberimage; snumber++)
                    _executeBuffer[threadnumber].all_test_buffer[s].imagebuffer.Add("");
                for (int snumber = 0; snumber < numbercontrol; snumber++)
                    _executeBuffer[threadnumber].all_test_buffer[s].controlbuffer.Add("");
                //  _executeBuffer.all_test_buffer[s].image_buffer = "";

            }
            else
            {
                _executeBuffer[threadnumber].all_test_buffer.Add(s, new All_buffer());
                for (int snumber = 0; snumber < numberimage; snumber++)
                    _executeBuffer[threadnumber].all_test_buffer[s].imagebuffer.Add("");
                for (int snumber = 0; snumber < numbercontrol; snumber++)
                    _executeBuffer[threadnumber].all_test_buffer[s].controlbuffer.Add("");

            }

        
        }





        private void Display_Graphic(HObject output)
        {
            try
            {
                if (output != null)
                {
                    if (output.GetObjClass() == "image")
                    {
                        hv_image = output;
                        HImage image = new HImage(output);
                        hWndCtrl.addIconicVar(image);
                        hWndCtrl.repaint();
                        show_center();
                    }
                    else
                    {
                        hWndCtrl.addIconicVar(output);
                        hWndCtrl.repaint();
                    }
                }
                else
                    MessageBox.Show("确认是否有图像");
            }
            catch {

            }

        }

        private void Display_Graphic1(HObject output)
        {
            if (output != null)
            {
                if (output.GetObjClass() == "image")
                {
                    hv_image = output;
                    HImage image = new HImage(output);
                    hWndCtrl.addIconicVar(image);
                 //   hWndCtrl.repaint();
                }
                else
                {
                    hWndCtrl.addIconicVar(output);
               //     hWndCtrl.repaint();
                }
            }
            else
                MessageBox.Show("确认是否有图像");

        }



        public bool Cam_Ini()
        {
            try
            {
                Img_show = true;


                if (CurCam_IniStatus == false)
                {
                    //Basler_Ini(Camera_name);
                  //  Basler_Ini(CamID);
                    if (Camera_Ini(listBox_相机编号.Items[0].ToString()))
                    {
                        Cam_ContinuousTake(true);
                        CurCam_IniStatus = true;
                        this.button_相机连接.Enabled = false;
                        this.button_断开.Enabled = true;
                    }

                }
                else
                {
                    CurCam_IniStatus = false;
                }
                return CurCam_IniStatus;
            }
            catch (Exception)
            {
                CurCam_IniStatus = false;
                return false;
            }
        }

        public void Cam_Close()
        {
            Basler_Closed();
            CurCam_IniStatus = false;
            this.button_相机连接.Enabled = true;
            this.button_断开.Enabled = false;
        }
        private void Cam_OpenImage()
        {

            Img_show = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "bmp files (*.bmp)|*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strPathName = openFileDialog1.FileName;
            }
        }

        public void Cam_SingleTake()
        {
            Img_show = false;
           // Basler_GrabOne();
        }
        private void Cam_ContinuousTake(bool SW)
        {
            if (SW)
            {
                Img_show = true;
                Basler_StartGrabbing();
            }
            else
            {
                Basler_StopGrabb();
            }
        }


        private List<string> Cam_GetImageInfo()
        {
            string CurCamPix = "";
            List<string> ArrayCamPix = new List<string>();
            CurCamPix = ResultPosX.ToString("F3") + "," + ResultPosY.ToString("F3") + "," + ResultAngle.ToString("F3") + "," + ResultScore.ToString("F3");
            ArrayCamPix.Add(CurCamPix + ".");
            return ArrayCamPix;
        }
        private string GetCamModelName()
        {
            return CamModelName;
        }
        #endregion

        #region 菜单

        #endregion

        #region 控件操作



        #endregion


        #region Basler
        BaslerCam mCamera;



        public bool Camera_Ini(String ID)
        {
            if (comboBox_相机品牌.SelectedIndex == 0)
            {
                if (Basler_Ini(ID))
                    return true;
                else
                    return false;
            }
            else if (comboBox_相机品牌.SelectedIndex == 1)
                Hikvision(ID);
            else
                OtherCamera_Ini(ID);

            return true;
        
        
        }

        private int Hikvision_checkCamera(string ID)
        {


                return -1;
        }

        private bool Hikvision(string ID)
        {


            return true;
        }
        private bool OtherCamera_Ini(String ID)
        {
            return true;
        }

        private bool Basler_Ini(string ID)
        {
            try
            {
                mCamera = new BaslerCam(ID);
                if (!mCamera.OpenCam())
                    return false;
                mCamera.SetHeartBeatTime(5000);                      // 设置相机1心跳时间

                mCamera.eventComputeGrabTime += GrabTime;    // 注册计算采集图像时间回调函数
                mCamera.eventProcessImage += VideoImage;         // 注册halcon显示回调函数 

                if (BaslerCam.camera.Parameters.Contains(PLCamera.GainRaw))
                {
                     intSliderUserControl1.Parameter = BaslerCam.camera.Parameters[PLCamera.GainRaw];

                }
                else
                {
                   // gainSliderControl1.Parameter = camera.Parameters[PLCamera.Gain];
                }


                if (BaslerCam.camera.Parameters.Contains(PLCamera.ExposureTimeAbs))
                {
                       floatSliderUserControl1.Parameter = BaslerCam.camera.Parameters[PLCamera.ExposureTimeAbs];
                }
                else
                {
                       floatSliderUserControl1.Parameter = BaslerCam.camera.Parameters[PLCamera.ExposureTime];
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        private bool Basler_Closed()
        {
            try
            {
                mCamera.StopGrabbing();
                delay(200);
                mCamera.CloseCam();
                return true;
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
                return false;
            }
        }

        private bool Basler_StartGrabbing()
        {
            mCamera.StartGrabbing();
            return true;
        }
        private bool Basler_StopGrabb()
        {

            return true;
        }
        private void GrabTime(long time)
        {

        }
        
        private void VideoImage(Bitmap image, IntPtr ptrBmp)
        {
                if (Realtime_display)
                {

                  // Display_show(BitmapToHImage(image));
                   Display_Graphic1(BitmapToHImage(image));
                   show_center();
                }
        }

        private void delay(float Interval)
        {
            if ((int)Interval == 0)
            {
                return;
            }
            DateTime __time = DateTime.Now;
            long __Span = (long)(Interval * 10000);           //因为时间是以100纳秒为单位。
            while (DateTime.Now.Ticks - __time.Ticks < __Span)
            {
                System.Windows.Forms.Application.DoEvents();

            }
        }
        #endregion


        private void PMAControl_Load(object sender, EventArgs e)
        {
            //窗口设定
            Ini_combox();
            MakeDir();
            hWndCtrl = new HWndCtrl(hWindowControl1);
            hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, "red");
            hWndCtrl.setViewState(HWndCtrl.MODE_VIEW_NONE);
            roiController = new ROIController();
            hWndCtrl.useROIController(roiController);
            asc.controllInitializeSize(this);

            
            Rectangle rect = new Rectangle();
            rect = Screen.GetWorkingArea(this);
            int srcWidth = rect.Width;//屏幕宽
            int srcHeight = rect.Height;//屏幕高

            asc.controlAutoSize(this,srcWidth,srcHeight);

            roiController.reset();
            讀取(CommonData.Path);
          //  MessageBox.Show(this.hWindowControl1.Width.ToString());
          //  MessageBox.Show(this.hWindowControl1.Height.ToString());
           // hWindowControl1.HalconWindow.ClearWindow();

        }



        public void Cam_Realshow()
        {
           if (CurCam_IniStatus)
            {
                Realtime_display = true;
                Cam_ContinuousTake(true);
            }
            else
                MessageBox.Show("相机未打开");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            流程切換(false);
            this.txt_Solutionname.Text = this.comboBox1.SelectedItem.ToString();
        }

      








        private int Grap()
        {
            return ErrorCode;
        }

        private void Hikvision_SingleTrigger()
        {
            ErrorCode = Grap();
            if (ErrorCode != 1)
            {
                MessageBox.Show("Grapp fail");
            }

        }

        private void Camera_onepicture()
        {

            if (comboBox_相机品牌.SelectedIndex == 0)
                Basler_GrabOne();
            else
            {
                ErrorCode = Grap();
                if (ErrorCode != 1)
                {
                    MessageBox.Show("Grapp fail");
                }

            }

        }
        private bool Camera_onepicture_Process(out HObject out_bitmap)
        {
            Bitmap bitmap = null;
            IntPtr ptrBmp = new IntPtr();
            out_bitmap = null;
            try
            {  
                if (comboBox_相机品牌.SelectedIndex == 0)
                {
                mCamera.GrabOne(out bitmap, out ptrBmp);
                out_bitmap = BitmapToHImage(bitmap);
                }
                else
                {
                    ErrorCode = Grap();
                    if (ErrorCode != 1)
                    {
                        MessageBox.Show("Grapp fail");
                    }
                    out_bitmap = null;
                  //myCamera_hi.Disp();
                }
                return true;

            }
            catch (Exception)

            {
                MessageBox.Show("请确认相机是否打开");
                return false; 
            }



        }




        private static HObject BitmapToHImage(Bitmap SrcImage)
        {
            HObject Hobj;
            HOperatorSet.GenEmptyObj(out Hobj);
 
            Point po = new Point(0, 0);
            Size so = new Size(SrcImage.Width, SrcImage.Height);//template.Width, template.Height
            Rectangle ro = new Rectangle(po, so);
 
            Bitmap DstImage = new Bitmap(SrcImage.Width, SrcImage.Height, PixelFormat.Format8bppIndexed);
            DstImage = SrcImage.Clone(ro, PixelFormat.Format8bppIndexed);
 
            int width = DstImage.Width;
            int height = DstImage.Height;
            
            Rectangle rect = new Rectangle(0, 0, width, height);
            System.Drawing.Imaging.BitmapData dstBmpData =
                DstImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);//pImage.PixelFormat
            int PixelSize = Bitmap.GetPixelFormatSize(dstBmpData.PixelFormat) / 8;
            int stride = dstBmpData.Stride;
            
            //重点在此
            unsafe
            {
                int count = height * width;
                byte[] data = new byte[count];
                byte* bptr = (byte*)dstBmpData.Scan0;
                fixed (byte* pData = data)
                {
                    for (int i = 0; i < height; i++)
                        for (int j = 0; j < width; j++ )
                            {
                                 data[i * width + j ] = bptr[i * stride + j];
                            }
                    HOperatorSet.GenImage1(out Hobj, "byte", width, height, new IntPtr(pData));
                }
            }
 
            DstImage.UnlockBits(dstBmpData);
          //  DstImage.Save("F:\\2.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            return Hobj;
        }


        private bool Basler_GrabOne()
        {
            try
            {
                Bitmap bitmap = null; 
                IntPtr ptrBmp = new IntPtr();
                mCamera.GrabOne(out bitmap, out ptrBmp);

                Display_Graphic(BitmapToHImage(bitmap));

                return true;
            }
            catch (Exception)
            { return false; }
        }

        private void Hikvision_ContinueTrigger()
        {
            Thread GrapContinue = new Thread(new ThreadStart(Continues));
            GrapContinue.Start();

        }

        public ManualResetEvent stopEventHandle = new ManualResetEvent(false);
        private void Continues()
        {
            while (true)
            {
                ErrorCode = Grap();
                if (ErrorCode != 1)
                {
                    MessageBox.Show("Grapp fail");
                }


                if (stopEventHandle.WaitOne(0, true)) break;
            }
            stopEventHandle.Reset();
        }


        //单工具运行
        private void 运行ToolMenuItem1_Click(object sender, EventArgs e)
        {
            int s = 0;
            s = this.dataGridView1.CurrentRow.Index; //取得選取位置
            OneRuntest(s);
        }











        //单流程按键动作
        private void button2_Click_1(object sender, EventArgs e)
        {
            int s1 = this.comboBox1_SelectedIndex;
            int s2 = this.ListTool[s1].Count;
            double cameratime = 0;
            double alltime = 0;
            Runtest(this.comboBox1.SelectedItem.ToString(), 0, s2, out cameratime, out alltime);
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            Panel_zhedie = true;
        }

        private void button_addsolution_Click(object sender, EventArgs e)
        {
            if (txt_Solutionname.Text == "")
            {
                MessageBox.Show("请重新设置流程名");
                return;
            }
            for (int i = 0; i < File_solution.Count; i++)
            {
                if (txt_Solutionname.Text == File_solution[i])
                {
                    MessageBox.Show("流程名已经存在 ，不能重复添加");
                    return;
                }
            }
            File_solution.Add(txt_Solutionname.Text);
            comboBox1.Items.Add(txt_Solutionname.Text);
            List创建(File_solution.Count - 1);
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;



        }

        private void Del_solution_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 1)
            {
                MessageBox.Show("最后一个工具，不能被删除");
                return;
            }
            if (ListTool.Count == 1)
            {
                MessageBox.Show("最后一个工具，不能被删除");
                return;
            }
            int m = this.comboBox1.SelectedIndex;
            this.comboBox1.Items.RemoveAt(m);
            File_solution.RemoveAt(m);
            ListTool.RemoveAt(m);
            _executeBuffer.RemoveAt(m);
            _sourceBuffer.RemoveAt(m);
            if (m == -1)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = m - 1;
            if (m == comboBox1.Items.Count)
                comboBox1_SelectedIndex = m - 1;
        }

        private void Modify_solution_Click(object sender, EventArgs e)
        {
            int m = this.comboBox1.SelectedIndex;
            if (txt_Solutionname.Text == "")
            {
                MessageBox.Show("请重新设置流程名");
                return;
            }
            for (int i = 0; i < File_solution.Count; i++)
            {
                if (txt_Solutionname.Text == File_solution[i])
                {
                    MessageBox.Show("流程名已经存在 ，不能修改成相同名称");
                    return;
                }
            }
            //this.comboBox1.Items[m] = txt_Solutionname.Text;
            // File_solution[m] = txt_Solutionname.Text;

            File_solution.Add(txt_Solutionname.Text);
            comboBox1.Items.Add(txt_Solutionname.Text);
            List创建(File_solution.Count - 1);
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            List复制(m, comboBox1.Items.Count - 1);
            // 儲存(CommonData.Path); 
            // 讀取(CommonData.Path);   

        }
        private void button_camera_Click(object sender, EventArgs e)
        {
            Button but_camera = sender as Button;
            switch (but_camera.Name)
            {
                case "button_相机连接":
                    if (!CurCam_IniStatus)
                    {
                        Cam_Ini();

                    }
                    break;
                case "button_断开":
                    if (CurCam_IniStatus)
                    {
                        Cam_Close();

                    }
                    else
                        MessageBox.Show("相机未打开");
                    break;
                case "button_取图":
                    button_取图.Enabled = false;
                    if (CurCam_IniStatus)
                        Camera_onepicture();
                    else
                        MessageBox.Show("相机未打开");
                    button_取图.Enabled = true;
                    break;
                case "button_实时显示":
                    button_实时显示.Enabled = false;
                    if (CurCam_IniStatus)
                    {
                        Realtime_display = true;
                        Cam_ContinuousTake(true);
                    }
                    else
                        MessageBox.Show("相机未打开");
                  //  show_center();
                    button_实时显示.Enabled = true;
                    break;
                default:
                    break;
            }
        }


        //相机设置面板隐藏
        private void 设置隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.SendToBack();
        }
        //相机设置面板显示 
        private void 设置显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Camerashow_setting = !Camerashow_setting;
            if (Camerashow_setting)
                panel2.BringToFront();
            else
                panel2.SendToBack();
        }
        //复制工具
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int s = 0;
            s = this.dataGridView1.CurrentRow.Index; //取得選取位置
            string formName;
            formName = ListTool[comboBox1_SelectedIndex][s].GetType().Name; //取得form類型
            ListTool_backup = ListTool[comboBox1_SelectedIndex][s];
            ListTool_exist = true;
        }


        //通过复制添加工具到工具流程中
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int s = 0;
            s = this.dataGridView1.CurrentRow.Index; //取得選取位置

            if (ListTool_exist)
            {
                ListToolADD(null, true);

            }

        }



        //显示软件测试结果
        private void button_Datagridview2show_Click(object sender, EventArgs e)
        {
            Button but_grid = sender as Button;
            switch (but_grid.Name)
            {
                case "button_点":
                    dataGridView2.DataSource = Pointlist;
                    break;
                case "button_直线":
                    dataGridView2.DataSource = Linelist;
                    break;
                case "button_圆":
                    dataGridView2.DataSource = Circlelist;
                    break;
                case "button_模板":
                    dataGridView2.DataSource = Modellist;
                    break;
                case "button_长度":
                    dataGridView2.DataSource = Lengthlist;
                    break;
                case "button_角度":
                    dataGridView2.DataSource = Anglelist;
                    break;
                case "button_字符串":
                    dataGridView2.DataSource = Charlist;
                    break;
                case "button_斑点":
                    dataGridView2.DataSource = Bloblist;
                    break;
                default:
                    break;
            }
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //標題寬度滿版
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            //選取整行
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }




        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int number = comboBox1_SelectedIndex;
            CleearBufferRedundance(number);
            Showwindow();
            //  MessageBox.Show("now is not godd");
        }
        //响应按键，删除单个工具
        private void 刪除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListTool.Count == 0)
            {
                return;
            }
            int row = this.dataGridView1.CurrentRow.Index;
            ListToolDelet(row);

        }

        private void Halcon_press(object sender, EventArgs e)
        {
            ToolStripDropDownItem sender_name = sender as ToolStripDropDownItem;
            switch (sender_name.Name)
            {
                case "moveToolStripMenuItem":
                    hWndCtrl.setViewState(HWndCtrl.MODE_VIEW_MOVE);
                    break;
                case "resetWindowToolStripMenuItem":
                    hWndCtrl.resetWindow();
                    hWndCtrl.repaint();
                    break;
                case "noneToolStripMenuItem":
                    hWndCtrl.setViewState(HWndCtrl.MODE_VIEW_NONE);
                    break;
                case "rectangle1ToolStripMenuItem":
                    roiController.setROIShape(new ROIRectangle1());
                    break;
                case "rectangle2ToolStripMenuItem":
                    roiController.setROIShape(new ROIRectangle2());
                    break;
                case "circleToolStripMenuItem":
                    roiController.setROIShape(new ROICircle());
                    break;
                case "circularArcToolStripMenuItem":
                    roiController.setROIShape(new ROICircularArc());
                    break;
                case "LineToolStripMenuItem":
                    roiController.setROIShape(new ROILine());
                    break;
                case "DelROIToolStripMenuItem":
                    roiController.removeActive();
                    break;
                case "保存当前ROIToolStripMenuItem1":
                    保存当前ROIToolStripMenuItem1_action();
                    break;
                case "reseToolStripMenuItem":
                    hWndCtrl.resetAll();
                    hWndCtrl.repaint();
                    break;
                case "保存图像ToolStripMenuItem":
                    save_imge();
                    break;
                default:
                    break;


            }


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Showwindow();

        }

        private void button_Preprocessing_Click(object sender, EventArgs e)
        {
            int Source_show = comboBox1_SelectedIndex;
            SourceManage sm = new SourceManage(_sourceBuffer, _executeBuffer, Source_show);

            sm.Show();
        }

        private void button_Makemodel_Click(object sender, EventArgs e)
        {
            int number = comboBox1_SelectedIndex;
            MatchingForm matchingForm = new MatchingForm(_executeBuffer[number], _sourceBuffer[number], _sourceIni);
            matchingForm.Show();
        }



        //储存工具流程
        private void 保存ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要保存吗?","保存系统",messButton);

             if (dr == DialogResult.OK)//如果点击“确定”按钮

             {

                 儲存(CommonData.Path);

             }

        }
        //读取文档工具流程
        private void 读取ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            讀取(CommonData.Path);
        }

        private void 参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void show_center()
        {

            HObject cross1, cross2;
            HTuple size1 = 10;
            HOperatorSet.GenEmptyObj(out cross1);
            HOperatorSet.GenEmptyObj(out cross2);

            HTuple outx, outy;

            HOperatorSet.GetImageSize(hv_image, out outx, out outy);
            HOperatorSet.GenContourPolygonXld(out cross1, (outy / 2).TupleConcat(
    outy / 2), (new HTuple(-0.5)).TupleConcat(outx-0.5));
            HOperatorSet.GenContourPolygonXld(out cross2, (new HTuple(-0.5)).TupleConcat(
outy-0.5), (outx / 2).TupleConcat(outx / 2));

            hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, "red");
            

            hWndCtrl.addIconicVar(cross2);
            hWndCtrl.addIconicVar(cross1);
            hWndCtrl.repaint();


        }


        private void show_result(string Type_show_name)
        {
            dataGridView2.DataSource = null;
            switch (Type_show_name)
            {
                case "show_点":
                    dataGridView2.DataSource = Pointlist;
                    break;
                case "show_直线":
                    dataGridView2.DataSource = Linelist;
                    break;
                case "show_圆":
                    dataGridView2.DataSource = Circlelist;
                    break;
                case "show_模板":
                    dataGridView2.DataSource = Modellist;
                    break;
                case "show_长度":
                    dataGridView2.DataSource = Lengthlist;
                    break;
                case "show_角度":
                    dataGridView2.DataSource = Anglelist;
                    break;
                case "show_字符串":
                    dataGridView2.DataSource = Charlist;
                    break;
                case "show_斑点":
                    dataGridView2.DataSource = Bloblist;
                    break;
                default:
                    break;
            }
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //標題寬度滿版
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            //選取整行
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
          //  dataGridView2.Refresh();
        }

        private void Initial_Datagridview_Result()
        {
            dataGridView2.DataSource = Pointlist;
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //標題寬度滿版
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //選取整行
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }








        private void CleearBufferRedundance(int Thread_number)
        {

            //移除sourcebuffer和buffer配置档里不含有的执行buffer
            Dictionary<string, HObject> tempImageBuffer = new Dictionary<string, HObject>();
            Dictionary<string, Object> tempControlBuffer = new Dictionary<string, Object>();
            foreach (Info_Source keybufferobject in _sourceBuffer[Thread_number]._s_ObjectBuffer.Keys)
            {
                if (!_executeBuffer[Thread_number].imageBuffer.ContainsKey(keybufferobject.Name))
                    _executeBuffer[Thread_number].imageBuffer.Add(keybufferobject.Name, _sourceBuffer[Thread_number]._s_ObjectBuffer[keybufferobject]);

                if (_executeBuffer[Thread_number].imageBuffer.ContainsKey(keybufferobject.Name))
                {
                    //_executeBuffer[Thread_number].imageBuffer.Add(keybufferobject.Name, _sourceBuffer[Thread_number]._s_ObjectBuffer[keybufferobject]);
                    _executeBuffer[Thread_number].imageBuffer[keybufferobject.Name] = _sourceBuffer[Thread_number]._s_ObjectBuffer[keybufferobject];
                }


            }
            foreach (Info_Source keybuffecontrol in _sourceBuffer[Thread_number]._s_ControlBuffer .Keys)
            {
                if (!_executeBuffer[Thread_number].controlBuffer.ContainsKey(keybuffecontrol.Name))
                    _executeBuffer[Thread_number].controlBuffer.Add(keybuffecontrol.Name, _sourceBuffer[Thread_number]._s_ControlBuffer[keybuffecontrol]);
                if (_executeBuffer[Thread_number].controlBuffer.ContainsKey(keybuffecontrol.Name))
                    _executeBuffer[Thread_number].controlBuffer[keybuffecontrol.Name] = _sourceBuffer[Thread_number]._s_ControlBuffer[keybuffecontrol];

            }

        }




        private void Clear_all_buffer(int row, int Thread_number)
        {
            List<string> buffer_remove = new List<string>();
            buffer_remove.Clear();
            bool should_move = true;
            for (int i = row; i < ListTool[comboBox1_SelectedIndex].Count - 1; i++)
            {
                //_executeBuffer[Thread_number].all_test_buffer[i] = _executeBuffer[Thread_number].all_test_buffer[i + 1];



                _executeBuffer[Thread_number].all_test_buffer[i].imagebuffer.Clear();
                _executeBuffer[Thread_number].all_test_buffer[i].controlbuffer.Clear();

                
                    for (int m = 0; m < _executeBuffer[Thread_number].all_test_buffer[i + 1].imagebuffer.Count(); m++)
                    {
                        _executeBuffer[Thread_number].all_test_buffer[i].imagebuffer.Add(_executeBuffer[Thread_number].all_test_buffer[i + 1].imagebuffer[m]);
                    }
                    for (int n = 0; n < _executeBuffer[Thread_number].all_test_buffer[i + 1].controlbuffer.Count(); n++)
                    {
                        _executeBuffer[Thread_number].all_test_buffer[i].controlbuffer.Add(_executeBuffer[Thread_number].all_test_buffer[i + 1].controlbuffer[n]);
                    }
                


            }
            _executeBuffer[Thread_number].all_test_buffer.Remove(ListTool[comboBox1_SelectedIndex].Count - 1);
            foreach (string keyc in _executeBuffer[Thread_number].imageBuffer.Keys)
            {
                should_move = true;

                foreach (int keyr in _executeBuffer[Thread_number].all_test_buffer.Keys)
                {
                    for (int mnumber = 0; mnumber < _executeBuffer[Thread_number].all_test_buffer[keyr].imagebuffer.Count; mnumber++)
                    {
                        if (keyc == _executeBuffer[Thread_number].all_test_buffer[keyr].imagebuffer[mnumber])
                        {
                            should_move = false;
                            break;
                        }
                        
                    }

                    foreach (Info_Source keysource in _sourceBuffer[Thread_number]._s_ObjectBuffer.Keys)
                    {
                        if (keysource.Name == keyc)
                        {
                            should_move = false;
                            break;
                        }
                    }


                    if (!should_move)
                        break;


                }

                if (should_move)
                    buffer_remove.Add(keyc);


         }
            for (int i = 0; i < buffer_remove.Count; i++)
                _executeBuffer[Thread_number].imageBuffer.Remove(buffer_remove[i]);

        }





        private void 檔案路徑創建資料夾()
        {
            for (int i = 0; i < File_solution.Count; i++)
            {
                N_Path.Add(CommonData.Path + @"\ToolProcess\Thread_" + i.ToString().PadLeft(3,'0') + File_solution[i]);

                if (Directory.Exists(N_Path[i]))
                {
                    //資料夾存在
                }
                else
                {
                    //新增資料夾
                    Directory.CreateDirectory(N_Path[i]);
                }
                /*
                if (Directory.Exists(N_Path[i] + @"\Out"))
                {
                    //資料夾存在
                }
                else
                {
                    //新增資料夾
                    Directory.CreateDirectory(N_Path[i] + @"\Out");
                }
                */
            }
        }



        private void ReCreate(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                String Solution_Path = dir + @"\Solution.ini";
                IniFile IniFile_solution = new IniFile(Solution_Path);
                IniFile_solution.IniWriteValue("Main", "All_Solution_Number", File_solution.Count.ToString());
                for (int S_number = 0; S_number < File_solution.Count; S_number++)
                    IniFile_solution.IniWriteValue("Main", S_number.ToString(), File_solution[S_number]);
            }
        }

        private void DeleteFolder(string dir)
        {




            foreach (string d in Directory.GetFileSystemEntries(dir))
            {
                if (File.Exists(d))
                {
                    try
                    {
                        FileInfo fi = new FileInfo(d);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                            fi.Attributes = FileAttributes.Normal;
                        File.Delete(d);//直接删除其中的文件 
                    }
                    catch
                    {

                    }
                }
                else
                {
                    try
                    {
                        DirectoryInfo d1 = new DirectoryInfo(d);
                        if (d1.GetFiles().Length != 0)
                        {
                            DeleteFolder(d1.FullName);////递归删除子文件夹
                        }
                        Directory.Delete(d);
                    }
                    catch
                    {

                    }
                }
            }
          //  this.labmsg.Text = "删除成功!时间：" + DateTime.Now.ToString();
        }

        private void 檔案路徑創建資料夾(String Dir_path)
        {

 

                if (Directory.Exists(Dir_path))
                {
                    //資料夾存在
                }
                else
                {
                    //新增資料夾
                    Directory.CreateDirectory(Dir_path);
                }
            /*
                if (Directory.Exists(Dir_path + @"\Out"))
                {
                    //資料夾存在
                }
                else
                {
                    //新增資料夾
                    Directory.CreateDirectory(Dir_path + @"\Out");
                }
            */
        }
        private void 資料表初始化()
        {

            //內文置中
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //標題寬度滿版
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //選取整行
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


        }



        /// <summary>
        /// 读取配置文档，更新流程
        /// </summary>
        /// <param name="path">配置文件的位置编号</param>

        private void Buffer_initial()
        {
            _sourceBuffer = new List<SourceBuffer>();
            _executeBuffer = new List<ExecuteBuffer>();

        }


        private bool LoadSourceFile(Info_Source infoget, int Thread_number)
        { 
               
            HObject obj;
            HOperatorSet.GenEmptyObj(out obj);
            HTuple ModelID;
            HTuple tuple_b;
            if (infoget.Type == "region")
            {
                try
                {
                    HOperatorSet.ReadRegion(out obj, infoget.Path);
                }
                catch (Exception)
                {
                    MessageBox.Show("read region fail");
                    return false;
                }

                foreach (Info_Source keybuffercontrol in _sourceBuffer[Thread_number]._s_ObjectBuffer.Keys)
                {
                    if (keybuffercontrol.Name == infoget.Name)
                    {
                        MessageBox.Show("read region fail,buffer中已存在同名region");
                        return false;
                    }
                }

                _sourceBuffer[Thread_number]._s_ObjectBuffer.Add(infoget, obj);

            }
            else if (infoget.Type == "shapemode_contour")
            {
                try
                {
                    HOperatorSet.ReadShapeModel(infoget.Path, out ModelID);
                }
                catch (Exception)
                {
                    MessageBox.Show("Read ShapeModel fail");
                    return false;
                }

                foreach (Info_Source keybuffermodel in _sourceBuffer[Thread_number]._s_ControlBuffer.Keys)
                {
                    if (keybuffermodel.Name == infoget.Name)
                    {
                        MessageBox.Show("已存在同名modelid");
                        return false;
                    }
                }

                _sourceBuffer[Thread_number]._s_ControlBuffer.Add(infoget, ModelID);
            }
            else if (infoget.Type == "shapemode_grey")
            {
                try
                {
                 //   HOperatorSet.ReadShapeModel(infoget.Path, out ModelID);
                    HOperatorSet.ReadTemplate(infoget.Path, out ModelID);
                }
                catch (Exception)
                {
                    MessageBox.Show("Read ShapeModel fail");
                    return false;
                }

                foreach (Info_Source keybuffermodel in _sourceBuffer[Thread_number]._s_ControlBuffer.Keys)
                {
                    if (keybuffermodel.Name == infoget.Name)
                    {
                        MessageBox.Show("已存在同名modelid");
                        return false;
                    }
                }

                _sourceBuffer[Thread_number]._s_ControlBuffer.Add(infoget, ModelID);
            }
            else if (infoget.Type == "tuple")
            {
                try
                {
                    HOperatorSet.ReadTuple(infoget.Path, out tuple_b);
                }
                catch (Exception)
                {
                    MessageBox.Show("Read tuple fail");
                    return false;
                }

                foreach (Info_Source keybuffermodel in _sourceBuffer[Thread_number]._s_ControlBuffer.Keys)
                {
                    if (keybuffermodel.Name == infoget.Name)
                    {
                        MessageBox.Show("已存在同名tuple");
                        return false;
                    }
                }

                _sourceBuffer[Thread_number]._s_ControlBuffer.Add(infoget, tuple_b);
            }



            else
                MessageBox.Show("now hobject is not region");

            return true;
        }


        private void ReadBufferFile(string path, int i)
        {
            string _sourcePath = path + "\\" + "source.ini";
            if (!File.Exists(_sourcePath))
            {
                //不存在配置文件
                _sourceIni = new INIOperation(_sourcePath);

            }
            else
            {
                _sourceIni = new INIOperation(_sourcePath);
                List<string> sources = new List<string>();
                sources = _sourceIni.ReadSections();
                for (int mnumber = 0; mnumber < sources.Count; mnumber++)
                {
                    Info_Source buffer_get = new Info_Source();
                    buffer_get.Path = _sourceIni.IniReadValue(sources[mnumber], "path");
                    buffer_get.Type = _sourceIni.IniReadValue(sources[mnumber], "type");
                    buffer_get.Name = sources[mnumber];
                    LoadSourceFile(buffer_get, i);

                }



            }
        }


        private void Buffer_reset()
        {

            _executeBuffer.Clear();


        }


   



 


        private INIOperation _sourceIni;
        private void SaveBufferFile(string path, int i)
        {
            string _sourcePath = path  + "\\" + "source.ini";
            if (!File.Exists(_sourcePath))
            {
                //不存在配置文件
                _sourceIni = new INIOperation(_sourcePath);

            }
            else
            {
                File.Delete(_sourcePath);
                _sourceIni = new INIOperation(_sourcePath);
            }
                foreach (Info_Source keybufferobject in _sourceBuffer[i]._s_ObjectBuffer.Keys)
                {
                    _sourceIni.IniWrite(keybufferobject.Name, "type", keybufferobject.Type);
                    _sourceIni.IniWrite(keybufferobject.Name, "path", keybufferobject.Path);

                }
                foreach (Info_Source keybuffercontrol in _sourceBuffer[i]._s_ControlBuffer.Keys)
                {
                    _sourceIni.IniWrite(keybuffercontrol.Name, "type", keybuffercontrol.Type);
                    _sourceIni.IniWrite(keybuffercontrol.Name, "path", keybuffercontrol.Path);

                }
            
         }







        private void save_imge()
        {
                    SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "BMP图像|*.bmp|所有文件|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (String.IsNullOrEmpty(sfd.FileName))
                {
                    return;
                }
                if (hv_image != null)
                    HOperatorSet.WriteImage(hv_image, "bmp", 0, sfd.FileName);
                else
                    MessageBox.Show("请确认图像是否存在");
            }
        }




        private void hWindowControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (!Line_find && !Circle_find)
                    contextMenuStrip2.Show(Cursor.Position);
            }
        }







        private void 保存当前ROIToolStripMenuItem1_action ()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "ROI保存结尾.hobj";
            sfd.RestoreDirectory = true;
            ROI roi = roiController.getActiveROI();

            if (roi == null)
            {
                MessageBox.Show("未选中任何ROI,请选择ROI");
                return;
            }
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string strPath = sfd.FileName;
                if (!strPath.EndsWith(".hobj"))
                {
                    strPath += ".hobj";
                }

                HOperatorSet.WriteRegion(roi.getRegion(), strPath);
                
                Match match = Regex.Match(strPath, "(\\w+)(.hobj)$");
                
                int i = comboBox1_SelectedIndex;
                Info_Source new_info = new Info_Source();
                new_info.Type = "region";
                new_info.Name = match.Groups[1].ToString()+".region";
                new_info.Path = strPath;
                _sourceBuffer[i]._s_ObjectBuffer.Add(new_info, roi.getRegion());

            }
        }


        private void hWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {
          //  double x_in = Math.Round((double)(e.X / hWndCtrl.hv_scale), 3);
            //double y_in = Math.Round((double)(e.Y / hWndCtrl.hv_scale), 3);
            label_location.Text = "(" + string.Format("{0:####.##}", e.X) + "," + string.Format("{0:####.##}", e.Y) + ")";
        }




        /// <summary>
        /// 显示子控件在软件主界面上
        /// </summary>
        private void Showwindow()
        {
            if (this.dataGridView1.CurrentRow != null)
            {

                int s;
                HObject bitmap_out;
                HOperatorSet.GenEmptyObj(out bitmap_out);
                string formName;

                s = this.dataGridView1.CurrentRow.Index; //取得選取位置

                formName = ListTool[comboBox1_SelectedIndex][s].GetType().Name; //取得form類型

                if (Panel_zhedie)
                {
                    splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
                    Panel_zhedie = false;
                }
                switch (formName)
                {
                    case "ReadPictureControl":

                        ReadPictureControl F_readpicturecontrol = (ReadPictureControl)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        F_readpicturecontrol.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_readpicturecontrol.TopLevel = false;
                        F_readpicturecontrol.Dock = DockStyle.Fill;//把子窗体设置为控件
                        F_readpicturecontrol.FormBorderStyle = FormBorderStyle.None;
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(F_readpicturecontrol);
                        // PanelRightMain.Controls.Add(sub);
                        F_readpicturecontrol.Show();

                            if (!Camera_onepicture_Process(out bitmap_out))
                            {
                                listBox_info.Items.Add("取得图像： 相机取图失败");

                                return;
                            }


                            _executeBuffer[comboBox1_SelectedIndex].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"] = bitmap_out;
                            Display_Graphic(_executeBuffer[comboBox1_SelectedIndex].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"]);

                        
                        break;

                    case "BinaryPro":

                        BinaryPro F_binarypro = (BinaryPro)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        F_binarypro.TopLevel = false;
                        F_binarypro.Dock = DockStyle.Fill;//把子窗体设置为控件
                        F_binarypro.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        F_binarypro.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_binarypro.FormBorderStyle = FormBorderStyle.None;
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(F_binarypro);
                        F_binarypro.Show();

                        break;
                    case "FindScaledShapeModel":
                        FindScaledShapeModel F_findscaledshapemodel = (FindScaledShapeModel)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        F_findscaledshapemodel.TopLevel = false;
                        F_findscaledshapemodel.Dock = DockStyle.Fill;//把子窗体设置为控件
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_findscaledshapemodel.FormBorderStyle = FormBorderStyle.None;
                        F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(F_findscaledshapemodel);
                        F_findscaledshapemodel.Show();
                        break;
                    case "FindGreyModel":
                        FindGreyModel F_findgreymodel = (FindGreyModel)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        F_findgreymodel.TopLevel = false;
                        F_findgreymodel.Dock = DockStyle.Fill;//把子窗体设置为控件
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_findgreymodel.FormBorderStyle = FormBorderStyle.None;
                        F_findgreymodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_findgreymodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(F_findgreymodel);
                        F_findgreymodel.Show();
                        break;
                    case "NinePointCal":
                        NinePointCal S_ninepointcal = (NinePointCal)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_ninepointcal.TopLevel = false;
                        S_ninepointcal.Dock = DockStyle.Fill;//把子窗体设置为控件
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_ninepointcal.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_ninepointcal);
                        S_ninepointcal.Show();
                        break;
                    case "MakeCalibration":
                        MakeCalibration S_makecalibration = (MakeCalibration)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_makecalibration.TopLevel = false;
                        S_makecalibration.Dock = DockStyle.Fill;//把子窗体设置为控件
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_makecalibration.FormBorderStyle = FormBorderStyle.None;
                        S_makecalibration.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_makecalibration.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_makecalibration);
                        S_makecalibration.Show();
                        break;
                    case "PuzzlePicture":
                        PuzzlePicture S_puzzlepicture = (PuzzlePicture)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_puzzlepicture.TopLevel = false;
                        S_puzzlepicture.Dock = DockStyle.Fill;//把子窗体设置为控件
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_puzzlepicture.FormBorderStyle = FormBorderStyle.None;
                        S_puzzlepicture.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_puzzlepicture);
                        S_puzzlepicture.Show();
                        break;
                    case "RoiMake":
                        RoiMake S_roimak = (RoiMake)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_roimak.TopLevel = false;
                        S_roimak.Dock = DockStyle.Fill;//把子窗体设置为控件
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        S_roimak.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_roimak.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_roimak);
                        S_roimak.Show();
                        break;
                    case "GeneralMorphology":
                        GeneralMorphology S_generalmorphology = (GeneralMorphology)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_generalmorphology.TopLevel = false;
                        S_generalmorphology.Dock = DockStyle.Fill;//把子窗体设置为控件
                        S_generalmorphology.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        S_generalmorphology.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_generalmorphology.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_generalmorphology);
                        S_generalmorphology.Show();
                        break;
                    case "SmoothTest":
                        SmoothTest S_smoothtest = (SmoothTest)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_smoothtest.TopLevel = false;
                        S_smoothtest.Dock = DockStyle.Fill;//把子窗体设置为控件
                        S_smoothtest.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        S_smoothtest.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_smoothtest.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_smoothtest);
                        S_smoothtest.Show();
                        break;
                    case "RegionProcess":
                        RegionProcess S_regionprocess = (RegionProcess)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_regionprocess.TopLevel = false;
                        S_regionprocess.Dock = DockStyle.Fill;//把子窗体设置为控件
                        S_regionprocess.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_regionprocess.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_regionprocess);
                        S_regionprocess.Show();
                        break;
                    case "Transformation":
                        Transformation S_ransformation = (Transformation)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_ransformation.TopLevel = false;
                        S_ransformation.Dock = DockStyle.Fill;//把子窗体设置为控件
                        //S_ransformation.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        S_ransformation.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_ransformation.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_ransformation);
                        S_ransformation.Show();
                        break;
                    case "MathCalc":
                        MathCalc S_mathcalc = (MathCalc)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_mathcalc.TopLevel = false;
                        S_mathcalc.Dock = DockStyle.Fill;//把子窗体设置为控件
                        //S_ransformation.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                      //  S_mathcalc.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_mathcalc.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_mathcalc);
                        S_mathcalc.Show();
                        break;
                    case "PointLine":
                        PointLine S_pointline = (PointLine)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_pointline.TopLevel = false;
                        S_pointline.Dock = DockStyle.Fill;//把子窗体设置为控件
                        //S_ransformation.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        S_pointline.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_pointline.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_pointline);
                        S_pointline.Show();
                        break;
                    case "LineLine":
                        LineLine S_lineline = (LineLine)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_lineline.TopLevel = false;
                        S_lineline.Dock = DockStyle.Fill;//把子窗体设置为控件
                        //S_ransformation.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        S_lineline.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_lineline.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_lineline);
                        S_lineline.Show();
                        break;
                    case "PuzzleData":
                        PuzzleData S_puzzledata = (PuzzleData)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_puzzledata.TopLevel = false;
                        S_puzzledata.Dock = DockStyle.Fill;//把子窗体设置为控件
                        //S_ransformation.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_puzzledata.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_puzzledata);
                        S_puzzledata.Show();
                        break;
                    case "FocusPoint":
                        FocusPoint S_focuspoint = (FocusPoint)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_focuspoint.TopLevel = false;
                        S_focuspoint.Dock = DockStyle.Fill;//把子窗体设置为控件
                        //S_ransformation.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        S_focuspoint.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_focuspoint.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_focuspoint);
                        S_focuspoint.Show();
                        break;
                    case "PointShow":
                        PointShow S_pointshow = (PointShow)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_pointshow.TopLevel = false;
                        S_pointshow.Dock = DockStyle.Fill;//把子窗体设置为控件
                        //S_ransformation.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        S_pointshow.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_pointshow.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_pointshow);
                        S_pointshow.Show();
                        break;
                    case "TestResultsShow":
                        TestResultsShow S_testresultsshow = (TestResultsShow)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_testresultsshow.TopLevel = false;
                        S_testresultsshow.Dock = DockStyle.Fill;//把子窗体设置为控件
                        //S_ransformation.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                       // S_testresultsshow.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_testresultsshow.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_testresultsshow);
                        S_testresultsshow.Show();
                        break;
                    case "RegionInterest":
                        RegionInterest S_regioninterest = (RegionInterest)ListTool[comboBox1_SelectedIndex][s]; //轉型
                        S_regioninterest.TopLevel = false;
                        S_regioninterest.Dock = DockStyle.Fill;//把子窗体设置为控件
                        //S_ransformation.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        S_regioninterest.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaRegion(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        S_regioninterest.FormBorderStyle = FormBorderStyle.None;
                        //F_findscaledshapemodel.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        // F_findscaledshapemodel.SetParawindow(_executeBuffer[comboBox1_SelectedIndex]);
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(S_regioninterest);
                        S_regioninterest.Show();
                        break;
                    case "SaveShapeTem":

                        SaveShapeTem F_saveshapetem = (SaveShapeTem)ListTool[comboBox1_SelectedIndex][s]; //轉型

                        //F_邊緣.Data_Read();

                        F_saveshapetem.TopLevel = false;
                        F_saveshapetem.Dock = DockStyle.Fill;//把子窗体设置为控件
                        F_saveshapetem.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_saveshapetem.FormBorderStyle = FormBorderStyle.None;
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(F_saveshapetem);
                        // PanelRightMain.Controls.Add(sub);
                        F_saveshapetem.Show();
                        break;
                    case "SaveGreyTem":

                        SaveGreyTem F_savegreytem = (SaveGreyTem)ListTool[comboBox1_SelectedIndex][s]; //轉型

                        //F_邊緣.Data_Read();

                        F_savegreytem.TopLevel = false;
                        F_savegreytem.Dock = DockStyle.Fill;//把子窗体设置为控件
                        F_savegreytem.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_savegreytem.FormBorderStyle = FormBorderStyle.None;
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(F_savegreytem);
                        // PanelRightMain.Controls.Add(sub);
                        F_savegreytem.Show();
                        break;
                    case "FindLine":

                        FindLine F_findline= (FindLine)ListTool[comboBox1_SelectedIndex][s]; //轉型

                        //F_邊緣.Data_Read();

                        F_findline.TopLevel = false;
                        F_findline.Dock = DockStyle.Fill;//把子窗体设置为控件
                        F_findline.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_findline.FormBorderStyle = FormBorderStyle.None;
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(F_findline);
                        // PanelRightMain.Controls.Add(sub);
                        F_findline.Show();
                        break;
                    case "FindCircle":

                        FindCircle F_findcircle = (FindCircle)ListTool[comboBox1_SelectedIndex][s]; //轉型

                        //F_邊緣.Data_Read();

                        F_findcircle.TopLevel = false;
                        F_findcircle.Dock = DockStyle.Fill;//把子窗体设置为控件
                        F_findcircle.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_findcircle.FormBorderStyle = FormBorderStyle.None;
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(F_findcircle);
                        // PanelRightMain.Controls.Add(sub);
                        F_findcircle.Show();
                        break;
                    case "ReadQRcode":

                        ReadQRcode F_readqrcode = (ReadQRcode)ListTool[comboBox1_SelectedIndex][s]; //轉型

                        //F_邊緣.Data_Read();

                        F_readqrcode.TopLevel = false;
                        F_readqrcode.Dock = DockStyle.Fill;//把子窗体设置为控件
                        F_readqrcode.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_readqrcode.FormBorderStyle = FormBorderStyle.None;
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(F_readqrcode);
                        // PanelRightMain.Controls.Add(sub);
                        F_readqrcode.Show();
                        break;
                    case "ReadBarcode":

                        ReadBarcode F_readbarcode = (ReadBarcode)ListTool[comboBox1_SelectedIndex][s]; //轉型

                        //F_邊緣.Data_Read();

                        F_readbarcode.TopLevel = false;
                        F_readbarcode.Dock = DockStyle.Fill;//把子窗体设置为控件
                        F_readbarcode.SetParaImage(_executeBuffer[comboBox1_SelectedIndex]);
                        F_readbarcode.FormBorderStyle = FormBorderStyle.None;
                        this.splitContainer1.Panel1.Controls.Clear();
                        this.splitContainer1.Panel1.Controls.Add(button1);
                        this.splitContainer1.Panel1.Controls.Add(F_readbarcode);
                        // PanelRightMain.Controls.Add(sub);
                        F_readbarcode.Show();
                        break;

                }
            }

        }




        /// <summary>
        /// 删除单个工具
        /// </summary>
        /// <param name="row">删除工具所在的位置编号</param>
        private void ListToolDelet(int row)
        {


            dataGridView1.DataSource = null;
            List<DataGVName> ListDataGV1 = new List<DataGVName>();

            ListDataGV.RemoveAt(row);
            for (int m = 0; m < ListDataGV.Count; m++)
            {
                ListDataGV1.Insert(m, ListDataGV[m]);
            }
            ListTool[comboBox1_SelectedIndex].RemoveAt(row);

            //連結資料表
            dataGridView1.DataSource = ListDataGV1;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = 0;
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;//选定一行
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];//每次只选定一行
                contextMenuStrip1.Show(dataGridView1, e.Location);//右键列表显示在datagridview控件上
                contextMenuStrip1.Show(Cursor.Position);//右键快捷列表显示在鼠标停留位置
                index = e.RowIndex;

            }


        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListTool.Count == 0)
            {
                return;
            }

            int row = this.dataGridView1.CurrentRow.Index;
            int Thread_number = comboBox1_SelectedIndex;
            String formName = ListTool[Thread_number][row].GetType().Name; //取得form類型
            List<string> buffer_remove = new List<string>();
            buffer_remove.Clear();
            switch (formName)
            {
                case "ReadPictureControl":

                    ReadPictureControl F_readpicturecontrol = (ReadPictureControl)ListTool[comboBox1_SelectedIndex][row];
                    string output_readpicturebuffer = F_readpicturecontrol.get_imagebuf();
                    F_readpicturecontrol.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "BinaryPro":
                    BinaryPro F_binarypro = (BinaryPro)ListTool[comboBox1_SelectedIndex][row];
                    F_binarypro.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "FindScaledShapeModel":
                    FindScaledShapeModel D_findscaleshapemodel = (FindScaledShapeModel)ListTool[comboBox1_SelectedIndex][row];
                    D_findscaleshapemodel.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "SaveShapeTem":
                    SaveShapeTem D_saveshapetem = (SaveShapeTem)ListTool[comboBox1_SelectedIndex][row];
                    D_saveshapetem.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "SaveGreyTem":
                    SaveGreyTem D_savegreytem = (SaveGreyTem)ListTool[comboBox1_SelectedIndex][row];
                    D_savegreytem.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "FindGreyModel":
                    FindGreyModel D_findgreymodel = (FindGreyModel)ListTool[comboBox1_SelectedIndex][row];
                    D_findgreymodel.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "GeneralMorphology":
                    GeneralMorphology D_generalmorphology = (GeneralMorphology)ListTool[comboBox1_SelectedIndex][row];
                    D_generalmorphology.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "RoiMake":
                    RoiMake D_roimake = (RoiMake)ListTool[comboBox1_SelectedIndex][row];
                    D_roimake.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "SmoothTest":
                    SmoothTest D_smoothtest = (SmoothTest)ListTool[comboBox1_SelectedIndex][row];
                    D_smoothtest.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "RegionProcess":
                    RegionProcess D_regionprocess = (RegionProcess)ListTool[comboBox1_SelectedIndex][row];
                    D_regionprocess.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "Transformation":
                    Transformation D_transformation = (Transformation)ListTool[comboBox1_SelectedIndex][row];
                    D_transformation.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "MathCalc":
                    MathCalc D_mathcalc = (MathCalc)ListTool[comboBox1_SelectedIndex][row];
                    D_mathcalc.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "PointLine":
                    PointLine D_pointline = (PointLine)ListTool[comboBox1_SelectedIndex][row];
                    D_pointline.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "LineLine":
                    LineLine D_lineline = (LineLine)ListTool[comboBox1_SelectedIndex][row];
                    D_lineline.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "RegionInterest":
                    RegionInterest D_regioninterest = (RegionInterest)ListTool[comboBox1_SelectedIndex][row];
                    D_regioninterest.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "PointShow":
                    PointShow D_pointshow = (PointShow)ListTool[comboBox1_SelectedIndex][row];
                    D_pointshow.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "TestResultsShow":
                    TestResultsShow D_testresultsshow = (TestResultsShow)ListTool[comboBox1_SelectedIndex][row];
                    D_testresultsshow.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "NinePointCal":
                    NinePointCal D_ninepointcal = (NinePointCal)ListTool[comboBox1_SelectedIndex][row];
                    D_ninepointcal.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "MakeCalibration":
                    MakeCalibration D_makecalibration = (MakeCalibration)ListTool[comboBox1_SelectedIndex][row];
                    D_makecalibration.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "PuzzlePicture":
                    PuzzlePicture D_puzzlepicture = (PuzzlePicture)ListTool[comboBox1_SelectedIndex][row];
                    D_puzzlepicture.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "PuzzleData":
                    PuzzleData D_puzzledata = (PuzzleData)ListTool[comboBox1_SelectedIndex][row];
                    D_puzzledata.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "FocusPoint":
                    FocusPoint D_focuspoint = (FocusPoint)ListTool[comboBox1_SelectedIndex][row];
                    D_focuspoint.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "FindLine":
                    FindLine D_findline = (FindLine)ListTool[comboBox1_SelectedIndex][row];
                    D_findline.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "FindCircle":
                    FindCircle D_findcircle = (FindCircle)ListTool[comboBox1_SelectedIndex][row];
                    D_findcircle.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "ReadQRcode":
                    ReadQRcode D_readqrcode = (ReadQRcode)ListTool[comboBox1_SelectedIndex][row];
                    D_readqrcode.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                case "ReadBarcode":
                    ReadBarcode D_readbarcode = (ReadBarcode)ListTool[comboBox1_SelectedIndex][row];
                    D_readbarcode.Close();
                    Clear_all_buffer(row, Thread_number);
                    break;
                default:
                    break;

            }
            ListToolDelet(row);
        }




        private void 讀取(string path)
        {


            String Solution_Path = path + @"\ToolProcess\Solution.ini";
            if (!File.Exists(Solution_Path))
            {
                // MessageBox.Show("配置文档为空，请先保存再读取");
                return;
            }
            File_solution.Clear();
            ListTool.Clear();
            comboBox1.Items.Clear();
            _executeBuffer.Clear();
            _sourceBuffer.Clear();

            IniFile IniFile_solution = new IniFile(Solution_Path);

            int Solution_number = Convert.ToInt32(IniFile_solution.IniReadValue("Main", "All_Solution_Number"));
            // MessageBox.Show(comboBox1.SelectedIndex.ToString());
            for (int m = 0; m < Solution_number; m++)
            {
                String Solution_name = IniFile_solution.IniReadValue("Main", m.ToString());
                File_solution.Add(Solution_name);
                ListTool.Add(new List<Form>());
                comboBox1.Items.Add(Solution_name);
                _executeBuffer.Add(new ExecuteBuffer());
                _sourceBuffer.Add(new SourceBuffer());

            }
            comboBox1_SelectedIndex = 0;
            List<string> N_Path = new List<string>();




            //  MessageBox.Show(comboBox1.SelectedIndex.ToString());

            for (int i = 0; i < ListTool.Count; i++)
            {

                _sourceBuffer[i]._s_ObjectBuffer = new Dictionary<Info_Source, HObject>();
                _sourceBuffer[i]._s_ControlBuffer = new Dictionary<Info_Source, object>();
                _executeBuffer[i].controlBuffer = new Dictionary<string, object>();
                _executeBuffer[i].imageBuffer = new Dictionary<string, HObject>();
                _executeBuffer[i].all_test_buffer = new Dictionary<int, All_buffer>();


                N_Path.Clear();
                N_Path.Add(path + @"\ToolProcess\Thread_" + i.ToString().PadLeft(3, '0') + File_solution[i]);
                N_Path.Add(@"\Setting.ini");




                IniFile IniFile = new IniFile(N_Path[0] + N_Path[1]);
                ReadBufferFile(N_Path[0], i);
                CleearBufferRedundance(i);
                if (File.Exists(N_Path[0] + N_Path[1]))
                {


                    int N_All_Tool_Number;
                    int.TryParse((IniFile.IniReadValue("Main", "All_Tool_Number")), out  N_All_Tool_Number);

                    ListTool[i].Clear();
                    //MessageBox.Show("now number is " + ListTool[i].Count);
                    for (int j = 0; j < N_All_Tool_Number; j++)
                    {
                        string N_Tool_Name = IniFile.IniReadValue(j.ToString(), "Tool_Name");


                        switch (N_Tool_Name)
                        {
                            case "ReadPictureControl":

                                ReadPictureControl readpicturecontrol2 = new ReadPictureControl(_executeBuffer[i], false);
                                readpicturecontrol2.ReadData(N_Path, j);
                                ListTool[i].Add(readpicturecontrol2);
                                String Imagebuffer_readimage = readpicturecontrol2.ReadData_buf(N_Path, j).Imagebuffer_val;
                                //  String Controlbuffer_readimage = readpicturecontrol2.ReadData_buf(N_Path, j).Controlbuffer_val;
                                All_buffer readpicture = new All_buffer();
                                readpicture.imagebuffer.Add(Imagebuffer_readimage);
                                //   readpicture.controlbuffer.Add(Controlbuffer_readimage);

                                if (_executeBuffer[i].imageBuffer.ContainsKey(Imagebuffer_readimage))
                                {
                                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                                }
                                else
                                {
                                    _executeBuffer[i].imageBuffer.Add(Imagebuffer_readimage, null);

                                }
                                _executeBuffer[i].all_test_buffer.Add(j, readpicture);

                                break;


                            case "BinaryPro":
                                BinaryPro Binarypro1 = new BinaryPro(_executeBuffer[i], false);
                                Binarypro1.ReadData(N_Path, j);
                                ListTool[i].Add(Binarypro1);


                                string regionout = Binarypro1.get_Region();
                                string imageout = Binarypro1.get_Imageout();
                                All_buffer Binaryprobuffer = new All_buffer();

                                Binaryprobuffer.imagebuffer.Add(regionout);
                                Binaryprobuffer.imagebuffer.Add(imageout);
                                if (_executeBuffer[i].imageBuffer.ContainsKey(regionout))
                                {
                                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                                }
                                else
                                {
                                    _executeBuffer[i].imageBuffer.Add(regionout, null);

                                }
                                if (_executeBuffer[i].imageBuffer.ContainsKey(imageout))
                                {
                                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                                }
                                else
                                {
                                    _executeBuffer[i].imageBuffer.Add(imageout, null);

                                }
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, Binaryprobuffer);

                                break;

                            case "FindScaledShapeModel":
                                FindScaledShapeModel Findscaledshapemodel1 = new FindScaledShapeModel(_executeBuffer[i], false);
                                Findscaledshapemodel1.ReadData(N_Path, j);
                                ListTool[i].Add(Findscaledshapemodel1);
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;

                            case "SaveShapeTem":
                                SaveShapeTem SaveShapeTem1 = new SaveShapeTem(_executeBuffer[i], false);
                                SaveShapeTem1.ReadData(N_Path, j, roiController);
                                ListTool[i].Add(SaveShapeTem1);
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                sou_regions2 = SaveShapeTem1.sou_regions;
                                regions2 = SaveShapeTem1.regions;
                                break;
                            case "SaveGreyTem":
                                SaveGreyTem SaveGreyTem1 = new SaveGreyTem(_executeBuffer[i], false);
                                SaveGreyTem1.ReadData(N_Path, j, roiController);
                                ListTool[i].Add(SaveGreyTem1);
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                sou_regions1 = SaveGreyTem1.sou_regions;
                                regions1 = SaveGreyTem1.regions;
                                break;
                            case "FindGreyModel":
                                FindGreyModel Findgreymodel1 = new FindGreyModel(_executeBuffer[i], false);
                                Findgreymodel1.ReadData(N_Path, j);
                                ListTool[i].Add(Findgreymodel1);
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "RoiMake":
                                RoiMake Roimake1 = new RoiMake(_executeBuffer[i], false);
                                Roimake1.ReadData(N_Path, j, roiController);
                                ListTool[i].Add(Roimake1);
                                string Roishapename = Roimake1.getshapename();
                                All_buffer roimakebuffer = new All_buffer();
                                roimakebuffer.imagebuffer.Add(Roishapename);
                                sou_regions_interest = Roimake1.sou_regions;
                                if (_executeBuffer[i].imageBuffer.ContainsKey(Roishapename))
                                {
                                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                                }
                                else
                                {
                                    _executeBuffer[i].imageBuffer.Add(Roishapename, null);

                                }
                                _executeBuffer[i].all_test_buffer.Add(j, roimakebuffer);

                                break;
                            case "GeneralMorphology":
                                GeneralMorphology Generalmorphology1 = new GeneralMorphology(_executeBuffer[i], false);
                                Generalmorphology1.ReadData(N_Path, j);
                                ListTool[i].Add(Generalmorphology1);

                                string morregionout = Generalmorphology1.getRegionoutname();
                                All_buffer Morbuffer = new All_buffer();
                                Morbuffer.imagebuffer.Add(morregionout);

                                if (_executeBuffer[i].imageBuffer.ContainsKey(morregionout))
                                {
                                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                                }
                                else
                                {
                                    _executeBuffer[i].imageBuffer.Add(morregionout, null);

                                }
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, Morbuffer);

                                break;
                            case "SmoothTest":
                                SmoothTest Smoothtest1 = new SmoothTest(_executeBuffer[i], false);
                                Smoothtest1.ReadData(N_Path, j);
                                ListTool[i].Add(Smoothtest1);

                                string smoothregionout = Smoothtest1.get_Region();
                                All_buffer smoothbuffer = new All_buffer();
                                smoothbuffer.imagebuffer.Add(smoothregionout);

                                if (_executeBuffer[i].imageBuffer.ContainsKey(smoothregionout))
                                {
                                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                                }
                                else
                                {
                                    _executeBuffer[i].imageBuffer.Add(smoothregionout, null);

                                }
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, smoothbuffer);
                                break;
                            case "RegionProcess":
                                RegionProcess Regionprocess1 = new RegionProcess(_executeBuffer[i], false);
                                Regionprocess1.ReadData(N_Path, j);
                                ListTool[i].Add(Regionprocess1);

                                string regionprocessout = Regionprocess1.get_Regionout();
                                All_buffer regioninbuffer = new All_buffer();
                                regioninbuffer.imagebuffer.Add(regionprocessout);

                                if (_executeBuffer[i].imageBuffer.ContainsKey(regionprocessout))
                                {
                                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                                }
                                else
                                {
                                    _executeBuffer[i].imageBuffer.Add(regionprocessout, null);

                                }
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, regioninbuffer);
                                break;

                            case "Transformation":
                                Transformation Transformation1 = new Transformation(_executeBuffer[i], false);
                                Transformation1.ReadData(N_Path, j);
                                ListTool[i].Add(Transformation1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "MathCalc":
                                MathCalc Mathcalc1 = new MathCalc(_executeBuffer[i], false);
                                Mathcalc1.ReadData(N_Path, j);
                                ListTool[i].Add(Mathcalc1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "PointLine":
                                PointLine PointLine1 = new PointLine(_executeBuffer[i], false);
                                PointLine1.ReadData(N_Path, j);
                                ListTool[i].Add(PointLine1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "LineLine":
                                LineLine LineLine1 = new LineLine(_executeBuffer[i], false);
                                LineLine1.ReadData(N_Path, j);
                                ListTool[i].Add(LineLine1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "PuzzleData":
                                PuzzleData PuzzleData1 = new PuzzleData();
                                PuzzleData1.ReadData(N_Path, j);
                                ListTool[i].Add(PuzzleData1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "FocusPoint":
                                FocusPoint FocusPoint1 = new FocusPoint(_executeBuffer[i], false);
                                FocusPoint1.ReadData(N_Path, j);
                                ListTool[i].Add(FocusPoint1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "RegionInterest":
                                RegionInterest RegionInterest1 = new RegionInterest(_executeBuffer[i], false);
                                RegionInterest1.ReadData(N_Path, j);
                                ListTool[i].Add(RegionInterest1);
                                //all_test_buffer_save[i].Window_buffer = "";


                                string regioninterestout = RegionInterest1.get_Regionout();
                                All_buffer regioninterestbuffer = new All_buffer();
                                regioninterestbuffer.imagebuffer.Add(regioninterestout);

                                if (_executeBuffer[i].imageBuffer.ContainsKey(regioninterestout))
                                {
                                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                                }
                                else
                                {
                                    _executeBuffer[i].imageBuffer.Add(regioninterestout, null);

                                }
                                _executeBuffer[i].all_test_buffer.Add(j, regioninterestbuffer);
                                break;

                            case "MakeCalibration":
                                MakeCalibration MakeCalibration1 = new MakeCalibration(_executeBuffer[i], false);
                                MakeCalibration1.ReadData(N_Path, j);
                                ListTool[i].Add(MakeCalibration1);
                                //all_test_buffer_save[i].Window_buffer = "";


                                string calimageout = MakeCalibration1.get_Imageout();
                                All_buffer calimagebutffer= new All_buffer();
                                calimagebutffer.imagebuffer.Add(calimageout);

                                if (_executeBuffer[i].imageBuffer.ContainsKey(calimageout))
                                {
                                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                                }
                                else
                                {
                                    _executeBuffer[i].imageBuffer.Add(calimageout, null);

                                }
                                _executeBuffer[i].all_test_buffer.Add(j, calimagebutffer);
                                break;
                            case "PuzzlePicture":
                                PuzzlePicture PuzzlePicture1 = new PuzzlePicture(_executeBuffer[i], false);
                                PuzzlePicture1.ReadData(N_Path, j);
                                ListTool[i].Add(PuzzlePicture1);
                                //all_test_buffer_save[i].Window_buffer = "";


                                string puzzleimageout = PuzzlePicture1.get_Imageout();
                                All_buffer puzzleimagebutffer = new All_buffer();
                                puzzleimagebutffer.imagebuffer.Add(puzzleimageout);

                                if (_executeBuffer[i].imageBuffer.ContainsKey(puzzleimageout))
                                {
                                    // MessageBox.Show("资源buffer与执行buffer相冲突" + key);
                                }
                                else
                                {
                                    _executeBuffer[i].imageBuffer.Add(puzzleimageout, null);

                                }
                                _executeBuffer[i].all_test_buffer.Add(j, puzzleimagebutffer);
                                break;
                            case "PointShow":
                                PointShow Pointshow1 = new PointShow(_executeBuffer[i], false);
                                Pointshow1.ReadData(N_Path, j);
                                ListTool[i].Add(Pointshow1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "TestResultsShow":
                                TestResultsShow TestResultsShow1 = new TestResultsShow();
                                TestResultsShow1.ReadData(N_Path, j);
                                ListTool[i].Add(TestResultsShow1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "FindLine":
                                FindLine Findline1 = new FindLine(_executeBuffer[i], false);
                                Findline1.ReadData(N_Path, j);
                                ListTool[i].Add(Findline1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "FindCircle":
                                FindCircle Findcircle1 = new FindCircle(_executeBuffer[i], false);
                                Findcircle1.ReadData(N_Path, j);
                                ListTool[i].Add(Findcircle1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "ReadQRcode":
                                ReadQRcode ReadQRcode1 = new ReadQRcode(_executeBuffer[i], false);
                                ReadQRcode1.ReadData(N_Path, j);
                                ListTool[i].Add(ReadQRcode1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            case "ReadBarcode":
                                ReadBarcode ReadBarcode1 = new ReadBarcode(_executeBuffer[i], false);
                                ReadBarcode1.ReadData(N_Path, j);
                                ListTool[i].Add(ReadBarcode1);
                                //all_test_buffer_save[i].Window_buffer = "";
                                _executeBuffer[i].all_test_buffer.Add(j, new All_buffer());
                                break;
                            default:
                                break;
                        }

                    }

                }


            }
            comboBox1_SelectedIndex = 0;
            if (this.comboBox1.Items.Count > 0)
                this.comboBox1.SelectedIndex = 0;
            roiController.reset();
            hWindowControl1.HalconWindow.ClearWindow();
            流程切換(true);
        }



















        /// <summary>
        /// 储存工具流程
        /// </summary>
        /// <param name="path">指定存档的文件路径</param>

        private void 儲存(string path)
        {


            List<string> N_Path = new List<string>();
            ReCreate(path + @"\ToolProcess");
            DeleteFolder(path + @"\ToolProcess");

            String Solution_Path = path + @"\ToolProcess\Solution.ini";


            //     List<string> pixel = new List<string>();
            //     pixel = CommonData.Real_pixel;

            if (File.Exists(Solution_Path))
            {
                try
                {
                    File.Delete(Solution_Path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            IniFile IniFile_solution = new IniFile(Solution_Path);
            IniFile_solution.IniWriteValue("Main", "All_Solution_Number", File_solution.Count.ToString());
            for (int S_number = 0; S_number < File_solution.Count; S_number++)
                IniFile_solution.IniWriteValue("Main", S_number.ToString(), File_solution[S_number]);

            for (int i = 0; i < ListTool.Count; i++)
            {



                N_Path.Clear();
                N_Path.Add(path + @"\ToolProcess\Thread_" + i.ToString().PadLeft(3, '0') + File_solution[i]);

                檔案路徑創建資料夾(N_Path[0]);
                N_Path.Add(@"\Setting.ini");


                if (File.Exists(N_Path[0] + N_Path[1]))
                {
                    try
                    {
                        File.Delete(N_Path[0] + N_Path[1]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

                IniFile IniFile = new IniFile(N_Path[0] + N_Path[1]);

                // IniFile.IniWriteValue("Main", "pixel", pixel[i].ToString());
                IniFile.IniWriteValue("Main", "All_Tool_Number", ListTool[i].Count.ToString());
                SaveBufferFile(N_Path[0], i);


                for (int j = 0; j < ListTool[i].Count; j++)
                {
                    switch (ListTool[i][j].GetType().Name)
                    {
                        case "ReadPictureControl":
                            ReadPictureControl Readpicturecontrol1 = (ReadPictureControl)ListTool[i][j];
                            Readpicturecontrol1.WriteData(N_Path, j);
                            break;
                        case "BinaryPro":
                            BinaryPro BinaryPro1 = (BinaryPro)ListTool[i][j];
                            BinaryPro1.WriteData(N_Path, j);
                            break;
                        case "FindScaledShapeModel":
                            FindScaledShapeModel FindScaledShapeModel1 = (FindScaledShapeModel)ListTool[i][j];
                            FindScaledShapeModel1.WriteData(N_Path, j);
                            break;
                        case "FindGreyModel":
                            FindGreyModel FindGreyModel1 = (FindGreyModel)ListTool[i][j];
                            FindGreyModel1.WriteData(N_Path, j);
                            break;
                        case "RoiMake":
                            RoiMake Roimake1 = (RoiMake)ListTool[i][j];
                            Roimake1.WriteData(N_Path, j);
                            break;
                        case "GeneralMorphology":
                            GeneralMorphology Generalmorphology1 = (GeneralMorphology)ListTool[i][j];
                            Generalmorphology1.WriteData(N_Path, j);
                            break;
                        case "SmoothTest":
                            SmoothTest Smoothtest1 = (SmoothTest)ListTool[i][j];
                            Smoothtest1.WriteData(N_Path, j);
                            break;
                        case "RegionProcess":
                            RegionProcess Regionprocess1 = (RegionProcess)ListTool[i][j];
                            Regionprocess1.WriteData(N_Path, j);
                            break;
                        case "Transformation":
                            Transformation Transformation1 = (Transformation)ListTool[i][j];
                            Transformation1.WriteData(N_Path, j);
                            break;
                        case "MathCalc":
                            MathCalc MathCalc1 = (MathCalc)ListTool[i][j];
                            MathCalc1.WriteData(N_Path, j);
                            break;
                        case "PointLine":
                            PointLine PointLine1 = (PointLine)ListTool[i][j];
                            PointLine1.WriteData(N_Path, j);
                            break;
                        case "LineLine":
                            LineLine LineLine1 = (LineLine)ListTool[i][j];
                            LineLine1.WriteData(N_Path, j);
                            break;
                        case "RegionInterest":
                            RegionInterest RegionInterest1 = (RegionInterest)ListTool[i][j];
                            RegionInterest1.WriteData(N_Path, j);
                            break;
                        case "ReadQRcode":
                            ReadQRcode ReadQRcode1 = (ReadQRcode)ListTool[i][j];
                            ReadQRcode1.WriteData(N_Path, j);
                            break;
                        case "ReadBarcode":
                            ReadBarcode ReadBarcode1 = (ReadBarcode)ListTool[i][j];
                            ReadBarcode1.WriteData(N_Path, j);
                            break;
                        case "PointShow":
                            PointShow PointShow1 = (PointShow)ListTool[i][j];
                            PointShow1.WriteData(N_Path, j);
                            break;
                        case "TestResultsShow":
                            TestResultsShow TestResultsShow1 = (TestResultsShow)ListTool[i][j];
                            TestResultsShow1.WriteData(N_Path, j);
                            break;
                        case "SaveShapeTem":
                            SaveShapeTem SaveShapeTem1 = (SaveShapeTem)ListTool[i][j];
                            SaveShapeTem1.WriteData(N_Path, j);
                            break;
                        case "SaveGreyTem":
                            SaveGreyTem SaveGreyTem1 = (SaveGreyTem)ListTool[i][j];
                            SaveGreyTem1.WriteData(N_Path, j);
                            break;
                        case "FindLine":
                            FindLine FindLine1 = (FindLine)ListTool[i][j];
                            FindLine1.WriteData(N_Path, j);
                            break;
                        case "FindCircle":
                            FindCircle FindCircle1 = (FindCircle)ListTool[i][j];
                            FindCircle1.WriteData(N_Path, j);
                            break;
                        case "MakeCalibration":
                            MakeCalibration MakeCalibration1 = (MakeCalibration)ListTool[i][j];
                            MakeCalibration1.WriteData(N_Path, j);
                            break;
                        case "PuzzlePicture":
                            PuzzlePicture PuzzlePicture1 = (PuzzlePicture)ListTool[i][j];
                            PuzzlePicture1.WriteData(N_Path, j);
                            break;
                        case "PuzzleData":
                            PuzzleData PuzzleData1 = (PuzzleData)ListTool[i][j];
                            PuzzleData1.WriteData(N_Path, j);
                            break;
                        case "FocusPoint":
                            FocusPoint FocusPoint1 = (FocusPoint)ListTool[i][j];
                            FocusPoint1.WriteData(N_Path, j);
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        public bool Set_Solutionname(string Test_name)
        {
            int number = 0;
            bool Test_Current = true;

            for (int mtestnumber = 0; mtestnumber < File_solution.Count; mtestnumber++)
            {
                if (File_solution[mtestnumber] == Test_name)
                {
                    number = mtestnumber;
                   
                    Test_Current = false;
                    break;
                }
            }
            if (!Test_Current)
            {
                this.comboBox1.SelectedIndex = number;
                return true;
            }
            else
                return false;

        }


        public bool Runtest(string Test_name, int start, int end, out double Cameratime, out double alltime, out string type)
        {
            Cameratime = 0;
            alltime = 0;
          bool return_value =   Runtest(Test_name, start, end, out  Cameratime, out  alltime);
            type = Result_Type;
            return return_value;

        }


        public bool Runtest(string Test_name, int start, int end, out double Cameratime, out double alltime)
        {


            HObject bitmap_out = null;
            Realtime_display = false;
            DateTime beforDT = System.DateTime.Now;
            string outrun_info;
            listBox_info.Items.Clear();
            string formName;
            Cameratime = 0;
            alltime = 0;
            Result_Type = "";
            int number = 0;
            Linelist.Clear();
            bool Test_Current = true;
            Circlelist.Clear();
            Charlist.Clear();
            Bloblist.Clear();
            for (int mtestnumber = 0; mtestnumber < File_solution.Count; mtestnumber++)
            {
                if (File_solution[mtestnumber] == Test_name)
                {
                    number = mtestnumber;

                    Test_Current = false;
                    this.comboBox1.SelectedIndex = number;
                    break;
                }
            }
            if (Test_Current)
                number = this.comboBox1_SelectedIndex;



            if (number > ListTool.Count)
            {
            //    MessageBox.Show("Now the list range is out");
                return false;
            }

            if (start > ListTool[number].Count)
            {
             //   MessageBox.Show("Now the start is about all tool");
                return false;
            }

            if (end > ListTool[number].Count)
            {
          //      MessageBox.Show("Now the end is about all tool");
                return false;
            }


            CleearBufferRedundance(number);
            for (int i = start; i < end; i++)
            {
                DateTime beforDT1 = System.DateTime.Now;
                dataGridView1.ClearSelection();
                this.dataGridView1.Rows[i].Selected = true;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[i].Cells[0];
                this.dataGridView1.Refresh();
                formName = ListTool[number][i].GetType().Name; //取得form類型
                switch (formName)
                {
                    case "ReadPictureControl":
                        DateTime beforcameraDT = System.DateTime.Now;
                        ReadPictureControl F_readpicturecontrol = (ReadPictureControl)ListTool[number][i];

                        F_readpicturecontrol.Send_name();
                        bool picture_way = F_readpicturecontrol.Set_Pictureway();
                        bool exp_way = F_readpicturecontrol.Set_Exp();

                        if (_executeBuffer[number].imageBuffer.ContainsKey(F_readpicturecontrol.get_imagebuf() + ".img"))
                        {
                            if (_executeBuffer[number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"] != null)
                            {
                                if (_executeBuffer[number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"].IsInitialized())
                                {
                                    _executeBuffer[number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"].Dispose();
                                }
                            }
                        }
                        else
                        {
                   //         MessageBox.Show("取得图像：请先点击设置，确认输出加入链表中");
                            listBox_info.Items.Add("取得图像： 检查输入");

                            return false;
                        }

                        if (picture_way)
                        {
                            if (!F_readpicturecontrol.Cam_OpenImage(out bitmap_out))
                            {
                                listBox_info.Items.Add("取得图像： 文件取图失败");

                                return false;
                            }
                            if (_executeBuffer[number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"] != null)
                            {
                                if (_executeBuffer[number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"].IsInitialized())
                                {
                                    _executeBuffer[number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"].Dispose();
                                }
                            }
                        }
                        else
                        {

                            if (exp_way)
                            {
                                int exposure_time = F_readpicturecontrol.get_time();
                                if (BaslerCam.camera != null)
                                BaslerCam.camera.Parameters[PLCamera.ExposureTimeAbs].TrySetValue(exposure_time);
                            }
                            if (!Camera_onepicture_Process(out bitmap_out))
                            {
                                listBox_info.Items.Add("取得图像： 相机取图失败");

                                return false;
                            }

                        }
                        _executeBuffer[number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"] = bitmap_out;
                        Display_Graphic(_executeBuffer[number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"]);
                        DateTime cameraafterDT1 = System.DateTime.Now;
                        TimeSpan ts1 = cameraafterDT1.Subtract(beforcameraDT);
                        Cameratime = Convert.ToDouble(ts1.TotalMilliseconds.ToString());
                        listBox_info.Items.Add("取得图像: 完成");

                        break;

                    case "BinaryPro":
                        BinaryPro F_binarypro = (BinaryPro)ListTool[number][i];
                        F_binarypro.SetParaImage(_executeBuffer[number]);
                        F_binarypro.SetParaRegion(_executeBuffer[number]);
                        斑点GVName_halcon Blobresult;
                        if (!F_binarypro.Send_name())
                        {
                       //     MessageBox.Show("二值化处理: 输入图像为空");
                            listBox_info.Items.Add("二值化处理: 输入图像为空");
                            return false;
                        }
                        if (F_binarypro.Check_pal())
                        {

                            ExecuteBuffer executeBuffer_updatebinary = new ExecuteBuffer();
                            if (!F_binarypro.RunThresholdTest_new(_executeBuffer[number], out executeBuffer_updatebinary,out  Blobresult, out outrun_info))
                            {
                             //   MessageBox.Show("二值化处理: 异常，流程中止");
                                listBox_info.Items.Add(outrun_info);
                                return false;
                            }
                            if (F_binarypro.getshow())
                                Display_Graphic(_executeBuffer[number].imageBuffer[F_binarypro.get_Imageout()]);
                            listBox_info.Items.Add("二值化处理: 完成");

                        }
                        else
                        {
                            listBox_info.Items.Add("二值化处理： 参数错误,重新设置");
                            return false;
                        }
                        int blob_number = Bloblist.Count();
                        for (int imx = 0; imx < Blobresult.域面积.TupleLength(); imx++)
                        {
                            斑点GVName newblob = new 斑点GVName((blob_number + imx).ToString(), Blobresult.域中心X[imx].D.ToString("F3"), Blobresult.域中心Y[imx].D.ToString("F3"), Blobresult.域面积[imx].D.ToString("F3"), Blobresult.域角度[imx].D.ToString("F3"));
                            Bloblist.Add(newblob);
                        }
                        Result_Type = "Blob";
                        show_result("show_斑点");
                        break;


                    case "FindScaledShapeModel":
                        FindScaledShapeModel F_findscaledshapemodel = (FindScaledShapeModel)ListTool[number][i];
                        if (F_findscaledshapemodel.Check_pal())
                        {
                            hWndCtrl.clearList();

                            string Modelname = F_findscaledshapemodel.get_Modelname() + ".shapemode_contour";
                            string Imagename = F_findscaledshapemodel.get_Imagename() + ".img";
                            if (!_executeBuffer[number].imageBuffer.ContainsKey(Imagename))
                            {
                         //       MessageBox.Show("形状模板匹配： 无法找到输入图像");
                                listBox_info.Items.Add("形状模板匹配： 未加载输入图像");
                                return false;
                            }
                            Display_Graphic(_executeBuffer[number].imageBuffer[Imagename]);
                            List<模板GVName> match_test;
                            List<HObject> Graphic_out;
                            模板GVName_halcon match_test_halcon;
                            if (!_executeBuffer[number].controlBuffer.ContainsKey(Modelname))
                            {
                       //         MessageBox.Show("形状模板匹配： 无法找到输入模板");
                                listBox_info.Items.Add("形状模板匹配： 未加载匹配资源");
                                return false;
                            }
                            if (!F_findscaledshapemodel.MatchingModel(_executeBuffer[number].imageBuffer[Imagename], (HTuple)_executeBuffer[number].controlBuffer[Modelname], out match_test, out match_test_halcon, out Graphic_out, out outrun_info))
                            {
                         //       MessageBox.Show("形状模板匹配： 异常，请检测设置");
                                listBox_info.Items.Add(outrun_info);
                                return false;
                            }
                            for (int modelnumber = 0; modelnumber < Graphic_out.Count; modelnumber++)
                                hWndCtrl.addIconicVar(Graphic_out[modelnumber]);

                            hWndCtrl.repaint();
                            Modellist = match_test;
                            Modellist_halcon = match_test_halcon;
                            if (Modellist_halcon.点X.TupleLength() > 0)
                                listBox_info.Items.Add("形状模板匹配： 完成");
                            else
                                listBox_info.Items.Add("形状模板匹配： 找到数量：0");

                        }
                        else
                        {
                       //     MessageBox.Show("形状模板匹配： 形状匹配参数设置错误,请重新设置");
                            listBox_info.Items.Add("形状模板匹配:参数错误,重新设置");
                            return false;
                        }
                        show_result("show_模板");
                        break;
                    case "SaveShapeTem":
                        SaveShapeTem F_saveshapetem = (SaveShapeTem)ListTool[number][i];
                        if (F_saveshapetem.Check_pal())
                        {
                            hWndCtrl.clearList();


                            string Imagename = F_saveshapetem.get_Imagename() + ".img";
                            if (!_executeBuffer[number].imageBuffer.ContainsKey(Imagename))
                            {
                              //  MessageBox.Show("取形状模板： 无法找到输入图像");
                                listBox_info.Items.Add("取形状模板： 未加载输入图像");
                                return false;
                            }
                            Display_Graphic(_executeBuffer[number].imageBuffer[Imagename]);
                            List<模板GVName> match_test;
                            List<HObject> Graphic_out;
                            模板GVName_halcon match_test_halcon;
                            if (!F_saveshapetem.MatchingModel(hWndCtrl,_executeBuffer[number].imageBuffer[Imagename], out match_test, out match_test_halcon, out Graphic_out, out outrun_info, roiController))
                            {
                        //        MessageBox.Show("取形状模板： 异常，请检测设置");
                                listBox_info.Items.Add(outrun_info);
                                return false;
                            }


                            for (int modelnumber = 0; modelnumber < Graphic_out.Count; modelnumber++)
                                hWndCtrl.addIconicVar(Graphic_out[modelnumber]);

                            hWndCtrl.repaint();
                            Modellist = match_test;
                            Modellist_halcon = match_test_halcon;

                            if (Modellist_halcon.点X.TupleLength() > 0)
                                listBox_info.Items.Add("取形状模板： 完成");
                            else
                            { listBox_info.Items.Add("取形状模板： 找到数量：0");
                                return false;
                            }
                            Result_Type = "Match";
                        }
                        else
                        {
                      //      MessageBox.Show("取形状模板： 形状匹配参数设置错误,请重新设置");
                            listBox_info.Items.Add("取形状模板:参数错误,重新设置");
                            return false;
                        }
                        show_result("show_模板");
                        break;
                    case "SaveGreyTem":
                        SaveGreyTem F_savegreytem = (SaveGreyTem)ListTool[number][i];
                        if (F_savegreytem.Check_pal())
                        {
                            hWndCtrl.clearList();


                            string Imagename = F_savegreytem.get_Imagename() + ".img";
                            if (!_executeBuffer[number].imageBuffer.ContainsKey(Imagename))
                            {
                          //      MessageBox.Show("取灰度模板： 无法找到输入图像");
                                listBox_info.Items.Add("取灰度模板： 未加载输入图像");
                                return false;
                            }
                            Display_Graphic(_executeBuffer[number].imageBuffer[Imagename]);
                            List<模板GVName> match_test;
                            List<HObject> Graphic_out;
                            模板GVName_halcon match_test_halcon;
                            if (!F_savegreytem.MatchingModel(hWndCtrl,_executeBuffer[number].imageBuffer[Imagename], out match_test, out match_test_halcon, out Graphic_out, out outrun_info, roiController))
                            {
                         //       MessageBox.Show("取灰度模板： 异常，请检测设置");
                                listBox_info.Items.Add(outrun_info);
                                return false;
                            }


                            for (int modelnumber = 0; modelnumber < Graphic_out.Count; modelnumber++)
                                hWndCtrl.addIconicVar(Graphic_out[modelnumber]);

                            hWndCtrl.repaint();
                            Modellist = match_test;
                            Modellist_halcon = match_test_halcon;
                            Result_Type = "Match";
                            if (Modellist_halcon.点X.TupleLength() > 0)
                                listBox_info.Items.Add("取灰度模板： 完成");
                            else
                                listBox_info.Items.Add("取灰度模板： 找到数量：0");

                        }
                        else
                        {
                        //    MessageBox.Show("取灰度模板： 形状匹配参数设置错误,请重新设置");
                            listBox_info.Items.Add("取灰度模板:参数错误,重新设置");
                            return false;
                        }
                        show_result("show_模板");
                        break;
                    case "FindGreyModel":
                        FindGreyModel F_findgreymodel = (FindGreyModel)ListTool[number][i];

                        if (F_findgreymodel.Check_pal())
                        {
                            hWndCtrl.clearList();

                            string Modelname = F_findgreymodel.get_Modelname() + ".shapemode_grey";
                            string Imagename = F_findgreymodel.get_Imagename() + ".img";
                            if (!_executeBuffer[number].imageBuffer.ContainsKey(Imagename))
                            {
                            //    MessageBox.Show("灰度模板匹配： 无法找到输入图像");
                                listBox_info.Items.Add("灰度模板匹配:未载入图像");
                                return false;
                            }
                            Display_Graphic(_executeBuffer[number].imageBuffer[Imagename]);
                            List<模板GVName> match_test;
                            List<HObject> Graphic_out;
                            模板GVName_halcon match_test_halcon;

                            if (!_executeBuffer[number].controlBuffer.ContainsKey(Modelname))
                            {
                        //        MessageBox.Show("灰度模板匹配： 无法找到输入模板");
                                listBox_info.Items.Add("灰度模板匹配： 未加载匹配资源");
                                return false;
                            }
                            if (!F_findgreymodel.MatchingModel(_executeBuffer[number].imageBuffer[Imagename], (HTuple)_executeBuffer[number].controlBuffer[Modelname], out match_test, out match_test_halcon, out Graphic_out, out outrun_info))
                            {
                         //       MessageBox.Show("灰度模板匹配： 异常，请检测设置");
                                listBox_info.Items.Add(outrun_info);
                                return false;
                            }



                            for (int modelnumber = 0; modelnumber < Graphic_out.Count; modelnumber++)
                                hWndCtrl.addIconicVar(Graphic_out[modelnumber]);

                            hWndCtrl.repaint();
                            Modellist = match_test;
                            Modellist_halcon = match_test_halcon;
                            if (Modellist_halcon.点X.TupleLength() > 0)
                                listBox_info.Items.Add("灰度模板匹配： 完成");
                            else
                                listBox_info.Items.Add("灰度模板匹配： 找到数量：0");
                        }
                        else
                        {
                        //    MessageBox.Show("灰度模板匹配： 参数设置错误,请重新设置");
                            listBox_info.Items.Add("灰度模板匹配： 参数设置错误,请重新设置");
                            return false;
                        }
                        show_result("show_模板");

                        break;
                    case "NinePointCal":
                        NinePointCal R_ninepointcal = (NinePointCal)ListTool[number][i];
                        SourceBuffer sourceBuffer_update = new SourceBuffer();
                        ExecuteBuffer executeBuffer_update = new ExecuteBuffer();
                        int run_way = R_ninepointcal.getlabel();
                        int runway_show = 0;

                        if (Modellist.Count == 0)
                        {
                       //     MessageBox.Show("九点标定:  异常,请确认匹配是否成功");
                            listBox_info.Items.Add("九点标定：异常，匹配结果为0 ");
                            return false;
                        }
                        if (run_way < 9)
                        {
                            runway_show = run_way + 1;
                            R_ninepointcal.Setlabel(Convert.ToDouble(Modellist[0].点X), Convert.ToDouble(Modellist[0].点Y));
                            listBox_info.Items.Add("九点标定：" + runway_show + "完成");

                        }

                        int run_lastway = R_ninepointcal.getlabel();
                        if (run_lastway == 9)
                        {
                            if (R_ninepointcal.NinePointCalMake(_sourceBuffer[number], _executeBuffer[number], out sourceBuffer_update, out executeBuffer_update))
                            {
                                _sourceBuffer[number] = sourceBuffer_update;
                                _executeBuffer[number] = executeBuffer_update;
                                listBox_info.Items.Add("九点标定： 全部完成");
                                R_ninepointcal.SetLabelcom();
                            }
                            else
                            {
                                R_ninepointcal.SetLabelcom();
                            }
                        }

                        break;
                    case "MakeCalibration":
                        MakeCalibration R_makecalibration = (MakeCalibration)ListTool[number][i];
                        SourceBuffer sourceBuffer_updatecal = new SourceBuffer();
                        ExecuteBuffer executeBuffer_updatecal = new ExecuteBuffer();

                        R_makecalibration.CalibrationMake(_sourceBuffer[number], _executeBuffer[number], out sourceBuffer_update, out executeBuffer_update, hWndCtrl, out outrun_info);

                        if (R_makecalibration.get_result())
                        {
                            if (_executeBuffer[number].imageBuffer[R_makecalibration.get_Imageout()] != null)
                            {
                                hWndCtrl.repaint();
                                Display_Graphic(_executeBuffer[number].imageBuffer[R_makecalibration.get_Imageout()]);
                            }
                        }

                         listBox_info.Items.Add(outrun_info);
                        break;

                    case "PuzzlePicture":
                        PuzzlePicture R_makepuzzle = (PuzzlePicture)ListTool[number][i];
                        ExecuteBuffer executeBuffer_updatepuzle = new ExecuteBuffer();

                        R_makepuzzle.RunThresholdTest_new(_executeBuffer[number], out executeBuffer_update, out outrun_info, Tem_Pointlist);


                        if (_executeBuffer[number].imageBuffer[R_makepuzzle.get_Imageout()] != null)
                            Display_Graphic(_executeBuffer[number].imageBuffer[R_makepuzzle.get_Imageout()]);


                        listBox_info.Items.Add(outrun_info);



                        break;
                    case "RoiMake":
                        RoiMake R_roimake = (RoiMake)ListTool[number][i];
                        R_roimake.Send_name();
                        if (R_roimake.Check_pal())
                        {
                            ExecuteBuffer executeBuffer_roi = new ExecuteBuffer();
                            R_roimake.Genshape(_executeBuffer[number], out executeBuffer_roi,hWndCtrl, Modellist_halcon, Tem_Pointlist, Linelist);
                            _executeBuffer[number] = executeBuffer_roi;

                            listBox_info.Items.Add("生成ROI: 完成");
                            //   Display_Graphic(_executeBuffer[number].imageBuffer[R_roimake.getshapename()]);
                        }
                        else
                        {
                     //       MessageBox.Show("生成ROI: 参数设置错误,请重新设置");
                            listBox_info.Items.Add("生成ROI：参数设置错误，请重新设置");
                            return false;
                        }


                        break;
                    case "GeneralMorphology":
                        GeneralMorphology R_generalmorphology = (GeneralMorphology)ListTool[number][i];
                        if (R_generalmorphology.Check_pal())
                        {
                            ExecuteBuffer executeBuffer_mor = new ExecuteBuffer();
                            R_generalmorphology.Run_Mor(_executeBuffer[number], out executeBuffer_mor);
                            //R_generalmorphology.(_executeBuffer[Thread_number], out executeBuffer_mor);
                            _executeBuffer[number] = executeBuffer_mor;
                            Display_Graphic(_executeBuffer[number].imageBuffer[R_generalmorphology.getRegionoutname()]);
                        }
                        else
                        {
                         //   MessageBox.Show("通用形态学： 通用形态参数设置错误,请重新设置");
                            return false;
                        }
                        break;
                    case "SmoothTest":
                        SmoothTest R_smooth = (SmoothTest)ListTool[number][i];
                        if (R_smooth.Check_pal())
                        {
                            ExecuteBuffer smooth_buffer = new ExecuteBuffer();
                            R_smooth.Run_Smooth(_executeBuffer[number], out smooth_buffer);
                            //R_generalmorphology.(_executeBuffer[Thread_number], out executeBuffer_mor);
                            _executeBuffer[number] = smooth_buffer;
                            Display_Graphic(_executeBuffer[number].imageBuffer[R_smooth.get_Region()]);
                        }
                        else
                        {
                   //         MessageBox.Show("平滑处理: 参数设置错误,请重新设置");
                            listBox_info.Items.Add("平滑处理：参数错误,重新设置");
                            return false;

                        }
                        listBox_info.Items.Add("平滑处理: 完成");

                        break;
                    case "RegionProcess":
                        RegionProcess R_regionprocess = (RegionProcess)ListTool[number][i];
                        if (R_regionprocess.Check_pal())
                        {
                            ExecuteBuffer regionout_buffer = new ExecuteBuffer();
                            Dictionary<int, PointName> Tem_out2Pointlist = new Dictionary<int, PointName>();
                            R_regionprocess.Run_Region(_executeBuffer[number], out regionout_buffer, out outrun_info, Tem_Pointlist, out Tem_out2Pointlist);
                            //R_generalmorphology.(_executeBuffer[Thread_number], out executeBuffer_mor);
                            _executeBuffer[number] = regionout_buffer;
                            // Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_regionprocess.get_Regionout()]);
                        }
                        else
                        {
                    //        MessageBox.Show("区域处理： 操作参数设置错误,请重新设置");

                            return false;
                        }
                        break;
                    case "Transformation":
                        Transformation R_transformation = (Transformation)ListTool[number][i];
                        if (R_transformation.Check_pal())
                        {
                            Dictionary<int, PointName> Tem_outPointlist = new Dictionary<int, PointName>();
                            R_transformation.Run_transform(_executeBuffer[number], Pointlist_halcon, Modellist_halcon, Circlelist_halcon, Tem_Pointlist, out Tem_outPointlist);
                            Tem_Pointlist = Tem_outPointlist;

                        }
                        else
                        {
                     //       MessageBox.Show("坐标转换：  参数设置错误,请重新设置");
                            listBox_info.Items.Add("坐标转换：参数设置错误,请重新设置");

                            return false;
                        }
                        break;
                    case "MathCalc":
                        MathCalc R_mathcalc = (MathCalc)ListTool[number][i];
                        if (R_mathcalc.Check_pal())
                        {
                            Dictionary<int, PointName> Tem_out1Pointlist = new Dictionary<int, PointName>();
                            R_mathcalc.Run_transform(_executeBuffer[number], Pointlist_halcon, Modellist_halcon, Circlelist_halcon, Tem_Pointlist, out Tem_out1Pointlist);
                            Tem_Pointlist = Tem_out1Pointlist;
                        }
                        else
                        {
                     //       MessageBox.Show("算术计算： 参数设置错误,请重新设置");
                            label_infotext.Text = "算术计算：参数设置错误,请重新设置";
                            return false;
                        }
                        listBox_info.Items.Add("算术计算：完成");
                        break;
                    case "PointLine":
                        PointLine R_pointline = (PointLine)ListTool[number][i];
                        if (R_pointline.Check_pal())
                        {
                            Dictionary<int, PointName> Tem_out2Pointlist = new Dictionary<int, PointName>();
                            R_pointline.Run_transform(_executeBuffer[number], Modellist_halcon, Circlelist_halcon, Tem_Pointlist, out Tem_out2Pointlist,Linelist);
                            Tem_Pointlist = Tem_out2Pointlist;
                        }
                        else
                        {
                     //       MessageBox.Show("点线距离： 参数设置错误,请重新设置");
                            label_infotext.Text = "点线距离：参数设置错误,请重新设置";
                            return false;
                        }
                        listBox_info.Items.Add("点线距离：完成");
                        break;
                    case "LineLine":
                        LineLine R_lineline = (LineLine)ListTool[number][i];
                        if (R_lineline.Check_pal())
                        {
                            Dictionary<int, PointName> Tem_out2Linelist = new Dictionary<int, PointName>();
                            if (R_lineline.Run_transform(_executeBuffer[number], Modellist_halcon, Circlelist_halcon, Tem_Pointlist, out Tem_out2Linelist, Linelist))
                                Tem_Pointlist = Tem_out2Linelist;
                            else
                            {
                                label_infotext.Text = "线线交点：找线失败";
                                return false;
                            }
                        }
                        else
                        {
                      //      MessageBox.Show("线线交点： 参数设置错误,请重新设置");
                            label_infotext.Text = "线线交点：参数设置错误,请重新设置";
                            return false;
                        }
                        listBox_info.Items.Add("线线交点：完成");
                        Result_Type = "Point" + R_lineline.get_outnumber();
                        break;
                    case "PuzzleData":
                        PuzzleData R_puzzledata = (PuzzleData)ListTool[number][i];
                        if (R_puzzledata.Check_pal())
                        {
                            Dictionary<int, PointName> Tem_out3Pointlist = new Dictionary<int, PointName>();
                            R_puzzledata.Run_transform(Modellist_halcon, Tem_Pointlist, out Tem_out3Pointlist);
                            Tem_Pointlist = Tem_out3Pointlist;
                        }
                        else
                        {
                         //   MessageBox.Show("拼图数据： 参数设置错误,请重新设置");
                            label_infotext.Text = "拼图数据：参数设置错误,请重新设置";
                            return false;
                        }
                        listBox_info.Items.Add("拼图数据：完成");
                        break;
                    case "FocusPoint":
                        FocusPoint R_focuspoint = (FocusPoint)ListTool[number][i];
                        if (R_focuspoint.Check_pal())
                        {
                            Dictionary<int, PointName> Tem_out4Pointlist = new Dictionary<int, PointName>();
                            R_focuspoint.Run_transform(_executeBuffer[number],Modellist_halcon, Tem_Pointlist, out Tem_out4Pointlist,out outrun_info);
                            Tem_Pointlist = Tem_out4Pointlist;
                        }
                        else
                        {
                  //          MessageBox.Show("焦点： 参数设置错误,请重新设置");
                            label_infotext.Text = "焦点：参数设置错误,请重新设置";
                            return false;
                        }
                        listBox_info.Items.Add("焦点：完成");
                        break;
                    case "RegionInterest":
                        RegionInterest R_regioninterest = (RegionInterest)ListTool[number][i];
                        if (R_regioninterest.Check_pal())
                        {
                            Dictionary<int, PointName> Tem_out1Pointlist = new Dictionary<int, PointName>();
                            ExecuteBuffer executeBuffer_regioninterest = new ExecuteBuffer();
                            if (!R_regioninterest.Run_Region(_executeBuffer[number], out executeBuffer_regioninterest))
                                return false;
                            else
                                Display_Graphic(_executeBuffer[number].imageBuffer[R_regioninterest.get_Regionout()]);

                        }
                        else
                        {
                            MessageBox.Show("感兴趣区域： 参数设置错误,请重新设置");
                            listBox_info.Items.Add("感兴趣区域：参数设置错误,请重新设置");
                            return false;
                        }
                        listBox_info.Items.Add("感兴趣区域：完成");
                        break;
                    case "ReadQRcode":
                        ReadQRcode R_readqrcode = (ReadQRcode)ListTool[number][i];
                        字符串GVName_halcon xx_string = new 字符串GVName_halcon();
                        if (R_readqrcode.Check_pal())
                        {
                            int number1 = Charlist.Count;
                            R_readqrcode.Run_Region(_executeBuffer[number], out xx_string, hWndCtrl);
                            for (int ix = 0; ix < xx_string.字符串.Length; ix++)
                            {
                                Charlist.Add(new 字符串GVName((ix + 1 + number1).ToString(), xx_string.字符串[ix]));
                            }


                        }
                        else
                        {
                         //   MessageBox.Show("二维码读取： 参数设置错误,请重新设置");
                            listBox_info.Items.Add("二维码读取：参数设置错误,请重新设置");
                            return false;
                        }
                        listBox_info.Items.Add("二维码读取：完成");
                        break;
                    case "ReadBarcode":
                        ReadBarcode R_readbarcode = (ReadBarcode)ListTool[number][i];
                        字符串GVName_halcon yy_string = new 字符串GVName_halcon();
                        if (R_readbarcode.Check_pal())
                        {
                            int number2 = Charlist.Count;
                            R_readbarcode.Run_Region(_executeBuffer[number], out xx_string, hWndCtrl);
                            for (int ix = 0; ix < xx_string.字符串.Length; ix++)
                            {
                                Charlist.Add(new 字符串GVName((ix + 1 + number2).ToString(), xx_string.字符串[ix]));
                            }


                        }
                        else
                        {
                      //      MessageBox.Show("条形码读取： 参数设置错误,请重新设置");
                            listBox_info.Items.Add("条形码读取：参数设置错误,请重新设置");
                            return false;
                        }
                        listBox_info.Items.Add("条形码读取：完成");
                        break;
                    case "PointShow":
                        PointShow R_pointshow = (PointShow)ListTool[number][i];
                        if (R_pointshow.Check_pal())
                        {

                            R_pointshow.Run_show(_executeBuffer[number], Modellist_halcon, Tem_Pointlist, hWndCtrl, out outrun_info);
                            listBox_info.Items.Add(outrun_info);
                        }
                        else
                        {
                      //      MessageBox.Show("点位显示： 参数设置错误,请重新设置");
                            listBox_info.Items.Add("点位显示：参数设置错误,请重新设置");
                            return false;
                        }
                        break;
                    case "TestResultsShow":
                        TestResultsShow R_testresultsshow = (TestResultsShow)ListTool[number][i];

                            R_testresultsshow.Run_show("",Tem_Circlelist, Tem_CircleArclist,Tem_Linelist, hWndCtrl, out outrun_info);
                            
                            listBox_info.Items.Add(outrun_info);
                        

                        break;
                    case "FindLine":
                        FindLine R_findline = (FindLine)ListTool[number][i];
                        listBox_info.Items.Clear();
                        HTuple Row1, Column1, Row2, Column2;
                        int count_line = Linelist.Count;
                        if (R_findline.Check_pal())
                        {
                            if (R_findline.Find_halcon_line(_executeBuffer[number], hWndCtrl, Modellist_halcon, Tem_Pointlist, out outrun_info, out Row1, out Column1, out Row2, out Column2, false))
                            {
                                for (int im = 0; im < 1; im++)
                                {
                                    直线GVName newline = new 直线GVName((count_line + im).ToString(), Row1[im].D.ToString(), Column1[im].D.ToString(), Row2[im].D.ToString(), Column2[im].D.ToString());
                                    Linelist.Add(newline);
                                }
                            }
                            else
                            {
                                listBox_info.Items.Add("查找直线：未找到直线");
                                return false;
                            }
                            listBox_info.Items.Add(outrun_info);
                            show_result("show_直线");
                            Result_Type = "Line";
                        }
                        else
                        {
                    //        MessageBox.Show("查找直线： 参数设置错误,请重新设置");
                            listBox_info.Items.Add("查找直线：参数设置错误,请重新设置");

                            return false;
                        }

                        break;

                    case "FindCircle":
                        FindCircle R_findcircle = (FindCircle)ListTool[number][i];
                        listBox_info.Items.Clear();
                        HTuple Row_circle, Column_circle,Radius_circle;
                        int count_circle = Circlelist.Count;
                        圆GVName newcircle;

                        if (R_findcircle.Check_pal())
                        {
                            if (R_findcircle.Find_halcon_circle(_executeBuffer[number], hWndCtrl,Modellist_halcon,Tem_Pointlist,out outrun_info, out Row_circle, out Column_circle, out Radius_circle,false))
                            {
                                for (int im = 0; im < Row_circle.TupleLength(); im++)
                                {
                                    newcircle = new 圆GVName((count_circle+im).ToString(), Row_circle[im].D.ToString(), Column_circle[im].D.ToString(), Radius_circle[im].D.ToString());
                                    Circlelist.Add(newcircle);
                                }
                                show_result("show_圆");
                                Result_Type = "Circle";
                            }
                            listBox_info.Items.Add(outrun_info);


                        }
                        else
                        {
                      //      MessageBox.Show("查找圆： 参数设置错误,请重新设置");
                            listBox_info.Items.Add("查找圆：参数设置错误,请重新设置");

                            return false;
                        }

                        break;

                    default:
                        break;

                }


                if (i != ListTool[number].Count - 1)
                    this.dataGridView1.Rows[i].Selected = false;




            }


            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            label_时间.Text = ts.TotalMilliseconds.ToString()+"ms";
            alltime = Convert.ToDouble(ts.TotalMilliseconds.ToString());

            roiController.reset();
            return true;

        }



        public static Form DeepCopyByBinary<Form>(Form obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (Form)retval;
        }




        public bool showContour(string color)
        {
            if (CurCam_IniStatus)
                Camera_onepicture();
            else
            {
                MessageBox.Show("相机未打开");
                return false;
            }
            TestResultsShow R_testresultsshow =  new TestResultsShow();
            string out_result_info = "";
            if (color != null)
                R_testresultsshow.Run_show(color, Tem_Circlelist, Tem_CircleArclist, Tem_Linelist, hWndCtrl, out out_result_info);
            return true;
        }

        /// <summary>
        /// 运行单个工具
        /// </summary>
        /// <param name="snumber">指定要运行的工具编号</param>
        private void OneRuntest(int snumber)
        {
            DateTime beforDT = System.DateTime.Now;
            HObject bitmap_out = null;
            string formName;
            int Thread_number = comboBox1_SelectedIndex;
            string out_result_info = "";
            CleearBufferRedundance(Thread_number);
            formName = ListTool[comboBox1_SelectedIndex][snumber].GetType().Name; //取得form類型
            Realtime_display = false;
            switch (formName)
            {
                case "ReadPictureControl":
                    ReadPictureControl F_readpicturecontrol = (ReadPictureControl)ListTool[comboBox1_SelectedIndex][snumber];
                    F_readpicturecontrol.Send_name();
                    listBox_info.Items.Clear();
                    bool picture_way = F_readpicturecontrol.Set_Pictureway();
                    bool exp_way = F_readpicturecontrol.Set_Exp();

                    if (_executeBuffer[Thread_number].imageBuffer.ContainsKey(F_readpicturecontrol.get_imagebuf() + ".img"))
                    {
                        if (_executeBuffer[Thread_number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"] != null)
                        {
                            if (_executeBuffer[Thread_number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"].IsInitialized())
                            {
                                _executeBuffer[Thread_number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"].Dispose();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("取得图像： 请先点击设置，确认输出加入链表中");
                        listBox_info.Items.Add("取得图像： 请先点击设置，确认输出加入链表中");

                        return;
                    }

                    if (picture_way)
                    {

                        if (exp_way)
                        {
                            int exposure_time = F_readpicturecontrol.get_time();
                            BaslerCam.camera.Parameters[PLCamera.ExposureTimeAbs].TrySetValue(exposure_time);
                        }
                        if (!F_readpicturecontrol.Cam_OpenImage(out bitmap_out))
                        {

                            listBox_info.Items.Add("取得图像： 文件取图失败");
                            break;
                        }

                        if (_executeBuffer[Thread_number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"] != null)
                        {
                            if (_executeBuffer[Thread_number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"].IsInitialized())
                            {
                                _executeBuffer[Thread_number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"].Dispose();
                            }
                        }
                    }
                    else
                    {
                        if (!Camera_onepicture_Process(out bitmap_out))
                        {

                            listBox_info.Items.Add("取得图像： 相机取图失败");
                            break;
                        }
                    }
                    _executeBuffer[Thread_number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"] = bitmap_out;
                    Display_Graphic(_executeBuffer[Thread_number].imageBuffer[F_readpicturecontrol.get_imagebuf() + ".img"]);

                    listBox_info.Items.Add("取得图像： 完成");
                    break;

                case "BinaryPro":
                    BinaryPro F_binarypro = (BinaryPro)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    F_binarypro.SetParaImage(_executeBuffer[Thread_number]);
                    F_binarypro.SetParaRegion(_executeBuffer[Thread_number]);
                    F_binarypro.Send_name();
                    斑点GVName_halcon Blobresult;
                    if (F_binarypro.Check_pal())
                    {

                        String Image_name = F_binarypro.get_Imagename();
                        ExecuteBuffer executeBuffer_updatebinary = new ExecuteBuffer();
                           Display_Graphic(_executeBuffer[Thread_number].imageBuffer[F_binarypro.get_Imagename()]);
                           if (!F_binarypro.RunThresholdTest_new(_executeBuffer[Thread_number], out executeBuffer_updatebinary, out Blobresult, out out_result_info))
                        {
                            MessageBox.Show("二值化处理: 异常，流程中止");
                            listBox_info.Items.Add(out_result_info);
                            break;
                        }
                        if (F_binarypro.getshow())
                        Display_Graphic(_executeBuffer[Thread_number].imageBuffer[F_binarypro.get_Imageout()]);
                    }
                    else
                    {

                        listBox_info.Items.Add("二值化处理： 参数设置错误,请重新设置");
                        MessageBox.Show("二值化处理： 参数设置错误,请重新设置");
                        break;
                    }
                      int blob_number = Bloblist.Count();
                        for (int imx = 0; imx < Blobresult.域面积.TupleLength(); imx++)
                        {
                            斑点GVName newblob = new 斑点GVName((blob_number + imx).ToString(), Blobresult.域中心X[imx].D.ToString("F3"), Blobresult.域中心Y[imx].D.ToString("F3"), Blobresult.域面积[imx].D.ToString("F3"),Blobresult.域角度[imx].D.ToString("F3"));
                            Bloblist.Add(newblob);
                        }
                    listBox_info.Items.Add("二值化处理： 完成");
                    break;


                case "FindScaledShapeModel":
                    FindScaledShapeModel F_findscaledshapemodel = (FindScaledShapeModel)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (F_findscaledshapemodel.Check_pal())
                    {
                        hWndCtrl.clearList();
                        string Modelname = F_findscaledshapemodel.get_Modelname() + ".shapemode_contour";
                        string Imagename = F_findscaledshapemodel.get_Imagename() + ".img";
                        if (!_executeBuffer[Thread_number].imageBuffer.ContainsKey(Imagename))
                        {
                            MessageBox.Show("形状模板匹配： 找不到输入图像，请设置");
                            listBox_info.Items.Add("形状模板匹配： 未加载输入图像");
                            break;
                        }

                        Display_Graphic(_executeBuffer[Thread_number].imageBuffer[Imagename]);
                        List<模板GVName> match_test;
                        List<HObject> Graphic_out;
                        模板GVName_halcon match_test_halcon;
                        if (!_executeBuffer[Thread_number].controlBuffer.ContainsKey(Modelname))
                        {
                            MessageBox.Show("形状模板匹配： 未加载匹配资源：" + Modelname);
                            listBox_info.Items.Add("形状模板匹配： 未加载匹配资源");
                            break;
                        }
                        if (!F_findscaledshapemodel.MatchingModel(_executeBuffer[Thread_number].imageBuffer[Imagename], (HTuple)_executeBuffer[Thread_number].controlBuffer[Modelname], out match_test, out match_test_halcon, out Graphic_out, out out_result_info))
                        {
                            MessageBox.Show("形状模板匹配：    异常，请检查");
                            listBox_info.Items.Add(out_result_info);
                            break;
                        }
                        for (int modelnumber = 0; modelnumber < Graphic_out.Count; modelnumber++)
                            hWndCtrl.addIconicVar(Graphic_out[modelnumber]);

                        hWndCtrl.repaint();
                        Modellist = match_test;
                        Modellist_halcon = match_test_halcon;
                        if (Modellist_halcon.点X.TupleLength() > 0)
                            listBox_info.Items.Add("形状模板匹配： 完成");
                        else
                            listBox_info.Items.Add("形状模板匹配： 找到数量：0");
                    }
                    else
                    {
                        MessageBox.Show("形状模板匹配： 形状匹配参数设置错误,请重新设置");
                        listBox_info.Items.Add("形状模板匹配：  参数设置错误,请重新设置");
                        break;
                    }
                    show_result("show_模板");
                    break;
                case "SaveShapeTem":
                    SaveShapeTem F_saveshapetem = (SaveShapeTem)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (F_saveshapetem.Check_pal())
                    {
                        hWndCtrl.clearList();

                        string Imagename = F_saveshapetem.get_Imagename() + ".img";
                        if (!_executeBuffer[Thread_number].imageBuffer.ContainsKey(Imagename))
                        {
                            MessageBox.Show("取形状模板： 找不到输入图像，请设置");
                            listBox_info.Items.Add("取形状模板： 未加载输入图像");
                            break;
                        }

                        Display_Graphic(_executeBuffer[Thread_number].imageBuffer[Imagename]);
                        List<模板GVName> match_test;
                        List<HObject> Graphic_out;
                        模板GVName_halcon match_test_halcon;

                        if (!F_saveshapetem.MatchingModel(hWndCtrl,_executeBuffer[Thread_number].imageBuffer[Imagename], out match_test, out match_test_halcon, out Graphic_out, out out_result_info, roiController))
                        {
                            MessageBox.Show("取形状模板：    异常，请检查");
                            listBox_info.Items.Add(out_result_info);
                            break;
                        }
                        for (int modelnumber = 0; modelnumber < Graphic_out.Count; modelnumber++)
                            hWndCtrl.addIconicVar(Graphic_out[modelnumber]);

                        hWndCtrl.repaint();
                        Modellist = match_test;
                        Modellist_halcon = match_test_halcon;
                        if (Modellist_halcon.点X.TupleLength() > 0)
                            listBox_info.Items.Add("取形状模板： 完成");
                        else
                            listBox_info.Items.Add("取形状模板： 找到数量：0");
                    }
                    else
                    {
                        MessageBox.Show("取形状模板： 形状匹配参数设置错误,请重新设置");
                        listBox_info.Items.Add("取形状模板：  参数设置错误,请重新设置");
                        break;
                    }
                    show_result("show_模板");
                    break;

                case "SaveGreyTem":
                    SaveGreyTem F_savegreytem = (SaveGreyTem)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (F_savegreytem.Check_pal())
                    {
                        hWndCtrl.clearList();

                        string Imagename = F_savegreytem.get_Imagename() + ".img";
                        if (!_executeBuffer[Thread_number].imageBuffer.ContainsKey(Imagename))
                        {
                            MessageBox.Show("取灰度模板： 找不到输入图像，请设置");
                            listBox_info.Items.Add("取灰度模板： 未加载输入图像");
                            break;
                        }

                        Display_Graphic(_executeBuffer[Thread_number].imageBuffer[Imagename]);
                        List<模板GVName> match_test;
                        List<HObject> Graphic_out;
                        模板GVName_halcon match_test_halcon;

                        if (!F_savegreytem.MatchingModel(hWndCtrl,_executeBuffer[Thread_number].imageBuffer[Imagename], out match_test, out match_test_halcon, out Graphic_out, out out_result_info, roiController))
                        {
                            MessageBox.Show("取灰度模板：    异常，请检查");
                            listBox_info.Items.Add(out_result_info);
                            break;
                        }
                        for (int modelnumber = 0; modelnumber < Graphic_out.Count; modelnumber++)
                            hWndCtrl.addIconicVar(Graphic_out[modelnumber]);

                        hWndCtrl.repaint();
                        Modellist = match_test;
                        Modellist_halcon = match_test_halcon;
                        if (Modellist_halcon.点X.TupleLength() > 0)
                            listBox_info.Items.Add("取灰度模板： 完成");
                        else
                            listBox_info.Items.Add("取灰度模板： 找到数量：0");
                    }
                    else
                    {
                        MessageBox.Show("取灰度模板： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("取灰度模板：  参数设置错误,请重新设置");
                        break;
                    }
                    show_result("show_模板");
                    break;
                case "FindGreyModel":
                    FindGreyModel F_findgreymodel = (FindGreyModel)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (F_findgreymodel.Check_pal())
                    {
                        hWndCtrl.clearList();
                        string Modelname = F_findgreymodel.get_Modelname() + ".shapemode_grey";
                        string Imagename = F_findgreymodel.get_Imagename() + ".img";
                        if (!_executeBuffer[Thread_number].imageBuffer.ContainsKey(Imagename))
                        {
                            MessageBox.Show("灰度模板匹配： 找不到输入图像，请设置");
                            listBox_info.Items.Add("灰度模板匹配： 未载入图像");
                            break;
                        }

                        Display_Graphic(_executeBuffer[Thread_number].imageBuffer[Imagename]);
                        List<模板GVName> match_test;
                        List<HObject> Graphic_out;
                        模板GVName_halcon match_test_halcon;
                        if (!_executeBuffer[Thread_number].controlBuffer.ContainsKey(Modelname))
                        {
                            MessageBox.Show("灰度模板匹配： 未加载匹配资源：" + Modelname);
                            listBox_info.Items.Add("灰度模板匹配： 未加载匹配资源");
                            break;
                        }
                        if (!F_findgreymodel.MatchingModel(_executeBuffer[Thread_number].imageBuffer[Imagename], (HTuple)_executeBuffer[Thread_number].controlBuffer[Modelname], out match_test, out match_test_halcon, out Graphic_out, out out_result_info))
                        {
                            MessageBox.Show("灰度模板匹配：    异常，请检查");
                            //label_infotext.Text = "灰度模板匹配： 异常，请检查";
                            listBox_info.Items.Add("灰度模板匹配： 异常，请检查");
                            break;
                        }
                        for (int modelnumber = 0; modelnumber < Graphic_out.Count; modelnumber++)
                            hWndCtrl.addIconicVar(Graphic_out[modelnumber]);

                        hWndCtrl.repaint();
                        Modellist = match_test;
                        Modellist_halcon = match_test_halcon;

                    }
                    else
                    {
                        MessageBox.Show("灰度模板匹配： 参数设置错误,重新设置");
                        listBox_info.Items.Add("灰度模板匹配： 参数设置错误,重新设置");
                        break;
                    }
                    listBox_info.Items.Add("灰度模板匹配： 完成");
                    show_result("show_模板");
                    break;

                case "NinePointCal":
                    NinePointCal R_ninepointcal = (NinePointCal)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    SourceBuffer sourceBuffer_update = new SourceBuffer();
                    ExecuteBuffer executeBuffer_update = new ExecuteBuffer();
                    int run_way = R_ninepointcal.getlabel();



                    if (run_way < 9)
                    {
                        if (Modellist.Count > 0)
                            R_ninepointcal.Setlabel(Convert.ToDouble(Modellist[0].点X), Convert.ToDouble(Modellist[0].点Y));
                        else
                        {
                            MessageBox.Show("九点标定: 请先确认匹配结果是否正常");
                            listBox_info.Items.Add("九点标定：" + run_way + "异常");
                            break;
                        }
                    }

                    int run_lastway = R_ninepointcal.getlabel();
                    if (run_lastway == 9)
                    {
                        R_ninepointcal.NinePointCalMake(_sourceBuffer[Thread_number], _executeBuffer[Thread_number], out sourceBuffer_update, out executeBuffer_update);
                        _sourceBuffer[Thread_number] = sourceBuffer_update;
                        _executeBuffer[Thread_number] = executeBuffer_update;
                    }
                    listBox_info.Items.Add("九点标定：" + run_way + "完成");
                    break;
                case "MakeCalibration":
                    MakeCalibration R_makecalibration = (MakeCalibration)ListTool[comboBox1_SelectedIndex][snumber];
                    SourceBuffer sourceBuffer_updatecal = new SourceBuffer();
                    ExecuteBuffer executeBuffer_updatecal = new ExecuteBuffer();

                    R_makecalibration.CalibrationMake(_sourceBuffer[Thread_number], _executeBuffer[Thread_number], out sourceBuffer_update, out executeBuffer_update, hWndCtrl, out out_result_info);

                    if (R_makecalibration.get_result())
                    {
                        if (_executeBuffer[Thread_number].imageBuffer[R_makecalibration.get_Imageout()] != null)
                        {
                            hWndCtrl.repaint();
                            Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_makecalibration.get_Imageout()]);
                        }
                    }

                    listBox_info.Items.Add(out_result_info);
                    break;
                case "PuzzlePicture":
                    PuzzlePicture R_makepuzzle = (PuzzlePicture)ListTool[comboBox1_SelectedIndex][snumber];
                    ExecuteBuffer executeBuffer_updatepuzle = new ExecuteBuffer();

                    R_makepuzzle.RunThresholdTest_new(_executeBuffer[Thread_number], out executeBuffer_update, out out_result_info, Tem_Pointlist);


                    if (_executeBuffer[Thread_number].imageBuffer[R_makepuzzle.get_Imageout()] != null)
                        Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_makepuzzle.get_Imageout()]);


                    listBox_info.Items.Add(out_result_info);



                    break;
                case "RoiMake":
                    RoiMake R_roimake = (RoiMake)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    R_roimake.Send_name();
                    if (R_roimake.Check_pal())
                    {
                        ExecuteBuffer executeBuffer_roi = new ExecuteBuffer();
                        R_roimake.Genshape(_executeBuffer[Thread_number], out executeBuffer_roi,hWndCtrl, Modellist_halcon, Tem_Pointlist, Linelist);
                        _executeBuffer[Thread_number] = executeBuffer_roi;
                        // Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_roimake.getshapename()]);
                    }
                    else
                    {
                        MessageBox.Show("生成ROI： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("生成ROI：参数设置错误，请重新设置");

                    }

                    // label_infotext.Text = "生成ROI区域完成";
                    listBox_info.Items.Add("生成ROI区域完成");
                    break;
                case "GeneralMorphology":
                    GeneralMorphology R_generalmorphology = (GeneralMorphology)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (R_generalmorphology.Check_pal())
                    {
                        ExecuteBuffer executeBuffer_mor = new ExecuteBuffer();
                        R_generalmorphology.Run_Mor(_executeBuffer[Thread_number], out executeBuffer_mor);
                        //R_generalmorphology.(_executeBuffer[Thread_number], out executeBuffer_mor);
                        _executeBuffer[Thread_number] = executeBuffer_mor;
                        Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_generalmorphology.getRegionoutname()]);
                    }
                    else
                        listBox_info.Items.Add("通用形态学： 参数设置错误,请重新设置");


                    break;
                case "SmoothTest":
                    SmoothTest R_smooth = (SmoothTest)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (R_smooth.Check_pal())
                    {
                        ExecuteBuffer smooth_buffer = new ExecuteBuffer();

                        if (!R_smooth.Run_Smooth(_executeBuffer[Thread_number], out smooth_buffer))
                            break;
                        _executeBuffer[Thread_number] = smooth_buffer;
                        Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_smooth.get_Region()]);
                    }
                    else
                    {
                        MessageBox.Show("平滑处理：  参数设置错误,请重新设置");
                        listBox_info.Items.Add("参数错误,重新设置");
                        break;
                    }


                    listBox_info.Items.Add("平滑处理：完成");
                    break;
                case "RegionProcess":
                    RegionProcess R_regionprocess = (RegionProcess)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (R_regionprocess.Check_pal())
                    {
                        ExecuteBuffer regionout_buffer = new ExecuteBuffer();
                        Dictionary<int, PointName> Tem_outxPointlist = new Dictionary<int, PointName>();
                        R_regionprocess.Run_Region(_executeBuffer[Thread_number], out regionout_buffer, out out_result_info, Tem_Pointlist, out Tem_outxPointlist);
                        //R_generalmorphology.(_executeBuffer[Thread_number], out executeBuffer_mor);
                        _executeBuffer[Thread_number] = regionout_buffer;
                        // Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_regionprocess.get_Regionout()]);
                    }
                    else
                        MessageBox.Show("区域处理：  参数设置错误,请重新设置");



                    break;
                case "Transformation":
                    Transformation R_transformation = (Transformation)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (R_transformation.Check_pal())
                    {
                        Dictionary<int, PointName> Tem_outPointlist = new Dictionary<int, PointName>();
                        R_transformation.Run_transform(_executeBuffer[Thread_number], Pointlist_halcon, Modellist_halcon, Circlelist_halcon, Tem_Pointlist, out Tem_outPointlist);
                        Tem_Pointlist = Tem_outPointlist;

                    }
                    else
                    {
                        MessageBox.Show("坐标转换：  参数设置错误,请重新设置");
                        listBox_info.Items.Add("坐标转换：参数设置错误,请重新设置");

                    }

                    listBox_info.Items.Add("坐标转换：完成");
                    break;
                case "MathCalc":
                    MathCalc R_mathcalc = (MathCalc)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (R_mathcalc.Check_pal())
                    {
                        Dictionary<int, PointName> Tem_out1Pointlist = new Dictionary<int, PointName>();
                        R_mathcalc.Run_transform(_executeBuffer[Thread_number], Pointlist_halcon, Modellist_halcon, Circlelist_halcon, Tem_Pointlist, out Tem_out1Pointlist);
                        Tem_Pointlist = Tem_out1Pointlist;
                    }
                    else
                    {
                        MessageBox.Show("算术计算： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("算术计算：参数设置错误,请重新设置");

                    }

                    listBox_info.Items.Add("算术计算：完成");
                    break;
                case "PointLine":
                    PointLine R_pointline = (PointLine)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (R_pointline.Check_pal())
                    {
                        Dictionary<int, PointName> Tem_out2Pointlist = new Dictionary<int, PointName>();
                        R_pointline.Run_transform(_executeBuffer[Thread_number],  Modellist_halcon, Circlelist_halcon, Tem_Pointlist, out Tem_out2Pointlist,Linelist);
                        Tem_Pointlist = Tem_out2Pointlist;
                    }
                    else
                    {
                        MessageBox.Show("算术计算： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("算术计算：参数设置错误,请重新设置");

                    }

                    listBox_info.Items.Add("算术计算：完成");
                    break;
                case "LineLine":
                    LineLine R_lineline = (LineLine)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (R_lineline.Check_pal())
                    {
                        Dictionary<int, PointName> Tem_out2linelist = new Dictionary<int, PointName>();
                        R_lineline.Run_transform(_executeBuffer[Thread_number], Modellist_halcon, Circlelist_halcon, Tem_Pointlist, out Tem_out2linelist, Linelist);
                        Tem_Pointlist = Tem_out2linelist;
                    }
                    else
                    {
                        MessageBox.Show("算术计算： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("算术计算：参数设置错误,请重新设置");

                    }

                    listBox_info.Items.Add("算术计算：完成");
                    break;
                case "PuzzleData":
                    PuzzleData R_puzzledata = (PuzzleData)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (R_puzzledata.Check_pal())
                    {
                        Dictionary<int, PointName> Tem_out3Pointlist = new Dictionary<int, PointName>();
                        R_puzzledata.Run_transform(Modellist_halcon, Tem_Pointlist, out Tem_out3Pointlist);
                        Tem_Pointlist = Tem_out3Pointlist;
                    }
                    else
                    {
                        MessageBox.Show("拼图数据： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("拼图数据：参数设置错误,请重新设置");

                    }

                    listBox_info.Items.Add("拼图数据：完成");
                    break;
                case "FocusPoint":
                    FocusPoint R_focuspoint = (FocusPoint)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    if (R_focuspoint.Check_pal())
                    {
                        Dictionary<int, PointName> Tem_out4Pointlist = new Dictionary<int, PointName>();
                        R_focuspoint.Run_transform(_executeBuffer[Thread_number], Modellist_halcon, Tem_Pointlist, out Tem_out4Pointlist, out out_result_info);
                        Tem_Pointlist = Tem_out4Pointlist;
                    }
                    else
                    {
                        MessageBox.Show("焦点： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("焦点：参数设置错误,请重新设置");

                    }

                    listBox_info.Items.Add("焦点：完成");
                    break;
                case "RegionInterest":
                    RegionInterest R_regioninterest = (RegionInterest)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();

                    if (R_regioninterest.Check_pal())
                    {
                        ExecuteBuffer executeBuffer_regioninterest = new ExecuteBuffer();
                        Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_regioninterest.get_Imagein()]);
                        Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_regioninterest.get_Regionin() + ".region"]);
                        if (!R_regioninterest.Run_Region(_executeBuffer[Thread_number], out executeBuffer_regioninterest))
                            return;
                        // 
                        else
                            Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_regioninterest.get_Regionout()]);


                    }
                    else
                    {
                        MessageBox.Show("感兴趣区域： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("参数设置错误,请重新设置");
                        break;
                    }

                    listBox_info.Items.Add("感兴趣区域：完成");
                    break;
                case "ReadQRcode":
                    ReadQRcode R_readqrcode = (ReadQRcode)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    字符串GVName_halcon xx_string = new 字符串GVName_halcon();
                    Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_readqrcode.get_Imagein()]);
                    if (R_readqrcode.Check_pal())
                    {
                        ExecuteBuffer executeBuffer_regioninterest = new ExecuteBuffer();
                        R_readqrcode.Run_Region(_executeBuffer[Thread_number], out xx_string, hWndCtrl);
                        int number_charlist1 = Charlist.Count;
                        for (int ix = 0; ix < xx_string.字符串.Length; ix++)
                        {
                            Charlist.Add(new 字符串GVName((ix + 1 + number_charlist1).ToString(), xx_string.字符串[ix]));
                        }
                        

                    }
                    else
                    {
                        MessageBox.Show("二维码读取： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("参数设置错误,请重新设置");
                        break;
                    }

                    listBox_info.Items.Add("二维码读取：完成");
                    break;
                case "ReadBarcode":
                    ReadBarcode R_readbarcode = (ReadBarcode)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    字符串GVName_halcon yy_string = new 字符串GVName_halcon();
                    Display_Graphic(_executeBuffer[Thread_number].imageBuffer[R_readbarcode.get_Imagein()]);
                    if (R_readbarcode.Check_pal())
                    {
                        int number_charlist2 = Charlist.Count;
                        ExecuteBuffer executeBuffer_regioninterest = new ExecuteBuffer();
                        R_readbarcode.Run_Region(_executeBuffer[Thread_number], out yy_string, hWndCtrl);
                        for (int ix = 0; ix < yy_string.字符串.Length; ix++)
                        {
                            Charlist.Add(new 字符串GVName((ix + 1 + number_charlist2).ToString(), yy_string.字符串[ix]));
                        }


                    }
                    else
                    {
                        MessageBox.Show("条形码读取： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("参数设置错误,请重新设置");
                        break;
                    }

                    listBox_info.Items.Add("条形码读取：完成");
                    break;
                case "PointShow":
                    listBox_info.Items.Clear();
                    PointShow R_pointshow = (PointShow)ListTool[comboBox1_SelectedIndex][snumber];
                    if (R_pointshow.Check_pal())
                    {

                        R_pointshow.Run_show(_executeBuffer[Thread_number], Modellist_halcon, Tem_Pointlist, hWndCtrl, out out_result_info);
                        listBox_info.Items.Add(out_result_info);
                    }
                    else
                    {
                        MessageBox.Show("点位显示： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("点位显示：参数设置错误,请重新设置");

                        return;
                    }

                    break;
                case "TestResultsShow":
                    listBox_info.Items.Clear();
                    TestResultsShow R_testresultsshow = (TestResultsShow)ListTool[comboBox1_SelectedIndex][snumber];


                        R_testresultsshow.Run_show("",Tem_Circlelist,Tem_CircleArclist,Tem_Linelist, hWndCtrl, out out_result_info);
                        listBox_info.Items.Add(out_result_info);
                    


                    break;
                case "FindLine":
                    FindLine R_findline = (FindLine)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    HTuple Row1, Column1, Row2, Column2;
                    int count_line = Linelist.Count;
                    if (R_findline.Check_pal())
                    {
                        if (R_findline.Find_halcon_line(_executeBuffer[Thread_number], hWndCtrl,Modellist_halcon,Tem_Pointlist, out out_result_info, out Row1, out Column1, out Row2, out Column2,false))
                        {
                            for (int imx = 0; imx < Row1.TupleLength(); imx++)
                            {
                                直线GVName newline = new 直线GVName((count_line+imx).ToString(), Row1[imx].D.ToString(), Column1[imx].D.ToString(), Row2[imx].D.ToString(), Column2[imx].D.ToString());
                                Linelist.Add(newline);
                                //count_line++;
                            }

                        }
                        show_result("show_直线");
                        listBox_info.Items.Add(out_result_info);
                    }
                    else
                    {
                        MessageBox.Show("查找直线： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("查找直线：参数设置错误,请重新设置");

                        return;
                    }

                    break;
                case "FindCircle":
                    FindCircle R_findcircle = (FindCircle)ListTool[comboBox1_SelectedIndex][snumber];
                    listBox_info.Items.Clear();
                    HTuple Row_circle, Column_circle,Radius_circle;
                    int count_circle = Circlelist.Count;
                    if (R_findcircle.Check_pal())
                    {
                        if (R_findcircle.Find_halcon_circle(_executeBuffer[Thread_number], hWndCtrl ,Modellist_halcon,Tem_Pointlist,out out_result_info, out Row_circle, out Column_circle, out Radius_circle,false))
                        {
                            for (int imx = 0; imx < Row_circle.TupleLength(); imx++)
                            {
                                圆GVName newcircle = new 圆GVName((count_circle+imx).ToString(), Row_circle[imx].D.ToString(), Column_circle[imx].D.ToString(), Radius_circle[imx].D.ToString());
                                Circlelist.Add(newcircle);
                            }
                            show_result("show_圆");

                        }
                        listBox_info.Items.Add(out_result_info);
                    }
                    else
                    {
                        MessageBox.Show("查找圆： 参数设置错误,请重新设置");
                        listBox_info.Items.Add("查找圆：参数设置错误,请重新设置");

                        return;
                    }

                    break;
                default:
                    break;

            }


            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            label_时间.Text = ts.TotalMilliseconds.ToString();
        }


        private void 流程切換(bool Read_file)
        {



            dataGridView1.DataSource = null;

            if (comboBox1_SelectedIndex == ListTool.Count)
                comboBox1_SelectedIndex = ListTool.Count - 1;


            for (int i = 0; i < ListTool[comboBox1_SelectedIndex].Count; i++)
            {
                dataGridView1.DataSource = null;

                if (ListDataGV.Count != 0)
                    ListDataGV.RemoveAt(0);


                //連結資料表
                dataGridView1.DataSource = ListDataGV;
                dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
            if (Read_file)
                ListDataGV.Clear();
            if (comboBox1.SelectedIndex == -1)
                comboBox1_SelectedIndex = 0;
            else
                comboBox1_SelectedIndex = comboBox1.SelectedIndex;
            List<DataGVName> ListDataGV1 = new List<DataGVName>();
            for (int i = 0; i < ListTool[comboBox1_SelectedIndex].Count; i++)
            {
                dataGridView1.DataSource = null;
                int s = i;

                switch (ListTool[comboBox1_SelectedIndex][i].GetType().Name)
                {
                    case "ReadPictureControl":
                        ListDataGV.Insert(s, new DataGVName("取得图像"));
                        break;
                    case "SavePictureControl":
                        ListDataGV.Insert(s, new DataGVName("记录一张图片"));
                        break;
                    case "SaveShapeTem":
                        ListDataGV.Insert(s, new DataGVName("取形状模板(合)"));
                        break;
                    case "SaveGreyTem":
                        ListDataGV.Insert(s, new DataGVName("取灰度模板(合)"));
                        break;
                    case "FindScaledShapeModel":
                        ListDataGV.Insert(s, new DataGVName("形状模板匹配"));
                        break;
                    case "FindGreyModel":
                        ListDataGV.Insert(s, new DataGVName("灰度模板匹配"));
                        break;
                    case "BinaryPro":
                        ListDataGV.Insert(s, new DataGVName("二值化处理"));
                        break;
                    case "RoiMake":
                        ListDataGV.Insert(s, new DataGVName("ROI生成"));
                        break;
                    case "GeneralMorphology":
                        ListDataGV.Insert(s, new DataGVName("通用形态学"));
                        break;
                    case "SmoothTest":
                        ListDataGV.Insert(s, new DataGVName("平滑处理"));
                        break;
                    case "RegionProcess":
                        ListDataGV.Insert(s, new DataGVName("区域处理"));
                        break;
                    case "Transformation":
                        ListDataGV.Insert(s, new DataGVName("坐标转换"));
                        break;
                    case "MathCalc":
                        ListDataGV.Insert(s, new DataGVName("数学计算"));
                        break;
                    case "PointLine":
                        ListDataGV.Insert(s, new DataGVName("点线距离"));
                        break;
                    case "LineLine":
                        ListDataGV.Insert(s, new DataGVName("线线交点"));
                        break;
                    case "RegionInterest":
                        ListDataGV.Insert(s, new DataGVName("感兴趣区域"));
                        break;
                    case "PointShow":
                        ListDataGV.Insert(s, new DataGVName("点位显示"));
                        break;
                    case "TestResultsShow":
                        ListDataGV.Insert(s, new DataGVName("轮廓显示"));
                        break;
                    case "FindLine":
                        ListDataGV.Insert(s, new DataGVName("查找直线"));
                        break;
                    case "FindCircle":
                        ListDataGV.Insert(s, new DataGVName("查找圆"));
                        break;
                    case "NinePointCal":
                        ListDataGV.Insert(s, new DataGVName("九点标定"));
                        break;
                    case "MakeCalibration":
                        ListDataGV.Insert(s, new DataGVName("校正"));
                        break;
                    case "PuzzlePicture":
                        ListDataGV.Insert(s, new DataGVName("拼图"));
                        break;
                    case "PuzzleData":
                        ListDataGV.Insert(s, new DataGVName("拼图数据"));
                        break;
                    case "FocusPoint":
                        ListDataGV.Insert(s, new DataGVName("焦点"));
                        break;
                    case "ReadQRcode":
                        ListDataGV.Insert(s, new DataGVName("二维码读取"));
                        break;
                    case "ReadBarcode":
                        ListDataGV.Insert(s, new DataGVName("条形码读取"));
                        break;

                    default:
                        break;
                }
                dataGridView1.DataSource = null;
                ListDataGV1.Insert(s, ListDataGV[s]);
                //連結資料表
                dataGridView1.DataSource = ListDataGV1;
                dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }



        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int s = 0;
            s = this.dataGridView1.CurrentRow.Index; //取得選取位置


            //   ListToolADD(null, true);
            ChangeList(s);


        }



        /// <summary>
        /// 添加单个工具到流程中
        /// </summary>
        /// <param name="ListDataGV">工具流程的名称链表</param>
        /// <param name="ListTool">工具流程的实例链表</param>
        /// <param name="ToolStripMenuItem">工具对应的名称选项</param>
        /// <param name="s">添加的工具在流程中的添加位置</param>
        /// <param name="Action_copy">确认工具是新增还是复制粘贴选项</param>
        private void ADDSwitch(List<DataGVName> ListDataGV, List<List<Form>> ListTool, string ToolStripMenuItem, int s, bool Action_copy)
        {
            int Thread_Number = comboBox1_SelectedIndex;
            if (Action_copy)
            {
                string formName = "";

                formName = ListTool_backup.GetType().Name; //取得form類型
                switch (formName)
                {
                    case "ReadPictureControl":
                        ListDataGV.Insert(s, new DataGVName("取得图像"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        break;

                    case "BinaryPro":
                        ListDataGV.Insert(s, new DataGVName("二值化处理"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 2, 0);
                        break;
                    case "FindCircle":
                        ListDataGV.Insert(s, new DataGVName("查找圆"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "FindLine":
                        ListDataGV.Insert(s, new DataGVName("查找直线"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "FindScaledShapeModel":
                        ListDataGV.Insert(s, new DataGVName("形状轮廓匹配"));
                        FindScaledShapeModel Form_newfindscaleshapemodel = new FindScaledShapeModel(_executeBuffer[Thread_Number], false);
                        FindScaledShapeModel Form_oldfindscaleshapemodel = (FindScaledShapeModel)ListTool_backup;
                        // Form_newreadshape.Set_parameters(Form_oldreadshape.Get_parameters());
                        ListTool[Thread_Number].Insert(s, Form_newfindscaleshapemodel);
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "NinePointCal":
                        ListDataGV.Insert(s, new DataGVName("九点标定"));
                        NinePointCal Form_newninepointcal = new NinePointCal();
                        NinePointCal Form_oldninepointcal = (NinePointCal)ListTool_backup;
                        ListTool[Thread_Number].Insert(s, Form_newninepointcal);
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "MakeCalibration":
                        ListDataGV.Insert(s, new DataGVName("校正"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        break;
                    case "RoiMake":
                        ListDataGV.Insert(s, new DataGVName("ROI生成"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        break;
                    case "GeneralMorphology":
                        ListDataGV.Insert(s, new DataGVName("通用形态学"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 3, 0);
                        break;
                    case "SmoothTest":
                        ListDataGV.Insert(s, new DataGVName("平滑处理"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        break;
                    case "RegionProcess":
                        ListDataGV.Insert(s, new DataGVName("区域处理"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        break;
                    case "Transformation":
                        ListDataGV.Insert(s, new DataGVName("坐标转换"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "MathCalc":
                        ListDataGV.Insert(s, new DataGVName("算术计算"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "PointLine":
                        ListDataGV.Insert(s, new DataGVName("点线距离"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "LineLine":
                        ListDataGV.Insert(s, new DataGVName("线线交点"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "PointShow":
                        ListDataGV.Insert(s, new DataGVName("点位显示"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "TestResultsShow":
                        ListDataGV.Insert(s, new DataGVName("点位显示"));
                        TestResultsShow Form_newtestresultsshow = (TestResultsShow)ListTool_backup;
                        ListTool[Thread_Number].Insert(s, Form_newtestresultsshow);
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "RegionInterest":
                        ListDataGV.Insert(s, new DataGVName("感兴趣区域"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        break;
                    case "SaveShapeTem":
                        ListDataGV.Insert(s, new DataGVName("取形状模板(合)"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "SaveGreyTem":
                        ListDataGV.Insert(s, new DataGVName("取灰度模板(合)"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "ReadBarcode":
                        ListDataGV.Insert(s, new DataGVName("条形码读取"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "ReadQRcode":
                        ListDataGV.Insert(s, new DataGVName("二维码读取"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "PuzzlePicture":
                        ListDataGV.Insert(s, new DataGVName("图像拼接"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "FocusPoint":
                        ListDataGV.Insert(s, new DataGVName("焦点"));
                        ListTool[Thread_Number].Insert(s, DeepCopyByBinary(ListTool_backup));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    default:
                        break;

                }

            }
            else
            {


                switch (ToolStripMenuItem)
                {


                    case "button_Readimg":
                        ListDataGV.Insert(s, new DataGVName("取得图像"));
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        ListTool[Thread_Number].Insert(s, new ReadPictureControl(_executeBuffer[Thread_Number], false));


                        break;
                    case "button_Binary":
                        ListDataGV.Insert(s, new DataGVName("二值化处理"));
                        BinaryPro A_binarypro = new BinaryPro(_executeBuffer[Thread_Number], false);
                        A_binarypro.SetParaImage(_executeBuffer[Thread_Number]);
                        ListTool[Thread_Number].Insert(s, A_binarypro);
                        Add_test_buffer(s, Thread_Number, 2, 0);
                        break;
                    case "button_Modelmatching":
                        ListDataGV.Insert(s, new DataGVName("形状轮廓匹配"));
                        FindScaledShapeModel A_newfindscalemodel = new FindScaledShapeModel(_executeBuffer[Thread_Number], false);
                        A_newfindscalemodel.SetParawindow(_executeBuffer[Thread_Number]);
                        A_newfindscalemodel.SetParaImage(_executeBuffer[Thread_Number]);
                        ListTool[Thread_Number].Insert(s, A_newfindscalemodel);
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_Greymatching":
                        ListDataGV.Insert(s, new DataGVName("灰度轮廓匹配"));
                        FindGreyModel A_newfindgreymodel = new FindGreyModel(_executeBuffer[Thread_Number], false);
                        A_newfindgreymodel.SetParawindow(_executeBuffer[Thread_Number]);
                        A_newfindgreymodel.SetParaImage(_executeBuffer[Thread_Number]);
                        ListTool[Thread_Number].Insert(s, A_newfindgreymodel);
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_Ninecal":
                        ListDataGV.Insert(s, new DataGVName("九点标定"));
                        ListTool[Thread_Number].Insert(s, new NinePointCal());
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_MakeCalibration":
                        ListDataGV.Insert(s, new DataGVName("校正"));
                        ListTool[Thread_Number].Insert(s, new MakeCalibration());
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        break;
                    case "button_Puzzle":
                        ListDataGV.Insert(s, new DataGVName("拼图"));
                        ListTool[Thread_Number].Insert(s, new PuzzlePicture(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        break;
                    case "button_Makeroi":
                        ListDataGV.Insert(s, new DataGVName("ROI生成"));
                        ListTool[Thread_Number].Insert(s, new RoiMake(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        break;
                    case "button_Morph":
                        ListDataGV.Insert(s, new DataGVName("通用形态学"));
                        ListTool[Thread_Number].Insert(s, new GeneralMorphology(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 3, 0);
                        break;
                    case "button_Smooth":
                        ListDataGV.Insert(s, new DataGVName("平滑处理"));
                        ListTool[Thread_Number].Insert(s, new SmoothTest(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        break;
                    case "button_RegionPro":
                        ListDataGV.Insert(s, new DataGVName("区域处理"));
                        ListTool[Thread_Number].Insert(s, new RegionProcess(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 2, 0);
                        break;
                    case "button_Transform":
                        ListDataGV.Insert(s, new DataGVName("坐标转换"));
                        ListTool[Thread_Number].Insert(s, new Transformation(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_Calc":
                        ListDataGV.Insert(s, new DataGVName("算术计算"));
                        ListTool[Thread_Number].Insert(s, new MathCalc(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_Displ":
                        ListDataGV.Insert(s, new DataGVName("点线距离"));
                        ListTool[Thread_Number].Insert(s, new PointLine(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_LineLine":
                        ListDataGV.Insert(s, new DataGVName("线线交点"));
                        ListTool[Thread_Number].Insert(s, new LineLine(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;

                    case "button_RegionInterest":
                        ListDataGV.Insert(s, new DataGVName("感兴趣区域"));
                        ListTool[Thread_Number].Insert(s, new RegionInterest(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 1, 0);
                        break;
                    case "button_Pointshow":
                        ListDataGV.Insert(s, new DataGVName("点位显示"));
                        ListTool[Thread_Number].Insert(s, new PointShow(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_Contour":
                        ListDataGV.Insert(s, new DataGVName("显示轮廓"));
                        ListTool[Thread_Number].Insert(s, new TestResultsShow());
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_Contourmodel":
                        ListDataGV.Insert(s, new DataGVName("取形状模板(合)"));
                        ListTool[Thread_Number].Insert(s, new SaveShapeTem(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_Greymodel":
                        ListDataGV.Insert(s, new DataGVName("取灰度模板(合)"));
                        ListTool[Thread_Number].Insert(s, new SaveGreyTem(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_Findline":
                        ListDataGV.Insert(s, new DataGVName("查找直线"));
                        ListTool[Thread_Number].Insert(s, new FindLine(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_Findcircle":
                        ListDataGV.Insert(s, new DataGVName("查找圆"));
                        ListTool[Thread_Number].Insert(s, new FindCircle(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_PuzzleData":
                        ListDataGV.Insert(s, new DataGVName("拼图数据"));
                        ListTool[Thread_Number].Insert(s, new PuzzleData());
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_Focus":
                        ListDataGV.Insert(s, new DataGVName("焦点"));
                        ListTool[Thread_Number].Insert(s, new FocusPoint(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_QRcode":
                        ListDataGV.Insert(s, new DataGVName("二维码读取"));
                        ListTool[Thread_Number].Insert(s, new ReadQRcode(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    case "button_Barcode":
                        ListDataGV.Insert(s, new DataGVName("条形码读取"));
                        ListTool[Thread_Number].Insert(s, new ReadBarcode(_executeBuffer[Thread_Number], false));
                        Add_test_buffer(s, Thread_Number, 0, 0);
                        break;
                    default:

                        break;
                }
            }
        }

        private void button_datamodel_Click(object sender, EventArgs e)
        {

        }

        private void 结果显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resultshow_setting = !Resultshow_setting;
            if (Resultshow_setting)
                panel4.BringToFront();
            else
                panel4.SendToBack();
        }

        private void 结果隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.SendToBack();
        }

        private void 轨迹显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (accept != null)
            {
                accept(this, EventArgs.Empty);
            }
        }

        private void 实时显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button_实时显示.Enabled = false;
            if (CurCam_IniStatus)
            {
                Realtime_display = true;
                Cam_ContinuousTake(true);
            }
            else
                MessageBox.Show("相机未打开");
            button_实时显示.Enabled = true;
        }

        private void hWindowControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("bad");
        }

        private void 相机ToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要保存吗?", "保存系统", messButton);

            if (dr == DialogResult.OK)//如果点击“确定”按钮
            {

                儲存(CommonData.Path);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button split_but = sender as Button;
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            Panel_zhedie = false;
            Ensure_step = false;
            ListToolADD("button_Readimg", false);
            ListToolADD("button_Contourmodel", false);
            Ensure_step = true;

        }











    }


}
