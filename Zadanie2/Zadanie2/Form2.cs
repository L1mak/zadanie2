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
    public partial class Form2 : Form
    {
        public SqlConnection con = new SqlConnection("Data source = 303-9\\MSSQLSERVERRR; Initial Catalog = input; Integrated Security = true;");
        public Form2()
        {
            InitializeComponent();

        }

        private void City_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var source = new AutoCompleteStringCollection();
            source.AddRange(new string[]
                {
                    "Москва","Питер","Пенза","Уфа"
                });
            City.AutoCompleteCustomSource = source;
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand($"INSERT INTO dbo.home([Название_ЖК],[Город],[Статус_строительства_ЖК],[Добавочная_стоимость_ЖК],[Затраты_на_строительство_ЖК]) VALUES('{Name.Text}','{City.Text}','{comboBox1.Text}','{Koeff.Text}','{Traty.Text}') ", con);
            command.ExecuteNonQuery();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 fm1 = new Form1();
            fm1.Show();
            this.Hide();
        }
    }
}
