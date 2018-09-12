using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImport
{
    public class DataImportHelper
    {
        public static string Conn = System.Configuration.ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;

        public static int ExecuteNonQuery(string sql, params DbParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.CommandType = System.Data.CommandType.Text;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string sql, params DbParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                command.CommandType = System.Data.CommandType.Text;
                return command.ExecuteScalar();
            }
        }

        public static DataTable ExecuteQuery(string sql, params DbParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.CommandType = System.Data.CommandType.Text;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static int ExecuteProcedure(string procedureName, params DbParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(procedureName, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteNonQuery();
            }
        }

        public static void BulkInsert(DataTable data, string destinationTable)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlBulkCopy copy = new SqlBulkCopy(conn);
                foreach (DataColumn c in data.Columns)
                {
                    copy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(c.ColumnName, c.ColumnName));
                }
                copy.DestinationTableName = destinationTable;
                copy.WriteToServer(data);
            }
        }


        public static object ExecuteReader(DataTable table, string sql, params DbParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.CommandType = System.Data.CommandType.Text;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                SqlDataReader reder = command.ExecuteReader();
                while (reder.Read())
                {
                    DataRow dr = table.NewRow();
                    dr["ID"] = reder["ClientID"].ToString();
                    dr["Assets"] = Convert.ToDouble(reder["amount"].ToString());
                    table.Rows.Add(dr);
                }
                return table;
            }
        }

        /// <summary>
        /// 通过临时表的方式，批量更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="destinationTable"></param>
        public static void TempTableBulkInsert(DataTable data, string destinationTable)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                //1.创建临时表
                string sqlDrop = "if object_id('tmpTable') is not null begin drop table tmpTable end;";
                string sql = "if object_id('tmpTable') is not null begin drop table tmpTable end; create table tmpTable(ID nvarchar(128) NOT NULL, Assets float NOT NULL,)";

                SqlCommand commandS = new SqlCommand(sql, conn);
                commandS.CommandType = System.Data.CommandType.Text;
                commandS.ExecuteNonQuery();
                //2.数据批量插入到临时表中
                SqlBulkCopy copy = new SqlBulkCopy(conn);
                foreach (DataColumn c in data.Columns)
                {
                    copy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(c.ColumnName, c.ColumnName));
                }
                copy.DestinationTableName = "tmpTable";
                copy.WriteToServer(data);

                //3.临时表批量跟新到客户表中
                string updateSql = "update dbo.ClientInfoes set Assets = t.Assets from tmpTable t inner join dbo.ClientInfoes c on t.ID = c.ID;";
                SqlCommand command = new SqlCommand(updateSql, conn);
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
                //4.移除临时表
                SqlCommand commandT = new SqlCommand(sqlDrop, conn);
                commandT.CommandType = System.Data.CommandType.Text;
                commandT.ExecuteNonQuery();
            }
        }

    }
}
