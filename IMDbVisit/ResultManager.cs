using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using cn.com.farsight.IM.IMModel;
using System.Data.Common;
using cn.com.farsight.IM.DB.SqlDB;

namespace cn.com.farsight.IM.IMDbVisit
{
    public class ResultManager
    {
        DBHelper dbHelper = DBHelper.getHelper();
        public bool getModel(result model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from result_table ");
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

                if (dt.Tables[0].Rows[0]["ecg_data"] != null && dt.Tables[0].Rows[0]["ecg_data"].ToString() != "")
                {
                    model.Ecg_data = dt.Tables[0].Rows[0]["ecg_data"].ToString();
                }
                if (dt.Tables[0].Rows[0]["ecg_time"] != null && dt.Tables[0].Rows[0]["ecg_time"].ToString() != "")
                {
                    model.Ecg_time = long.Parse(dt.Tables[0].Rows[0]["ecg_time"].ToString());
                }
                if (dt.Tables[0].Rows[0]["ecg_info"] != null && dt.Tables[0].Rows[0]["ecg_info"].ToString() != "")
                {
                    model.Ecg_info = dt.Tables[0].Rows[0]["ecg_info"].ToString();
                }
                if (dt.Tables[0].Rows[0]["hp_data"] != null && dt.Tables[0].Rows[0]["hp_data"].ToString() != "")
                {
                    model.Hp_data = dt.Tables[0].Rows[0]["hp_data"].ToString();
                }
                if (dt.Tables[0].Rows[0]["lp_data"] != null && dt.Tables[0].Rows[0]["lp_data"].ToString() != "")
                {
                    model.Lp_data = dt.Tables[0].Rows[0]["lp_data"].ToString();
                }
                if (dt.Tables[0].Rows[0]["bloodpress_time"] != null && dt.Tables[0].Rows[0]["bloodpress_time"].ToString() != "")
                {
                    model.Bloodpress_time = long.Parse(dt.Tables[0].Rows[0]["bloodpress_time"].ToString());
                }
                if (dt.Tables[0].Rows[0]["bloodpress_info"] != null && dt.Tables[0].Rows[0]["bloodpress_info"].ToString() != "")
                {
                    model.Bloodpress_info = dt.Tables[0].Rows[0]["bloodpress_info"].ToString();
                }
                if (dt.Tables[0].Rows[0]["glucose_data"] != null && dt.Tables[0].Rows[0]["glucose_data"].ToString() != "")
                {
                    model.Glucose_data = dt.Tables[0].Rows[0]["glucose_data"].ToString();
                }
                if (dt.Tables[0].Rows[0]["glucose_time"] != null && dt.Tables[0].Rows[0]["glucose_time"].ToString() != "")
                {
                    model.Glucose_time = long.Parse(dt.Tables[0].Rows[0]["glucose_time"].ToString());
                }
                if (dt.Tables[0].Rows[0]["glucose_info"] != null && dt.Tables[0].Rows[0]["glucose_info"].ToString() != "")
                {
                    model.Glucose_info = dt.Tables[0].Rows[0]["glucose_info"].ToString();
                }
                if (dt.Tables[0].Rows[0]["temperature"] != null && dt.Tables[0].Rows[0]["temperature"].ToString() != "")
                {
                    model.Temperature = dt.Tables[0].Rows[0]["temperature"].ToString();
                }
                if (dt.Tables[0].Rows[0]["temperature_time"] != null && dt.Tables[0].Rows[0]["temperature_time"].ToString() != "")
                {
                    model.Temperature_time = long.Parse(dt.Tables[0].Rows[0]["temperature_time"].ToString());
                }
                if (dt.Tables[0].Rows[0]["temperature_info"] != null && dt.Tables[0].Rows[0]["temperature_info"].ToString() != "")
                {
                    model.Temperature_info = dt.Tables[0].Rows[0]["temperature_info"].ToString();
                }
                if (dt.Tables[0].Rows[0]["date"] != null && dt.Tables[0].Rows[0]["date"].ToString() != "")
                {
                    model.Date = dt.Tables[0].Rows[0]["date"].ToString();
                }
                if (dt.Tables[0].Rows[0]["info"] != null && dt.Tables[0].Rows[0]["info"].ToString() != "")
                {
                    model.Info = dt.Tables[0].Rows[0]["info"].ToString();
                }

                return true;
            }
            return false;
        }

        private DbParameter[] CreateWhere(result model, out string where)
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

            if (model.Ecg_time.HasValue)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }

                sqlwhere.Append(" ecg_time = @ecg_time");
                parameter = new SqlParameter("ecg_time", SqlDbType.Int);
                parameter.Value = model.Ecg_time.Value;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Ecg_info))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" ecg_info like @ecg_info");
                parameter = new SqlParameter("ecg_info", SqlDbType.NText);
                parameter.Value = model.Ecg_info;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Hp_data))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" hp_data like @hp_data");
                parameter = new SqlParameter("hp_data", SqlDbType.NText);
                parameter.Value = model.Hp_data;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Lp_data))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" lp_data like @lp_data");
                parameter = new SqlParameter("lp_data", SqlDbType.NText);
                parameter.Value = model.Lp_data;
                parameterList.Add(parameter);
            }
            if (model.Bloodpress_time.HasValue)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" bloodpress_time = @bloodpress_time");
                parameter = new SqlParameter("bloodpress_time", SqlDbType.Int);
                parameter.Value = model.Bloodpress_time.Value;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Bloodpress_info))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" bloodpress_info like @bloodpress_info");
                parameter = new SqlParameter("bloodpress_info", SqlDbType.NText);
                parameter.Value = model.Bloodpress_info;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Glucose_data))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" glucose_data like @glucose_data");
                parameter = new SqlParameter("glucose_data", SqlDbType.NText);
                parameter.Value = model.Glucose_data;
                parameterList.Add(parameter);
            }
            if (model.Glucose_time.HasValue)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" glucose_time = @glucose_time");
                parameter = new SqlParameter("glucose_time", SqlDbType.Int);
                parameter.Value = model.Glucose_time.Value;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Glucose_info))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" glucose_info like @glucose_info");
                parameter = new SqlParameter("glucose_info", SqlDbType.NText);
                parameter.Value = model.Glucose_info;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Temperature))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" temperature like @temperature");
                parameter = new SqlParameter("temperature", SqlDbType.NText);
                parameter.Value = model.Temperature;
                parameterList.Add(parameter);
            }
            if (model.Temperature_time.HasValue)
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" temperature_time = @temperature_time");
                parameter = new SqlParameter("temperature_time", SqlDbType.Int);
                parameter.Value = model.Temperature_time.Value;
                parameterList.Add(parameter);
            }
            if (!string.IsNullOrEmpty(model.Temperature_info))
            {
                if (sqlwhere.Length > 0)
                {
                    sqlwhere.Append(" and ");
                }
                sqlwhere.Append(" temperature_info like @temperature_info");
                parameter = new SqlParameter("temperature_info", SqlDbType.NText);
                parameter.Value = model.Temperature_info;
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


            where = sqlwhere.ToString();
            return parameterList.ToArray();
        }
        public bool addModel(result model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into result_table(");
            strSql.Append("ecg_data,ecg_time,ecg_info,hp_data,lp_data,bloodpress_time,bloodpress_info,");
            strSql.Append("glucose_data,glucose_time,glucose_info,temperature,temperature_time,temperature_info,date,info)");
            strSql.Append(" values (");
            strSql.Append("@ecg_data,@ecg_time,@ecg_info,@hp_data,@lp_data,@bloodpress_time,@bloodpress_info,@glucose_data,@glucose_time,@glucose_info,@temperature,@temperature_time,@temperature_info,@date,@info)");
            strSql.Append("select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ecg_data", SqlDbType.NText),
					new SqlParameter("@ecg_time", SqlDbType.BigInt),
					new SqlParameter("@ecg_info", SqlDbType.NText),
					new SqlParameter("@hp_data", SqlDbType.NText),
					new SqlParameter("@lp_data", SqlDbType.NText),
					new SqlParameter("@bloodpress_time", SqlDbType.BigInt),
					new SqlParameter("@bloodpress_info", SqlDbType.NText),
					new SqlParameter("@glucose_data", SqlDbType.NText),
					new SqlParameter("@glucose_time", SqlDbType.BigInt),
					new SqlParameter("@glucose_info", SqlDbType.NText),
					new SqlParameter("@temperature", SqlDbType.NText),
					new SqlParameter("@temperature_time", SqlDbType.BigInt),
					new SqlParameter("@temperature_info", SqlDbType.NText),
					new SqlParameter("@date", SqlDbType.NText),
					new SqlParameter("@info", SqlDbType.NText)};

            parameters[0].Value = model.Ecg_data;
            parameters[1].Value = model.Ecg_time;
            parameters[2].Value = model.Ecg_info;
            parameters[3].Value = model.Hp_data;
            parameters[4].Value = model.Lp_data;
            parameters[5].Value = model.Bloodpress_time;
            parameters[6].Value = model.Bloodpress_info;
            parameters[7].Value = model.Glucose_data;
            parameters[8].Value = model.Glucose_time;
            parameters[9].Value = model.Glucose_info;
            parameters[10].Value = model.Temperature;
            parameters[11].Value = model.Temperature_time;
            parameters[12].Value = model.Temperature_info;
            parameters[13].Value = model.Date;
            parameters[14].Value = model.Info;
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
        public bool updateModel(result model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update result_table set ");
            strSql.Append("ecg_data=@ecg_data,");
            strSql.Append("ecg_time=@ecg_time,");
            strSql.Append("ecg_info=@ecg_info,");
            strSql.Append("hp_data=@hp_data,");
            strSql.Append("lp_data=@lp_data,");
            strSql.Append("bloodpress_time=@bloodpress_time,");
            strSql.Append("bloodpress_info=@bloodpress_info,");
            strSql.Append("glucose_data=@glucose_data,");
            strSql.Append("glucose_time=@glucose_time,");
            strSql.Append("glucose_info=@glucose_info,");
            strSql.Append("temperature=@temperature,");
            strSql.Append("temperature_time=@temperature_time,");
            strSql.Append("temperature_info=@temperature_info,");
            strSql.Append("date=@date,");
            strSql.Append("info=@info");
            strSql.Append(" where _id = @_id");
            SqlParameter[] parameters = {
					new SqlParameter("@_id", SqlDbType.Int),
					new SqlParameter("@ecg_data", SqlDbType.NText),
					new SqlParameter("@ecg_time", SqlDbType.BigInt),
					new SqlParameter("@ecg_info", SqlDbType.NText),
					new SqlParameter("@hp_data", SqlDbType.NText),
					new SqlParameter("@lp_data", SqlDbType.NText),
					new SqlParameter("@bloodpress_time", SqlDbType.BigInt),
					new SqlParameter("@bloodpress_info", SqlDbType.NText),
					new SqlParameter("@glucose_data", SqlDbType.NText),
					new SqlParameter("@glucose_time", SqlDbType.BigInt),
					new SqlParameter("@glucose_info", SqlDbType.NText),
					new SqlParameter("@temperature", SqlDbType.NText),
					new SqlParameter("@temperature_time", SqlDbType.BigInt),
					new SqlParameter("@temperature_info", SqlDbType.NText),
					new SqlParameter("@date", SqlDbType.NText),
					new SqlParameter("@info", SqlDbType.NText)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Ecg_data;
            parameters[2].Value = model.Ecg_time;
            parameters[3].Value = model.Ecg_info;
            parameters[4].Value = model.Hp_data;
            parameters[5].Value = model.Lp_data;
            parameters[6].Value = model.Bloodpress_time;
            parameters[7].Value = model.Bloodpress_info;
            parameters[8].Value = model.Glucose_data;
            parameters[9].Value = model.Glucose_time;
            parameters[10].Value = model.Glucose_info;
            parameters[11].Value = model.Temperature;
            parameters[12].Value = model.Temperature_time;
            parameters[13].Value = model.Temperature_info;
            parameters[14].Value = model.Date;
            parameters[15].Value = model.Info;
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
        public bool deleteModel(result model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from result_table ");
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
        public List<result> getModelList(result model)
        {
            List<result> ModelList = new List<result>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from result_table ");
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
        public result CreateModel(DataRow dr)
        {
            result model = new result();
            if (dr["_id"] != null && dr["_id"].ToString() != "")
            {
                model.Id = int.Parse(dr["_id"].ToString());
            }

            if (dr["ecg_data"] != null && dr["ecg_data"].ToString() != "")
            {
                model.Ecg_data = dr["ecg_data"].ToString();
            }
            if (dr["ecg_time"] != null && dr["ecg_time"].ToString() != "")
            {
                model.Ecg_time = long.Parse(dr["ecg_time"].ToString());
            }
            if (dr["ecg_info"] != null && dr["ecg_info"].ToString() != "")
            {
                model.Ecg_info = dr["ecg_info"].ToString();
            }
            if (dr["hp_data"] != null && dr["hp_data"].ToString() != "")
            {
                model.Hp_data = dr["hp_data"].ToString();
            }
            if (dr["lp_data"] != null && dr["lp_data"].ToString() != "")
            {
                model.Lp_data = dr["lp_data"].ToString();
            }
            if (dr["bloodpress_time"] != null && dr["bloodpress_time"].ToString() != "")
            {
                model.Bloodpress_time = long.Parse(dr["bloodpress_time"].ToString());
            }
            if (dr["bloodpress_info"] != null && dr["bloodpress_info"].ToString() != "")
            {
                model.Bloodpress_info = dr["bloodpress_info"].ToString();
            }
            if (dr["glucose_data"] != null && dr["glucose_data"].ToString() != "")
            {
                model.Glucose_data = dr["glucose_data"].ToString();
            }
            if (dr["glucose_time"] != null && dr["glucose_time"].ToString() != "")
            {
                model.Glucose_time = long.Parse(dr["glucose_time"].ToString());
            }
            if (dr["glucose_info"] != null && dr["glucose_info"].ToString() != "")
            {
                model.Glucose_info = dr["glucose_info"].ToString();
            }
            if (dr["temperature"] != null && dr["temperature"].ToString() != "")
            {
                model.Temperature = dr["temperature"].ToString();
            }
            if (dr["temperature_time"] != null && dr["temperature_time"].ToString() != "")
            {
                model.Temperature_time = long.Parse(dr["temperature_time"].ToString());
            }
            if (dr["temperature_info"] != null && dr["temperature_info"].ToString() != "")
            {
                model.Temperature_info = dr["temperature_info"].ToString();
            }
            if (dr["date"] != null && dr["date"].ToString() != "")
            {
                model.Date = dr["date"].ToString();
            }
            if (dr["info"] != null && dr["info"].ToString() != "")
            {
                model.Info = dr["info"].ToString();
            }

            return model;
        }
        /// <summary>
        /// 查询一定数量的数据
        /// </summary>
        /// <returns></returns>
        public List<result> getModelListTop(result model, int top)
        {
            List<result> ModelList = new List<result>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select ");
            if (top > 0)
                sql.Append(" top " + top.ToString());
            sql.Append("* from result_table ");
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
    }
}
