using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ViewROI;
namespace PMACam
{
    public partial class TestResultsShow : Form
    {
        public TestResultsShow()
        {
            InitializeComponent();




        }

        public void WriteData(List<string> n_Path, int j)
        {
            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);

            IniFile.IniWriteValue(j.ToString(), "Tool_Name", this.GetType().Name);



            IniFile.IniWriteValue(j.ToString(), "Show_color", this.comboBox1.SelectedIndex.ToString());

        }

        internal void ReadData(List<string> n_Path, int j)
        {

            IniFile IniFile = new IniFile(n_Path[0] + n_Path[1]);
            if (IniFile.IniReadValue(j.ToString(), "Show_color") != "")
                this.comboBox1.SelectedIndex = Convert.ToInt32(IniFile.IniReadValue(j.ToString(), "Show_color"));



        }






        public bool Run_show(string color_set,圆GVName_halcon Circle_result, 圆弧GVName_halcon Circlearc_result, 直线GVName_halcon Line_result, HWndCtrl hWndCtrl, out string out_info)
        {


            out_info = "";
            if (color_set == "")
                color_set = "red";
            HObject Circle1;
            HObject CircleArc1;
            HObject Line1;

            if (Circle_result == null)
            {
                MessageBox.Show("轮廓显示：  显示圆列表为空");
                out_info = "轮廓显示：  圆为空";

            }
            else
            {
                int Circle_result_number = Circle_result.圆心X.TupleLength();
                for (int j = 0; j < Circle_result_number; j++)
                {
                    //HOperatorSet.GenCircle(out Circle1, Circle_result.圆心X[j], Circle_result.圆心Y[j], Circle_result.半径R[j]);
                    HOperatorSet.GenCircleContourXld(out Circle1, Circle_result.圆心X[j], Circle_result.圆心Y[j], Circle_result.半径R[j], 0, 2 * Math.PI, "positive", 1);
                    hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, color_set);
                    hWndCtrl.addIconicVar(Circle1);
                }


            }

            if (Circlearc_result == null)
            {
                MessageBox.Show("轮廓显示：  圆弧为空");
                out_info = out_info + " 圆弧为空";

            }
            else
            {

                int Circlearc_result_number = Circlearc_result.半径R.TupleLength();
                for (int m = 0; m < Circlearc_result_number; m++)
                {
                    //HOperatorSet.GenCircleSector(out CircleArc1, Circlearc_result.圆心X[m], Circlearc_result.圆心Y[m], Circlearc_result.半径R[m], Circlearc_result.圆弧Start[m], Circlearc_result.圆弧End[m]);
                    HOperatorSet.GenCircleContourXld(out CircleArc1, Circlearc_result.圆心X[m], Circlearc_result.圆心Y[m], Circlearc_result.半径R[m], Circlearc_result.圆弧Start[m], Circlearc_result.圆弧End[m], "positive", 1);
                    hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, color_set);
                    hWndCtrl.addIconicVar(CircleArc1);
                }


            }

            if (Line_result == null)
            {
                MessageBox.Show("轮廓显示：  直线为空");
                out_info = out_info + "直线为空";

            }
            else
            {
                int line_result_number = Line_result.点1X.TupleLength();
                for (int l = 0; l < line_result_number; l++)
                {
                    HOperatorSet.GenContourPolygonXld(out Line1, ((HTuple)Line_result.点1X[l]).TupleConcat(
(HTuple)Line_result.点2X[l]), ((HTuple)Line_result.点1Y[l]).TupleConcat((HTuple)Line_result.点2Y[l]));

                    hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, color_set);
                    hWndCtrl.addIconicVar(Line1);


                }
            }
            hWndCtrl.repaint();

            return true;









        }










        public bool Run_show_backup(圆GVName_halcon Circle_result, 圆弧GVName_halcon Circlearc_result, 直线GVName_halcon Line_result,HWndCtrl hWndCtrl, out string out_info)
        {


            out_info = "";
            HObject Circle1;
            HObject CircleArc1;
            HObject Line1;

            if (Circle_result == null)
            {
                MessageBox.Show("轮廓显示：  显示圆列表为空");
                out_info = "轮廓显示：  圆为空";

            }
            else
            {
                int Circle_result_number = Circle_result.圆心X.TupleLength();
                for (int j = 0; j < Circle_result_number; j++)
                {
                    //HOperatorSet.GenCircle(out Circle1, Circle_result.圆心X[j], Circle_result.圆心Y[j], Circle_result.半径R[j]);
                    HOperatorSet.GenCircleContourXld(out Circle1,Circle_result.圆心X[j], Circle_result.圆心Y[j],Circle_result.半径R[j],0,2*Math.PI,"positive",1);
                    hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, this.comboBox1.SelectedItem.ToString());
                    hWndCtrl.addIconicVar(Circle1);
                }
                
                
            }

            if (Circlearc_result == null)
            {
                MessageBox.Show("轮廓显示：  圆弧为空");
                out_info = out_info + " 圆弧为空";

            }
            else
            {

                int Circlearc_result_number = Circlearc_result.半径R.TupleLength();
                for (int m = 0; m < Circlearc_result_number; m++)
                {
                    //HOperatorSet.GenCircleSector(out CircleArc1, Circlearc_result.圆心X[m], Circlearc_result.圆心Y[m], Circlearc_result.半径R[m], Circlearc_result.圆弧Start[m], Circlearc_result.圆弧End[m]);
                    HOperatorSet.GenCircleContourXld(out CircleArc1, Circle_result.圆心X[m], Circle_result.圆心Y[m], Circle_result.半径R[m], Circlearc_result.圆弧Start[m], Circlearc_result.圆弧End[m], "positive", 1);
                    hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, this.comboBox1.SelectedItem.ToString());
                    hWndCtrl.addIconicVar(CircleArc1);
                }

                
            }

            if (Line_result == null)
            {
                MessageBox.Show("轮廓显示：  直线为空");
                out_info = out_info +  "直线为空";

            }
            else
            {
                int line_result_number = Line_result.点1X.TupleLength();
                for (int l = 0; l < line_result_number;l++ )
                {
                    HOperatorSet.GenContourPolygonXld(out Line1, ((HTuple)Line_result.点1X[l]).TupleConcat(
(HTuple)Line_result.点2X[l]), ((HTuple)Line_result.点1Y[l]).TupleConcat((HTuple)Line_result.点2Y[l]));

                    hWndCtrl.changeGraphicSettings(GraphicsContext.GC_COLOR, this.comboBox1.SelectedItem.ToString());
                    hWndCtrl.addIconicVar(Line1);


                }
            }
            hWndCtrl.repaint();

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
        private void button1_Click(object sender, EventArgs e)
        {

                this.Visible = false;
        }









    }
}
