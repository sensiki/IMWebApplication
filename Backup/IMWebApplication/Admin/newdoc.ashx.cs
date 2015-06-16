using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.IO;
using Newtonsoft.Json;
using cn.com.farsight.IM.IMDbVisit;
using cn.com.farsight.IM.IMModel;

namespace cn.com.farsight.IM.IMWebApplication.Admin
{
    /// <summary>
    /// newdoc 的摘要说明
    /// </summary>
    public class newdoc : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            if (context.Request.RequestType == "GET")
            {
                context.Response.ContentType = "text/html";

                if (context.Session["LoginUserName"] == null || string.IsNullOrEmpty(context.Session["LoginUserName"].ToString()))
                    context.Response.Write("<script>alert(\"权限不足,请重新登录!\");window.location.href=\"login.ashx\"</script>");
                else
                {
                    StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/html/newdoc.html");
                    string html = reader.ReadToEnd();
                    reader.Close();
                    context.Response.Write(html);
                }
            }
            else
            {
                if (context.Request["newdoc"] == null || string.IsNullOrEmpty(context.Request["newdoc"]))
                    return;
                string request = context.Request["newdoc"];
                Update_Doctor nc = JsonConvert.DeserializeObject<Update_Doctor>(request);
                DateTime dt;
                if (string.IsNullOrEmpty(nc.name) || string.IsNullOrEmpty(nc.tname) || string.IsNullOrEmpty(nc.sex) || string.IsNullOrEmpty(nc.bir) || !DateTime.TryParse(nc.bir, out dt))
                {
                    context.Response.Write("{\"result\":-1,\"data\":\"用户名、姓名、性别或者出生日期为空或出生日期(1990-01-01)格式错误！\"}");
                    return;
                }
                DoctorManager dm = new DoctorManager();
                doctor d = new doctor()
                {
                    Doctor_name = nc.name,
                };
                if (dm.getModel(d))
                {
                    context.Response.Write("{\"result\":-2,\"data\":\"用户名已存在\"}");
                    return;
                }
                if (nc.passwd1.Length < 8)
                {
                    context.Response.Write("{\"result\":-3,\"data\":\"密码长度不能小于8\"}");
                    return;
                }
                else if (!nc.passwd1.Equals(nc.passwd2))
                {
                    context.Response.Write("{\"result\":-4,\"data\":\"两次密码输入不一致\"}");
                    return;
                }

                d.Doctor_tname = nc.tname;
                d.Doctor_gender = nc.sex;
                d.Doctor_dob = nc.bir;
                d.Doctor_tel = nc.tel;
                d.Doctor_add = nc.add;
                d.Doctor_pwd = nc.passwd1;
                d.Doctor_permisson = nc.permisson ? 1 : 0;


                dm.addModel(d);
                context.Response.Write("{\"result\":0,\"data\":\"注册成功\"}");
            }
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