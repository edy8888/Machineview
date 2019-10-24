namespace PMACam
{
    partial class BinaryPro
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
            this.thresold_region = new System.Windows.Forms.ComboBox();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.Threshold_image = new System.Windows.Forms.ComboBox();
            this.trackBar_min = new System.Windows.Forms.TrackBar();
            this.trackBar_max = new System.Windows.Forms.TrackBar();
            this.checkBox_dya = new System.Windows.Forms.CheckBox();
            this.comboBox_imageout = new System.Windows.Forms.ComboBox();
            this.checkBox_过滤 = new System.Windows.Forms.CheckBox();
            this.panel_参数设置3 = new System.Windows.Forms.Panel();
            this.txt_iterations = new System.Windows.Forms.TextBox();
            this.lab_iterations = new System.Windows.Forms.Label();
            this.mor_structElement = new System.Windows.Forms.ComboBox();
            this.lab_structElement = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox_form = new System.Windows.Forms.ComboBox();
            this.panel_参数设置2 = new System.Windows.Forms.Panel();
            this.lab_operation = new System.Windows.Forms.Label();
            this.lab_min = new System.Windows.Forms.Label();
            this.cbb_features = new System.Windows.Forms.ComboBox();
            this.lab_max = new System.Windows.Forms.Label();
            this.cbb_operation = new System.Windows.Forms.ComboBox();
            this.lab_features = new System.Windows.Forms.Label();
            this.txt_min = new System.Windows.Forms.TextBox();
            this.txt_max = new System.Windows.Forms.TextBox();
            this.button_参数设置2 = new System.Windows.Forms.Button();
            this.button_参数设置3 = new System.Windows.Forms.Button();
            this.checkBox_预处理 = new System.Windows.Forms.CheckBox();
            this.checkBox_black = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBox_show = new System.Windows.Forms.CheckBox();
            this.lab_selectedRegions = new System.Windows.Forms.Label();
            this.lab_image = new System.Windows.Forms.Label();
            this.lab_maxGray = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_number = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_max)).BeginInit();
            this.panel_参数设置3.SuspendLayout();
            this.panel_参数设置2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(1, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "二值化处理";
            // 
            // thresold_region
            // 
            this.thresold_region.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.thresold_region.FormattingEnabled = true;
            this.thresold_region.Location = new System.Drawing.Point(101, 117);
            this.thresold_region.Name = "thresold_region";
            this.thresold_region.Size = new System.Drawing.Size(87, 22);
            this.thresold_region.TabIndex = 29;
            this.thresold_region.Text = "region1";
            // 
            // btn_Sure
            // 
            this.btn_Sure.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Sure.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Sure.Location = new System.Drawing.Point(6, 309);
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
            this.Threshold_image.Location = new System.Drawing.Point(101, 81);
            this.Threshold_image.Name = "Threshold_image";
            this.Threshold_image.Size = new System.Drawing.Size(87, 22);
            this.Threshold_image.TabIndex = 28;
            // 
            // trackBar_min
            // 
            this.trackBar_min.Location = new System.Drawing.Point(94, 206);
            this.trackBar_min.Maximum = 255;
            this.trackBar_min.Name = "trackBar_min";
            this.trackBar_min.Size = new System.Drawing.Size(104, 45);
            this.trackBar_min.TabIndex = 35;
            this.trackBar_min.Value = 40;
            this.trackBar_min.Scroll += new System.EventHandler(this.trackBar_min_Scroll);
            this.trackBar_min.ValueChanged += new System.EventHandler(this.trackBar_min_ValueChanged);
            // 
            // trackBar_max
            // 
            this.trackBar_max.Location = new System.Drawing.Point(94, 239);
            this.trackBar_max.Maximum = 255;
            this.trackBar_max.Name = "trackBar_max";
            this.trackBar_max.Size = new System.Drawing.Size(104, 45);
            this.trackBar_max.TabIndex = 36;
            this.trackBar_max.Value = 90;
            this.trackBar_max.ValueChanged += new System.EventHandler(this.trackBar_max_ValueChanged);
            this.trackBar_max.Validating += new System.ComponentModel.CancelEventHandler(this.trackBar_max_Validating);
            // 
            // checkBox_dya
            // 
            this.checkBox_dya.AutoSize = true;
            this.checkBox_dya.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBox_dya.Location = new System.Drawing.Point(4, 54);
            this.checkBox_dya.Name = "checkBox_dya";
            this.checkBox_dya.Size = new System.Drawing.Size(48, 16);
            this.checkBox_dya.TabIndex = 37;
            this.checkBox_dya.Text = "实时";
            this.checkBox_dya.UseVisualStyleBackColor = true;
            // 
            // comboBox_imageout
            // 
            this.comboBox_imageout.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_imageout.FormattingEnabled = true;
            this.comboBox_imageout.Location = new System.Drawing.Point(101, 151);
            this.comboBox_imageout.Name = "comboBox_imageout";
            this.comboBox_imageout.Size = new System.Drawing.Size(87, 22);
            this.comboBox_imageout.TabIndex = 38;
            this.comboBox_imageout.Text = "image2";
            // 
            // checkBox_过滤
            // 
            this.checkBox_过滤.AutoSize = true;
            this.checkBox_过滤.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBox_过滤.Location = new System.Drawing.Point(155, 369);
            this.checkBox_过滤.Name = "checkBox_过滤";
            this.checkBox_过滤.Size = new System.Drawing.Size(48, 16);
            this.checkBox_过滤.TabIndex = 46;
            this.checkBox_过滤.Text = "过滤";
            this.checkBox_过滤.UseVisualStyleBackColor = true;
            // 
            // panel_参数设置3
            // 
            this.panel_参数设置3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_参数设置3.Controls.Add(this.txt_iterations);
            this.panel_参数设置3.Controls.Add(this.lab_iterations);
            this.panel_参数设置3.Controls.Add(this.mor_structElement);
            this.panel_参数设置3.Controls.Add(this.lab_structElement);
            this.panel_参数设置3.Controls.Add(this.label12);
            this.panel_参数设置3.Controls.Add(this.comboBox_form);
            this.panel_参数设置3.Location = new System.Drawing.Point(8, 394);
            this.panel_参数设置3.Name = "panel_参数设置3";
            this.panel_参数设置3.Size = new System.Drawing.Size(164, 118);
            this.panel_参数设置3.TabIndex = 52;
            // 
            // txt_iterations
            // 
            this.txt_iterations.Location = new System.Drawing.Point(66, 69);
            this.txt_iterations.Multiline = true;
            this.txt_iterations.Name = "txt_iterations";
            this.txt_iterations.Size = new System.Drawing.Size(79, 22);
            this.txt_iterations.TabIndex = 59;
            this.txt_iterations.Text = "1";
            // 
            // lab_iterations
            // 
            this.lab_iterations.AutoSize = true;
            this.lab_iterations.Location = new System.Drawing.Point(1, 72);
            this.lab_iterations.Name = "lab_iterations";
            this.lab_iterations.Size = new System.Drawing.Size(59, 12);
            this.lab_iterations.TabIndex = 57;
            this.lab_iterations.Text = " 迭代次数";
            // 
            // mor_structElement
            // 
            this.mor_structElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mor_structElement.FormattingEnabled = true;
            this.mor_structElement.Location = new System.Drawing.Point(66, 38);
            this.mor_structElement.Name = "mor_structElement";
            this.mor_structElement.Size = new System.Drawing.Size(79, 20);
            this.mor_structElement.TabIndex = 58;
            this.mor_structElement.SelectedIndexChanged += new System.EventHandler(this.mor_structElement_SelectedIndexChanged);
            // 
            // lab_structElement
            // 
            this.lab_structElement.AutoSize = true;
            this.lab_structElement.Location = new System.Drawing.Point(6, 41);
            this.lab_structElement.Name = "lab_structElement";
            this.lab_structElement.Size = new System.Drawing.Size(53, 12);
            this.lab_structElement.TabIndex = 55;
            this.lab_structElement.Text = "区域输入";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 56;
            this.label12.Text = "形态操作:";
            // 
            // comboBox_form
            // 
            this.comboBox_form.FormattingEnabled = true;
            this.comboBox_form.Items.AddRange(new object[] {
            "打开",
            "关闭",
            "腐蚀",
            "膨胀"});
            this.comboBox_form.Location = new System.Drawing.Point(66, 7);
            this.comboBox_form.Name = "comboBox_form";
            this.comboBox_form.Size = new System.Drawing.Size(79, 20);
            this.comboBox_form.TabIndex = 54;
            this.comboBox_form.Text = "打开";
            // 
            // panel_参数设置2
            // 
            this.panel_参数设置2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_参数设置2.Controls.Add(this.lab_operation);
            this.panel_参数设置2.Controls.Add(this.lab_min);
            this.panel_参数设置2.Controls.Add(this.cbb_features);
            this.panel_参数设置2.Controls.Add(this.lab_max);
            this.panel_参数设置2.Controls.Add(this.cbb_operation);
            this.panel_参数设置2.Controls.Add(this.lab_features);
            this.panel_参数设置2.Controls.Add(this.txt_min);
            this.panel_参数设置2.Controls.Add(this.txt_max);
            this.panel_参数设置2.Location = new System.Drawing.Point(8, 394);
            this.panel_参数设置2.Name = "panel_参数设置2";
            this.panel_参数设置2.Size = new System.Drawing.Size(164, 118);
            this.panel_参数设置2.TabIndex = 51;
            // 
            // lab_operation
            // 
            this.lab_operation.AutoSize = true;
            this.lab_operation.Location = new System.Drawing.Point(10, 36);
            this.lab_operation.Name = "lab_operation";
            this.lab_operation.Size = new System.Drawing.Size(29, 12);
            this.lab_operation.TabIndex = 45;
            this.lab_operation.Text = "操作";
            // 
            // lab_min
            // 
            this.lab_min.AutoSize = true;
            this.lab_min.Location = new System.Drawing.Point(11, 60);
            this.lab_min.Name = "lab_min";
            this.lab_min.Size = new System.Drawing.Size(41, 12);
            this.lab_min.TabIndex = 46;
            this.lab_min.Text = "最小值";
            // 
            // cbb_features
            // 
            this.cbb_features.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_features.FormattingEnabled = true;
            this.cbb_features.Items.AddRange(new object[] {
            "area",
            "row",
            "column",
            "width",
            "height",
            "circularity",
            "Compactness",
            "contlength",
            "convexity",
            "rectangularity",
            "ra",
            "rb",
            "phi",
            "anisometry",
            "outer_radius",
            "inner_radius",
            "inner_width",
            "inner_height",
            "rect2_phi",
            "rect2_len1",
            "rect2_len2"});
            this.cbb_features.Location = new System.Drawing.Point(66, 7);
            this.cbb_features.Name = "cbb_features";
            this.cbb_features.Size = new System.Drawing.Size(69, 20);
            this.cbb_features.TabIndex = 48;
            // 
            // lab_max
            // 
            this.lab_max.AutoSize = true;
            this.lab_max.Location = new System.Drawing.Point(11, 97);
            this.lab_max.Name = "lab_max";
            this.lab_max.Size = new System.Drawing.Size(41, 12);
            this.lab_max.TabIndex = 47;
            this.lab_max.Text = "最大值";
            // 
            // cbb_operation
            // 
            this.cbb_operation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_operation.FormattingEnabled = true;
            this.cbb_operation.Items.AddRange(new object[] {
            "and",
            "or"});
            this.cbb_operation.Location = new System.Drawing.Point(64, 33);
            this.cbb_operation.Name = "cbb_operation";
            this.cbb_operation.Size = new System.Drawing.Size(73, 20);
            this.cbb_operation.TabIndex = 49;
            // 
            // lab_features
            // 
            this.lab_features.AutoSize = true;
            this.lab_features.Location = new System.Drawing.Point(11, 10);
            this.lab_features.Name = "lab_features";
            this.lab_features.Size = new System.Drawing.Size(29, 12);
            this.lab_features.TabIndex = 44;
            this.lab_features.Text = "特征";
            // 
            // txt_min
            // 
            this.txt_min.Location = new System.Drawing.Point(66, 56);
            this.txt_min.Multiline = true;
            this.txt_min.Name = "txt_min";
            this.txt_min.Size = new System.Drawing.Size(72, 22);
            this.txt_min.TabIndex = 50;
            this.txt_min.Text = "0";
            // 
            // txt_max
            // 
            this.txt_max.Location = new System.Drawing.Point(64, 87);
            this.txt_max.Multiline = true;
            this.txt_max.Name = "txt_max";
            this.txt_max.Size = new System.Drawing.Size(72, 22);
            this.txt_max.TabIndex = 51;
            this.txt_max.Text = "10000";
            // 
            // button_参数设置2
            // 
            this.button_参数设置2.Location = new System.Drawing.Point(8, 362);
            this.button_参数设置2.Name = "button_参数设置2";
            this.button_参数设置2.Size = new System.Drawing.Size(135, 23);
            this.button_参数设置2.TabIndex = 48;
            this.button_参数设置2.Text = "过滤";
            this.button_参数设置2.UseVisualStyleBackColor = true;
            this.button_参数设置2.Click += new System.EventHandler(this.button_参数设置2_Click);
            // 
            // button_参数设置3
            // 
            this.button_参数设置3.Location = new System.Drawing.Point(7, 336);
            this.button_参数设置3.Name = "button_参数设置3";
            this.button_参数设置3.Size = new System.Drawing.Size(136, 23);
            this.button_参数设置3.TabIndex = 49;
            this.button_参数设置3.Text = "通用形态";
            this.button_参数设置3.UseVisualStyleBackColor = true;
            this.button_参数设置3.Click += new System.EventHandler(this.button_参数设置3_Click);
            // 
            // checkBox_预处理
            // 
            this.checkBox_预处理.AutoSize = true;
            this.checkBox_预处理.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBox_预处理.Location = new System.Drawing.Point(156, 340);
            this.checkBox_预处理.Name = "checkBox_预处理";
            this.checkBox_预处理.Size = new System.Drawing.Size(60, 16);
            this.checkBox_预处理.TabIndex = 53;
            this.checkBox_预处理.Text = "预处理";
            this.checkBox_预处理.UseVisualStyleBackColor = true;
            // 
            // checkBox_black
            // 
            this.checkBox_black.AutoSize = true;
            this.checkBox_black.Location = new System.Drawing.Point(101, 184);
            this.checkBox_black.Name = "checkBox_black";
            this.checkBox_black.Size = new System.Drawing.Size(72, 16);
            this.checkBox_black.TabIndex = 10;
            this.checkBox_black.Text = "黑底白点";
            this.checkBox_black.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(4, 277);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(199, 20);
            this.label13.TabIndex = 54;
            this.label13.Text = "注: 退出时,请点确定";
            // 
            // checkBox_show
            // 
            this.checkBox_show.AutoSize = true;
            this.checkBox_show.Location = new System.Drawing.Point(101, 54);
            this.checkBox_show.Name = "checkBox_show";
            this.checkBox_show.Size = new System.Drawing.Size(72, 16);
            this.checkBox_show.TabIndex = 55;
            this.checkBox_show.Text = "显示图像";
            this.checkBox_show.UseVisualStyleBackColor = true;
            // 
            // lab_selectedRegions
            // 
            this.lab_selectedRegions.AutoSize = true;
            this.lab_selectedRegions.Location = new System.Drawing.Point(4, 122);
            this.lab_selectedRegions.Name = "lab_selectedRegions";
            this.lab_selectedRegions.Size = new System.Drawing.Size(53, 12);
            this.lab_selectedRegions.TabIndex = 2;
            this.lab_selectedRegions.Text = "区域输出";
            // 
            // lab_image
            // 
            this.lab_image.AutoSize = true;
            this.lab_image.Location = new System.Drawing.Point(4, 86);
            this.lab_image.Name = "lab_image";
            this.lab_image.Size = new System.Drawing.Size(53, 12);
            this.lab_image.TabIndex = 1;
            this.lab_image.Text = "图像输入";
            // 
            // lab_maxGray
            // 
            this.lab_maxGray.AutoSize = true;
            this.lab_maxGray.Location = new System.Drawing.Point(5, 251);
            this.lab_maxGray.Name = "lab_maxGray";
            this.lab_maxGray.Size = new System.Drawing.Size(65, 12);
            this.lab_maxGray.TabIndex = 4;
            this.lab_maxGray.Text = "最大值输入";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "图像输出";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "最小值输入";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 523);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 56;
            this.label2.Text = "区域个数：";
            // 
            // label_number
            // 
            this.label_number.AutoSize = true;
            this.label_number.Location = new System.Drawing.Point(74, 523);
            this.label_number.Name = "label_number";
            this.label_number.Size = new System.Drawing.Size(11, 12);
            this.label_number.TabIndex = 57;
            this.label_number.Text = "0";
            // 
            // BinaryPro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(218, 547);
            this.Controls.Add(this.label_number);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox_show);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.checkBox_black);
            this.Controls.Add(this.checkBox_预处理);
            this.Controls.Add(this.lab_maxGray);
            this.Controls.Add(this.button_参数设置3);
            this.Controls.Add(this.button_参数设置2);
            this.Controls.Add(this.lab_image);
            this.Controls.Add(this.checkBox_过滤);
            this.Controls.Add(this.comboBox_imageout);
            this.Controls.Add(this.checkBox_dya);
            this.Controls.Add(this.lab_selectedRegions);
            this.Controls.Add(this.trackBar_max);
            this.Controls.Add(this.trackBar_min);
            this.Controls.Add(this.thresold_region);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.Threshold_image);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel_参数设置3);
            this.Controls.Add(this.panel_参数设置2);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Name = "BinaryPro";
            this.Text = "BinaryPro";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_max)).EndInit();
            this.panel_参数设置3.ResumeLayout(false);
            this.panel_参数设置3.PerformLayout();
            this.panel_参数设置2.ResumeLayout(false);
            this.panel_参数设置2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox thresold_region;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.ComboBox Threshold_image;
        private System.Windows.Forms.TrackBar trackBar_min;
        private System.Windows.Forms.TrackBar trackBar_max;
        private System.Windows.Forms.CheckBox checkBox_dya;
        private System.Windows.Forms.ComboBox comboBox_imageout;
        private System.Windows.Forms.CheckBox checkBox_过滤;
        private System.Windows.Forms.Panel panel_参数设置3;
        private System.Windows.Forms.Panel panel_参数设置2;
        private System.Windows.Forms.Button button_参数设置2;
        private System.Windows.Forms.Button button_参数设置3;
        private System.Windows.Forms.CheckBox checkBox_预处理;
        private System.Windows.Forms.Label lab_operation;
        private System.Windows.Forms.Label lab_min;
        private System.Windows.Forms.ComboBox cbb_features;
        private System.Windows.Forms.Label lab_max;
        private System.Windows.Forms.ComboBox cbb_operation;
        private System.Windows.Forms.Label lab_features;
        private System.Windows.Forms.TextBox txt_min;
        private System.Windows.Forms.TextBox txt_max;
        private System.Windows.Forms.TextBox txt_iterations;
        private System.Windows.Forms.Label lab_iterations;
        private System.Windows.Forms.ComboBox mor_structElement;
        private System.Windows.Forms.Label lab_structElement;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox_form;
        private System.Windows.Forms.CheckBox checkBox_black;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox checkBox_show;
        private System.Windows.Forms.Label lab_selectedRegions;
        private System.Windows.Forms.Label lab_image;
        private System.Windows.Forms.Label lab_maxGray;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_number;
    }
}