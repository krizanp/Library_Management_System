using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LMSKrizan
{
    public partial class Author : Form
    {
        public Author()
        {
            InitializeComponent();
            
        }
        private void PopulateGrid()
        {
            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";

            using (SqlConnection con = new SqlConnection(connectionStr))
            {

                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spGetAuthor";

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvAuthor.DataSource = dt;
                }

            }
        }
        private int AuthorId = 0;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Author_Load(object sender, EventArgs e)
        {
           
            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "spGetAuthor";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    gvAuthor.DataSource = dt;
                }

            }
            this.gvAuthor.Columns["Id"].Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtFirstName.Text == string.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid First Name...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtLastName.Text == string.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Last Name...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
                using (SqlConnection con = new SqlConnection(connectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "spAuthor";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Success...!!!, Author's Data Registered Successfully...!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtFirstName.Text = string.Empty;
                        txtLastName.Text = string.Empty;



                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
                MessageBox.Show("Warning...!!!,Something Went Wrong, Try Again...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

        }

        private void gvAuthor_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AuthorId = int.Parse(gvAuthor.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtFirstName.Text = gvAuthor.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtLastName.Text = gvAuthor.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (txtFirstName.Text == string.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid First Name...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtLastName.Text == string.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Last Name...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
                using (SqlConnection con = new SqlConnection(connectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "spUpdateAuthor";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AuthorId", AuthorId);
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Success...!!!, Author's Data Updated Successfully...!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtFirstName.Text = string.Empty;
                        txtLastName.Text = string.Empty;



                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void gvAuthor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            {
                {
                    var confirm = MessageBox.Show("Do you want to delete", "Delete", MessageBoxButtons.YesNo
                        , MessageBoxIcon.Information);
                    if (confirm.ToString() == "Yes")
                    {
                        string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";

                        using (SqlConnection con = new SqlConnection(connectionStr))
                        {
                            //Opening connection
                            con.Open();
                            //SQL Command

                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = @"spDeleteAuthor";
                                cmd.Parameters.AddWithValue("@Id", AuthorId);



                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Deleted Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                PopulateGrid();

                            }

                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtSearch.Text = string.Empty;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
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
                        cmd.CommandText = "spSearchAuthor";
                        cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvAuthor.DataSource = dt;
                    }

                }
            }
        }
    }
}
