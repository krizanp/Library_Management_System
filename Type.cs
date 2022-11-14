using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSKrizan
{
    public partial class Type : Form
    {
        public Type()
        {
            InitializeComponent();
        }

        private int typeID = 0;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBookType.Text == string.Empty)
            {
                MessageBox.Show("Type TextBox Is Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookType.Focus();
                return;
            }

            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "spSaveBookType";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookType", txtBookType.Text);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Success...!!!,  Data Saved Successfully...!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                if (txtBookType.Text == string.Empty)
                {
                    MessageBox.Show("Type TextBox Is Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBookType.Focus();
                    return;
                }

                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "spUpdateBookType";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TypeID", typeID);
                    cmd.Parameters.AddWithValue("@BookType", txtBookType.Text);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Success...!!!,  Data Updated Successfully...!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
        }

        private void Type_Load(object sender, EventArgs e)
        {

            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {

                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "spGetbooktype";
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvtype.DataSource = dt;

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                this.gvtype.Columns["Id"].Visible = false;
            }
        }

        private void gvtype_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            typeID = int.Parse(gvtype.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtBookType.Text = gvtype.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtBookType.Text = string.Empty;
            btnSave.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtBookType.Text == string.Empty)
            {
                MessageBox.Show("Type TextBox Is Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookType.Focus();
                return;
            }

            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                                     
                        cmd.Connection = con;
                        cmd.CommandText = "spDeleteBookType";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TypeID", typeID);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Success...!!!,  Data Deleted Successfully...!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);



                   

                }   
            }return;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            {
                string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
                // SQL Connection
                using (SqlConnection con = new SqlConnection(connectionStr))
                {

                    //Opening connection
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spSearchBookType";
                        cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvtype.DataSource = dt;
                    }

                }
            }
        }
    }
}
    



    

