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


namespace PMACam
{
    public partial class SourceManage : Form
    {
        public SourceManage()
        {
            InitializeComponent();
            this.cbb_Type.SelectedIndex = 0;
        }

 
        private List<SourceBuffer> _sourceBuffer;
        private List<ExecuteBuffer> _executeBuffer;
        int Source_number;
        public SourceManage(List<SourceBuffer> sourceBuffer_manage, List<ExecuteBuffer> executeBuffer_manage,int Soucenumber_get)
        {
            InitializeComponent();

            _sourceBuffer = sourceBuffer_manage;
            Source_number = Soucenumber_get;
            _executeBuffer = executeBuffer_manage;

        }

        private void SourceManage_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            int Rownumber = 0;

                foreach (Info_Source keybufferobject in _sourceBuffer[Source_number]._s_ObjectBuffer.Keys)
                {
                    dataGridView1.Rows.Add();


                    dataGridView1.Rows[Rownumber].Cells[1].Value = keybufferobject.Type;
                    if (keybufferobject.Type == "region")
                        dataGridView1.Rows[Rownumber].Cells[0].Value = keybufferobject.Name.Substring(0, keybufferobject.Name.Length-7);
                    else if (keybufferobject.Type == "shapemode_contour")
                        dataGridView1.Rows[Rownumber].Cells[0].Value = keybufferobject.Name.Substring(0, keybufferobject.Name.Length - 18);
                    else if (keybufferobject.Type == "shapemode_grey")
                        dataGridView1.Rows[Rownumber].Cells[0].Value = keybufferobject.Name.Substring(0, keybufferobject.Name.Length - 15);
                    else if (keybufferobject.Type == "tuple")
                        dataGridView1.Rows[Rownumber].Cells[0].Value = keybufferobject.Name.Substring(0, keybufferobject.Name.Length - 6);
                    dataGridView1.Rows[Rownumber].Cells[2].Value = keybufferobject.Path;
                    Rownumber++;

                }
                foreach (Info_Source keybuffercontrol in _sourceBuffer[Source_number]._s_ControlBuffer.Keys)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[Rownumber].Cells[1].Value = keybuffercontrol.Type;
                    if (keybuffercontrol.Type == "region")
                        dataGridView1.Rows[Rownumber].Cells[0].Value = keybuffercontrol.Name.Substring(0, keybuffercontrol.Name.Length-7);
                    else if(keybuffercontrol.Type == "shapemode_contour")
                        dataGridView1.Rows[Rownumber].Cells[0].Value = keybuffercontrol.Name.Substring(0, keybuffercontrol.Name.Length-18);
                    else if (keybuffercontrol.Type == "shapemode_grey")
                        dataGridView1.Rows[Rownumber].Cells[0].Value = keybuffercontrol.Name.Substring(0, keybuffercontrol.Name.Length - 15);
                    else if (keybuffercontrol.Type == "tuple")
                        dataGridView1.Rows[Rownumber].Cells[0].Value = keybuffercontrol.Name.Substring(0, keybuffercontrol.Name.Length - 6);
                    dataGridView1.Rows[Rownumber].Cells[2].Value = keybuffercontrol.Path;
                    Rownumber++;

                }
            

            dataGridView1.Refresh();
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

            Info_Source Del_item = new Info_Source();

            int model = 0;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {

                if (row.Cells[1].Value.ToString() == "region")
                {
                    model = 0;
                    foreach (Info_Source keybufferobject in _sourceBuffer[Source_number]._s_ObjectBuffer.Keys)
                    {
                        if (keybufferobject.Name == (row.Cells[0].Value.ToString()+ ".region") && keybufferobject.Type == row.Cells[1].Value.ToString() && keybufferobject.Path == row.Cells[2].Value.ToString())
                        {
                            Del_item = keybufferobject;
                            break;
                        }

                    }
                    Clear_list(Del_item.Name, model);
                    _sourceBuffer[Source_number]._s_ObjectBuffer.Remove(Del_item);
                 //   Clear_list(Del_item.Name,model);
                    UpdateSourceBuffer.OnSendUpdateSourceBuffer(new UpdateSourceBufferEventArgs(_sourceBuffer,_executeBuffer));
                }

                else
                {
                    model = 1;
                    foreach (Info_Source keybuffcontrol in _sourceBuffer[Source_number]._s_ControlBuffer .Keys)
                    {
                        if (  keybuffcontrol.Type == row.Cells[1].Value.ToString() && keybuffcontrol.Path == row.Cells[2].Value.ToString())
                        {
                            if (keybuffcontrol.Type == "shapemode_contour")
                            {
                                if (keybuffcontrol.Name == row.Cells[0].Value.ToString() + ".shapemode_contour")
                                {
                                    Del_item = keybuffcontrol;
                                    break;
                                }

                            }
                            if (keybuffcontrol.Type == "shapemode_grey")
                            {
                                if (keybuffcontrol.Name == row.Cells[0].Value.ToString() + ".shapemode_grey")
                                {
                                    Del_item = keybuffcontrol;
                                    break;
                                }

                            }
                            if (keybuffcontrol.Type == "tuple")
                            {
                                if (keybuffcontrol.Name == row.Cells[0].Value.ToString() + ".tuple")
                                {
                                    Del_item = keybuffcontrol;
                                    break;
                                }

                            }
                            if (keybuffcontrol.Type == "xld")
                            {
                                if (keybuffcontrol.Name == row.Cells[0].Value.ToString() + ".xld")
                                {
                                    Del_item = keybuffcontrol;
                                    break;
                                }

                            }

                        }

                    }
                    Clear_list(Del_item.Name, 1);
                    _sourceBuffer[Source_number]._s_ControlBuffer.Remove(Del_item);

                    UpdateSourceBuffer.OnSendUpdateSourceBuffer(new UpdateSourceBufferEventArgs(_sourceBuffer, _executeBuffer));
                
                
                
                
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




        private void Clear_list(String name,int model)
        {

            bool imagebuffer_remove = true;
            bool control_remove = true;
            if (model == 0)
            {

                foreach (int keyr in _executeBuffer[Source_number].all_test_buffer.Keys)
                {
                    for (int mnumber = 0; mnumber < _executeBuffer[Source_number].all_test_buffer[keyr].imagebuffer.Count; mnumber++)
                    {
                        if (name == _executeBuffer[Source_number].all_test_buffer[keyr].imagebuffer[mnumber])
                        {
                            imagebuffer_remove = false;
                            break;
                        }

                    }
                    if (!imagebuffer_remove)
                        break;
                }

                if (imagebuffer_remove)
                    _executeBuffer[Source_number].imageBuffer.Remove(name);


            }
            else
            {

                foreach (int keyc in _executeBuffer[Source_number].all_test_buffer.Keys)
                {
                    for (int mnumber = 0; mnumber < _executeBuffer[Source_number].all_test_buffer[keyc].controlbuffer.Count; mnumber++)
                    {
                        if (name == _executeBuffer[Source_number].all_test_buffer[keyc].controlbuffer[mnumber])
                        {
                            control_remove  = false;
                            break;
                        }

                    }
                    if (!control_remove)
                        break;
                }

                if (control_remove)
                    _executeBuffer[Source_number].controlBuffer.Remove(name);
            
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

            string Typeobject = cbb_Type.SelectedItem.ToString();
            if (Typeobject == null)
            {
                MessageBox.Show("请选择类型，再填加");
                return;
            }
            HObject hobj;
            HTuple obj_model = new HTuple();

            if (Typeobject == "region")
            {


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (txt_Name.Text == row.Cells[0].Value.ToString())
                    {
                        MessageBox.Show("已包含同名资源，请重新设定");
                        return;
                    }
                }
                HOperatorSet.GenEmptyObj(out hobj);
                try
                {
                    HOperatorSet.ReadRegion(out hobj, txt_Path.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("载入区域region失败，请检测类型是否正确");
                    return;
                }
                Info_Source new_info = new Info_Source(txt_Name.Text+".region", "region", txt_Path.Text);
                _sourceBuffer[Source_number]._s_ObjectBuffer.Add(new_info, hobj);
                int index = dataGridView1.Rows.Add();

                dataGridView1.Rows[index].Cells[0].Value = txt_Name.Text.Trim();
                dataGridView1.Rows[index].Cells[1].Value = cbb_Type.SelectedItem.ToString();
                dataGridView1.Rows[index].Cells[2].Value = txt_Path.Text;
                UpdateSourceBuffer.OnSendUpdateSourceBuffer(new UpdateSourceBufferEventArgs(_sourceBuffer, _executeBuffer));
            }


            if (Typeobject == "shapemode_contour" || Typeobject == "tuple" || Typeobject == "shapemode_grey")
            {


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (txt_Name.Text == row.Cells[0].Value.ToString())
                    {
                        MessageBox.Show("已包含同名资源，请重新设定");
                        return;
                    }
                }
               // HOperatorSet.GenEmptyObj(out obj_model);
                Info_Source new_info = new Info_Source(txt_Name.Text, "region", txt_Path.Text);
                try
                {
                    if (Typeobject == "shapemode_contour")
                    {
                        HOperatorSet.ReadShapeModel(txt_Path.Text, out obj_model);
                        new_info.Name = txt_Name.Text + ".shapemode_contour";
                        new_info.Type = "shapemode_contour";
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("载入形状模板失败，请检查类型是否正确");
                    return;
                }


                try
                {

                    if (Typeobject == "shapemode_grey")
                    {
                        //HOperatorSet.ReadShapeModel(txt_Path.Text, out obj_model);
                        HOperatorSet.ReadTemplate(txt_Path.Text, out obj_model);
                        new_info.Name = txt_Name.Text + ".shapemode_grey";
                        new_info.Type = "shapemode_grey";
                    
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("载入灰度模板失败，请检查类型是否正确");
                    return;
                }

                try
                {


                    if (Typeobject == "tuple")
                    {

                        HOperatorSet.ReadTuple(txt_Path.Text, out obj_model);
                        new_info.Name = txt_Name.Text + ".tuple";
                        new_info.Type = "tuple";
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("载入Tuple文件失败，请检查类型是否正确");
                    return;
                }

                _sourceBuffer[Source_number]._s_ControlBuffer.Add(new_info, obj_model);
                int index = dataGridView1.Rows.Add();

                dataGridView1.Rows[index].Cells[0].Value = txt_Name.Text.Trim();
                dataGridView1.Rows[index].Cells[1].Value = cbb_Type.SelectedItem.ToString();
                dataGridView1.Rows[index].Cells[2].Value = txt_Path.Text;
                UpdateSourceBuffer.OnSendUpdateSourceBuffer(new UpdateSourceBufferEventArgs(_sourceBuffer, _executeBuffer));
            }


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

        }

        private void btn_Repair_Click(object sender, EventArgs e)
        {
            

        }

        private void SourceManage_FormClosing(object sender, FormClosingEventArgs e)
        {



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



  
    }
}
