using System;
using System.Collections.Generic;
using System.Text;
using HalconDotNet;
using ViewROI;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using ViewROI.Config;


namespace ViewROI
{
    public class ViewWindow : IViewWindow
    {
        private ViewROI.HWndCtrl _hWndControl;

        private ViewROI.ROIController _roiController;


        public ViewWindow(HWindowControl window,Label location_point)
        {
            this._hWndControl = new ViewROI.HWndCtrl(window, location_point);
            this._roiController = new ViewROI.ROIController();
            this._hWndControl.setROIController(this._roiController);
            this._hWndControl.setViewState(ViewROI.HWndCtrl.MODE_VIEW_NONE);
        }

        //清空所有显示内容
        public void ClearWindow()
        {
            //清空显示image
            _hWndControl.clearList();
            //清空hobjectList
            _hWndControl.clearHObjectList();
        }
        public void displayImage(HObject img)
        {
            //添加背景图片
            this._hWndControl.addImageShow(img);
            //清空roi容器,不让roi显示
            this._roiController.reset();
            //显示图片
           this._roiController.resetWindowImage();


            //this._hWndControl.resetWindow();
           // this._hWndControl.resetAll();
            //this._hWndControl.repaint();
        }
        public void notDisplayRoi()
        {

            this._roiController.reset();
            //显示图片
            this._roiController.resetWindowImage();

        }
        //获取当前窗口显示的roi数量
        public int getRoiCount()
        {
            return _roiController.ROIList.Count;
        }
        //是否开启缩放事件
        public void setDrawModel(bool flag)
        {
            _hWndControl.drawModel = flag;
        }
        public void resetWindowImage()
        {
            this._hWndControl.resetWindow();
            this._roiController.resetWindowImage();
        }

        public void mouseleave()
        {
            _hWndControl.raiseMouseup();
        }

        public void zoomWindowImage()
        {
            this._roiController.zoomWindowImage();
        }

        public void moveWindowImage()
        {
            this._roiController.moveWindowImage();
        }

        public void noneWindowImage()
        {
            this._roiController.noneWindowImage();
        }

        public void genRect1(double row1, double col1, double row2, double col2, ref List<ViewROI.ROI> rois)
        {
            this._roiController.genRect1(row1, col1, row2, col2, ref rois);
        }

        public void genRect2(double row, double col, double phi, double length1, double length2, ref List<ViewROI.ROI> rois)
        {
            this._roiController.genRect2(row, col, phi, length1, length2,ref rois);
        }

        public void genCircle(double row, double col, double radius, ref List<ViewROI.ROI> rois)
        {
            this._roiController.genCircle(row, col, radius, ref rois);
        }

        public void genLine(double beginRow, double beginCol, double endRow, double endCol, ref List<ViewROI.ROI> rois)
        {
            this._roiController.genLine(beginRow, beginCol, endRow, endCol, ref rois);
        }

        public List<double> smallestActiveROI(out string name, out int index)
        {
            List<double> resual = this._roiController.smallestActiveROI(out name,out index);
            return resual;
        }



        public ViewROI.ROI smallestActiveROI(out List<double> data, out int index)
        {
            ViewROI.ROI roi = this._roiController.smallestActiveROI(out data, out index);
            return roi;
        }

        public void selectROI(int index)
        {
            this._roiController.selectROI(index);
          
        }





        public void selectROI(List<ViewROI.ROI> rois, int index)
        {
            //this._roiController.selectROI(index);
            if ((rois.Count > index)&&(index>=0))
            {
                this._hWndControl.resetAll();
                this._hWndControl.repaint();

                HTuple m_roiData = null;
                m_roiData = rois[index].getModelData();

                switch (rois[index].Type)
                {
                    case "ROIRectangle1":

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect1(rois[index].Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    case "ROIRectangle2":

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect2(rois[index].Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D, m_roiData[4].D);
                        }
                        break;
                    case "ROICircle":

                        if (m_roiData != null)
                        {
                            this._roiController.displayCircle(rois[index].Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D);
                        }
                        break;
                    case "ROILine":

                        if (m_roiData != null)
                        {
                            this._roiController.displayLine(rois[index].Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void displayROI(List<ViewROI.ROI> rois)
        {
            if (rois == null)
            {
                return;
            }
            //this._hWndControl.resetAll();
            //this._hWndControl.repaint();

            foreach (var roi in rois)
            {
                HTuple m_roiData = null;
                m_roiData = roi.getModelData();

                switch (roi.Type)
                {
                    case "ROIRectangle1":

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect1(roi.Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    case "ROIRectangle2":

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect2(roi.Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D, m_roiData[4].D);
                     
                        }
                        break;
                    case "ROICircle":

                        if (m_roiData != null)
                        {
                            this._roiController.displayCircle(roi.Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D);
                        }
                        break;
                    case "ROILine":

                        if (m_roiData != null)
                        {
                            this._roiController.displayLine(roi.Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void removeActiveROI(ref List<ViewROI.ROI> rois)
        {
            this._roiController.removeActiveROI(ref rois);
        }



        public void removeROI(ref List<ViewROI.ROI> rois)
        {
            this._roiController.removeROI(ref rois);
        }
        public void setActiveRoi(int index)
        {
            this._roiController.activeROIidx = index;
        }
        public void saveROI(List<ViewROI.ROI> rois, string fileNmae)
        {
            List<ViewROI.RoiData> m_RoiData = new List<ViewROI.RoiData>();
            for (int i = 0; i < rois.Count; i++)
            {
                m_RoiData.Add(new ViewROI.RoiData(i, rois[i]));
            }

            Config.SerializeHelper.Save(m_RoiData, fileNmae);
        }



        public void loadROI(string fileName, out List<ViewROI.ROI> rois)
        {
            rois = new List<ViewROI.ROI>();
            List<ViewROI.RoiData> m_RoiData = new List<ViewROI.RoiData>();
            m_RoiData = (List<ViewROI.RoiData>)Config.SerializeHelper.Load(m_RoiData.GetType(), fileName);
            for (int i = 0; i < m_RoiData.Count; i++)
            {
                switch (m_RoiData[i].Name)
                {
                    case "Rectangle1":
                        this._roiController.genRect1(m_RoiData[i].Rectangle1.Row1, m_RoiData[i].Rectangle1.Column1,
                            m_RoiData[i].Rectangle1.Row2, m_RoiData[i].Rectangle1.Column2, ref rois);
                        rois.Last().Color = m_RoiData[i].Rectangle1.Color;
                        break;
                    case "Rectangle2":
                        this._roiController.genRect2(m_RoiData[i].Rectangle2.Row, m_RoiData[i].Rectangle2.Column,
                            m_RoiData[i].Rectangle2.Phi, m_RoiData[i].Rectangle2.Lenth1, m_RoiData[i].Rectangle2.Lenth2, ref rois);
                        rois.Last().Color = m_RoiData[i].Rectangle2.Color;
                        break;
                    case "Circle":
                        this._roiController.genCircle(m_RoiData[i].Circle.Row, m_RoiData[i].Circle.Column, m_RoiData[i].Circle.Radius, ref rois);
                        rois.Last().Color = m_RoiData[i].Circle.Color;
                        break;
                    case "Line":
                        this._roiController.genLine(m_RoiData[i].Line.RowBegin, m_RoiData[i].Line.ColumnBegin,
                            m_RoiData[i].Line.RowEnd, m_RoiData[i].Line.ColumnEnd, ref rois);
                        rois.Last().Color = m_RoiData[i].Line.Color;
                        break;
                    default:
                        break;
                }
            }

            this._hWndControl.resetAll();
            this._hWndControl.repaint();
        }

        #region 专门用于 显示region 和xld的方法
        public void displayHobject(HObject obj,string color)
        {
            _hWndControl.DispObj(obj, color);
        
        }
        public void displayHobject(HObject obj)
        {
            _hWndControl.DispObj(obj, null);
        }
        #endregion
    }
}
