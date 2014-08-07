using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;

/// <summary>
/// Summary description for DB
/// </summary>
namespace AJ.UtiliTools
{
    public class DB
    {
        public static String ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[UtiliSetting.AppSetting("DefaultConnection")].ConnectionString; }
        }

        public static SqlConnection GetConnection()
        {
            return GetConnection(ConnectionString);
        }

        public static SqlConnection GetConnection(String connection)
        {
            return new SqlConnection(connection);
        }

        #region GetDS

        public static DataSet GetDS(String sql)
        {
            return GetDS(sql, ConnectionString);
        }

        public static DataSet GetDS(String sql, params SqlParameter[] prams)
        {
            return GetDS(sql, ConnectionString, prams);
        }

        public static DataSet GetDS(String sql, String connection)
        {
            return GetDS(sql, connection, new SqlParameter[0]);
        }

        public static DataSet GetDS(String sql, String connection, params SqlParameter[] prams)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    foreach (SqlParameter pram in prams)
                    {
                        cmd.Parameters.Add(pram);
                    }

                    using (SqlDataAdapter adapt = new SqlDataAdapter(cmd))
                    {
                        adapt.Fill(ds);
                    }
                }
            }

            return ds;
        }

        #endregion

        #region GetDT

        public static DataTable GetDT(String sql)
        {
            return GetDT(sql, ConnectionString);
        }

        public static DataTable GetDT(String sql, params SqlParameter[] prams)
        {
            return GetDT(sql, ConnectionString, prams);
        }

        public static DataTable GetDT(String sql, String connection)
        {
            return GetDT(sql, connection, new SqlParameter[0]);
        }

        public static DataTable GetDT(String sql, String connection, params SqlParameter[] prams)
        {
            DataSet ds = GetDS(sql, connection, prams);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return new DataTable();
        }

        #endregion

        #region GetReader

        public static SqlDataReader GetReader(String sql)
        {
            return GetReader(sql, ConnectionString, new SqlParameter[0]);
        }

        public static SqlDataReader GetReader(String sql, params SqlParameter[] prams)
        {
            return GetReader(sql, ConnectionString, prams);
        }

        public static SqlDataReader GetReader(String sql, String connection)
        {
            return GetReader(sql, connection, new SqlParameter[0]);
        }

        public static SqlDataReader GetReader(String sql, String connection, params SqlParameter[] prams)
        {
            SqlConnection con = new SqlConnection(connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);

            foreach (SqlParameter pram in prams)
            {
                cmd.Parameters.Add(pram);
            }

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #endregion

        #region ExecuteScalar

        public static T ExecuteScalar<T>(String sql)
        {
            return ExecuteScalar<T>(sql, ConnectionString, new SqlParameter[0]);
        }

        public static T ExecuteScalar<T>(String sql, params SqlParameter[] prams)
        {
            return ExecuteScalar<T>(sql, ConnectionString, prams);
        }

        public static T ExecuteScalar<T>(String sql, String connection)
        {
            return ExecuteScalar<T>(sql, connection, new SqlParameter[0]);
        }

        public static T ExecuteScalar<T>(String sql, String connection, params SqlParameter[] prams)
        {
            T value;
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    foreach (SqlParameter pram in prams)
                    {
                        cmd.Parameters.Add(pram);
                    }

                    value = Helper.Parse<T>(cmd.ExecuteScalar());
                }
            }

            return value;
        }

        #endregion

        #region ExecuteSQL

        public static int ExecuteSQL(String sql)
        {
            return ExecuteSQL(sql, ConnectionString, new SqlParameter[0]);
        }

        public static int ExecuteSQL(String sql, params SqlParameter[] prams)
        {
            return ExecuteSQL(sql, ConnectionString, prams);
        }

        public static int ExecuteSQL(String sql, String connection)
        {
            return ExecuteSQL(sql, connection, new SqlParameter[0]);
        }

        public static int ExecuteSQL(String sql, String connection, params SqlParameter[] prams)
        {
            int returnVal = 0;

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    foreach (SqlParameter pram in prams)
                    {
                        cmd.Parameters.Add(pram);
                    }

                    try
                    {

                        returnVal = cmd.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            return returnVal;
        }

        #endregion

        public static T ReaderField<T>(SqlDataReader reader, String fieldname)
        {
            try
            {
                int idx = reader.GetOrdinal(fieldname);
                if (reader.IsDBNull(idx))
                {
                    return (T)default(T);
                }
                else
                {
                    object o = reader.GetValue(idx);

                    try
                    {
                        return (T)Convert.ChangeType(o, typeof(T));
                    }
                    catch
                    {
                        return (T)default(T);
                    }
                }
            }
            catch { }

            return (T)default(T);
        }

        public static bool IsFieldNull(SqlDataReader reader, String fieldname)
        {
            int idx = reader.GetOrdinal(fieldname);
            if (reader.IsDBNull(idx))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static String Quote(String s)
        {
            StringBuilder value = new StringBuilder();
            value.Append("N'");
            value.Append(s.Replace("'", "''"));
            value.Append("'");
            return value.ToString();
        }
    }
}