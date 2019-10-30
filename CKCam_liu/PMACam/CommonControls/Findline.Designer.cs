namespace PMACam
{
    partial class FindLine
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
            this.lab_image = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.Threshold_image = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lab_minScore = new System.Windows.Forms.Label();
            this.measure_length2 = new System.Windows.Forms.TextBox();
            this.lab_num_measure = new System.Windows.Forms.Label();
            this.num_measure = new System.Windows.Forms.TextBox();
            this.measure_length1 = new System.Windows.Forms.TextBox();
            this.lab_measure_length1 = new System.Windows.Forms.Label();
            this.lab_measure_threshold = new System.Windows.Forms.Label();
            this.measure_threshold = new System.Windows.Forms.TextBox();
            this.cbb_transition = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_min_score = new System.Windows.Forms.Label();
            this.min_score = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbb_measure_select = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBox_line = new System.Windows.Forms.CheckBox();
            this.cbb_source1 = new System.Windows.Forms.ComboBox();
            this.tb_value1 = new System.Windows.Forms.TextBox();
            this.cbb_source2 = new System.Windows.Forms.ComboBox();
            this.tb_value2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "查找线";
            // 
            // lab_image
            // 
            this.lab_image.AutoSize = true;
            this.lab_image.Location = new System.Drawing.Point(12, 82);
            this.lab_image.Name = "lab_image";
            this.lab_image.Size = new System.Drawing.Size(29, 12);
            this.lab_image.TabIndex = 1;
            this.lab_image.Text = "图像";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "输入";
            // 
            // btn_Sure
            // 
            this.btn_Sure.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Sure.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Sure.Location = new System.Drawing.Point(8, 549);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(64, 21);
            this.btn_Sure.TabIndex = 32;
            this.btn_Sure.Text = "确定";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // Threshold_image
            // 
            this.Threshold_image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Threshold_image.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Threshold_image.FormattingEnabled = true;
            this.Threshold_image.Location = new System.Drawing.Point(115, 77);
            this.Threshold_image.Name = "Threshold_image";
            this.Threshold_image.Size = new System.Drawing.Size(86, 22);
            this.Threshold_image.TabIndex = 28;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(4, 517);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(199, 20);
            this.label13.TabIndex = 54;
            this.label13.Text = "注: 退出时,请点确定";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 23);
            this.button1.TabIndex = 55;
            this.button1.Text = "画线";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lab_minScore
            // 
            this.lab_minScore.AutoSize = true;
            this.lab_minScore.Location = new System.Drawing.Point(13, 353);
            this.lab_minScore.Name = "lab_minScore";
            this.lab_minScore.Size = new System.Drawing.Size(53, 12);
            this.lab_minScore.TabIndex = 69;
            this.lab_minScore.Text = "卡尺宽度";
            // 
            // measure_length2
            // 
            this.measure_length2.Location = new System.Drawing.Point(104, 350);
            this.measure_length2.Multiline = true;
            this.measure_length2.Name = "measure_length2";
            this.measure_length2.Size = new System.Drawing.Size(89, 22);
            this.measure_length2.TabIndex = 70;
            this.measure_length2.Text = "10";
            // 
            // lab_num_measure
            // 
            this.lab_num_measure.AutoSize = true;
            this.lab_num_measure.Location = new System.Drawing.Point(13, 294);
            this.lab_num_measure.Name = "lab_num_measure";
            this.lab_num_measure.Size = new System.Drawing.Size(59, 12);
            this.lab_num_measure.TabIndex = 65;
            this.lab_num_measure.Text = "卡尺数量 ";
            // 
            // num_measure
            // 
            this.num_measure.Location = new System.Drawing.Point(104, 294);
            this.num_measure.Multiline = true;
            this.num_measure.Name = "num_measure";
            this.num_measure.Size = new System.Drawing.Size(89, 22);
            this.num_measure.TabIndex = 67;
            this.num_measure.Text = "30";
            // 
            // measure_length1
            // 
            this.measure_length1.Location = new System.Drawing.Point(104, 322);
            this.measure_length1.Multiline = true;
            this.measure_length1.Name = "measure_length1";
            this.measure_length1.Size = new System.Drawing.Size(89, 22);
            this.measure_length1.TabIndex = 68;
            this.measure_length1.Text = "30";
            // 
            // lab_measure_length1
            // 
            this.lab_measure_length1.AutoSize = true;
            this.lab_measure_length1.Location = new System.Drawing.Point(13, 325);
            this.lab_measure_length1.Name = "lab_measure_length1";
            this.lab_measure_length1.Size = new System.Drawing.Size(53, 12);
            this.lab_measure_length1.TabIndex = 66;
            this.lab_measure_length1.Text = "卡尺长度";
            // 
            // lab_measure_threshold
            // 
            this.lab_measure_threshold.AutoSize = true;
            this.lab_measure_threshold.Location = new System.Drawing.Point(13, 381);
            this.lab_measure_threshold.Name = "lab_measure_threshold";
            this.lab_measure_threshold.Size = new System.Drawing.Size(53, 12);
            this.lab_measure_threshold.TabIndex = 71;
            this.lab_measure_threshold.Text = "灰度阈值";
            // 
            // measure_threshold
            // 
            this.measure_threshold.Location = new System.Drawing.Point(104, 378);
            this.measure_threshold.Multiline = true;
            this.measure_threshold.Name = "measure_threshold";
            this.measure_threshold.Size = new System.Drawing.Size(89, 22);
            this.measure_threshold.TabIndex = 72;
            this.measure_threshold.Text = "10";
            // 
            // cbb_transition
            // 
            this.cbb_transition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_transition.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbb_transition.FormattingEnabled = true;
            this.cbb_transition.Items.AddRange(new object[] {
            "all",
            "positive",
            "negative",
            "uniform"});
            this.cbb_transition.Location = new System.Drawing.Point(104, 462);
            this.cbb_transition.Name = "cbb_transition";
            this.cbb_transition.Size = new System.Drawing.Size(89, 22);
            this.cbb_transition.TabIndex = 73;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 467);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 74;
            this.label2.Text = "卡尺变化";
            // 
            // lab_min_score
            // 
            this.lab_min_score.AutoSize = true;
            this.lab_min_score.Location = new System.Drawing.Point(13, 409);
            this.lab_min_score.Name = "lab_min_score";
            this.lab_min_score.Size = new System.Drawing.Size(53, 12);
            this.lab_min_score.TabIndex = 75;
            this.lab_min_score.Text = "最小分数";
            // 
            // min_score
            // 
            this.min_score.Location = new System.Drawing.Point(104, 406);
            this.min_score.Multiline = true;
            this.min_score.Name = "min_score";
            this.min_score.Size = new System.Drawing.Size(89, 22);
            this.min_score.TabIndex = 76;
            this.min_score.Text = "0.8";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 439);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 78;
            this.label6.Text = "边缘选择";
            // 
            // cbb_measure_select
            // 
            this.cbb_measure_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_measure_select.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbb_measure_select.FormattingEnabled = true;
            this.cbb_measure_select.Items.AddRange(new object[] {
            "all",
            "first",
            "last"});
            this.cbb_measure_select.Location = new System.Drawing.Point(104, 434);
            this.cbb_measure_select.Name = "cbb_measure_select";
            this.cbb_measure_select.Size = new System.Drawing.Size(89, 22);
            this.cbb_measure_select.TabIndex = 77;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "red",
            "green",
            "blue",
            "white",
            "black",
            "yellow",
            "orange"});
            this.comboBox1.Location = new System.Drawing.Point(109, 255);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(69, 20);
            this.comboBox1.TabIndex = 79;
            this.comboBox1.Text = "red";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(68, 258);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 80;
            this.label8.Text = "颜色:";
            // 
            // checkBox_line
            // 
            this.checkBox_line.AutoSize = true;
            this.checkBox_line.Location = new System.Drawing.Point(4, 228);
            this.checkBox_line.Name = "checkBox_line";
            this.checkBox_line.Size = new System.Drawing.Size(72, 16);
            this.checkBox_line.TabIndex = 2;
            this.checkBox_line.Text = "手动画线";
            this.checkBox_line.UseVisualStyleBackColor = true;
            // 
            // cbb_source1
            // 
            this.cbb_source1.FormattingEnabled = true;
            this.cbb_source1.Items.AddRange(new object[] {
            "匹配列表",
            "全局点列表"});
            this.cbb_source1.Location = new System.Drawing.Point(12, 114);
            this.cbb_source1.Name = "cbb_source1";
            this.cbb_source1.Size = new System.Drawing.Size(112, 20);
            this.cbb_source1.TabIndex = 2;
            // 
            // tb_value1
            // 
            this.tb_value1.Location = new System.Drawing.Point(156, 113);
            this.tb_value1.Name = "tb_value1";
            this.tb_value1.Size = new System.Drawing.Size(44, 21);
            this.tb_value1.TabIndex = 81;
            this.tb_value1.Text = "0";
            // 
            // cbb_source2
            // 
            this.cbb_source2.FormattingEnabled = true;
            this.cbb_source2.Items.AddRange(new object[] {
            "全局点列表"});
            this.cbb_source2.Location = new System.Drawing.Point(12, 152);
            this.cbb_source2.Name = "cbb_source2";
            this.cbb_source2.Size = new System.Drawing.Size(112, 20);
            this.cbb_source2.TabIndex = 82;
            // 
            // tb_value2
            // 
            this.tb_value2.Location = new System.Drawing.Point(157, 152);
            this.tb_value2.Name = "tb_value2";
            this.tb_value2.Size = new System.Drawing.Size(44, 21);
            this.tb_value2.TabIndex = 83;
            this.tb_value2.Text = "0";
            // 
            // FindLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(216, 670);
            this.Controls.Add(this.tb_value2);
            this.Controls.Add(this.lab_image);
            this.Controls.Add(this.cbb_source2);
            this.Controls.Add(this.cbb_source1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_value1);
            this.Controls.Add(this.checkBox_line);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbb_measure_select);
            this.Controls.Add(this.lab_min_score);
            this.Controls.Add(this.min_score);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbb_transition);
            this.Controls.Add(this.lab_measure_threshold);
            this.Controls.Add(this.measure_threshold);
            this.Controls.Add(this.lab_minScore);
            this.Controls.Add(this.measure_length2);
            this.Controls.Add(this.lab_num_measure);
            this.Controls.Add(this.num_measure);
            this.Controls.Add(this.measure_length1);
            this.Controls.Add(this.lab_measure_length1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.Threshold_image);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Name = "FindLine";
            this.Text = "FindLine";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_image;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.ComboBox Threshold_image;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lab_minScore;
        private System.Windows.Forms.TextBox measure_length2;
        private System.Windows.Forms.Label lab_num_measure;
        private System.Windows.Forms.TextBox num_measure;
        private System.Windows.Forms.TextBox measure_length1;
        private System.Windows.Forms.Label lab_measure_length1;
        private System.Windows.Forms.Label lab_measure_threshold;
        private System.Windows.Forms.TextBox measure_threshold;
        private System.Windows.Forms.ComboBox cbb_transition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lab_min_score;
        private System.Windows.Forms.TextBox min_score;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbb_measure_select;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBox_line;
        private System.Windows.Forms.ComboBox cbb_source1;
        private System.Windows.Forms.TextBox tb_value1;
        private System.Windows.Forms.ComboBox cbb_source2;
        private System.Windows.Forms.TextBox tb_value2;
    }
}