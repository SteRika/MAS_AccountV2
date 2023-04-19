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
using MAS_Account.npgsql;

namespace MAS_Account
{
    public partial class report : Form
    {
        private DataTable dt = new DataTable();

        public report()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                try
                {
                    dt = new DataTable();
                    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                        "Port=5432;Username=postgres;Password=erika9698;");
                    NpgsqlConnection conn = new NpgsqlConnection(connstring);
                    conn.Open();
                    string sql = "SELECT date,keterangan, masuk,keluar,nama FROM kas WHERE keterangan LIKE '%INC%' AND date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.Message);
                }
            }

            if(radioButton2.Checked == true)
            {
                try
                {
                    dt = new DataTable();
                    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                        "Port=5432;Username=postgres;Password=erika9698;");
                    NpgsqlConnection conn = new NpgsqlConnection(connstring);
                    conn.Open();
                    string sql = "SELECT date,keterangan, masuk,keluar,nama FROM kas WHERE keterangan LIKE '%OUT%' AND date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.Message);
                }

            }

            if (radioButton3.Checked == true)
            {
                try
                {
                    dt = new DataTable();
                    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                        "Port=5432;Username=postgres;Password=erika9698;");
                    NpgsqlConnection conn = new NpgsqlConnection(connstring);
                    conn.Open();
                    string sql = "SELECT date,keterangan,masuk,keluar,nama  FROM kas WHERE date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC";
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

            if(radioButton3.Checked == true)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                    ws.Cells["A1:O2000000"].AutoFitColumns();
                    ws.Cells["A1:O2000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["F3"].Value = "PENJUALAN:";
                    //ws.Cells["G3"].Value = money_in;
                    ws.Cells["G3"].Formula = "=SUM(C2:C2000)";
                    ws.Cells["F3"].AutoFitColumns();
                    ws.Cells["F3:F100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F4"].Value = "PEMBELIAN   :";
                    ws.Cells["G4"].Formula = "=SUM(D2:D2000)";
                    ws.Cells["C2:G2000"].Style.Numberformat.Format = "_-Rp* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";
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

            if(radioButton1.Checked == true)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                    ws.Cells["A1:O2000000"].AutoFitColumns();
                    ws.Cells["A1:O2000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["F3"].Value = "PENJUALAN:";
                    //ws.Cells["G3"].Value = money_in;
                    ws.Cells["G3"].Formula = "=SUM(C2:C2000)";
                    ws.Cells["F3"].AutoFitColumns();
                    ws.Cells["F3:F100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F4"].Value = "PEMBELIAN   :";
                    ws.Cells["G4"].Formula = "=SUM(D2:D2000)";
                    ws.Cells["C2:G2000"].Style.Numberformat.Format = "_-Rp* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";
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

            if(radioButton2.Checked == true)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                    ws.Cells["A1:O2000000"].AutoFitColumns();
                    ws.Cells["A1:O2000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["F3"].Value = "PENJUALAN:";
                    //ws.Cells["G3"].Value = money_in;
                    ws.Cells["G3"].Formula = "=SUM(C2:C2000)";
                    ws.Cells["F3"].AutoFitColumns();
                    ws.Cells["F3:F100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F4"].Value = "PEMBELIAN   :";
                    ws.Cells["G4"].Formula = "=SUM(D2:D2000)";
                    ws.Cells["C2:G2000"].Style.Numberformat.Format = "_-Rp* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";
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
            
        }

        private void report_Load(object sender, EventArgs e)
        {

        }

    }
}
