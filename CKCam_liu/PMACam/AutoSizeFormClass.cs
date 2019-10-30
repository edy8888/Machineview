using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PMACam  
{  
    class AutoSizeFormClass  
    {  
        //(1).声明结构,只记录窗体和其控件的初始位置和大小。  
        public struct controlRect  
        {  
            public int Left;  
            public int Top;  
            public int Width;  
            public int Height;
            public Font font_con;
            
        } 
        //(2).声明 1个对象  
        //注意这里不能使用控件列表记录 List<Control> nCtrl;，因为控件的关联性，记录的始终是当前的大小。  
         public List<controlRect> oldCtrl;  
                int ctrlNo = 0;//1;  
        //int ctrl_first = 0;  
        //(3). 创建两个函数  
        //(3.1)记录窗体和其控件的初始位置和大小,  
                public void controllInitializeSize(Control mForm)
                {
                    controlRect cR;
                    oldCtrl = new List<controlRect>();  
                    cR.Left = mForm.Left; cR.Top = mForm.Top; cR.Width = mForm.Width; cR.Height = mForm.Height;
                    cR.font_con = mForm.Font;
                    oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可  
                    AddControl(mForm);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用  
                    //this.WindowState = (System.Windows.Forms.FormWindowState)(2);//记录完控件的初始位置和大小后，再最大化  
                    //0 - Normalize , 1 - Minimize,2- Maximize  
                }  


        public void controlAutoSize(Control mForm,double width,double height)
        {
            if (ctrlNo == 0)
            { //*如果在窗体的Form1_Load中，记录控件原始的大小和位置，正常没有问题，但要加入皮肤就会出现问题，因为有些控件如dataGridView的的子控件还没有完成，个数少  
                //*要在窗体的Form1_SizeChanged中，第一次改变大小时，记录控件原始的大小和位置,这里所有控件的子控件都已经形成  
                controlRect cR;
                //  cR.Left = mForm.Left; cR.Top = mForm.Top; cR.Width = mForm.Width; cR.Height = mForm.Height;  
                cR.Left = 0; cR.Top = 0; cR.Width = mForm.PreferredSize.Width; cR.Height = mForm.PreferredSize.Height;
                cR.font_con = mForm.Font;
                oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可  
                AddControl(mForm);//窗体内其余控件可能嵌套其它控件(比如panel),故单独抽出以便递归调用  
            }
       //     float wScale = (float)mForm.Width / (float)oldCtrl[0].Width;//新旧窗体之间的比例，与最早的旧窗体  
        //    float hScale = (float)mForm.Height / (float)oldCtrl[0].Height;//.Height;  
            float wScale = (float)width / 1920;//新旧窗体之间的比例，与最早的旧窗体  
                float hScale = (float)height / 1040;//.Height;  
            ctrlNo = 1;//进入=1，第0个为窗体本身,窗体内的控件,从序号1开始  
            wScale = (float)(Math.Sqrt(wScale) * 0.95);
            hScale = (float)(Math.Sqrt(hScale) * 0.95);

            AutoScaleControl(mForm, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用  
        }  

        private void AddControl(Control ctl)
        {
            foreach (Control c in ctl.Controls)
            {  //**放在这里，是先记录控件的子控件，后记录控件本身  
                //if (c.Controls.Count > 0)  
                //    AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用  
                controlRect objCtrl;
                objCtrl.Left = c.Left; objCtrl.Top = c.Top; objCtrl.Width = c.Width; objCtrl.Height = c.Height;
                objCtrl.font_con = c.Font;
                oldCtrl.Add(objCtrl);
                //**放在这里，是先记录控件本身，后记录控件的子控件  
                if (c.Controls.Count > 0)
                    AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用  
            }
        } 





        //(3.2)控件自适应大小,  
        /*
        public void controlAutoSize(UserControl mForm,int height,int width)  
        {  
            //int wLeft0 = oldCtrl[0].Left; ;//窗体最初的位置  
            //int wTop0 = oldCtrl[0].Top;  
            ////int wLeft1 = this.Left;//窗体当前的位置  
            //int wTop1 = this.Top;  


        //    float wScale = (float)mForm.Width / (float)oldCtrl[0].Width;//新旧窗体之间的比例，与最早的旧窗体  
          //  float hScale = (float)mForm.Height / (float)oldCtrl[0].Height;//.Height;  
            float wScale = (float)width/1920 ;//新旧窗体之间的比例，与最早的旧窗体  
            float hScale = (float)height/1040 ;//.Height;  
            int ctrLeft0, ctrTop0, ctrWidth0, ctrHeight0;  
            int ctrlNo = 1;//第1个是窗体自身的 Left,Top,Width,Height，所以窗体控件从ctrlNo=1开始  
         //   MessageBox.Show("count is ", mForm.Controls.Count.ToString());
              
            foreach (Control c in mForm.Controls)
            {

                ctrLeft0 = oldCtrl[ctrlNo].Left;  
                ctrTop0 = oldCtrl[ctrlNo].Top;  
                ctrWidth0 = oldCtrl[ctrlNo].Width;  
                ctrHeight0 = oldCtrl[ctrlNo].Height;  
                //c.Left = (int)((ctrLeft0 - wLeft0) * wScale) + wLeft1;//新旧控件之间的线性比例  
                //c.Top = (int)((ctrTop0 - wTop0) * h) + wTop1;  
                c.Left = (int)((ctrLeft0) * wScale);//新旧控件之间的线性比例。控件位置只相对于窗体，所以不能加 + wLeft1  
                c.Top = (int)((ctrTop0) * hScale);//  
                c.Width = (int)(ctrWidth0 * wScale);//只与最初的大小相关，所以不能与现在的宽度相乘 (int)(c.Width * w);  
                c.Height = (int)(ctrHeight0 * hScale);//  

                ctrlNo += 1;  
            }  
        }  
        */

        private void AutoScaleControl(Control ctl, float wScale, float hScale)  
        {  
            int ctrLeft0, ctrTop0, ctrWidth0, ctrHeight0;
            Font ctrFont0;
            //int ctrlNo = 1;//第1个是窗体自身的 Left,Top,Width,Height，所以窗体控件从ctrlNo=1开始  
            foreach (Control c in ctl.Controls)  
            { //**放在这里，是先缩放控件的子控件，后缩放控件本身  
                //if (c.Controls.Count > 0)  
                //   AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用  
                ctrLeft0 = oldCtrl[ctrlNo].Left;  
                ctrTop0 = oldCtrl[ctrlNo].Top;  
                ctrWidth0 = oldCtrl[ctrlNo].Width;  
                ctrHeight0 = oldCtrl[ctrlNo].Height;
                ctrFont0 = oldCtrl[ctrlNo].font_con;

                //c.Left = (int)((ctrLeft0 - wLeft0) * wScale) + wLeft1;//新旧控件之间的线性比例  
                //c.Top = (int)((ctrTop0 - wTop0) * h) + wTop1;  
                c.Left = (int)((ctrLeft0) * wScale);//新旧控件之间的线性比例。控件位置只相对于窗体，所以不能加 + wLeft1  
                c.Top = (int)((ctrTop0) * hScale);//  
                c.Width = (int)(ctrWidth0 * wScale);//只与最初的大小相关，所以不能与现在的宽度相乘 (int)(c.Width * w);  
                c.Height = (int)(ctrHeight0 * hScale);//  
                Single currentSize = Convert.ToSingle(ctrFont0.Size * Math.Sqrt(hScale));
                c.Font = new Font(ctrFont0.Name, currentSize, ctrFont0.Style, ctrFont0.Unit);


                ctrlNo++;//累加序号  
                //**放在这里，是先缩放控件本身，后缩放控件的子控件  
                if (c.Controls.Count > 0)  
                    AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用  
  
                if (ctl is DataGridView)  
                {  
                    DataGridView dgv = ctl as DataGridView;  
                    Cursor.Current = Cursors.WaitCursor;  
  
                    int widths = 0;  
                    for (int i = 0; i < dgv.Columns.Count; i++)  
                    {  
                        dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);  // 自动调整列宽    
                        widths += dgv.Columns[i].Width;   // 计算调整列后单元列的宽度和                         
                    }  
                    if (widths >= ctl.Size.Width)  // 如果调整列的宽度大于设定列宽    
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;  // 调整列的模式 自动    
                    else  
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;  // 如果小于 则填充    
  
                    Cursor.Current = Cursors.Default;  
                }  
            }  
  
  
        }  
    }  
}  






    
  