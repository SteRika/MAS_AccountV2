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
    public partial class input_inventory : Form
    {
        public input_inventory()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                    "Port=5432;Username=postgres;Password=erika9698;");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "INSERT INTO public.main(kode, nama, date) VALUES ('" + this.textBox1.Text + "', '" + this.textBox2.Text+"','"+ this.dateTimePicker1.Text +"');";
                Npgsql_Helper.Exec(sql);
                string sql3 = "INSERT INTO public.money(kode,nama, beli) VALUES ('" + this.textBox1.Text + "','" + this.textBox2.Text + "','" + this.textBox4.Text + "');";
                Npgsql_Helper.Exec(sql3);
                string sql2 = "INSERT INTO public.stock_card(kode, nama, stocka) VALUES ('" + this.textBox1.Text + "', '" + this.textBox2.Text + "','" + this.textBox3.Text + "');";
                Npgsql_Helper.Exec(sql2);
                MessageBox.Show("PRODUK TELAH DITAMBAHKAN");


            }
            catch (Exception msg)
            {
                MessageBox.Show("ERROR");
            }
        }

        private void input_inventory_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
        }


    }
}
