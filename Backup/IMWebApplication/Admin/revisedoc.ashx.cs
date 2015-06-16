using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using cn.com.farsight.IM.IMDbVisit;
using cn.com.farsight.IM.IMModel;
using System.IO;
using Newtonsoft.Json;

namespace cn.com.farsight.IM.IMWebApplication.Admin
{
    /// <summary>
    /// revisedoc 的摘要说明
    /// </summary>
    public class revisedoc : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            DoctorManager dm = new DoctorManager();
            if (context.Request.RequestType == "GET")
            {
                //context.Response.ContentType = "text/plain";
                context.Response.ContentType = "text/html";
                if (context.Session["LoginUserName"] == null || string.IsNullOrEmpty(context.Session["LoginUserName"].ToString()))
                {
                    context.Response.Write("<script>alert(\"权限不足,请重新登录!\");window.location.href=\"login.ashx\"</script>");
                    return;
                }
                else
                {

                    StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/html/revisedoc.html");
                    string html = reader.ReadToEnd();
                    reader.Close();
                    context.Response.Write(html);
                }
            }
            else
            {

                int id = int.Parse(context.Request["doc_id"]);
                doctor d = new doctor() { Id = id };
                dm.getModel(d);
                if (context.Request["changedoc"] == null)
                {
                    Update_Doctor c = new Update_Doctor()
                    {
                        name = d.Doctor_name,
                        tname = d.Doctor_tname,
                        sex = d.Doctor_gender,
                        bir = d.Doctor_dob,
                        tel = d.Doctor_tel,
                        add = d.Doctor_add,
                        permisson = d.Doctor_permisson.HasValue && d.Doctor_permisson.Value == 1 ? true : false

                    };
                    context.Response.Write(JsonConvert.SerializeObject(c));
                    return;
                }
                else
                {
                    Simple_Result2 sp = new Simple_Result2();
                    Update_Doctor c = JsonConvert.DeserializeObject<Update_Doctor>(context.Request["changedoc"]);
                    DateTime dt;
                    if (string.IsNullOrEmpty(c.name) || string.IsNullOrEmpty(c.tname) || string.IsNullOrEmpty(c.sex) || string.IsNullOrEmpty(c.bir) || !DateTime.TryParse(c.bir, out dt))
                    {
                        sp.result = "false";
                        sp.data = "用户名、姓名、性别或者出生日期为空或出生日期(1990-01-01)格式错误！";
                        context.Response.Write(JsonConvert.SerializeObject(sp));
                        return;
                    }
                    if (!string.IsNullOrEmpty(c.oldpasswd))
                    {
                        if (!d.Doctor_pwd.Equals(c.oldpasswd))
                        {
                            sp.result = "false";
                            sp.data = "当前密码输入错误!";
                            context.Response.Write(JsonConvert.SerializeObject(sp));
                            return;
                        }
                        else if (c.passwd1.Length < 8)
                        {
                            sp.result = "false";
                            sp.data = "新密码长度不能小于8!";
                            context.Response.Write(JsonConvert.SerializeObject(sp));
                            return;
                        }
                        else if (!c.passwd1.Equals(c.passwd2))
                        {
                            sp.result = "false";
                            sp.data = "两次密码输入不一致";
                            context.Response.Write(JsonConvert.SerializeObject(sp));
                            return;
                        }
                        d.Doctor_pwd = c.passwd1;
                    }
                    d.Doctor_tname = c.tname;
                    d.Doctor_gender = c.sex;
                    d.Doctor_dob = c.bir;
                    d.Doctor_tel = c.tel;
                    d.Doctor_add = c.add;
                    d.Doctor_permisson = c.permisson ? 1 : 0;
                    dm.updateModel(d);
                    sp.result = "true";
                    if (context.Session["LoginUserName"].ToString() == "admin")
                        sp.data = "admin.ashx";
                    else
                        sp.data = "main.ashx";
                    context.Response.Write(JsonConvert.SerializeObject(sp));
                    return;
                }
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