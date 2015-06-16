using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using cn.com.farsight.IM.IMModel;
using cn.com.farsight.IM.IMDbVisit;
using System.IO;

namespace cn.com.farsight.IM.IMWebApplication.Admin
{
    /// <summary>
    /// login 的摘要说明
    /// </summary>
    public class login : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {

            bool isLogin = !string.IsNullOrEmpty(context.Request["login"]);
            if (isLogin)
            {
                context.Response.ContentType = "text/plain";
                Simple_Result lresult = new Simple_Result();
                string name = context.Request["name"];
                string pwd = context.Request["pwd"];
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
                {
                    lresult.result = "false";
                    lresult.data = "用户名或密码不能为空(>_<)";
                    context.Response.Write(JsonConvert.SerializeObject(lresult));
                    return;
                }
                doctor d = new doctor() { Doctor_name = name, Doctor_pwd = pwd };
                DoctorManager dm = new DoctorManager();
                if (dm.getModel(d))
                {
                    if (d.Doctor_pwd.CompareTo(pwd) != 0)
                    {
                        lresult.result = "false";
                        lresult.data = "用户名不存在或密码错误!(>_<)";
                    }
                    else if (d.Doctor_permisson.HasValue && d.Doctor_permisson.Value == 0)
                    {
                        lresult.result = "false";
                        lresult.data = "当前用户没有权限访问，请联系管理员！";
                    }
                    else
                    {
                        lresult.result = "true";
                        context.Session["LoginUserName"] = name;
                        if (d.Doctor_name == "admin")
                            lresult.data = "admin.ashx";
                        else
                            lresult.data = "main.ashx";
                    }
                }
                else
                {
                    lresult.result = "false";
                    lresult.data = "用户名不存在或密码错误!(>_<)";
                }
                context.Response.Write(JsonConvert.SerializeObject(lresult));
            }
            else
            {
                context.Response.ContentType = "text/html";
                StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/html/login.html");
                string html = reader.ReadToEnd();
                reader.Close();
                context.Response.Write(html);
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