using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace PMACam
{
    class ResultShow
    {
    }

    class DataGVName
    {
        public DataGVName(string inOrderText)
        {
            orderText = inOrderText;
        }

        string orderText = "";
        public string 工具名
        {
            set { orderText = value; }
            get { return orderText; }
        }


    }
    public class PointName
    {
        public PointName(HTuple inPointXText, HTuple inPointYText)
        {

            PointXText = inPointXText;
            PointYText = inPointYText;
        }

        HTuple PointXText = new HTuple();
        public HTuple 点X
        {
            set { PointXText = value; }
            get { return PointXText; }
        }
        HTuple PointYText = new HTuple();
        public HTuple 点Y
        {
            set { PointYText = value; }
            get { return PointYText; }
        }


    }

    class 长度GVName
    {
        public 长度GVName(string inOrderLengthNoText, string inOrderLengthText)
        {
            orderLengthText = inOrderLengthText;
            orderLengthNoText = inOrderLengthNoText;
        }


        string orderLengthNoText = "";
        string orderLengthText = "";
        public string 编号
        {
            set { orderLengthNoText = value; }
            get { return orderLengthNoText; }
        }

        public string 长度
        {
            set { orderLengthText = value; }
            get { return orderLengthText; }
        }
    }



    class 角度GVName
    {
        public 角度GVName(string inOrderAngleNoText, string inOrderAngleText)
        {
            orderAngleNoText = inOrderAngleNoText;
            orderAngleText = inOrderAngleText;

        }

        string orderAngleNoText = "";
        string orderAngleText = "";
        public string 编号
        {
            set { orderAngleNoText = value; }
            get { return orderAngleNoText; }
        }

        public string 角度
        {
            set { orderAngleText = value; }
            get { return orderAngleText; }
        }
    }




    class 字符串GVName
    {
        public 字符串GVName(string inorderAngleNoText, string inOrderAngleText)
        {
            orderAngleNoText = inorderAngleNoText;
            orderAngleText = inOrderAngleText;

        }


        string orderAngleNoText = "";
        public string 编号
        {
            set { orderAngleNoText = value; }
            get { return orderAngleNoText; }
        }
        string orderAngleText = "";
        public string 字符串
        {
            set { orderAngleText = value; }
            get { return orderAngleText; }
        }
    }



    public class 点GVName
    {
        public 点GVName(string inOrderPointNoText, string inOrderPointXText, string inOrderPointYText)
        {
            orderPointNoText = inOrderPointNoText;
            orderPointXText = inOrderPointXText;
            orderPointYText = inOrderPointYText;
        }

        string orderPointNoText = "";
        public string 编号
        {
            set { orderPointNoText = value; }
            get { return orderPointNoText; }
        }
        string orderPointXText = "";
        public string 点X
        {
            set { orderPointXText = value; }
            get { return orderPointXText; }
        }
        string orderPointYText = "";
        public string 点Y
        {
            set { orderPointYText = value; }
            get { return orderPointYText; }
        }


    }

    public class 点GVName_halcon
    {
        public 点GVName_halcon(HTuple inOrderPointNoText, HTuple inOrderPointXText, HTuple inOrderPointYText)
        {

            orderPointXText = inOrderPointXText;
            orderPointYText = inOrderPointYText;
        }
        public 点GVName_halcon()
        { }

        HTuple orderPointXText = new HTuple();
        public HTuple 点X
        {
            set { orderPointXText = value; }
            get { return orderPointXText; }
        }
        HTuple orderPointYText = new HTuple();
        public HTuple 点Y
        {
            set { orderPointYText = value; }
            get { return orderPointYText; }
        }


    }

    public class 圆GVName
    {
        public 圆GVName(string inOrderCircleNoText, string inOrderCircleXText, string inOrderCircleYText, string inOrderCircleRText)
        {
            orderCircleNoText = inOrderCircleNoText;
            orderCircleXText = inOrderCircleXText;
            orderCircleYText = inOrderCircleYText;
            orderCircleRText = inOrderCircleRText;
        }

        string orderCircleNoText = "";
        string orderCircleXText = "";
        string orderCircleYText = "";
        string orderCircleRText = "";

        public string 编号
        {
            set { orderCircleNoText = value; }
            get { return orderCircleNoText; }
        }

        public string 圆心X
        {
            set { orderCircleXText = value; }
            get { return orderCircleXText; }
        }
        public string 圆心Y
        {
            set { orderCircleYText = value; }
            get { return orderCircleYText; }
        }

        public string 半径R
        {
            set { orderCircleRText = value; }
            get { return orderCircleRText; }
        }
    }


    public class 斑点GVName
    {
        public 斑点GVName(string inOrderBlobNoText, string inOrderBlobXText, string inOrderBlobYText, string inOrderBlobRText,string inOrderBlobWText)
        {
            orderBlobNoText = inOrderBlobNoText;
            orderBlobXText = inOrderBlobXText;
            orderBlobYText = inOrderBlobYText;
            orderBlobRText = inOrderBlobRText;
            orderBlobWText = inOrderBlobWText;
        }

        string orderBlobNoText = "";
        string orderBlobXText = "";
        string orderBlobYText = "";
        string orderBlobRText = "";
        string orderBlobWText = "";

        public string 编号
        {
            set { orderBlobNoText = value; }
            get { return orderBlobNoText; }
        }

        public string 域中心X
        {
            set { orderBlobXText = value; }
            get { return orderBlobXText; }
        }
        public string 域中心Y
        {
            set { orderBlobYText = value; }
            get { return orderBlobYText; }
        }

        public string 域面积
        {
            set { orderBlobRText = value; }
            get { return orderBlobRText; }
        }
        public string 域角度
        {
            set { orderBlobWText = value; }
            get { return orderBlobWText; }
        }
    }










    public class 斑点GVName_halcon
    {
        public 斑点GVName_halcon(HTuple inOrderBlobXText, HTuple inOrderBlobYText, HTuple inOrderBlobRText,HTuple inOrderBlobWText)
        {

            orderBlobXText = inOrderBlobXText;
            orderBlobYText = inOrderBlobYText;
            orderBlobRText = inOrderBlobRText;
            orderBlobWText = inOrderBlobWText;
        }

        public 斑点GVName_halcon()
        { }
        HTuple orderBlobXText = new HTuple();
        HTuple orderBlobYText = new HTuple();
        HTuple orderBlobRText = new HTuple();
        HTuple orderBlobWText = new HTuple();



        public HTuple 域中心X
        {
            set { orderBlobXText = value; }
            get { return orderBlobXText; }
        }
        public HTuple 域中心Y
        {
            set { orderBlobYText = value; }
            get { return orderBlobYText; }
        }

        public HTuple 域面积
        {
            set { orderBlobRText = value; }
            get { return orderBlobRText; }
        }
        public HTuple 域角度
        {
            set { orderBlobWText = value; }
            get { return orderBlobWText; }
        }
    }



    public class 圆GVName_halcon
    {
        public 圆GVName_halcon(HTuple inOrderCircleXText, HTuple inOrderCircleYText, HTuple inOrderCircleRText)
        {

            orderCircleXText = inOrderCircleXText;
            orderCircleYText = inOrderCircleYText;
            orderCircleRText = inOrderCircleRText;
        }

        public 圆GVName_halcon()
        { }
        HTuple orderCircleXText = new HTuple();
        HTuple orderCircleYText = new HTuple();
        HTuple orderCircleRText = new HTuple();



        public HTuple 圆心X
        {
            set { orderCircleXText = value; }
            get { return orderCircleXText; }
        }
        public HTuple 圆心Y
        {
            set { orderCircleYText = value; }
            get { return orderCircleYText; }
        }

        public HTuple 半径R
        {
            set { orderCircleRText = value; }
            get { return orderCircleRText; }
        }
    }




    public class 圆弧GVName_halcon
    {
        public 圆弧GVName_halcon(HTuple inOrderCircleXText, HTuple inOrderCircleYText, HTuple inOrderCircleRText,HTuple inOrderCircleArcstart,HTuple inOrderCircleArcend)
        {

            orderCircleXText = inOrderCircleXText;
            orderCircleYText = inOrderCircleYText;
            orderCircleRText = inOrderCircleRText;
            orderCircleArcstart = inOrderCircleArcstart;
            orderCircleArcend = inOrderCircleArcend;

            
        }

        public 圆弧GVName_halcon()
        { }
        HTuple orderCircleXText = new HTuple();
        HTuple orderCircleYText = new HTuple();
        HTuple orderCircleRText = new HTuple();
        HTuple orderCircleArcstart = new HTuple();
        HTuple orderCircleArcend = new HTuple();


        public HTuple 圆心X
        {
            set { orderCircleXText = value; }
            get { return orderCircleXText; }
        }
        public HTuple 圆心Y
        {
            set { orderCircleYText = value; }
            get { return orderCircleYText; }
        }

        public HTuple 半径R
        {
            set { orderCircleRText = value; }
            get { return orderCircleRText; }
        }

        public HTuple 圆弧Start
        {
            set { orderCircleArcstart = value; }
            get { return orderCircleArcstart; }
        }

        public HTuple 圆弧End
        {
            set { orderCircleArcend = value; }
            get { return orderCircleArcend; }
        }
    }



    public class 字符串GVName_halcon
    {
        public 字符串GVName_halcon(HTuple inOrderAngleText)
        {
            orderAngleText = inOrderAngleText;
        }
        public 字符串GVName_halcon()
        { }
        HTuple orderAngleText = new HTuple();
        public HTuple 字符串
        {
            set { orderAngleText = value; }
            get { return orderAngleText; }
        }
    }



    public class 模板GVName_halcon
    {
        public 模板GVName_halcon(HTuple inOrderModelXText, HTuple inOrderModelYText, HTuple inOrderModelAngleText, HTuple inOrderModelScoreText,HTuple inOrderModelphiText)
        {

            orderModelXText = inOrderModelXText;
            orderModelYText = inOrderModelYText;
            orderModelModelAngleText = inOrderModelAngleText;
            orderModelModelScoreText = inOrderModelScoreText;
            inOrderModelphiText = orderModelPhiText;

        }

        public 模板GVName_halcon()
        { }
        HTuple orderModelXText = new HTuple();
        HTuple orderModelYText = new HTuple();
        HTuple orderModelModelAngleText = new HTuple();
        HTuple orderModelModelScoreText = new HTuple();
        HTuple orderModelPhiText = new HTuple();


        public HTuple 点X
        {
            set { orderModelXText = value; }
            get { return orderModelXText; }
        }
        public HTuple 点Y
        {
            set { orderModelYText = value; }
            get { return orderModelYText; }
        }
        public HTuple 角度Angle
        {
            set { orderModelModelAngleText = value; }
            get { return orderModelModelAngleText; }
        }

        public HTuple 分数Score
        {
            set { orderModelModelScoreText = value; }
            get { return orderModelModelScoreText; }
        }
        public HTuple 模板Angle
        {
            set { orderModelPhiText = value; }
            get { return orderModelPhiText; }
        }
    }













    public class 模板GVName
    {
        public 模板GVName(string inOrderModelNoText, string inOrderModelXText, string inOrderModelYText, string inOrderModelAngleText, string inOrderModelScoreText)
        {
            orderModelNoText = inOrderModelNoText;
            orderModelXText = inOrderModelXText;
            orderModelYText = inOrderModelYText;
            orderModelModelAngleText = inOrderModelAngleText;
            orderModelModelScoreText = inOrderModelScoreText;
        }

        string orderModelNoText = "";
        string orderModelXText = "";
        string orderModelYText = "";
        string orderModelModelAngleText = "";
        string orderModelModelScoreText = "";
        public string 编号
        {
            set { orderModelNoText = value; }
            get { return orderModelNoText; }
        }

        public string 点X
        {
            set { orderModelXText = value; }
            get { return orderModelXText; }
        }
        public string 点Y
        {
            set { orderModelYText = value; }
            get { return orderModelYText; }
        }
        public string 角度Angle
        {
            set { orderModelModelAngleText = value; }
            get { return orderModelModelAngleText; }
        }

        public string 分数Score
        {
            set { orderModelModelScoreText = value; }
            get { return orderModelModelScoreText; }
        }
    }



   public  class 直线GVName_halcon
    {
        public 直线GVName_halcon(HTuple inOrderLineP1XText, HTuple inOrderLineP1YText, HTuple inOrderLineP2XText, HTuple inOrderLineP2YText)
        {

            orderLineP1XText = inOrderLineP1XText;
            orderLineP1YText = inOrderLineP1YText;
            orderLineP2XText = inOrderLineP2XText;
            orderLineP2YText = inOrderLineP2YText;
        }

        public 直线GVName_halcon()
        { }
        HTuple orderLineP1XText = new HTuple();
        HTuple orderLineP1YText = new HTuple();
        HTuple orderLineP2XText = new HTuple();
        HTuple orderLineP2YText = new HTuple();


        public HTuple 点1X
        {
            set { orderLineP1XText = value; }
            get { return orderLineP1XText; }
        }
        public HTuple 点1Y
        {
            set { orderLineP1YText = value; }
            get { return orderLineP1YText; }
        }
        public HTuple 点2X
        {
            set { orderLineP2XText = value; }
            get { return orderLineP2XText; }
        }
        public HTuple 点2Y
        {
            set { orderLineP2YText = value; }
            get { return orderLineP2YText; }
        }

    }


   public class 直线GVName
    {
        public 直线GVName(string inOrderLineNoText, string inOrderLineP1XText, string inOrderLineP1YText, string inOrderLineP2XText, string inOrderLineP2YText)
        {
            orderLineNoText = inOrderLineNoText;
            orderLineP1XText = inOrderLineP1XText;
            orderLineP1YText = inOrderLineP1YText;
            orderLineP2XText = inOrderLineP2XText;
            orderLineP2YText = inOrderLineP2YText;
        }

        string orderLineNoText = "";
        string orderLineP1XText = "";
        string orderLineP1YText = "";
        string orderLineP2XText = "";
        string orderLineP2YText = "";
        public string 编号
        {
            set { orderLineNoText = value; }
            get { return orderLineNoText; }
        }

        public string 点1X
        {
            set { orderLineP1XText = value; }
            get { return orderLineP1XText; }
        }
        public string 点1Y
        {
            set { orderLineP1YText = value; }
            get { return orderLineP1YText; }
        }
        public string 点2X
        {
            set { orderLineP2XText = value; }
            get { return orderLineP2XText; }
        }
        public string 点2Y
        {
            set { orderLineP2YText = value; }
            get { return orderLineP2YText; }
        }

    }

    public class CommonData
    {
        public static string Path;

        //  public static double Real_pixel1;
        //  public static double Real_pixel2;
        //  public static double Real_pixel3;
        //   public static double Real_pixel4;
        public static List<string> Real_pixel;

    }
}