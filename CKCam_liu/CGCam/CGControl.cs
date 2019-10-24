using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CGCam
{
    public partial class CGControl: UserControl
    {

        /// <summary>
        /// 鼠标中键事件
        /// </summary>
        /// <param name="imgx"></param>
        /// <param name="imgy"></param>
        /// 

        public delegate void MouseMidleClick(double imgx, double imgy);
        public event MouseMidleClick OnMouseMidleClick;
        public static string CamModelName;
        public static bool CurCam_IniStatus; 
        private Bitmap m_bitmap = null; /* The bitmap is used for displaying the image. */
        public static string theFirstCamIp = "";
        public static string theSecondCamIp = "";
        public static uint theFirstCamIndex = 0;
        public static uint theSecondCamIndex = 0;
        public string Cam_ModelType = "形状匹配";
        public string Cam_TemModelType = "形状匹配";
        public double Cam_RoiLeft=0;
        public double Cam_RoiTop = 0;
        public double Cam_RoiRight = 0;
        public double Cam_RoiBottom = 0;
        private int ImageWidth = 2592, ImageHeight = 1944, ImageFormat;
        public double Cam_AoiLeft = 0;
        public double Cam_AoiTop = 0;
        public double Cam_AoiRight = 0;
        public double Cam_AoiBottom = 0;
        public double ResultAngle = 0, ResultPosX = 0, ResultPosY = 0, ResultScore = 0;
        public int X = 0, Y = 0;
        private static CGControl instance = null;
        private static readonly object padlock = new object();
        public static CGControl Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new CGControl();
                    }
                    return instance;
                }
            }
        }
    
        public CGControl()
        {                   
            InitializeComponent();
            ImageWidth = 0; ImageHeight = 0; ImageFormat = 0;
            ModelTypecombo.Items.Clear();
            ModelTypecombo.Items.Add("形状匹配");
            ModelTypecombo.Items.Add("灰度匹配");
            ModelTypecombo.SelectedIndex = 0;
            BaslerControlIni();           
        }





        private void BaslerControlIni()
        {
          
        }

        #region 相机按钮事件
        private void CamFormbut_Click(object sender, EventArgs e)
        {
            Button but = sender as Button;

        }

        #endregion

        #region 相机对外事件

        private string _CamID = "1";
        public string CamID
        {
            get { return _CamID; }
            set { _CamID = value; }
        }
        public bool Cam_Ini()
        {
            try
            {
                if (CurCam_IniStatus == false)
                {
                    Basler_Ini(CamID);
                    Cam_ContinuousTake(true);
                    CurCam_IniStatus = true;
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
        }
        public void Cam_OpenImage()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "bmp files (*.bmp)|*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strPathName = openFileDialog1.FileName;

            }
        }
        public void Cam_SaveImage()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                   string strPathName = saveFileDialog1.FileName;

                }
                catch (Exception ex)
                {
                   // MessageBox.Show("图片保存失败");
                }
            }
        }
        public void Cam_SingleTake()
        {
            Basler_GrabOne();
        }
        public void Cam_ContinuousTake(bool SW)
        {
            if (SW)
            {
                Basler_StartGrabbing();
            }
            else
            {
                Basler_StopGrabb();
            }
        }
        public bool Cam_ReadModel(string Path,bool IsShowModelImage)
        {
            try
            {
                string ModelPath = Path + ".model";
                string PicturePath = Path + ".bmp";
                string ProductPath = Path;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
         
        }
        public bool Cam_MatchingModel()
        {
            bool Result;

            try
            {
                if (Cam_ModelType == "灰度匹配")
                {


                }


            }
            catch (Exception)
            {
                Xlabel.Text = "X:" + ResultPosX.ToString("F3");
                Ylabel.Text = "Y:" + ResultPosY.ToString("F3");
                Alabel.Text = "A:" + ResultAngle.ToString("F3");
                Slabel.Text = "S:" + ResultScore.ToString("F3");
                Result = false;
            }

            //axCkvsModel1.RoiColor = Color.Blue;
            //axCkvsModel1.RoiLeft = Cam_RoiLeft;
            //axCkvsModel1.RoiTop = Cam_RoiTop;
            //axCkvsModel1.RoiRight = Cam_RoiRight;
            //axCkvsModel1.RoiBottom = Cam_RoiBottom;
            //axCkvsDisplay1.AddRoi(axCkvsModel1.GetRoi());
            //axCkvsModel1.ClearResults();
            //ResultPosX = 0; ResultAngle = 0; ResultPosY = 0; ResultScore = 0;
            //axCkvsModel1.Execute(axCkvsDisplay1.GetImage());
            //if (axCkvsModel1.GetMatchCount() > 0)
            //{
            //    ResultPosX = axCkvsModel1.GetMatchPositionX(0);
            //    ResultPosY = axCkvsModel1.GetMatchPositionY(0);
            //    ResultAngle = axCkvsModel1.GetMatchAngle(0);
            //    ResultScore = axCkvsModel1.GetMatchScore(0);

            //    Xlabel.Text = "X:" + ResultPosX.ToString("F3");
            //    Ylabel.Text = "Y:" + ResultPosY.ToString("F3");
            //    Alabel.Text = "A:" + ResultAngle.ToString("F3");
            //    Slabel.Text = "S:" + ResultScore.ToString("F3");
            //    Result = true;
            //}
            //else
            //{
            //    Xlabel.Text = "X:" + ResultPosX.ToString("F3");
            //    Ylabel.Text = "Y:" + ResultPosY.ToString("F3");
            //    Alabel.Text = "A:" + ResultAngle.ToString("F3");
            //    Slabel.Text = "S:" + ResultScore.ToString("F3");
            //    Result = false;
            //}

            return false;
        }
        public List<string> Cam_GetImageInfo()
        {
            string CurCamPix = "";
            List<string> ArrayCamPix = new List<string>();
            CurCamPix = ResultPosX.ToString("F3") +"," + ResultPosY.ToString("F3") + "," + ResultAngle.ToString("F3") + "," + ResultScore.ToString("F3");             
            ArrayCamPix.Add(CurCamPix + ".");                     
            return ArrayCamPix;
        }
        public string GetCamModelName()
        {
            return CamModelName;
        }
        #endregion

        #region 菜单
        public void Menu_ItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem but = sender as ToolStripMenuItem;
            switch (but.Name)
            {
                #region 文件
                case "MenuFile_Open":
                
                    break;
                case "MenuFile_Save":

                    break;

                case "MenuFile_SavePicture":
                    Cam_SaveImage();
                    break;
                case "MenuFile_LoadPicture":
                     Cam_OpenImage();
                    break;
                #endregion
                #region 编辑
                case "MenuEdit_New":
            
                    break;
                #endregion
                #region 工具
                case "MenuTool_CamOpen":
                    Cam_Ini();
                    break;
                case "MenuTool_CamClosed":
                    Cam_Close();
                    break;
                case "MenuTool_TakeImage":
                    Cam_SingleTake();
                    break;
                case "MenuTool_Match":
                
                    break;
                case "MenuTool_Video":
                    Cam_ContinuousTake(true);
                    break;
                case "MenuTool_ShowCross":
                    break;
                #endregion

            }
        }
        #endregion

        #region 控件操作
  


        #endregion

        #region Basler

      
        public bool Basler_Ini(string ID)
        {
            try
            {


                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Basler_Closed()
        {
            try
            {

                return true;
            }
            catch (Exception EX)
            {
                return false;
            }
        }
        public bool Basler_GrabOne()
        {
            try
            {

                return true;
            }
            catch(Exception)
            { return false; }           
        }
        public bool Basler_StartGrabbing()
        {

            return true;
        }
        public bool Basler_StopGrabb()
        {

            return true;
        }
        private void GrabTime(long time)
        {

        }
        private void VideoImage(Bitmap image, IntPtr ptrBmp)
        {
            try
            {
            }
            catch (Exception)
            { }
        }     
        public void delay(float Interval)
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

        private void ModelTypecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cam_TemModelType = ModelTypecombo.Text;
        }

    }
}
