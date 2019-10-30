namespace PMACam
{
    partial class RegionProcess
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
            this.cbb_operation = new System.Windows.Forms.ComboBox();
            this.cbb_features = new System.Windows.Forms.ComboBox();
            this.cbb_regions = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lab_regions = new System.Windows.Forms.Label();
            this.lab_features = new System.Windows.Forms.Label();
            this.lab_operation = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lab_min = new System.Windows.Forms.Label();
            this.lab_max = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_min = new System.Windows.Forms.TextBox();
            this.txt_max = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_outnumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "区域处理";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(135, 417);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(64, 21);
            this.btn_Cancel.TabIndex = 38;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Sure
            // 
            this.btn_Sure.Location = new System.Drawing.Point(63, 417);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(64, 21);
            this.btn_Sure.TabIndex = 37;
            this.btn_Sure.Text = "确定";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // cbb_operation
            // 
            this.cbb_operation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_operation.FormattingEnabled = true;
            this.cbb_operation.Items.AddRange(new object[] {
            "and",
            "or"});
            this.cbb_operation.Location = new System.Drawing.Point(110, 168);
            this.cbb_operation.Name = "cbb_operation";
            this.cbb_operation.Size = new System.Drawing.Size(98, 20);
            this.cbb_operation.TabIndex = 34;
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
            this.cbb_features.Location = new System.Drawing.Point(110, 140);
            this.cbb_features.Name = "cbb_features";
            this.cbb_features.Size = new System.Drawing.Size(98, 20);
            this.cbb_features.TabIndex = 33;
            // 
            // cbb_regions
            // 
            this.cbb_regions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_regions.FormattingEnabled = true;
            this.cbb_regions.Location = new System.Drawing.Point(111, 114);
            this.cbb_regions.Name = "cbb_regions";
            this.cbb_regions.Size = new System.Drawing.Size(98, 20);
            this.cbb_regions.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "输入";
            // 
            // lab_regions
            // 
            this.lab_regions.AutoSize = true;
            this.lab_regions.Location = new System.Drawing.Point(4, 33);
            this.lab_regions.Name = "lab_regions";
            this.lab_regions.Size = new System.Drawing.Size(29, 12);
            this.lab_regions.TabIndex = 1;
            this.lab_regions.Text = "区域";
            // 
            // lab_features
            // 
            this.lab_features.AutoSize = true;
            this.lab_features.Location = new System.Drawing.Point(4, 65);
            this.lab_features.Name = "lab_features";
            this.lab_features.Size = new System.Drawing.Size(29, 12);
            this.lab_features.TabIndex = 3;
            this.lab_features.Text = "特征";
            // 
            // lab_operation
            // 
            this.lab_operation.AutoSize = true;
            this.lab_operation.Location = new System.Drawing.Point(4, 93);
            this.lab_operation.Name = "lab_operation";
            this.lab_operation.Size = new System.Drawing.Size(29, 12);
            this.lab_operation.TabIndex = 4;
            this.lab_operation.Text = "操作";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "输入";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "输入";
            // 
            // lab_min
            // 
            this.lab_min.AutoSize = true;
            this.lab_min.Location = new System.Drawing.Point(6, 116);
            this.lab_min.Name = "lab_min";
            this.lab_min.Size = new System.Drawing.Size(47, 12);
            this.lab_min.TabIndex = 7;
            this.lab_min.Text = "最小值 ";
            // 
            // lab_max
            // 
            this.lab_max.AutoSize = true;
            this.lab_max.Location = new System.Drawing.Point(4, 144);
            this.lab_max.Name = "lab_max";
            this.lab_max.Size = new System.Drawing.Size(47, 12);
            this.lab_max.TabIndex = 8;
            this.lab_max.Text = "最大值 ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lab_max);
            this.groupBox1.Controls.Add(this.lab_min);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lab_operation);
            this.groupBox1.Controls.Add(this.lab_features);
            this.groupBox1.Controls.Add(this.lab_regions);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(7, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(98, 163);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // txt_min
            // 
            this.txt_min.Location = new System.Drawing.Point(111, 196);
            this.txt_min.Multiline = true;
            this.txt_min.Name = "txt_min";
            this.txt_min.Size = new System.Drawing.Size(98, 22);
            this.txt_min.TabIndex = 35;
            this.txt_min.Text = "0";
            // 
            // txt_max
            // 
            this.txt_max.Location = new System.Drawing.Point(110, 224);
            this.txt_max.Multiline = true;
            this.txt_max.Name = "txt_max";
            this.txt_max.Size = new System.Drawing.Size(98, 22);
            this.txt_max.TabIndex = 36;
            this.txt_max.Text = "100000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 12);
            this.label3.TabIndex = 159;
            this.label3.Text = "全局点列表,可以用于暂存点数据";
            // 
            // tb_outnumber
            // 
            this.tb_outnumber.Location = new System.Drawing.Point(112, 268);
            this.tb_outnumber.Name = "tb_outnumber";
            this.tb_outnumber.Size = new System.Drawing.Size(94, 21);
            this.tb_outnumber.TabIndex = 158;
            this.tb_outnumber.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(8, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 14);
            this.label4.TabIndex = 157;
            this.label4.Text = "输出点位编号";
            // 
            // RegionProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(220, 526);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_outnumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.txt_max);
            this.Controls.Add(this.txt_min);
            this.Controls.Add(this.cbb_operation);
            this.Controls.Add(this.cbb_features);
            this.Controls.Add(this.cbb_regions);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "RegionProcess";
            this.Text = "RegionProcess";

            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.ComboBox cbb_operation;
        private System.Windows.Forms.ComboBox cbb_features;
        private System.Windows.Forms.ComboBox cbb_regions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lab_regions;
        private System.Windows.Forms.Label lab_features;
        private System.Windows.Forms.Label lab_operation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lab_min;
        private System.Windows.Forms.Label lab_max;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_min;
        private System.Windows.Forms.TextBox txt_max;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_outnumber;
        private System.Windows.Forms.Label label4;
    }
}