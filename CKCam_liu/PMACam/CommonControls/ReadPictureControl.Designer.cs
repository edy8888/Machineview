namespace PMACam
{
    partial class ReadPictureControl
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
            this.button_文件位置 = new System.Windows.Forms.Button();
            this.textBox_file = new System.Windows.Forms.TextBox();
            this.checkBox_相机 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.read_ImageName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lab_Image = new System.Windows.Forms.Label();
            this.checkBox_exp = new System.Windows.Forms.CheckBox();
            this.button_exp = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_文件位置
            // 
            this.button_文件位置.Location = new System.Drawing.Point(3, 91);
            this.button_文件位置.Name = "button_文件位置";
            this.button_文件位置.Size = new System.Drawing.Size(75, 23);
            this.button_文件位置.TabIndex = 4;
            this.button_文件位置.Text = "文件位置";
            this.button_文件位置.UseVisualStyleBackColor = true;
            this.button_文件位置.Click += new System.EventHandler(this.button_文件位置_Click);
            // 
            // textBox_file
            // 
            this.textBox_file.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_file.Location = new System.Drawing.Point(6, 120);
            this.textBox_file.Name = "textBox_file";
            this.textBox_file.Size = new System.Drawing.Size(209, 18);
            this.textBox_file.TabIndex = 5;
            // 
            // checkBox_相机
            // 
            this.checkBox_相机.AutoSize = true;
            this.checkBox_相机.Location = new System.Drawing.Point(6, 69);
            this.checkBox_相机.Name = "checkBox_相机";
            this.checkBox_相机.Size = new System.Drawing.Size(72, 16);
            this.checkBox_相机.TabIndex = 6;
            this.checkBox_相机.Text = "文件取图";
            this.checkBox_相机.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "取一张图像";
            // 
            // btn_Sure
            // 
            this.btn_Sure.Location = new System.Drawing.Point(6, 329);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(64, 21);
            this.btn_Sure.TabIndex = 14;
            this.btn_Sure.Text = "确定";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // read_ImageName
            // 
            this.read_ImageName.FormattingEnabled = true;
            this.read_ImageName.Location = new System.Drawing.Point(80, 208);
            this.read_ImageName.Name = "read_ImageName";
            this.read_ImageName.Size = new System.Drawing.Size(90, 20);
            this.read_ImageName.TabIndex = 18;
            this.read_ImageName.Text = "image1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(2, 274);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "注: 退出时,请点确定";
            // 
            // lab_Image
            // 
            this.lab_Image.AutoSize = true;
            this.lab_Image.Location = new System.Drawing.Point(4, 208);
            this.lab_Image.Name = "lab_Image";
            this.lab_Image.Size = new System.Drawing.Size(59, 12);
            this.lab_Image.TabIndex = 1;
            this.lab_Image.Text = "图像输出 ";
            // 
            // checkBox_exp
            // 
            this.checkBox_exp.AutoSize = true;
            this.checkBox_exp.Location = new System.Drawing.Point(6, 145);
            this.checkBox_exp.Name = "checkBox_exp";
            this.checkBox_exp.Size = new System.Drawing.Size(72, 16);
            this.checkBox_exp.TabIndex = 20;
            this.checkBox_exp.Text = "爆光时间";
            this.checkBox_exp.UseVisualStyleBackColor = true;
            // 
            // button_exp
            // 
            this.button_exp.Location = new System.Drawing.Point(12, 167);
            this.button_exp.Name = "button_exp";
            this.button_exp.Size = new System.Drawing.Size(94, 23);
            this.button_exp.TabIndex = 21;
            this.button_exp.Text = "保存爆光时间";
            this.button_exp.UseMnemonic = false;
            this.button_exp.UseVisualStyleBackColor = true;
            this.button_exp.Click += new System.EventHandler(this.button_exp_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "1528";
            // 
            // ReadPictureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(222, 560);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_exp);
            this.Controls.Add(this.checkBox_exp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.read_ImageName);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.lab_Image);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_相机);
            this.Controls.Add(this.textBox_file);
            this.Controls.Add(this.button_文件位置);
            this.Name = "ReadPictureControl";
            this.Text = "ReadPictureControl";
            this.Load += new System.EventHandler(this.ReadPictureControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_文件位置;
        private System.Windows.Forms.TextBox textBox_file;
        private System.Windows.Forms.CheckBox checkBox_相机;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.ComboBox read_ImageName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lab_Image;
        private System.Windows.Forms.CheckBox checkBox_exp;
        private System.Windows.Forms.Button button_exp;
        private System.Windows.Forms.Label label2;
    }
}