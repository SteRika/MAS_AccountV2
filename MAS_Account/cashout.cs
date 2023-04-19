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
    public partial class cashout : Form
    {
        string a = "OUT";
        public cashout()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
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
                string sql = "INSERT INTO public.cash_report(date, keterangan, cash_out, con) VALUES ('" + this.dateTimePicker1.Text + "', '" + this.comboBox1.Text + "', '" + this.textBox1.Text + "', 'OUT'); ";
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
