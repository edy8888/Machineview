namespace PMACam
{
    partial class FindCircle
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
            this.label7 = new System.Windows.Forms.Label();
            this.lab_image = new System.Windows.Forms.Label();
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
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbb_measure_select = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbb_transition = new System.Windows.Forms.ComboBox();
            this.checkBox_circle = new System.Windows.Forms.CheckBox();
            this.cbb_source1 = new System.Windows.Forms.ComboBox();
            this.tb_value1 = new System.Windows.Forms.TextBox();
            this.textBox_radius = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.label1.Text = "查找圆";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(102, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 14);
            this.label7.TabIndex = 34;
            this.label7.Text = "命名规范:*_b";
            // 
            // lab_image
            // 
            this.lab_image.AutoSize = true;
            this.lab_image.Location = new System.Drawing.Point(16, 150);
            this.lab_image.Name = "lab_image";
            this.lab_image.Size = new System.Drawing.Size(53, 12);
            this.lab_image.TabIndex = 1;
            this.lab_image.Text = "图像输入";
            // 
            // btn_Sure
            // 
            this.btn_Sure.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Sure.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Sure.Location = new System.Drawing.Point(5, 570);
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
            this.Threshold_image.Location = new System.Drawing.Point(105, 145);
            this.Threshold_image.Name = "Threshold_image";
            this.Threshold_image.Size = new System.Drawing.Size(80, 22);
            this.Threshold_image.TabIndex = 28;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(1, 547);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(199, 20);
            this.label13.TabIndex = 54;
            this.label13.Text = "注: 退出时,请点确定";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 293);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 55;
            this.button1.Text = "画圆";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lab_minScore
            // 
            this.lab_minScore.AutoSize = true;
            this.lab_minScore.Location = new System.Drawing.Point(10, 392);
            this.lab_minScore.Name = "lab_minScore";
            this.lab_minScore.Size = new System.Drawing.Size(53, 12);
            this.lab_minScore.TabIndex = 62;
            this.lab_minScore.Text = "卡尺宽度";
            // 
            // measure_length2
            // 
            this.measure_length2.Location = new System.Drawing.Point(101, 389);
            this.measure_length2.Multiline = true;
            this.measure_length2.Name = "measure_length2";
            this.measure_length2.Size = new System.Drawing.Size(84, 22);
            this.measure_length2.TabIndex = 64;
            this.measure_length2.Text = "10";
            // 
            // lab_num_measure
            // 
            this.lab_num_measure.AutoSize = true;
            this.lab_num_measure.Location = new System.Drawing.Point(10, 328);
            this.lab_num_measure.Name = "lab_num_measure";
            this.lab_num_measure.Size = new System.Drawing.Size(59, 12);
            this.lab_num_measure.TabIndex = 58;
            this.lab_num_measure.Text = "卡尺数量 ";
            // 
            // num_measure
            // 
            this.num_measure.Location = new System.Drawing.Point(101, 328);
            this.num_measure.Multiline = true;
            this.num_measure.Name = "num_measure";
            this.num_measure.Size = new System.Drawing.Size(84, 22);
            this.num_measure.TabIndex = 60;
            this.num_measure.Text = "30";
            // 
            // measure_length1
            // 
            this.measure_length1.Location = new System.Drawing.Point(101, 359);
            this.measure_length1.Multiline = true;
            this.measure_length1.Name = "measure_length1";
            this.measure_length1.Size = new System.Drawing.Size(84, 22);
            this.measure_length1.TabIndex = 61;
            this.measure_length1.Text = "30";
            // 
            // lab_measure_length1
            // 
            this.lab_measure_length1.AutoSize = true;
            this.lab_measure_length1.Location = new System.Drawing.Point(10, 362);
            this.lab_measure_length1.Name = "lab_measure_length1";
            this.lab_measure_length1.Size = new System.Drawing.Size(53, 12);
            this.lab_measure_length1.TabIndex = 59;
            this.lab_measure_length1.Text = "卡尺长度";
            // 
            // lab_measure_threshold
            // 
            this.lab_measure_threshold.AutoSize = true;
            this.lab_measure_threshold.Location = new System.Drawing.Point(10, 425);
            this.lab_measure_threshold.Name = "lab_measure_threshold";
            this.lab_measure_threshold.Size = new System.Drawing.Size(53, 12);
            this.lab_measure_threshold.TabIndex = 73;
            this.lab_measure_threshold.Text = "灰度阈值";
            // 
            // measure_threshold
            // 
            this.measure_threshold.Location = new System.Drawing.Point(101, 422);
            this.measure_threshold.Multiline = true;
            this.measure_threshold.Name = "measure_threshold";
            this.measure_threshold.Size = new System.Drawing.Size(84, 22);
            this.measure_threshold.TabIndex = 74;
            this.measure_threshold.Text = "10";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(89, 303);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 82;
            this.label8.Text = "颜色:";
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
            this.comboBox1.Location = new System.Drawing.Point(146, 295);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(89, 20);
            this.comboBox1.TabIndex = 81;
            this.comboBox1.Text = "red";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 479);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 84;
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
            this.cbb_measure_select.Location = new System.Drawing.Point(96, 474);
            this.cbb_measure_select.Name = "cbb_measure_select";
            this.cbb_measure_select.Size = new System.Drawing.Size(89, 22);
            this.cbb_measure_select.TabIndex = 83;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 453);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 86;
            this.label2.Text = "卡尺变化";
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
            this.cbb_transition.Location = new System.Drawing.Point(96, 448);
            this.cbb_transition.Name = "cbb_transition";
            this.cbb_transition.Size = new System.Drawing.Size(89, 22);
            this.cbb_transition.TabIndex = 85;
            // 
            // checkBox_circle
            // 
            this.checkBox_circle.AutoSize = true;
            this.checkBox_circle.Location = new System.Drawing.Point(5, 251);
            this.checkBox_circle.Name = "checkBox_circle";
            this.checkBox_circle.Size = new System.Drawing.Size(72, 16);
            this.checkBox_circle.TabIndex = 87;
            this.checkBox_circle.Text = "手动画圆";
            this.checkBox_circle.UseVisualStyleBackColor = true;
            // 
            // cbb_source1
            // 
            this.cbb_source1.FormattingEnabled = true;
            this.cbb_source1.Items.AddRange(new object[] {
            "匹配列表",
            "全局点列表"});
            this.cbb_source1.Location = new System.Drawing.Point(12, 189);
            this.cbb_source1.Name = "cbb_source1";
            this.cbb_source1.Size = new System.Drawing.Size(112, 20);
            this.cbb_source1.TabIndex = 88;
            // 
            // tb_value1
            // 
            this.tb_value1.Location = new System.Drawing.Point(156, 189);
            this.tb_value1.Name = "tb_value1";
            this.tb_value1.Size = new System.Drawing.Size(44, 21);
            this.tb_value1.TabIndex = 89;
            this.tb_value1.Text = "0";
            // 
            // textBox_radius
            // 
            this.textBox_radius.Location = new System.Drawing.Point(156, 225);
            this.textBox_radius.Name = "textBox_radius";
            this.textBox_radius.Size = new System.Drawing.Size(44, 21);
            this.textBox_radius.TabIndex = 90;
            this.textBox_radius.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 91;
            this.label3.Text = "半径:";
            // 
            // FindCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(247, 717);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_radius);
            this.Controls.Add(this.cbb_source1);
            this.Controls.Add(this.tb_value1);
            this.Controls.Add(this.checkBox_circle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbb_transition);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbb_measure_select);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lab_image);
            this.Controls.Add(this.comboBox1);
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
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.Threshold_image);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Name = "FindCircle";
            this.Text = "FindCircle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lab_image;
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbb_measure_select;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbb_transition;
        private System.Windows.Forms.CheckBox checkBox_circle;
        private System.Windows.Forms.ComboBox cbb_source1;
        private System.Windows.Forms.TextBox tb_value1;
        private System.Windows.Forms.TextBox textBox_radius;
        private System.Windows.Forms.Label label3;
    }
}