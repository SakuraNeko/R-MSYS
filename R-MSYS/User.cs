using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;
namespace WindowsFormsApp1
{
    public partial class User : Form
    {
        public string output;
        public string path;
        SQLiteConnection m_dbConnection;
        ListView.SelectedIndexCollection indexes;
        ListView.SelectedIndexCollection indexes2;
        ListView.SelectedIndexCollection indexes3;
        int Uid;
        public User(int uid)
        {
            Uid = uid;
            InitializeComponent();
        }
        private void User_Load(object sender, EventArgs e)
        {
            button9.Click += new EventHandler(button1_Click);
            button10.Click += new EventHandler(button1_Click);
            
            m_dbConnection = new SQLiteConnection("Data Source=RMDB.db3;Version=3;");
            m_dbConnection.Open();           

            string sql = "select Name,Price from Food";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // 构建一个ListView的数据，存入数据库数据，以便添加到listView1的行数据中
                ListViewItem lt = new ListViewItem();
                //将数据库数据转变成ListView类型的一行数据      转换成行数据
                lt.Text = (reader["Name"].ToString());
                lt.SubItems.Add(reader["Price"].ToString());
                lt.SubItems.Add("");
                //将lt数据添加到listView1控件中
                listView1.Items.Add(lt);

            }
            /*foreach (ListViewItem item in listView1.Items)
                listView2.Items.Add((ListViewItem)item.Clone());*/
            History_Order();
            string path = Application.StartupPath + @"\icon";
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            pictureBox3.ImageLocation = path + @"\" + Uid + ".jpg";

        }

        private void button1_Click(object sender, EventArgs e)//添加到购物车
        {
            if(indexes==null)
            {
                return;
            }
            string pr = "";
            ListViewItem lt = new ListViewItem();
            int num = 0;
            foreach (int i in indexes)      //
            {
                pr += listView1.Items[i].Text;//
                string temp = listView1.Items[i].SubItems[2].Text;
                if(temp!="")//
                    //int.TryParse(listView1.Items[i].SubItems[2].Text, out num);
                    num = Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                //把选中的列数量的string值转int类型的num(上面是两种写法）

            }
            num = Convert.ToInt32(comboBox1.Text);   //数量改变
            int flag = 0;//标记是否存在列
            for (int i = 0;i<listView2.Items.Count;i++)
                //如果已经存在列，则修改listview2中num的值
            {
                if (String.Equals(listView2.Items[i].Text,pr))
                {
                    flag = 1;
                    listView2.Items[i].SubItems[2].Text = num.ToString();
                    break;
                }
            }
            listView1.Items[indexes[0]].SubItems[2].Text = num.ToString();
            if (flag ==0)
            {
                string sql = "select Name,Price from Food where name = '" + pr + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                  //将数据库数据转变成ListView类型的一行数据      转换成行数据

                    lt.Text = (reader["Name"].ToString());
                    lt.SubItems.Add(reader["Price"].ToString());
                    //将lt数据添加到listView1控件中                
                    lt.SubItems.Add(num.ToString());
                    //修改listview1的数量的值
                    listView2.Items.Add(lt);
                }
                flag = 1;
                SumPrice();
            }
            
        }
        private void button9_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(comboBox1.Text);
            a++;
            comboBox1.Text = a.ToString();
            comboBox1_TextUpdate(sender, e);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(comboBox1.Text);
            if(a>0)
            {
                a--;
                comboBox1.Text = a.ToString();
            }
            comboBox1_TextUpdate(sender, e);
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            comboBox2.Text = comboBox1.Text;
        }

        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            comboBox1.Text = comboBox2.Text;
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = "1";           
            ListView listview = (ListView)sender;
            indexes = listview.SelectedIndices;// 获取选定项的索引          
            string pr = "";

            foreach (int i in indexes)      //遍历获取的listview里
            {
                pr += listview.Items[i].Text;//此时的i等价于indexes[i]
                string temp = listview.Items[i].SubItems[2].Text;

                if (temp != "") { comboBox1.Text = temp; }
            }

            string picId = "";
            string sql = "select FID,Massage from Food where name = '" + pr + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // 构建一个ListView的数据，存入数据库数据，以便添加到listview的行数据中 
                pr = reader["Massage"].ToString();
                picId = reader["FID"].ToString();
            }

            textBox1.Text = pr;// 显示出  选择的行的内容   
            string path = Application.StartupPath + @"\imgfood";
            
            if (System.IO.Directory.Exists(path) == false)
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory(path);
            }
            pictureBox1.ImageLocation = path + @"\"+picId+".jpg";

        }
        bool deleflag = false;
        
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexes2 = listView2.SelectedIndices;// 获取选定项的索引          
            string pr = "";

            foreach (int i in indexes2)      //遍历获取的listview里
            {
                pr += listView2.Items[i].Text;//此时的i等价于indexes2[i]
                string temp = listView2.Items[i].SubItems[2].Text;

                if (temp != "") { comboBox1.Text = temp; comboBox2.Text = temp; }
            }
            string picId = "";
            string sql = "select FID,Massage from Food where name = '" + pr + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string message = null;
            while (reader.Read())
            {
                // 构建一个ListView的数据，存入数据库数据，以便添加到listview的行数据中 
                message = reader["Massage"].ToString();
                picId = reader["FID"].ToString();
            }

            textBox2.Text = message;// 显示出  选择的行的内容   
            string path = Application.StartupPath + @"\imgfood";

            if (System.IO.Directory.Exists(path) == false)
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory(path);
            }
            pictureBox2.ImageLocation = path + @"\" + picId + ".jpg";

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (String.Equals(listView1.Items[i].Text, pr))
                {

                    if (deleflag == true)
                    {
                        listView1.Items[i].SubItems[2].Text = "";
                        deleflag = false;
                    }
                    else
                    {
                        listView1.Items[i].SubItems[2].Text = comboBox2.Text;
                    }



                }
            }
            try
            {
                listView2.Items[indexes2[0]].SubItems[2].Text = comboBox1.Text;
            }
            catch { }

        }
        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexes3 = listView3.SelectedIndices;// 获取选定项的索引          
            string pr = "";

            foreach (int i in indexes3)      //遍历获取的listview里
            {
                pr += listView3.Items[i].Text;//获取订单号
                //string temp = listView2.Items[i].SubItems[2].Text;       
            }
            if(pr == "")
            {
                return;
            }
            int oid = Convert.ToInt32(pr);
            string sql = string.Format("select FIDS,UserEvaluation from OrderList where OID = {0} and UID = {1}",oid,Uid);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string message = null;
            string Fids = null;
            while (reader.Read())
            {
                // 构建一个ListView的数据，存入数据库数据，以便添加到listview的行数据中 
                message = reader["UserEvaluation"].ToString();
                Fids = reader["FIDS"].ToString();
            }

            textBox7.Text = message;// 显示出  选择的行的内容   
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//删除
        {
            if (indexes2 == null)
            {
                return;
            }
            deleflag = true;
            //因为删除时选择集是变为空而不会改变listview2的索引，所以
            listView2_SelectedIndexChanged(sender, e);//先执行
            listView2.Items[indexes2[0]].Remove();  //后移除项
            //也可以把listView2_SIChanged部分代码复制到这里
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (indexes2==null)
            {
                return;
            }
            string pr = "";
            ListViewItem lt = new ListViewItem();
            int num = 0;
            foreach (int i in indexes2)      //
            {
                pr += listView2.Items[i].Text;//
                string temp = listView2.Items[i].SubItems[2].Text;
                if (temp != "")//
                    num = Convert.ToInt32(listView2.Items[i].SubItems[2].Text);
                //把选中的列数量的string值转int类型的num(上面是两种写法）
            }
            num = Convert.ToInt32(comboBox2.Text);   //数量改变            
            for (int i = 0; i < listView1.Items.Count; i++)
            //如果已经存在列，则修改listview2中num的值
            {
                if (String.Equals(listView1.Items[i].Text, pr))
                {
                  
                    listView1.Items[i].SubItems[2].Text = num.ToString();
                    break;
                }
            }
            try {
                listView2.Items[indexes2[0]].SubItems[2].Text = num.ToString();
                SumPrice();
                } catch { }
            

        }
        private void SumPrice()
        {
            int sum = 0;
            for (int i = 0;i<listView2.Items.Count;i++)
            {
                sum += Convert.ToInt32(listView2.Items[i].SubItems[1].Text )* Convert.ToInt32(listView2.Items[i].SubItems[2].Text);
            }
            label7.Text = sum.ToString();
           
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (listView2.Items.Count != 0)
            {
                DialogResult dr = MessageBox.Show("确定支付吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                if (dr == DialogResult.OK)
                {
                    Paymoney();
                    MessageBox.Show("支付成功");
                    History_Order();
                }
                else if (dr == DialogResult.Cancel)
                {
                }
            }
            else
                MessageBox.Show("订单内没有食物");
        }
        private void Paymoney()//订单读入数据库,并修改Day表
        {
            string Oid, Fids = null;
            Random random = new Random();
            string datetime = null;
            string evalu = "good";
            Oid = random.Next(1, 99999).ToString();
            string sql;
            SQLiteCommand command;
            SQLiteDataReader reader;
            for (int i = 0; i < listView2.Items.Count; i++)
            {//获取FIDS集
                sql = string.Format("select FID from Food where Name = '{0}'", listView2.Items[i].Text);
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int n = Convert.ToInt32(listView2.Items[i].SubItems[2].Text);
                    for(int j = 0;j<n;j++)
                    {
                        Fids += reader["FID"].ToString() + ",";
                    }                    
                }

            }
            Fids = Fids.Substring(0, Fids.Length - 1);//去除最后的逗号

            System.DateTime currentTime = new System.DateTime();//获取时间
            currentTime = System.DateTime.Now;
            datetime = currentTime.ToString("d") + " " + currentTime.ToString("t");
            
            sql = string.Format("insert into OrderList VALUES ({0},'{1}','{2}',{3},'{4}','{5}')", Oid, datetime, label7.Text, Uid, Fids, evalu);//修改
            //label7.Text = sql;测试代码
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            //修改Day表
            string date1 = currentTime.ToString("yyyy/MM/dd");//今天的日期
            string date2 = null;          //用string读取数据库中有的日期
            int hour = currentTime.Hour;
            int hour2 = 0;
            int[] a = new int[9];//用数组读取数量
            string sql2 = string.Format("select * from Day where Day = '{0}'", date1);
            //如果不存在，就先插入一条初始化的数据，例如(日期,0,0,0,0,0...)

            sql = string.Format("INSERT INTO Day select '{0}',0,0,0,0,0,0,0,0,0 where not exists({1})", date1,sql2);
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = string.Format("select * from Day where Day = '{0}'",date1);
            command = new SQLiteCommand(sql, m_dbConnection);
            reader = command.ExecuteReader();          
            while (reader.Read())//只读一行
            {
                //date2 = reader["Day"].ToString();
                for(int i = 0;i<9;i++)
                {
                    a[i] = Convert.ToInt32(reader[i + 1].ToString());
                }                
            }
            for (int i = 0; i < listView2.Items.Count; i++)//修改a[]的值
            {
                sql = string.Format("select TID from Food where Name = '{0}'", listView2.Items[i].Text);
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int n = Convert.ToInt32(listView2.Items[i].SubItems[2].Text);
                    int type = Convert.ToInt32(reader["TID"].ToString());
                    a[type+3-1] += n;
                    if(hour>=0&&hour<10)
                    {
                        a[0] += n;
                    }
                    else if(hour>=10&&hour<16)
                    {
                        a[1] += n;
                    }
                    else if(hour>=16&&hour<24)
                    {
                        a[2] += n;
                    }
                }
            }
            //把a[]放回数据库
            sql = string.Format("UPDATE Day SET ShopInBreakfast = {0},ShopInLunch = {1},ShopInDinner = {2},ShopFoodTypeA = {3},ShopFoodTypeB = {4},ShopFoodTypeC = {5},ShopFoodTypeD = {6},ShopFoodTypeE = {7},ShopFoodTypeF = {8} WHERE Day = '{9}'",a[0], a[1], a[2], a[3], a[4], a[5], a[6], a[7], a[8],date1);
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
       

        private void History_Order()
        {
            listView3.Items.Clear();
            string sql;
            SQLiteCommand command;
            SQLiteDataReader reader;
            sql = "select OID,Time,AllPrice from OrderList where UID = "+Uid.ToString();
            command = new SQLiteCommand(sql, m_dbConnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem lt = new ListViewItem();
                //将数据库数据转变成ListView类型的一行数据      转换成行数据
                lt.Text = (reader["OID"].ToString());
                lt.SubItems.Add(reader["Time"].ToString());
                lt.SubItems.Add(reader["AllPrice"].ToString());
                //将lt数据添加到listView1控件中
                listView3.Items.Add(lt);
            }
        }
        private void ShowHistoryOrderDetail()
        {

        }
        private void Write_Evaluation()
        {           
            string sql;
            SQLiteCommand command;
            if (indexes3.Count == 0)
                return;
            string pr = listView3.Items[indexes3[0]].Text;
            
            int oid = Convert.ToInt32(pr);
            sql = string.Format("update OrderList SET UserEvaluation = '{0}' WHERE OID = {1} and UID = {2}",textBox7.Text,oid,Uid);
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listView3.Items.Count != 0)
            {
                DialogResult dr = MessageBox.Show("确定评价吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                if (dr == DialogResult.OK)
                {
                    Write_Evaluation();
                    MessageBox.Show("评价成功");
                }
                else if (dr == DialogResult.Cancel)
                {
                }
            }
            else
                MessageBox.Show("没有订单记录");
        }

        private void button8_Click(object sender, EventArgs e)
            //删除listv3选中行
        {
            if (indexes3 == null)
            {
                return;
            }
            listView3.Items[indexes3[0]].Remove();  //后移除项
        }

        private void button5_Click(object sender, EventArgs e)
            //修改信息 textbox456
        {
            if(textBox4.Text == "")
            {
                MessageBox.Show("密码不能为空");
            }
            else if(!String.Equals(textBox4.Text, textBox5.Text))
            {
                MessageBox.Show("确认密码不一致");
            }
            else if(String.Equals(textBox4.Text, textBox5.Text))
            {
                if(textBox6.Text == "")
                {
                    MessageBox.Show("请输入电话号码");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("是否确定修改", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                    if (dr == DialogResult.OK)
                    {
                        //修改个人部分信息
                        Revise_UserInfo(true,true);
                        MessageBox.Show("修改成功");
                        textBox5.Text = "";
                        textBox6.Text = "";
                    }
                    else if (dr == DialogResult.Cancel)
                    {
                    }
                }
            }
        }
        private void Revise_UserInfo(bool pasw,bool phone1)
        {
            string sql = null;
            SQLiteCommand command;            
            
            if(pasw&&phone1)
            {
                sql = string.Format("update User SET PhoneMun = '{0}',PassWord = '{1}' WHERE UID = {2}", textBox6.Text, textBox5.Text, Uid);
            }   //3个if语句备用
            if(!pasw && phone1)
                sql = string.Format("update User SET PhoneMun = '{0}' WHERE UID = {1}", textBox6.Text, Uid);
            if (pasw && !phone1)
                sql = string.Format("update User SET PassWord = '{0}' WHERE UID = {1}", textBox5.Text, Uid); ;
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要退出吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (dr == DialogResult.OK)
            {
                //退出
                this.Close();
                

            }
            else if (dr == DialogResult.Cancel)
            {
            }
        }
        private void Revise_Day_Table()
        {

        }
    }
}
