using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using cn.com.farsight.IM.IMModel;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using cn.com.farsight.IM.IMDbVisit;
using System.IO;
using System.Web.SessionState;

namespace cn.com.farsight.IM.IMWebApplication.Admin
{
    /// <summary>
    /// newtask 的摘要说明
    /// </summary>
    public class newtask : IHttpHandler, IRequiresSessionState
    {
        class post_newtask
        {
            public int task { get; set; }
            public byte[] patient { get; set; }
            public int doctor { get; set; }
        }
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            if (context.Request.RequestType == "GET")
            {
                context.Response.ContentType = "text/html";
                if (context.Session["LoginUserName"] == null || string.IsNullOrEmpty(context.Session["LoginUserName"].ToString()) || context.Session["LoginUserName"].ToString() != "admin")
                    context.Response.Write("<script>alert(\"权限不足,请重新登录!\");window.location.href=\"login.ashx\"</script>");
                else
                {
                    StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/html/newtask.html");
                    string html = reader.ReadToEnd();
                    reader.Close();
                    context.Response.Write(html);
                }
            }
            else
            {
                PatientManager pm = new PatientManager();
                TaskManager tm = new TaskManager();
                ResultManager rm = new ResultManager();
                DoctorManager dm = new DoctorManager();
                if (context.Request["type"] == null || string.IsNullOrEmpty(context.Request["type"]))
                    context.Response.Write("<script>alert(\"请求出错!\");window.location.href=\"login.ashx\"</script>");
                else
                {
                    if (context.Request["type"] == "newtask")
                    {
                        #region 创建患者跳转
                        string request = context.Request["patient_id"];
                        patient p = new patient();
                        List<patient> list = new List<patient>();
                        int id;
                        if (int.TryParse(request, out id))
                        {
                            p.Id = id;
                            if (pm.getModel(p))
                                list.Add(p);
                        }

                        Simple_Result2 sp = new Simple_Result2();
                        sp.result = "true";
                        sp.data = create_patient_list(list);
                        list.Clear();
                        context.Response.Write(JsonConvert.SerializeObject(sp));
                        #endregion
                    }
                    else if (context.Request["type"] == "slpatient")
                    {
                        #region 患者管理查询
                        string request = context.Request["slpatient"];
                        patient p = new patient();
                        List<patient> list = new List<patient>();
                        List<patient> temp = new List<patient>();
                        p.Patient_tel = request;
                        list.AddRange(pm.getModelList(p));
                        if (!string.IsNullOrEmpty(request))
                        {
                            p.Patient_tel = string.Empty;
                            p.Patient_idcard = request;
                            temp.AddRange(pm.getModelList(p));
                            foreach (var item in temp)
                            {
                                if (list.Find(delegate(patient pa) { return pa.Id == item.Id; }) != null)
                                    continue;
                                list.Add(item);
                            }
                            temp.Clear();
                            p.Patient_idcard = string.Empty;
                            p.Patient_name = request;
                            temp.AddRange(pm.getModelList(p));
                            foreach (var item in temp)
                            {
                                if (list.Find(delegate(patient pa) { return pa.Id == item.Id; }) != null)
                                    continue;
                                list.Add(item);
                            }
                            temp.Clear();
                            p.Patient_name = string.Empty;
                            p.Patient_add = request;
                            temp.AddRange(pm.getModelList(p));
                            foreach (var item in temp)
                            {
                                if (list.Find(delegate(patient pa) { return pa.Id == item.Id; }) != null)
                                    continue;
                                list.Add(item);
                            }
                            temp.Clear();
                            p.Patient_add = string.Empty;
                            p.Patient_qr_code = request;
                            temp.AddRange(pm.getModelList(p));
                            foreach (var item in temp)
                            {
                                if (list.Find(delegate(patient pa) { return pa.Id == item.Id; }) != null)
                                    continue;
                                list.Add(item);
                            }
                            temp.Clear();
                        }

                        Simple_Result2 sp = new Simple_Result2();
                        sp.result = "true";
                        sp.data = create_patient_list(list);
                        list.Clear();
                        context.Response.Write(JsonConvert.SerializeObject(sp));
                        #endregion
                    }
                    else if (context.Request["type"] == "sldocta")
                    {
                        #region 医生管理查询
                        string request = context.Request["sldoc"];
                        Regex r = new Regex("\\d");
                        Match mc = r.Match(request);
                        doctor d = new doctor();
                        if (mc.Success)
                        {
                            d.Doctor_tel = request;
                        }
                        else
                            d.Doctor_name = request;
                        List<doctor> list = dm.getModelList(d);
                        foreach (var item in list)
                        {
                            if (item.Doctor_name == "admin")
                            {
                                list.Remove(item);
                                break;
                            }
                        }
                        //patientList.Rows
                        Simple_Result2 sd = new Simple_Result2();
                        sd.result = "true";
                        sd.data = create_doctor_list(list);
                        list.Clear();
                        context.Response.Write(JsonConvert.SerializeObject(sd));
                        #endregion
                    }
                    else if (context.Request["type"] == "settask")
                    {
                        #region 确定新增任务
                        string list = context.Request["list"];
                        post_newtask pn = JsonConvert.DeserializeObject<post_newtask>(list);
                        task t = new task();
                        t.Patient_id = new patient();
                        t.Doctor_id = new doctor();
                        for (int i = 0; i < pn.patient.Length; i++)
                        {
                            t.Patient_id.Id = pn.patient[i];
                            t.Task_items = pn.task;
                            t.Doctor_id.Id = pn.doctor;
                            t.Date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            tm.addModel(t);
                        }
                        context.Response.Write("true");
                        #endregion
                    }
                }

            }
        }
        private string create_doctor_list(List<doctor> list)
        {
            StringBuilder sb = new StringBuilder();
            int i;
            sb.Append("<thead><tr><th>医生编号</th><th>医生名</th><th>性别</th><th>年龄</th><th>级别</th><th>选择</th></tr></thead><tbody>");
            for (i = 0; i < 6 && i < list.Count; i++)
            {
                sb.Append("<tr><td>" + list[i].Id.Value.ToString() +
                    "</td><td>" + list[i].Doctor_tname +
                    "</td><td>" + list[i].Doctor_gender +
                    "</td><td>" + (DateTime.Now.Year - DateTime.Parse(list[i].Doctor_dob).Year).ToString() +
                    "</td><td>" + list[i].Doctor_level +
                    "</td><td><input name='doclist' value='" + list[i].Id.Value.ToString() + "' type ='radio'/></td></tr>");
            }
            for (; i < 6; i++)
            {
                sb.Append("<tr><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            }
            sb.Append("</tbody>");
            return sb.ToString();
        }
        private string create_patient_list(List<patient> list)
        {
            StringBuilder sb = new StringBuilder();
            int i;
            sb.Append("<thead><tr><th>编号</th><th>姓名</th><th>性别</th><th>年龄</th><th>联系方式</th><th>住址</th><th>操作</th></tr></thead><tbody>");
            for (i = 0; i < list.Count; i++)
            {
                sb.Append("<tr><td>" + list[i].Id.Value.ToString() +
                    "</td><td>" + list[i].Patient_name +
                    "</td><td>" + list[i].Patient_gender +
                    "</td><td>" + (DateTime.Now.Year - DateTime.Parse(list[i].Patient_dob).Year).ToString() +
                    "</td><td>" + list[i].Patient_tel +
                    "</td><td>" + list[i].Patient_add +
                    "</td><td><input name='tasklist' value='" + list[i].Id.Value.ToString() + "' type ='checkbox'/></td></tr>");
            }
            for (; i < 10; i++)
            {
                sb.Append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            }
            sb.Append("</tbody>");
            return sb.ToString();
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