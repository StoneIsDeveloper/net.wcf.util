using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo
{
    public static class DatabaseDemo
    {
        public static void RunProcodureTran()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PTR0L8C;database=mis_demo;uid=admin;pwd=admin123");
            int money = 100;
            SqlCommand cmd = new SqlCommand("mon", con); //调用存储过程
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlParameter para = new SqlParameter(); //传递参数
            cmd.Parameters.AddWithValue("@fromID", 1);
            cmd.Parameters.AddWithValue("@toID", 2);
            cmd.Parameters.AddWithValue("@change", money);

            cmd.Parameters.AddWithValue("@return", "").Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            string value = cmd.Parameters["@return"].Value.ToString();//返回值
            if (value == "1")
            {
                Console.WriteLine("secusss...RunProcodureTran");
            }
            else
            {
                Console.WriteLine("failed...RunProcodureTran");
            }
        }

        public static void RunTran()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PTR0L8C;database=mis_demo;uid=admin;pwd=admin123");
            con.Open();
            SqlTransaction tran = con.BeginTransaction();//开启事务
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = tran;
            cmd.Connection = con;
            try
            {
                cmd.CommandText = @"update dbo.UserInfo set  UserId = UserId - " + 100 + " where Id='3' ";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"update dbo.UserInfo set  UserId = UserId - " + 100 + " where Id='4' ";
                cmd.ExecuteNonQuery();

                tran.Commit();//如果两个sql命令都执行成功，则执行commit这个方法，执行这些操作
                Console.WriteLine("success...RunTran");
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed...RunTran");
                Console.WriteLine(ex.Message);
                tran.Rollback(); //回滚
            }
        }
    }
}
