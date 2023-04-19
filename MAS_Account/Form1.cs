using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MAS_Account
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input_inventory input = new input_inventory();
            input.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            incoming incoming = new incoming();
            incoming.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            outgoing outgoing = new outgoing();
            outgoing.Show();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            inventory inventory = new inventory();
            inventory.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            history history = new history();
            history.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            updateprice updateprice = new updateprice();
            updateprice.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            report report = new report();
            report.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Cash cash = new Cash();
            cash.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            cashin cashin = new cashin();
            cashin.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            cashout cashout = new cashout();
            cashout.Show();
        }
    }
}
