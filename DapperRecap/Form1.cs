using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Dapper;

namespace DapperRecap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            using (IDbConnection connection = new SqlConnection(Connection.ConStr("DapperReview")))  //"DapperReview" is naam in App.Config
            {
                //var result = connection.Query<DapperTest>("Select * from DapperTest").ToList();
                string sql = "select id, firstname, lastname from dappertest where age=@age";
                var list = connection.Query<DapperTest>(sql, new { age = txtAge.Text }).ToList();
                lstData.DataSource = list;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            DapperTest dapperTest = new DapperTest();
            dapperTest.FirstName = txtFirstName.Text;
            dapperTest.LastName = txtLastName.Text;
            dapperTest.Age = int.Parse(txtAgeInsert.Text);
            dapperTest.Gender = txtGender.Text;

            //DIT IS SQL INJECTION! DIT IS HEEEEL GEVAARLIJK!  NOOIT ' GEBRUIKEN IN QUERIES.
            //string sql = $"insert into dapperTest(firstName, lastname, age, gender) values('{txtFirstName.Text}', '{txtLastName.Text}', {int.Parse(txtAgeInsert.Text)}, '{txtGender.Text}')";
            string sql = "insert into dappertest (firstName, lastname, age, gender) values (@FirstName, @LastName, @Age, @Gender)";

            using (IDbConnection connection = new SqlConnection(Connection.ConStr("DapperReview")))
            {
                connection.Query(sql, dapperTest);
            }
            GetData();
            ClearTextBoxes();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            using (IDbConnection connection = new SqlConnection(Connection.ConStr("DapperReview")))  //"DapperReview" is naam in App.Config
            {
                //var result = connection.Query<DapperTest>("Select * from DapperTest").ToList();
                string sql = "select * from dappertest";
                var list = connection.Query<DapperTest>(sql, new { age = txtAge.Text }).ToList();
                lstData.DataSource = list;
            }
        }

        private void ClearTextBoxes()
        {
            txtAge.Clear();
            txtAgeInsert.Clear();
            txtFirstName.Clear();
            txtGender.Clear();
            txtLastName.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void lstData_DoubleClick(object sender, EventArgs e)
        {
            var dappertest = lstData.SelectedItem as DapperTest;
            if (dappertest != null)
            {
                txtFirstName.Text = dappertest.FirstName;
                txtLastName.Text = dappertest.LastName;
                txtAgeInsert.Text = dappertest.Age.ToString();
                txtGender.Text = dappertest.Gender;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DapperTest dapperTest = new DapperTest();
            dapperTest.FirstName = txtFirstName.Text;
            dapperTest.LastName = txtLastName.Text;
            dapperTest.Age = int.Parse(txtAgeInsert.Text);
            dapperTest.Gender = txtGender.Text;

            string sql = $"Delete from dappertest where firstname = @FirstName and lastname = @LastName and age = @age and gender = @gender";
            using (IDbConnection connection = new SqlConnection(Connection.ConStr("DapperReview")))  //"DapperReview" is naam in App.Config
            {
                //var list = connection.Query<DapperTest>(sql, dapperTest).ToList();
                //var command = connection.Execute(sql, dapperTest);
                //connection.Execute(sql, new { txtFirstName = dapperTest.FirstName, txtLastName = dapperTest.LastName, txtAge = dapperTest.Age, txtGender = dapperTest.Gender });
                connection.Execute(sql, dapperTest);
            }
            GetData();
        }
    }
}
