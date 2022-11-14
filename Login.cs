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
    public partial class Login : Form
    {
        public static bool IsAdmin;
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Warning...!!!,User Name Is Required...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Warning...!!!,Password Is Required...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";
            using(SqlConnection con = new SqlConnection(connectionStr))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "spLogin";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);


                    if(dt.Rows.Count>0)
                    {
                        DataRow dr = dt.Rows[0];
                        IsAdmin = bool.Parse(dr["IsAdmin"].ToString());

                        Dashboard obj = new Dashboard();
                        obj.Show();
                    }
                    else
                    {
                        lblMessage.Text = "Invalid UserName/or Password";

                    }
                }

            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }
    }
}
