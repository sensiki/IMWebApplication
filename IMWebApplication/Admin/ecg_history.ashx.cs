using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using cn.com.farsight.IM.IMDbVisit;
using cn.com.farsight.IM.IMModel;
using System.Drawing;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using cn.com.farsight.IM.ToolsHelper;

namespace cn.com.farsight.IM.IMWebApplication.Admin
{
    /// <summary>
    /// ecg_history 的摘要说明
    /// </summary>
    public class ecg_history : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            PatientManager pm = new PatientManager();
            ResultManager rm = new ResultManager();
            TaskManager tm = new TaskManager();
            DoctorManager dm = new DoctorManager();
            if (context.Request.RequestType == "GET")
            {
                if (context.Request["gettype"] != null)
                {
                    if (context.Request["gettype"] == "getecg")
                    {
                        #region 获取当前心电图
                        int id = int.Parse(context.Request["task_id"]);
                        task t = new task() { Id = id };
                        if (tm.getModel(t) && t.Result_id != null && rm.getModel(t.Result_id))
                        {
                            if (!string.IsNullOrEmpty(t.Result_id.Ecg_data))
                            {
                                Bitmap bt = get_ecg_image(t.Result_id.Ecg_data);
                                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                                bt.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                context.Response.ClearContent();
                                context.Response.ContentType = "image/Png";
                                context.Response.BinaryWrite(ms.ToArray());
                            }
                        }
                        #endregion
                    }
                    return;
                }
                context.Response.ContentType = "text/html";
                StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/html/ecg_history.html");
                string html = reader.ReadToEnd();
                reader.Close();
                context.Response.Write(html);
            }
            else
            {
                string type = context.Request["type"];
                if (type == "getpatient")
                {
                    #region 获取患者信息
                    string paid = context.Request["patient_id"];
                    string taid = context.Request["task_id"];
                    int id;
                    if (!int.TryParse(paid, out id))
                    {
                        context.Response.Write("false");
                        return;
                    }
                    patient p = new patient() { Id = id };
                    if (!pm.getModel(p))
                    {
                        context.Response.Write("false");
                        return;
                    }
                    ecg_historyResult ehr = new ecg_historyResult();
                    ehr.patient_name = p.Patient_name;
                    ehr.patient_gender = p.Patient_gender;
                    ehr.patient_tel = p.Patient_tel;
                    ehr.patient_dob = p.Patient_dob;
                    ehr.patient_add = p.Patient_add;
                    StringBuilder sb = new StringBuilder();
                    List<task> list = tm.getModelList(new task() { Patient_id = p });
                    foreach (var item in list)
                    {
                        if (item.Result_id != null && (item.Task_items & (1 << 0)) != 0 && rm.getModel(item.Result_id))
                        {
                            sb.Append("<span>心电结果:</span> <span id='ecg_date'>采集时间:" + (Convert.ToDateTime("1970-01-01 00:00:00").AddMilliseconds(item.Result_id.Ecg_time.Value).AddHours(8)).ToString("yyyy-MM-dd hh:mm")
                                + "</span><img id='ecg_data' src='ecg_history.ashx?gettype=getecg&task_id=" + item.Id
                                + "' title='心电图' alt='心电图' onclick='window.open(this.src)'/><textarea id='ecg_info'>" + item.Result_id.Ecg_info
                                + "</textarea><hr />");
                        }
                    }
                    ehr.ecg_history = sb.ToString();
                    context.Response.Write(JsonConvert.SerializeObject(ehr));
                    #endregion
                }
            }
        }
        public static int[] convertByteArrToIntArr(byte[] byteArr)
        {

            int remained = byteArr.Length % 4;
            int intNum = 0;

            if (remained != 0)
                return null;

            //
            intNum = byteArr.Length / 4;

            //
            int[] intArr = new int[intNum];

            int ch1, ch2, ch3, ch4;
            for (int j = 0, k = 0; j < intArr.Length; j++, k += 4)
            {

                ch1 = byteArr[k];
                ch2 = byteArr[k + 1];
                ch3 = byteArr[k + 2];
                ch4 = byteArr[k + 3];

                //
                if (ch1 < 0)
                {
                    ch1 = 256 + ch1;
                }
                if (ch2 < 0)
                {
                    ch2 = 256 + ch2;
                }
                if (ch3 < 0)
                {
                    ch3 = 256 + ch3;
                }
                if (ch4 < 0)
                {
                    ch4 = 256 + ch4;
                }

                intArr[j] = (ch1 << 24) + (ch2 << 16) + (ch3 << 8) + (ch4 << 0);
            }

            return intArr;
        }

        public Bitmap get_ecg_image(string ecg_data)
        {
            int multiple = 2;//放大倍数
            int width = 800 * multiple;
            int height = 480 * multiple;
            int bk_width = width;
            int bk_height = height;
            int ey_ck_ct = 200 / 5;//每200ms纸张走1个方格, 设备每5ms获取一个数据
            int show_interval = 40 * multiple / ey_ck_ct;//数据间隔像素
            int start_y = height / 2;//心电图开始位置
            float ey_y = (5 * 40 * multiple) / 500f;//
            int[] bytes = convertByteArrToIntArr(Base64.decodeBase64(ecg_data));
            //sbyte[] sbytes = new sbyte[bytes.Length];
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    if (bytes[i] > 127)
            //        sbytes[i] = (sbyte)(bytes[i] - 256);
            //    else
            //        sbytes[i] = (sbyte)bytes[i];
            //}
            //byte[] list = new byte[bytes.Length / 4];
            //for (int i = 3, j = 0; i < bytes.Length; i += 4)
            //{
            //    list[j++] = bytes[i];
            //}
            bk_width = bytes.Length * multiple;//理论背景宽度
            bk_width = bk_width % width != 0 ? (bk_width / width + 1) : bk_width / width;//实际宽度
            Brush b = new SolidBrush(Color.Black);
            Pen p = new Pen(b, 1);
            Bitmap bm = new Bitmap(width * bk_width, height);
            Graphics g = Graphics.FromImage(bm);

            for (int i = 0; i < bk_width; i++)
                g.DrawImage(Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "images\\ecg_back.png"), i * width, 0, width, height);

            //bm.SetPixel(show_interval, start_y + sbytes[0] * ey_y, Color.Black);
            for (int i = 1; i < bytes.Length; i++)
            {
                // bm.SetPixel(show_interval * (i + 1), start_y + sbytes[i] * ey_y, Color.Black);
                g.DrawLine(p, i * show_interval, height - (start_y + bytes[i + -1] * ey_y), (i + 1) * show_interval, height - (start_y + bytes[i] * ey_y));
            }
            return bm;
        }
       
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}