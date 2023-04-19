using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Npgsql;

namespace MAS_Account
{
    public partial class history : Form
    {
        private DataTable dt = new DataTable();
        public history()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void history_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd-MM-yyyy";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                try
                {
                    dt = new DataTable();
                    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                        "Port=5432;Username=postgres;Password=erika9698;");
                    NpgsqlConnection conn = new NpgsqlConnection(connstring);
                    conn.Open();
                    string sql = "SELECT * FROM stock_report WHERE  date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.Message);
                }
            }

            if (radioButton2.Checked)
            {
                try
                {
                    dt = new DataTable();
                    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                        "Port=5432;Username=postgres;Password=erika9698;");
                    NpgsqlConnection conn = new NpgsqlConnection(connstring);
                    conn.Open();
                    string sql = "SELECT * FROM stock_report WHERE kode = '" + this.textBox1.Text + "' AND  date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dt = new DataTable();
                string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                    "Port=5432;Username=postgres;Password=erika9698;");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "SELECT kode, nama, outgoing, date FROM stock_report WHERE  date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "'";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                    ws.Cells["A1"].LoadFromDataTable(dt, true);

                    ws.Cells["A1:O2000000"].AutoFitColumns();
                    ws.Cells["A1:O2000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    // ws.Column(1).Width = 37;
                    //ws.Column(4).Width = 12;
                    //pck.SaveAs(new FileInfo(@"d:\test2.xlsx" ));
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = @"d:\Report";
                    saveFileDialog1.Title = "Save Excel sheet";
                    saveFileDialog1.Filter = "Excel files|*.xlsx|All files|*.*";
                    saveFileDialog1.FileName = "Report__" + dateTimePicker1.Text + " to " + dateTimePicker2.Text + " Report .xlsx";

                    //check if user clicked the save button
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        //Get the FileInfo
                        FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                        //write the file to the disk
                        pck.SaveAs(fi);
                    }
                }
            }
            if (radioButton2.Checked)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                    ws.Cells["A1"].LoadFromDataTable(dt, true);

                    ws.Cells["A1:O2000000"].AutoFitColumns();
                    ws.Cells["A1:O2000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    // ws.Column(1).Width = 37;
                    //ws.Column(4).Width = 12;
                    //pck.SaveAs(new FileInfo(@"d:\test2.xlsx" ));
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = @"d:\Report";
                    saveFileDialog1.Title = "Save Excel sheet";
                    saveFileDialog1.Filter = "Excel files|*.xlsx|All files|*.*";
                    saveFileDialog1.FileName = "Report__" + dateTimePicker1.Text + " to " + dateTimePicker2.Text + " Report FOR " + this.textBox1.Text + ".xlsx";

                    //check if user clicked the save button
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        //Get the FileInfo
                        FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                        //write the file to the disk
                        pck.SaveAs(fi);
                    }
                }
            }
        }
    }
}
