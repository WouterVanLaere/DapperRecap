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
                string sql = "select firstname from dappertest where age=@age";
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
                connection.Query(sql,dapperTest); 
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            using (IDbConnection connection = new SqlConnection(Connection.ConStr("DapperReview")))  //"DapperReview" is naam in App.Config
            {
                //var result = connection.Query<DapperTest>("Select * from DapperTest").ToList();
                string sql = "select * from dappertest where age=@age";
                var list = connection.Query<DapperTest>(sql, new { age = txtAge.Text }).ToList();
                lstData.DataSource = list;
            }
        }
    }
}
