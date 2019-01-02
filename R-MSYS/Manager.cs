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
using System.Text.RegularExpressions;
using System.Web;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;

namespace WindowsFormsApp1
{
    public partial class Manager : Form
    {
        private SQLiteConnection m_dbConnection;
        private string tmpFoodName = "";

        private string date1 = "";

        int Uid;
        public Manager(int uid)
        {
            Uid = uid;
            InitializeComponent();
        }
        public Manager()
        {
            InitializeComponent();
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            m_dbConnection = new SQLiteConnection("Data Source=RMDB.db3;Version=3;");
            m_dbConnection.Open();


            /***********************************加载信息至菜品预览************************************/
            this.listView2.View = View.LargeIcon;
            this.listView2.BeginUpdate();

            string sql = "select FID,Name from Food";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // 构建一个ListView的数据，存入数据库数据，以便添加到listView1的行数据中
                ListViewItem lt = new ListViewItem();


                //将数据库数据转变成ListView类型的一行数据      转换成行数据
                lt.Text = (reader["Name"].ToString());


                string picId = reader["FID"].ToString();

                string path = Application.StartupPath + @"\imgfood";

                if (System.IO.Directory.Exists(path) == false)
                {
                    //创建pic文件夹
                    System.IO.Directory.CreateDirectory(path);
                }
                string dishPath = path + @"\" + picId + ".jpg";
                this.imageList1.Images.Add(Image.FromFile(dishPath));

                this.listView2.LargeImageList = this.imageList1;

                lt.ImageIndex = int.Parse(picId) - 1;

                this.listView2.Items.Add(lt);
            }

            this.listView2.EndUpdate();



            /******************************加载信息至菜品管理**************************************/
            this.listView1.View = View.LargeIcon;
            this.listView1.BeginUpdate();

            sql = "select FID,Name from Food";
            command = new SQLiteCommand(sql, m_dbConnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                // 构建一个ListView的数据，存入数据库数据，以便添加到listView1的行数据中
                ListViewItem lt = new ListViewItem();


                //将数据库数据转变成ListView类型的一行数据      转换成行数据
                lt.Text = (reader["Name"].ToString());


                string picId = reader["FID"].ToString();

                string path = Application.StartupPath + @"\imgfood";

                if (System.IO.Directory.Exists(path) == false)
                {
                    //创建pic文件夹
                    System.IO.Directory.CreateDirectory(path);
                }
                string dishPath = path + @"\" + picId + ".jpg";
                this.imageList2.Images.Add(Image.FromFile(dishPath));

                this.listView1.LargeImageList = this.imageList2;

                lt.ImageIndex = int.Parse(picId) - 1;

                this.listView1.Items.Add(lt);
            }

            this.listView1.EndUpdate();

            /**********************************************************************/
            m_dbConnection = new SQLiteConnection("Data Source=RMDB.db3;Version=3;");
            m_dbConnection.Open();


            //加载数据至DataGridView
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dateTimePicker1.CustomFormat = "yyyy/MM/dd";

            date1 = dateTimePicker1.Text.ToString();


            m_dbConnection = new SQLiteConnection("Data Source=RMDB.db3;Version=3;");
            m_dbConnection.Open();
            sql = "SELECT * FROM Day  ";
            command = new SQLiteCommand(sql, m_dbConnection);


            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.dataGridView1.DataSource = ds.Tables[0];

        }
        

        //菜品预览
        private void listView2_MouseMove(object sender, MouseEventArgs e)
        {
            
            ListViewItem item = this.listView2.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                //通过text,连接数据库查询该项的所有信息

                string pr = "";

                tmpFoodName = item.Text;
                
                string sql = "select* from Food where name = '" + item.Text + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pr = reader["Massage"].ToString();

                }
                
                toolTip1.Show(pr, listView2, new Point(e.X + 15, e.Y + 15), 10000);
                toolTip1.Active = true;
            }
            else
            {
                toolTip1.Active = false;
            }
        }

        //菜品管理
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ListViewItem item = this.listView1.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                string sql = "select* from Food where Name = '" + item.Text + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    textBox1.Text = reader["FID"].ToString();
                    textBox2.Text = reader["Name"].ToString();
                    textBox3.Text = reader["TID"].ToString();
                    textBox7.Text = reader["Price"].ToString();
                    textBox8.Text = reader["Massage"].ToString();
                    
                    string picId = reader["FID"].ToString();

                    string path = Application.StartupPath + @"\imgfood";

                    if (System.IO.Directory.Exists(path) == false)
                    {
                        //创建pic文件夹
                        System.IO.Directory.CreateDirectory(path);
                    }
                    string dishPath = path + @"\" + picId + ".jpg";
                    pictureBox1.ImageLocation = dishPath;
                }
                
            }
        }

        //选择图片
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(fileDialog.FileName);//获取绝对路径下的图片
            }
        }

        //更改菜品信息
        private void button3_Click(object sender, EventArgs e)
        {
            SQLiteCommand command = new SQLiteCommand(null, m_dbConnection);
            //更新

            command.CommandText = "UPDATE Food SET FID=@FID,Name=@Name,TID=@TID,Price=@Price,Massage=@Massage WHERE Name='" + tmpFoodName + "'";
            command.Parameters.Add("FID", DbType.Int32).Value = int.Parse(textBox1.Text);
            command.Parameters.Add("Name", DbType.String).Value = textBox2.Text;
            command.Parameters.Add("TID", DbType.Int32).Value = int.Parse(textBox3.Text);
            command.Parameters.Add("Price", DbType.Int32).Value = int.Parse(textBox7.Text);
            command.Parameters.Add("Massage", DbType.String).Value = textBox8.Text;
            command.ExecuteNonQuery();

            //当点击添加、更改按钮
            //将获取到的图片另存为目标文件夹，根据菜名序号重命名，更新当前picturebox的图片加载路径
            string picId = textBox1.Text;

            string path1 = Application.StartupPath + @"\imgfood";
            string dishPath = path1 + @"\" + picId + ".jpg";

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = dishPath;


            if (pictureBox1.Image != null)
            {

                string pictureName = sfd.FileName;
                //照片另存
                using (MemoryStream mem = new MemoryStream())
                {
                    //这句很重要，不然不能正确保存图片或出错（关键就这一句）
                    Bitmap bmp = new Bitmap(pictureBox1.Image);
                    //保存到磁盘文件
                    bmp.Save(@pictureName, pictureBox1.Image.RawFormat);
                    bmp.Dispose();
                    MessageBox.Show("附件另存成功！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            //更新ListView控件
            this.Invalidate();

        }

        //添加菜品
        private void button2_Click(object sender, EventArgs e)
        {
            SQLiteCommand command = new SQLiteCommand(null, m_dbConnection);

            command.CommandText = "INSERT INTO Food(FID,Name,TID,Price,Massage) VALUES(@FID,@Name,@TID,@Price,@Massage)";
            command.Parameters.Add("FID", DbType.Int32).Value = int.Parse(textBox1.Text);
            command.Parameters.Add("Name", DbType.String).Value = textBox2.Text;
            command.Parameters.Add("TID", DbType.Int32).Value = int.Parse(textBox3.Text);
            command.Parameters.Add("Price", DbType.Int32).Value = int.Parse(textBox7.Text);
            command.Parameters.Add("Massage", DbType.String).Value = textBox8.Text;
            command.ExecuteNonQuery();

            string picId = textBox1.Text;

            string path1 = Application.StartupPath + @"\imgfood";
            string dishPath = path1 + @"\" + picId + ".jpg";

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = dishPath;


            if (pictureBox1.Image != null)
            {

                string pictureName = sfd.FileName;
                //照片另存
                using (MemoryStream mem = new MemoryStream())
                {
                    //这句很重要，不然不能正确保存图片或出错（关键就这一句）
                    Bitmap bmp = new Bitmap(pictureBox1.Image);
                    //保存到磁盘文件
                    bmp.Save(@pictureName, pictureBox1.Image.RawFormat);
                    bmp.Dispose();
                    MessageBox.Show("附件另存成功！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            //更新ListView控件
            this.Invalidate();

        }

        //删除菜品
        private void button4_Click(object sender, EventArgs e)
        {
            SQLiteCommand command = new SQLiteCommand(null, m_dbConnection);
            //更新
            
            command.CommandText = "DELETE FROM Food WHERE Name='" + tmpFoodName + "'";
            
            command.ExecuteNonQuery();

            string picId = textBox1.Text;

            string path1 = Application.StartupPath + @"\imgfood";
            string dishPath = path1 + @"\" + picId + ".jpg";

            System.IO.FileInfo file = new System.IO.FileInfo(dishPath);
            if (file.Exists)//文件是否存在，存在则执行删除  
            {
                file.Delete();
                
            }

            //更新ListView控件、其他控件
            this.Invalidate();
        }

        private void Revise_UserInfo(bool pasw, bool phone1)
        {
            string sql = null;
            SQLiteCommand command;

            if (pasw && phone1)
            {
                sql = string.Format("update User SET PhoneMun = '{0}',PassWord = '{1}' WHERE UID = {2}", textBox6.Text, textBox5.Text, Uid);
            }   //3个if语句备用
            if (!pasw && phone1)
                sql = string.Format("update User SET PhoneMun = '{0}' WHERE UID = {1}", textBox6.Text, Uid);
            if (pasw && !phone1)
                sql = string.Format("update User SET PassWord = '{0}' WHERE UID = {1}", textBox5.Text, Uid); ;
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //修改管理员的信息
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("密码不能为空");
            }
            else if (!String.Equals(textBox4.Text, textBox5.Text))
            {
                MessageBox.Show("确认密码不一致");
            }
            else if (String.Equals(textBox4.Text, textBox5.Text))
            {
                if (textBox6.Text == "")
                {
                    MessageBox.Show("请输入电话号码");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("是否确定修改", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                    if (dr == DialogResult.OK)
                    {
                        //修改个人部分信息
                        Revise_UserInfo(true, true);
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


        //选着时间段
        private void button7_Click(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dateTimePicker1.CustomFormat = "yyyy/MM/dd";
            date1 = dateTimePicker1.Text.ToString();
            
            this.Invalidate();

        }

        //每天的不同时段销售情况
        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {

            m_dbConnection = new SQLiteConnection("Data Source=RMDB.db3;Version=3;");
            m_dbConnection.Open();


            //加载数据至DataGridView
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dateTimePicker1.CustomFormat = "yyyy/MM/dd";

            date1 = dateTimePicker1.Text.ToString();


            string sql = "SELECT ShopInBreakFast,ShopInLunch,ShopInDinner FROM Day WHERE Day ='" + date1 + "'";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            string breakfast="", lunch="", dinner="";

            while (reader.Read())
            {
                
                breakfast= reader["ShopInBreakFast"].ToString();
                lunch = reader["ShopInLunch"].ToString();
                dinner = reader["ShopInDinner"].ToString();

            }

            int[] saleNum = { int.Parse(breakfast), int.Parse(lunch), int.Parse(dinner) }; 
            

            //获取总销量和各月分别销量
            int sum = 0, threeNum = 0, fourNum = 0, fiveNum = 0;
            for (int i = 0; i < saleNum.Length; i++)
            {
                sum += saleNum[i];
                if (i == 0)
                    threeNum = saleNum[0];
                else if (i == 1)
                    fourNum = saleNum[1];
                else
                    fiveNum = saleNum[2];
            }


            //创建画图对象
            int width = 400, height = 450;

            Graphics g = e.Graphics;


            //清空背景色
            g.Clear(Color.White);

            Pen pen1 = new Pen(Color.Red);                  //实例化Pen类

            //创建4个Brush对象用于设置颜色
            Brush brush1 = new SolidBrush(Color.PowderBlue);
            Brush brush2 = new SolidBrush(Color.Blue);
            Brush brush3 = new SolidBrush(Color.Wheat);
            Brush brush4 = new SolidBrush(Color.Orange);
            Brush brush5 = new SolidBrush(Color.Black);

            //创建两个Font对象用于设置字体
            Font font1 = new Font("Courier New", 16, FontStyle.Bold);
            Font font2 = new Font("Courier New", 10);

            //绘制背景图
            g.FillRectangle(brush1, 0, 0, width, height);
            g.DrawString("每天不同时段销量占比饼形图", font1, brush5, new Point(70, 20));//书写标题
            int piex = 100, piey = 60, piew = 200, pieh = 200;
            
            float angle1 = Convert.ToSingle((360 / Convert.ToSingle(sum)) * Convert.ToSingle(threeNum));
            
            float angle2 = Convert.ToSingle((360 / Convert.ToSingle(sum)) * Convert.ToSingle(fourNum));
            
            float angle3 = Convert.ToSingle((360 / Convert.ToSingle(sum)) * Convert.ToSingle(fiveNum));
            g.FillPie(brush2, piex, piey, piew, pieh, 0, angle1);		//绘制3月份销量所占比例
            g.FillPie(brush3, piex, piey, piew, pieh, angle1, angle2);//绘制4月份销量所占比例

            //绘制晚餐销量所占比例
            g.FillPie(brush4, piex, piey, piew, pieh, angle1 + angle2, angle3);

           

            //绘制标识
            g.DrawRectangle(pen1, 20, 300, 360, 50);				//绘制范围框
            g.FillRectangle(brush2, 30, 320, 20, 10);				//绘制小矩形
            g.DrawString(string.Format("早餐销量"), font2, brush5, 60, 320);
            g.FillRectangle(brush3, 150, 320, 20, 10);
            g.DrawString(string.Format("中餐销量"), font2, brush5, 180, 320);
            g.FillRectangle(brush4, 270, 320, 20, 10);
            g.DrawString(string.Format("晚餐销量"), font2, brush5, 300, 320);

            g.DrawString(string.Format("{0:P2}", Convert.ToSingle(threeNum) / Convert.ToSingle(sum)), font2, brush5, piex + 130, piey + 130);
            g.DrawString(string.Format("{0:P2}", Convert.ToSingle(fourNum) / Convert.ToSingle(sum)), font2, brush5, piex + 100, piey + 50);
            g.DrawString(string.Format("{0:P2}", Convert.ToSingle(fiveNum) / Convert.ToSingle(sum)), font2, brush5, piex + 30, piey + 100);

            
        }


        //菜品种类销售情况
        private void tabPage2_Paint(object sender, PaintEventArgs e)
        {
            m_dbConnection = new SQLiteConnection("Data Source=RMDB.db3;Version=3;");
            m_dbConnection.Open();


            //加载数据至DataGridView
            

            //date1 = dateTimePicker1.Text.ToString();


            string sql = "SELECT ShopFoodTypeA,ShopFoodTypeB,ShopFoodTypeC,ShopFoodTypeD,ShopFoodTypeE FROM Day WHERE Day ='" + date1 + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            string A = "", B = "", C = "", D = "", E = "";

            while (reader.Read())
            {

                A = reader["ShopFoodTypeA"].ToString();
                B = reader["ShopFoodTypeB"].ToString();
                C = reader["ShopFoodTypeC"].ToString();
                D = reader["ShopFoodTypeD"].ToString();
                E = reader["ShopFoodTypeE"].ToString();

            }
            
                

            int[] saleNum = { int.Parse(A),int.Parse(B),int.Parse(C),int.Parse(D), int.Parse(E) };


            //创建画图对象
            int width = 400, height = 400;
            Graphics ghs = e.Graphics;

            Font font = new Font("Arial", 6, FontStyle.Regular);
            Pen pen = new Pen(Color.Blue, 1);
            ////绘制横向线条
            int x = 100;

            //绘制坐标系
            int halfWidth = width / 2;
            int halfHeight = height / 2;
            AdjustableArrowCap arrow = new AdjustableArrowCap(8, 8, false);//定义画笔线帽
            pen.CustomEndCap = arrow;
            ghs.DrawLine(pen, 30, height - 130, width - 100, height - 130);//画横坐标轴
            ghs.DrawLine(pen, 70, height - 100, 70, 70);//画纵坐标轴

            //绘制字体

            Font myFont = new Font("Courier New", 14, FontStyle.Bold);//创建字体对象
            Font myFont1 = new Font("宋体", 10, FontStyle.Bold);//创建字体对象
            Brush brush = new SolidBrush(Color.Black);//创建画刷对象

            string str = "每天菜品种类销量直方图";//定义绘制的文本
            ghs.DrawString(str, myFont, brush, halfWidth - 120, 20);//在窗体的指定位置绘制文本

            str = "0";//定义绘制的文本
            myFont = new Font("宋体", 10, FontStyle.Bold);//创建字体对象
            brush = new SolidBrush(Color.Black);//创建画刷对象

            ghs.DrawString(str, myFont, brush, 50, height - 120);//在窗体的指定位置绘制文本

            str = "销量";//定义绘制的文本
            brush = new SolidBrush(Color.Black);//创建画刷对象

            ghs.DrawString(str, myFont, brush, 40, 90);//在窗体的指定位置绘制文本

            str = "种类";//定义绘制的文本
            brush = new SolidBrush(Color.Black);//创建画刷对象

            ghs.DrawString(str, myFont, brush, width - 130, height - 120);//在窗体的指定位置绘制文本

            
          
            //显示柱状效果
            x = 80;
            for (int i = 0; i < saleNum.Length; i++)
            {
                SolidBrush mybrush = new SolidBrush(Color.YellowGreen);
                str = saleNum[i].ToString();
                ghs.DrawString(str, myFont1, brush, x +5, height - 170 - saleNum[i] / 4);//在窗体的指定位置绘制文本

                str = (i + 1).ToString();
                ghs.DrawString(str, myFont1, brush, x+5, height - 120);//在窗体的指定位置绘制文本
                ghs.FillRectangle(mybrush, x, height - 150 - saleNum[i] / 4, 20, saleNum[i] / 4 + 20);
                x = x + 40;
            }

            ghs.Dispose();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
