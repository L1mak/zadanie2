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
        DataTable DataTable = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand("select distinct [Название_ЖК], [Статус_строительства_ЖК], (select COUNT([Название_ЖК]) from [dbo].[houses] as h2 where h1.[Название_ЖК] = h2.[Название_ЖК]) as [Кол-во домов], [Город]from[dbo].[houses] as h1", con);
            SqlDataReader reader = command.ExecuteReader();
            
            int n = 2;
            while (reader.Read())
            {
                dataGridView1.Rows.Add(
                    //reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()
                    reader["Название_ЖК"].ToString(),
                    reader["Статус_строительства_ЖК"].ToString(),
                    reader["Кол-во домов"].ToString(),
                    reader["Город"].ToString()
                    );
                
            }
            reader.Close();
            con.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            dataGridView1.EndEdit();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM '"+ dataGridView1.DataSource +"' ", con);
            adapt.Update(DataTable);
            SqlCommand command = new SqlCommand($"UPDATE dbo.houses SET [Название_ЖК] = {dataGridView1.CurrentRow.Cells[0].Value.ToString()}, [Статус_строительства_ЖК] = {dataGridView1.CurrentRow.Cells[1].Value.ToString()} [Город] = {dataGridView1.CurrentRow.Cells[3].Value.ToString()}");
            //SqlCommand commanda = new SqlCommand("SELECT * FROM '"+ dataGridView1. +"'", con);
            con.Close();
        }
    }
}
