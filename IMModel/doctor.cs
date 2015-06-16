using System;
using System.Collections.Generic;
using System.Text;

namespace cn.com.farsight.IM.IMModel
{
    public class doctor
    {
        private int? _id;
        /// <summary>
        /// doctor 唯一标识
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
        private string doctor_name;
        /// <summary>
        /// 医生名(用于登陆网站和医疗客户端使用，非真实姓名)
        /// </summary>
        public string Doctor_name
        {
            get { return doctor_name; }
            set { doctor_name = value; }
        }
        private string doctor_tname;
        /// <summary>
        /// 医生真实姓名
        /// </summary>
        public string Doctor_tname
        {
            get { return doctor_tname; }
            set { doctor_tname = value; }
        }
        private string doctor_pwd;
        /// <summary>
        /// 密码(用于登陆网站和医疗客户端使用)
        /// </summary>
        public string Doctor_pwd
        {
            get { return doctor_pwd; }
            set { doctor_pwd = value; }
        }
        private int? doctor_permisson;
        /// <summary>
        /// 医生权限
        /// </summary>
        public int? Doctor_permisson
        {
            get { return doctor_permisson; }
            set { doctor_permisson = value; }
        }
        private string card_data;
        /// <summary>
        /// 医生卡号
        /// </summary>
        public string Card_data
        {
            get { return card_data; }
            set { card_data = value; }
        }
        private string doctor_tel;
        /// <summary>
        /// 医生电话
        /// </summary>
        public string Doctor_tel
        {
            get { return doctor_tel; }
            set { doctor_tel = value; }
        }
        private string doctor_add;
        /// <summary>
        /// 医生地址
        /// </summary>
        public string Doctor_add
        {
            get { return doctor_add; }
            set { doctor_add = value; }
        }
        private string doctor_level;
        /// <summary>
        /// 医生级别
        /// </summary>
        public string Doctor_level
        {
            get { return doctor_level; }
            set { doctor_level = value; }
        }
        private string doctor_gender;
        /// <summary>
        /// 医生性别
        /// </summary>
        public string Doctor_gender
        {
            get { return doctor_gender; }
            set
            {
                if (value.Equals("男") || value.Equals("女"))
                    doctor_gender = value;
            }
        }
        private string doctor_dob;
        /// <summary>
        /// 医生出生日期
        /// </summary>
        public string Doctor_dob
        {
            get { return doctor_dob; }
            set
            {
                DateTime dt;
                if (DateTime.TryParse(value, out dt))
                    doctor_dob = value;
            }
        }
    }
}
