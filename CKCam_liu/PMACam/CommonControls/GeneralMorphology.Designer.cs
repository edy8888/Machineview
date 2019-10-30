namespace PMACam
{
    partial class GeneralMorphology
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_iterations = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.mor_structElement = new System.Windows.Forms.ComboBox();
            this.mor_region = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lab_iterations = new System.Windows.Forms.Label();
            this.lab_regionErosion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lab_region = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lab_structElement = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.mor_regionout = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "通用形态学";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "打开",
            "关闭",
            "腐蚀",
            "膨胀"});
            this.comboBox1.Location = new System.Drawing.Point(80, 109);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "打开";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "形态操作:";
            // 
            // txt_iterations
            // 
            this.txt_iterations.Location = new System.Drawing.Point(113, 362);
            this.txt_iterations.Multiline = true;
            this.txt_iterations.Name = "txt_iterations";
            this.txt_iterations.Size = new System.Drawing.Size(106, 22);
            this.txt_iterations.TabIndex = 52;
            this.txt_iterations.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(113, 312);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 51;
            this.label6.Text = "命名规范:*_b";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(115, 449);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(64, 21);
            this.btn_Cancel.TabIndex = 50;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Sure
            // 
            this.btn_Sure.Location = new System.Drawing.Point(12, 449);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(64, 21);
            this.btn_Sure.TabIndex = 49;
            this.btn_Sure.Text = "确定";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // mor_structElement
            // 
            this.mor_structElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mor_structElement.FormattingEnabled = true;
            this.mor_structElement.Location = new System.Drawing.Point(113, 281);
            this.mor_structElement.Name = "mor_structElement";
            this.mor_structElement.Size = new System.Drawing.Size(106, 20);
            this.mor_structElement.TabIndex = 47;
            // 
            // mor_region
            // 
            this.mor_region.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mor_region.FormattingEnabled = true;
            this.mor_region.Location = new System.Drawing.Point(113, 243);
            this.mor_region.Name = "mor_region";
            this.mor_region.Size = new System.Drawing.Size(106, 20);
            this.mor_region.TabIndex = 46;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lab_iterations);
            this.groupBox1.Controls.Add(this.lab_regionErosion);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lab_region);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lab_structElement);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(14, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(94, 216);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 38;
            this.label7.Text = "in";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "in";
            // 
            // lab_iterations
            // 
            this.lab_iterations.AutoSize = true;
            this.lab_iterations.Location = new System.Drawing.Point(-3, 195);
            this.lab_iterations.Name = "lab_iterations";
            this.lab_iterations.Size = new System.Drawing.Size(65, 12);
            this.lab_iterations.TabIndex = 37;
            this.lab_iterations.Text = "iterations";
            // 
            // lab_regionErosion
            // 
            this.lab_regionErosion.AutoSize = true;
            this.lab_regionErosion.Location = new System.Drawing.Point(0, 160);
            this.lab_regionErosion.Name = "lab_regionErosion";
            this.lab_regionErosion.Size = new System.Drawing.Size(41, 12);
            this.lab_regionErosion.TabIndex = 3;
            this.lab_regionErosion.Text = "region";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "类型";
            // 
            // lab_region
            // 
            this.lab_region.AutoSize = true;
            this.lab_region.Location = new System.Drawing.Point(1, 78);
            this.lab_region.Name = "lab_region";
            this.lab_region.Size = new System.Drawing.Size(41, 12);
            this.lab_region.TabIndex = 1;
            this.lab_region.Text = "region";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "参数名";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(69, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "in";
            // 
            // lab_structElement
            // 
            this.lab_structElement.AutoSize = true;
            this.lab_structElement.Location = new System.Drawing.Point(1, 103);
            this.lab_structElement.Name = "lab_structElement";
            this.lab_structElement.Size = new System.Drawing.Size(83, 12);
            this.lab_structElement.TabIndex = 2;
            this.lab_structElement.Text = "structElement";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(70, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "out";
            // 
            // mor_regionout
            // 
            this.mor_regionout.FormattingEnabled = true;
            this.mor_regionout.Location = new System.Drawing.Point(115, 327);
            this.mor_regionout.Name = "mor_regionout";
            this.mor_regionout.Size = new System.Drawing.Size(104, 20);
            this.mor_regionout.TabIndex = 53;
            this.mor_regionout.Text = "Region1111";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(8, 413);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(199, 20);
            this.label10.TabIndex = 54;
            this.label10.Text = "注: 退出时,请点确定";
            // 
            // GeneralMorphology
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(227, 588);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.mor_regionout);
            this.Controls.Add(this.txt_iterations);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.mor_structElement);
            this.Controls.Add(this.mor_region);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Name = "GeneralMorphology";
            this.Text = "GeneralMorphology";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_iterations;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.ComboBox mor_structElement;
        private System.Windows.Forms.ComboBox mor_region;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lab_iterations;
        private System.Windows.Forms.Label lab_regionErosion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lab_region;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lab_structElement;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox mor_regionout;
        private System.Windows.Forms.Label label10;
    }
}