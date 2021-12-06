using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1008team
{
    public class Wbdb
    {
        public List<Team> tlist = new List<Team>();
        public List<Organization> orlist = new List<Organization>();
        private SqlConnection conn;
        private string constr1;
        //private string constr2;

        public Wbdb()
        {
            conn = new SqlConnection();
            constr1 = @"Server=user-pc;database=wb30;uid=ccm;pwd=1234;";
           /* constr2 = @"Server=localhost\SQLEXPRESS;database=
                              master;uid=hsw;pwd=1234";*/

            conn.ConnectionString = constr1;
        }
        #region 연결 해제
        public bool Connect()
        {
            try
            {
                conn.Open();
               
                MessageBox.Show("데이터 배이스 연결성공");
                TeamGetData();
                TeamGetData_listbox();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void DisConnect()
        {
            conn.Close();
        }

        #endregion
        #region 데이터 받아오기
        public void TeamGetData()
        {
            tlist.Clear();
            string comtext = string.Format("select s.sname, t.tname, t.tstartdate, t.tenddate from team t, organization o, student s where o.tid = t.tid and o.snum = s.snum");
            
            SqlCommand command = new SqlCommand(comtext, conn);
            SqlDataReader reader = command.ExecuteReader(); // 2.Select 전용

            while (reader.Read() == true)
            {
                tlist.Add(new Team(reader[0].ToString(),
                                   reader[1].ToString(),
                                   Convert.ToDateTime(reader[2].ToString()),
                                   Convert.ToDateTime(reader[3].ToString())));
            }
            reader.Close();
            command.Dispose();
        }
        public void TeamGetData_listbox()
        {
            tlist.Clear();
            string comtext = string.Format("select s.sname from team t, organization o, student s where o.tid = t.tid and o.snum = s.snum");

            SqlCommand command = new SqlCommand(comtext, conn);
            SqlDataReader reader = command.ExecuteReader(); // 2.Select 전용

            while (reader.Read() == true)
            {
                orlist.Add(new Organization(reader[0].ToString()));
            }
            reader.Close();
            command.Dispose();
        }
        public void TeamGetData_sname(string sname)
        {
            tlist.Clear();
            string comtext = string.Format("select s.sname, t.tname, t.tstartdate, t.tenddate from team t, organization o, student s where o.tid = t.tid and o.snum = s.snum and s.sname='{0}'", sname);


            SqlCommand command = new SqlCommand(comtext, conn);
            SqlDataReader reader = command.ExecuteReader(); // 2.Select 전용

            while (reader.Read() == true)
            {
                tlist.Add(new Team(reader[0].ToString(),
                                   reader[1].ToString(),
                                   Convert.ToDateTime(reader[2].ToString()),
                                   Convert.ToDateTime(reader[3].ToString())));
            }
            reader.Close();
            command.Dispose();
        }
        public void TeamGetData_tname(string tname)
        {
            string comtext = string.Format("select s.sname, t.tname, t.tstartdate, t.tenddate from team t, organization o, student s where o.tid = t.tid and o.snum = s.snum and t.tname='{0}'", tname);


            SqlCommand command = new SqlCommand(comtext, conn);
            SqlDataReader reader = command.ExecuteReader(); // 2.Select 전용

            while (reader.Read() == true)
            {
                tlist.Add(new Team(reader[0].ToString(),
                                   reader[1].ToString(),
                                   Convert.ToDateTime(reader[2].ToString()),
                                   Convert.ToDateTime(reader[3].ToString())));
            }
            reader.Close();
            command.Dispose();
        }
        public bool TeamChange(string tname, string sname)
        {
            string str = string.Format("update organization set tid =(select tid from team where tname ='{0}') where snum =(select snum from student where sname ='{1}')", sname, tname);
            return ExSqlCommand(str);
        }
        #endregion

        private bool ExSqlCommand(string comstr)
        {
            SqlCommand scom = new SqlCommand();
            scom.Connection = conn;
            scom.CommandText = comstr;
            scom.CommandType = System.Data.CommandType.Text;
            //page112
            if (scom.ExecuteNonQuery() > 0)         //1)DDL, I, U, D
            {
                scom.Dispose();
                return true;
            }
            scom.Dispose();
            return false;
        }
    }
}
