using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PMACam;

namespace CGUse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          //  this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.pmaControl1.accept += new EventHandler(frm7_accept);

         //   this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            //pmaControl1.Ini_combox();
          //  pmaControl1.MakeDir();



        }

        void frm7_accept(object sender, EventArgs e)
        {


            //事件的接收者通过一个简单的类型转换得到Form2的引用 
            //PMAControl PMAControl1 = (PMAControl)sender;
            double[] input_row = { 1 };
            double[] input_col = { 1 };
            double[] info1 = { 10};
            // double[] info2 = { 2, 3, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50, 200, 500 };
            //  double[] info3 = { 2, 3, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50, 200, 500 };
            double[] info2 = { 0 };
            double[] info3 = { 3.1415926};
            int count_number = input_row.Length;
            pmaControl1.clear_result_show();
            for(int m=0;m<count_number;m++)
            {

            //    pmaControl1.add_Result_show(input_row[m], input_col[m], "Circle", info1[m]);

                pmaControl1.add_Result_show(input_row[m], input_col[m], "CircleArc", info1[m], info2[m], info3[m]);

           //     pmaControl1.add_Result_show(input_row[m], input_col[m], "line", info1[m], info2[m]);

            }
            string testname = pmaControl1.get_Solution_name();
            pmaControl1.showContour("green");

            
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();//强制中止调用线程上的所有消息，同样面临其它线程无法正确退出的问题
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            //如果你是需要关闭进程的代码，则如下：
            //先确定你的进程 
          //  Process[] plist = Process.GetProcessesByName("服务");
         //   Process p = plist[0];
            // 结束进程的方式： 
          //  p.Kill();  // 就可以强制关掉进程。
            System.Environment.Exit(0);   //这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净

            this.Close();// This.Close()是关闭当前的窗体
            this.Dispose();//Dispose方法只能释放当前窗体资源，不能强制结束循环
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            
        }

        private void pmaControl1_Load(object sender, EventArgs e)
        {
            pmaControl1.Cam_Ini();
            pmaControl1.Cam_Realshow();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           pmaControl1.Cam_SingleTake();


            

           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            double alltime_get = 0;
            double cameratime_get = 0;
            string typex = "";
            pmaControl1.Runtest("", out cameratime_get, out alltime_get,out typex);
            MessageBox.Show(typex);

           // 
            /*


            pmaControl1.Runtest(0, 0, 3,out cameratime_get,out alltime_get);
            MessageBox.Show("cameratime:" + cameratime_get.ToString() + "alltime:" + alltime_get.ToString());
           // MessageBox.Show("this camera time is"+cameratime_get.ToString());
          //  MessageBox.Show("this all time is"+alltime_get.ToString());
            double[]  xrow;
            double[] ycol;
            double[] angle;
            
            pmaControl1.get_point(out xrow,out ycol,out angle);
            for (int m = 0; m < xrow.Count(); m++)
                MessageBox.Show("x:" + xrow[m].ToString() + "y:" + ycol[m].ToString() + "angle:" + angle[m].ToString());
            */

           // double[] input_row = { 588, 300, -360  };
           // double[] input_col = { 85.5, 109.5, 279.5 };
           // pmaControl1.add_point(2, input_row, input_col);
            /*
            pmaControl1.Runtest("模板", out cameratime_get, out alltime_get);
            double[] xrow;
            double[] ycol;
            double[] xrow1;
            double[] ycol1;
            double[] angle;
            double[] score;

            pmaControl1.get_line_point(out xrow, out ycol, out xrow1,out ycol1);
            for (int m = 0; m < xrow.Count(); m++)
                MessageBox.Show("x:" + xrow[m].ToString() + "y:" + ycol[m].ToString() + "row2:" + xrow1[m].ToString() + "col2:" + ycol1[m].ToString());
            */
            //pmaControl1.Runtest("模板000", out cameratime_get, out alltime_get);
            //double[] xrow2;
            //double[] ycol2;
            //double[] radius;

            //pmaControl1.get_circle_point(out xrow2, out ycol2, out radius);
            //for (int m = 0; m < xrow2.Count(); m++)
            //    MessageBox.Show("x:" + xrow2[m].ToString() + "y:" + ycol2[m].ToString() + "row2:" + radius[m].ToString());

            //pmaControl1.get_list_point(0, out xrow_dian, out ycol_dian);
         //   for (int m = 0; m < xrow.Count(); m++)
          //      MessageBox.Show("x:" + xrow[m].ToString() + "y:" + ycol[m].ToString() + "angle:" + angle[m].ToString());
            //MessageBox.Show("cameratime:" + cameratime_get.ToString() + "alltime:" + alltime_get.ToString());

/*
            double[] input_row = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 ,200,500};
            double[] input_col = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,200,500 };
            pmaControl1.add_point(2, input_row, input_col);
            String testsolution = pmaControl1.get_Solution_name();
            double[] xrow;
            double[] ycol;

            double[] angle;
            double[] score;
            pmaControl1.Runtest(testsolution,out cameratime_get, out alltime_get);
            pmaControl1.get_match_point(out xrow, out ycol, out angle, out score);
            for (int m = 0; m < xrow.Count(); m++)
                MessageBox.Show("x:" + xrow[m].ToString() + "y:" + ycol[m].ToString() + "angle:" + angle[m].ToString() + "score:" + score[m].ToString());
*/

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[] xrow2;
            double[] ycol2;
            double[] radius;
            double alltime_get = 0;
            double cameratime_get = 0;
            String testsolution = pmaControl1.get_Solution_name();
            pmaControl1.Runtest(testsolution, out cameratime_get, out alltime_get);
            pmaControl1.get_circle_point(out xrow2, out ycol2, out radius);
            for (int m = 0; m < xrow2.Count(); m++)
                MessageBox.Show("x:" + xrow2[m].ToString() + "y:" + ycol2[m].ToString() + "row2:" + radius[m].ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {/*
            double[] xrow;
            double[] ycol;
            double[] xrow1;
            double[] ycol1;
            double alltime_get = 0;
            double cameratime_get = 0;
            String testsolution = pmaControl1.get_Solution_name();
            pmaControl1.Runtest(testsolution, out cameratime_get, out alltime_get);

            pmaControl1.get_line_point(out xrow, out ycol, out xrow1, out ycol1);
            for (int m = 0; m < xrow.Count(); m++)
                MessageBox.Show("x:" + xrow[m].ToString() + "y:" + ycol[m].ToString() + "row2:" + xrow1[m].ToString() + "col2:" + ycol1[m].ToString());
          * */
            double alltime_get = 0;
            double cameratime_get = 0;
            String testsolution = pmaControl1.get_Solution_name();
            pmaControl1.Runtest(testsolution, 24, 27, out cameratime_get, out alltime_get);

        }

        private void button4_Click(object sender, EventArgs e)
        {
          //  double[] input_row = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 200, 500 };
        //    double[] input_col = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 200, 500 };
         //   pmaControl1.add_point(2, input_row, input_col);
            pmaControl1.Set_Solutionname("模板5");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double[] xrow;
            double[] ycol;
            double alltime_get = 0;
            double cameratime_get = 0;
            String testsolution = pmaControl1.get_Solution_name();
            pmaControl1.Runtest(testsolution, out cameratime_get, out alltime_get);
            pmaControl1.get_list_point(2, out xrow, out ycol);
            MessageBox.Show("x is " + xrow[0].ToString() + " y is "+ ycol[0].ToString());



        }

        private void pmaControl1_FontChanged(object sender, EventArgs e)
        {
            MessageBox.Show("bad");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         //   pmaControl1.Cam_Ini();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double[] xrow2;
            double[] ycol2;
            double[] radius;
            double alltime_get = 0;
            double cameratime_get = 0;
           // String testsolution = pmaControl1.get_Solution_name();
            pmaControl1.Runtest("模板", out cameratime_get, out alltime_get);

    
        }
    }
}
