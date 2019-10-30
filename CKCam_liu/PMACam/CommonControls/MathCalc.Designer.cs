namespace PMACam 
{
    partial class MathCalc
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
            this.cbb_CalcType = new System.Windows.Forms.ComboBox();
            this.cbb_Inputsource = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_outnumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_innumber = new System.Windows.Forms.TextBox();
            this.label_input1 = new System.Windows.Forms.Label();
            this.label_input2 = new System.Windows.Forms.Label();
            this.txt_number2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbb_inputsource2 = new System.Windows.Forms.ComboBox();
            this.panel_inno1 = new System.Windows.Forms.Panel();
            this.panel_inno2 = new System.Windows.Forms.Panel();
            this.tb_inputx = new System.Windows.Forms.TextBox();
            this.tb_inputy = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lab_Y = new System.Windows.Forms.Label();
            this.lab_X = new System.Windows.Forms.Label();
            this.panel_input = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cbb_tri = new System.Windows.Forms.ComboBox();
            this.tb_angle = new System.Windows.Forms.TextBox();
            this.check_angle = new System.Windows.Forms.CheckBox();
            this.cbb_CalcType1 = new System.Windows.Forms.ComboBox();
            this.cbb_tri1 = new System.Windows.Forms.ComboBox();
            this.tb_angle1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_inno1.SuspendLayout();
            this.panel_inno2.SuspendLayout();
            this.panel_input.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "算术计算";
            // 
            // cbb_CalcType
            // 
            this.cbb_CalcType.FormattingEnabled = true;
            this.cbb_CalcType.Items.AddRange(new object[] {
            "Add",
            "Sub1",
            "Sub2"});
            this.cbb_CalcType.Location = new System.Drawing.Point(2, 161);
            this.cbb_CalcType.Name = "cbb_CalcType";
            this.cbb_CalcType.Size = new System.Drawing.Size(66, 20);
            this.cbb_CalcType.TabIndex = 1;
            // 
            // cbb_Inputsource
            // 
            this.cbb_Inputsource.FormattingEnabled = true;
            this.cbb_Inputsource.Items.AddRange(new object[] {
            "找点列表",
            "匹配列表",
            "找圆列表",
            "全局点列表"});
            this.cbb_Inputsource.Location = new System.Drawing.Point(99, 71);
            this.cbb_Inputsource.Name = "cbb_Inputsource";
            this.cbb_Inputsource.Size = new System.Drawing.Size(93, 20);
            this.cbb_Inputsource.TabIndex = 150;
            this.cbb_Inputsource.SelectedIndexChanged += new System.EventHandler(this.cbb_Inputsource_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(26, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 14);
            this.label13.TabIndex = 151;
            this.label13.Text = "输入1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(4, 410);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 152;
            this.label3.Text = "输出点位编号";
            // 
            // tb_outnumber
            // 
            this.tb_outnumber.Location = new System.Drawing.Point(108, 403);
            this.tb_outnumber.Name = "tb_outnumber";
            this.tb_outnumber.Size = new System.Drawing.Size(94, 21);
            this.tb_outnumber.TabIndex = 153;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 440);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 12);
            this.label6.TabIndex = 156;
            this.label6.Text = "全局点列表,可以用于暂存点数据";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 496);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 157;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_innumber
            // 
            this.tb_innumber.Location = new System.Drawing.Point(97, 8);
            this.tb_innumber.Name = "tb_innumber";
            this.tb_innumber.Size = new System.Drawing.Size(93, 21);
            this.tb_innumber.TabIndex = 158;
            this.tb_innumber.Text = "0";
            // 
            // label_input1
            // 
            this.label_input1.AutoSize = true;
            this.label_input1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_input1.Location = new System.Drawing.Point(3, 10);
            this.label_input1.Name = "label_input1";
            this.label_input1.Size = new System.Drawing.Size(70, 14);
            this.label_input1.TabIndex = 159;
            this.label_input1.Text = "输入编号1";
            // 
            // label_input2
            // 
            this.label_input2.AutoSize = true;
            this.label_input2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_input2.Location = new System.Drawing.Point(6, 5);
            this.label_input2.Name = "label_input2";
            this.label_input2.Size = new System.Drawing.Size(70, 14);
            this.label_input2.TabIndex = 161;
            this.label_input2.Text = "输入编号2";
            // 
            // txt_number2
            // 
            this.txt_number2.AutoCompleteCustomSource.AddRange(new string[] {
            "全局点列表"});
            this.txt_number2.Location = new System.Drawing.Point(93, 4);
            this.txt_number2.Name = "txt_number2";
            this.txt_number2.Size = new System.Drawing.Size(93, 21);
            this.txt_number2.TabIndex = 160;
            this.txt_number2.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(22, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 162;
            this.label7.Text = "输入2";
            // 
            // cbb_inputsource2
            // 
            this.cbb_inputsource2.FormattingEnabled = true;
            this.cbb_inputsource2.Items.AddRange(new object[] {
            "全局点列表",
            "手动输入"});
            this.cbb_inputsource2.Location = new System.Drawing.Point(95, 230);
            this.cbb_inputsource2.Name = "cbb_inputsource2";
            this.cbb_inputsource2.Size = new System.Drawing.Size(93, 20);
            this.cbb_inputsource2.TabIndex = 163;
            this.cbb_inputsource2.SelectedIndexChanged += new System.EventHandler(this.cbb_inputsource2_SelectedIndexChanged);
            // 
            // panel_inno1
            // 
            this.panel_inno1.Controls.Add(this.label_input1);
            this.panel_inno1.Controls.Add(this.tb_innumber);
            this.panel_inno1.Location = new System.Drawing.Point(2, 94);
            this.panel_inno1.Name = "panel_inno1";
            this.panel_inno1.Size = new System.Drawing.Size(190, 34);
            this.panel_inno1.TabIndex = 164;
            // 
            // panel_inno2
            // 
            this.panel_inno2.Controls.Add(this.label_input2);
            this.panel_inno2.Controls.Add(this.txt_number2);
            this.panel_inno2.Location = new System.Drawing.Point(2, 256);
            this.panel_inno2.Name = "panel_inno2";
            this.panel_inno2.Size = new System.Drawing.Size(198, 31);
            this.panel_inno2.TabIndex = 165;
            // 
            // tb_inputx
            // 
            this.tb_inputx.Location = new System.Drawing.Point(84, 8);
            this.tb_inputx.Name = "tb_inputx";
            this.tb_inputx.Size = new System.Drawing.Size(93, 21);
            this.tb_inputx.TabIndex = 154;
            this.tb_inputx.Text = "0";
            // 
            // tb_inputy
            // 
            this.tb_inputy.Location = new System.Drawing.Point(84, 35);
            this.tb_inputy.Name = "tb_inputy";
            this.tb_inputy.Size = new System.Drawing.Size(93, 21);
            this.tb_inputy.TabIndex = 155;
            this.tb_inputy.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(37, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 127;
            this.label11.Text = "输入";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(37, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 126;
            this.label12.Text = "输入";
            // 
            // lab_Y
            // 
            this.lab_Y.AutoSize = true;
            this.lab_Y.Location = new System.Drawing.Point(7, 44);
            this.lab_Y.Name = "lab_Y";
            this.lab_Y.Size = new System.Drawing.Size(17, 12);
            this.lab_Y.TabIndex = 125;
            this.lab_Y.Text = "Y2";
            // 
            // lab_X
            // 
            this.lab_X.AutoSize = true;
            this.lab_X.Location = new System.Drawing.Point(7, 11);
            this.lab_X.Name = "lab_X";
            this.lab_X.Size = new System.Drawing.Size(17, 12);
            this.lab_X.TabIndex = 124;
            this.lab_X.Text = "X2";
            // 
            // panel_input
            // 
            this.panel_input.Controls.Add(this.lab_X);
            this.panel_input.Controls.Add(this.lab_Y);
            this.panel_input.Controls.Add(this.label12);
            this.panel_input.Controls.Add(this.label11);
            this.panel_input.Controls.Add(this.tb_inputy);
            this.panel_input.Controls.Add(this.tb_inputx);
            this.panel_input.Location = new System.Drawing.Point(11, 293);
            this.panel_input.Name = "panel_input";
            this.panel_input.Size = new System.Drawing.Size(189, 68);
            this.panel_input.TabIndex = 166;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(3, 463);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 20);
            this.label4.TabIndex = 167;
            this.label4.Text = "注: 退出时,请点确定";
            // 
            // cbb_tri
            // 
            this.cbb_tri.FormattingEnabled = true;
            this.cbb_tri.Items.AddRange(new object[] {
            "cos",
            "sin"});
            this.cbb_tri.Location = new System.Drawing.Point(74, 161);
            this.cbb_tri.Name = "cbb_tri";
            this.cbb_tri.Size = new System.Drawing.Size(66, 20);
            this.cbb_tri.TabIndex = 168;
            // 
            // tb_angle
            // 
            this.tb_angle.Location = new System.Drawing.Point(156, 160);
            this.tb_angle.Name = "tb_angle";
            this.tb_angle.Size = new System.Drawing.Size(59, 21);
            this.tb_angle.TabIndex = 169;
            this.tb_angle.Text = "0";
            // 
            // check_angle
            // 
            this.check_angle.AutoSize = true;
            this.check_angle.Location = new System.Drawing.Point(156, 134);
            this.check_angle.Name = "check_angle";
            this.check_angle.Size = new System.Drawing.Size(72, 16);
            this.check_angle.TabIndex = 170;
            this.check_angle.Text = "匹配角度";
            this.check_angle.UseVisualStyleBackColor = true;
            // 
            // cbb_CalcType1
            // 
            this.cbb_CalcType1.FormattingEnabled = true;
            this.cbb_CalcType1.Items.AddRange(new object[] {
            "Add",
            "Sub1",
            "Sub2"});
            this.cbb_CalcType1.Location = new System.Drawing.Point(2, 188);
            this.cbb_CalcType1.Name = "cbb_CalcType1";
            this.cbb_CalcType1.Size = new System.Drawing.Size(66, 20);
            this.cbb_CalcType1.TabIndex = 171;
            // 
            // cbb_tri1
            // 
            this.cbb_tri1.FormattingEnabled = true;
            this.cbb_tri1.Items.AddRange(new object[] {
            "cos",
            "sin"});
            this.cbb_tri1.Location = new System.Drawing.Point(74, 188);
            this.cbb_tri1.Name = "cbb_tri1";
            this.cbb_tri1.Size = new System.Drawing.Size(66, 20);
            this.cbb_tri1.TabIndex = 172;
            // 
            // tb_angle1
            // 
            this.tb_angle1.Location = new System.Drawing.Point(156, 188);
            this.tb_angle1.Name = "tb_angle1";
            this.tb_angle1.Size = new System.Drawing.Size(59, 21);
            this.tb_angle1.TabIndex = 173;
            this.tb_angle1.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 382);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 174;
            this.label2.Text = "0";
            // 
            // MathCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(227, 639);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_angle1);
            this.Controls.Add(this.cbb_tri1);
            this.Controls.Add(this.cbb_CalcType1);
            this.Controls.Add(this.check_angle);
            this.Controls.Add(this.tb_angle);
            this.Controls.Add(this.cbb_tri);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel_input);
            this.Controls.Add(this.panel_inno2);
            this.Controls.Add(this.panel_inno1);
            this.Controls.Add(this.cbb_inputsource2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_outnumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cbb_Inputsource);
            this.Controls.Add(this.cbb_CalcType);
            this.Controls.Add(this.label1);
            this.Name = "MathCalc";
            this.Text = "MathCalc";
            this.panel_inno1.ResumeLayout(false);
            this.panel_inno1.PerformLayout();
            this.panel_inno2.ResumeLayout(false);
            this.panel_inno2.PerformLayout();
            this.panel_input.ResumeLayout(false);
            this.panel_input.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbb_CalcType;
        private System.Windows.Forms.ComboBox cbb_Inputsource;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_outnumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_innumber;
        private System.Windows.Forms.Label label_input1;
        private System.Windows.Forms.Label label_input2;
        private System.Windows.Forms.TextBox txt_number2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbb_inputsource2;
        private System.Windows.Forms.Panel panel_inno1;
        private System.Windows.Forms.Panel panel_inno2;
        private System.Windows.Forms.TextBox tb_inputx;
        private System.Windows.Forms.TextBox tb_inputy;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lab_Y;
        private System.Windows.Forms.Label lab_X;
        private System.Windows.Forms.Panel panel_input;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbb_tri;
        private System.Windows.Forms.TextBox tb_angle;
        private System.Windows.Forms.CheckBox check_angle;
        private System.Windows.Forms.ComboBox cbb_CalcType1;
        private System.Windows.Forms.ComboBox cbb_tri1;
        private System.Windows.Forms.TextBox tb_angle1;
        private System.Windows.Forms.Label label2;
    }
}