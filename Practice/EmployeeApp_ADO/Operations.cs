using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ques1_Employee
{
    class Operations
    {
        public SqlConnection getConn()
        {
            SqlConnection sqlconn = new SqlConnection();
            string connstring = ConfigurationManager.ConnectionStrings["EmpDBConn"].ConnectionString;
            sqlconn.ConnectionString = connstring;
            return sqlconn;
        }
        public int insertData(Employee e)
        {
            SqlConnection scon = getConn();
            scon.Open();
            int records = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("insert into Employee(Name,Salary) Values(@name,@salary);",scon);
                //cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = e.ID;
                cmd.Parameters.AddWithValue("@name", SqlDbType.NVarChar).Value = e.Name;
                cmd.Parameters.AddWithValue("@salary", SqlDbType.Decimal).Value = e.Salary;
                records = cmd.ExecuteNonQuery();

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            finally
            {
                scon.Close();
            }
            return records;
        }

        public Employee Search(int id)
        {
            SqlConnection scon = getConn();
            scon.Open();
            Employee e = null;
            try
            {

                SqlCommand cmd = new SqlCommand("select * from Employee where id=@id", scon);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    e = new Employee();
                    e.ID = Convert.ToInt32(rd["Id"]);
                    e.Name = rd["Name"].ToString();
                    e.Salary = Convert.ToInt64(rd["Salary"]);
                    break;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            finally
            {
                scon.Close();
            }
            return e;
        }

        public List<Employee> Search(string name)
        {
            SqlConnection scon = getConn();
            scon.Open();
            List<Employee> eList = new List<Employee>();
            //Employee e = new Employee();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Employee where Name=@name", scon);
                cmd.Parameters.AddWithValue("@name", name);
                SqlDataReader rd = cmd.ExecuteReader();
                
                while (rd.Read())
                {
                    Employee e = new Employee();
                    e.ID = Convert.ToInt32(rd["Id"]);
                    e.Name = rd["Name"].ToString();
                    e.Salary = Convert.ToInt64(rd["Salary"]);
                    eList.Add(e);
                }
            }
            catch (SqlException ee)
            { 
                Console.WriteLine(ee.Message); }
            finally
            {
                scon.Close();
            }
            return eList;
        }
    }

}
