using System;
using System.Collections.Generic;
using System.Web;

namespace cn.com.farsight.IM.IMWebApplication
{
    public class WebResult
    {
    }
    /*
     * 以下类只用于网页与后台的数据传输载体
     */
    /// <summary>
    /// 登陆结果
    /// </summary>
    public class LoginResult
    {
        private int _code;

        public int code
        {
            get { return _code; }
            set { _code = value; }
        }
        private string _errorStr;

        public string errorStr
        {
            get { return _errorStr; }
            set { _errorStr = value; }
        }
        private int _doctor_id;

        public int doctor_id
        {
            get { return _doctor_id; }
            set { _doctor_id = value; }
        }
        private string _doctor_name;

        public string doctor_name
        {
            get { return _doctor_name; }
            set { _doctor_name = value; }
        }
        private string _doctor_tname;

        public string doctor_tname
        {
            get { return _doctor_tname; }
            set { _doctor_tname = value; }
        }
        private string _card_data;

        public string card_data
        {
            get { return _card_data; }
            set { _card_data = value; }
        }
        private string _doctor_tel;

        public string doctor_tel
        {
            get { return _doctor_tel; }
            set { _doctor_tel = value; }
        }
        private string _doctor_add;

        public string doctor_add
        {
            get { return _doctor_add; }
            set { _doctor_add = value; }
        }
        private string _doctor_level;

        public string doctor_level
        {
            get { return _doctor_level; }
            set { _doctor_level = value; }
        }
        private string _doctor_gender;

        public string doctor_gender
        {
            get { return _doctor_gender; }
            set { _doctor_gender = value; }
        }
        private string _doctor_dob;

        public string doctor_dob
        {
            get { return _doctor_dob; }
            set { _doctor_dob = value; }
        }
    }
    /// <summary>
    /// 简单json结果
    /// </summary>
    public class Simple_Result
    {
        public string result { get; set; }
        public string data { get; set; }
    }
    public class Simple_Result2
    {
        public string result { get; set; }
        public int count { get; set; }
        public string data { get; set; }
    }
    /// <summary>
    /// 结果列表
    /// </summary>
    public class Result_List
    {
        private int _code;
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }
        private int _count;
        public int count
        {
            get { return _count; }
            set { _count = value; }
        }
        private string _errorStr;

        public string errorStr
        {
            get { return _errorStr; }
            set { _errorStr = value; }
        }
        private List<Task_Result> _list;

        public List<Task_Result> list
        {
            get { return _list; }
            set { _list = value; }
        }
    }
    /// <summary>
    /// 任务结果
    /// </summary>
    public class Task_Result
    {
        private int _id;

        public int result_id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _ecg_data;

        public string ecg_data
        {
            get { return _ecg_data; }
            set { _ecg_data = value; }
        }
        private long _ecg_time;

        public long ecg_time
        {
            get { return _ecg_time; }
            set { _ecg_time = value; }
        }
        private string _ecg_info;

        public string ecg_info
        {
            get { return _ecg_info; }
            set { _ecg_info = value; }
        }
        private string _hp_data;

        public string hp_data
        {
            get { return _hp_data; }
            set { _hp_data = value; }
        }
        private string _lp_data;

        public string lp_data
        {
            get { return _lp_data; }
            set { _lp_data = value; }
        }
        private long _bloodpress_time;

        public long bloodpress_time
        {
            get { return _bloodpress_time; }
            set { _bloodpress_time = value; }
        }
        private string _bloodpress_info;

        public string bloodpress_info
        {
            get { return _bloodpress_info; }
            set { _bloodpress_info = value; }
        }
        private string _glucose_data;

        public string glucose_data
        {
            get { return _glucose_data; }
            set { _glucose_data = value; }
        }
        private long _glucose_time;
        public long glucose_time
        {
            get { return _glucose_time; }
            set { _glucose_time = value; }
        }
        private string _glucose_info;

        public string glucose_info
        {
            get { return _glucose_info; }
            set { _glucose_info = value; }
        }
        private string _temperature;

        public string temperature
        {
            get { return _temperature; }
            set { _temperature = value; }
        }
        private long _temperature_time;

        public long temperature_time
        {
            get { return _temperature_time; }
            set { _temperature_time = value; }
        }
        private string _temperature_info;

        public string temperature_info
        {
            get { return _temperature_info; }
            set { _temperature_info = value; }
        }
        private string _date;

        public string date
        {
            get { return _date; }
            set { _date = value; }
        }
        private string _info;

        public string info
        {
            get { return _info; }
            set { _info = value; }
        }
    }

    public class Patient_Task
    {
        private int _patient_id;

        public int patient_id
        {
            get { return _patient_id; }
            set { _patient_id = value; }
        }
        private string _patient_name;

        public string patient_name
        {
            get { return _patient_name; }
            set { _patient_name = value; }
        }
        private string _patient_dob;

        public string patient_dob
        {
            get { return _patient_dob; }
            set { _patient_dob = value; }
        }
        private string _patient_gender;

        public string patient_gender
        {
            get { return _patient_gender; }
            set { _patient_gender = value; }
        }
        private string _patient_tel;

        public string patient_tel
        {
            get { return _patient_tel; }
            set { _patient_tel = value; }
        }
        private string _patient_mail;

        public string patient_mail
        {
            get { return _patient_mail; }
            set { _patient_mail = value; }
        }
        private string _patient_id_type;

        public string patient_id_type
        {
            get { return _patient_id_type; }
            set { _patient_id_type = value; }
        }
        private string _patient_idcard;

        public string patient_idcard
        {
            get { return _patient_idcard; }
            set { _patient_idcard = value; }
        }
        private string _patient_add;

        public string patient_add
        {
            get { return _patient_add; }
            set { _patient_add = value; }
        }
        private string _patient_marriage;

        public string patient_marriage
        {
            get { return _patient_marriage; }
            set { _patient_marriage = value; }
        }
        private string _patient_photo;

        public string patient_photo
        {
            get { return _patient_photo; }
            set { _patient_photo = value; }
        }
        private string _patient_height;

        public string patient_height
        {
            get { return _patient_height; }
            set { _patient_height = value; }
        }
        private string _patient_weight;

        public string patient_weight
        {
            get { return _patient_weight; }
            set { _patient_weight = value; }
        }
        private string _patient_disease;

        public string patient_disease
        {
            get { return _patient_disease; }
            set { _patient_disease = value; }
        }
        private string _patient_qr_code;

        public string patient_qr_code
        {
            get { return _patient_qr_code; }
            set { _patient_qr_code = value; }
        }
        private string _info;

        public string info
        {
            get { return _info; }
            set { _info = value; }
        }
        private string _reg_date;

        public string reg_date
        {
            get { return _reg_date; }
            set { _reg_date = value; }
        }
        private int _task_id;

        public int task_id
        {
            get { return _task_id; }
            set { _task_id = value; }
        }
        private int _task_items;

        public int task_items
        {
            get { return _task_items; }
            set { _task_items = value; }
        }
        private int _finished_items;
        public int finished_items
        {
            get { return _finished_items; }
            set { _finished_items = value; }
        }
        private int _result_id;

        public int result_id
        {
            get { return _result_id; }
            set { _result_id = value; }
        }
    }
    public class Task_List
    {
        private int _code;

        public int code
        {
            get { return _code; }
            set { _code = value; }
        }
        private string _errorStr;

        public string errorStr
        {
            get { return _errorStr; }
            set { _errorStr = value; }
        }
        private int _count;

        public int count
        {
            get { return _count; }
            set { _count = value; }
        }
        private List<Patient_Task> _task;

        public List<Patient_Task> tasklist
        {
            get { return _task; }
            set { _task = value; }
        }
    }
    public class Exam_Result
    {
        private string _lp_data;

        public string lp_data
        {
            get { return _lp_data; }
            set { _lp_data = value; }
        }
        private long? _glucose_time;

        public long? glucose_time
        {
            get { return _glucose_time; }
            set { _glucose_time = value; }
        }
        private long? _bloodpress_time;

        public long? bloodpress_time
        {
            get { return _bloodpress_time; }
            set { _bloodpress_time = value; }
        }
        private string _patient_photo;

        public string patient_photo
        {
            get { return _patient_photo; }
            set { _patient_photo = value; }
        }
        private int _finished_items;

        public int finished_items
        {
            get { return _finished_items; }
            set { _finished_items = value; }
        }
        private int _task_id;

        public int task_id
        {
            get { return _task_id; }
            set { _task_id = value; }
        }
        private string _temperature;

        public string temperature
        {
            get { return _temperature; }
            set { _temperature = value; }
        }
        private long? _ecg_time;

        public long? ecg_time
        {
            get { return _ecg_time; }
            set { _ecg_time = value; }
        }
        private int _result_id;

        public int result_id
        {
            get { return _result_id; }
            set { _result_id = value; }
        }
        private long? _temperature_time;

        public long? temperature_time
        {
            get { return _temperature_time; }
            set { _temperature_time = value; }
        }
        private string _hp_data;

        public string hp_data
        {
            get { return _hp_data; }
            set { _hp_data = value; }
        }
        private int _glucose_data;

        public int glucose_data
        {
            get { return _glucose_data; }
            set { _glucose_data = value; }
        }
        private int _patient_id;

        public int patient_id
        {
            get { return _patient_id; }
            set { _patient_id = value; }
        }
        private string _ecg_data;

        public string ecg_data
        {
            get { return _ecg_data; }
            set { _ecg_data = value; }
        }
    }
    public class Update_Result
    {
        private int _count;

        public int count
        {
            get { return _count; }
            set { _count = value; }
        }
        private List<Exam_Result> _resultList;

        public List<Exam_Result> resultList
        {
            get { return _resultList; }
            set { _resultList = value; }
        }
    }
   
    public class Patient_Info
    {
        public int task_item { get; set; }
        public string patient_name { get; set; }
        public string patient_gender { get; set; }
        public string patient_tel { get; set; }
        public string patient_dob { get; set; }
        public string patient_add { get; set; }
        public string doctorName { get; set; }
        public string ecg_data { get; set; }
        public string ecg_info { get; set; }
        public string ecg_date { get; set; }
        public string temperature { get; set; }
        public string temperature_date { get; set; }
        public string temperature_info { get; set; }
        public string temperature_history { get; set; }
        public string hp_data { get; set; }
        public string lp_data { get; set; }
        public string bloodpress_time { get; set; }
        public string bloodpress_info { get; set; }
        public string bloodpress_history { get; set; }
        public string info { get; set; }
        public bool sendtopatient { get; set; }
    }
    public class ecg_historyResult
    {
        public int task_item { get; set; }
        public string patient_name { get; set; }
        public string patient_gender { get; set; }
        public string patient_tel { get; set; }
        public string patient_dob { get; set; }
        public string patient_add { get; set; }
        public string ecg_history { get; set; }
    }
    public class Update_Case
    {
        public string name { get; set; }
        public string sex { get; set; }
        public string bir { get; set; }
        public string tel { get; set; }
        public string marriage { get; set; }
        public string address { get; set; }
        public string mail { get; set; }
        public string credentialstype { get; set; }
        public string credentials { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public bool createtask { get; set; }
        public string disease { get; set; }
        public string remask { get; set; }
        public string cardNumber { get; set; }
    }
    public class Update_Doctor
    {
        public string name { get; set; }
        public string tname { get; set; }
        public string sex { get; set; }
        public string tel { get; set; }
        public string bir { get; set; }
        public string add { get; set; }
        public bool permisson { get; set; }
        public string oldpasswd { get; set; }
        public string passwd1 { get; set; }
        public string passwd2 { get; set; }
    }
}