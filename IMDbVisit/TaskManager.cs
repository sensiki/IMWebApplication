using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using cn.com.farsight.IM.IMModel;
using System.Data.Common;
using cn.com.farsight.IM.DB.SqlDB;

namespace cn.com.farsight.IM.IMDbVisit
{
    public class TaskManager
    {
        DBHelper dbHelper = DBHelper.getHelper();
        public bool getModel(task model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from task_table ");
            string where = string.Empty;
            DbParameter[] parameters = CreateWhere(model, out where);
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(" where " + where);
            }
            DataSet dt = dbHelper.Query(sql.ToString(), parameters);
            if (dt.Tables[0].Rows.Count > 0)
            {
                if (dt.Tables[0].Rows[0]["_id"] != null && dt.Tables[0].Rows[0]["_id"].ToString() != "")
                {
                    model.Id = int.Parse(dt.Tables[0].Rows[0]["_id"].ToString());
                }
                if (dt.Tables[0].Rows[0]["doctor_id"] != null && dt.Tables[0].Rows[0]["doctor_id"].ToString() != "")
                {
                    int doctor_id = int.Parse(dt.Tables[0].Rows[0]["doctor_id"].ToString());
                    if (model.Doctor_id == null)
                        model.Doctor_id = new doctor();
                    model.Doctor_id.Id = doctor_id;
                }
                if (dt.Tables[0].Rows[0]["result_id"] != null && dt.Tables[0].Rows[0]["result_id"].ToString() != "")
                {
                    int result_id = int.Parse(dt.Tables[0].Rows[0]["result_id"].ToString());
                    if (model.Result_id == null)
                        model.Result_id = new result();
                    model.Result_id.Id = result_id;
                }
                if (dt.Tables[0].Rows[0]["patient_id"] != null && dt.Tables[0].Rows[0]["patient_id"].ToString() != "")
                {
                    int patient_id = int.Parse(dt.Tables[0].Rows[0]["patient_id"].ToString());
                    if (model.Patient_id == null)
                        model.Patient_id = new patient();
                    model.Patient_id.Id = patient_id;
                }
                if (dt.Tables[0].Rows[0]["task_items"] != null && dt.Tables[0].Rows[0]["task_items"].ToString() != "")
                {
                    model.Task_items = int.Parse(dt.Tables[0].Rows[0]["task_items"].ToString());
                }
                if (dt.Tables[0].Rows[0]["finished_items"] != null && dt.Tables[0].Rows[0]["finished_items"].ToString() != "")
                {
                    model.Finished_items = int.Parse(dt.Tables[0].Rows[0]["finished_items"].ToString());
                }
                if (dt.Tables[0].Rows[0]["date"] != null && dt.Tables[0].Rows[0]["date"].ToString() != "")
                {
                    model.Date = dt.Tables[0].Rows[0]["date"].ToString();
                }


                return true;
            }
            return false;
        }

        private DbParameter[] CreateWhere(task model, out string where)
        {
            StringBuilder sqlwhere = new StringBuilder();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            SqlParameter parameter;
            if (model.Id.HasValue)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" _id = @_id");
                parameter = new SqlParameter("_id", SqlDbType.Int);
                parameter.Value = model.Id.Value;
                parameterList.Add(parameter);
            }
            if (model.Doctor_id != null)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" doctor_id = @doctor_id");
                parameter = new SqlParameter("doctor_id", SqlDbType.Int);
                parameter.Value = model.Doctor_id.Id;
                parameterList.Add(parameter);
            }
            if (model.Result_id != null)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" result_id = @result_id");
                parameter = new SqlParameter("result_id", SqlDbType.Int);
                parameter.Value = model.Result_id.Id;
                parameterList.Add(parameter);
            }
            if (model.Patient_id != null)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_id = @patient_id");
                parameter = new SqlParameter("patient_id", SqlDbType.Int);
                parameter.Value = model.Patient_id.Id;
                parameterList.Add(parameter);
            }
            if (model.Finished_items.HasValue)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" finished_items = @finished_items");
                parameter = new SqlParameter("finished_items", SqlDbType.Int);
                parameter.Value = model.Finished_items.Value;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Date))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append("datediff(day, convert(varchar(50), date),'" + model.Date + "')=0");
            }

            where = sqlwhere.ToString();
            return parameterList.ToArray();
        }
        private DbParameter[] CreateWhere_view(task model, out string where)
        {
            StringBuilder sqlwhere = new StringBuilder();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            SqlParameter parameter;
            if (model.Id.HasValue)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" _id = @_id");
                parameter = new SqlParameter("_id", SqlDbType.Int);
                parameter.Value = model.Id.Value;
                parameterList.Add(parameter);
            }
            if (model.Doctor_id != null)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" doctor_id = @doctor_id");
                parameter = new SqlParameter("doctor_id", SqlDbType.Int);
                parameter.Value = model.Doctor_id.Id;
                parameterList.Add(parameter);
            }
            if (model.Result_id != null)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" result_id = @result_id");
                parameter = new SqlParameter("result_id", SqlDbType.Int);
                parameter.Value = model.Result_id.Id;
                parameterList.Add(parameter);
            }
            if (model.Patient_id != null)
            {
                if (model.Patient_id.Id.HasValue)
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_id = @patient_id");
                    parameter = new SqlParameter("patient_id", SqlDbType.Int);
                    parameter.Value = model.Patient_id.Id;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_name))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_name like @patient_name");
                    parameter = new SqlParameter("patient_name", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_name;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_dob))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_dob like @patient_dob");
                    parameter = new SqlParameter("patient_dob", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_dob;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_gender))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_gender like @patient_gender");
                    parameter = new SqlParameter("patient_gender", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_gender;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_tel))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_tel like @patient_tel");
                    parameter = new SqlParameter("patient_tel", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_tel;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_mail))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_mail like @patient_mail");
                    parameter = new SqlParameter("patient_mail", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_mail;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_id_type))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_id_type like @patient_id_type");
                    parameter = new SqlParameter("patient_id_type", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_id_type;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_idcard))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_idcard like @patient_idcard");
                    parameter = new SqlParameter("patient_idcard", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_idcard;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_add))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_add like @patient_add");
                    parameter = new SqlParameter("patient_add", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_add;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_marriage))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_marriage like @patient_marriage");
                    parameter = new SqlParameter("patient_marriage", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_marriage;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_height))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_height like @patient_height");
                    parameter = new SqlParameter("patient_height", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_height;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_weight))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_weight like @patient_weight");
                    parameter = new SqlParameter("patient_weight", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_weight;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_disease))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_disease like @patient_disease");
                    parameter = new SqlParameter("patient_disease", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_disease;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Patient_qr_code))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" patient_qr_code like @patient_qr_code");
                    parameter = new SqlParameter("patient_qr_code", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Patient_qr_code;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Info))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" info like @info");
                    parameter = new SqlParameter("info", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Info;
                    parameterList.Add(parameter);
                }
                if (!string.IsNullOrEmpty(model.Patient_id.Reg_date))
                {
                    if (sqlwhere.Length > 0)
                    {
                        sqlwhere.Append(" and ");
                    }
                    sqlwhere.Append(" reg_date like @reg_date");
                    parameter = new SqlParameter("reg_date", SqlDbType.NText);
                    parameter.Value = model.Patient_id.Reg_date;
                    parameterList.Add(parameter);
                }
            }
            if (model.Finished_items.HasValue)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" finished_items = @finished_items");
                parameter = new SqlParameter("finished_items", SqlDbType.Int);
                parameter.Value = model.Finished_items.Value;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Date))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" date like @date");
                parameter = new SqlParameter("date", SqlDbType.NText);
                parameter.Value = model.Date;
                parameterList.Add(parameter);
            }

            where = sqlwhere.ToString();
            return parameterList.ToArray();
        }
        public bool addModel(task model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into task_table(");
            strSql.Append("doctor_id,result_id,patient_id,task_items,finished_items,date)");
            strSql.Append(" values (");
            strSql.Append("@doctor_id,@result_id,@patient_id,@task_items,@finished_items,@date)");
            SqlParameter[] parameters = {
					new SqlParameter("@doctor_id", SqlDbType.Int),
					new SqlParameter("@result_id", SqlDbType.Int),
                    new SqlParameter("@patient_id", SqlDbType.Int),
					new SqlParameter("@task_items", SqlDbType.Int),
					new SqlParameter("@finished_items", SqlDbType.Int),
                    new SqlParameter("@date", SqlDbType.NText)};
            if (model.Doctor_id != null)
                parameters[0].Value = model.Doctor_id.Id.Value;
            if (model.Result_id != null)
                parameters[1].Value = model.Result_id.Id;
            if (model.Patient_id != null)
                parameters[2].Value = model.Patient_id.Id;
            parameters[3].Value = model.Task_items.HasValue ? model.Task_items.Value : 0;
            parameters[4].Value = model.Finished_items.HasValue ? model.Finished_items.Value : 0;
            parameters[5].Value = model.Date;
            int rows = dbHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool updateModel(task model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update task_table set ");
            strSql.Append("doctor_id=@doctor_id,");
            strSql.Append("result_id=@result_id,");
            strSql.Append("patient_id=@patient_id,");
            strSql.Append("task_items=@task_items,");
            strSql.Append("finished_items=@finished_items,");
            strSql.Append("date=@date");
            strSql.Append(" where _id = @_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@_id", SqlDbType.Int),
					new SqlParameter("@doctor_id", SqlDbType.Int),
					new SqlParameter("@result_id", SqlDbType.Int),
                    new SqlParameter("@patient_id", SqlDbType.Int),
					new SqlParameter("@task_items", SqlDbType.Int),
					new SqlParameter("@finished_items", SqlDbType.Int),
                    new SqlParameter("@date", SqlDbType.NText)};
            parameters[0].Value = model.Id;
            if (model.Doctor_id != null)
                parameters[1].Value = model.Doctor_id.Id;
            if (model.Result_id != null)
                parameters[2].Value = model.Result_id.Id;
            if (model.Patient_id != null)
                parameters[3].Value = model.Patient_id.Id;
            parameters[4].Value = model.Task_items;
            parameters[5].Value = model.Finished_items;
            parameters[6].Value = model.Date;
            int rows = dbHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool deleteModel(task model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from task_table ");
            string where = string.Empty;
            DbParameter[] parameters = CreateWhere(model, out where);
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(" where " + where);
            }
            int cout = dbHelper.ExecuteSql(sql.ToString(), parameters);
            if (cout > 0)
            {
                return true;
            }
            return false;
        }
        public List<task> getModelList(task model)
        {
            List<task> ModelList = new List<task>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from task_table ");
            string where = string.Empty;
            DbParameter[] parameters = CreateWhere(model, out where);
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(" where " + where);
            }
            DataSet dt = dbHelper.Query(sql.ToString(), parameters);
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                ModelList.Add(CreateModel(dt.Tables[0].Rows[i]));
            }
            return ModelList;
        }
        /// <summary>
        /// 根据DataRow创建对象
        /// </summary>
        /// <returns></returns>
        public task CreateModel(DataRow dr)
        {
            task model = new task();
            if (dr["_id"] != null && dr["_id"].ToString() != "")
            {
                model.Id = int.Parse(dr["_id"].ToString());
            }
            if (dr["doctor_id"] != null && dr["doctor_id"].ToString() != "")
            {
                int doctor_id = int.Parse(dr["doctor_id"].ToString());
                if (model.Doctor_id == null)
                    model.Doctor_id = new doctor();
                model.Doctor_id.Id = doctor_id;
            }
            if (dr["result_id"] != null && dr["result_id"].ToString() != "")
            {
                int result_id = int.Parse(dr["result_id"].ToString());
                if (model.Result_id == null)
                    model.Result_id = new result();
                model.Result_id.Id = result_id;
            }
            if (dr["patient_id"] != null && dr["patient_id"].ToString() != "")
            {
                int patient_id = int.Parse(dr["patient_id"].ToString());
                if (model.Patient_id == null)
                    model.Patient_id = new patient();
                model.Patient_id.Id = patient_id;
            }
            if (dr["task_items"] != null && dr["task_items"].ToString() != "")
            {
                model.Task_items = int.Parse(dr["task_items"].ToString());
            }
            if (dr["finished_items"] != null && dr["finished_items"].ToString() != "")
            {
                model.Finished_items = int.Parse(dr["finished_items"].ToString());
            }
            if (dr["date"] != null && dr["date"].ToString() != "")
            {
                model.Date = dr["date"].ToString();
            }

            return model;
        }
        /// <summary>
        /// 根据DataRow创建对象
        /// </summary>
        /// <returns></returns>
        public task CreateModel_view(DataRow dr)
        {
            task model = new task();
            if (dr["_id"] != null && dr["_id"].ToString() != "")
            {
                model.Id = int.Parse(dr["_id"].ToString());
            }
            if (dr["doctor_id"] != null && dr["doctor_id"].ToString() != "")
            {
                int doctor_id = int.Parse(dr["doctor_id"].ToString());
                if (model.Doctor_id == null)
                    model.Doctor_id = new doctor();
                model.Doctor_id.Id = doctor_id;
            }
            if (dr["result_id"] != null && dr["result_id"].ToString() != "")
            {
                int result_id = int.Parse(dr["result_id"].ToString());
                if (model.Result_id == null)
                    model.Result_id = new result();
                model.Result_id.Id = result_id;
            }
            if (model.Patient_id == null)
                model.Patient_id = new patient();

            if (dr["patient_id"] != null && dr["patient_id"].ToString() != "")
            {
                int patient_id = int.Parse(dr["patient_id"].ToString());

                model.Patient_id.Id = patient_id;
            }
            if (dr["task_items"] != null && dr["task_items"].ToString() != "")
            {
                model.Task_items = int.Parse(dr["task_items"].ToString());
            }
            if (dr["finished_items"] != null && dr["finished_items"].ToString() != "")
            {
                model.Finished_items = int.Parse(dr["finished_items"].ToString());
            }
            if (dr["date"] != null && dr["date"].ToString() != "")
            {
                model.Date = dr["date"].ToString();
            }
            if (dr["patient_name"] != null && dr["patient_name"].ToString() != "")
            {
                model.Patient_id.Patient_name = dr["patient_name"].ToString();
            }
            if (dr["patient_dob"] != null && dr["patient_dob"].ToString() != "")
            {
                model.Patient_id.Patient_dob = dr["patient_dob"].ToString();
            }
            if (dr["patient_gender"] != null && dr["patient_gender"].ToString() != "")
            {
                model.Patient_id.Patient_gender = dr["patient_gender"].ToString();
            }
            if (dr["patient_tel"] != null && dr["patient_tel"].ToString() != "")
            {
                model.Patient_id.Patient_tel = dr["patient_tel"].ToString();
            }
            if (dr["patient_mail"] != null && dr["patient_mail"].ToString() != "")
            {
                model.Patient_id.Patient_mail = dr["patient_mail"].ToString();
            }
            if (dr["patient_id_type"] != null && dr["patient_id_type"].ToString() != "")
            {
                model.Patient_id.Patient_id_type = dr["patient_id_type"].ToString();
            }
            if (dr["patient_idcard"] != null && dr["patient_idcard"].ToString() != "")
            {
                model.Patient_id.Patient_idcard = dr["patient_idcard"].ToString();
            }
            if (dr["patient_add"] != null && dr["patient_add"].ToString() != "")
            {
                model.Patient_id.Patient_add = dr["patient_add"].ToString();
            }
            if (dr["patient_marriage"] != null && dr["patient_marriage"].ToString() != "")
            {
                model.Patient_id.Patient_marriage = dr["patient_marriage"].ToString();
            }
            if (dr["patient_photo"] != null && dr["patient_photo"].ToString() != "")
            {
                model.Patient_id.Patient_photo = dr["patient_photo"].ToString();
            }
            if (dr["patient_height"] != null && dr["patient_height"].ToString() != "")
            {
                model.Patient_id.Patient_height = dr["patient_height"].ToString();
            }
            if (dr["patient_weight"] != null && dr["patient_weight"].ToString() != "")
            {
                model.Patient_id.Patient_weight = dr["patient_weight"].ToString();
            }
            if (dr["patient_disease"] != null && dr["patient_disease"].ToString() != "")
            {
                model.Patient_id.Patient_disease = dr["patient_disease"].ToString();
            }
            if (dr["patient_qr_code"] != null && dr["patient_qr_code"].ToString() != "")
            {
                model.Patient_id.Patient_qr_code = dr["patient_qr_code"].ToString();
            }
            if (dr["info"] != null && dr["info"].ToString() != "")
            {
                model.Patient_id.Info = dr["info"].ToString();
            }
            if (dr["reg_date"] != null && dr["reg_date"].ToString() != "")
            {
                model.Patient_id.Reg_date = dr["reg_date"].ToString();
            }

            return model;
        }
        /// <summary>
        /// 查询一定数量的数据
        /// </summary>
        /// <returns></returns>
        public List<task> getModelListTop(task model, int top)
        {
            List<task> ModelList = new List<task>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select ");
            if (top > 0)
                sql.Append(" top " + top.ToString());
            sql.Append("* from task_table ");
            string where = string.Empty;
            DbParameter[] parameters = CreateWhere(model, out where);
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(" where " + where);
            }
            sql.Append(" order by _id");
            DataSet dt = dbHelper.Query(sql.ToString(), parameters);
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                ModelList.Add(CreateModel(dt.Tables[0].Rows[i]));
            }
            return ModelList;
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(task model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(1) FROM task_table ");
            string where = string.Empty;
            DbParameter[] parameters = CreateWhere(model, out where);
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(" where " + where);
            }

            object obj = dbHelper.GetSinge(sql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int rgc_view(task model, bool isdiag)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(1) FROM view_task_table ");
            string where = string.Empty;
            DbParameter[] parameters = CreateWhere_view(model, out where);
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(" where " + where + (isdiag ? " and finished_items != task_items " : ""));
            }
            else if (isdiag)
            {
                sql.Append("where finished_items != task_items");
            }



            object obj = dbHelper.GetSinge(sql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<task> GetListByPage(task model, int startIndex, int endIndex)
        {
            StringBuilder sql = new StringBuilder();
            List<task> ModelList = new List<task>();
            sql.Append("SELECT * FROM ( ");
            sql.Append(" SELECT ROW_NUMBER() OVER (");
            string where = string.Empty;
            DbParameter[] parameters = CreateWhere(model, out where);

            sql.Append("order by T._id");
            sql.Append(")AS Row, T.*  from task_table T ");
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(" where " + where);
            }

            sql.Append(" ) TT");
            sql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataSet dt = dbHelper.Query(sql.ToString(), parameters);
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                ModelList.Add(CreateModel(dt.Tables[0].Rows[i]));
            }
            return ModelList;
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<task> glbp_view(task model, int startIndex, int endIndex, bool isdiag)
        {
            StringBuilder sql = new StringBuilder();
            List<task> ModelList = new List<task>();
            sql.Append("SELECT * FROM ( ");
            sql.Append(" SELECT ROW_NUMBER() OVER (");
            string where = string.Empty;
            DbParameter[] parameters = CreateWhere_view(model, out where);

            sql.Append("order by T._id");
            sql.Append(")AS Row, T.*  from view_task_table T ");
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(" where " + where + (isdiag ? " and finished_items != task_items " : ""));
            }
            else if (isdiag)
            {
                sql.Append("where finished_items != task_items");
            }

            sql.Append(" ) TT");
            sql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataSet dt = dbHelper.Query(sql.ToString(), parameters);
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                ModelList.Add(CreateModel_view(dt.Tables[0].Rows[i]));
            }
            return ModelList;
        }
    }
}
