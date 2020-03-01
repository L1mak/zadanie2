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
        int n = 0;
        int countPage = 0;
        List<DataTable> list = new List<DataTable>();
        public SqlConnection con = new SqlConnection("Data source = LAPTOP-R3TPO26M\\SQLEXPRESS;" +
            " Initial Catalog = input; Integrated Security = true;");
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
            SqlCommand sql = new SqlCommand("SELECT [Название_ЖК], [Улица],[Номер_дома],[Номер_квартиры]," +
                "[Площадь],[Количество_комнат],[Подъезд],[Этаж],[Статус_продажи] " +
                "FROM [dbo].[apartaments], [dbo].[house2], [dbo].[house1] " +
                "WHERE house1.ИД_ЖК = house2.ИД_ЖК AND apartaments.ИД_Дома = house2.ИД_ДОМА", con);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);
            DataSet data = new DataSet();
            adapter.Fill(data);
            countPage = Convert.ToInt32(Math.Ceiling(data.Tables[0].Rows.Count / 20.0));
            for (int i = 0; i < countPage; i++)
            {
                list.Add(new DataTable());
                list[i].Columns.Add("Название ЖК");
                list[i].Columns.Add("Адрес");
                list[i].Columns.Add("Площадь квартиры");
                list[i].Columns.Add("Количество комнат");
                list[i].Columns.Add("Подъезд");
                list[i].Columns.Add("Этаж");
                list[i].Columns.Add("Статус");
            }
            int w = 0, j = 0;
            DataTable toble;

            foreach (DataRow row in data.Tables[0].Rows)
            {
                if (j > 20)
                {
                    w++; j = 0; 
                }
                
                string s = "";
                DataRow Row = list[w].NewRow();
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {

                    switch (i)
                    {
                        case 0:
                            {
                                Row[0] = row[i];
                                break;
                            }
                        case 1:
                            {
                                s = s + "Ул." + row[i] + " ";
                                break;
                            }
                        case 2:
                            {
                                s = s + "Д." + row[i] + " ";
                                break;
                            }
                        case 3:
                            {
                                s = s + "Кв." + row[i] + " ";
                                Row[1] = s;
                                break;
                            }
                        case 8:
                            {
                                Row[i - 2] = row[i].ToString() == "sold" ? "Продана" : "Продается";
                                break;
                            }
                        default:
                            {
                                Row[i - 2] = row[i];
                                break;
                            }
                    }
                }
                //MessageBox.Show(Row.ToString());
                list[w].Rows.Add();
                for(int l=0; l<7; l++)
                list[w].Rows[j][l] = Row[l];
                j++;
            }
            dataGridView1.DataSource = list[0];
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = list[++n];

            }
            catch (Exception)
            {
                --n;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = list[--n];
            }
            catch (Exception)
            {
                ++n;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            
                dataGridView1.DataSource = list[list.Count-1];
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = list[0];
        }
    }
}
