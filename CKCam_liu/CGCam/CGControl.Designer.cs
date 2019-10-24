namespace CGCam
{
    partial class CGControl
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.updateDeviceListTimer = new System.Windows.Forms.Timer(this.components);
            this.deviceListView = new System.Windows.Forms.ListView();
            this.CamMenu = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFile_LoadPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFile_SavePicture = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTool = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTool_CamOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTool_CamClosed = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTool_Video = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTool_TakeImage = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTool_Match = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadModelbut = new System.Windows.Forms.Button();
            this.SaveModelbut = new System.Windows.Forms.Button();
            this.Searchbut = new System.Windows.Forms.Button();
            this.SetParameterbut = new System.Windows.Forms.Button();
            this.EditModelbut = new System.Windows.Forms.Button();
            this.LearnModelbut = new System.Windows.Forms.Button();
            this.LearnAreaModelbut = new System.Windows.Forms.Button();
            this.Xlabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Slabel = new System.Windows.Forms.Label();
            this.Alabel = new System.Windows.Forms.Label();
            this.Ylabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Learnbut = new System.Windows.Forms.Button();
            this.ModelTypecombo = new System.Windows.Forms.ComboBox();
            this.Videobut = new System.Windows.Forms.Button();
            this.CurModellabel = new System.Windows.Forms.Label();
            this.CamMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // deviceListView
            // 
            this.deviceListView.Location = new System.Drawing.Point(758, 32);
            this.deviceListView.Name = "deviceListView";
            this.deviceListView.Size = new System.Drawing.Size(90, 27);
            this.deviceListView.TabIndex = 2;
            this.deviceListView.UseCompatibleStateImageBehavior = false;
            // 
            // CamMenu
            // 
            this.CamMenu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CamMenu.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.CamMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.CamMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuTool});
            this.CamMenu.Location = new System.Drawing.Point(0, 0);
            this.CamMenu.Name = "CamMenu";
            this.CamMenu.Size = new System.Drawing.Size(1110, 25);
            this.CamMenu.TabIndex = 18;
            this.CamMenu.Text = "menuStrip2";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile_LoadPicture,
            this.MenuFile_SavePicture});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(44, 21);
            this.MenuFile.Text = "文件";
            // 
            // MenuFile_LoadPicture
            // 
            this.MenuFile_LoadPicture.Name = "MenuFile_LoadPicture";
            this.MenuFile_LoadPicture.Size = new System.Drawing.Size(124, 22);
            this.MenuFile_LoadPicture.Text = "打开图片";
            this.MenuFile_LoadPicture.Click += new System.EventHandler(this.Menu_ItemClick);
            // 
            // MenuFile_SavePicture
            // 
            this.MenuFile_SavePicture.Name = "MenuFile_SavePicture";
            this.MenuFile_SavePicture.Size = new System.Drawing.Size(124, 22);
            this.MenuFile_SavePicture.Text = "保存图片";
            this.MenuFile_SavePicture.Click += new System.EventHandler(this.Menu_ItemClick);
            // 
            // MenuTool
            // 
            this.MenuTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuTool_CamOpen,
            this.MenuTool_CamClosed,
            this.MenuTool_Video,
            this.MenuTool_TakeImage,
            this.MenuTool_Match});
            this.MenuTool.Name = "MenuTool";
            this.MenuTool.Size = new System.Drawing.Size(44, 21);
            this.MenuTool.Text = "工具";
            // 
            // MenuTool_CamOpen
            // 
            this.MenuTool_CamOpen.Name = "MenuTool_CamOpen";
            this.MenuTool_CamOpen.Size = new System.Drawing.Size(100, 22);
            this.MenuTool_CamOpen.Text = "连接";
            this.MenuTool_CamOpen.Click += new System.EventHandler(this.Menu_ItemClick);
            // 
            // MenuTool_CamClosed
            // 
            this.MenuTool_CamClosed.Name = "MenuTool_CamClosed";
            this.MenuTool_CamClosed.Size = new System.Drawing.Size(100, 22);
            this.MenuTool_CamClosed.Text = "断开";
            this.MenuTool_CamClosed.Click += new System.EventHandler(this.Menu_ItemClick);
            // 
            // MenuTool_Video
            // 
            this.MenuTool_Video.Name = "MenuTool_Video";
            this.MenuTool_Video.Size = new System.Drawing.Size(100, 22);
            this.MenuTool_Video.Text = "影像";
            this.MenuTool_Video.Click += new System.EventHandler(this.Menu_ItemClick);
            // 
            // MenuTool_TakeImage
            // 
            this.MenuTool_TakeImage.Name = "MenuTool_TakeImage";
            this.MenuTool_TakeImage.Size = new System.Drawing.Size(100, 22);
            this.MenuTool_TakeImage.Text = "取图";
            this.MenuTool_TakeImage.Click += new System.EventHandler(this.Menu_ItemClick);
            // 
            // MenuTool_Match
            // 
            this.MenuTool_Match.Name = "MenuTool_Match";
            this.MenuTool_Match.Size = new System.Drawing.Size(100, 22);
            this.MenuTool_Match.Text = "匹配";
            this.MenuTool_Match.Click += new System.EventHandler(this.Menu_ItemClick);
            // 
            // LoadModelbut
            // 
            this.LoadModelbut.Location = new System.Drawing.Point(8, 220);
            this.LoadModelbut.Name = "LoadModelbut";
            this.LoadModelbut.Size = new System.Drawing.Size(88, 35);
            this.LoadModelbut.TabIndex = 27;
            this.LoadModelbut.Text = "装载模版";
            this.LoadModelbut.UseVisualStyleBackColor = true;
            this.LoadModelbut.Click += new System.EventHandler(this.CamFormbut_Click);
            // 
            // SaveModelbut
            // 
            this.SaveModelbut.Location = new System.Drawing.Point(8, 183);
            this.SaveModelbut.Name = "SaveModelbut";
            this.SaveModelbut.Size = new System.Drawing.Size(88, 35);
            this.SaveModelbut.TabIndex = 26;
            this.SaveModelbut.Text = "保存模板";
            this.SaveModelbut.UseVisualStyleBackColor = true;
            this.SaveModelbut.Click += new System.EventHandler(this.CamFormbut_Click);
            // 
            // Searchbut
            // 
            this.Searchbut.Location = new System.Drawing.Point(8, 332);
            this.Searchbut.Name = "Searchbut";
            this.Searchbut.Size = new System.Drawing.Size(88, 35);
            this.Searchbut.TabIndex = 25;
            this.Searchbut.Text = "搜索测试";
            this.Searchbut.UseVisualStyleBackColor = true;
            this.Searchbut.Click += new System.EventHandler(this.CamFormbut_Click);
            // 
            // SetParameterbut
            // 
            this.SetParameterbut.Location = new System.Drawing.Point(8, 257);
            this.SetParameterbut.Name = "SetParameterbut";
            this.SetParameterbut.Size = new System.Drawing.Size(88, 35);
            this.SetParameterbut.TabIndex = 24;
            this.SetParameterbut.Text = "参数设置";
            this.SetParameterbut.UseVisualStyleBackColor = true;
            this.SetParameterbut.Click += new System.EventHandler(this.CamFormbut_Click);
            // 
            // EditModelbut
            // 
            this.EditModelbut.Location = new System.Drawing.Point(8, 146);
            this.EditModelbut.Name = "EditModelbut";
            this.EditModelbut.Size = new System.Drawing.Size(88, 35);
            this.EditModelbut.TabIndex = 23;
            this.EditModelbut.Text = "编辑模板";
            this.EditModelbut.UseVisualStyleBackColor = true;
            this.EditModelbut.Click += new System.EventHandler(this.CamFormbut_Click);
            // 
            // LearnModelbut
            // 
            this.LearnModelbut.Location = new System.Drawing.Point(8, 72);
            this.LearnModelbut.Name = "LearnModelbut";
            this.LearnModelbut.Size = new System.Drawing.Size(88, 35);
            this.LearnModelbut.TabIndex = 22;
            this.LearnModelbut.Text = "特征区域";
            this.LearnModelbut.UseVisualStyleBackColor = true;
            this.LearnModelbut.Click += new System.EventHandler(this.CamFormbut_Click);
            // 
            // LearnAreaModelbut
            // 
            this.LearnAreaModelbut.Location = new System.Drawing.Point(8, 34);
            this.LearnAreaModelbut.Name = "LearnAreaModelbut";
            this.LearnAreaModelbut.Size = new System.Drawing.Size(88, 35);
            this.LearnAreaModelbut.TabIndex = 28;
            this.LearnAreaModelbut.Text = "搜索区域";
            this.LearnAreaModelbut.UseVisualStyleBackColor = true;
            this.LearnAreaModelbut.Click += new System.EventHandler(this.CamFormbut_Click);
            // 
            // Xlabel
            // 
            this.Xlabel.AutoSize = true;
            this.Xlabel.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Xlabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Xlabel.Location = new System.Drawing.Point(-1, 9);
            this.Xlabel.Name = "Xlabel";
            this.Xlabel.Size = new System.Drawing.Size(79, 19);
            this.Xlabel.TabIndex = 31;
            this.Xlabel.Text = "X:0.000";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.Slabel);
            this.panel1.Controls.Add(this.Alabel);
            this.panel1.Controls.Add(this.Ylabel);
            this.panel1.Controls.Add(this.Xlabel);
            this.panel1.Location = new System.Drawing.Point(1004, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(104, 151);
            this.panel1.TabIndex = 32;
            // 
            // Slabel
            // 
            this.Slabel.AutoSize = true;
            this.Slabel.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Slabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Slabel.Location = new System.Drawing.Point(0, 118);
            this.Slabel.Name = "Slabel";
            this.Slabel.Size = new System.Drawing.Size(79, 19);
            this.Slabel.TabIndex = 34;
            this.Slabel.Text = "S:0.000";
            // 
            // Alabel
            // 
            this.Alabel.AutoSize = true;
            this.Alabel.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Alabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Alabel.Location = new System.Drawing.Point(0, 82);
            this.Alabel.Name = "Alabel";
            this.Alabel.Size = new System.Drawing.Size(79, 19);
            this.Alabel.TabIndex = 33;
            this.Alabel.Text = "A:0.000";
            // 
            // Ylabel
            // 
            this.Ylabel.AutoSize = true;
            this.Ylabel.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Ylabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Ylabel.Location = new System.Drawing.Point(0, 46);
            this.Ylabel.Name = "Ylabel";
            this.Ylabel.Size = new System.Drawing.Size(79, 19);
            this.Ylabel.TabIndex = 32;
            this.Ylabel.Text = "Y:0.000";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SkyBlue;
            this.panel2.Controls.Add(this.Learnbut);
            this.panel2.Controls.Add(this.ModelTypecombo);
            this.panel2.Controls.Add(this.Videobut);
            this.panel2.Controls.Add(this.LearnAreaModelbut);
            this.panel2.Controls.Add(this.Searchbut);
            this.panel2.Controls.Add(this.LearnModelbut);
            this.panel2.Controls.Add(this.SetParameterbut);
            this.panel2.Controls.Add(this.LoadModelbut);
            this.panel2.Controls.Add(this.EditModelbut);
            this.panel2.Controls.Add(this.SaveModelbut);
            this.panel2.Location = new System.Drawing.Point(1004, 178);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(104, 399);
            this.panel2.TabIndex = 34;
            // 
            // Learnbut
            // 
            this.Learnbut.Location = new System.Drawing.Point(8, 109);
            this.Learnbut.Name = "Learnbut";
            this.Learnbut.Size = new System.Drawing.Size(88, 35);
            this.Learnbut.TabIndex = 35;
            this.Learnbut.Text = "学习模板";
            this.Learnbut.UseVisualStyleBackColor = true;
            this.Learnbut.Click += new System.EventHandler(this.CamFormbut_Click);
            // 
            // ModelTypecombo
            // 
            this.ModelTypecombo.FormattingEnabled = true;
            this.ModelTypecombo.Location = new System.Drawing.Point(8, 10);
            this.ModelTypecombo.Name = "ModelTypecombo";
            this.ModelTypecombo.Size = new System.Drawing.Size(88, 20);
            this.ModelTypecombo.TabIndex = 34;
            this.ModelTypecombo.SelectedIndexChanged += new System.EventHandler(this.ModelTypecombo_SelectedIndexChanged);
            // 
            // Videobut
            // 
            this.Videobut.Location = new System.Drawing.Point(8, 294);
            this.Videobut.Name = "Videobut";
            this.Videobut.Size = new System.Drawing.Size(88, 35);
            this.Videobut.TabIndex = 30;
            this.Videobut.Text = "影像";
            this.Videobut.UseVisualStyleBackColor = true;
            this.Videobut.Click += new System.EventHandler(this.CamFormbut_Click);
            // 
            // CurModellabel
            // 
            this.CurModellabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CurModellabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CurModellabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CurModellabel.Location = new System.Drawing.Point(145, 4);
            this.CurModellabel.Name = "CurModellabel";
            this.CurModellabel.Size = new System.Drawing.Size(703, 16);
            this.CurModellabel.TabIndex = 35;
            this.CurModellabel.Text = "模板：";
            this.CurModellabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CGControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CurModellabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CamMenu);
            this.Controls.Add(this.deviceListView);
            this.Name = "CGControl";
            this.Size = new System.Drawing.Size(1110, 580);
            this.CamMenu.ResumeLayout(false);
            this.CamMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer updateDeviceListTimer;
        private System.Windows.Forms.ListView deviceListView;
        private System.Windows.Forms.MenuStrip CamMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem MenuFile_LoadPicture;
        private System.Windows.Forms.ToolStripMenuItem MenuFile_SavePicture;
        private System.Windows.Forms.ToolStripMenuItem MenuTool;
        private System.Windows.Forms.ToolStripMenuItem MenuTool_CamOpen;
        private System.Windows.Forms.ToolStripMenuItem MenuTool_CamClosed;
        private System.Windows.Forms.ToolStripMenuItem MenuTool_Video;
        private System.Windows.Forms.ToolStripMenuItem MenuTool_TakeImage;
        private System.Windows.Forms.ToolStripMenuItem MenuTool_Match;

        private System.Windows.Forms.Button LoadModelbut;
        private System.Windows.Forms.Button SaveModelbut;
        private System.Windows.Forms.Button Searchbut;
        private System.Windows.Forms.Button SetParameterbut;
        private System.Windows.Forms.Button EditModelbut;
        private System.Windows.Forms.Button LearnModelbut;
        private System.Windows.Forms.Button LearnAreaModelbut;
        private System.Windows.Forms.Label Xlabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Slabel;
        private System.Windows.Forms.Label Alabel;
        private System.Windows.Forms.Label Ylabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label CurModellabel;
        private System.Windows.Forms.Button Videobut;
        private System.Windows.Forms.ComboBox ModelTypecombo;
        private System.Windows.Forms.Button Learnbut;

    }
}
