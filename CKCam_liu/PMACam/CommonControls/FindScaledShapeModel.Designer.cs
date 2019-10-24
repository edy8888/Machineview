namespace PMACam
{
    partial class FindScaledShapeModel
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
            this.button_参数设置1 = new System.Windows.Forms.Button();
            this.panel_参数设置1 = new System.Windows.Forms.Panel();
            this.lab_maxOverlap = new System.Windows.Forms.Label();
            this.txt_maxOverlap = new System.Windows.Forms.TextBox();
            this.lab_subPixel = new System.Windows.Forms.Label();
            this.model_subPixel = new System.Windows.Forms.ComboBox();
            this.lab_numLevels = new System.Windows.Forms.Label();
            this.txt_numLevels = new System.Windows.Forms.TextBox();
            this.lab_greediness = new System.Windows.Forms.Label();
            this.txt_greediness = new System.Windows.Forms.TextBox();
            this.button_参数设置3 = new System.Windows.Forms.Button();
            this.button_参数设置2 = new System.Windows.Forms.Button();
            this.panel_参数设置3 = new System.Windows.Forms.Panel();
            this.lab_numMatches = new System.Windows.Forms.Label();
            this.txt_numMatches = new System.Windows.Forms.TextBox();
            this.lab_minScore = new System.Windows.Forms.Label();
            this.txt_minScore = new System.Windows.Forms.TextBox();
            this.lab_angleStart = new System.Windows.Forms.Label();
            this.txt_angleStart = new System.Windows.Forms.TextBox();
            this.txt_angleExtent = new System.Windows.Forms.TextBox();
            this.lab_angleExtent = new System.Windows.Forms.Label();
            this.panel_参数设置2 = new System.Windows.Forms.Panel();
            this.lab_scaleMax = new System.Windows.Forms.Label();
            this.lab_scaleMin = new System.Windows.Forms.Label();
            this.txt_scaleMax = new System.Windows.Forms.TextBox();
            this.txt_scaleMin = new System.Windows.Forms.TextBox();
            this.lab_modelID = new System.Windows.Forms.Label();
            this.lab_image = new System.Windows.Forms.Label();
            this.Model_modelID = new System.Windows.Forms.ComboBox();
            this.Model_image = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_参数设置1.SuspendLayout();
            this.panel_参数设置3.SuspendLayout();
            this.panel_参数设置2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_参数设置1
            // 
            this.button_参数设置1.Location = new System.Drawing.Point(2, 437);
            this.button_参数设置1.Name = "button_参数设置1";
            this.button_参数设置1.Size = new System.Drawing.Size(217, 23);
            this.button_参数设置1.TabIndex = 14;
            this.button_参数设置1.Text = "其他设置项";
            this.button_参数设置1.UseVisualStyleBackColor = true;
            this.button_参数设置1.Click += new System.EventHandler(this.button_参数设置1_Click);
            // 
            // panel_参数设置1
            // 
            this.panel_参数设置1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_参数设置1.Controls.Add(this.lab_maxOverlap);
            this.panel_参数设置1.Controls.Add(this.txt_maxOverlap);
            this.panel_参数设置1.Controls.Add(this.lab_subPixel);
            this.panel_参数设置1.Controls.Add(this.model_subPixel);
            this.panel_参数设置1.Controls.Add(this.lab_numLevels);
            this.panel_参数设置1.Controls.Add(this.txt_numLevels);
            this.panel_参数设置1.Controls.Add(this.lab_greediness);
            this.panel_参数设置1.Controls.Add(this.txt_greediness);
            this.panel_参数设置1.Location = new System.Drawing.Point(-1, 468);
            this.panel_参数设置1.Name = "panel_参数设置1";
            this.panel_参数设置1.Size = new System.Drawing.Size(220, 197);
            this.panel_参数设置1.TabIndex = 17;
            // 
            // lab_maxOverlap
            // 
            this.lab_maxOverlap.AutoSize = true;
            this.lab_maxOverlap.Location = new System.Drawing.Point(3, 32);
            this.lab_maxOverlap.Name = "lab_maxOverlap";
            this.lab_maxOverlap.Size = new System.Drawing.Size(29, 12);
            this.lab_maxOverlap.TabIndex = 50;
            this.lab_maxOverlap.Text = "重叠";
            // 
            // txt_maxOverlap
            // 
            this.txt_maxOverlap.Location = new System.Drawing.Point(97, 32);
            this.txt_maxOverlap.Multiline = true;
            this.txt_maxOverlap.Name = "txt_maxOverlap";
            this.txt_maxOverlap.Size = new System.Drawing.Size(106, 22);
            this.txt_maxOverlap.TabIndex = 54;
            this.txt_maxOverlap.Text = "0.5";
            // 
            // lab_subPixel
            // 
            this.lab_subPixel.AutoSize = true;
            this.lab_subPixel.Location = new System.Drawing.Point(5, 60);
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
            this.model_subPixel.Location = new System.Drawing.Point(97, 61);
            this.model_subPixel.Name = "model_subPixel";
            this.model_subPixel.Size = new System.Drawing.Size(106, 20);
            this.model_subPixel.TabIndex = 57;
            // 
            // lab_numLevels
            // 
            this.lab_numLevels.AutoSize = true;
            this.lab_numLevels.Location = new System.Drawing.Point(5, 91);
            this.lab_numLevels.Name = "lab_numLevels";
            this.lab_numLevels.Size = new System.Drawing.Size(41, 12);
            this.lab_numLevels.TabIndex = 52;
            this.lab_numLevels.Text = "金子塔";
            // 
            // txt_numLevels
            // 
            this.txt_numLevels.Location = new System.Drawing.Point(97, 91);
            this.txt_numLevels.Multiline = true;
            this.txt_numLevels.Name = "txt_numLevels";
            this.txt_numLevels.Size = new System.Drawing.Size(106, 22);
            this.txt_numLevels.TabIndex = 55;
            this.txt_numLevels.Text = "0";
            // 
            // lab_greediness
            // 
            this.lab_greediness.AutoSize = true;
            this.lab_greediness.Location = new System.Drawing.Point(6, 118);
            this.lab_greediness.Name = "lab_greediness";
            this.lab_greediness.Size = new System.Drawing.Size(41, 12);
            this.lab_greediness.TabIndex = 53;
            this.lab_greediness.Text = "贪婪度";
            // 
            // txt_greediness
            // 
            this.txt_greediness.Location = new System.Drawing.Point(97, 117);
            this.txt_greediness.Multiline = true;
            this.txt_greediness.Name = "txt_greediness";
            this.txt_greediness.Size = new System.Drawing.Size(106, 22);
            this.txt_greediness.TabIndex = 56;
            this.txt_greediness.Text = "0.9";
            // 
            // button_参数设置3
            // 
            this.button_参数设置3.Location = new System.Drawing.Point(2, 381);
            this.button_参数设置3.Name = "button_参数设置3";
            this.button_参数设置3.Size = new System.Drawing.Size(217, 23);
            this.button_参数设置3.TabIndex = 16;
            this.button_参数设置3.Text = "数量角度分数项";
            this.button_参数设置3.UseVisualStyleBackColor = true;
            this.button_参数设置3.Click += new System.EventHandler(this.button_参数设置3_Click);
            // 
            // button_参数设置2
            // 
            this.button_参数设置2.Location = new System.Drawing.Point(2, 410);
            this.button_参数设置2.Name = "button_参数设置2";
            this.button_参数设置2.Size = new System.Drawing.Size(217, 23);
            this.button_参数设置2.TabIndex = 15;
            this.button_参数设置2.Text = "比例设置项";
            this.button_参数设置2.UseVisualStyleBackColor = true;
            this.button_参数设置2.Click += new System.EventHandler(this.button_参数设置2_Click);
            // 
            // panel_参数设置3
            // 
            this.panel_参数设置3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_参数设置3.Controls.Add(this.lab_numMatches);
            this.panel_参数设置3.Controls.Add(this.txt_numMatches);
            this.panel_参数设置3.Controls.Add(this.lab_minScore);
            this.panel_参数设置3.Controls.Add(this.txt_minScore);
            this.panel_参数设置3.Controls.Add(this.lab_angleStart);
            this.panel_参数设置3.Controls.Add(this.txt_angleStart);
            this.panel_参数设置3.Controls.Add(this.txt_angleExtent);
            this.panel_参数设置3.Controls.Add(this.lab_angleExtent);
            this.panel_参数设置3.Location = new System.Drawing.Point(-1, 468);
            this.panel_参数设置3.Name = "panel_参数设置3";
            this.panel_参数设置3.Size = new System.Drawing.Size(220, 200);
            this.panel_参数设置3.TabIndex = 19;
            // 
            // lab_numMatches
            // 
            this.lab_numMatches.AutoSize = true;
            this.lab_numMatches.Location = new System.Drawing.Point(13, 130);
            this.lab_numMatches.Name = "lab_numMatches";
            this.lab_numMatches.Size = new System.Drawing.Size(53, 12);
            this.lab_numMatches.TabIndex = 43;
            this.lab_numMatches.Text = "匹配个数";
            // 
            // txt_numMatches
            // 
            this.txt_numMatches.Location = new System.Drawing.Point(111, 127);
            this.txt_numMatches.Multiline = true;
            this.txt_numMatches.Name = "txt_numMatches";
            this.txt_numMatches.Size = new System.Drawing.Size(106, 22);
            this.txt_numMatches.TabIndex = 45;
            this.txt_numMatches.Text = "0";
            // 
            // lab_minScore
            // 
            this.lab_minScore.AutoSize = true;
            this.lab_minScore.Location = new System.Drawing.Point(15, 91);
            this.lab_minScore.Name = "lab_minScore";
            this.lab_minScore.Size = new System.Drawing.Size(53, 12);
            this.lab_minScore.TabIndex = 42;
            this.lab_minScore.Text = "最小分数";
            // 
            // txt_minScore
            // 
            this.txt_minScore.Location = new System.Drawing.Point(111, 91);
            this.txt_minScore.Multiline = true;
            this.txt_minScore.Name = "txt_minScore";
            this.txt_minScore.Size = new System.Drawing.Size(106, 22);
            this.txt_minScore.TabIndex = 44;
            this.txt_minScore.Text = "0.6";
            // 
            // lab_angleStart
            // 
            this.lab_angleStart.AutoSize = true;
            this.lab_angleStart.Location = new System.Drawing.Point(14, 11);
            this.lab_angleStart.Name = "lab_angleStart";
            this.lab_angleStart.Size = new System.Drawing.Size(53, 12);
            this.lab_angleStart.TabIndex = 34;
            this.lab_angleStart.Text = "起始角度";
            // 
            // txt_angleStart
            // 
            this.txt_angleStart.Location = new System.Drawing.Point(114, 11);
            this.txt_angleStart.Multiline = true;
            this.txt_angleStart.Name = "txt_angleStart";
            this.txt_angleStart.Size = new System.Drawing.Size(106, 22);
            this.txt_angleStart.TabIndex = 36;
            this.txt_angleStart.Text = "0";
            // 
            // txt_angleExtent
            // 
            this.txt_angleExtent.Location = new System.Drawing.Point(114, 46);
            this.txt_angleExtent.Multiline = true;
            this.txt_angleExtent.Name = "txt_angleExtent";
            this.txt_angleExtent.Size = new System.Drawing.Size(106, 22);
            this.txt_angleExtent.TabIndex = 37;
            this.txt_angleExtent.Text = "360";
            // 
            // lab_angleExtent
            // 
            this.lab_angleExtent.AutoSize = true;
            this.lab_angleExtent.Location = new System.Drawing.Point(14, 49);
            this.lab_angleExtent.Name = "lab_angleExtent";
            this.lab_angleExtent.Size = new System.Drawing.Size(53, 12);
            this.lab_angleExtent.TabIndex = 35;
            this.lab_angleExtent.Text = "终止角度";
            // 
            // panel_参数设置2
            // 
            this.panel_参数设置2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_参数设置2.Controls.Add(this.lab_scaleMax);
            this.panel_参数设置2.Controls.Add(this.lab_scaleMin);
            this.panel_参数设置2.Controls.Add(this.txt_scaleMax);
            this.panel_参数设置2.Controls.Add(this.txt_scaleMin);
            this.panel_参数设置2.Location = new System.Drawing.Point(-1, 468);
            this.panel_参数设置2.Name = "panel_参数设置2";
            this.panel_参数设置2.Size = new System.Drawing.Size(217, 197);
            this.panel_参数设置2.TabIndex = 18;
            // 
            // lab_scaleMax
            // 
            this.lab_scaleMax.AutoSize = true;
            this.lab_scaleMax.Location = new System.Drawing.Point(13, 52);
            this.lab_scaleMax.Name = "lab_scaleMax";
            this.lab_scaleMax.Size = new System.Drawing.Size(53, 12);
            this.lab_scaleMax.TabIndex = 39;
            this.lab_scaleMax.Text = "比例放大";
            // 
            // lab_scaleMin
            // 
            this.lab_scaleMin.AutoSize = true;
            this.lab_scaleMin.Location = new System.Drawing.Point(13, 23);
            this.lab_scaleMin.Name = "lab_scaleMin";
            this.lab_scaleMin.Size = new System.Drawing.Size(53, 12);
            this.lab_scaleMin.TabIndex = 38;
            this.lab_scaleMin.Text = "比例缩小";
            // 
            // txt_scaleMax
            // 
            this.txt_scaleMax.Location = new System.Drawing.Point(111, 52);
            this.txt_scaleMax.Multiline = true;
            this.txt_scaleMax.Name = "txt_scaleMax";
            this.txt_scaleMax.Size = new System.Drawing.Size(106, 22);
            this.txt_scaleMax.TabIndex = 41;
            this.txt_scaleMax.Text = "1";
            // 
            // txt_scaleMin
            // 
            this.txt_scaleMin.Location = new System.Drawing.Point(111, 23);
            this.txt_scaleMin.Multiline = true;
            this.txt_scaleMin.Name = "txt_scaleMin";
            this.txt_scaleMin.Size = new System.Drawing.Size(106, 22);
            this.txt_scaleMin.TabIndex = 40;
            this.txt_scaleMin.Text = "1";
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
            this.label1.Location = new System.Drawing.Point(11, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 21);
            this.label1.TabIndex = 37;
            this.label1.Text = "形状模板匹配";
            // 
            // FindScaledShapeModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(231, 785);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lab_modelID);
            this.Controls.Add(this.lab_image);
            this.Controls.Add(this.Model_modelID);
            this.Controls.Add(this.Model_image);
            this.Controls.Add(this.button_参数设置1);
            this.Controls.Add(this.button_参数设置3);
            this.Controls.Add(this.button_参数设置2);
            this.Controls.Add(this.panel_参数设置1);
            this.Controls.Add(this.panel_参数设置3);
            this.Controls.Add(this.panel_参数设置2);
            this.Name = "FindScaledShapeModel";
            this.Text = "FindScaledShapeModel";
            this.Load += new System.EventHandler(this.FindScaledShapeModel_Load);
            this.panel_参数设置1.ResumeLayout(false);
            this.panel_参数设置1.PerformLayout();
            this.panel_参数设置3.ResumeLayout(false);
            this.panel_参数设置3.PerformLayout();
            this.panel_参数设置2.ResumeLayout(false);
            this.panel_参数设置2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_参数设置1;
        private System.Windows.Forms.Panel panel_参数设置1;
        private System.Windows.Forms.Button button_参数设置3;
        private System.Windows.Forms.Button button_参数设置2;
        private System.Windows.Forms.Panel panel_参数设置3;
        private System.Windows.Forms.Panel panel_参数设置2;
        private System.Windows.Forms.Label lab_maxOverlap;
        private System.Windows.Forms.TextBox txt_maxOverlap;
        private System.Windows.Forms.Label lab_subPixel;
        private System.Windows.Forms.ComboBox model_subPixel;
        private System.Windows.Forms.Label lab_numLevels;
        private System.Windows.Forms.TextBox txt_numLevels;
        private System.Windows.Forms.Label lab_greediness;
        private System.Windows.Forms.TextBox txt_greediness;
        private System.Windows.Forms.Label lab_angleStart;
        private System.Windows.Forms.TextBox txt_angleStart;
        private System.Windows.Forms.TextBox txt_angleExtent;
        private System.Windows.Forms.Label lab_angleExtent;
        private System.Windows.Forms.Label lab_scaleMax;
        private System.Windows.Forms.Label lab_scaleMin;
        private System.Windows.Forms.TextBox txt_scaleMax;
        private System.Windows.Forms.TextBox txt_scaleMin;
        private System.Windows.Forms.Label lab_modelID;
        private System.Windows.Forms.Label lab_image;
        private System.Windows.Forms.ComboBox Model_modelID;
        private System.Windows.Forms.ComboBox Model_image;
        private System.Windows.Forms.Label lab_numMatches;
        private System.Windows.Forms.TextBox txt_numMatches;
        private System.Windows.Forms.Label lab_minScore;
        private System.Windows.Forms.TextBox txt_minScore;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;

    }
}