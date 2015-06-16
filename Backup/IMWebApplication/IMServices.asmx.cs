using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using cn.com.farsight.IM.IMDbVisit;
using cn.com.farsight.IM.IMModel;
using Newtonsoft.Json;
using cn.com.farsight.IM.ToolsHelper;
namespace cn.com.farsight.IM.IMWebApplication
{
    /// <summary>
    /// IMWebServices 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class IMServices : System.Web.Services.WebService
    {
        ResultManager rm = new ResultManager();
        TaskManager tm = new TaskManager();
        PatientManager pm = new PatientManager();
        DoctorManager dm = new DoctorManager();
        /// <summary>
        /// 小测试
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 检查用户(根据ID卡号)
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        [WebMethod]
        public string checkCardUser(string card)
        {

            LoginResult lr = new LoginResult();
           
            doctor d = new doctor();
            d.Card_data = card;
            if (string.IsNullOrEmpty(card) || dm.getModel(d) == false)
            {//未找到用户
                lr.code = -1;
                lr.errorStr = "用户名或密码错误！";
                lr.card_data = string.Empty;
                lr.doctor_name = string.Empty;
                lr.doctor_tname = string.Empty;
                lr.doctor_add = string.Empty;
                lr.doctor_level = string.Empty;
                lr.doctor_gender = string.Empty;
                lr.doctor_dob = string.Empty;
            }
            else
            {
                lr.code = 0;
                lr.errorStr = string.Empty;
                lr.doctor_id = d.Id.Value;
                lr.doctor_name = d.Doctor_name;
                lr.doctor_tname = d.Doctor_tname;
                lr.card_data = d.Card_data;
                lr.doctor_tel = d.Doctor_tel;
                lr.doctor_add = d.Doctor_add;
                lr.doctor_level = d.Doctor_level;
                lr.doctor_gender = d.Doctor_gender;
                lr.doctor_dob = d.Doctor_dob;
            }
            return JsonConvert.SerializeObject(lr);
        }


        /// <summary>
        /// 检查用户(根据用户名和密码)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [WebMethod]
        public string checkPasswdUser(string name, string pwd)
        {
            string result = string.Empty;
            LoginResult lr = new LoginResult();
            doctor d = new doctor();
            d.Doctor_name = name;
            d.Doctor_pwd = pwd;
            if (string.IsNullOrEmpty(name) || dm.getModel(d) == false || d.Doctor_pwd.CompareTo(pwd) != 0)
            {//未找到用户或者密码不匹配
                lr.code = -1;
                lr.errorStr = "用户名或密码错误！";
                lr.card_data = string.Empty;
                lr.doctor_name = string.Empty;
                lr.doctor_tname = string.Empty;
                lr.doctor_add = string.Empty;
                lr.doctor_level = string.Empty;
                lr.doctor_gender = string.Empty;
                lr.doctor_dob = string.Empty;
            }
            else
            {
                lr.code = 0;
                lr.errorStr = string.Empty;
                lr.doctor_name = d.Doctor_name;
                lr.doctor_tname = d.Doctor_tname;
                lr.doctor_id = d.Id.Value;
                lr.card_data = d.Card_data;
                lr.doctor_tel = d.Doctor_tel;
                lr.doctor_add = d.Doctor_add;
                lr.doctor_level = d.Doctor_level;
                lr.doctor_gender = d.Doctor_gender;
                lr.doctor_dob = d.Doctor_dob;
            }
            try
            {
                result = JsonConvert.SerializeObject(lr);
            }
            catch (Exception)
            {
                result = "{code:-1,errorStr:\"Json String build error!\"}";
            }

            return result;
        }


        /// <summary>
        /// 获得任务列表
        /// </summary>
        /// <param name="date">日期(yyyy-MM-dd)</param>
        /// <param name="doctor_id">医生编号</param>
        /// <returns>任务列表(Json字符串)</returns>
        [WebMethod]
        public string getTaskList(string date, int doctor_id)
        {
            string result = string.Empty;
            task r = new task() { Date = date, Doctor_id = new doctor() { Id = doctor_id } };
            List<task> list = tm.getModelList(r);
            Task_List tl = new Task_List();
            tl.code = list.Count > 0 ? 0 : -1;
            tl.count = list.Count;
            tl.errorStr = list.Count > 0 ? "" : "not found!";
            tl.tasklist = new List<Patient_Task>();
            Patient_Task rt;
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (pm.getModel(item.Patient_id))
                    {
                        rt = new Patient_Task();
                        if (item.Patient_id != null)
                        {
                            rt.patient_id = item.Patient_id.Id.Value;
                            rt.patient_name = item.Patient_id.Patient_name;
                            rt.patient_dob = item.Patient_id.Patient_dob;
                            rt.patient_gender = item.Patient_id.Patient_gender;
                            rt.patient_tel = item.Patient_id.Patient_tel;
                            rt.patient_id_type = item.Patient_id.Patient_id_type;
                            rt.patient_idcard = item.Patient_id.Patient_idcard;
                            rt.patient_add = item.Patient_id.Patient_add;
                            rt.patient_marriage = item.Patient_id.Patient_marriage;
                            rt.patient_photo = item.Patient_id.Patient_photo;
                            rt.patient_height = item.Patient_id.Patient_height;
                            rt.patient_weight = item.Patient_id.Patient_weight;
                            rt.patient_disease = item.Patient_id.Patient_disease;
                            rt.patient_qr_code = item.Patient_id.Patient_qr_code;
                            rt.reg_date = item.Patient_id.Reg_date;
                        }
                        if (item.Result_id != null)
                        {
                            rt.info = item.Result_id.Info;
                            rt.result_id = item.Result_id != null ? item.Result_id.Id.Value : -1;
                        }

                        rt.task_id = item.Id.Value;
                        rt.task_items = item.Task_items.HasValue ? item.Task_items.Value : -1;
                        rt.finished_items = item.Finished_items.HasValue ? item.Finished_items.Value : -1;
                        tl.tasklist.Add(rt);
                    }
                }

            }
            try
            {
                result = JsonConvert.SerializeObject(tl);
            }
            catch (Exception)
            {
                result = "{code:-1,errorStr:\"Json String build error!\"}";
            }
            return result;
        }


        /// <summary>
        /// 上传结果
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string uploadResult(string data)
        {
            Update_Result ur;
            try
            {
                ur = JsonConvert.DeserializeObject<Update_Result>(data);
            }
            catch (Exception)
            {
                return "{\"code\":-1,\"errorStr\":\"Json String build error!\"}";
            }
            patient p = new patient();
            result r = new result();
            task t = new task();
            foreach (var item in ur.resultList)
            {
                p.Id = item.patient_id;
                if (!pm.getModel(p))
                    continue;
                //未用
                //p.Patient_photo = Encoding.Default.GetBytes(item.patient_photo);
                //pm.updateModel(p);
                t.Id = item.task_id;
                t.Patient_id = p;
                if (!tm.getModel(t))
                    continue;
                //if (ecg)
                //    temp |= 1 << 0;
                //if (bloodpress)
                //    temp |= 1 << 1;
                //if (glucose)
                //    temp |= 1 << 2;
                //if (temperature)
                //    temp |= 1 << 3;
                t.Finished_items |= item.finished_items;
                if (t.Result_id == null)
                {
                    t.Result_id = new result();
                }
                else
                    rm.getModel(t.Result_id);

                if ((item.finished_items & (1 << 0)) != 0)
                {
                    t.Result_id.Ecg_data = item.ecg_data;
                    t.Result_id.Ecg_time = item.ecg_time;
                }
                if ((item.finished_items & (1 << 1)) != 0)
                {
                    t.Result_id.Hp_data = item.hp_data;
                    t.Result_id.Lp_data = item.lp_data;
                    t.Result_id.Bloodpress_time = item.bloodpress_time;
                }
                if ((item.finished_items & (1 << 2)) != 0)
                {
                    t.Result_id.Glucose_data = item.glucose_data.ToString();
                    t.Result_id.Glucose_time = item.glucose_time;
                }
                if ((item.finished_items & (1 << 3)) != 0)
                {
                    t.Result_id.Temperature = item.temperature;
                    t.Result_id.Temperature_time = item.temperature_time;
                }
                if (t.Result_id.Id == null)
                    rm.addModel(t.Result_id);
                else
                    rm.updateModel(t.Result_id);

                tm.updateModel(t);
            }

            return "{code:0,errorStr:\"\"}";
        }


        /// <summary>
        /// 查询患者体检结果
        /// </summary>
        /// <param name="patient_id">患者编号</param>
        /// <returns></returns>
        [WebMethod]
        public string queryPaientResult(int patient_id)
        {
            List<task> list = tm.getModelList(new task() { Patient_id = new patient() { Id = patient_id } });
            Result_List tl = new Result_List();
            tl.code = list.Count > 0 ? 0 : -1;
            tl.count = 0;
            tl.errorStr = list.Count > 0 ? "" : "not found!";
            tl.list = new List<Task_Result>();
            Task_Result rl;
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item.Result_id == null || !rm.getModel(item.Result_id))
                        continue;
                    tl.count++;
                    rl = new Task_Result();
                    rl.result_id = item.Result_id.Id.Value;
                    rl.ecg_data = item.Result_id.Ecg_data;
                    rl.ecg_time = item.Result_id.Ecg_time.HasValue ? item.Result_id.Ecg_time.Value : -1;
                    rl.ecg_info = item.Result_id.Ecg_info;
                    rl.hp_data = item.Result_id.Hp_data;
                    rl.lp_data = item.Result_id.Lp_data;
                    rl.bloodpress_time = item.Result_id.Bloodpress_time.HasValue ? item.Result_id.Bloodpress_time.Value : -1;
                    rl.bloodpress_info = item.Result_id.Bloodpress_info;
                    rl.glucose_data = item.Result_id.Glucose_data;
                    rl.glucose_time = item.Result_id.Glucose_time.HasValue ? item.Result_id.Glucose_time.Value : -1;
                    rl.glucose_info = item.Result_id.Glucose_info;
                    rl.temperature = item.Result_id.Temperature;
                    rl.temperature_time = item.Result_id.Temperature_time.HasValue ? item.Result_id.Temperature_time.Value : -1;
                    rl.temperature_info = item.Result_id.Temperature_info;
                    rl.date = item.Date;
                    rl.info = item.Result_id.Info;

                    tl.list.Add(rl);
                }

            }

            return JsonConvert.SerializeObject(tl);
        }


        /// <summary>
        /// 测试上传心电图
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string test_up_ecg(string data)
        {
            byte[] bytes = Base64.decodeBase64(data);
            return Base64.encodeBase64String(bytes);
        }
    }
}
