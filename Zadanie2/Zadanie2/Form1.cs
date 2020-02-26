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
    

    public partial class Form1 : Form
    {
        public SqlConnection con = new SqlConnection("Data source = 303-9\\MSSQLSERVERRR; Initial Catalog = input; Integrated Security = true;");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand("SELECT [Название_ЖК],[Статус_строительства_ЖК],[Город] FROM dbo.houses", con);
            SqlDataReader reader = command.ExecuteReader();
            int n = 0;
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                
            }

            reader.Close();
            con.Close();
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
