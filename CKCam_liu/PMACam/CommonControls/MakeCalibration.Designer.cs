namespace PMACam
{
    partial class MakeCalibration
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
            this.label_9point = new System.Windows.Forms.Label();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.lab_m = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Input_image = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Output_image = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_pixel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_pixel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_width = new System.Windows.Forms.TextBox();
            this.textBox_height = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textbox_lens = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button_文件位置 = new System.Windows.Forms.Button();
            this.textBox_file = new System.Windows.Forms.TextBox();
            this.checkBox_focalize = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_9point
            // 
            this.label_9point.AutoSize = true;
            this.label_9point.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_9point.Location = new System.Drawing.Point(6, 21);
            this.label_9point.Name = "label_9point";
            this.label_9point.Size = new System.Drawing.Size(47, 19);
            this.label_9point.TabIndex = 0;
            this.label_9point.Text = "校正";
            // 
            // btn_Sure
            // 
            this.btn_Sure.Location = new System.Drawing.Point(6, 331);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(64, 21);
            this.btn_Sure.TabIndex = 77;
            this.btn_Sure.Text = "确定";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // lab_m
            // 
            this.lab_m.AutoSize = true;
            this.lab_m.Location = new System.Drawing.Point(12, 56);
            this.lab_m.Name = "lab_m";
            this.lab_m.Size = new System.Drawing.Size(53, 12);
            this.lab_m.TabIndex = 69;
            this.lab_m.Text = "图像输入";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 377);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 79;
            this.label4.Text = "已采集:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(62, 374);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(36, 21);
            this.textBox1.TabIndex = 80;
            this.textBox1.Text = "0";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 23);
            this.button1.TabIndex = 83;
            this.button1.Text = "清零";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Input_image
            // 
            this.Input_image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Input_image.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Input_image.FormattingEnabled = true;
            this.Input_image.Location = new System.Drawing.Point(80, 56);
            this.Input_image.Name = "Input_image";
            this.Input_image.Size = new System.Drawing.Size(87, 22);
            this.Input_image.TabIndex = 84;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 270);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 16);
            this.checkBox1.TabIndex = 85;
            this.checkBox1.Text = "已校正";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Output_image
            // 
            this.Output_image.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Output_image.FormattingEnabled = true;
            this.Output_image.Location = new System.Drawing.Point(80, 81);
            this.Output_image.Name = "Output_image";
            this.Output_image.Size = new System.Drawing.Size(87, 22);
            this.Output_image.TabIndex = 86;
            this.Output_image.Text = "image2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 87;
            this.label1.Text = "图像输出";
            // 
            // textBox_pixel
            // 
            this.textBox_pixel.Location = new System.Drawing.Point(80, 109);
            this.textBox_pixel.Name = "textBox_pixel";
            this.textBox_pixel.Size = new System.Drawing.Size(58, 21);
            this.textBox_pixel.TabIndex = 89;
            this.textBox_pixel.Text = "1.5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 90;
            this.label2.Text = "两圆距离";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 415);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 91;
            this.label3.Text = "像素比:";
            // 
            // label_pixel
            // 
            this.label_pixel.AutoSize = true;
            this.label_pixel.Location = new System.Drawing.Point(68, 415);
            this.label_pixel.Name = "label_pixel";
            this.label_pixel.Size = new System.Drawing.Size(11, 12);
            this.label_pixel.TabIndex = 92;
            this.label_pixel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 95;
            this.label5.Text = "单像素宽";
            // 
            // textBox_width
            // 
            this.textBox_width.Location = new System.Drawing.Point(80, 134);
            this.textBox_width.Name = "textBox_width";
            this.textBox_width.Size = new System.Drawing.Size(58, 21);
            this.textBox_width.TabIndex = 96;
            this.textBox_width.Text = "2.2";
            // 
            // textBox_height
            // 
            this.textBox_height.Location = new System.Drawing.Point(80, 161);
            this.textBox_height.Name = "textBox_height";
            this.textBox_height.Size = new System.Drawing.Size(58, 21);
            this.textBox_height.TabIndex = 97;
            this.textBox_height.Text = "2.2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 98;
            this.label6.Text = "单像素高";
            // 
            // textbox_lens
            // 
            this.textbox_lens.Location = new System.Drawing.Point(80, 186);
            this.textbox_lens.Name = "textbox_lens";
            this.textbox_lens.Size = new System.Drawing.Size(58, 21);
            this.textbox_lens.TabIndex = 99;
            this.textbox_lens.Text = "50";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 100;
            this.label7.Text = "镜头焦距";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 213);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 101;
            this.label8.Text = "标定文件";
            // 
            // button_文件位置
            // 
            this.button_文件位置.Location = new System.Drawing.Point(80, 213);
            this.button_文件位置.Name = "button_文件位置";
            this.button_文件位置.Size = new System.Drawing.Size(75, 23);
            this.button_文件位置.TabIndex = 102;
            this.button_文件位置.Text = "文件位置";
            this.button_文件位置.UseVisualStyleBackColor = true;
            this.button_文件位置.Click += new System.EventHandler(this.button_文件位置_Click);
            // 
            // textBox_file
            // 
            this.textBox_file.Location = new System.Drawing.Point(14, 237);
            this.textBox_file.Name = "textBox_file";
            this.textBox_file.Size = new System.Drawing.Size(153, 21);
            this.textBox_file.TabIndex = 103;
            // 
            // checkBox_focalize
            // 
            this.checkBox_focalize.AutoSize = true;
            this.checkBox_focalize.Location = new System.Drawing.Point(145, 191);
            this.checkBox_focalize.Name = "checkBox_focalize";
            this.checkBox_focalize.Size = new System.Drawing.Size(72, 16);
            this.checkBox_focalize.TabIndex = 110;
            this.checkBox_focalize.Text = "远心镜头";
            this.checkBox_focalize.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 304);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(161, 21);
            this.textBox2.TabIndex = 111;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(173, 302);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 23);
            this.button2.TabIndex = 112;
            this.button2.Text = "文件位置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 289);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 113;
            this.label9.Text = "校正文件保存";
            // 
            // MakeCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(242, 626);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.checkBox_focalize);
            this.Controls.Add(this.textBox_file);
            this.Controls.Add(this.button_文件位置);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textbox_lens);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_height);
            this.Controls.Add(this.textBox_width);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label_pixel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_pixel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Output_image);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Input_image);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.lab_m);
            this.Controls.Add(this.label_9point);
            this.Name = "MakeCalibration";
            this.Text = "MakeCalibration";
            this.Load += new System.EventHandler(this.MakeCalibration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_9point;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.Label lab_m;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox Input_image;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox Output_image;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_pixel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_pixel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_width;
        private System.Windows.Forms.TextBox textBox_height;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textbox_lens;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_文件位置;
        private System.Windows.Forms.TextBox textBox_file;
        private System.Windows.Forms.CheckBox checkBox_focalize;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
    }
}