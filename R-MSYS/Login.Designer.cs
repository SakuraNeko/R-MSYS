namespace WindowsFormsApp1
{
    partial class Login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.登录 = new System.Windows.Forms.TabPage();
            this.Loginbutton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.注册 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.regbutton = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.登录.SuspendLayout();
            this.注册.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.登录);
            this.tabControl1.Controls.Add(this.注册);
            this.tabControl1.Location = new System.Drawing.Point(6, 6);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(706, 672);
            this.tabControl1.TabIndex = 4;
            // 
            // 登录
            // 
            this.登录.Controls.Add(this.Loginbutton);
            this.登录.Controls.Add(this.textBox2);
            this.登录.Controls.Add(this.textBox1);
            this.登录.Controls.Add(this.label2);
            this.登录.Controls.Add(this.label1);
            this.登录.Location = new System.Drawing.Point(8, 39);
            this.登录.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.登录.Name = "登录";
            this.登录.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.登录.Size = new System.Drawing.Size(690, 625);
            this.登录.TabIndex = 0;
            this.登录.Text = "登录";
            this.登录.UseVisualStyleBackColor = true;
            // 
            // Loginbutton
            // 
            this.Loginbutton.Location = new System.Drawing.Point(300, 354);
            this.Loginbutton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Loginbutton.Name = "Loginbutton";
            this.Loginbutton.Size = new System.Drawing.Size(114, 46);
            this.Loginbutton.TabIndex = 17;
            this.Loginbutton.Text = "登录";
            this.Loginbutton.UseVisualStyleBackColor = true;
            this.Loginbutton.Click += new System.EventHandler(this.Loginbutton_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(300, 270);
            this.textBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(196, 35);
            this.textBox2.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(300, 194);
            this.textBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(196, 35);
            this.textBox1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(134, 280);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 33);
            this.label2.TabIndex = 5;
            this.label2.Text = "密码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(134, 194);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 33);
            this.label1.TabIndex = 4;
            this.label1.Text = "用户名";
            // 
            // 注册
            // 
            this.注册.Controls.Add(this.label7);
            this.注册.Controls.Add(this.regbutton);
            this.注册.Controls.Add(this.textBox6);
            this.注册.Controls.Add(this.label6);
            this.注册.Controls.Add(this.textBox5);
            this.注册.Controls.Add(this.label5);
            this.注册.Controls.Add(this.textBox3);
            this.注册.Controls.Add(this.textBox4);
            this.注册.Controls.Add(this.label3);
            this.注册.Controls.Add(this.label4);
            this.注册.Location = new System.Drawing.Point(8, 39);
            this.注册.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.注册.Name = "注册";
            this.注册.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.注册.Size = new System.Drawing.Size(690, 625);
            this.注册.TabIndex = 1;
            this.注册.Text = "注册";
            this.注册.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 6F);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(512, 116);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 16);
            this.label7.TabIndex = 17;
            // 
            // regbutton
            // 
            this.regbutton.Location = new System.Drawing.Point(296, 346);
            this.regbutton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.regbutton.Name = "regbutton";
            this.regbutton.Size = new System.Drawing.Size(114, 46);
            this.regbutton.TabIndex = 16;
            this.regbutton.Text = "注册";
            this.regbutton.UseVisualStyleBackColor = true;
            this.regbutton.Click += new System.EventHandler(this.regbutton_Click);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(296, 264);
            this.textBox6.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(196, 35);
            this.textBox6.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(130, 274);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 33);
            this.label6.TabIndex = 14;
            this.label6.Text = "电话";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(296, 214);
            this.textBox5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox5.Name = "textBox5";
            this.textBox5.PasswordChar = '*';
            this.textBox5.Size = new System.Drawing.Size(196, 35);
            this.textBox5.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(130, 224);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 33);
            this.label5.TabIndex = 12;
            this.label5.Text = "确认密码";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(296, 156);
            this.textBox3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new System.Drawing.Size(196, 35);
            this.textBox3.TabIndex = 11;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(296, 100);
            this.textBox4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(196, 35);
            this.textBox4.TabIndex = 10;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            this.textBox4.Leave += new System.EventHandler(this.textBox4_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(130, 166);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 33);
            this.label3.TabIndex = 9;
            this.label3.Text = "密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(130, 100);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 33);
            this.label4.TabIndex = 8;
            this.label4.Text = "用户名";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 676);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.tabControl1.ResumeLayout(false);
            this.登录.ResumeLayout(false);
            this.登录.PerformLayout();
            this.注册.ResumeLayout(false);
            this.注册.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage 登录;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage 注册;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Loginbutton;
        private System.Windows.Forms.Button regbutton;
        private System.Windows.Forms.Label label7;
    }
}

