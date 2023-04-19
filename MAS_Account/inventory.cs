using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Npgsql;

namespace MAS_Account
{
    public partial class inventory : Form
    {
        public inventory()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                    "Port=5432;Username=postgres;Password=erika9698;");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "SELECT nama,stocka FROM public.stock_card where kode = '" + this.textBox1.Text + "';";
                NpgsqlCommand da = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = da.ExecuteReader();
                while (dr.Read())
                {
                    textBox2.Text = dr["nama"].ToString();
                    textBox3.Text = dr["stocka"].ToString();
                }

            }
            catch (Exception msg)
            {

            }
        }
    }
}
