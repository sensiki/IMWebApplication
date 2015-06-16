using System;
using System.Collections.Generic;
using System.Text;
using cn.com.farsight.IM.DB.SqlDB;
using cn.com.farsight.IM.IMModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace cn.com.farsight.IM.IMDbVisit
{
    public class DoctorManager
    {
        DBHelper dbHelper = DBHelper.getHelper();
        public bool getModel(doctor model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from doctor_table ");
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
                if (dt.Tables[0].Rows[0]["doctor_name"] != null && dt.Tables[0].Rows[0]["doctor_name"].ToString() != "")
                {
                    model.Doctor_name = dt.Tables[0].Rows[0]["doctor_name"].ToString();
                }
                if (dt.Tables[0].Rows[0]["doctor_tname"] != null && dt.Tables[0].Rows[0]["doctor_tname"].ToString() != "")
                {
                    model.Doctor_tname = dt.Tables[0].Rows[0]["doctor_tname"].ToString();
                }
                if (dt.Tables[0].Rows[0]["doctor_pwd"] != null && dt.Tables[0].Rows[0]["doctor_pwd"].ToString() != "")
                {
                    model.Doctor_pwd = dt.Tables[0].Rows[0]["doctor_pwd"].ToString();
                }
                if (dt.Tables[0].Rows[0]["doctor_permisson"] != null && dt.Tables[0].Rows[0]["doctor_permisson"].ToString() != "")
                {
                    model.Doctor_permisson = int.Parse(dt.Tables[0].Rows[0]["doctor_permisson"].ToString());
                }
                if (dt.Tables[0].Rows[0]["card_data"] != null && dt.Tables[0].Rows[0]["card_data"].ToString() != "")
                {
                    model.Card_data = dt.Tables[0].Rows[0]["card_data"].ToString();
                }
                if (dt.Tables[0].Rows[0]["doctor_tel"] != null && dt.Tables[0].Rows[0]["doctor_tel"].ToString() != "")
                {
                    model.Doctor_tel = dt.Tables[0].Rows[0]["doctor_tel"].ToString();
                }
                if (dt.Tables[0].Rows[0]["doctor_add"] != null && dt.Tables[0].Rows[0]["doctor_add"].ToString() != "")
                {
                    model.Doctor_add = dt.Tables[0].Rows[0]["doctor_add"].ToString();
                }
                if (dt.Tables[0].Rows[0]["doctor_level"] != null && dt.Tables[0].Rows[0]["doctor_level"].ToString() != "")
                {
                    model.Doctor_level = dt.Tables[0].Rows[0]["doctor_level"].ToString();
                }
                if (dt.Tables[0].Rows[0]["doctor_gender"] != null && dt.Tables[0].Rows[0]["doctor_gender"].ToString() != "")
                {
                    model.Doctor_gender = dt.Tables[0].Rows[0]["doctor_gender"].ToString();
                }
                if (dt.Tables[0].Rows[0]["doctor_dob"] != null && dt.Tables[0].Rows[0]["doctor_dob"].ToString() != "")
                {
                    model.Doctor_dob = dt.Tables[0].Rows[0]["doctor_dob"].ToString();
                }
                
                return true;
            }
            return false;
        }

        private DbParameter[] CreateWhere(doctor model, out string where)
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
            if (!string.IsNullOrEmpty(model.Doctor_name))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" doctor_name like @doctor_name");
                parameter = new SqlParameter("doctor_name", SqlDbType.NText);
                parameter.Value = model.Doctor_name;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Doctor_tname))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" doctor_tname like @doctor_tname");
                parameter = new SqlParameter("doctor_tname", SqlDbType.NText);
                parameter.Value = model.Doctor_tname;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Doctor_pwd))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" doctor_pwd like @doctor_pwd");
                parameter = new SqlParameter("doctor_pwd", SqlDbType.NText);
                parameter.Value = model.Doctor_pwd;
                parameterList.Add(parameter);
            }
            if (model.Doctor_permisson.HasValue)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");                
                }
                sqlwhere.Append(" doctor_permisson = @doctor_permisson");
                parameter = new SqlParameter("doctor_permisson", SqlDbType.Int);
                parameter.Value = model.Doctor_permisson;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Card_data))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" card_data like @card_data");
                parameter = new SqlParameter("card_data", SqlDbType.NText);
                parameter.Value = model.Card_data;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Doctor_tel))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" doctor_tel like @doctor_tel");
                parameter = new SqlParameter("doctor_tel", SqlDbType.NText);
                parameter.Value = model.Doctor_tel;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Doctor_add))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" doctor_add like @doctor_add");
                parameter = new SqlParameter("doctor_add", SqlDbType.NText);
                parameter.Value = model.Doctor_add;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Doctor_level))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" doctor_level like @doctor_level");
                parameter = new SqlParameter("doctor_level", SqlDbType.NText);
                parameter.Value = model.Doctor_level;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Doctor_gender))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" doctor_gender like @doctor_gender");
                parameter = new SqlParameter("doctor_gender", SqlDbType.NText);
                parameter.Value = model.Doctor_gender;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Doctor_dob))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" doctor_dob like @doctor_dob");
                parameter = new SqlParameter("doctor_dob", SqlDbType.NText);
                parameter.Value = model.Doctor_dob;
                parameterList.Add(parameter);
            }
            

            where = sqlwhere.ToString();
            return parameterList.ToArray();
        }
        public bool addModel(doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into doctor_table(");
            strSql.Append("doctor_name,doctor_tname,doctor_pwd,doctor_permisson,card_data,doctor_tel,doctor_add,doctor_level,doctor_gender,doctor_dob)");
            strSql.Append(" values (");
            strSql.Append("@doctor_name,@doctor_tname,@doctor_pwd,@doctor_permisson,@card_data,@doctor_tel,@doctor_add,@doctor_level,@doctor_gender,@doctor_dob)");
            SqlParameter[] parameters = {
					new SqlParameter("@doctor_name", SqlDbType.NText),
                    new SqlParameter("@doctor_tname", SqlDbType.NText),
					new SqlParameter("@doctor_pwd", SqlDbType.NText),
                    new SqlParameter("@doctor_permisson", SqlDbType.Int),
					new SqlParameter("@card_data", SqlDbType.NText),
					new SqlParameter("@doctor_tel", SqlDbType.NText),
                    new SqlParameter("@doctor_add", SqlDbType.NText),
                    new SqlParameter("@doctor_level", SqlDbType.NText),
                    new SqlParameter("@doctor_gender", SqlDbType.NText),
                    new SqlParameter("@doctor_dob", SqlDbType.NText)};
            parameters[0].Value = model.Doctor_name;
            parameters[1].Value = model.Doctor_tname;
            parameters[2].Value = model.Doctor_pwd;
            parameters[3].Value = model.Doctor_permisson;
            parameters[4].Value = model.Card_data;
            parameters[5].Value = model.Doctor_tel;
            parameters[6].Value = model.Doctor_add;
            parameters[7].Value = model.Doctor_level;
            parameters[8].Value = model.Doctor_gender;
            parameters[9].Value = model.Doctor_dob;
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
        public bool updateModel(doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update doctor_table set ");
            strSql.Append("doctor_name=@doctor_name,");
            strSql.Append("doctor_tname=@doctor_tname,");
            strSql.Append("doctor_pwd=@doctor_pwd,");
            strSql.Append("doctor_permisson=@doctor_permisson,");
            strSql.Append("card_data=@card_data,");
            strSql.Append("doctor_tel=@doctor_tel,");
            strSql.Append("doctor_add=@doctor_add,");
            strSql.Append("doctor_level=@doctor_level,");
            strSql.Append("doctor_gender=@doctor_gender,");
            strSql.Append("doctor_dob=@doctor_dob");
            strSql.Append(" where _id = @_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@_id", SqlDbType.Int),
					new SqlParameter("@doctor_name", SqlDbType.NText),
                    new SqlParameter("@doctor_tname", SqlDbType.NText),
					new SqlParameter("@doctor_pwd", SqlDbType.NText),
                    new SqlParameter("@doctor_permisson", SqlDbType.Int),
					new SqlParameter("@card_data", SqlDbType.NText),
					new SqlParameter("@doctor_tel", SqlDbType.NText),
                    new SqlParameter("@doctor_add", SqlDbType.NText),
                    new SqlParameter("@doctor_level", SqlDbType.NText),
                    new SqlParameter("@doctor_gender", SqlDbType.NText),
                    new SqlParameter("@doctor_dob", SqlDbType.NText)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Doctor_name;
            parameters[2].Value = model.Doctor_tname;
            parameters[3].Value = model.Doctor_pwd;
            parameters[4].Value = model.Doctor_permisson;
            parameters[5].Value = model.Card_data;
            parameters[6].Value = model.Doctor_tel;
            parameters[7].Value = model.Doctor_add;
            parameters[8].Value = model.Doctor_level;
            parameters[9].Value = model.Doctor_gender;
            parameters[10].Value = model.Doctor_dob;
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
        public bool deleteModel(doctor model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from doctor_table ");
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
        public List<doctor> getModelList(doctor model)
        {
            List<doctor> ModelList = new List<doctor>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from doctor_table ");
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
        public doctor CreateModel(DataRow dr)
        {
            doctor model = new doctor();
            if (dr["_id"] != null && dr["_id"].ToString() != "")
            {
                model.Id = int.Parse(dr["_id"].ToString());
            }
            if (dr["doctor_name"] != null && dr["doctor_name"].ToString() != "")
            {
                model.Doctor_name = dr["doctor_name"].ToString();
            }
            if (dr["doctor_tname"] != null && dr["doctor_tname"].ToString() != "")
            {
                model.Doctor_tname = dr["doctor_tname"].ToString();
            }
            if (dr["doctor_pwd"] != null && dr["doctor_pwd"].ToString() != "")
            {
                model.Doctor_pwd = dr["doctor_pwd"].ToString();
            }
            if (dr["doctor_permisson"] != null && dr["doctor_permisson"].ToString() != "")
            {
                model.Doctor_permisson = int.Parse(dr["doctor_permisson"].ToString());
            }
            if (dr["card_data"] != null && dr["card_data"].ToString() != "")
            {
                model.Card_data = dr["card_data"].ToString();
            }
            if (dr["doctor_tel"] != null && dr["doctor_tel"].ToString() != "")
            {
                model.Doctor_tel = dr["doctor_tel"].ToString();
            }
            if (dr["doctor_add"] != null && dr["doctor_add"].ToString() != "")
            {
                model.Doctor_add = dr["doctor_add"].ToString();
            }
            if (dr["doctor_level"] != null && dr["doctor_level"].ToString() != "")
            {
                model.Doctor_level = dr["doctor_level"].ToString();
            }
            if (dr["doctor_gender"] != null && dr["doctor_gender"].ToString() != "")
            {
                model.Doctor_gender = dr["doctor_gender"].ToString();
            }
            if (dr["doctor_dob"] != null && dr["doctor_dob"].ToString() != "")
            {
                model.Doctor_dob = dr["doctor_dob"].ToString();
            }
            
            return model;
        }
        /// <summary>
		/// 查询一定数量的数据
		/// </summary>
		/// <returns></returns>
        public List<doctor> getModelListTop(doctor model, int top)
		{
            List<doctor> ModelList = new List<doctor>();
			StringBuilder sql = new StringBuilder();
			sql.Append("select ");
            if(top > 0)
                sql.Append(" top " + top.ToString());
            sql.Append("* from doctor_table ");
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
        public int GetRecordCount(doctor model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(1) FROM doctor_table ");
            string where = string.Empty;
            DbParameter[] parameters = CreateWhere(model, out where);
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(" where doctor_name not like 'admin' and " + where);
            }
            else
                sql.Append(" where doctor_name not like 'admin'");

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
        public List<doctor> GetListByPage(doctor model, int startIndex, int endIndex)
        {
            StringBuilder sql = new StringBuilder();
            List<doctor> ModelList = new List<doctor>();
            sql.Append("SELECT * FROM ( ");
            sql.Append(" SELECT ROW_NUMBER() OVER (");
            string where = string.Empty;
            DbParameter[] parameters = CreateWhere(model, out where);

            sql.Append("order by T._id");
            sql.Append(")AS Row, T.*  from doctor_table T ");
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(" where " + where + " and doctor_name not like 'admin'");
            }
            else
                sql.Append("where doctor_name not like 'admin'");

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
