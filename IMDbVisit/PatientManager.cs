using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using cn.com.farsight.IM.IMModel;
using System.Data;
using System.Data.Common;
using cn.com.farsight.IM.DB.SqlDB;

namespace cn.com.farsight.IM.IMDbVisit
{
    public class PatientManager
    {
        DBHelper dbHelper = DBHelper.getHelper();
        public bool getModel(patient model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from patient_table ");
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
                if (dt.Tables[0].Rows[0]["patient_name"] != null && dt.Tables[0].Rows[0]["patient_name"].ToString() != "")
                {
                    model.Patient_name = dt.Tables[0].Rows[0]["patient_name"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_dob"] != null && dt.Tables[0].Rows[0]["patient_dob"].ToString() != "")
                {
                    model.Patient_dob = dt.Tables[0].Rows[0]["patient_dob"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_gender"] != null && dt.Tables[0].Rows[0]["patient_gender"].ToString() != "")
                {
                    model.Patient_gender = dt.Tables[0].Rows[0]["patient_gender"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_tel"] != null && dt.Tables[0].Rows[0]["patient_tel"].ToString() != "")
                {
                    model.Patient_tel = dt.Tables[0].Rows[0]["patient_tel"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_mail"] != null && dt.Tables[0].Rows[0]["patient_mail"].ToString() != "")
                {
                    model.Patient_mail = dt.Tables[0].Rows[0]["patient_mail"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_id_type"] != null && dt.Tables[0].Rows[0]["patient_id_type"].ToString() != "")
                {
                    model.Patient_id_type = dt.Tables[0].Rows[0]["patient_id_type"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_idcard"] != null && dt.Tables[0].Rows[0]["patient_idcard"].ToString() != "")
                {
                    model.Patient_idcard = dt.Tables[0].Rows[0]["patient_idcard"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_add"] != null && dt.Tables[0].Rows[0]["patient_add"].ToString() != "")
                {
                    model.Patient_add = dt.Tables[0].Rows[0]["patient_add"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_marriage"] != null && dt.Tables[0].Rows[0]["patient_marriage"].ToString() != "")
                {
                    model.Patient_marriage = dt.Tables[0].Rows[0]["patient_marriage"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_photo"] != null && dt.Tables[0].Rows[0]["patient_photo"].ToString() != "")
                {
                    model.Patient_photo = dt.Tables[0].Rows[0]["patient_photo"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_height"] != null && dt.Tables[0].Rows[0]["patient_height"].ToString() != "")
                {
                    model.Patient_height = dt.Tables[0].Rows[0]["patient_height"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_weight"] != null && dt.Tables[0].Rows[0]["patient_weight"].ToString() != "")
                {
                    model.Patient_weight = dt.Tables[0].Rows[0]["patient_weight"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_disease"] != null && dt.Tables[0].Rows[0]["patient_disease"].ToString() != "")
                {
                    model.Patient_disease = dt.Tables[0].Rows[0]["patient_disease"].ToString();
                }
                if (dt.Tables[0].Rows[0]["patient_qr_code"] != null && dt.Tables[0].Rows[0]["patient_qr_code"].ToString() != "")
                {
                    model.Patient_qr_code = dt.Tables[0].Rows[0]["patient_qr_code"].ToString();
                }
                if (dt.Tables[0].Rows[0]["info"] != null && dt.Tables[0].Rows[0]["info"].ToString() != "")
                {
                    model.Info = dt.Tables[0].Rows[0]["info"].ToString();
                }
                if (dt.Tables[0].Rows[0]["reg_date"] != null && dt.Tables[0].Rows[0]["reg_date"].ToString() != "")
                {
                    model.Reg_date = dt.Tables[0].Rows[0]["reg_date"].ToString();
                }

                return true;
            }
            return false;
        }

        private DbParameter[] CreateWhere(patient model, out string where)
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
            if (!string.IsNullOrEmpty(model.Patient_name))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_name like @patient_name");
                parameter = new SqlParameter("patient_name", SqlDbType.NText);
                parameter.Value = model.Patient_name;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_dob))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_dob like @patient_dob");
                parameter = new SqlParameter("patient_dob", SqlDbType.NText);
                parameter.Value = model.Patient_dob;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_gender))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_gender like @patient_gender");
                parameter = new SqlParameter("patient_gender", SqlDbType.NText);
                parameter.Value = model.Patient_gender;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_tel))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_tel like @patient_tel");
                parameter = new SqlParameter("patient_tel", SqlDbType.NText);
                parameter.Value = model.Patient_tel;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_mail))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_mail like @patient_mail");
                parameter = new SqlParameter("patient_mail", SqlDbType.NText);
                parameter.Value = model.Patient_mail;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_id_type))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_id_type like @patient_id_type");
                parameter = new SqlParameter("patient_id_type", SqlDbType.NText);
                parameter.Value = model.Patient_id_type;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_idcard))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_idcard like @patient_idcard");
                parameter = new SqlParameter("patient_idcard", SqlDbType.NText);
                parameter.Value = model.Patient_idcard;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_add))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_add like @patient_add");
                parameter = new SqlParameter("patient_add", SqlDbType.NText);
                parameter.Value = model.Patient_add;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_marriage))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_marriage like @patient_marriage");
                parameter = new SqlParameter("patient_marriage", SqlDbType.NText);
                parameter.Value = model.Patient_marriage;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_height))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_height like @patient_height");
                parameter = new SqlParameter("patient_height", SqlDbType.NText);
                parameter.Value = model.Patient_height;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_weight))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_weight like @patient_weight");
                parameter = new SqlParameter("patient_weight", SqlDbType.NText);
                parameter.Value = model.Patient_weight;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_disease))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_disease like @patient_disease");
                parameter = new SqlParameter("patient_disease", SqlDbType.NText);
                parameter.Value = model.Patient_disease;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Patient_qr_code))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" patient_qr_code like @patient_qr_code");
                parameter = new SqlParameter("patient_qr_code", SqlDbType.NText);
                parameter.Value = model.Patient_qr_code;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Info))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" info like @info");
                parameter = new SqlParameter("info", SqlDbType.NText);
                parameter.Value = model.Info;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Reg_date))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append("datediff(day, convert(varchar(50), reg_date),'" + model.Reg_date + "')=0");
            }


            where = sqlwhere.ToString();
            return parameterList.ToArray();
        }
        public bool addModel(patient model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into patient_table(");
            strSql.Append("patient_name,patient_dob,patient_gender,patient_tel,patient_mail,");
            strSql.Append("patient_id_type,patient_idcard,patient_add,patient_marriage,patient_photo,patient_height,patient_weight,patient_disease,patient_qr_code,info,reg_date)");
            strSql.Append(" values (");
            strSql.Append("@patient_name,@patient_dob,@patient_gender,@patient_tel,@patient_mail,@patient_id_type,@patient_idcard,@patient_add,@patient_marriage,@patient_photo,@patient_height,@patient_weight,@patient_disease,@patient_qr_code,@info,@reg_date)");
            strSql.Append("select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@patient_name", SqlDbType.NText),
					new SqlParameter("@patient_dob", SqlDbType.NText),
					new SqlParameter("@patient_gender", SqlDbType.NText),
					new SqlParameter("@patient_tel", SqlDbType.NText),
                    new SqlParameter("@patient_mail", SqlDbType.NText),
                    new SqlParameter("@patient_id_type", SqlDbType.NText),
                    new SqlParameter("@patient_idcard", SqlDbType.NText),
                    new SqlParameter("@patient_add", SqlDbType.NText),
                    new SqlParameter("@patient_marriage", SqlDbType.NText),
                    new SqlParameter("@patient_photo", SqlDbType.NText),
                    new SqlParameter("@patient_height", SqlDbType.NText),
                    new SqlParameter("@patient_weight", SqlDbType.NText),
                    new SqlParameter("@patient_disease", SqlDbType.NText),
                    new SqlParameter("@patient_qr_code", SqlDbType.NText),
                    new SqlParameter("@info", SqlDbType.NText),
                    new SqlParameter("@reg_date", SqlDbType.NText)};
            parameters[0].Value = model.Patient_name;
            parameters[1].Value = model.Patient_dob;
            parameters[2].Value = model.Patient_gender;
            parameters[3].Value = model.Patient_tel;
            parameters[4].Value = model.Patient_mail;
            parameters[5].Value = model.Patient_id_type;
            parameters[6].Value = model.Patient_idcard;
            parameters[7].Value = model.Patient_add;
            parameters[8].Value = model.Patient_marriage;
            parameters[9].Value = model.Patient_photo;
            parameters[10].Value = model.Patient_height;
            parameters[11].Value = model.Patient_weight;
            parameters[12].Value = model.Patient_disease;
            parameters[13].Value = model.Patient_qr_code;
            parameters[14].Value = model.Info;
            parameters[15].Value = model.Reg_date;
            int result = Convert.ToInt32(dbHelper.GetSinge(strSql.ToString(), parameters));
            if (result > 0)
            {
                model.Id = result;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool updateModel(patient model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update patient_table set ");
            strSql.Append("patient_name=@patient_name,");
            strSql.Append("patient_dob=@patient_dob,");
            strSql.Append("patient_gender=@patient_gender,");
            strSql.Append("patient_tel=@patient_tel,");
            strSql.Append("patient_mail=@patient_mail,");
            strSql.Append("patient_id_type=@patient_id_type,");
            strSql.Append("patient_idcard=@patient_idcard,");
            strSql.Append("patient_add=@patient_add,");
            strSql.Append("patient_marriage=@patient_marriage,");
            strSql.Append("patient_photo=@patient_photo,");
            strSql.Append("patient_height=@patient_height,");
            strSql.Append("patient_weight=@patient_weight,");
            strSql.Append("patient_disease=@patient_disease,");
            strSql.Append("patient_qr_code=@patient_qr_code,");
            strSql.Append("info=@info,");
            strSql.Append("reg_date=@reg_date");
            strSql.Append(" where _id = @_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@_id", SqlDbType.Int),
					new SqlParameter("@patient_name", SqlDbType.NText),
					new SqlParameter("@patient_dob", SqlDbType.NText),
					new SqlParameter("@patient_gender", SqlDbType.NText),
					new SqlParameter("@patient_tel", SqlDbType.NText),
                    new SqlParameter("@patient_mail", SqlDbType.NText),
                    new SqlParameter("@patient_id_type", SqlDbType.NText),
                    new SqlParameter("@patient_idcard", SqlDbType.NText),
                    new SqlParameter("@patient_add", SqlDbType.NText),
                    new SqlParameter("@patient_marriage", SqlDbType.NText),
                    new SqlParameter("@patient_photo", SqlDbType.NText),
                    new SqlParameter("@patient_height", SqlDbType.NText),
                    new SqlParameter("@patient_weight", SqlDbType.NText),
                    new SqlParameter("@patient_disease", SqlDbType.NText),
                    new SqlParameter("@patient_qr_code", SqlDbType.NText),
                    new SqlParameter("@info", SqlDbType.NText),
                    new SqlParameter("@reg_date", SqlDbType.NText)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Patient_name;
            parameters[2].Value = model.Patient_dob;
            parameters[3].Value = model.Patient_gender;
            parameters[4].Value = model.Patient_tel;
            parameters[5].Value = model.Patient_mail;
            parameters[6].Value = model.Patient_id_type;
            parameters[7].Value = model.Patient_idcard;
            parameters[8].Value = model.Patient_add;
            parameters[9].Value = model.Patient_marriage;
            parameters[10].Value = model.Patient_photo;
            parameters[11].Value = model.Patient_height;
            parameters[12].Value = model.Patient_weight;
            parameters[13].Value = model.Patient_disease;
            parameters[14].Value = model.Patient_qr_code;
            parameters[15].Value = model.Info;
            parameters[16].Value = model.Reg_date;
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
        public bool deleteModel(patient model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from patient_table ");
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
        public List<patient> getModelList(patient model)
        {
            List<patient> ModelList = new List<patient>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from patient_table ");
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
        public patient CreateModel(DataRow dr)
        {
            patient model = new patient();
            if (dr["_id"] != null && dr["_id"].ToString() != "")
            {
                model.Id = int.Parse(dr["_id"].ToString());
            }
            if (dr["patient_name"] != null && dr["patient_name"].ToString() != "")
            {
                model.Patient_name = dr["patient_name"].ToString();
            }
            if (dr["patient_dob"] != null && dr["patient_dob"].ToString() != "")
            {
                model.Patient_dob = dr["patient_dob"].ToString();
            }
            if (dr["patient_gender"] != null && dr["patient_gender"].ToString() != "")
            {
                model.Patient_gender = dr["patient_gender"].ToString();
            }
            if (dr["patient_tel"] != null && dr["patient_tel"].ToString() != "")
            {
                model.Patient_tel = dr["patient_tel"].ToString();
            }
            if (dr["patient_mail"] != null && dr["patient_mail"].ToString() != "")
            {
                model.Patient_mail = dr["patient_mail"].ToString();
            }
            if (dr["patient_id_type"] != null && dr["patient_id_type"].ToString() != "")
            {
                model.Patient_id_type = dr["patient_id_type"].ToString();
            }
            if (dr["patient_idcard"] != null && dr["patient_idcard"].ToString() != "")
            {
                model.Patient_idcard = dr["patient_idcard"].ToString();
            }
            if (dr["patient_add"] != null && dr["patient_add"].ToString() != "")
            {
                model.Patient_add = dr["patient_add"].ToString();
            }
            if (dr["patient_marriage"] != null && dr["patient_marriage"].ToString() != "")
            {
                model.Patient_marriage = dr["patient_marriage"].ToString();
            }
            if (dr["patient_photo"] != null && dr["patient_photo"].ToString() != "")
            {
                model.Patient_photo = dr["patient_photo"].ToString();
            }
            if (dr["patient_height"] != null && dr["patient_height"].ToString() != "")
            {
                model.Patient_height = dr["patient_height"].ToString();
            }
            if (dr["patient_weight"] != null && dr["patient_weight"].ToString() != "")
            {
                model.Patient_weight = dr["patient_weight"].ToString();
            }
            if (dr["patient_disease"] != null && dr["patient_disease"].ToString() != "")
            {
                model.Patient_disease = dr["patient_disease"].ToString();
            }
            if (dr["patient_qr_code"] != null && dr["patient_qr_code"].ToString() != "")
            {
                model.Patient_qr_code = dr["patient_qr_code"].ToString();
            }
            if (dr["info"] != null && dr["info"].ToString() != "")
            {
                model.Info = dr["info"].ToString();
            }
            if (dr["reg_date"] != null && dr["reg_date"].ToString() != "")
            {
                model.Reg_date = dr["reg_date"].ToString();
            }

            return model;
        }
        /// <summary>
        /// 查询一定数量的数据
        /// </summary>
        /// <returns></returns>
        public List<patient> getModelListTop(patient model, int top)
        {
            List<patient> ModelList = new List<patient>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select ");
            if (top > 0)
                sql.Append(" top " + top.ToString());
            sql.Append("* from patient_table ");
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
        public int GetRecordCount(patient model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(1) FROM patient_table ");
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
        /// 分页获取数据列表
        /// </summary>
        public List<patient> GetListByPage(patient model, int startIndex, int endIndex)
        {
            StringBuilder sql = new StringBuilder();
            List<patient> ModelList = new List<patient>();
            sql.Append("SELECT * FROM ( ");
            sql.Append(" SELECT ROW_NUMBER() OVER (");
            string where = string.Empty;
            DbParameter[] parameters = CreateWhere(model, out where);

            sql.Append("order by T._id");
            sql.Append(")AS Row, T.*  from patient_table T ");
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
    }
}
