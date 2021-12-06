using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1008team
{
    public partial class Form1 : Form
    {
        private Wbdb db = new Wbdb();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            db.Connect();
           // TeamPrintAll();
            TeamPrintAll_list();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            

        }//테이블(조/조편성) 콤보박스 선택
        #region 전체출력

        public void TeamPrintAll()
        {
            listView1.Items.Clear();
            foreach (Team t in db.tlist)
            {
                string sname = t.Sname;
                string tname = t.Tname;
                DateTime sdate = t.Tstartdate;
                DateTime edate = t.Tenddate;

                string[] strs = new string[] {sname, tname, sdate.ToString(), edate.ToString()};
                ListViewItem lvi = new ListViewItem(strs);

                listView1.Items.Add(lvi);
            }
        }
        public void TeamPrintAll_list()
        {
            foreach (Organization t in db.orlist)
            {
                listBox2.Items.Add(t.Sname);
            }
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)//검색버튼
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "학생이름":
                    listView1.Items.Clear();
                    string sname1 = textBox2.Text;
                    db.TeamGetData_sname(sname1);
                    foreach (Team t in db.tlist)
                    {
                        string sname = t.Sname;
                        string tname = t.Tname;
                        DateTime sdate = t.Tstartdate;
                        DateTime edate = t.Tenddate;

                        string[] strs = new string[] { sname, tname, sdate.ToString(), edate.ToString() };
                        ListViewItem lvi = new ListViewItem(strs);
                       
                        listView1.Items.Add(lvi);
                    }
                    MessageBox.Show("학생이름 검색입니다.");
                    break;
                case "조이름":
                    listView1.Items.Clear();
                    string tname1 = textBox2.Text;
                    db.TeamGetData_tname(tname1);
                    foreach (Team t in db.tlist)
                    {
                        string sname = t.Sname;
                        string tname = t.Tname;
                        DateTime sdate = t.Tstartdate;
                        DateTime edate = t.Tenddate;

                        string[] strs = new string[] { sname, tname, sdate.ToString(), edate.ToString() };
                        ListViewItem lvi = new ListViewItem(strs);
                       
                        listView1.Items.Add(lvi);
                    }
                    MessageBox.Show("조이름 검색입니다.");
                    break;
                 default: MessageBox.Show("검색 종류를 선택하세요"); break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            db.TeamGetData();
            foreach (Team t in db.tlist)
            {
                string sname = t.Sname;
                string tname = t.Tname;
                DateTime sdate = t.Tstartdate;
                DateTime edate = t.Tenddate;

                string[] strs = new string[] { sname, tname, sdate.ToString(), edate.ToString() };
                ListViewItem lvi = new ListViewItem(strs);

                listView1.Items.Add(lvi);
            }
        }

        private void button4_Click(object sender, EventArgs e) //조 투입
        {
            string sname = listBox2.SelectedItem.ToString();
            string tname = comboBox2.SelectedItem.ToString();
            if (db.TeamChange(sname, tname) == true)
            {
                foreach (Organization t in db.orlist)
                {
                    listBox3.Items.Add(t.Sname);
                }
                MessageBox.Show("저장 성공");
            }
            else
            {
                MessageBox.Show("저장 오류");
            }
            
        }
    }
}
