using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using cn.com.farsight.IM.IMModel;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Web.SessionState;
using cn.com.farsight.IM.IMDbVisit;
using System.IO;

namespace cn.com.farsight.IM.IMWebApplication.Admin
{
    /// <summary>
    /// admin 的摘要说明
    /// </summary>
    public class admin : IHttpHandler, IRequiresSessionState
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
                context.Response.ContentType = "text/html";
                if (context.Session["LoginUserName"] == null || string.IsNullOrEmpty(context.Session["LoginUserName"].ToString()) || context.Session["LoginUserName"].ToString() != "admin")
                    context.Response.Write("<script>alert(\"权限不足,请重新登录!\");window.location.href=\"login.ashx\"</script>");
                else
                {

                    context.Response.ContentType = "text/html";
                    StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/html/admin.html");
                    string html = reader.ReadToEnd();
                    reader.Close();
                    context.Response.Write(html);
                }
            }
            else
            {
                if (context.Request["type"] == null || string.IsNullOrEmpty(context.Request["type"]))
                    context.Response.Write("<script>alert(\"请求出错!\");window.location.href=\"login.ashx\"</script>");
                else
                {
                    if (context.Request["type"] == "slpatient")
                    {
                        #region 患者管理查询
                        string request = context.Request["slpatient"];
                        int page = int.Parse(context.Request["page"]);
                        int count = 0;
                        int start, end;
                        patient p = new patient();
                        List<patient> list = new List<patient>();
                        p.Patient_tel = request;
                        if ((count = pm.GetRecordCount(p)) > 0)
                        {
                            getpage(count, 10, page, out start, out  end);
                            list.AddRange(pm.GetListByPage(p, start, end));
                        }
                        if (count == 0)
                        {
                            p.Patient_tel = string.Empty;
                            p.Patient_idcard = request;
                            if ((count = pm.GetRecordCount(p)) > 0)
                            {
                                getpage(count, 10, page, out start, out  end);
                                list.AddRange(pm.GetListByPage(p, start, end));
                            }
                        }
                        if (count == 0)
                        {
                            p.Patient_idcard = string.Empty;
                            p.Patient_name = request;
                            if ((count = pm.GetRecordCount(p)) > 0)
                            {
                                getpage(count, 10, page, out start, out  end);
                                list.AddRange(pm.GetListByPage(p, start, end));
                            }
                        }
                        if (count == 0)
                        {
                            p.Patient_name = string.Empty;
                            p.Patient_add = request;
                            if ((count = pm.GetRecordCount(p)) > 0)
                            {
                                getpage(count, 10, page, out start, out  end);
                                list.AddRange(pm.GetListByPage(p, start, end));
                            }
                        }

                        if (count == 0)
                        {
                            p.Patient_add = string.Empty;
                            p.Patient_qr_code = request;
                            if ((count = pm.GetRecordCount(p)) > 0)
                            {
                                getpage(count, 10, page, out start, out  end);
                                list.AddRange(pm.GetListByPage(p, start, end));
                            }
                        }

                        Simple_Result2 sp = new Simple_Result2();
                        sp.result = "true";
                        sp.count = count / 10 + 1;
                        sp.data = create_patient_list(list);
                        list.Clear();
                        context.Response.Write(JsonConvert.SerializeObject(sp));
                        #endregion
                    }
                    else if (context.Request["type"] == "delpatient")
                    {
                        #region 患者管理删除
                        //删除
                        int id = -1;
                        if (context.Request["patient_id"] == null || !int.TryParse(context.Request["patient_id"], out id))
                        {
                            context.Response.Write("<script>window.opener=null;window.close();</script>");
                            return;
                        }
                        patient p = new patient() { Id = id };
                        List<task> tlist = tm.getModelList(new task() { Patient_id = p });
                        result r = new result();
                        foreach (var item in tlist)
                        {
                            if (tm.getModel(item))
                            {
                                if (item.Result_id != null)
                                    rm.deleteModel(item.Result_id);
                                tm.deleteModel(item);
                            }
                        }
                        tlist.Clear();
                        pm.deleteModel(p);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("true");
                        #endregion
                    }
                    else if (context.Request["type"] == "sltata")
                    {
                        #region 任务管理查询
                        string request = context.Request["sltask"];
                        bool isdiag = bool.Parse(context.Request["isdiag"]);
                        task t = new task();
                        List<task> list = new List<task>();
                        int page = int.Parse(context.Request["page"]);
                        int count = 0;
                        int start, end;
                        if (!string.IsNullOrEmpty(request))
                        {
                            t.Patient_id = new patient();
                            t.Patient_id.Patient_tel = request;
                            if ((count = tm.rgc_view(t, isdiag)) > 0)
                            {
                                getpage(count, 10, page, out start, out  end);
                                list.AddRange(tm.glbp_view(t, start, end, isdiag));
                            }
                            if (count == 0)
                            {
                                t.Patient_id.Patient_tel = string.Empty;
                                t.Patient_id.Patient_idcard = request;
                                if ((count = tm.rgc_view(t, isdiag)) > 0)
                                {
                                    getpage(count, 10, page, out start, out  end);
                                    list.AddRange(tm.glbp_view(t, start, end, isdiag));
                                }
                            }
                            if (count == 0)
                            {
                                t.Patient_id.Patient_idcard = string.Empty;
                                t.Patient_id.Patient_name = request;
                                if ((count = tm.rgc_view(t, isdiag)) > 0)
                                {
                                    getpage(count, 10, page, out start, out  end);
                                    list.AddRange(tm.glbp_view(t, start, end, isdiag));
                                }
                            }
                            if (count == 0)
                            {
                                t.Patient_id.Patient_name = string.Empty;
                                t.Patient_id.Patient_add = request;
                                if ((count = tm.rgc_view(t, isdiag)) > 0)
                                {
                                    getpage(count, 10, page, out start, out  end);
                                    list.AddRange(tm.glbp_view(t, start, end, isdiag));
                                }
                            }
                            if (count == 0)
                            {
                                t.Patient_id.Patient_add = string.Empty;
                                t.Patient_id.Patient_qr_code = request;
                                if ((count = tm.rgc_view(t, isdiag)) > 0)
                                {
                                    getpage(count, 10, page, out start, out  end);
                                    list.AddRange(tm.glbp_view(t, start, end, isdiag));
                                }
                            }
                        }
                        else
                        {
                            if ((count = tm.rgc_view(t, isdiag)) > 0)
                            {
                                getpage(count, 10, page, out start, out  end);
                                list.AddRange(tm.glbp_view(t, start, end, isdiag));
                            }
                        }


                        //patientList.Rows
                        Simple_Result2 sp = new Simple_Result2();
                        sp.result = "true";
                        sp.count = count / 10 + 1;
                        sp.data = create_task_list(list);
                        list.Clear();
                        context.Response.Write(JsonConvert.SerializeObject(sp));
                        #endregion
                    }
                    else if (context.Request["type"] == "deltask")
                    {
                        #region 任务管理删除
                        //删除
                        int id = -1;
                        if (context.Request["taskid"] == null || !int.TryParse(context.Request["taskid"], out id))
                        {
                            context.Response.Write("<script>window.opener=null;window.close();</script>");
                            return;
                        }
                        task t = new task() { Id = id };
                        if (tm.getModel(t) && t.Result_id != null)
                        {
                            rm.deleteModel(t.Result_id);
                        }
                        tm.deleteModel(t);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("true");
                        #endregion
                    }
                    else if (context.Request["type"] == "sldocta")
                    {
                        #region 医生管理查询
                        string request = context.Request["sldoc"];
                        Regex r = new Regex("\\d");
                        Match mc = r.Match(request);
                        doctor d = new doctor();
                        int page = int.Parse(context.Request["page"]);
                        int count = 0;
                        int start, end;
                        if (mc.Success)
                        {
                            d.Doctor_tel = request;
                        }
                        else
                            d.Doctor_name = request;
                        count = dm.GetRecordCount(d);
                        getpage(count, 10, page, out start, out  end);

                        List<doctor> list = dm.GetListByPage(d, start, end);

                        //patientList.Rows
                        Simple_Result2 sd = new Simple_Result2();
                        sd.result = "true";
                        sd.count = count / 10 + 1;
                        sd.data = create_doctor_list(list);
                        list.Clear();
                        context.Response.Write(JsonConvert.SerializeObject(sd));
                        #endregion
                    }
                    else if (context.Request["type"] == "docper")
                    {
                        #region 医生授权
                        int docid = int.Parse(context.Request["docid"]);
                        bool check = bool.Parse(context.Request["checked"]);
                        doctor d = new doctor();
                        d.Id = docid;
                        if (!dm.getModel(d))
                            context.Response.Write("false");
                        else
                        {
                            d.Doctor_permisson = check ? 1 : 0;
                            if (!dm.updateModel(d))
                                context.Response.Write("false");
                            else
                                context.Response.Write("true");
                        }
                        return;
                        #endregion
                    }
                    else if (context.Request["type"] == "deldoc")
                    {
                        #region 医生管理删除
                        //删除
                        int id = -1;
                        if (context.Request["doc_id"] == null || !int.TryParse(context.Request["doc_id"], out id))
                        {
                            context.Response.Write("<script>window.opener=null;window.close();</script>");
                            return;
                        }
                        doctor d = new doctor() { Id = id };

                        dm.deleteModel(d);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("true");
                        #endregion
                    }
                }
            }
        }

        void getpage(int allcount, int eachcount, int page, out int start, out int end)
        {
            start = eachcount * (page - 1) + 1;
            if (start > allcount)
                start = allcount - page;
            end = eachcount * page > allcount ? allcount : eachcount * page;
        }
        private string create_doctor_list(List<doctor> list)
        {
            StringBuilder sb = new StringBuilder();
            int i;
            sb.Append("<thead><tr><th>编号</th><th>医师名</th><th>性别</th><th>年龄</th><th>联系方式</th><th>级别</th><th>授权</th><th>操作</th></tr></thead><tbody>");
            for (i = 0; i < 10 && i < list.Count; i++)
            {
                sb.Append("<tr><td>" + list[i].Id.Value.ToString() +
                    "</td><td>" + list[i].Doctor_tname +
                    "</td><td>" + list[i].Doctor_gender +
                    "</td><td>" + (list[i].Doctor_dob == null ? "" : (DateTime.Now.Year - DateTime.Parse(list[i].Doctor_dob).Year).ToString()) +
                    "</td><td>" + list[i].Doctor_tel +
                    "</td><td>" + list[i].Doctor_level +
                    "</td><td> <input class='doccheck' onclick='docpermisson(this)' value='" + list[i].Id.Value.ToString() + "' type='checkbox' " + (list[i].Doctor_permisson.HasValue && list[i].Doctor_permisson.Value == 1 ? "checked='checked'" : "") + "/>" +
                    "</td><td><span class='revise revisecase' onclick=\"javascript:window.open('revisedoc.ashx?doc_id=" + list[i].Id.Value.ToString() +
                    "')\">修改</span> <span class='delete' onclick=\"delete_doc(" + list[i].Id.Value.ToString() + ")\">删除</span></td></tr>");
            }
            for (; i < 10; i++)
            {
                sb.Append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            }
            sb.Append("</tbody>");
            return sb.ToString();
        }
        private string create_task_list(List<task> list)
        {
            StringBuilder sb = new StringBuilder();
            int i;
            sb.Append("<thead><tr><th>编号</th><th>姓名</th><th>性别</th><th>年龄</th><th>登记时间</th><th>住址</th><th>状态</th><th>操作</th></tr></thead><tbody>");
            for (i = 0; i < 10 && i < list.Count; i++)
            {
                sb.Append("<tr><td onclick=\"detailcase(this, " + list[i].Patient_id.Id + ", " + list[i].Id + ")\">" + list[i].Patient_id.Id +
                    "</td><td onclick=\"detailcase(this, " + list[i].Patient_id.Id + ", " + list[i].Id + ")\">" + list[i].Patient_id.Patient_name +
                    "</td><td onclick=\"detailcase(this, " + list[i].Patient_id.Id + ", " + list[i].Id + ")\">" + list[i].Patient_id.Patient_gender +
                    "</td><td onclick=\"detailcase(this, " + list[i].Patient_id.Id + ", " + list[i].Id + ")\">" + (DateTime.Now.Year - DateTime.Parse(list[i].Patient_id.Patient_dob).Year).ToString() +
                    "</td><td onclick=\"detailcase(this, " + list[i].Patient_id.Id + ", " + list[i].Id + ")\">" + list[i].Date +
                    "</td><td onclick=\"detailcase(this, " + list[i].Patient_id.Id + ", " + list[i].Id + ")\">" + list[i].Patient_id.Patient_add +
                    "</td><td onclick=\"detailcase(this, " + list[i].Patient_id.Id + ", " + list[i].Id + ")\">" + (list[i].Finished_items == list[i].Task_items ? "已完成" : "未完成") +
                    "</td><td><span class='delete' onclick = \"delete_task(" + list[i].Id.Value.ToString() + ")\">删除</span></td></tr>");

            }
            for (; i < 10; i++)
            {
                sb.Append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            }
            sb.Append("</tbody>");
            return sb.ToString();
        }
        private string create_patient_list(List<patient> list)
        {
            StringBuilder sb = new StringBuilder();
            int i;
            sb.Append("<thead><tr><th>编号</th><th>姓名</th><th>性别</th><th>年龄</th><th>联系方式</th><th>住址</th><th>操作</th></tr></thead><tbody>");
            for (i = 0; i < 10 && i < list.Count; i++)
            {
                sb.Append("<tr><td>" + list[i].Id.Value.ToString() +
                    "</td><td>" + list[i].Patient_name +
                    "</td><td>" + list[i].Patient_gender +
                    "</td><td>" + (DateTime.Now.Year - DateTime.Parse(list[i].Patient_dob).Year).ToString() +
                    "</td><td>" + list[i].Patient_tel +
                    "</td><td>" + list[i].Patient_add +
                    "</td><td><span class='revise revisecase' onclick=\"javascript:window.open('revisecase.ashx?patient_id=" + list[i].Id.Value.ToString() +
                    "')\">修改</span> <span class='delete' onclick=\"delete_patient(" + list[i].Id.Value.ToString() + ")\">删除</span></td></tr>");
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