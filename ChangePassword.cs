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
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }
        private int UserId = 0;
        private void button1_Click(object sender, EventArgs e)
        {

            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "spLogin";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@Password", txtCurrentPassword.Text);
                    
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        using (SqlCommand cmmd = new SqlCommand())
                        {
                            cmmd.Connection = con;
                            DataRow dr = dt.Rows[0];
                            UserId = int.Parse(dr["id"].ToString());
                            cmmd.CommandType = CommandType.StoredProcedure;
                            cmmd.CommandText = "spChangePassword";
                            cmmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                            cmmd.Parameters.AddWithValue("@Password", txtNewPassword.Text);
                            cmmd.Parameters.AddWithValue("@UserId", UserId);
                            cmmd.ExecuteNonQuery();
                            MessageBox.Show("Password Change Successfully", "infromation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }





                    }

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Success...!!!, Password Successfully...!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            txtCurrentPassword.UseSystemPasswordChar = false;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            txtCurrentPassword.UseSystemPasswordChar = true;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            txtNewPassword.UseSystemPasswordChar = false;
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            txtNewPassword.UseSystemPasswordChar = true;
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = false;
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = true;
        }
    }
}



