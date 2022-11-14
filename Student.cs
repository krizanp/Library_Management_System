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
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSave_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid FirstName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return;
            }
            if (txtLastName.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid LastName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return;
            }
            if (txtAddress.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            if (rbMale.Checked == false && rbFemale.Checked == false)
            {
                MessageBox.Show("Warning...!!!,Gender Not Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rbMale.Focus();
                return;
            }
            if (txtClass.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClass.Focus();
                return;
            }
            if (txtPoint.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Points", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPoint.Focus();
                return;
            }
            if (txtPhoneNumber.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Phone Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPoint.Focus();
                return;
            }

            if (txtPoint.Text != string.Empty)
            {
                decimal Points;
                bool CheckPoints = decimal.TryParse(txtPoint.Text, out Points);
                if (CheckPoints == false)
                {
                    MessageBox.Show("Warning...!!!,Invalid Points", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPoint.Focus();
                    return;
                }
                string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
                using (SqlConnection con = new SqlConnection(connectionStr))
                {
                    string gender = "";
                    if (rbMale.Checked == true) { gender = "M"; } else { gender = "F"; }
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "spSaveStudents";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@Phoneno", txtPhoneNumber.Text);
                        cmd.Parameters.AddWithValue("@BirthDate", txtBirthDate.Text);
                        cmd.Parameters.AddWithValue("@Class", txtClass.Text);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Point", decimal.Parse(txtPoint.Text));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Success...!!!, Students's Data Registered Successfully...!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        populategrid();
                    }
                }
            } }
        
        private int studentid = 0;
        private void populategrid()
        {
            string connectionstr = "data source=DESKTOP-8EJDGQ8;initial catalog=database;integrated security=true;";
            using (SqlConnection con = new SqlConnection(connectionstr))
            {
                using (SqlCommand cmd = new SqlCommand())

                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getStudent";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvStudents.DataSource = dt;


                }
            }
        }

        private int StudentsId = 0;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid FirstName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return;
            }
            if (txtLastName.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid LastName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return;
            }
            if (rbMale.Checked == false && rbFemale.Checked == false)
            {
                MessageBox.Show("Warning...!!!,Gender Not Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rbMale.Focus();
                return;
            }
            if (txtClass.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClass.Focus();
                return;
            }
            if (txtPoint.Text == String.Empty)
            {
                MessageBox.Show("Warning...!!!,Invalid Points", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPoint.Focus();
                return;
            }
            if (txtPoint.Text != string.Empty)
            {
                decimal Points;
                bool CheckPoints = decimal.TryParse(txtPoint.Text, out Points);
                if (CheckPoints == false)
                {
                    MessageBox.Show("Warning...!!!,Invalid Points", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPoint.Focus();
                    return;
                }
                string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
                using (SqlConnection con = new SqlConnection(connectionStr))
                {
                    string gender = "";
                    if (rbMale.Checked == true) { gender = "M"; } else { gender = "F"; }
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "spUpdateStudents";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Studentsid", StudentsId);
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@Phoneno", txtPhoneNumber.Text);
                        cmd.Parameters.AddWithValue("@BirthDate", txtBirthDate.Text);
                        cmd.Parameters.AddWithValue("@Class", txtClass.Text);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Point", decimal.Parse(txtPoint.Text));



                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Success...!!!, Author's Data Updated Successfully...!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        populategrid();
                    }
                }
            }
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
                                cmd.CommandText = @"spDeleteStudents";
                                cmd.Parameters.AddWithValue("@StudentsId", StudentsId);
                                populategrid();

                            }
                        }
                    }
                }
            }

           }

        private void button4_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtBirthDate.Value = DateTime.Today;
            txtPoint.Text = string.Empty;
            txtClass.Text = string.Empty;
            rbMale.Checked = false;
            rbFemale.Checked = false;
        }

        private void Student_Load(object sender, EventArgs e)
        {
            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand())

                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spGetStudent";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvStudents.DataSource = dt;
                }
            }
            this.gvStudents.Columns["Id"].Visible = false;
        }

        private void gvStudents_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            studentid = int.Parse(gvStudents.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtFirstName.Text = gvStudents.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtLastName.Text = gvStudents.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtAddress.Text = gvStudents.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtPhoneNumber.Text = gvStudents.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtBirthDate.Value = DateTime.Parse(gvStudents.Rows[e.RowIndex].Cells[5].Value.ToString());
            txtClass.Text = gvStudents.Rows[e.RowIndex].Cells[6].Value.ToString();
            if (gvStudents.Rows[e.RowIndex].Cells[7].Value.ToString() == "M") { rbMale.Checked = true; } else { rbFemale.Checked = true; }
            txtPoint.Text = gvStudents.Rows[e.RowIndex].Cells[8].Value.ToString();
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
                        cmd.CommandText = "spSearchStudents";
                        cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvStudents.DataSource = dt;
                    }

                }
            }
        }
    } 
    }
