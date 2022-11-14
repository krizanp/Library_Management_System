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
    public partial class Borrow : Form
    {
        public Borrow()
        {
            InitializeComponent();
        }
        #region outer layer

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
                    cmd.CommandText = "spGetBorrrow";

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvBorrow.DataSource = dt;
                }

            }
        }
        private void empty()
        {

            cmbStudentId.SelectedValue = -1;
            cmbBookId.SelectedValue = -1;
            txtBroughtDate.Value = DateTime.Today;
            txtTakenDate.Value = DateTime.Today;

            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;


        }

        private void Borrow_Load(object sender, EventArgs e)
        {

            {
                PopulateGrid();
                string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";

                using (SqlConnection con = new SqlConnection(connectionStr))
                {

                    con.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "Select id,(firstname+' '+lastname)name from students";

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        DataRow dr = dt.NewRow();
                        dr.ItemArray = new object[] { 0, "-Select Students-" };
                        dt.Rows.InsertAt(dr, 0);


                        cmbStudentId.DisplayMember = "name";
                        cmbStudentId.ValueMember = "id";

                        cmbStudentId.DataSource = dt;
                        txtBroughtDate.Enabled = false;
                    }

                }
                using (SqlConnection con = new SqlConnection(connectionStr))
                {

                    con.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "Select id,name from book";

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        DataRow dr = dt.NewRow();
                        dr.ItemArray = new object[] { 0, "-Select Book-" };
                        dt.Rows.InsertAt(dr, 0);


                        cmbBookId.DisplayMember = "name";
                        cmbBookId.ValueMember = "id";

                        cmbBookId.DataSource = dt;

                    }

                }

                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                this.gvBorrow.Columns["id"].Visible = false;
                this.gvBorrow.Columns["StudentsId"].Visible = false;
                this.gvBorrow.Columns["BookId"].Visible = false;


            }
        }
        private int BorrowId;
        private void gvBorrow_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            BorrowId = int.Parse(gvBorrow.Rows[e.RowIndex].Cells[0].Value.ToString());

            cmbStudentId.SelectedValue = gvBorrow.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbBookId.SelectedValue = gvBorrow.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtTakenDate.Text = gvBorrow.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtBroughtDate.Text = gvBorrow.Rows[e.RowIndex].Cells[4].Value.ToString();
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            txtBroughtDate.Enabled = true;


        }

        #endregion
        #region save btn
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (cmbStudentId.SelectedIndex == 0) 
            { MessageBox.Show ("Student Not Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbStudentId.Focus();
                return;
            }
            if (cmbBookId.SelectedIndex == 0)
            {
                MessageBox.Show("Book Not Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbBookId.Focus();
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
                    cmd.CommandText = @"spSaveBorrow";

                    cmd.Parameters.AddWithValue("@StudentsId", cmbStudentId.SelectedValue);
                    cmd.Parameters.AddWithValue("@BookId", cmbBookId.SelectedValue);
                    cmd.Parameters.AddWithValue("@TakenDate", txtTakenDate.Text);




                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Borrowed Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateGrid();
                    empty();

                }


            }
        }

        #endregion
        #region  updatebtn
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbStudentId.SelectedIndex == 0)
            {
                MessageBox.Show("Student Not Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbStudentId.Focus();
                return;
            }
            if (cmbBookId.SelectedIndex == 0)
            {
                MessageBox.Show("Book Not Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbBookId.Focus();
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
                        cmd.CommandText = @"spUpdateBorrow";
                        
                        cmd.Parameters.AddWithValue("@StudentsId", cmbStudentId.SelectedValue);
                        cmd.Parameters.AddWithValue("@BookId", cmbBookId.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id", BorrowId);
                        cmd.Parameters.AddWithValue("@TakenDate", txtTakenDate.Text);
                        cmd.Parameters.AddWithValue("@BroughtDate", txtTakenDate.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Updated Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PopulateGrid();
                        empty();
                    }

                }
            }

        #endregion
        #region Delete btn

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
                                cmd.CommandText = @"spDeleteBorrow";
                                cmd.Parameters.AddWithValue("@Id", BorrowId);



                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Deleted Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                PopulateGrid();
                                empty();
                            }

                        }
                    }
                }
            }
        }

            #endregion

            #region Search
            private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {

                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spSearchBorrow";
                    cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvBorrow.DataSource = dt;
                }

            }
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            empty();
        }
    }
}

