
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public string output;
        public string path;

        public Login()
        {
            InitializeComponent();

        }
        SQLiteConnection m_dbConnection;
        void createNewDatabase()
        {
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
        }
        //创建一个连接到指定数据库
        void connectToDatabase()
        {
            m_dbConnection = new SQLiteConnection("Data Source=RMDB.db3;Version=3;");
            m_dbConnection.Open();
        }
        //在指定数据库中创建一个table
        void createTable()
        {
            string sql = "create table highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        //插入一些数据
        void fillTable()
        {
            string sql = "insert into highscores (name, score) values ('Me', 3000)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            string s = "Me";
            int n = 3000;
            command.CommandText = "INSERT INTO highscores(name, score) VALUES(@name,@score)";
            command.Parameters.Add("name", DbType.String).Value = s;
            command.Parameters.Add("score", DbType.Int32).Value = n;
            command.ExecuteNonQuery();
            s = "MyS";
            n = 6000;

            command.CommandText = "INSERT INTO highscores(name, score) VALUES(@name,@score)";
            command.Parameters.Add("name", DbType.String).Value = s;
            command.Parameters.Add("score", DbType.Int32).Value = n;
            command.ExecuteNonQuery();
            s = "I";
            n = 9000;

            command.CommandText = "INSERT INTO highscores(name, score) VALUES(@name,@score)";
            command.Parameters.Add("name", DbType.String).Value = s;
            command.Parameters.Add("score", DbType.Int32).Value = n;

            command.ExecuteNonQuery();

            //更新
            command.CommandText = "UPDATE highscores SET score=@score111 WHERE name='Me'";
            command.Parameters.Add("score111", DbType.Int32).Value = n;


            //删除
            command.CommandText = "DELETE FROM highscores WHERE score='9001'";
            command.ExecuteNonQuery();
            
        }

        //使用sql查询语句，并显示结果
        void printHighscores()//测试代码
        {
            string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                output = "Name: " + reader["name"] + "\tScore: " + reader["score"] + "\r\n";
                //textBox7.Text += output;

                //Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
                //Console.ReadLine();
            }

            //string path = @"MyDatabase.sqlite";
            //SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            //cn.Open();
            //SQLiteCommand command = cn.CreateCommand();

            //command.CommandText = "SELECT 1 FROM highscores WHERE score='9001' ";
            //SQLiteDataReader sr = command.ExecuteReader();
            //while (sr.Read())
            //{
            //    Console.WriteLine($"{sr.GetString(0)} {sr.GetInt32(1).ToString()}");
            //}
            //sr.Close();


        }



        private void Login_Load(object sender, EventArgs e)
        {           
            //createNewDatabase();
            connectToDatabase();
            //createTable();
            //fillTable();
            //printHighscores();
            path = Directory.GetCurrentDirectory() + @"\RMDB.db3";//后面是数据库名称
           // System.Diagnostics.Process.Start("explorer.exe", path);

        }

        private void Loginbutton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                return;
            int userType = -1;  //密码错误（游客）   
            int uid = 0;
            string sql = string.Format("select UserType,UID from User where Name = '{0}' and PassWord = '{1}'",textBox1.Text,textBox2.Text);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                userType = Convert.ToInt32(reader["UserType"]);
                uid = Convert.ToInt32(reader["UID"]);
            }
            if(userType == -1)
            {
                MessageBox.Show("用户名或密码错误");
            }
            else if(userType == 1)
            {
                Manager form1 = new Manager(uid);
                form1.Show();
            }
            else if(userType == 2)
            {
                User form2 = new User(uid);
                form2.Show();
            }
            
            
        }
        bool theSameName = false;
        private void textBox4_Leave(object sender, EventArgs e)
            //失去焦点时查询用户名
        {
            if (textBox4.Text == "" )
                return;
            string name = "";
            string sql = null;
            
            sql = string.Format("select Name from User where Name = '{0}'", textBox4.Text);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                name = reader["Name"].ToString();
            }
            if(string.Equals(name,textBox4.Text))
            {
                label7.Text = "已存在该用户名";
                theSameName = true;
            }
            else
            {
                theSameName = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label7.Text = "";
        }

        private void regbutton_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "")
                return;
            if (theSameName)
                
                return;
            if(!string.Equals(textBox3.Text,textBox5.Text))
            {
                MessageBox.Show("密码不一致");
                return;
            }

            string sql;
            int uid = 0;
            sql = "select max(UID) from User";//获取UID的最大值，并+1
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                uid = Convert.ToInt32(reader[0]) + 1;
            }
            int userType = 2;
            //插入新建的用户
            sql = string.Format("insert into User VALUES ({0},{1},'{2}',{3},'{4}')",uid,userType,textBox6.Text,textBox5.Text,textBox4.Text);
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            MessageBox.Show("注册成功");
        }
    }
}
