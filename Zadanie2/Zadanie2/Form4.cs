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

namespace Zadanie2
{
    public partial class Form4 : Form
    {

        public SqlConnection con = new SqlConnection("Data source = 303-9\\MSSQLSERVERRR; Initial Catalog = input; Integrated Security = true;");
        public Form4()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 fm3 = new Form3();
            fm3.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand sql = new SqlCommand("SELECT * FROM dbo.apartaments",con);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
            }
            con.Close();
        }

    }
}
