using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using cn.com.farsight.IM.IMModel;
using cn.com.farsight.IM.ToolsHelper;
using Newtonsoft.Json;
using cn.com.farsight.IM.IMDbVisit;
using System.Web.SessionState;
using System.IO;
using System.Net.Mail;

namespace cn.com.farsight.IM.IMWebApplication.Admin
{
    /// <summary>
    /// cureinfo 的摘要说明
    /// </summary>
    public class cureinfo : IHttpHandler, IRequiresSessionState
    {
      
        public void ProcessRequest(HttpContext context)
        {
            PatientManager pm = new PatientManager();
            ResultManager rm = new ResultManager();
            TaskManager tm = new TaskManager();
            DoctorManager dm = new DoctorManager();
            //context.Response.ContentType = "text/plain";
            if (context.Request.RequestType == "GET")
            {
                if (context.Request["gettype"] != null)
                {
                    if (context.Request["gettype"] == "gettemperature")
                    {
                        #region 获取历史温度
                        int id = int.Parse(context.Request["patient_id"]);
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        List<task> temp = new List<task>();
                        List<result> list = new List<result>();

                        temp.AddRange(tm.getModelList(new task() { Patient_id = new patient() { Id = id } }));
                        foreach (var item in temp)
                        {
                            if (item.Result_id != null)
                            {
                                if (rm.getModel(item.Result_id) && !string.IsNullOrEmpty(item.Result_id.Temperature))
                                    list.Add(item.Result_id);
                            }
                        }
                        Bitmap bt = get_temperature_image(list);
                        temp.Clear();
                        list.Clear();
                        bt.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        context.Response.ClearContent();
                        context.Response.ContentType = "image/Jpeg";
                        context.Response.BinaryWrite(ms.ToArray());
                        ms.Close();
                        #endregion
                    }
                    else if (context.Request["gettype"] == "getecg")
                    {
                        #region 获取当前心电图
                        int id = int.Parse(context.Request["task_id"]);
                        task t = new task() { Id = id };
                        if (tm.getModel(t) && t.Result_id != null && rm.getModel(t.Result_id))
                        {
                            Bitmap bt;
                            if (!string.IsNullOrEmpty(t.Result_id.Ecg_data))
                                bt = get_ecg_image(t.Result_id.Ecg_data);
                            else
                                bt = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "images\\ecg_back.png");
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            bt.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            context.Response.ClearContent();
                            context.Response.ContentType = "image/Png";
                            context.Response.BinaryWrite(ms.ToArray());
                        }
                        #endregion
                    }
                    else if (context.Request["gettype"] == "getbloodpress")
                    {
                        #region 获取历史血压
                        int id = int.Parse(context.Request["patient_id"]);
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        List<task> temp = new List<task>();
                        List<result> list = new List<result>();

                        temp.AddRange(tm.getModelList(new task() { Patient_id = new patient() { Id = id } }));
                        foreach (var item in temp)
                        {
                            if (item.Result_id != null)
                            {
                                if (rm.getModel(item.Result_id) && !string.IsNullOrEmpty(item.Result_id.Hp_data))
                                    list.Add(item.Result_id);
                            }
                        }

                        Bitmap bt = get_bloodpress_image(list);
                        temp.Clear();
                        list.Clear();
                        bt.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        context.Response.ClearContent();
                        context.Response.ContentType = "image/Jpeg";
                        context.Response.BinaryWrite(ms.ToArray());
                        #endregion
                    }
                    return;
                }
                context.Response.ContentType = "text/html";
                StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/html/cureinfo.html");
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
                    Patient_Info pc = new Patient_Info();
                    pc.patient_name = p.Patient_name;
                    pc.patient_gender = p.Patient_gender;
                    pc.patient_tel = p.Patient_tel;
                    pc.patient_dob = p.Patient_dob;
                    pc.patient_add = p.Patient_add;

                    if (int.TryParse(taid, out id))
                    {
                        task t = new task();
                        t.Id = id;
                        if (tm.getModel(t))
                        {
                            if (t.Doctor_id != null)
                            {
                                if (dm.getModel(t.Doctor_id))
                                    pc.doctorName = t.Doctor_id.Doctor_tname;
                            }
                            if (t.Result_id != null && rm.getModel(t.Result_id))
                            {
                                pc.task_item = t.Task_items.HasValue ? t.Task_items.Value : 0;
                                //if (ecg)
                                //    temp |= 1 << 0;
                                //if (bloodpress)
                                //    temp |= 1 << 1;
                                //if (glucose)
                                //    temp |= 1 << 2;
                                //if (temperature)
                                //    temp |= 1 << 3;
                                if ((pc.task_item & (1 << 0)) != 0)
                                {
                                    pc.ecg_info = t.Result_id.Ecg_info;
                                    if (t.Result_id.Ecg_time.HasValue)
                                        pc.ecg_date = (Convert.ToDateTime("1970-01-01 00:00:00").AddMilliseconds(t.Result_id.Ecg_time.Value).AddHours(8)).ToString("yyyy-MM-dd hh:mm");
                                }
                                pc.ecg_data = "cureinfo.ashx?task_id=" + t.Id + "&gettype=getecg";
                                if ((pc.task_item & (1 << 3)) != 0)
                                {
                                    pc.temperature = t.Result_id.Temperature;
                                    if (t.Result_id.Temperature_time.HasValue)
                                        pc.temperature_date = (Convert.ToDateTime("1970-01-01 00:00:00").AddMilliseconds(t.Result_id.Temperature_time.Value).AddHours(8)).ToString("yyyy-MM-dd hh:mm");
                                    pc.temperature_info = t.Result_id.Temperature_info;
                                }
                                pc.temperature_history = "cureinfo.ashx?patient_id=" + p.Id + "&gettype=gettemperature";
                                if ((pc.task_item & (1 << 1)) != 0)
                                {
                                    pc.hp_data = t.Result_id.Hp_data;
                                    pc.lp_data = t.Result_id.Lp_data;

                                    pc.bloodpress_info = t.Result_id.Bloodpress_info;
                                    if (t.Result_id.Bloodpress_time.HasValue)
                                        pc.bloodpress_time = (Convert.ToDateTime("1970-01-01 00:00:00").AddMilliseconds(t.Result_id.Bloodpress_time.Value).AddHours(8)).ToString("yyyy-MM-dd hh:mm");
                                }
                                pc.bloodpress_history = "cureinfo.ashx?patient_id=" + p.Id + "&gettype=getbloodpress";
                                pc.info = t.Result_id.Info;
                            }
                        }
                    }
                    context.Response.Write(JsonConvert.SerializeObject(pc));
                    #endregion
                }
                else if (type == "up_result")
                {
                    #region 专家上传结果

                    string taid = context.Request["task_id"];
                    string data = context.Request["data"];
                    Patient_Info p = JsonConvert.DeserializeObject<Patient_Info>(data);
                    int id;
                    if (!int.TryParse(taid, out id))
                    {
                        context.Response.Write("false");
                        return;
                    }
                    task t = new task() { Id = id };
                    if (tm.getModel(t) && t.Result_id != null && rm.getModel(t.Result_id))
                    {
                        t.Result_id.Bloodpress_info = p.bloodpress_info.Replace("诊断结果:", "");
                        t.Result_id.Ecg_info = p.ecg_info.Replace("诊断结果:", "");
                        t.Result_id.Temperature_info = p.temperature_info.Replace("诊断结果:", "");
                        t.Result_id.Info = p.info.Replace("专家建议:", "");
                        if (rm.updateModel(t.Result_id))
                        {
                            context.Response.Write("true");
                            if (pm.getModel(t.Patient_id))
                            {
                                this.SendMail(t.Patient_id.Patient_mail, t.Patient_id.Patient_name, context.Request.UrlReferrer.AbsoluteUri);
                            }
                        }
                        else
                            context.Response.Write("false");
                        return;
                    }
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
            //int all_ck = 20 * 5;//所有方格数量
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
        public Bitmap get_temperature_image(List<result> list)
        {
            Bitmap bm = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "images\\lc_tem.jpg");
            if (list.Count == 0)
                return bm;
            Graphics g = Graphics.FromImage(bm);
            //保证日期递增
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - 1; j++)
                {
                    if (list[j].Temperature_time > list[j + 1].Temperature_time)
                    {
                        result r = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = r;
                    }
                }
            }
            int eachpx = 500 / (list.Count + 1);
            int y_px = 175;
            int x_px = 40;
            Brush b = new SolidBrush(Color.FromArgb(29, 23, 141));//折线填充色
            Brush black = new SolidBrush(Color.Black);
            Font font = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
            /*
            g.DrawString("34", font, black, new PointF(23, 168));
            g.DrawString("36", font, black, new PointF(23, 138));
            g.DrawString("38", font, black, new PointF(23, 108));
            g.DrawString("40", font, black, new PointF(23, 78));
            g.DrawString("42", font, black, new PointF(23, 48));
            Pen p = new Pen(black, 3);
            g.DrawLine(p, 40, 142, 50, 142);
            g.DrawLine(p, 40, 112, 50, 112);
            g.DrawLine(p, 40, 82, 50, 82);
            g.DrawLine(p, 40, 52, 50, 52);
             * */
            Pen p = new Pen(black, 3);
            //绘制X轴
            for (int i = 0; i < list.Count; i++)
            {
                g.DrawLine(p, x_px + eachpx * i + eachpx, y_px, x_px + eachpx * i + eachpx, y_px - 10);
                g.DrawString((Convert.ToDateTime("1970-01-01 00:00:00").AddMilliseconds(list[i].Temperature_time.Value).AddHours(8)).ToString("yyyy-MM-dd"), font, black, new PointF(x_px + eachpx * i + eachpx - 25, y_px));
            }

            Pen line = new Pen(b, 2);
            Pen point = new Pen(b, 2);
            //bm.SetPixel(x_px + eachpx, (int.Parse(list[0].Temperature) - 34) * 15, Color.FromArgb(29, 23, 141));
            g.DrawRectangle(point, x_px + eachpx - 1, y_px - (float.Parse(list[0].Temperature) - 34) * 15 - 1, 3, 3);
            for (int i = 1; i < list.Count; i++)
            {
                g.DrawRectangle(point, x_px + eachpx * (i + 1) - 1, y_px - (float.Parse(list[i].Temperature) - 34) * 15 - 1, 3, 3);
                g.DrawLine(line, x_px + eachpx * i, y_px - (float.Parse(list[i - 1].Temperature) - 34) * 15, x_px + eachpx * (i + 1), y_px - (float.Parse(list[i].Temperature) - 34) * 15);
            }
            return bm;

        }
        public Bitmap get_bloodpress_image(List<result> list)
        {
            Bitmap bm = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "images\\lc_bp.jpg");
            if (list.Count == 0)
                return bm;
            Graphics g = Graphics.FromImage(bm);
            //保证日期递增
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - 1; j++)
                {
                    if (list[j].Bloodpress_time > list[j + 1].Bloodpress_time)
                    {
                        result r = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = r;
                    }
                }
            }
            int eachpx = 500 / (list.Count + 1);
            int y_px = 175;
            int x_px = 40;

            Brush black = new SolidBrush(Color.Black);
            Font font = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
            /*
            g.DrawString("0", font, black, new PointF(32, 168));
            g.DrawString("50", font, black, new PointF(28, 143));
            g.DrawString("100", font, black, new PointF(23, 118));
            g.DrawString("150", font, black, new PointF(23, 93));
            g.DrawString("200", font, black, new PointF(23, 68));
            g.DrawString("250", font, black, new PointF(23, 43));
            g.DrawString("300", font, black, new PointF(23, 18));
            Pen p = new Pen(black, 3);
            g.DrawLine(p, 40, 149, 50, 149);
            g.DrawLine(p, 40, 124, 50, 124);
            g.DrawLine(p, 40, 99, 50, 99);
            g.DrawLine(p, 40, 74, 50, 74);
            g.DrawLine(p, 40, 49, 50, 49);
            g.DrawLine(p, 40, 24, 50, 24);
            */

            Pen p = new Pen(black, 3);
            //绘制X轴
            for (int i = 0; i < list.Count; i++)
            {
                g.DrawLine(p, x_px + eachpx * i + eachpx, y_px, x_px + eachpx * i + eachpx, y_px - 10);
                g.DrawString((Convert.ToDateTime("1970-01-01 00:00:00").AddMilliseconds(list[i].Bloodpress_time.Value).AddHours(8)).ToString("yyyy-MM-dd"), font, black, new PointF(x_px + eachpx * i + eachpx - 25, y_px));
            }
            Brush hb = new SolidBrush(Color.FromArgb(29, 23, 141));//折线填充色
            Brush lb = new SolidBrush(Color.FromArgb(188, 125, 134));
            Pen hp = new Pen(hb, 2);
            Pen lp = new Pen(lb, 2);
            Pen hpoint = new Pen(hb, 1);
            Pen lpoint = new Pen(lb, 1);
            //bm.SetPixel(x_px + eachpx, y_px - int.Parse(list[0].Hp_data) / 2, Color.FromArgb(29, 23, 141));
            //bm.SetPixel(x_px + eachpx, y_px - int.Parse(list[0].Lp_data) / 2, Color.FromArgb(29, 23, 141));
            g.DrawRectangle(hpoint, x_px + eachpx - 1, y_px - float.Parse(list[0].Hp_data) / 2 - 1, 3, 3);
            g.DrawRectangle(lpoint, x_px + eachpx - 1, y_px - float.Parse(list[0].Lp_data) / 2 - 1, 3, 3);
            for (int i = 1; i < list.Count; i++)
            {
                g.DrawRectangle(hpoint, x_px + eachpx * i - 1, y_px - float.Parse(list[i].Hp_data) / 2 - 1, 3, 3);
                g.DrawRectangle(lpoint, x_px + eachpx * i - 1, y_px - float.Parse(list[i].Lp_data) / 2 - 1, 3, 3);
                g.DrawLine(hp, x_px + eachpx * i, y_px - float.Parse(list[i - 1].Hp_data) / 2, x_px + eachpx * (i + 1), y_px - float.Parse(list[i].Hp_data) / 2);
                g.DrawLine(lp, x_px + eachpx * i, y_px - float.Parse(list[i - 1].Lp_data) / 2, x_px + eachpx * (i + 1), y_px - float.Parse(list[i].Lp_data) / 2);
            }
            return bm;

        }
        #region 发送邮件：此方法可行
        protected void SendMail(string mail, string name, string url)
        {
            string body = "<h2 style=\"font-family:Arial, sans-serif; font-size: 18px; color: #3f3f3f; font-weight: bold; padding:0; margin:0;\">" +
                "尊敬的 " + name + "：</h2>" +
                "<p style=\"font-family:Arial, sans-serif; font-size: 13px; line-height:15px; color: #000000; padding:0; margin:0; margin-top:15px; margin-bottom:15px;\">" +
                "您的体检报告已生成，请点击链接查看：<a style=\"color: #0857a6; text-decoration: underline;\" href=\"" +
                url + "\">" + url + "</a></p>;";

            SendSMTPEMail("smtp.ym.163.com", "xuf@farsight.com.cn", "jfbloc564196761!", mail, "智能医疗-检查结果", body);
        }
        /// <summary>
        /// 使用smtp发送电子邮件
        /// </summary>
        /// <param name="strSmtpServer">smtp服务器地址</param>
        /// <param name="strFrom">发件人邮箱用户名</param>
        /// <param name="strFromPass">发件人邮箱密码</param>
        /// <param name="strto">收件人邮箱</param>
        /// <param name="strSubject">主题</param>
        /// <param name="strBody">内容</param>
        public void SendSMTPEMail(string strSmtpServer, string strFrom, string strFromPass, string strto, string strSubject, string strBody)
        {
            System.Net.Mail.SmtpClient client = new SmtpClient(strSmtpServer);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(strFrom, strFromPass);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.Mail.MailMessage message = new MailMessage(strFrom, strto, strSubject, strBody);

            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            try
            {
                client.Send(message);
            }
            catch (Exception ee)
            {

            }
        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}