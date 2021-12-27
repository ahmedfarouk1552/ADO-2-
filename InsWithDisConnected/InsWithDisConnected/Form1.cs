using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace InsWithDisConnected
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        DataTable dt;
        public Form1()
        {
            
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["iticon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from instructor", con);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
             dt = new DataTable();
            adpt.Fill(dt);
            dgv_instructor.DataSource = dt;
            
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.NewRow();
            dr["ins_id"] = txt_id.Text;
            dr["ins_name"] = txt_name.Text;
            dr["ins_degree"] = txt_degree.Text;
            dr["salary"] = txt_salary.Text;
            dt.Rows.Add(dr);
            dgv_instructor.DataSource = dt;
            txt_id.Text = txt_name.Text = txt_degree.Text = txt_salary.Text = "";
            lbl_result.Text = "Successfully Added";
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

        private void btn_update_Click(object sender, EventArgs e)
        {
            foreach (DataRow  item in dt.Rows)
            {
                if (item["ins_id"].ToString() == txt_id.Text)
                {
                    item["ins_name"] = txt_name.Text;
                    item["ins_degree"] = txt_degree.Text;
                    item["salary"] = txt_salary.Text;

                }
                
            }
            txt_id.Enabled = true;
            btn_add.Visible = true;
            btn_update.Visible = false;
            lbl_result.Text = "Data Updated";
            txt_id.Text = txt_name.Text = txt_degree.Text = txt_salary.Text = "";
            dgv_instructor.DataSource = dt;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            
            
            foreach (DataRow  item in dt.Rows)
            {
               
               if(item["ins_id"].ToString()== txt_id.Text )
                {
                    item.Delete();
                   
                }
            }
            txt_id.Text = txt_name.Text = txt_degree.Text = txt_salary.Text = "";
            btn_remove.Visible = false;
            btn_update.Visible = false;
            btn_add.Visible = true;
            txt_id.Enabled = true;
            lbl_result.Text = "Row Deleted";
            dgv_instructor.DataSource = dt;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {            //insert
            SqlCommand insertcommand = new SqlCommand("insert into instructor(ins_id,ins_name,ins_degree,salary) values(@id,@name,@degree,@salary)", con);
            insertcommand.Parameters.Add("@id", SqlDbType.Int, 4, "ins_id");
            insertcommand.Parameters.Add("@name", SqlDbType.NVarChar, 50, "ins_name");
            insertcommand.Parameters.Add("@degree", SqlDbType.NVarChar, 50, "ins_degree");
            insertcommand.Parameters.Add("@salary", SqlDbType.Money, 6, "Salary");

            //update
            SqlCommand updatecommand = new SqlCommand("update instructor set ins_name=@name,ins_degree=@degree,salary=@salary where ins_id=@id", con);

            updatecommand.Parameters.Add("@id", SqlDbType.Int, 4, "ins_id");
            updatecommand.Parameters.Add("@name", SqlDbType.NVarChar, 50, "ins_name");
            updatecommand.Parameters.Add("@degree", SqlDbType.NVarChar, 50, "ins_degree");
            updatecommand.Parameters.Add("@salary", SqlDbType.Money, 9, "salary");

                    //delete
            SqlCommand deletecommand = new SqlCommand("delete from instructor where ins_id=@id", con);

            deletecommand.Parameters.Add("@id", SqlDbType.Int, 4, "ins_id");
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = insertcommand;
            adapter.UpdateCommand = updatecommand;
            adapter.DeleteCommand = deletecommand;


             adapter.Update(dt);




        }
    }
}
