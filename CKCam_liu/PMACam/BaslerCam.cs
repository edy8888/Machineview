﻿



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basler.Pylon;



using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;


using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace PMACam
{
    public class BaslerCam
    {
        public static Camera camera = null;
        private PixelDataConverter converter = new PixelDataConverter();
        private String strUserID = null;

        public long imageWidth = 0;         // 图像宽
        public long imageHeight = 0;        // 图像高
        public long minExposureTime = 0;    // 最小曝光时间
        public long maxExposureTime = 0;    // 最大曝光时间
        public long minGain = 0;            // 最小增益
        public long maxGain = 0;            // 最大增益
        public int numWindowIndex = 0;

        private long grabTime = 0;          // 采集图像时间

        //private HObject hPylonImage = null;
        private IntPtr latestFrameAddress = IntPtr.Zero;
        private Stopwatch stopWatch = new Stopwatch();

        /// <summary>
        /// 计算采集图像时间自定义委托
        /// </summary>
        /// <param name="time">采集图像时间</param>
        public delegate void delegateComputeGrabTime(long time);
        /// <summary>
        /// 计算采集图像时间委托事件
        /// </summary>
        public event delegateComputeGrabTime eventComputeGrabTime;

        /// <summary>
        /// 图像处理自定义委托
        /// </summary>
        /// <param name="hImage">halcon图像变量</param>
        public delegate void delegateProcessHImage(Bitmap bitmap, IntPtr ptrBmp);
        /// <summary>
        /// 图像处理委托事件
        /// </summary>
        public event delegateProcessHImage eventProcessImage;

        /// <summary>
        /// if >= Sfnc2_0_0,说明是ＵＳＢ３的相机
        /// </summary>
        static Version Sfnc2_0_0 = new Version(2, 0, 0);


        /******************    实例化相机    ******************/
        /// <summary>
        /// 实例化第一个找到的相机
        /// </summary>
        public BaslerCam()
        {
            try
            {
                camera = new Camera();
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /// <summary>
        /// 根据相机序列号实例化相机
        /// </summary>
        /// <param name="SN"></param>
        //public BaslerCam(string SN)
        //{
        //    camera = new Camera(SN);
        //}

        /// <summary>
        /// 根据相机UserID实例化相机
        /// </summary>
        /// <param name="UserID"></param>
        public BaslerCam(string UserID)
        {
            try
            {
                strUserID = UserID;     //掉线重连用

                // 枚举相机列表
                List<ICameraInfo> allCameraInfos = CameraFinder.Enumerate();

                foreach (ICameraInfo cameraInfo in allCameraInfos)
                {
                     
                     if (strUserID == cameraInfo[CameraInfoKey.UserDefinedName])
                    {
                        camera = new Camera(cameraInfo);
                    }
                 //   MessageBox.Show("first is " + strUserID);
                //    MessageBox.Show("second is " + getId);
                }

                if (camera == null)
                {
                    MessageBox.Show("未识别到UserID为“" + strUserID + "”的相机！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }
        /*****************************************************/

        /******************    相机操作     ******************/
        /// <summary>
        /// 打开相机
        /// </summary>
        public bool OpenCam()
        {
            if (camera == null)
                return false;
            try
            {
                camera.Open();
                //camera.Parameters[PLCamera.AcquisitionFrameRateEnable].SetValue(true);  // 限制相机帧率
                //camera.Parameters[PLCamera.AcquisitionFrameRateAbs].SetValue(90);
                camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(10);          // 设置内存中接收图像缓冲区大小
                camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);   
                imageWidth = camera.Parameters[PLCamera.Width].GetValue();               // 获取图像宽 
                imageHeight = camera.Parameters[PLCamera.Height].GetValue();              // 获取图像高
                GetMinMaxExposureTime();
                GetMinMaxGain();
                camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;                      // 注册采集回调函数
                camera.ConnectionLost += OnConnectionLost;

                return true;
            }
            catch (Exception e)
            {
                ShowException(e);
                return false;
            }
        }

        /// <summary>
        /// 关闭相机,释放相关资源
        /// </summary>
        public void CloseCam()
        {
            try
            {
                camera.Close();
                camera.Dispose();

                //if (hPylonImage != null)
                //{
                //    hPylonImage.Dispose();
                //}

                if (latestFrameAddress != null)
                {
                    Marshal.FreeHGlobal(latestFrameAddress);
                    latestFrameAddress = IntPtr.Zero;
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        public void GrabOne(out Bitmap bitmap, out IntPtr ptrBmp)
        {
            try
            {
               // GrabImage = null;
                if (camera.StreamGrabber.IsGrabbing)
                {
                    StopGrabbing();
                    delay(200);
                }

                IGrabResult grabResult = camera.StreamGrabber.GrabOne(3000);
                stopWatch.Restart();    // ****  重启采集时间计时器   ****
                if (grabResult.PixelTypeValue == PixelType.Mono8)
                {
                    if (latestFrameAddress == IntPtr.Zero)
                    {
                        latestFrameAddress = Marshal.AllocHGlobal((Int32)grabResult.PayloadSize);
                    }
                    converter.OutputPixelFormat = PixelType.Mono8;
                    converter.Convert(latestFrameAddress, grabResult.PayloadSize, grabResult);
                    // 转换为Halcon图像显示
                    //HOperatorSet.GenImage1(out GrabImage, "byte", (HTuple)grabResult.Width, (HTuple)grabResult.Height, (HTuple)latestFrameAddress);


                    //      BitmapData bmpData = m_bitmap.LockBits(new System.Drawing.Rectangle(0, 0, m_bitmap.Width, m_bitmap.Height), ImageLockMode.ReadOnly,
                    //System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    //IntPtr intp = bmpData.Scan0;
                    //m_bitmap.UnlockBits(bmpData);


                    bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format8bppIndexed);
                    BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    converter.OutputPixelFormat = PixelType.Mono8;
                    ptrBmp = bmpData.Scan0;
                    converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                    bitmap.UnlockBits(bmpData);
                                                  
                }
                else
                {
                    bitmap = null; ptrBmp = new IntPtr();
                }
            }
            catch (Exception e)
            {
                bitmap = null; ptrBmp =new IntPtr();
               // ShowException(e);
            }
        }
        /// <summary>
        /// 单张采集
        /// </summary>
        //public bool GrabOne()
        //{
        //    try
        //    {
        //        if (camera.StreamGrabber.IsGrabbing)
        //        {
        //            StopGrabbing();
        //        }
        //        camera.Parameters[PLCamera.AcquisitionMode].SetValue("SingleFrame");
        //        camera.StreamGrabber.Start(1, GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
        //        stopWatch.Restart();    // ****  重启采集时间计时器   ****
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        ShowException(e);
        //        return false;
        //    }
        //}

        /// <summary>
        /// 开始连续采集
        /// </summary>
        public bool StartGrabbing()
        {
            try
            {

                if (camera.StreamGrabber.IsGrabbing)
                {

                    StopGrabbing();
                    delay(200);
                }
               // camera.Parameters[PLCamera.Gamma].TrySetValue(3);
               // camera.Parameters[PLCamera.DigitalShift].TrySetValue(1);
                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);

                camera.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);

                stopWatch.Restart();    // ****  重启采集时间计时器   ****
                return true;
            }
            catch (Exception e)
            {
                ShowException(e);
                return false;
            }
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
        /// <summary>
        /// 停止连续采集
        /// </summary>
        public void StopGrabbing()
        {
            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                {
                    camera.StreamGrabber.Stop();
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /*********************************************************/

        /******************    相机参数设置   ********************/
        /// <summary>
        /// 设置Gige相机心跳时间
        /// </summary>
        /// <param name="value"></param>
        public void SetHeartBeatTime(long value)
        {
            try
            {
                // 判断是否是网口相机
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    camera.Parameters[PLGigECamera.GevHeartbeatTimeout].SetValue(value);
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /// <summary>
        /// 设置相机曝光时间
        /// </summary>
        /// <param name="value"></param>
        public void SetExposureTime(long value)
        {
            try
            {
                // Some camera models may have auto functions enabled. To set the ExposureTime value to a specific value,
                // the ExposureAuto function must be disabled first (if ExposureAuto is available).
                camera.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Off); // Set ExposureAuto to Off if it is writable.
                camera.Parameters[PLCamera.ExposureMode].TrySetValue(PLCamera.ExposureMode.Timed); // Set ExposureMode to Timed if it is writable.

                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    // In previous SFNC versions, ExposureTimeRaw is an integer parameter,单位us
                    // integer parameter的数据，设置之前，需要进行有效值整合，否则可能会报错
                    long min = camera.Parameters[PLCamera.ExposureTimeRaw].GetMinimum();
                    long max = camera.Parameters[PLCamera.ExposureTimeRaw].GetMaximum();
                    long incr = camera.Parameters[PLCamera.ExposureTimeRaw].GetIncrement();
                    if (value < min)
                    {
                        value = min;
                    }
                    else if (value > max)
                    {
                        value = max;
                    }
                    else
                    {
                        value = min + (((value - min) / incr) * incr);
                    }
                    camera.Parameters[PLCamera.ExposureTimeRaw].SetValue(value);

                    // Or,here, we let pylon correct the value if needed.
                    //camera.Parameters[PLCamera.ExposureTimeRaw].SetValue(value, IntegerValueCorrection.Nearest);
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    // In SFNC 2.0, ExposureTimeRaw is renamed as ExposureTime,is a float parameter, 单位us.
                    camera.Parameters[PLUsbCamera.ExposureTime].SetValue((double)value);
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /// <summary>
        /// 获取最小最大曝光时间
        /// </summary>
        public void GetMinMaxExposureTime()
        {
            try
            {
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    minExposureTime = camera.Parameters[PLCamera.ExposureTimeRaw].GetMinimum();
                    maxExposureTime = camera.Parameters[PLCamera.ExposureTimeRaw].GetMaximum();
                }
                else
                {
                    minExposureTime = (long)camera.Parameters[PLUsbCamera.ExposureTime].GetMinimum();
                    maxExposureTime = (long)camera.Parameters[PLUsbCamera.ExposureTime].GetMaximum();
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="value"></param>
        public void SetGain(long value)
        {
            try
            {
                // Some camera models may have auto functions enabled. To set the gain value to a specific value,
                // the Gain Auto function must be disabled first (if gain auto is available).
                camera.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Off); // Set GainAuto to Off if it is writable.

                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    // Some parameters have restrictions. You can use GetIncrement/GetMinimum/GetMaximum to make sure you set a valid value.                              
                    // In previous SFNC versions, GainRaw is an integer parameter.
                    // integer parameter的数据，设置之前，需要进行有效值整合，否则可能会报错
                    long min = camera.Parameters[PLCamera.GainRaw].GetMinimum();
                    long max = camera.Parameters[PLCamera.GainRaw].GetMaximum();
                    long incr = camera.Parameters[PLCamera.GainRaw].GetIncrement();
                    if (value < min)
                    {
                        value = min;
                    }
                    else if (value > max)
                    {
                        value = max;
                    }
                    else
                    {
                        value = min + (((value - min) / incr) * incr);
                    }
                    camera.Parameters[PLCamera.GainRaw].SetValue(value);

                    //// Or,here, we let pylon correct the value if needed.
                    //camera.Parameters[PLCamera.GainRaw].SetValue(value, IntegerValueCorrection.Nearest);
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    // In SFNC 2.0, Gain is a float parameter.
                    camera.Parameters[PLUsbCamera.Gain].SetValue(value);
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /// <summary>
        /// 获取最小最大增益
        /// </summary>
        public void GetMinMaxGain()
        {
            try
            {
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    minGain = camera.Parameters[PLCamera.GainRaw].GetMinimum();
                    maxGain = camera.Parameters[PLCamera.GainRaw].GetMaximum();
                }
                else
                {
                    minGain = (long)camera.Parameters[PLUsbCamera.Gain].GetMinimum();
                    maxGain = (long)camera.Parameters[PLUsbCamera.Gain].GetMaximum();
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /// <summary>
        /// 设置相机Freerun模式
        /// </summary>
        public void SetFreerun()
        {
            try
            {
                // Set an enum parameter.
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                    }
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                    }
                }
                stopWatch.Restart();    // ****  重启采集时间计时器   ****
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /// <summary>
        /// 设置相机软触发模式
        /// </summary>
        public void SetSoftwareTrigger()
        {
            try
            {
                // Set an enum parameter.
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                        }
                    }
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                        }
                    }
                }
                stopWatch.Reset();    // ****  重置采集时间计时器   ****
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /// <summary>
        /// 发送软触发命令
        /// </summary>
        public void SendSoftwareExecute()
        {
            try
            {
                if (camera.WaitForFrameTriggerReady(1000, TimeoutHandling.ThrowException))
                {
                    camera.ExecuteSoftwareTrigger();
                    stopWatch.Restart();    // ****  重启采集时间计时器   ****
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }















        


       

        

        /// <summary>
        /// 设置相机外触发模式
        /// </summary>
        public void SetExternTrigger()
        {
            try
            {
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                    }

                    //Sets the trigger delay time in microseconds.
                    camera.Parameters[PLCamera.TriggerDelayAbs].SetValue(5);        // 设置触发延时

                    //Sets the absolute value of the selected line debouncer time in microseconds
                    camera.Parameters[PLCamera.LineSelector].TrySetValue(PLCamera.LineSelector.Line1);
                    camera.Parameters[PLCamera.LineMode].TrySetValue(PLCamera.LineMode.Input);
                    camera.Parameters[PLCamera.LineDebouncerTimeAbs].SetValue(5);       // 设置去抖延时，过滤触发信号干扰

                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                    }

                    //Sets the trigger delay time in microseconds.//float
                    camera.Parameters[PLCamera.TriggerDelay].SetValue(5);       // 设置触发延时

                    //Sets the absolute value of the selected line debouncer time in microseconds
                    camera.Parameters[PLCamera.LineSelector].TrySetValue(PLCamera.LineSelector.Line1);
                    camera.Parameters[PLCamera.LineMode].TrySetValue(PLCamera.LineMode.Input);
                    camera.Parameters[PLCamera.LineDebouncerTime].SetValue(5);       // 设置去抖延时，过滤触发信号干扰

                }
                stopWatch.Reset();    // ****  重置采集时间计时器   ****
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }
        /****************************************************/


        /****************  图像响应事件函数  ****************/


        // 相机取像回调函数.
        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.

                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

                // Check if the image can be displayed.
                if (grabResult.GrabSucceeded)
                {
                    grabTime = stopWatch.ElapsedMilliseconds;
                    eventComputeGrabTime(grabTime);

                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    // ****  降低显示帧率，减少CPU占用率  **** //
                    //if (!stopWatch.IsRunning || stopWatch.ElapsedMilliseconds > 33)

                    if (grabResult.PixelTypeValue == PixelType.Mono8)
                    {
                        //allocate the m_stream_size amount of bytes in non-managed environment 
                        if (latestFrameAddress == IntPtr.Zero)
                        {
                            latestFrameAddress = Marshal.AllocHGlobal((Int32)grabResult.PayloadSize);
                        }
                        converter.OutputPixelFormat = PixelType.Mono8;
                        converter.Convert(latestFrameAddress, grabResult.PayloadSize, grabResult);


                        Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format8bppIndexed);
                        BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        //converter.OutputPixelFormat = PixelType.Mono8;
                        IntPtr ptrBmp = bmpData.Scan0;
                        converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                        bitmap.UnlockBits(bmpData);
                        eventProcessImage(bitmap, ptrBmp);
                        bitmap.Dispose();
                    }
                }

                else
                {
                 //   MessageBox.Show("Grab faild!\n" + grabResult.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
          
        }

        /****************************************************/


        /// <summary>
        /// 掉线重连回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionLost(Object sender, EventArgs e)
        {
            try
            {
                camera.Close();

                for (int i = 0; i < 2; i++)
                {
                    try
                    {
                        camera.Open();
                        if (camera.IsOpen)
                        {
                            MessageBox.Show("已重新连接上UserID为“" + strUserID + "”的相机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        Thread.Sleep(200);
                    }
                    catch
                    {
                        MessageBox.Show("请重新连接UserID为“" + strUserID + "”的相机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (camera == null)
                {
                    MessageBox.Show("重连超时20s:未识别到UserID为“" + strUserID + "”的相机！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                SetHeartBeatTime(5000);
                //camera.Parameters[PLCamera.AcquisitionFrameRateEnable].SetValue(true);  // 限制相机帧率
                //camera.Parameters[PLCamera.AcquisitionFrameRateAbs].SetValue(90);
                //camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(10);          // 设置内存中接收图像缓冲区大小

                imageWidth = camera.Parameters[PLCamera.Width].GetValue();               // 获取图像宽 
                imageHeight = camera.Parameters[PLCamera.Height].GetValue();              // 获取图像高
                GetMinMaxExposureTime();
                GetMinMaxGain();
                camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;                      // 注册采集回调函数
                camera.ConnectionLost += OnConnectionLost;
                camera.StreamGrabber.Start();
                Thread.Sleep(10);
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }

        // Shows exceptions in a message box.
        private void ShowException(Exception exception)
        {
        //    MessageBox.Show("Exception caught:\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
