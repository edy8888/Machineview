using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace HYvision
{
    public partial class SourceManage : Form
    {
        public SourceManage()
        {
            InitializeComponent();
        }

        private SourceBuffer _sb;
        private INIOperation _source;
        private ExecuteBuffer _excBuffer;
        public SourceManage(SourceBuffer sb,INIOperation source,ExecuteBuffer executeBuffer )
        {
            InitializeComponent();
            _sb = sb;
            _source = source;
            _excBuffer = executeBuffer;

        }

        private Dictionary<string, HObject> sourceHobjTemp;
        private Dictionary<string, Object> sourceCtrlTemp;
        private void SourceManage_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            sourceHobjTemp=new Dictionary<string, HObject>(_sb._s_ObjectBuffer);
            sourceCtrlTemp = new Dictionary<string, Object>(_sb._s_ControlBuffer);
            List<string> sections = _source.ReadSections();
            for (int i = 0; i < sections.Count; i++)
            {
                
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = sections[i];
                dataGridView1.Rows[i].Cells[1].Value = _source.IniReadValue(sections[i],"type");
                dataGridView1.Rows[i].Cells[2].Value = _source.IniReadValue(sections[i], "path");
                //buffer是否含有，含有是否为空？
                if ((!_sb._s_ObjectBuffer.ContainsKey(sections[i])) && (!_sb._s_ControlBuffer.ContainsKey(sections[i])))
                {
                    MessageBox.Show("buffer中不含" + sections[i]);
                    dataGridView1.Rows[i].Cells[3].Value = "不在buffer中";
                }
                if (_sb._s_ObjectBuffer.ContainsKey(sections[i]))
                {
                    if (_sb._s_ObjectBuffer[sections[i]].IsInitialized())
                    {
                        dataGridView1.Rows[i].Cells[3].Value = "非空";
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[3].Value = "空";
                    }
                }
                if (_sb._s_ControlBuffer.ContainsKey(sections[i]))
                {
                    if (_sb._s_ControlBuffer[sections[i]]==null)
                    {
                        dataGridView1.Rows[i].Cells[3].Value = "非空";
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[3].Value = "空";
                    }
                }

            }
            cbb_Type.SelectedIndex = 0;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);//右键快捷列表显示在鼠标停留位置
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectcount = dataGridView1.SelectedRows.Count;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                _source.EraseSection(row.Cells[0].Value.ToString());
                if (_sb._s_ObjectBuffer.ContainsKey(row.Cells[0].Value.ToString()))
                {
                    _sb._s_ObjectBuffer.Remove(row.Cells[0].Value.ToString());
                }
                if (_sb._s_ControlBuffer.ContainsKey(row.Cells[0].Value.ToString()))
                {
                    _sb._s_ControlBuffer.Remove(row.Cells[0].Value.ToString());
                }
            }
            while (selectcount > 0)
            {
                
                if (!dataGridView1.SelectedRows[0].IsNewRow)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                selectcount--;
            }

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            cbb_Type.SelectedIndex = 0;
            txt_Name.Text = "";
            txt_Path.Text = "";
        }

        private ILoadSource Iloadsource;
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text.Trim() == "")
            {
                MessageBox.Show("请输入资源名称");
                return;
            }
            if (txt_Path.Text.Trim() == "")
            {
                MessageBox.Show("请输入资源路径");
                return;
            }

            List<string> sections1=_source.ReadSections();
            if (sections1.Contains(txt_Name.Text.Trim()))
            {
                MessageBox.Show("已包含同名资源，请重新设定");
                return;
            }
            
            string nscl = "HYvision" + "." + cbb_Type.SelectedItem + "source";
            var x = Activator.CreateInstance(null, nscl);
            Iloadsource = (ILoadSource)x.Unwrap();
            if (!Iloadsource.LoadSource(_sb, txt_Name.Text.Trim(), txt_Path.Text.Trim()))
            {
                MessageBox.Show("资源加载失败，请检查");
                return;
            }
            _source.IniWrite(txt_Name.Text.Trim(),"type",cbb_Type.SelectedItem.ToString());
            _source.IniWrite(txt_Name.Text.Trim(), "path", txt_Path.Text.Trim());
            
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = txt_Name.Text.Trim();
            dataGridView1.Rows[index].Cells[1].Value = cbb_Type.SelectedItem.ToString();
            dataGridView1.Rows[index].Cells[2].Value = txt_Path.Text;
            dataGridView1.Rows[index].Cells[3].Value = "非空";
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd=new OpenFileDialog();
            ofd.RestoreDirectory = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!ofd.FileName.EndsWith(".hobj") || ofd.FileName.EndsWith(".shm"))
                {
                    MessageBox.Show("请注意文件是否正确");
                }
                txt_Path.Text = ofd.FileName;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbb_Type.SelectedIndex = cbb_Type.Items.IndexOf(dataGridView1.SelectedRows[0].Cells[1].Value);
            txt_Name.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txt_Path.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btn_Repair_Click(object sender, EventArgs e)
        {
            
            if (_sb._s_ControlBuffer.ContainsKey(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
            {
                _sb._s_ControlBuffer.Remove(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
            if (_sb._s_ObjectBuffer.ContainsKey(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
            {
                _sb._s_ObjectBuffer.Remove(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }

            string nscl = "HYvision" + "." + cbb_Type.SelectedItem + "source";
            var x = Activator.CreateInstance(null, nscl);
            Iloadsource = (ILoadSource)x.Unwrap();
            if (!Iloadsource.LoadSource(_sb, txt_Name.Text.Trim(), txt_Path.Text.Trim()))
            {
                MessageBox.Show("资源新增失败，请检查");
                return;
            }
            _source.EraseSection(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            _source.IniWrite(txt_Name.Text.Trim(), "type", cbb_Type.SelectedItem.ToString());
            _source.IniWrite(txt_Name.Text.Trim(), "path", txt_Path.Text.Trim());
            dataGridView1.SelectedRows[0].Cells[0].Value = txt_Name.Text.Trim();
            dataGridView1.SelectedRows[0].Cells[1].Value = cbb_Type.SelectedItem;
            dataGridView1.SelectedRows[0].Cells[2].Value = txt_Path.Text.Trim();
            dataGridView1.SelectedRows[0].Cells[3].Value = "非空";
        }

        private void SourceManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (KeyValuePair<string,Object> kc in sourceCtrlTemp)
            {
                if (_excBuffer.controlBuffer.ContainsKey(kc.Key))
                {
                    _excBuffer.controlBuffer.Remove(kc.Key);
                }
            }
            foreach (KeyValuePair<string,HObject> ko in sourceHobjTemp)
            {
                if (_excBuffer.imageBuffer.ContainsKey(ko.Key))
                {
                    _excBuffer.imageBuffer.Remove(ko.Key);
                }
            }

            //将当前sourcebuffer更新到executebuffer
            foreach (KeyValuePair<string, HObject> ksb in _sb._s_ObjectBuffer)
            {
                if (_excBuffer.imageBuffer.ContainsKey(ksb.Key))
                {
                    MessageBox.Show(ksb.Key + "该类型与执行buffer相冲突无法添加");
                }
                else
                {
                    _excBuffer.imageBuffer.Add(ksb.Key,ksb.Value);
                }
            }

            foreach (KeyValuePair<string, Object> ksc in _sb._s_ControlBuffer)
            {
                if (_excBuffer.controlBuffer.ContainsKey(ksc.Key))
                {
                    MessageBox.Show(ksc.Key + "该类型与执行buffer相冲突无法添加");
                }
                else
                {
                    _excBuffer.controlBuffer.Add(ksc.Key, ksc.Value);
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



  
    }
}
