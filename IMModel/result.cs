using System;
using System.Collections.Generic;
using System.Text;

namespace cn.com.farsight.IM.IMModel
{
    public class result
    {
        private int? _id;
        /// <summary>
        /// 结果唯一标识
        /// </summary>
        public int? Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string ecg_data;
        /// <summary>
        /// 心电数据
        /// </summary>
        public string Ecg_data
        {
            get { return ecg_data; }
            set { ecg_data = value; }
        }
        private long? ecg_time;
        /// <summary>
        /// 心电采集时间
        /// </summary>
        public long? Ecg_time
        {
            get { return ecg_time; }
            set { ecg_time = value; }
        }
        private string ecg_info;
        /// <summary>
        /// 心电结果建议
        /// </summary>
        public string Ecg_info
        {
            get { return ecg_info; }
            set { ecg_info = value; }
        }
        private string hp_data;
        /// <summary>
        /// 收缩压数据
        /// </summary>
        public string Hp_data
        {
            get { return hp_data; }
            set { hp_data = value; }
        }
        private string lp_data;
        /// <summary>
        /// 舒张压数据
        /// </summary>
        public string Lp_data
        {
            get { return lp_data; }
            set { lp_data = value; }
        }
        private long? bloodpress_time;
        /// <summary>
        /// 血压采集时间
        /// </summary>
        public long? Bloodpress_time
        {
            get { return bloodpress_time; }
            set { bloodpress_time = value; }
        }
        private string bloodpress_info;
        /// <summary>
        /// 血压结果建议
        /// </summary>
        public string Bloodpress_info
        {
            get { return bloodpress_info; }
            set { bloodpress_info = value; }
        }
        private string glucose_data;
        /// <summary>
        /// 血糖数据
        /// </summary>
        public string Glucose_data
        {
            get { return glucose_data; }
            set { glucose_data = value; }
        }
        private long? glucose_time;
        /// <summary>
        /// 血糖采集时间
        /// </summary>
        public long? Glucose_time
        {
            get { return glucose_time; }
            set { glucose_time = value; }
        }
        private string glucose_info;
        /// <summary>
        /// 血糖结果建议
        /// </summary>
        public string Glucose_info
        {
            get { return glucose_info; }
            set { glucose_info = value; }
        }
        private string temperature;
        /// <summary>
        /// 体温数据
        /// </summary>
        public string Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }
        private long? temperature_time;
        /// <summary>
        /// 体温采集时间
        /// </summary>
        public long? Temperature_time
        {
            get { return temperature_time; }
            set { temperature_time = value; }
        }
        private string temperature_info;
        /// <summary>
        /// 体温结果建议
        /// </summary>
        public string Temperature_info
        {
            get { return temperature_info; }
            set { temperature_info = value; }
        }
        private string date;
        /// <summary>
        /// 结果上传时间
        /// </summary>
        public string Date
        {
            get { return date; }
            set
            {
                DateTime dt;
                if (DateTime.TryParse(value, out dt))
                    date = value;
                else
                    date = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        private string info;
        /// <summary>
        /// 专家备注信息
        /// </summary>
        public string Info
        {
            get { return info; }
            set { info = value; }
        }
    }
}
