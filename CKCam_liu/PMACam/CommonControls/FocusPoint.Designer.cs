namespace PMACam 
{
    partial class FocusPoint
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
            this.label3 = new System.Windows.Forms.Label();
            this.tb_outnumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label_show = new System.Windows.Forms.Label();
            this.cbb_image = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label.Location = new System.Drawing.Point(5, 26);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(106, 24);
            this.label.TabIndex = 0;
            this.label.Text = "像素距离";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 152;
            this.label3.Text = "输出点位编号";
            // 
            // tb_outnumber
            // 
            this.tb_outnumber.Location = new System.Drawing.Point(121, 151);
            this.tb_outnumber.Name = "tb_outnumber";
            this.tb_outnumber.Size = new System.Drawing.Size(94, 21);
            this.tb_outnumber.TabIndex = 153;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 350);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 12);
            this.label6.TabIndex = 156;
            this.label6.Text = "全局点列表,可以用于暂存点数据";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 406);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 157;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(3, 373);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 20);
            this.label4.TabIndex = 167;
            this.label4.Text = "注: 退出时,请点确定";
            // 
            // label_show
            // 
            this.label_show.AutoSize = true;
            this.label_show.Location = new System.Drawing.Point(9, 326);
            this.label_show.Name = "label_show";
            this.label_show.Size = new System.Drawing.Size(0, 12);
            this.label_show.TabIndex = 180;
            // 
            // cbb_image
            // 
            this.cbb_image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_image.FormattingEnabled = true;
            this.cbb_image.Location = new System.Drawing.Point(121, 98);
            this.cbb_image.Name = "cbb_image";
            this.cbb_image.Size = new System.Drawing.Size(94, 20);
            this.cbb_image.TabIndex = 182;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 25);
            this.button2.TabIndex = 181;
            this.button2.Text = "图像";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FocusPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(227, 526);
            this.Controls.Add(this.cbb_image);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label_show);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_outnumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label);
            this.Name = "FocusPoint";
            this.Text = "PointLine";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_outnumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_show;
        private System.Windows.Forms.ComboBox cbb_image;
        private System.Windows.Forms.Button button2;
    }
}