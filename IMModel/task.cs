using System;
using System.Collections.Generic;
using System.Text;

namespace cn.com.farsight.IM.IMModel
{
    public class task
    {
        private int? _id;
        /// <summary>
        /// 任务唯一标识
        /// </summary>
        public int? Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private doctor doctor_id;
        /// <summary>
        /// 医生ID
        /// </summary>
        public doctor Doctor_id
        {
            get { return doctor_id; }
            set { doctor_id = value; }
        }
        private result result_id;
        /// <summary>
        /// 结果ID
        /// </summary>
        public result Result_id
        {
            get { return result_id; }
            set { result_id = value; }
        }
        private patient patient_id;
        /// <summary>
        /// 患者ID
        /// </summary>
        public patient Patient_id
        {
            get { return patient_id; }
            set { patient_id = value; }
        }
        private int? task_items;
        /// <summary>
        /// 检查项目
        /// 0 bit:心电任务
        /// 1 bit:血压任务
        /// 2 bit:血糖任务
        /// 3 bit:体温任务
        /// </summary>
        public int? Task_items
        {
            get { return task_items; }
            set { task_items = value; }
        }
        private int? finished_items;
        /// <summary>
        /// 完成项目
        /// 0 bit:心电任务
        /// 1 bit:血压任务
        /// 2 bit:血糖任务
        /// 3 bit:体温任务
        /// </summary>
        public int? Finished_items
        {
            get { return finished_items; }
            set { finished_items = value; }
        }
        private string date;
        /// <summary>
        /// 任务生成时间
        /// </summary>
        public string Date
        {
            get { return date; }
            set
            {
                DateTime dt;
                if (DateTime.TryParse(value, out dt))
                    date = value;
            }
        }
    }
}
