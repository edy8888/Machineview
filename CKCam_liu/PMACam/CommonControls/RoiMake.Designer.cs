namespace PMACam
{
    partial class RoiMake
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.shape_name = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel_rectangle2 = new System.Windows.Forms.Panel();
            this.txt_rec2phi = new System.Windows.Forms.TextBox();
            this.textBox_rec2col1 = new System.Windows.Forms.TextBox();
            this.textBox_rec2row1 = new System.Windows.Forms.TextBox();
            this.textBox_rec2len2 = new System.Windows.Forms.TextBox();
            this.textBox_rec2len1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.lab_phi = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel_circle = new System.Windows.Forms.Panel();
            this.textBox_circlecolumn = new System.Windows.Forms.TextBox();
            this.textBox_circlerow = new System.Windows.Forms.TextBox();
            this.textBox_circleradius = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.cbb_point = new System.Windows.Forms.ComboBox();
            this.cbb_angle = new System.Windows.Forms.ComboBox();
            this.tb_no = new System.Windows.Forms.TextBox();
            this.checkBox_get = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button_搜索形状 = new System.Windows.Forms.Button();
            this.cbb_image = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.panel_rectangle2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel_circle.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "生成ROI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "形状:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Circle",
            "Rectangle2"});
            this.comboBox1.Location = new System.Drawing.Point(59, 110);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(90, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "Circle";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // shape_name
            // 
            this.shape_name.FormattingEnabled = true;
            this.shape_name.Location = new System.Drawing.Point(64, 145);
            this.shape_name.Name = "shape_name";
            this.shape_name.Size = new System.Drawing.Size(85, 20);
            this.shape_name.TabIndex = 38;
            this.shape_name.Text = "ROI1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 39;
            this.label7.Text = "名字:";
            // 
            // panel_rectangle2
            // 
            this.panel_rectangle2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_rectangle2.Controls.Add(this.txt_rec2phi);
            this.panel_rectangle2.Controls.Add(this.textBox_rec2col1);
            this.panel_rectangle2.Controls.Add(this.textBox_rec2row1);
            this.panel_rectangle2.Controls.Add(this.textBox_rec2len2);
            this.panel_rectangle2.Controls.Add(this.textBox_rec2len1);
            this.panel_rectangle2.Controls.Add(this.groupBox2);
            this.panel_rectangle2.Location = new System.Drawing.Point(4, 249);
            this.panel_rectangle2.Name = "panel_rectangle2";
            this.panel_rectangle2.Size = new System.Drawing.Size(189, 173);
            this.panel_rectangle2.TabIndex = 40;
            // 
            // txt_rec2phi
            // 
            this.txt_rec2phi.Location = new System.Drawing.Point(103, 141);
            this.txt_rec2phi.Multiline = true;
            this.txt_rec2phi.Name = "txt_rec2phi";
            this.txt_rec2phi.Size = new System.Drawing.Size(74, 22);
            this.txt_rec2phi.TabIndex = 44;
            this.txt_rec2phi.Text = "0.5";
            // 
            // textBox_rec2col1
            // 
            this.textBox_rec2col1.Location = new System.Drawing.Point(101, 57);
            this.textBox_rec2col1.Multiline = true;
            this.textBox_rec2col1.Name = "textBox_rec2col1";
            this.textBox_rec2col1.Size = new System.Drawing.Size(74, 22);
            this.textBox_rec2col1.TabIndex = 43;
            this.textBox_rec2col1.Text = "0";
            // 
            // textBox_rec2row1
            // 
            this.textBox_rec2row1.Location = new System.Drawing.Point(102, 26);
            this.textBox_rec2row1.Multiline = true;
            this.textBox_rec2row1.Name = "textBox_rec2row1";
            this.textBox_rec2row1.Size = new System.Drawing.Size(74, 22);
            this.textBox_rec2row1.TabIndex = 42;
            this.textBox_rec2row1.Text = "0";
            // 
            // textBox_rec2len2
            // 
            this.textBox_rec2len2.Location = new System.Drawing.Point(101, 113);
            this.textBox_rec2len2.Multiline = true;
            this.textBox_rec2len2.Name = "textBox_rec2len2";
            this.textBox_rec2len2.Size = new System.Drawing.Size(74, 22);
            this.textBox_rec2len2.TabIndex = 40;
            this.textBox_rec2len2.Text = "10";
            // 
            // textBox_rec2len1
            // 
            this.textBox_rec2len1.Location = new System.Drawing.Point(101, 85);
            this.textBox_rec2len1.Multiline = true;
            this.textBox_rec2len1.Name = "textBox_rec2len1";
            this.textBox_rec2len1.Size = new System.Drawing.Size(74, 22);
            this.textBox_rec2len1.TabIndex = 39;
            this.textBox_rec2len1.Text = "10";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.lab_phi);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Location = new System.Drawing.Point(5, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(91, 157);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(47, 138);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 12);
            this.label20.TabIndex = 12;
            this.label20.Text = "输入";
            // 
            // lab_phi
            // 
            this.lab_phi.AutoSize = true;
            this.lab_phi.Location = new System.Drawing.Point(3, 135);
            this.lab_phi.Name = "lab_phi";
            this.lab_phi.Size = new System.Drawing.Size(29, 12);
            this.lab_phi.TabIndex = 11;
            this.lab_phi.Text = "角度";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "输入";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(53, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "输入";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 110);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "宽度";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 7;
            this.label13.Text = "长度";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(56, 54);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 6;
            this.label14.Text = "输入";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(54, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 5;
            this.label15.Text = "输入";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 54);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 12);
            this.label16.TabIndex = 4;
            this.label16.Text = "起点Y";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 12);
            this.label17.TabIndex = 3;
            this.label17.Text = "起点X";
            // 
            // panel_circle
            // 
            this.panel_circle.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_circle.Controls.Add(this.textBox_circlecolumn);
            this.panel_circle.Controls.Add(this.textBox_circlerow);
            this.panel_circle.Controls.Add(this.textBox_circleradius);
            this.panel_circle.Controls.Add(this.groupBox3);
            this.panel_circle.Location = new System.Drawing.Point(4, 249);
            this.panel_circle.Name = "panel_circle";
            this.panel_circle.Size = new System.Drawing.Size(189, 170);
            this.panel_circle.TabIndex = 41;
            // 
            // textBox_circlecolumn
            // 
            this.textBox_circlecolumn.Location = new System.Drawing.Point(103, 67);
            this.textBox_circlecolumn.Multiline = true;
            this.textBox_circlecolumn.Name = "textBox_circlecolumn";
            this.textBox_circlecolumn.Size = new System.Drawing.Size(58, 22);
            this.textBox_circlecolumn.TabIndex = 43;
            this.textBox_circlecolumn.Text = "0";
            // 
            // textBox_circlerow
            // 
            this.textBox_circlerow.Location = new System.Drawing.Point(102, 35);
            this.textBox_circlerow.Multiline = true;
            this.textBox_circlerow.Name = "textBox_circlerow";
            this.textBox_circlerow.Size = new System.Drawing.Size(58, 22);
            this.textBox_circlerow.TabIndex = 42;
            this.textBox_circlerow.Text = "0";
            // 
            // textBox_circleradius
            // 
            this.textBox_circleradius.Location = new System.Drawing.Point(103, 98);
            this.textBox_circleradius.Multiline = true;
            this.textBox_circleradius.Name = "textBox_circleradius";
            this.textBox_circleradius.Size = new System.Drawing.Size(58, 22);
            this.textBox_circleradius.TabIndex = 39;
            this.textBox_circleradius.Text = "5";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Location = new System.Drawing.Point(6, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(91, 111);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox1";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(53, 89);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 12);
            this.label22.TabIndex = 9;
            this.label22.Text = "输入";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(11, 89);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 12);
            this.label24.TabIndex = 7;
            this.label24.Text = "半径";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(53, 58);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(29, 12);
            this.label25.TabIndex = 6;
            this.label25.Text = "输入";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(50, 30);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(29, 12);
            this.label26.TabIndex = 5;
            this.label26.Text = "输入";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(7, 58);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(35, 12);
            this.label27.TabIndex = 4;
            this.label27.Text = "圆心Y";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(7, 30);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(35, 12);
            this.label28.TabIndex = 3;
            this.label28.Text = "圆心X";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 42;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(7, 439);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(199, 20);
            this.label21.TabIndex = 43;
            this.label21.Text = "注: 退出时,请点确定";
            // 
            // cbb_point
            // 
            this.cbb_point.FormattingEnabled = true;
            this.cbb_point.Items.AddRange(new object[] {
            "匹配列表",
            "全局点列表"});
            this.cbb_point.Location = new System.Drawing.Point(60, 171);
            this.cbb_point.Name = "cbb_point";
            this.cbb_point.Size = new System.Drawing.Size(89, 20);
            this.cbb_point.TabIndex = 44;
            // 
            // cbb_angle
            // 
            this.cbb_angle.FormattingEnabled = true;
            this.cbb_angle.Items.AddRange(new object[] {
            "匹配列表",
            "线列表",
            "文本"});
            this.cbb_angle.Location = new System.Drawing.Point(61, 197);
            this.cbb_angle.Name = "cbb_angle";
            this.cbb_angle.Size = new System.Drawing.Size(88, 20);
            this.cbb_angle.TabIndex = 45;
            // 
            // tb_no
            // 
            this.tb_no.Location = new System.Drawing.Point(163, 171);
            this.tb_no.Name = "tb_no";
            this.tb_no.Size = new System.Drawing.Size(38, 21);
            this.tb_no.TabIndex = 46;
            // 
            // checkBox_get
            // 
            this.checkBox_get.AutoSize = true;
            this.checkBox_get.Location = new System.Drawing.Point(9, 224);
            this.checkBox_get.Name = "checkBox_get";
            this.checkBox_get.Size = new System.Drawing.Size(72, 16);
            this.checkBox_get.TabIndex = 47;
            this.checkBox_get.Text = "获取点位";
            this.checkBox_get.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 48;
            this.label4.Text = "点1:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 200);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 49;
            this.label9.Text = "角度:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(3, 63);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 50;
            this.checkBox1.Text = "手动";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button_搜索形状
            // 
            this.button_搜索形状.Location = new System.Drawing.Point(61, 58);
            this.button_搜索形状.Name = "button_搜索形状";
            this.button_搜索形状.Size = new System.Drawing.Size(63, 25);
            this.button_搜索形状.TabIndex = 51;
            this.button_搜索形状.Text = "区域";
            this.button_搜索形状.UseVisualStyleBackColor = true;
            this.button_搜索形状.Click += new System.EventHandler(this.button_搜索形状_Click);
            // 
            // cbb_image
            // 
            this.cbb_image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_image.FormattingEnabled = true;
            this.cbb_image.Location = new System.Drawing.Point(58, 84);
            this.cbb_image.Name = "cbb_image";
            this.cbb_image.Size = new System.Drawing.Size(91, 20);
            this.cbb_image.TabIndex = 54;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 52;
            this.label3.Text = "图像";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(130, 60);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(62, 23);
            this.button3.TabIndex = 66;
            this.button3.Text = "区域恢复";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // RoiMake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(209, 566);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbb_image);
            this.Controls.Add(this.button_搜索形状);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox_get);
            this.Controls.Add(this.tb_no);
            this.Controls.Add(this.cbb_angle);
            this.Controls.Add(this.cbb_point);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shape_name);
            this.Controls.Add(this.panel_circle);
            this.Controls.Add(this.panel_rectangle2);
            this.Name = "RoiMake";
            this.Text = "RoiMake";
            this.Load += new System.EventHandler(this.RoiMake_Load);
            this.panel_rectangle2.ResumeLayout(false);
            this.panel_rectangle2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel_circle.ResumeLayout(false);
            this.panel_circle.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox shape_name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel_rectangle2;
        private System.Windows.Forms.TextBox txt_rec2phi;
        private System.Windows.Forms.TextBox textBox_rec2col1;
        private System.Windows.Forms.TextBox textBox_rec2row1;
        private System.Windows.Forms.TextBox textBox_rec2len2;
        private System.Windows.Forms.TextBox textBox_rec2len1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lab_phi;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel_circle;
        private System.Windows.Forms.TextBox textBox_circlecolumn;
        private System.Windows.Forms.TextBox textBox_circlerow;
        private System.Windows.Forms.TextBox textBox_circleradius;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbb_point;
        private System.Windows.Forms.ComboBox cbb_angle;
        private System.Windows.Forms.TextBox tb_no;
        private System.Windows.Forms.CheckBox checkBox_get;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button_搜索形状;
        private System.Windows.Forms.ComboBox cbb_image;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
    }
}