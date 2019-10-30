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
using System.IO;
using System.Text.RegularExpressions;
using ViewROI;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.Serialization; 




namespace PMACam
{
    [Serializable]
    public partial class SaveGreyTem : Form, ISerializable
    {

  
       // HShapeModel ShapeModel;

        double Row, Column;
       // HWindow Display_window;
        public List<ViewROI .ROI> _regions = null;
        [DllImport("kernel32.dll")]
        public static extern void CopyMemory(int Destination, int Source, int Length);
        HTuple hv_MovementOfObject;

        private static string rootPath = System.Windows.Forms.Application.StartupPath;

        public List<ViewROI.ROI> regions;
        public List<ViewROI.ROI> sou_regions;
        public List<ViewROI.ROI> sousuo_regions;
        private HTuple ModelIDGrey = new HTuple();
        public  HTuple ModelID = new HTuple();
        public List<ViewROI.ROI> model_regions;
        ROI_Grey_Model Model_region = new ROI_Grey_Model();
        ROI_Grey_Model Sousuo_region_get = new ROI_Grey_Model();
        public bool SetROI = false;
        bool Button_press = false;
        public bool Copy_check;
        public bool Train_image = false;
        private HObject ModelImage = new HObject();
        private HObject ModelImage_full = new HObject();
        private HObject ModelImage_fullmodel = new HObject();



        public HObject Copypicture(HObject pic)
        {


            HObject Rectangelx, imageReduced, ImagePart;
            HTuple widthx, heightx;
            HOperatorSet.GetImageSize(pic, out widthx, out heightx);
            HOperatorSet.GenRectangle1(out Rectangelx, 0, 0, heightx, widthx);
            HOperatorSet.ReduceDomain(pic, Rectangelx, out imageReduced);
            HOperatorSet.CropDomain(imageReduced, out ImagePart);
            return ImagePart;

        }
        public SaveGreyTem(ExecuteBuffer buffer, bool addbuffer)
        {
            InitializeComponent();



            this.model_subPixel.SelectedIndex = 0;
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.button4.Enabled = false;

            if (addbuffer)
            {
                if (buffer != null)
                {
                    SetParaImage(buffer);
                  //  SetParaImage(buffer);

                }
            }

        }
        public bool getROI()
        {
            return SetROI;
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
        internal void WriteData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            ViewROI.ROIRectangle1 Rec1;
            ViewROI.ROIRectangle2 Rec2;
            ViewROI.ROICircle Cir1;
            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);

            if (ModelIDGrey.Length == 0)
                IniFile.IniWriteValue(j.ToString(), "Model_Not_Null", "0");
            else
            {
                IniFile.IniWriteValue(j.ToString(), "Model_Not_Null", "1");
                try
                {
                    string MOD_PATH = n_Path[0] + "\\Model_" + j + ".shm";
                    if (File.Exists(MOD_PATH))
                    {
                        try
                        {
                            File.Delete(MOD_PATH);
                        }
                        catch (Exception)
                        {

                            return;
                        }

                    }
                    HOperatorSet.WriteTemplate(ModelIDGrey, n_Path[0] + "\\Model_" + j + ".shm");
                    WriteModelRegion(n_Path[0] + "\\Model_" + j + ".ini");
                    HOperatorSet.WriteImage(ModelImage, "bmp", 0, n_Path[0] + "\\Model_" + j);
                    HOperatorSet.WriteImage(ModelImage_fullmodel, "bmp", 0, n_Path[0] + "\\Modelx_" + j);
                }
                catch (Exception)
                {
                    MessageBox.Show("流程[" + (j + 1) + "] 模板保存失敗");
                }
            }


            if (this.cbb_image.SelectedItem != null)
                IniFile.IniWriteValue(j.ToString(), "InputImage", this.cbb_image.SelectedItem.ToString());
            else
                IniFile.IniWriteValue(j.ToString(), "InputImage", "000000");

            IniFile.IniWriteValue(j.ToString(), "Region_type", this.comboBox2.SelectedIndex.ToString());
            if (this.sou_regions != null)
            {
                if (this.sou_regions.Count != 0)
                {
                    if (sou_regions[0].GetType().ToString() == "ViewROI.ROIRectangle1")
                    {

                        IniFile.IniWriteValue(j.ToString(), "Region_Not_null", "ROIRectangle1");
                        Rec1 = sou_regions[0] as ViewROI.ROIRectangle1;
                        IniFile.IniWriteValue(j.ToString(), "Rec1row1", Rec1.row1.ToString());
                        IniFile.IniWriteValue(j.ToString(), "Rec1col1", Rec1.col1.ToString());
                        IniFile.IniWriteValue(j.ToString(), "Rec1row2", Rec1.row2.ToString());
                        IniFile.IniWriteValue(j.ToString(), "Rec1col2", Rec1.col2.ToString());


                    }
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




            IniFile.IniWriteValue(j.ToString(), "Label_path", this.label_path.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "InputImage", this.cbb_image.SelectedItem.ToString());
            IniFile.IniWriteValue(j.ToString(), "Angle_start", this.txt_angleStart.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Angle_end", this.txt_angleExtent.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Grey_dis", this.txt_greydis.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "Max_error", this.txt_maxerror.Text.ToString());
            IniFile.IniWriteValue(j.ToString(), "NumMatches", this.txt_numMatches.Text.ToString());

            IniFile.IniWriteValue(j.ToString(), "ButtonPress", Button_press.ToString());
            IniFile.IniWriteValue(j.ToString(), "SubPixel", this.model_subPixel.SelectedItem.ToString());
            IniFile.IniWriteValue(j.ToString(), "ModelRegionClass", this.comboBox1.SelectedIndex.ToString());
            IniFile.IniWriteValue(j.ToString(), "SouRegionClass", this.comboBox2.SelectedIndex.ToString());
        }








        public void WriteModelRegion(string path_region)
        {
            IniFile IniFile_region = new IniFile(path_region);
            IniFile_region.IniWriteValue("region", "ModelType", Model_region.Roi_model);
            if (Model_region.Roi_model == "ViewROI.ROIRectangle2")
            {
                IniFile_region.IniWriteValue("region", "Rec2length1", Model_region.Roi_Rec2length1.ToString());
                IniFile_region.IniWriteValue("region", "Rec1length2", Model_region.Roi_Rec2length2.ToString());
                IniFile_region.IniWriteValue("region", "Rec2phi", Model_region.Roi_Rec2phi.ToString());
                IniFile_region.IniWriteValue("region", "Rec2x", Model_region.Roi_x.ToString());
                IniFile_region.IniWriteValue("region", "Rec2y", Model_region.Roi_y.ToString());
            }
            else if (Model_region.Roi_model == "ViewROI.ROICircle")
            {
                IniFile_region.IniWriteValue("region", "Cir1radius", Model_region.Roi_Cir1radius.ToString());
                IniFile_region.IniWriteValue("region", "Rec2x", Model_region.Roi_x.ToString());
                IniFile_region.IniWriteValue("region", "Rec2y", Model_region.Roi_y.ToString());

            }

        }



        public void ReadModelRegion(string path_region)
        {
            IniFile IniFile_region_read = new IniFile(path_region);
            Model_region.Roi_model = IniFile_region_read.IniReadValue("region", "ModelType");
            if (Model_region.Roi_model == "ViewROI.ROIRectangle2")
            {

                Model_region.Roi_Rec2length1 = Convert.ToDouble(IniFile_region_read.IniReadValue("region", "Rec2length1"));
                Model_region.Roi_Rec2length2 = Convert.ToDouble(IniFile_region_read.IniReadValue("region", "Rec1length2"));
                Model_region.Roi_Rec2phi = Convert.ToDouble(IniFile_region_read.IniReadValue("region", "Rec2phi"));
                if (IniFile_region_read.IniReadValue("region", "Rec2x") != "")
                    Model_region.Roi_x = Convert.ToDouble(IniFile_region_read.IniReadValue("region", "Rec2x"));
                if (IniFile_region_read.IniReadValue("region", "Rec2y") != "")
                    Model_region.Roi_y = Convert.ToDouble(IniFile_region_read.IniReadValue("region", "Rec2y"));

            }
            else if (Model_region.Roi_model == "ViewROI.ROICircle")
            {

                Model_region.Roi_Cir1radius = Convert.ToDouble(IniFile_region_read.IniReadValue("region", "Cir1radius"));
                if (IniFile_region_read.IniReadValue("region", "Rec2x") != "")
                    Model_region.Roi_x = Convert.ToDouble(IniFile_region_read.IniReadValue("region", "Rec2x"));
                if (IniFile_region_read.IniReadValue("region", "Rec2y") != "")
                    Model_region.Roi_y = Convert.ToDouble(IniFile_region_read.IniReadValue("region", "Rec2y"));

            }

        }
        public string get_Imagename()
        {
            if (this.cbb_image.SelectedItem != null)
                return this.cbb_image.SelectedItem.ToString();
            else
                return null;


        }
        public void Writeshm(string n_Path)
        {

            try
            {
                string MOD = n_Path + "\\Model_"  + "xx.shm";
                if (File.Exists(MOD))
                {
                    try
                    {
                        File.Delete(MOD);
                    }
                    catch (Exception)
                    {

                        return;
                    }

                }

                HOperatorSet.WriteShapeModel(ModelID, MOD);

            }
            catch (Exception)
            {
                MessageBox.Show("流程模板製作失敗");
            }
        }


        public void Readshm(string n_Path)
        {


                 string MOD = n_Path + "\\Model_" + "xx.shm";
                
                HOperatorSet.ReadShapeModel(MOD, out ModelID);

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

            if (this.cbb_image.Text == "")
            {
                MessageBox.Show("输入图像为空,请输入");
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

            if (!IsNumber(this.txt_greydis.Text.ToString()))
            {
                MessageBox.Show(" 输入参数灰度差值不是数字,请重新输入");
                return false;
            }

            if (!IsNumber(this.txt_maxerror.Text.ToString()))
            {
                MessageBox.Show(" 输入参数最大冗错值不是数字,请重新输入");
                return false;
            }









            return true;

        }






                public SaveGreyTem(SerializationInfo info, StreamingContext context)
        {
            InitializeComponent();

            this.model_subPixel.SelectedIndex = 0;
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;

            if ((string)(info.GetValue("ModelCheck", typeof(string))) == "good")
            {
                this.ModelIDGrey = (HTuple)info.GetValue("ModelID_Grey", typeof(HTuple));
                //  Model_region = (ROI_Model)info.GetValue("Model_Region_ROI_Model", typeof(ROI_Model));
                this.ModelImage = (HObject)info.GetValue("Model_Image", typeof(HObject));
                Bitmap show_model;
                HObject2Bpp8(ModelImage, out show_model);
                pictureBox1.Image = show_model;
                this.ModelImage_fullmodel = (HObject)info.GetValue("ModelImage_Fullmodel", typeof(HObject));
            }

            this.comboBox1.SelectedIndex = (Int32)(info.GetValue("ModelRegionClass", typeof(Int32)));
            this.comboBox2.SelectedIndex = (Int32)(info.GetValue("SouRegionClass", typeof(Int32)));

            if ((string)(info.GetValue("ButtonPress", typeof(string))) == "True")
                Button_press = true;
            else
                Button_press = false;
            this.txt_angleStart.Text = (string)(info.GetValue("Angle_start", typeof(string)));
            this.txt_angleExtent.Text = (string)(info.GetValue("Angle_end", typeof(string)));
            this.txt_numMatches.Text = (string)(info.GetValue("NumMatches", typeof(string)));
            this.txt_maxerror.Text = (string)(info.GetValue("Score", typeof(string)));

            this.txt_greydis.Text = (string)(info.GetValue("Greydis", typeof(string)));
            this.model_subPixel.SelectedIndex = (Int32)(info.GetValue("SubPixel", typeof(Int32)));
            this.comboBox2.SelectedIndex = (Int32)(info.GetValue("Region_type", typeof(Int32)));
            string Regionnull_check = (string)(info.GetValue("Region_Not_null", typeof(string)));
            if (Regionnull_check == "ROIRectangle2")
            {

                Sousuo_region_get.Roi_model = "ROIRectangle2";
               Sousuo_region_get.Roi_Rec2length1  = Convert.ToDouble((string)(info.GetValue("Rec2length1", typeof(string))));
                Sousuo_region_get.Roi_Rec2length2 = Convert.ToDouble((string)(info.GetValue("Rec2length2", typeof(string))));
                Sousuo_region_get.Roi_Rec2phi = Convert.ToDouble((string)(info.GetValue("Rec2phi", typeof(string))));
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

            string Get_Model_type = (string)(info.GetValue("ModelType", typeof(string)));

            if (Get_Model_type == "ViewROI.ROIRectangle2")
            {

                Model_region.Roi_Rec2length1 = Convert.ToDouble((string)(info.GetValue("Rec2length1x", typeof(string))));
                Model_region.Roi_Rec2length2 = Convert.ToDouble((string)(info.GetValue("Rec2length2x", typeof(string))));
                Model_region.Roi_Rec2phi = Convert.ToDouble((string)(info.GetValue("Rec2phix", typeof(string))));
                Model_region.Roi_x = Convert.ToDouble((string)(info.GetValue("Rec2x", typeof(string))));
                Model_region.Roi_y = Convert.ToDouble((string)(info.GetValue("Rec2y", typeof(string))));
                Model_region.Roi_model = "ViewROI.ROIRectangle2";

            }
            else if (Model_region.Roi_model == "ViewROI.ROICircle")
            {
                Model_region.Roi_Cir1radius = Convert.ToDouble((string)(info.GetValue("Cir1radius", typeof(string))));
                Model_region.Roi_x = Convert.ToDouble((string)(info.GetValue("Rec2x", typeof(string))));
                Model_region.Roi_y = Convert.ToDouble((string)(info.GetValue("Rec2y", typeof(string))));
                Model_region.Roi_model = "ViewROI.ROICircle";
            }

            Copy_check = true;
            this.button4.Enabled = true;
            string out_image_string = (string)(info.GetValue("Image_string", typeof(string)));
            string[] condition = { "," };
            string[] result = out_image_string.Split(condition, StringSplitOptions.None);

            for (int m = 0; m < result.Count(); m++)
                cbb_image.Items.Add(result[m]);
            cbb_image.SelectedIndex = (Int32)(info.GetValue("Input_Image", typeof(Int32)));



        }





                public void GetObjectData(SerializationInfo info, StreamingContext context)
                {
                    ViewROI.ROIRectangle2 Rec21;
                    ViewROI.ROICircle Cir11;
                    info.AddValue("Tool_Name ", this.GetType().Name);
                    if (ModelIDGrey == null)
                    {
                        info.AddValue("ModelCheck", "none");
                    }
                    else if (ModelIDGrey.Length == 0)
                    {
                        info.AddValue("ModelCheck", "none");
                    }
                    else
                    {
                        info.AddValue("ModelCheck", "good");
                        info.AddValue("ModelID_Grey", ModelIDGrey, typeof(HTuple));
                        info.AddValue("Model_Image", ModelImage, typeof(HObject));
                        info.AddValue("ModelImage_Fullmodel", ModelImage_fullmodel, typeof(HObject));
                    }
                    info.AddValue("ModelRegionClass", this.comboBox1.SelectedIndex.ToString());

                    info.AddValue("SouRegionClass", this.comboBox2.SelectedIndex.ToString());

                    info.AddValue("Input_Image", this.cbb_image.SelectedIndex.ToString());

                    info.AddValue("ButtonPress", Button_press.ToString());

                    info.AddValue("Angle_start", this.txt_angleStart.Text.ToString());
                    info.AddValue("Angle_end", this.txt_angleExtent.Text.ToString());
                    info.AddValue("NumMatches", this.txt_numMatches.Text.ToString());
                    info.AddValue("Score", this.txt_maxerror.Text.ToString());
                    info.AddValue("Greydis", this.txt_greydis.Text.ToString());
                    info.AddValue("SubPixel", this.model_subPixel.SelectedIndex.ToString());
                    info.AddValue("Region_type", this.comboBox2.SelectedIndex.ToString());

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
                                info.AddValue("Rec2phi", Rec21.phi.ToString());
                                info.AddValue("Rec2midc", Rec21.midC.ToString());
                                info.AddValue("Rec2midr", Rec21.midR.ToString());


                            }
                            if (sou_regions[0].GetType().ToString() == "ViewROI.ROICircle")
                            {
                                info.AddValue("Region_Not_null", "ROICircle");
                                Cir11 = sou_regions[0] as ViewROI.ROICircle;
                                info.AddValue("Rec2phi", Cir11.midC.ToString());
                                info.AddValue("Rec2midc", Cir11.midR.ToString());
                                info.AddValue("Rec2midr", Cir11.radius.ToString());

                            }

                        }
                        else
                            info.AddValue("Region_Not_null", "None");
                    }
                    else
                        info.AddValue("Region_Not_null", "NoneNone");
                    info.AddValue("ModelType", Model_region.Roi_model);

                    if (Model_region.Roi_model == "ViewROI.ROIRectangle2")
                    {
                        info.AddValue("Rec2y", Model_region.Roi_y.ToString());
                        info.AddValue("Rec2x", Model_region.Roi_x.ToString());
                        info.AddValue("Rec2phix", Model_region.Roi_Rec2phi.ToString());
                        info.AddValue("Rec2length2x", Model_region.Roi_Rec2length2.ToString());
                        info.AddValue("Rec2length1x", Model_region.Roi_Rec2length1.ToString());

                    }
                    else if (Model_region.Roi_model == "ViewROI.ROICircle")
                    {
                        info.AddValue("Cir1radius", Model_region.Roi_Cir1radius.ToString());
                        info.AddValue("Rec2x", Model_region.Roi_x.ToString());
                        info.AddValue("Rec2y", Model_region.Roi_y.ToString());
                    }
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
        internal void ReadData(List<string> n_Path,int j,ROIController mm)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);




            if (IniFile.IniReadValue(j.ToString(), "Model_Not_Null") == "1")
            {
                try
                {
                    string MODPATH = n_Path[0] + "\\Model_" + j + ".shm";
                    HOperatorSet.ReadTemplate(MODPATH, out ModelIDGrey);
                    ReadModelRegion(n_Path[0] + "\\Model_" + j + ".ini");
                    HOperatorSet.ReadImage(out ModelImage, n_Path[0] + "\\Model_" + j);
                    Bitmap show_model;
                    HObject2Bpp8(ModelImage, out show_model);
                    pictureBox1.Image = show_model;
                    if (Model_region.Roi_model == "ViewROI.ROIRectangle2")
                    {
                        mm.genRect2(Model_region.Roi_x, Model_region.Roi_y, Model_region.Roi_Rec2phi, Model_region.Roi_Rec2length1, Model_region.Roi_Rec2length2, ref regions);
                        mm.reset();
                    }
                    else
                    {
                        mm.genCircle(Model_region.Roi_x, Model_region.Roi_y, Model_region.Roi_Cir1radius, ref regions);
                        mm.reset();
                    }
                    HOperatorSet.ReadImage(out ModelImage_fullmodel, n_Path[0] + "\\Modelx_" + j);
                    ModelImage_full = Copypicture(ModelImage_fullmodel);
                    Train_image = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("流程[" + (j + 1) + "] 模板讀取失敗");
                }
            }



            string Regionnull_check = IniFile.IniReadValue(j.ToString(), "Region_Not_null");
            this.comboBox2.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "Region_type"));
            if (Regionnull_check == "ROIRectangle1")
            {

                double rec1row1 = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec1row1"));
                double rec1col1 = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec1col1"));
                double rec1row2 = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec1row2"));
                double rec1col2 = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec1col2"));

              //  this.comboBox2.SelectedIndex = 1;

                mm.genRect1(rec1row1, rec1col1, rec1row2, rec1col2, ref sou_regions);
                mm.reset();
            }
            else if (Regionnull_check == "ROIRectangle2")
            {



                double rec2length1 = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec2length1"));
                double rec2length2 = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec2length2"));
                double rec2phi = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec2phi"));
                double rec2midc = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec2midc"));
                double rec2midr = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Rec2midr"));

           //     this.comboBox2.SelectedIndex = 1;
                mm.genRect2(rec2midr, rec2midc, rec2phi, rec2length1, rec2length2, ref sou_regions);
                mm.reset();
            }
            else if (Regionnull_check == "ROICircle")
            {



                double cir1midc = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Cir1midc"));
                double cir1midr = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Cir1midr"));
                double cir1radius = Convert.ToDouble(IniFile.IniReadValue(j.ToString(), "Cir1radius"));
              //  this.comboBox2.SelectedIndex = 2;
                mm.genCircle(cir1midr, cir1midc, cir1radius, ref sou_regions);
                mm.reset();
            }
            this.label_path.Text = IniFile.IniReadValue(j.ToString(), "Label_path");
            this.txt_angleStart.Text = IniFile.IniReadValue(j.ToString(), "Angle_start");
            this.txt_angleExtent.Text = IniFile.IniReadValue(j.ToString(), "Angle_end");
            this.txt_greydis.Text = IniFile.IniReadValue(j.ToString(), "Grey_dis");
            this.txt_maxerror.Text = IniFile.IniReadValue(j.ToString(), "Max_error");
            this.txt_numMatches.Text = IniFile.IniReadValue(j.ToString(), "NumMatches");

            this.txt_angleStart.Text = IniFile.IniReadValue(j.ToString(), "Angle_start");
            cbb_image.Items.Clear();
            if (IniFile.IniReadValue(j.ToString(), "InputImage") != "000000")
            {
                cbb_image.Items.Add(IniFile.IniReadValue(j.ToString(), "InputImage"));
                cbb_image.SelectedIndex = 0;
            }
            model_subPixel.Text = IniFile.IniReadValue(j.ToString(), "SubPixel");
            if (IniFile.IniReadValue(j.ToString(), "ButtonPress") == "True")
            {
                Button_press = true;
                this.button_搜索形状.BackColor = Color.Yellow;
            }
            else
                Button_press = false;
            if (IniFile.IniReadValue(j.ToString(), "ModelRegionClass") != "")
                this.comboBox1.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "ModelRegionClass"));
            if (IniFile.IniReadValue(j.ToString(), "SouRegionClass") != "")
                this.comboBox2.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "SouRegionClass"));
        }

        public HObject Gettrain()
        {
            return ModelImage_full;
        }

        public void MakeModel(HObject Himage)
        {

            ViewROI.ROIRectangle2 Model_Rec2;
            ViewROI.ROICircle Model_Cir1;
            if (this.regions == null)
            {
                MessageBox.Show("Please Choose Region first");
                return;

            }
            HObject outRegion;
            this.regions[0].getRegion().AreaCenter(out Row, out Column);
            
            HOperatorSet.ReduceDomain(Himage, regions[0].getRegion(), out outRegion);




            if (ModelIDGrey.Type != HTupleType.EMPTY)
            {

                HOperatorSet.ClearTemplate(ModelIDGrey);
            }
            try
            {
                //   HOperatorSet.CreateShapeModel(ImageReduced1, 3, 0, (new HTuple(360)).TupleRad(), "auto", "none", "ignore_global_polarity", 30, 15, out ModelID);

              //  HOperatorSet.CreateTemplate(outRegion, 255, 4, "sort", "original", out ModelIDGrey);
              //  HOperatorSet.CreateTemplate(outRegion, 255, 4, "sort", "original", out ModelIDGrey);
             //   HOperatorSet.CreateTemplateRot(outRegion, 4, (HTuple)Convert.ToDouble(txt_angleStart.Text.ToString()), (HTuple)Convert.ToDouble(txt_angleExtent.Text.ToString()), 0.0982, "sort", "original", out ModelIDGrey);
                //create_template_rot (ImageReduced, 4, -pi, 2 * pi, pi / 45, 'sort', 'original', TemplateID)
                    HOperatorSet.CreateTemplateRot(outRegion, 4, -Math.PI, 2*Math.PI, Math.PI/45, "sort", "original", out ModelIDGrey);
                    HOperatorSet.CropDomain(outRegion, out ModelImage);
                    Bitmap show_bitmap;
                    HObject2Bpp8(ModelImage, out show_bitmap);
                    pictureBox1.Image = show_bitmap;
                    ModelImage_fullmodel = Copypicture(ModelImage_full);
                if (regions[0].GetType().ToString() == "ViewROI.ROIRectangle2")
                {
                    Model_region.Roi_model = "ViewROI.ROIRectangle2";
                    Model_Rec2 = regions[0] as ViewROI.ROIRectangle2;
                    Model_region.Roi_Rec2length1 = Model_Rec2.length1;
                    Model_region.Roi_Rec2length2 = Model_Rec2.length2;
                    Model_region.Roi_Rec2phi = Model_Rec2.phi;

                    Model_region.Roi_x = Model_Rec2.midR;
                    Model_region.Roi_y = Model_Rec2.midC;

                }
                else if (regions[0].GetType().ToString() == "ViewROI.ROICircle")
                {
                    Model_Cir1 = regions[0] as ViewROI.ROICircle;
                    Model_region.Roi_model = "ViewROI.ROICircle";
                    Model_region.Roi_Cir1radius = Model_Cir1.radius;
                    Model_region.Roi_x = Model_Cir1.midR;
                    Model_region.Roi_y = Model_Cir1.midC;

                }
                else
                    MessageBox.Show("模板没有区域");

            }

            catch (Exception)
            {

                ModelIDGrey = new HTuple();
                ModelImage = new HObject();
            }

        }

        public bool MatchingModel(HWndCtrl hWndCtrl, HObject In_Image, out List<模板GVName> pmatest_result, out 模板GVName_halcon pmatest_result_halcon, out List<HObject> object_graphic_out, out string out_info, ROIController out_result_region)
        {
            SetROI = false;
            HObject show_region;
            HTuple hv_Rowzz, hv_Columnzz, hv_Anglezz, hv_Error=0;
            pmatest_result = new List<模板GVName>();
            pmatest_result_halcon = new 模板GVName_halcon();
            object_graphic_out = new List<HObject>();
            out_info = "";
            ViewROI.ROIRectangle2 Rec2x;
            ViewROI.ROICircle Cir1x;
            HObject newRegion;
            HHomMat2D Matrix = new HHomMat2D();
            HObject In_Image1;
            HObject outdomain;

            List<double> xrowlist = new List<double>();
            List<double> ycollist = new List<double>();
            List<double> anglelist = new List<double>();
            List<double> hverrorlist = new List<double>();
            HObject difference_region;
            if (Button_press)
            {
                if (this.button4.Enabled)
                {
                    MessageBox.Show("请先恢复拷贝时使用的区域");
                    return false;
                }
            }
            if (ModelIDGrey.Length == 0)
            {
                MessageBox.Show("模板为空，请重做模板或者加载模板");
                return false;
            }
            double Error_number = Convert.ToDouble(this.txt_maxerror.Text.ToString());
            if (Button_press)
            {
                HOperatorSet.ReduceDomain(In_Image, sou_regions[0].getRegion(), out In_Image1);
            }
            else
                In_Image1 = In_Image;




            int Find_number = Convert.ToInt32(this.txt_numMatches.Text);



            int i = 0;
            if (this.comboBox2.SelectedIndex == 0 && Button_press)
            {



                if (this.sou_regions != null)
                {
                    if (this.sou_regions.Count != 0)
                    {

                        if (sou_regions[0].GetType().ToString() == "ViewROI.ROIRectangle2")
                        {

                            Rec2x = sou_regions[0] as ViewROI.ROIRectangle2;

                            // out_result_region.genRect2(Rec2x.midR, Rec2x.midC, Rec2x.phi, Rec2x.length1, Rec2x.length2, ref sousuo_regions);

                            HOperatorSet.GenEmptyObj(out show_region);
                            HOperatorSet.GenRectangle2(out show_region, Rec2x.midR, Rec2x.midC, Rec2x.phi, Rec2x.length1, Rec2x.length2);
                            hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, "orange");
                            hWndCtrl.addIconicVar(show_region);
                        }

                    }

                }
            }


            if (this.comboBox2.SelectedIndex == 1 && Button_press)
            {



                if (this.sou_regions != null)
                {
                    if (this.sou_regions.Count != 0)
                    {

                        if (sou_regions[0].GetType().ToString() == "ViewROI.ROICircle")
                        {
                            Cir1x = sou_regions[0] as ViewROI.ROICircle;
                            HOperatorSet.GenEmptyObj(out show_region);
                            HOperatorSet.GenCircle(out show_region, Cir1x.midR, Cir1x.midC, Cir1x.radius);
                            hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, "orange");
                            hWndCtrl.addIconicVar(show_region);

                        }

                    }

                }
            }
            while (hv_Error < Error_number)
            {


                 HOperatorSet.BestMatchRotMg(In_Image1, ModelIDGrey, (HTuple)Convert.ToDouble(txt_angleStart.Text.ToString()) * Math.PI / 180, (HTuple)Convert.ToDouble(txt_angleExtent.Text.ToString()) * Math.PI / 180, (HTuple)Convert.ToDouble(txt_greydis.Text.ToString()), this.model_subPixel.SelectedItem.ToString(), 2, out hv_Rowzz, out hv_Columnzz, out hv_Anglezz, out hv_Error);

                 if (hv_Error > Error_number || hv_Error == Error_number)
                     break;

                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Rowzz, hv_Columnzz, hv_Anglezz, out hv_MovementOfObject);


                pmatest_result.Add(new 模板GVName((i+1).ToString(), hv_Columnzz.D.ToString("F3"), hv_Rowzz.D.ToString("F3"), hv_Anglezz.D.ToString("F3"), hv_Error.D.ToString("F3")));

                xrowlist.Add(hv_Columnzz);
                ycollist.Add(hv_Rowzz);
                anglelist.Add(hv_Anglezz);
                hverrorlist.Add(hv_Error);
                if (Model_region.Roi_model == "ViewROI.ROIRectangle2")
                {
                  //  out_result_region.genRect2(hv_Rowzz, hv_Columnzz, (Model_region.Roi_Rec2phi - hv_Anglezz), Model_region.Roi_Rec2length1, Model_region.Roi_Rec2length2, ref model_regions);
                    HOperatorSet.GenRectangle2(out newRegion, hv_Rowzz, hv_Columnzz, (Model_region.Roi_Rec2phi - hv_Anglezz), Model_region.Roi_Rec2length1, Model_region.Roi_Rec2length2);



                }
                else
                {
                    //out_result_region.genRect2(hv_Rowzz[i], hv_Columnzz[i], (Model_region.Roi_Rec2phi - hv_Anglezz[i]), Model_region.Roi_Rec2length1, Model_region.Roi_Rec2length2, ref model_regions);
                //    out_result_region.genCircle(hv_Rowzz, hv_Columnzz, Model_region.Roi_Cir1radius, ref model_regions);
                    HOperatorSet.GenCircle(out newRegion, hv_Rowzz, hv_Columnzz, Model_region.Roi_Cir1radius);
                    if (Model_region.Roi_Cir1radius == 0)
                        return false;
                    

                }
                HOperatorSet.GetDomain(In_Image1, out outdomain);
                HOperatorSet.Difference(outdomain, newRegion, out difference_region);
                HOperatorSet.ReduceDomain(In_Image1, difference_region, out In_Image1);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Rowzz, hv_Columnzz, hv_Anglezz, out hv_MovementOfObject);
                HObject Model_result_region;
                HObject hv_cross;
                if (Model_region.Roi_model == "ViewROI.ROIRectangle2")
                {
                   // out_result_region.genRect2(hv_Rowzz, hv_Columnzz, (Model_region.Roi_Rec2phi - hv_Anglezz), Model_region.Roi_Rec2length1, Model_region.Roi_Rec2length2, ref model_regions);
                    HOperatorSet.GenEmptyObj(out Model_result_region);
                    HOperatorSet.GenRectangle2(out Model_result_region, hv_Rowzz, hv_Columnzz, -(Model_region.Roi_Rec2phi - hv_Anglezz), Model_region.Roi_Rec2length1, Model_region.Roi_Rec2length2);

                    hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, "green");
                    hWndCtrl.addIconicVar(Model_result_region);
                    HOperatorSet.GenContourPolygonXld(out Model_result_region, ((HTuple)hv_Rowzz).TupleConcat(hv_Rowzz + (Math.Sin(Model_region.Roi_Rec2phi - hv_Anglezz) * Model_region.Roi_Rec2length1 * 0.9)),
                        ((HTuple)hv_Columnzz).TupleConcat(hv_Columnzz + (Math.Cos(Model_region.Roi_Rec2phi - hv_Anglezz) * Model_region.Roi_Rec2length1 * 0.9)));
                    hWndCtrl.addIconicVar(Model_result_region);
                    HOperatorSet.GenCrossContourXld(out hv_cross, hv_Rowzz, hv_Columnzz, 11, -(Model_region.Roi_Rec2phi - hv_Anglezz));
                    hWndCtrl.addIconicVar(hv_cross);
                }
                if (Model_region.Roi_model == "ViewROI.ROICircle")
                {

                   // out_result_region.genCircle(hv_Rowzz, hv_Columnzz, Model_region.Roi_Cir1radius, ref model_regions);

                                    
                    HOperatorSet.GenEmptyObj(out Model_result_region);

                  //  out_result_region.genCircle(hv_Rowzz[i], hv_Columnzz[i],Model_region.Roi_Cir1radius, ref model_regions);
                    HOperatorSet.GenCircle(out Model_result_region, hv_Rowzz, hv_Columnzz, Model_region.Roi_Cir1radius);

                    hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, "green");
                    hWndCtrl.addIconicVar(Model_result_region);

                }
                i++;
                if (Find_number != 0 && i == Find_number)
                    break;
            }


            int allnumber = xrowlist.Count;
            double[] xrow = new double[allnumber] ;
            double[] ycol = new double[allnumber] ;
            double[] angle = new double[allnumber];
            double[] hv_errors = new double[allnumber];
            for (int m = 0; m < allnumber; m++)
            {
                xrow[m] = xrowlist[m];
                ycol[m] = ycollist[m];
                angle[m] = anglelist[m];
                hv_errors[m] = hverrorlist[m];

            
            }

            pmatest_result_halcon.点X = (HTuple)xrow;
            pmatest_result_halcon.点Y = (HTuple)ycol;
            pmatest_result_halcon.角度Angle = (HTuple)angle;
            pmatest_result_halcon.分数Score = (HTuple)hv_errors;
            return true;
        }





        public bool MatchingModel_backup(HObject In_Image, out List<模板GVName> pmatest_result, out 模板GVName_halcon pmatest_result_halcon, out List<HObject> object_graphic_out, out string out_info, ROIController out_result_region)
        {
            SetROI = false;
            HTuple hv_Rowzz, hv_Columnzz, hv_Anglezz,  hv_Error;
            pmatest_result = new List<模板GVName>();
            pmatest_result_halcon = new 模板GVName_halcon();
            object_graphic_out = new List<HObject>();
            out_info = "";
            HObject outRegion;
            HHomMat2D Matrix = new HHomMat2D();

            if (ModelIDGrey.Length == 0)
            {
                MessageBox.Show("模板为空，请重做模板或者加载模板");
                return false;
            }
            try
            {
                if (Button_press)
                {
                    HOperatorSet.ReduceDomain(In_Image, sou_regions[0].getRegion(), out outRegion);
                   // HOperatorSet.BestMatchRot(outRegion, ModelIDGrey, (HTuple)Convert.ToDouble(txt_angleStart.Text.ToString()), (HTuple)Convert.ToDouble(txt_angleExtent.Text.ToString()),
   // (HTuple)Convert.ToDouble(txt_greydis.Text.ToString()), this.model_subPixel.SelectedItem.ToString(), out hv_Rowzz, out hv_Columnzz, out hv_Anglezz, out hv_Error);
                    HOperatorSet.BestMatchRotMg(outRegion, ModelIDGrey,  (HTuple)Convert.ToDouble(txt_angleStart.Text.ToString()) * Math.PI /180, (HTuple)Convert.ToDouble(txt_angleExtent.Text.ToString())* Math.PI /180, (HTuple)Convert.ToDouble(txt_greydis.Text.ToString()), this.model_subPixel.SelectedItem.ToString(), 2, out hv_Rowzz, out hv_Columnzz, out hv_Anglezz, out hv_Error);
                 
                }
                else
                  //  HOperatorSet.BestMatchRot(In_Image, ModelIDGrey, (HTuple)Convert.ToDouble(txt_angleStart.Text.ToString()), (HTuple)Convert.ToDouble(txt_angleExtent.Text.ToString()),
   // (HTuple)Convert.ToDouble(txt_greydis.Text.ToString()), this.model_subPixel.SelectedItem.ToString(), out hv_Rowzz, out hv_Columnzz, out hv_Anglezz, out hv_Error);
                HOperatorSet.BestMatchRotMg(In_Image, ModelIDGrey,  (HTuple)Convert.ToDouble(txt_angleStart.Text.ToString()) * Math.PI /180,(HTuple)Convert.ToDouble(txt_angleExtent.Text.ToString())* Math.PI /180, (HTuple)Convert.ToDouble(txt_greydis.Text.ToString()), this.model_subPixel.SelectedItem.ToString(), 2, out hv_Rowzz, out hv_Columnzz, out hv_Anglezz, out hv_Error);
                    //HOperatorSet.FindScaledShapeModel(In_Image, ModelID, (new HTuple(0)).TupleRad(), (new HTuple(360)).TupleRad(), 1, 1, 0.6, 0, 0.5, "least_squares", 0, 0.8, out hv_Rowzz, out hv_Columnzz, out hv_Anglezz, out hv_scalezz, out hv_Score);
            }

            catch (Exception)
            {

                return false;
            }
            double Error_number = Convert.ToDouble(this.txt_maxerror.Text.ToString());
            //   HOperatorSet.GetShapeModelContours(out ShapeModel, ModelID, 1);
            if (hv_Error < Error_number)
            {
                pmatest_result_halcon.点X = hv_Columnzz;
                pmatest_result_halcon.点Y = hv_Rowzz;
                pmatest_result_halcon.角度Angle = hv_Anglezz;
                pmatest_result_halcon.分数Score = hv_Error;
            }

            // pmatest_result_halcon.分数Score = hv_Score;

            for (int i = 0; i < hv_Rowzz.Length; i = i + 1)
            {
                if (hv_Error < Error_number)
                {
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Rowzz[i], hv_Columnzz[i], hv_Anglezz[i], out hv_MovementOfObject);
                    //    HOperatorSet.AffineTransContourXld(ShapeModel, out ho_ModelAtNewPosition, hv_MovementOfObject);
                    //    object_graphic_out.Add(ho_ModelAtNewPosition);

                    pmatest_result.Add(new 模板GVName((i+1).ToString(), hv_Columnzz[i].D.ToString("F3"), hv_Rowzz[i].D.ToString("F3"), hv_Anglezz[i].D.ToString("F3"), hv_Error[i].D.ToString("F3")));
                    if (Model_region.Roi_model == "ViewROI.ROIRectangle2")
                    {
                        out_result_region.genRect2(hv_Rowzz[i], hv_Columnzz[i], (Model_region.Roi_Rec2phi-hv_Anglezz[i]), Model_region.Roi_Rec2length1, Model_region.Roi_Rec2length2, ref model_regions);
                    }
                    if (Model_region.Roi_model == "ViewROI.ROICircle")
                    {
                        //out_result_region.genRect2(hv_Rowzz[i], hv_Columnzz[i], (Model_region.Roi_Rec2phi - hv_Anglezz[i]), Model_region.Roi_Rec2length1, Model_region.Roi_Rec2length2, ref model_regions);
                        out_result_region.genCircle(hv_Rowzz[i], hv_Columnzz[i], Model_region.Roi_Cir1radius, ref model_regions);

                    }
                }




            }

            return true;
        }


        private void button_参数设置1_Click(object sender, EventArgs e)
        {

        }

        private void button_参数设置2_Click(object sender, EventArgs e)
        {


        }

        private void button_参数设置3_Click(object sender, EventArgs e)
        {

        }


        public void Model_SaveImagepath()
        {
            if (ModelIDGrey.Length == 0)
            {
                MessageBox.Show("No model can be save");
                return;
            }


            
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "model files (*.shm)|*.shm";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {

                string strPathName = saveFileDialog1.FileName;

                HOperatorSet.WriteTemplate(ModelIDGrey, strPathName);
               // ReadModelRegion(strPathName.Substring(0, strPathName.Length - 4) + ".ini");
                WriteModelRegion(strPathName.Substring(0, strPathName.Length - 4) + ".ini");
                HOperatorSet.WriteImage(ModelImage, "bmp", 0, strPathName.Substring(0, strPathName.Length - 4));


            }
        }

        public string Imagename()
        {
            return cbb_image.Text.ToString() +".img";
        }
        public void Model_ReadImagepath()
        {


            OpenFileDialog saveFileDialog1 = new OpenFileDialog();

            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "model files (*.shm)|*.shm";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {

                string strPathName = saveFileDialog1.FileName;
                //HOperatorSet.WriteShapeModel(ModelID, strPathName);
              //  HOperatorSet.ReadShapeModel(strPathName,out  ModelID);
                HOperatorSet.ReadTemplate(strPathName, out ModelIDGrey);
                label_path.Text = strPathName;
                ReadModelRegion(strPathName.Substring(0, strPathName.Length - 4) + ".ini");
                HOperatorSet.ReadImage(out ModelImage, strPathName.Substring(0, strPathName.Length - 4) + ".bmp");
                Bitmap show_model;
                HObject2Bpp8(ModelImage, out show_model);
                pictureBox1.Image = show_model;


            }
        }

        public void Bitmap2HObjectBpp8(Bitmap bmp, out HObject image)
        {
            try
            {
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
 
                BitmapData srcBmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
 
                HOperatorSet.GenImage1(out image, "byte", bmp.Width, bmp.Height, srcBmpData.Scan0);
                bmp.UnlockBits(srcBmpData);
            }
            catch (Exception ex)
            {
                image = null;
            }
        }


     private    void HObject2Bpp8(HObject image, out Bitmap res)  
    {  
	HTuple hpoint, type, width, height;  
 
	const int Alpha = 255;  
	int[] ptr = new int[2];  
	HOperatorSet.GetImagePointer1(image, out hpoint, out type, out width, out height);  
 
	res = new Bitmap(width, height, PixelFormat.Format8bppIndexed);  
	ColorPalette pal = res.Palette;  
	for (int i = 0; i <= 255; i++)  
	{  
		pal.Entries[i] = Color.FromArgb(Alpha, i, i, i);  
	}  
	res.Palette = pal;  
	Rectangle rect = new Rectangle(0, 0, width, height);  
	BitmapData bitmapData = res.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);  
	int PixelSize = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;  
	ptr[0] = bitmapData.Scan0.ToInt32();  
	ptr[1] = hpoint.I;  
	if (width % 4 == 0)  
		CopyMemory(ptr[0], ptr[1], width * height * PixelSize);  
	else  
	{  
		for (int i = 0; i < height - 1; i++)  
		{  
			ptr[1] += width;  
			CopyMemory(ptr[0], ptr[1], width * PixelSize);  
			ptr[0] += bitmapData.Stride;  
		}  
	}  
	res.UnlockBits(bitmapData);  
  
    } 



        private void button1_Click(object sender, EventArgs e)
        {
            String Shape = comboBox1.SelectedItem.ToString();
            SetROI = true;

            ShowToolGreyRegion.OnSendShowPattrenGreyRegion(new ShowToolGreyRegionEventArgs(Shape));
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ShowToolGreyRegion.OnSendShowPattrenGreyRegion(new ShowToolGreyRegionEventArgs("LearnModel"));
        }

        private void button3_匹配_Click(object sender, EventArgs e)
        {

            ShowToolGreyRegion.OnSendShowPattrenGreyRegion(new ShowToolGreyRegionEventArgs("ModelMatching"));

        }

        private void button_保存模板_Click(object sender, EventArgs e)
        {
            Model_SaveImagepath();
        }

        public void setImageIn(HObject Himage)
        {
            this.ModelImage_full = Copypicture(Himage);
            HTuple x1, y1;
            HOperatorSet.GetImageSize(ModelImage_full, out x1, out y1);
            Train_image = true;
        }
        public bool Get_image_result()
        {
            return Train_image;
        }

        public void Set_parameters(SetShapeModel_Parameters read_parameters)
        {
       //     textBox_startangle.Text = read_parameters.Model_startangle;
       //     textBox_endangle.Text = read_parameters.Model_endangle;
        //    textBox_num.Text = read_parameters.Model_num;
         //   textBox_score.Text = read_parameters.Model_score;
            regions = read_parameters.Model_regions;
            if (read_parameters.Model_check)
                Readshm(rootPath);

        }

        private void button_读取模板_Click(object sender, EventArgs e)
        {
            Model_ReadImagepath();
        }

        private void panel_参数设置1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_参数设置3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_参数设置2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbb_image_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void MakeRoi_model(ROIController mx)
        {

            if (Model_region.Roi_model == "ViewROI.ROICircle")
            {
                mx.genCircle(Model_region.Roi_x, Model_region.Roi_y, Model_region.Roi_Cir1radius, ref regions);
                mx.reset();
            }
            else if (Model_region.Roi_model == "ViewROI.ROIRectangle2")
            {
                mx.genRect2(Model_region.Roi_x, Model_region.Roi_y, Model_region.Roi_Rec2phi, Model_region.Roi_Rec2length1, Model_region.Roi_Rec2length2, ref regions);
                mx.reset();
            }
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
        private void button_搜索形状_Click(object sender, EventArgs e)
        {
            String Shape1 = comboBox2.SelectedItem.ToString();
            Button_press = !Button_press;

            if (Button_press)
            {
                this.button_搜索形状.BackColor = Color.Yellow;
                SetROI = true;
                ShowToolGreyRegion.OnSendShowPattrenGreyRegion(new ShowToolGreyRegionEventArgs(Shape1));
            }
            else
                this.button_搜索形状.BackColor = Color.Empty;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Check_pal())
                this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowToolGreyRegion.OnSendShowPattrenGreyRegion(new ShowToolGreyRegionEventArgs("TrainModel"));
        }

        private void button4_Click(object sender, EventArgs e)
        {

            ShowToolGreyRegion.OnSendShowPattrenGreyRegion(new ShowToolGreyRegionEventArgs("MakeRegion"));
            this.button4.Enabled = false;

            if (Button_press)
                this.button_搜索形状.BackColor = Color.Yellow;
            

        }
        
    }


    public class ROI_Grey_Model
    {
        public string Roi_model;
        public double Roi_Rec2length1;
        public double Roi_Rec2length2;
        public double Roi_Rec2phi;
        public double Roi_x;
        public double Roi_y;
        public double Roi_Cir1radius;
        public ROI_Grey_Model()
        {
        }

    }


}
