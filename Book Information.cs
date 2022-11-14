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
    public partial class Book_Information : Form
    {
        public Book_Information()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbBookName.SelectedIndex = 0;
            btnSearch.Enabled = true;


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmbBookName.SelectedIndex==0)
            {
                MessageBox.Show("Warning...!!!,Book Not Selected...!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.CommandText = "spBookInformation";
                        cmd.Parameters.AddWithValue("@BookName", cmbBookName.SelectedValue.ToString());

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvBookName.DataSource = dt;
                        cmd.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private void Book_Information_Load(object sender, EventArgs e)
        {
            string connectionStr = @"Data Source=DESKTOP-UN216IH\SQLEXPRESS; Initial Catalog=database;Integrated Security=true;";

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
                    dr.ItemArray = new object[] { 0, "-Select Books-" };
                    dt.Rows.InsertAt(dr, 0);


                    cmbBookName.DisplayMember = "name";
                    cmbBookName.ValueMember = "id";

                    cmbBookName.DataSource = dt;
                  
                }

            }
        }

        private void cmbBookName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
