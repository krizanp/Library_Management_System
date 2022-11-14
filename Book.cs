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
    public partial class Book : Form
    {
        public Book()
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
                    cmd.CommandText = "spGetBook";

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvBookName.DataSource = dt;
                }

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBookName.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Book Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookName.Focus();
                return;
            }
            if (txtPageCount.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Page Count", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPageCount.Focus();
                return;
            }
            if (txtPoint.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Point", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPoint.Focus();
                return;
            }
            if (cmbAuthor.SelectedIndex == 0)
            {
                MessageBox.Show("Warning...!!!,Invalid Author ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbAuthor.Focus();
                return;
            }
            if (cmbType.SelectedIndex == 0)
            {
                MessageBox.Show("Warning...!!!,Invalid Book Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbType.Focus();
                return;
            }
            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "spSaveBook";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookName", txtBookName.Text);
                    cmd.Parameters.AddWithValue("@PageCount", txtPageCount.Text);
                    cmd.Parameters.AddWithValue("@Point", txtPoint.Text);
                    cmd.Parameters.AddWithValue("@Author", cmbAuthor.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Type", cmbType.SelectedValue.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Success...!!! Saved Successfully...!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
        private void LoadAuthor()
        {
            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = @"select Id, (FirstName+' '+LastName)Name from author";
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbAuthor.DisplayMember = "Name";
                    cmbAuthor.ValueMember = "Id";
                    cmbAuthor.DataSource = dt;



                }
            }

        }
        private void LoadType()
        {

            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = @"select Id, Name from Type";
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbType.DisplayMember = "Name";
                    cmbType.ValueMember = "Id";
                    cmbType.DataSource = dt;



                }
            }

        }
        private void Book_Load(object sender, EventArgs e)
        {
            LoadAuthor();
            LoadType();
            PopulateGrid();
            this.gvBookName.Columns["Id"].Visible = false;
            this.gvBookName.Columns["Id1"].Visible = false;
            this.gvBookName.Columns["Id2"].Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
                        cmd.CommandText = "spSearchBook";
                        cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvBookName.DataSource = dt;
                    }

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtBookName.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Book Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookName.Focus();
                return;
            }
            if (txtPageCount.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Page Count", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPageCount.Focus();
                return;
            }
            if (txtPoint.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Point", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPoint.Focus();
                return;
            }
            if (cmbAuthor.SelectedIndex == 0)
            {
                MessageBox.Show("Author Not Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbAuthor.Focus();
                return;
            }
            if (cmbType.SelectedIndex == 0)
            {
                MessageBox.Show("Book Type Not Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
               cmbType.Focus();
                return;
            }
            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {

                con.Open();


                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = @"spUpdateBook";

                    cmd.Parameters.AddWithValue("@typeId", cmbType.SelectedValue);
                    cmd.Parameters.AddWithValue("@Id", BookId);
                    cmd.Parameters.AddWithValue("@Authorid", cmbAuthor.SelectedValue);
                    cmd.Parameters.AddWithValue("@point", txtPoint.Text);
                    cmd.Parameters.AddWithValue("@pagecount", txtPageCount.Text);
                    cmd.Parameters.AddWithValue("@bookname", txtBookName.Text);
                    
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Updated Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateGrid();

                }

            }
        }
        private int BookId = 0;
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
                                cmd.CommandText = @"spDeleteBook";
                                cmd.Parameters.AddWithValue("@Id", BookId);



                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Deleted Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                PopulateGrid();

                            }

                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtBookName.Text = string.Empty;
            txtPageCount.Text = string.Empty;
            txtPoint.Text = string.Empty;
            txtSearch.Text = string.Empty;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled=false;
        }
    }
}