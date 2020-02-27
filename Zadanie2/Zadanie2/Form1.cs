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
        public SqlConnection con = new SqlConnection("Data source = LAPTOP-R3TPO26M\\SQLEXPRESS; Initial Catalog = input; Integrated Security = true;");
        DataTable DataTable = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand("SELECT *, (SELECT COUNT([ИД_ЖК]) FROM dbo.house2 AS [home] WHERE home.ИД_ЖК = home.ИД_ЖК) FROM dbo.home", con);
            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                dataGridView1.Rows.Add(
                    //reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()
                    reader[1].ToString(),
                    reader[3].ToString(),
                    reader[6].ToString(),
                    reader[2].ToString()
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
            //MessageBox.Show(dataGridView1.CurrentRow.Index.ToString());
            con.Open();
            SqlCommand command = new SqlCommand($"UPDATE dbo.home SET [Название_ЖК] = '{dataGridView1.CurrentRow.Cells[0].Value.ToString()}', [Статус_строительства_ЖК] = '{dataGridView1.CurrentRow.Cells[1].Value.ToString()}', [Город] = '{dataGridView1.CurrentRow.Cells[3].Value.ToString()}' WHERE [ИД_ЖК] = '{dataGridView1.CurrentRow.Index+1}'", con);
            command.ExecuteNonQuery();
            //SqlCommand commanda = new SqlCommand("SELECT * FROM '"+ dataGridView1. +"'", con);
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand delete = new SqlCommand($"DELETE FROM dbo.home WHERE [ИД_ЖК] = '{dataGridView1.CurrentRow.Index + 1}'", con);
            delete.ExecuteNonQuery();
            con.Close();
        }
    }
}
