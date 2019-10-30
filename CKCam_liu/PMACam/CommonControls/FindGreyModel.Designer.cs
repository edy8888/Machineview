namespace PMACam
{
    partial class FindGreyModel
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
            this.lab_subPixel = new System.Windows.Forms.Label();
            this.model_subPixel = new System.Windows.Forms.ComboBox();
            this.lab_numMatches = new System.Windows.Forms.Label();
            this.txt_greydis = new System.Windows.Forms.TextBox();
            this.lab_error = new System.Windows.Forms.Label();
            this.txt_maxerror = new System.Windows.Forms.TextBox();
            this.lab_angleStart = new System.Windows.Forms.Label();
            this.txt_angleStart = new System.Windows.Forms.TextBox();
            this.txt_angleExtent = new System.Windows.Forms.TextBox();
            this.lab_angleExtent = new System.Windows.Forms.Label();
            this.lab_modelID = new System.Windows.Forms.Label();
            this.lab_image = new System.Windows.Forms.Label();
            this.Model_modelID = new System.Windows.Forms.ComboBox();
            this.Model_image = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lab_subPixel
            // 
            this.lab_subPixel.AutoSize = true;
            this.lab_subPixel.Location = new System.Drawing.Point(12, 418);
            this.lab_subPixel.Name = "lab_subPixel";
            this.lab_subPixel.Size = new System.Drawing.Size(53, 12);
            this.lab_subPixel.TabIndex = 51;
            this.lab_subPixel.Text = "精度模式";
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
            this.model_subPixel.Location = new System.Drawing.Point(91, 419);
            this.model_subPixel.Name = "model_subPixel";
            this.model_subPixel.Size = new System.Drawing.Size(77, 20);
            this.model_subPixel.TabIndex = 57;
            // 
            // lab_numMatches
            // 
            this.lab_numMatches.AutoSize = true;
            this.lab_numMatches.Location = new System.Drawing.Point(11, 576);
            this.lab_numMatches.Name = "lab_numMatches";
            this.lab_numMatches.Size = new System.Drawing.Size(53, 12);
            this.lab_numMatches.TabIndex = 43;
            this.lab_numMatches.Text = "灰度差值";
            // 
            // txt_greydis
            // 
            this.txt_greydis.Location = new System.Drawing.Point(96, 573);
            this.txt_greydis.Multiline = true;
            this.txt_greydis.Name = "txt_greydis";
            this.txt_greydis.Size = new System.Drawing.Size(77, 22);
            this.txt_greydis.TabIndex = 45;
            this.txt_greydis.Text = "15";
            // 
            // lab_error
            // 
            this.lab_error.AutoSize = true;
            this.lab_error.Location = new System.Drawing.Point(13, 537);
            this.lab_error.Name = "lab_error";
            this.lab_error.Size = new System.Drawing.Size(65, 12);
            this.lab_error.TabIndex = 42;
            this.lab_error.Text = "最大冗错值";
            // 
            // txt_maxerror
            // 
            this.txt_maxerror.Location = new System.Drawing.Point(96, 537);
            this.txt_maxerror.Multiline = true;
            this.txt_maxerror.Name = "txt_maxerror";
            this.txt_maxerror.Size = new System.Drawing.Size(77, 22);
            this.txt_maxerror.TabIndex = 44;
            this.txt_maxerror.Text = "30";
            // 
            // lab_angleStart
            // 
            this.lab_angleStart.AutoSize = true;
            this.lab_angleStart.Location = new System.Drawing.Point(12, 457);
            this.lab_angleStart.Name = "lab_angleStart";
            this.lab_angleStart.Size = new System.Drawing.Size(53, 12);
            this.lab_angleStart.TabIndex = 34;
            this.lab_angleStart.Text = "起始角度";
            // 
            // txt_angleStart
            // 
            this.txt_angleStart.Location = new System.Drawing.Point(99, 457);
            this.txt_angleStart.Multiline = true;
            this.txt_angleStart.Name = "txt_angleStart";
            this.txt_angleStart.Size = new System.Drawing.Size(77, 22);
            this.txt_angleStart.TabIndex = 36;
            this.txt_angleStart.Text = "0";
            // 
            // txt_angleExtent
            // 
            this.txt_angleExtent.Location = new System.Drawing.Point(99, 492);
            this.txt_angleExtent.Multiline = true;
            this.txt_angleExtent.Name = "txt_angleExtent";
            this.txt_angleExtent.Size = new System.Drawing.Size(77, 22);
            this.txt_angleExtent.TabIndex = 37;
            this.txt_angleExtent.Text = "360";
            // 
            // lab_angleExtent
            // 
            this.lab_angleExtent.AutoSize = true;
            this.lab_angleExtent.Location = new System.Drawing.Point(12, 495);
            this.lab_angleExtent.Name = "lab_angleExtent";
            this.lab_angleExtent.Size = new System.Drawing.Size(53, 12);
            this.lab_angleExtent.TabIndex = 35;
            this.lab_angleExtent.Text = "终止角度";
            // 
            // lab_modelID
            // 
            this.lab_modelID.AutoSize = true;
            this.lab_modelID.Location = new System.Drawing.Point(12, 159);
            this.lab_modelID.Name = "lab_modelID";
            this.lab_modelID.Size = new System.Drawing.Size(29, 12);
            this.lab_modelID.TabIndex = 32;
            this.lab_modelID.Text = "模板";
            // 
            // lab_image
            // 
            this.lab_image.AutoSize = true;
            this.lab_image.Location = new System.Drawing.Point(9, 130);
            this.lab_image.Name = "lab_image";
            this.lab_image.Size = new System.Drawing.Size(53, 12);
            this.lab_image.TabIndex = 31;
            this.lab_image.Text = "输入图像";
            // 
            // Model_modelID
            // 
            this.Model_modelID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Model_modelID.FormattingEnabled = true;
            this.Model_modelID.Location = new System.Drawing.Point(70, 156);
            this.Model_modelID.Name = "Model_modelID";
            this.Model_modelID.Size = new System.Drawing.Size(106, 20);
            this.Model_modelID.TabIndex = 34;
            // 
            // Model_image
            // 
            this.Model_image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Model_image.FormattingEnabled = true;
            this.Model_image.Location = new System.Drawing.Point(70, 130);
            this.Model_image.Name = "Model_image";
            this.Model_image.Size = new System.Drawing.Size(106, 20);
            this.Model_image.TabIndex = 33;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 339);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 35;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(7, 305);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 20);
            this.label4.TabIndex = 36;
            this.label4.Text = "注: 退出时,请点确定";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 21);
            this.label1.TabIndex = 37;
            this.label1.Text = "灰度模板匹配";
            // 
            // FindGreyModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(229, 785);
            this.Controls.Add(this.lab_numMatches);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_greydis);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lab_error);
            this.Controls.Add(this.lab_subPixel);
            this.Controls.Add(this.txt_maxerror);
            this.Controls.Add(this.model_subPixel);
            this.Controls.Add(this.lab_angleStart);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_angleStart);
            this.Controls.Add(this.lab_modelID);
            this.Controls.Add(this.txt_angleExtent);
            this.Controls.Add(this.lab_image);
            this.Controls.Add(this.lab_angleExtent);
            this.Controls.Add(this.Model_modelID);
            this.Controls.Add(this.Model_image);
            this.Name = "FindGreyModel";
            this.Text = "FindGreyModel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_subPixel;
        private System.Windows.Forms.ComboBox model_subPixel;
        private System.Windows.Forms.Label lab_angleStart;
        private System.Windows.Forms.TextBox txt_angleStart;
        private System.Windows.Forms.TextBox txt_angleExtent;
        private System.Windows.Forms.Label lab_angleExtent;
        private System.Windows.Forms.Label lab_modelID;
        private System.Windows.Forms.Label lab_image;
        private System.Windows.Forms.ComboBox Model_modelID;
        private System.Windows.Forms.ComboBox Model_image;
        private System.Windows.Forms.Label lab_numMatches;
        private System.Windows.Forms.TextBox txt_greydis;
        private System.Windows.Forms.Label lab_error;
        private System.Windows.Forms.TextBox txt_maxerror;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;

    }
}