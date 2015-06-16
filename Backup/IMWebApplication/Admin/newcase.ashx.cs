using System;
using System.Collections.Generic;
using System.Web;
using cn.com.farsight.IM.IMDbVisit;
using cn.com.farsight.IM.IMModel;
using Newtonsoft.Json;
using System.Web.SessionState;
using System.IO;

namespace cn.com.farsight.IM.IMWebApplication.Admin
{
    /// <summary>
    /// newcase 的摘要说明
    /// </summary>
    public class newcase : IHttpHandler, IRequiresSessionState
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
                    StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/html/newcase.html");
                    string html = reader.ReadToEnd();
                    reader.Close();
                    context.Response.Write(html);
                }
            }
            else
            {
                if (context.Request["newcase"] == null || string.IsNullOrEmpty(context.Request["newcase"]))
                    return;
                string request = context.Request["newcase"];
                Update_Case nc = JsonConvert.DeserializeObject<Update_Case>(request);
                DateTime dt;
                if (string.IsNullOrEmpty(nc.name) || string.IsNullOrEmpty(nc.sex) || string.IsNullOrEmpty(nc.bir) || !DateTime.TryParse(nc.bir, out dt))
                {
                    context.Response.Write("{\"result\":-1,\"data\":\"\"}");
                    return;
                }
                patient p = new patient()
                {
                    Patient_name = nc.name,
                    Patient_gender = nc.sex,
                    Patient_dob = nc.bir,
                    Patient_tel = nc.tel,
                    Patient_add = nc.address,
                    Patient_disease = nc.disease,
                    Patient_height = nc.height,
                    Patient_id_type = nc.credentialstype,
                    Patient_idcard = nc.credentials,
                    Patient_mail = nc.mail,
                    Patient_marriage = nc.marriage,
                    Patient_weight = nc.weight,
                    Info = nc.remask,
                    Patient_qr_code = nc.cardNumber,
                    Reg_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
                };
                PatientManager pm = new PatientManager();
                pm.addModel(p);
                string result = nc.createtask ? "2,\"data\":" + p.Id : "1,\"data\":\"\"";
                context.Response.Write("{\"result\":" + result + "}");
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