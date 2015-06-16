using System;
using System.Collections.Generic;
using System.Text;

namespace cn.com.farsight.IM.IMModel
{
    public class patient
    {
        private int? _id;
        /// <summary>
        /// 患者唯一标志
        /// </summary>
        public int? Id
        {
            get { return _id; }
            set
            {
                if (value > 0)
                    _id = value;
            }
        }
        private string patient_name;
        /// <summary>
        /// 患者真实姓名
        /// </summary>
        public string Patient_name
        {
            get { return patient_name; }
            set { patient_name = value; }
        }
        private string patient_dob;
        /// <summary>
        /// 患者生日
        /// </summary>
        public string Patient_dob
        {
            get { return patient_dob; }
            set
            {
                DateTime dt;
                if (DateTime.TryParse(value, out dt))
                    patient_dob = value;
            }
        }
        private string patient_gender;
        /// <summary>
        /// 患者性别
        /// </summary>
        public string Patient_gender
        {
            get { return patient_gender; }
            set
            {
                if (value.Equals("男") || value.Equals("女"))
                    patient_gender = value;
            }
        }
        private string patient_tel;
        /// <summary>
        /// 患者电话
        /// </summary>
        public string Patient_tel
        {
            get { return patient_tel; }
            set { patient_tel = value; }
        }
        private string patient_mail;
        /// <summary>
        /// 患者邮箱
        /// </summary>
        public string Patient_mail
        {
            get { return patient_mail; }
            set { patient_mail = value; }
        }
        private string patient_id_type;
        /// <summary>
        /// 证件类型
        /// </summary>
        public string Patient_id_type
        {
            get { return patient_id_type; }
            set { patient_id_type = value; }
        }
        private string patient_idcard;
        /// <summary>
        /// 证件号
        /// </summary>
        public string Patient_idcard
        {
            get { return patient_idcard; }
            set { patient_idcard = value; }
        }
        private string patient_add;
        /// <summary>
        /// 地址
        /// </summary>
        public string Patient_add
        {
            get { return patient_add; }
            set { patient_add = value; }
        }
        private string patient_marriage;
        /// <summary>
        /// 婚否
        /// </summary>
        public string Patient_marriage
        {
            get { return patient_marriage; }
            set { patient_marriage = value; }
        }
        private string patient_photo;
        /// <summary>
        /// 患者照片
        /// </summary>
        public string Patient_photo
        {
            get { return patient_photo; }
            set { patient_photo = value; }
        }
        private string patient_height;
        /// <summary>
        /// 患者身高
        /// </summary>
        public string Patient_height
        {
            get { return patient_height; }
            set { patient_height = value; }
        }
        private string patient_weight;
        /// <summary>
        /// 体重
        /// </summary>
        public string Patient_weight
        {
            get { return patient_weight; }
            set { patient_weight = value; }
        }
        private string patient_disease;
        /// <summary>
        /// 重大疾病史
        /// </summary>
        public string Patient_disease
        {
            get { return patient_disease; }
            set { patient_disease = value; }
        }
        private string patient_qr_code;
        /// <summary>
        /// 二维码数据
        /// </summary>
        public string Patient_qr_code
        {
            get { return patient_qr_code; }
            set { patient_qr_code = value; }
        }
        private string info;
        /// <summary>
        /// 其他备注信息
        /// </summary>
        public string Info
        {
            get { return info; }
            set { info = value; }
        }
        private string reg_date;
        /// <summary>
        /// 注册日期
        /// </summary>
        public string Reg_date
        {
            get { return reg_date; }
            set
            {
                DateTime dt;
                if (DateTime.TryParse(value, out dt))
                    reg_date = value;
            }
        }
    }
}
