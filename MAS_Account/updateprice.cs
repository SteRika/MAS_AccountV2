using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using MAS_Account.npgsql;

namespace MAS_Account
{
    public partial class updateprice : Form
    {
        public updateprice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                    "Port=5432;Username=postgres;Password=erika9698;");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "SELECT nama,beli FROM public.money where kode = '" + this.textBox1.Text + "';";
                NpgsqlCommand da = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = da.ExecuteReader();
                while (dr.Read())
                {
                    textBox2.Text = dr["nama"].ToString();
                    textBox5.Text = dr["beli"].ToString();
                }
                //textBox5.Text = Convert.ToString(Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox4.Text));

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float up = (Convert.ToInt32(textBox5.Text) * Convert.ToInt32(textBox6.Text)) / 100;
            float sell = Convert.ToInt32(textBox5.Text) + up;
            textBox4.Text = sell.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                    "Port=5432;Username=postgres;Password=erika9698;");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "UPDATE public.money SET jual='" + this.textBox4.Text + "' WHERE kode='" + this.textBox1.Text + "';"; 
                Npgsql_Helper.Exec(sql);
                MessageBox.Show("UPDATED");


            }
            catch (Exception msg)
            {
                MessageBox.Show("ERROR");
            }
        }
    }
}
