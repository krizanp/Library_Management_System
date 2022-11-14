using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSKrizan
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void securityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration obj = new Registration();
            obj.Show();

        }

        private void borrowerHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Borrower_History frmBorrower = new Borrower_History() ;
            frmBorrower.Show();
        }

        private void bookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Book objBook = new Book();
            objBook.Show();

        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student objStudent = new Student();
            objStudent.Show();
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Author objAuthor = new Author();
            objAuthor.Show();
        }

        private void borrowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Borrow objBorrow = new Borrow();
            objBorrow.Show();
        }

        private void bookHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Book_Information objBookInformation = new Book_Information();
            objBookInformation.Show();
        }

        private void yearWiseBorrowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Year_Wise_Borrow objYearWiseBorrow = new Year_Wise_Borrow();
            objYearWiseBorrow.Show();
        }

        private void passwwordChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword objChangePassword = new ChangePassword();
            objChangePassword.Show();
        }

        private void logOutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Login objLogin = new Login();
            objLogin.Show();
        }

        private void backupToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void bookTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Type objType = new Type();
            objType.Show();
        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
