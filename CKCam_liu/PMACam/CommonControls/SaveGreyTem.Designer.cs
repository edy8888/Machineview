namespace PMACam
{
    partial class SaveGreyTem
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
            this.button_读取模板 = new System.Windows.Forms.Button();
            this.button_保存模板 = new System.Windows.Forms.Button();
            this.button3_匹配 = new System.Windows.Forms.Button();
            this.button_学习模板 = new System.Windows.Forms.Button();
            this.button_模板形状 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.cbb_image = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button_搜索形状 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label_path = new System.Windows.Forms.Label();
            this.txt_angleStart = new System.Windows.Forms.TextBox();
            this.lab_angleStart = new System.Windows.Forms.Label();
            this.txt_maxerror = new System.Windows.Forms.TextBox();
            this.lab_minScore = new System.Windows.Forms.Label();
            this.txt_greydis = new System.Windows.Forms.TextBox();
            this.lab_numMatches = new System.Windows.Forms.Label();
            this.txt_angleExtent = new System.Windows.Forms.TextBox();
            this.lab_angleExtent = new System.Windows.Forms.Label();
            this.model_subPixel = new System.Windows.Forms.ComboBox();
            this.lab_subPixel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_numMatches = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_读取模板
            // 
            this.button_读取模板.Location = new System.Drawing.Point(76, 160);
            this.button_读取模板.Name = "button_读取模板";
            this.button_读取模板.Size = new System.Drawing.Size(75, 23);
            this.button_读取模板.TabIndex = 38;
            this.button_读取模板.Text = "读取模板";
            this.button_读取模板.UseVisualStyleBackColor = true;
            this.button_读取模板.Click += new System.EventHandler(this.button_读取模板_Click);
            // 
            // button_保存模板
            // 
            this.button_保存模板.Location = new System.Drawing.Point(-1, 160);
            this.button_保存模板.Name = "button_保存模板";
            this.button_保存模板.Size = new System.Drawing.Size(67, 23);
            this.button_保存模板.TabIndex = 34;
            this.button_保存模板.Text = "保存模板";
            this.button_保存模板.UseVisualStyleBackColor = true;
            this.button_保存模板.Click += new System.EventHandler(this.button_保存模板_Click);
            // 
            // button3_匹配
            // 
            this.button3_匹配.Location = new System.Drawing.Point(76, 130);
            this.button3_匹配.Name = "button3_匹配";
            this.button3_匹配.Size = new System.Drawing.Size(75, 23);
            this.button3_匹配.TabIndex = 33;
            this.button3_匹配.Text = "匹配";
            this.button3_匹配.UseVisualStyleBackColor = true;
            this.button3_匹配.Click += new System.EventHandler(this.button3_匹配_Click);
            // 
            // button_学习模板
            // 
            this.button_学习模板.Location = new System.Drawing.Point(-1, 131);
            this.button_学习模板.Name = "button_学习模板";
            this.button_学习模板.Size = new System.Drawing.Size(68, 23);
            this.button_学习模板.TabIndex = 32;
            this.button_学习模板.Text = "学习模板";
            this.button_学习模板.UseVisualStyleBackColor = true;
            this.button_学习模板.Click += new System.EventHandler(this.button2_Click);
            // 
            // button_模板形状
            // 
            this.button_模板形状.Location = new System.Drawing.Point(4, 102);
            this.button_模板形状.Name = "button_模板形状";
            this.button_模板形状.Size = new System.Drawing.Size(63, 23);
            this.button_模板形状.TabIndex = 31;
            this.button_模板形状.Text = "模板特征";
            this.button_模板形状.UseVisualStyleBackColor = true;
            this.button_模板形状.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(-1, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 24;
            this.label1.Text = "取灰度模板";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(4, 47);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 23);
            this.button2.TabIndex = 45;
            this.button2.Text = "图像";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // cbb_image
            // 
            this.cbb_image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_image.FormattingEnabled = true;
            this.cbb_image.Location = new System.Drawing.Point(77, 50);
            this.cbb_image.Name = "cbb_image";
            this.cbb_image.Size = new System.Drawing.Size(74, 20);
            this.cbb_image.TabIndex = 46;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Rectangle2",
            "Circle"});
            this.comboBox1.Location = new System.Drawing.Point(76, 102);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(75, 20);
            this.comboBox1.TabIndex = 47;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "SRectangle2",
            "SCircle"});
            this.comboBox2.Location = new System.Drawing.Point(77, 76);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(74, 20);
            this.comboBox2.TabIndex = 49;
            // 
            // button_搜索形状
            // 
            this.button_搜索形状.Location = new System.Drawing.Point(5, 76);
            this.button_搜索形状.Name = "button_搜索形状";
            this.button_搜索形状.Size = new System.Drawing.Size(63, 23);
            this.button_搜索形状.TabIndex = 48;
            this.button_搜索形状.Text = "搜索区域";
            this.button_搜索形状.UseVisualStyleBackColor = true;
            this.button_搜索形状.Click += new System.EventHandler(this.button_搜索形状_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(3, 501);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 20);
            this.label4.TabIndex = 57;
            this.label4.Text = "注: 退出时,请点确定";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 524);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 56;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 58;
            this.label2.Text = "模板路径:";
            // 
            // label_path
            // 
            this.label_path.AutoSize = true;
            this.label_path.Location = new System.Drawing.Point(74, 196);
            this.label_path.Name = "label_path";
            this.label_path.Size = new System.Drawing.Size(0, 12);
            this.label_path.TabIndex = 59;
            // 
            // txt_angleStart
            // 
            this.txt_angleStart.Location = new System.Drawing.Point(80, 367);
            this.txt_angleStart.Multiline = true;
            this.txt_angleStart.Name = "txt_angleStart";
            this.txt_angleStart.Size = new System.Drawing.Size(92, 22);
            this.txt_angleStart.TabIndex = 36;
            this.txt_angleStart.Text = "0";
            // 
            // lab_angleStart
            // 
            this.lab_angleStart.AutoSize = true;
            this.lab_angleStart.Location = new System.Drawing.Point(5, 370);
            this.lab_angleStart.Name = "lab_angleStart";
            this.lab_angleStart.Size = new System.Drawing.Size(53, 12);
            this.lab_angleStart.TabIndex = 34;
            this.lab_angleStart.Text = "起始角度";
            // 
            // txt_maxerror
            // 
            this.txt_maxerror.Location = new System.Drawing.Point(80, 423);
            this.txt_maxerror.Multiline = true;
            this.txt_maxerror.Name = "txt_maxerror";
            this.txt_maxerror.Size = new System.Drawing.Size(92, 22);
            this.txt_maxerror.TabIndex = 44;
            this.txt_maxerror.Text = "50";
            // 
            // lab_minScore
            // 
            this.lab_minScore.AutoSize = true;
            this.lab_minScore.Location = new System.Drawing.Point(-3, 426);
            this.lab_minScore.Name = "lab_minScore";
            this.lab_minScore.Size = new System.Drawing.Size(65, 12);
            this.lab_minScore.TabIndex = 42;
            this.lab_minScore.Text = "最大冗错值";
            // 
            // txt_greydis
            // 
            this.txt_greydis.Location = new System.Drawing.Point(80, 450);
            this.txt_greydis.Multiline = true;
            this.txt_greydis.Name = "txt_greydis";
            this.txt_greydis.Size = new System.Drawing.Size(92, 22);
            this.txt_greydis.TabIndex = 45;
            this.txt_greydis.Text = "10";
            // 
            // lab_numMatches
            // 
            this.lab_numMatches.AutoSize = true;
            this.lab_numMatches.Location = new System.Drawing.Point(5, 453);
            this.lab_numMatches.Name = "lab_numMatches";
            this.lab_numMatches.Size = new System.Drawing.Size(53, 12);
            this.lab_numMatches.TabIndex = 43;
            this.lab_numMatches.Text = "灰度差值";
            // 
            // txt_angleExtent
            // 
            this.txt_angleExtent.Location = new System.Drawing.Point(80, 395);
            this.txt_angleExtent.Multiline = true;
            this.txt_angleExtent.Name = "txt_angleExtent";
            this.txt_angleExtent.Size = new System.Drawing.Size(92, 22);
            this.txt_angleExtent.TabIndex = 37;
            this.txt_angleExtent.Text = "50";
            // 
            // lab_angleExtent
            // 
            this.lab_angleExtent.AutoSize = true;
            this.lab_angleExtent.Location = new System.Drawing.Point(5, 395);
            this.lab_angleExtent.Name = "lab_angleExtent";
            this.lab_angleExtent.Size = new System.Drawing.Size(53, 12);
            this.lab_angleExtent.TabIndex = 35;
            this.lab_angleExtent.Text = "旋转角度";
            // 
            // model_subPixel
            // 
            this.model_subPixel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.model_subPixel.FormattingEnabled = true;
            this.model_subPixel.Items.AddRange(new object[] {
            "least_squares",
            "interpolation",
            "least_squares_high",
            "least_squares_very_high"});
            this.model_subPixel.Location = new System.Drawing.Point(80, 478);
            this.model_subPixel.Name = "model_subPixel";
            this.model_subPixel.Size = new System.Drawing.Size(92, 20);
            this.model_subPixel.TabIndex = 57;
            // 
            // lab_subPixel
            // 
            this.lab_subPixel.AutoSize = true;
            this.lab_subPixel.Location = new System.Drawing.Point(5, 481);
            this.lab_subPixel.Name = "lab_subPixel";
            this.lab_subPixel.Size = new System.Drawing.Size(53, 12);
            this.lab_subPixel.TabIndex = 51;
            this.lab_subPixel.Text = "精度模式";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 201);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(173, 108);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 60;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 342);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 61;
            this.label3.Text = "匹配个数";
            // 
            // txt_numMatches
            // 
            this.txt_numMatches.Location = new System.Drawing.Point(77, 339);
            this.txt_numMatches.Multiline = true;
            this.txt_numMatches.Name = "txt_numMatches";
            this.txt_numMatches.Size = new System.Drawing.Size(94, 22);
            this.txt_numMatches.TabIndex = 62;
            this.txt_numMatches.Text = "1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(154, 99);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 23);
            this.button3.TabIndex = 63;
            this.button3.Text = "训练";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(127, 22);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 66;
            this.button4.Text = "区域恢复";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // SaveGreyTem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 566);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_numMatches);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lab_numMatches);
            this.Controls.Add(this.txt_greydis);
            this.Controls.Add(this.lab_subPixel);
            this.Controls.Add(this.model_subPixel);
            this.Controls.Add(this.label_path);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lab_minScore);
            this.Controls.Add(this.txt_maxerror);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lab_angleStart);
            this.Controls.Add(this.txt_angleStart);
            this.Controls.Add(this.txt_angleExtent);
            this.Controls.Add(this.lab_angleExtent);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.button_搜索形状);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.cbb_image);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_读取模板);
            this.Controls.Add(this.button_保存模板);
            this.Controls.Add(this.button3_匹配);
            this.Controls.Add(this.button_学习模板);
            this.Controls.Add(this.button_模板形状);
            this.Controls.Add(this.label1);
            this.Name = "SaveGreyTem";
            this.Text = "SaveGreyTem";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_读取模板;
        private System.Windows.Forms.Button button_保存模板;
        private System.Windows.Forms.Button button3_匹配;
        private System.Windows.Forms.Button button_学习模板;
        private System.Windows.Forms.Button button_模板形状;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbb_image;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button_搜索形状;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_path;
        private System.Windows.Forms.TextBox txt_angleStart;
        private System.Windows.Forms.Label lab_angleStart;
        private System.Windows.Forms.TextBox txt_maxerror;
        private System.Windows.Forms.Label lab_minScore;
        private System.Windows.Forms.Label lab_numMatches;
        private System.Windows.Forms.TextBox txt_greydis;
        private System.Windows.Forms.TextBox txt_angleExtent;
        private System.Windows.Forms.Label lab_angleExtent;
        private System.Windows.Forms.ComboBox model_subPixel;
        private System.Windows.Forms.Label lab_subPixel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_numMatches;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}