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
using System.Text.RegularExpressions;
namespace LMSKrizan
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }
        public void ClearControls()
        {
            txtUserName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            chkIsAdmin.Checked = false;
        }
        private void Registration_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;
            if (Login.IsAdmin==true)
            {
                btnRegister.Enabled = true;
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
                txtConfirmPassword.Enabled = true;
                txtEmail.Enabled = true;
                chkIsAdmin.Enabled = true;
            }
                else
            {
                btnRegister.Enabled = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                txtEmail.Enabled = false;
                chkIsAdmin.Enabled = false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtUserName.Text == string.Empty)
                {
                    MessageBox.Show("Warning...!!!,User Name Is Required...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!Regex.IsMatch(txtEmail.Text, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"))
                {
                    MessageBox.Show("Warning...!!!,Enter Valid Email...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPassword.Text == string.Empty)
                {
                    MessageBox.Show("Warning...!!!,Password Is Required...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Warning...!!!,Password And Confirm Password Should Match...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                #region registration
                string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
                using (SqlConnection con = new SqlConnection(connectionStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "spRegisteration";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@isadmin", chkIsAdmin.Checked);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Success...!!!,User Registered Successfully...!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearControls();

                    }
                }
            }
            catch (Exception )
            {

                MessageBox.Show("something error!", "warn", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        #endregion

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
