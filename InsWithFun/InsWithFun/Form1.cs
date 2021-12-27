using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsWithFun
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgv_instructor.DataSource = DBLayer.select("select ins_id,ins_name,ins_degree,salary from instructor");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int roweffect = DBLayer.dml($"insert into instructor(ins_id,ins_name,ins_degree,salary) values({txt_id.Text},'{txt_name.Text}','{txt_degree.Text}',{txt_salary.Text})");
               if(roweffect > 0)
            {
                txt_id.Text = txt_name.Text = txt_degree.Text = txt_salary.Text = "";
                dgv_instructor.DataSource = DBLayer.select("select * from instructor");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            int roweffect = DBLayer.dml($"update instructor set ins_name='{txt_name.Text}',ins_degree='{txt_degree.Text}',salary={txt_salary.Text} where ins_id={txt_id.Text}  ");
            if(roweffect > 0)
            {
                txt_id.Text = txt_name.Text = txt_degree.Text = txt_salary.Text = "";
                dgv_instructor.DataSource = DBLayer.select("select * from instructor");
            }
        }

        private void dgv_instructor_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_id.Text = dgv_instructor.SelectedRows[0].Cells[0].Value.ToString();
            txt_name.Text = dgv_instructor.SelectedRows[0].Cells[1].Value.ToString();
            txt_degree.Text = dgv_instructor.SelectedRows[0].Cells[2].Value.ToString();
            txt_salary.Text = dgv_instructor.SelectedRows[0].Cells[3].Value.ToString();
            txt_id.Enabled = false;
            btn_add.Visible = false;
            btn_update.Visible = true;
            btn_remove.Visible = true;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            int roweffect = DBLayer.dml($"delete from instructor where ins_id={txt_id.Text}");
            if (roweffect > 0)
            {
                txt_id.Text = txt_name.Text = txt_degree.Text = txt_salary.Text = "";
                dgv_instructor.DataSource = DBLayer.select("select * from instructor");
            }
        }
    }
}
