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
    public partial class Borrower_History : Form
    {
        public Borrower_History()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, EventArgs e)
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
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spBorrrowerHistory";
                        
                        cmd.Parameters.AddWithValue("@fromDate", txtFromDate.Text);
                        cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                        
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvBorrower.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                   

                }
            }

        }
    }
}
