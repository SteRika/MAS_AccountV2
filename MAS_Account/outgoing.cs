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
    public partial class outgoing : Form
    {
        public outgoing()
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

                textBox5.Text = Convert.ToString(Convert.ToInt32(textBox3.Text) - Convert.ToInt32(textBox4.Text));

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }

        }
        

        private void outgoing_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                    "Port=5432;Username=postgres;Password=erika9698;");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "INSERT INTO public.stock_report(kode, nama, outgoing, date) VALUES ('" + this.textBox1.Text + "', '" + this.textBox2.Text + "', '" + this.textBox4.Text + "', '" + this.dateTimePicker1.Text + "'); ";
                string sql2 = "UPDATE public.stock_card SET stocka='" + this.textBox5.Text + "' WHERE kode='" + this.textBox1.Text + "';";
                string sql3 = "INSERT INTO public.kas(masuk, keterangan, date, nama) VALUES('"+this.textBox6.Text+"', '"+this.label6.Text+"', '"+this.dateTimePicker1.Text+ "','" + this.textBox8.Text + "'); ";
                Npgsql_Helper.Exec(sql);
                Npgsql_Helper.Exec(sql2);
                Npgsql_Helper.Exec(sql3);
                MessageBox.Show("UPDATED");


            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                    "Port=5432;Username=postgres;Password=erika9698;");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "SELECT jual FROM public.money where kode = '" + this.textBox1.Text + "';";
                NpgsqlCommand da = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = da.ExecuteReader();
                while (dr.Read())
                {
                    textBox7.Text = dr["jual"].ToString();
                }

                textBox5.Text = Convert.ToString(Convert.ToInt32(textBox3.Text) - Convert.ToInt32(textBox4.Text));

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt32(textBox4.Text);
            int total = qty * Convert.ToInt32(textBox7.Text);

            textBox6.Text = total.ToString();
        }
    }
}
