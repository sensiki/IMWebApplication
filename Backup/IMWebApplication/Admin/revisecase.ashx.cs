using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using cn.com.farsight.IM.IMModel;
using System.Web.SessionState;
using cn.com.farsight.IM.IMDbVisit;
using System.IO;

namespace cn.com.farsight.IM.IMWebApplication.Admin
{
    /// <summary>
    /// revisecase 的摘要说明
    /// </summary>
    public class revisecase : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            PatientManager pm = new PatientManager();
            ResultManager rm = new ResultManager();
            TaskManager tm = new TaskManager();
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
                    StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/html/revisecase.html");
                    string html = reader.ReadToEnd();
                    reader.Close();
                    context.Response.Write(html);
                }
            }
            else
            {

                int id = int.Parse(context.Request["patient_id"]);
                patient p = new patient() { Id = id };
                if (!pm.getModel(p))
                {
                    context.Response.Write("false");
                    return;
                }
                if (context.Request["changecase"] == null)
                {
                    Update_Case c = new Update_Case()
                    {
                        name = p.Patient_name,
                        sex = p.Patient_gender,
                        bir = p.Patient_dob,
                        tel = p.Patient_tel,
                        address = p.Patient_add,
                        marriage = p.Patient_marriage,
                        credentials = p.Patient_idcard,
                        credentialstype = p.Patient_id_type,
                        disease = p.Patient_disease,
                        height = p.Patient_height,
                        weight = p.Patient_weight,
                        mail = p.Patient_mail,
                        remask = p.Info,
                        cardNumber = p.Patient_qr_code
                    };
                    context.Response.Write(JsonConvert.SerializeObject(c));
                    return;
                }
                else
                {
                    Update_Case c = JsonConvert.DeserializeObject<Update_Case>(context.Request["changecase"]);
                    DateTime dt;
                    if (string.IsNullOrEmpty(c.name) || string.IsNullOrEmpty(c.sex) || string.IsNullOrEmpty(c.bir) || !DateTime.TryParse(c.bir, out dt))
                    {
                        context.Response.Write("{\"result\":-1,\"data\":\"\"}");
                        return;
                    }

                    p.Patient_name = c.name;
                    p.Patient_gender = c.sex;
                    p.Patient_dob = c.bir;
                    p.Patient_tel = c.tel;
                    p.Patient_add = c.address;
                    p.Patient_marriage = c.marriage;
                    p.Patient_mail = c.mail;
                    p.Patient_disease = c.disease;
                    p.Patient_weight = c.weight;
                    p.Patient_height = c.height;
                    p.Info = c.remask;
                    p.Patient_id_type = c.credentialstype;
                    p.Patient_idcard = c.credentials;
                    p.Patient_qr_code = c.cardNumber;
                    pm.updateModel(p);

                    Simple_Result2 sp = new Simple_Result2();
                    sp.result = "true";
                    /*
                    if (context.Session["LoginUserName"].ToString() == "admin")
                        sp.data = "admin.ashx";
                    else
                        sp.data = "main.ashx";
                     */
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