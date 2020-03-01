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
        public SqlConnection con = new SqlConnection("Data source = 303-9\\MSSQLSERVERRR; Initial Catalog = input;" +
            " Integrated Security = true;");
        DataTable DataTable = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            con.Open();
            SqlCommand command = new SqlCommand("select [Название_ЖК], [Статус_строительства_ЖК]," +
                " (select COUNT([ИД_ЖК]) from [dbo].[house2] as h2 where h2.[ИД_ЖК] = h1.[ИД_ЖК])" +
                " AS count, [Город] from [dbo].[home] as h1", con);
            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                dataGridView1.Rows.Add(
                    //reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()
                    reader["Название_ЖК"].ToString(),
                    reader["Статус_строительства_ЖК"].ToString(),
                    reader["count"].ToString(),
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
            button4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button4.Visible = false;
            dataGridView1.EndEdit();
            con.Open();
            SqlCommand command = new SqlCommand($"UPDATE dbo.home SET [Название_ЖК] = '{dataGridView1.CurrentRow.Cells[0].Value.ToString()}', [Статус_строительства_ЖК] = '{dataGridView1.CurrentRow.Cells[1].Value.ToString()}', [Город] = '{dataGridView1.CurrentRow.Cells[3].Value.ToString()}' WHERE [ИД_ЖК] = '{dataGridView1.CurrentRow.Index+1}'", con);
            command.ExecuteNonQuery();
            con.Close();
            dataGridView1.Update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            int index = dataGridView1.CurrentRow.Index;
            string data = dataGridView1[0, index].Value.ToString();
            SqlCommand delete = new SqlCommand($"DELETE FROM dbo.home WHERE [Название_ЖК] = '{data}'", con);
            delete.ExecuteNonQuery();
            con.Close();
            Form1_Load(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 fm3 = new Form3();
            fm3.Show();
            this.Hide();
        }
    }
}
