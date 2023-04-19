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
using MAS_Account.npgsql;

namespace MAS_Account
{

    public partial class incoming : Form
    {
        public incoming()
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
                    textBox4.Text = dr["stocka"].ToString();
                }
                textBox5.Text = Convert.ToString(Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox4.Text));

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }


        private void incoming_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int harga = Convert.ToInt32(textBox7.Text) * Convert.ToInt32(textBox3.Text);
            try
            {
                string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                    "Port=5432;Username=postgres;Password=erika9698;");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "INSERT INTO public.stock_report(kode, nama, incoming, date) VALUES ('"+this.textBox1.Text+ "', '" + this.textBox2.Text + "', '" + this.textBox3.Text + "', '"+this.dateTimePicker1.Text+"'); ";
                string sql2 = "UPDATE public.stock_card SET stocka='"+ this.textBox5.Text +"' WHERE kode='"+this.textBox1.Text+"';";
                string sql3 = "UPDATE public.money SET beli='" + this.textBox8.Text + "' WHERE kode='" + this.textBox1.Text + "';";
                string sql4 = "INSERT INTO public.kas(keluar, keterangan , date, nama) VALUES('" + harga + "'  , '" + this.label6.Text +"','"+this.dateTimePicker1.Text+ "','" + this.textBox9.Text + "');";
                Npgsql_Helper.Exec(sql);
                Npgsql_Helper.Exec(sql2);
                Npgsql_Helper.Exec(sql3);
                Npgsql_Helper.Exec(sql4);
                MessageBox.Show("UPDATED");


            }
            catch (Exception msg)
            {
                MessageBox.Show("ERROR");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                    "Port=5432;Username=postgres;Password=erika9698;");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "SELECT beli FROM public.money where kode = '" + this.textBox1.Text + "';";
                NpgsqlCommand da = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = da.ExecuteReader();
                while (dr.Read())
                {
                    textBox6.Text = dr["beli"].ToString();
                }
                textBox5.Text = Convert.ToString(Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox4.Text));

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int totalAwal = Convert.ToInt32(textBox4.Text) * Convert.ToInt32(textBox6.Text);
            int totalSekarang= Convert.ToInt32(textBox3.Text) * Convert.ToInt32(textBox7.Text);
            int totalBaru = (totalAwal + totalSekarang);
            int hargaBaru = totalBaru / Convert.ToInt32(textBox5.Text);

            textBox8.Text = hargaBaru.ToString();
}
    }
}
