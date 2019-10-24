namespace PMACam
{
    partial class SmoothTest
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.txt_sizegauss = new System.Windows.Forms.ComboBox();
            this.smooth_image = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_size = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel_gauss = new System.Windows.Forms.Panel();
            this.panel_median = new System.Windows.Forms.Panel();
            this.smooth_margin = new System.Windows.Forms.ComboBox();
            this.smooth_typemedian = new System.Windows.Forms.ComboBox();
            this.txt_radiusmedian = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.margin = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lab_radius = new System.Windows.Forms.Label();
            this.lab_maskType = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel_mean = new System.Windows.Forms.Panel();
            this.txt_maskHeightmean = new System.Windows.Forms.TextBox();
            this.txt_maskWidthmean = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lab_maskHeight = new System.Windows.Forms.Label();
            this.lab_maskWidth = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.smooth_imageMeanout = new System.Windows.Forms.ComboBox();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.type = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel_gauss.SuspendLayout();
            this.panel_median.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel_mean.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "平滑处理";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Cancel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Cancel.Location = new System.Drawing.Point(111, 451);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(64, 21);
            this.btn_Cancel.TabIndex = 33;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Sure
            // 
            this.btn_Sure.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Sure.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Sure.Location = new System.Drawing.Point(7, 451);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(64, 21);
            this.btn_Sure.TabIndex = 32;
            this.btn_Sure.Text = "确定";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // txt_sizegauss
            // 
            this.txt_sizegauss.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txt_sizegauss.FormattingEnabled = true;
            this.txt_sizegauss.Items.AddRange(new object[] {
            "3",
            "5",
            "7",
            "9",
            "11"});
            this.txt_sizegauss.Location = new System.Drawing.Point(100, 61);
            this.txt_sizegauss.Name = "txt_sizegauss";
            this.txt_sizegauss.Size = new System.Drawing.Size(82, 20);
            this.txt_sizegauss.TabIndex = 50;
            // 
            // smooth_image
            // 
            this.smooth_image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.smooth_image.FormattingEnabled = true;
            this.smooth_image.Location = new System.Drawing.Point(80, 123);
            this.smooth_image.Name = "smooth_image";
            this.smooth_image.Size = new System.Drawing.Size(106, 20);
            this.smooth_image.TabIndex = 45;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lab_size);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(6, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(84, 82);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "in";
            // 
            // lab_size
            // 
            this.lab_size.AutoSize = true;
            this.lab_size.Location = new System.Drawing.Point(10, 51);
            this.lab_size.Name = "lab_size";
            this.lab_size.Size = new System.Drawing.Size(29, 12);
            this.lab_size.TabIndex = 3;
            this.lab_size.Text = "size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "参数名";
            // 
            // panel_gauss
            // 
            this.panel_gauss.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_gauss.Controls.Add(this.groupBox1);
            this.panel_gauss.Controls.Add(this.txt_sizegauss);
            this.panel_gauss.Location = new System.Drawing.Point(7, 218);
            this.panel_gauss.Name = "panel_gauss";
            this.panel_gauss.Size = new System.Drawing.Size(186, 111);
            this.panel_gauss.TabIndex = 51;
            // 
            // panel_median
            // 
            this.panel_median.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_median.Controls.Add(this.smooth_margin);
            this.panel_median.Controls.Add(this.smooth_typemedian);
            this.panel_median.Controls.Add(this.txt_radiusmedian);
            this.panel_median.Controls.Add(this.groupBox2);
            this.panel_median.Location = new System.Drawing.Point(7, 218);
            this.panel_median.Name = "panel_median";
            this.panel_median.Size = new System.Drawing.Size(186, 189);
            this.panel_median.TabIndex = 52;
            // 
            // smooth_margin
            // 
            this.smooth_margin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.smooth_margin.FormattingEnabled = true;
            this.smooth_margin.Items.AddRange(new object[] {
            "mirrored",
            "cyclic",
            "continued"});
            this.smooth_margin.Location = new System.Drawing.Point(94, 150);
            this.smooth_margin.Name = "smooth_margin";
            this.smooth_margin.Size = new System.Drawing.Size(85, 20);
            this.smooth_margin.TabIndex = 63;
            // 
            // smooth_typemedian
            // 
            this.smooth_typemedian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.smooth_typemedian.FormattingEnabled = true;
            this.smooth_typemedian.Items.AddRange(new object[] {
            "square",
            "circle"});
            this.smooth_typemedian.Location = new System.Drawing.Point(94, 86);
            this.smooth_typemedian.Name = "smooth_typemedian";
            this.smooth_typemedian.Size = new System.Drawing.Size(85, 20);
            this.smooth_typemedian.TabIndex = 61;

            // 
            // txt_radiusmedian
            // 
            this.txt_radiusmedian.Location = new System.Drawing.Point(94, 115);
            this.txt_radiusmedian.Multiline = true;
            this.txt_radiusmedian.Name = "txt_radiusmedian";
            this.txt_radiusmedian.Size = new System.Drawing.Size(85, 22);
            this.txt_radiusmedian.TabIndex = 58;
            this.txt_radiusmedian.Text = "3";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.margin);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lab_radius);
            this.groupBox2.Controls.Add(this.lab_maskType);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(2, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(86, 158);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";

            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(61, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "in";
            // 
            // margin
            // 
            this.margin.AutoSize = true;
            this.margin.Location = new System.Drawing.Point(3, 128);
            this.margin.Name = "margin";
            this.margin.Size = new System.Drawing.Size(53, 12);
            this.margin.TabIndex = 7;
            this.margin.Text = "空白处理";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(57, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "in";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(57, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 5;
            this.label11.Text = "in";
            // 
            // lab_radius
            // 
            this.lab_radius.AutoSize = true;
            this.lab_radius.Location = new System.Drawing.Point(6, 96);
            this.lab_radius.Name = "lab_radius";
            this.lab_radius.Size = new System.Drawing.Size(29, 12);
            this.lab_radius.TabIndex = 4;
            this.lab_radius.Text = "半径";
            // 
            // lab_maskType
            // 
            this.lab_maskType.AutoSize = true;
            this.lab_maskType.Location = new System.Drawing.Point(6, 64);
            this.lab_maskType.Name = "lab_maskType";
            this.lab_maskType.Size = new System.Drawing.Size(29, 12);
            this.lab_maskType.TabIndex = 3;
            this.lab_maskType.Text = "类型";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(49, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "类型";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "参数名";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 126);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 1;
            this.label13.Text = "图像";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(52, 126);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 1;
            this.label15.Text = "输入";
            // 
            // panel_mean
            // 
            this.panel_mean.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_mean.Controls.Add(this.txt_maskHeightmean);
            this.panel_mean.Controls.Add(this.txt_maskWidthmean);
            this.panel_mean.Controls.Add(this.groupBox3);
            this.panel_mean.Location = new System.Drawing.Point(7, 218);
            this.panel_mean.Name = "panel_mean";
            this.panel_mean.Size = new System.Drawing.Size(186, 133);
            this.panel_mean.TabIndex = 62;
            // 
            // txt_maskHeightmean
            // 
            this.txt_maskHeightmean.Location = new System.Drawing.Point(100, 95);
            this.txt_maskHeightmean.Multiline = true;
            this.txt_maskHeightmean.Name = "txt_maskHeightmean";
            this.txt_maskHeightmean.Size = new System.Drawing.Size(79, 22);
            this.txt_maskHeightmean.TabIndex = 55;
            this.txt_maskHeightmean.Text = "3";
            // 
            // txt_maskWidthmean
            // 
            this.txt_maskWidthmean.Location = new System.Drawing.Point(100, 61);
            this.txt_maskWidthmean.Multiline = true;
            this.txt_maskWidthmean.Name = "txt_maskWidthmean";
            this.txt_maskWidthmean.Size = new System.Drawing.Size(79, 22);
            this.txt_maskWidthmean.TabIndex = 54;
            this.txt_maskWidthmean.Text = "3";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.lab_maskHeight);
            this.groupBox3.Controls.Add(this.lab_maskWidth);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(91, 127);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(44, 91);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 12);
            this.label18.TabIndex = 6;
            this.label18.Text = "in";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(45, 58);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(17, 12);
            this.label19.TabIndex = 5;
            this.label19.Text = "in";
            // 
            // lab_maskHeight
            // 
            this.lab_maskHeight.AutoSize = true;
            this.lab_maskHeight.Location = new System.Drawing.Point(3, 96);
            this.lab_maskHeight.Name = "lab_maskHeight";
            this.lab_maskHeight.Size = new System.Drawing.Size(17, 12);
            this.lab_maskHeight.TabIndex = 4;
            this.lab_maskHeight.Text = "高";
            // 
            // lab_maskWidth
            // 
            this.lab_maskWidth.AutoSize = true;
            this.lab_maskWidth.Location = new System.Drawing.Point(5, 63);
            this.lab_maskWidth.Name = "lab_maskWidth";
            this.lab_maskWidth.Size = new System.Drawing.Size(17, 12);
            this.lab_maskWidth.TabIndex = 3;
            this.lab_maskWidth.Text = "宽";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(43, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 12);
            this.label20.TabIndex = 0;
            this.label20.Text = "类型";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 22);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 0;
            this.label22.Text = "参数名";
            // 
            // smooth_imageMeanout
            // 
            this.smooth_imageMeanout.FormattingEnabled = true;
            this.smooth_imageMeanout.Location = new System.Drawing.Point(80, 159);
            this.smooth_imageMeanout.Name = "smooth_imageMeanout";
            this.smooth_imageMeanout.Size = new System.Drawing.Size(106, 20);
            this.smooth_imageMeanout.TabIndex = 53;
            this.smooth_imageMeanout.Text = "image12";
            // 
            // comboBox_type
            // 
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            "高斯",
            "均值",
            "中值"});
            this.comboBox_type.Location = new System.Drawing.Point(82, 86);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(87, 20);
            this.comboBox_type.TabIndex = 59;
            this.comboBox_type.Text = "高斯";
            this.comboBox_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_type_SelectedIndexChanged);
            // 
            // type
            // 
            this.type.AutoSize = true;
            this.type.Location = new System.Drawing.Point(13, 89);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(53, 12);
            this.type.TabIndex = 63;
            this.type.Text = "平滑模板";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 64;
            this.label5.Text = "图像";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(46, 159);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 7;
            this.label21.Text = "输出";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(6, 410);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 20);
            this.label6.TabIndex = 65;
            this.label6.Text = "注: 退出时,请点确定";
            // 
            // SmoothTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(202, 546);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.type);
            this.Controls.Add(this.smooth_imageMeanout);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.smooth_image);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panel_gauss);
            this.Controls.Add(this.panel_mean);
            this.Controls.Add(this.panel_median);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Name = "SmoothTest";
            this.Text = "SmoothTest";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_gauss.ResumeLayout(false);
            this.panel_median.ResumeLayout(false);
            this.panel_median.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel_mean.ResumeLayout(false);
            this.panel_mean.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.ComboBox txt_sizegauss;
        private System.Windows.Forms.ComboBox smooth_image;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lab_size;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel_gauss;
        private System.Windows.Forms.Panel panel_median;
        private System.Windows.Forms.ComboBox smooth_typemedian;
        private System.Windows.Forms.TextBox txt_radiusmedian;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label margin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lab_radius;
        private System.Windows.Forms.Label lab_maskType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel_mean;
        private System.Windows.Forms.TextBox txt_maskHeightmean;
        private System.Windows.Forms.TextBox txt_maskWidthmean;
        private System.Windows.Forms.ComboBox smooth_imageMeanout;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lab_maskHeight;
        private System.Windows.Forms.Label lab_maskWidth;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.Label type;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox smooth_margin;
        private System.Windows.Forms.Label label6;
    }
}