namespace PMACam 
{
    partial class LineLine
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
            this.label = new System.Windows.Forms.Label();
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
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_show = new System.Windows.Forms.Label();
            this.panel_inno1.SuspendLayout();
            this.panel_inno2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label.Location = new System.Drawing.Point(3, 36);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(106, 24);
            this.label.TabIndex = 0;
            this.label.Text = "点线距离";
            // 
            // cbb_Inputsource
            // 
            this.cbb_Inputsource.FormattingEnabled = true;
            this.cbb_Inputsource.Items.AddRange(new object[] {
            "直线列表"});
            this.cbb_Inputsource.Location = new System.Drawing.Point(99, 71);
            this.cbb_Inputsource.Name = "cbb_Inputsource";
            this.cbb_Inputsource.Size = new System.Drawing.Size(93, 20);
            this.cbb_Inputsource.TabIndex = 150;
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
            this.label3.Location = new System.Drawing.Point(6, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 152;
            this.label3.Text = "输出点位编号";
            // 
            // tb_outnumber
            // 
            this.tb_outnumber.Location = new System.Drawing.Point(102, 231);
            this.tb_outnumber.Name = "tb_outnumber";
            this.tb_outnumber.Size = new System.Drawing.Size(47, 21);
            this.tb_outnumber.TabIndex = 153;
            this.tb_outnumber.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 378);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 12);
            this.label6.TabIndex = 156;
            this.label6.Text = "全局点列表,可以用于暂存点数据";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 434);
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
            this.tb_innumber.Size = new System.Drawing.Size(57, 21);
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
            this.txt_number2.Location = new System.Drawing.Point(96, 4);
            this.txt_number2.Name = "txt_number2";
            this.txt_number2.Size = new System.Drawing.Size(54, 21);
            this.txt_number2.TabIndex = 160;
            this.txt_number2.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(12, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 162;
            this.label7.Text = "输入2";
            // 
            // cbb_inputsource2
            // 
            this.cbb_inputsource2.FormattingEnabled = true;
            this.cbb_inputsource2.Items.AddRange(new object[] {
            "直线列表"});
            this.cbb_inputsource2.Location = new System.Drawing.Point(102, 154);
            this.cbb_inputsource2.Name = "cbb_inputsource2";
            this.cbb_inputsource2.Size = new System.Drawing.Size(90, 20);
            this.cbb_inputsource2.TabIndex = 163;
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
            this.panel_inno2.Location = new System.Drawing.Point(6, 180);
            this.panel_inno2.Name = "panel_inno2";
            this.panel_inno2.Size = new System.Drawing.Size(198, 31);
            this.panel_inno2.TabIndex = 165;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(-2, 401);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 20);
            this.label4.TabIndex = 167;
            this.label4.Text = "注: 退出时,请点确定";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 170;
            this.label2.Text = "(存投影点)";
            // 
            // label_show
            // 
            this.label_show.AutoSize = true;
            this.label_show.Location = new System.Drawing.Point(8, 305);
            this.label_show.Name = "label_show";
            this.label_show.Size = new System.Drawing.Size(41, 12);
            this.label_show.TabIndex = 172;
            this.label_show.Text = "label8";
            // 
            // LineLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(239, 639);
            this.Controls.Add(this.label_show);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
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
            this.Controls.Add(this.label);
            this.Name = "LineLine";
            this.Text = "LineLine";
            this.panel_inno1.ResumeLayout(false);
            this.panel_inno1.PerformLayout();
            this.panel_inno2.ResumeLayout(false);
            this.panel_inno2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_show;
    }
}