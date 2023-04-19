using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Npgsql;
using MAS_Account.npgsql;

namespace MAS_Account
{
    public partial class Cash : Form
    {
        private DataTable dt = new DataTable();
        string money_in;
        string cash_in;
        string cash_out;
        string money_out;
        public Cash()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                try
                {
                    dt = new DataTable();
                    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                        "Port=5432;Username=postgres;Password=erika9698;");
                    NpgsqlConnection conn = new NpgsqlConnection(connstring);
                    conn.Open();
                    string sql = "SELECT date,keterangan,cash_in FROM cash_report WHERE con = 'IN' AND date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.Message);
                }
            }

            if (radioButton5.Checked == true)
            {
                try
                {
                    dt = new DataTable();
                    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                        "Port=5432;Username=postgres;Password=erika9698;");
                    NpgsqlConnection conn = new NpgsqlConnection(connstring);
                    conn.Open();
                    string sql = "SELECT date,keterangan,cash_out FROM cash_report WHERE con = 'OUT' AND date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.Message);
                }
            }
            if (radioButton1.Checked == true && comboBox1.Text != null)
            {
                try
                {
                    dt = new DataTable();
                    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                        "Port=5432;Username=postgres;Password=erika9698;");
                    NpgsqlConnection conn = new NpgsqlConnection(connstring);
                    conn.Open();
                    string sql = "SELECT date,keterangan,cash_in FROM cash_report WHERE keterangan = '"+this.comboBox1.Text+ "' AND date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC";
                    string sql2 = "SELECT date,keterangan,cash_in FROM cash_report WHERE keterangan = '"+this.comboBox1.Text+ "' AND date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.Message);
                }
            }

            if (radioButton2.Checked == true && comboBox1.Text != null)
            {
                try
                {
                    dt = new DataTable();
                    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
                        "Port=5432;Username=postgres;Password=erika9698;");
                    NpgsqlConnection conn = new NpgsqlConnection(connstring);
                    conn.Open();
                    string sql = "SELECT date,keterangan, cash_out FROM cash_report WHERE keterangan = '" + this.comboBox1.Text + "'  AND date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC";
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
                    string sql = "SELECT date,keterangan,cash_in,cash_out FROM cash_report WHERE date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC";
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
                    ws.Cells["A1:D2000000"].AutoFitColumns();
                    ws.Cells["A1:D2000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F3"].Value = "MONEY IN    :";
                    //ws.Cells["G3"].Value = money_in;
                    ws.Cells["G3"].Formula = "=SUM(C2:C2000)";
                    ws.Cells["F3"].AutoFitColumns();
                    ws.Cells["F3:F100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F4"].Value = "MONEY OUT   :";
                    ws.Cells["G4"].Formula = "=SUM(D2:D2000)";
                    //ws.Cells["G3:G4"].Style.Numberformat.Format = "-Rp* #.##0,00-;-Rp* #.##0,00_-;-Rp* ''''-''''??-;-@-";
                    ws.Cells["C2:G2000"].Style.Numberformat.Format = "_-Rp* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";
                    // ws.Column(1).Width = 37;
                    //ws.Column(4).Width = 12;
                    //pck.SaveAs(new FileInfo(@"d:\test2.xlsx" ));
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = @"d:\Report\CashReport";
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
                    ws.Cells["A1:D2000000"].AutoFitColumns();
                    ws.Cells["A1:D2000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F3"].Value = "MONEY IN    :";
                    //ws.Cells["G3"].Value = money_in;
                    ws.Cells["G3"].Formula = "=SUM(C2:C2000)";
                    ws.Cells["F3"].AutoFitColumns();
                    ws.Cells["F3:F100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F4"].Value = "MONEY OUT   :";
                    ws.Cells["G4"].Formula = "=SUM(D2:D2000)";
                    ws.Cells["C2:G2000"].Style.Numberformat.Format = "_-Rp* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";
                    // ws.Column(1).Width = 37;
                    //ws.Column(4).Width = 12;
                    //pck.SaveAs(new FileInfo(@"d:\test2.xlsx" ));
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = @"d:\Report\CashReport";
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
                    ws.Cells["A1:D2000000"].AutoFitColumns();
                    ws.Cells["A1:D2000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F3"].Value = "MONEY IN    :";
                    //ws.Cells["G3"].Value = money_in;
                    ws.Cells["G4"].Formula = "=SUM(C2:C2000)";
                    ws.Cells["F3"].AutoFitColumns();
                    ws.Cells["F3:F100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F4"].Value = "MONEY OUT   :";
                    ws.Cells["G3"].Formula = "=SUM(D2:D2000)";
                    ws.Cells["C2:G2000"].Style.Numberformat.Format = "_-Rp* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";
                    // ws.Column(1).Width = 37;
                    //ws.Column(4).Width = 12;
                    //pck.SaveAs(new FileInfo(@"d:\test2.xlsx" ));
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = @"d:\Report\CashReport";
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

            if( radioButton4.Checked == true)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                    ws.Cells["A1:D2000000"].AutoFitColumns();
                    ws.Cells["A1:D2000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F3"].Value = "MONEY IN    :";
                    //ws.Cells["G3"].Value = money_in;
                    ws.Cells["G3"].Formula = "=SUM(C2:C2000)";
                    ws.Cells["F3"].AutoFitColumns();
                    ws.Cells["F3:F100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F4"].Value = "MONEY OUT   :";
                    ws.Cells["G4"].Formula = "=SUM(D2:D2000)";
                    ws.Cells["C2:G2000"].Style.Numberformat.Format = "_-Rp* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";
                    // ws.Column(1).Width = 37;
                    //ws.Column(4).Width = 12;
                    //pck.SaveAs(new FileInfo(@"d:\test2.xlsx" ));
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = @"d:\Report\CashReport";
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

            if (radioButton5.Checked == true)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                    ws.Cells["A1:D2000000"].AutoFitColumns();
                    ws.Cells["A1:D2000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F3"].Value = "MONEY IN    :";
                    //ws.Cells["G3"].Value = money_in;
                    ws.Cells["G4"].Formula = "=SUM(C2:C2000)";
                    ws.Cells["F3"].AutoFitColumns();
                    ws.Cells["F3:F100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells["F4"].Value = "MONEY OUT   :";
                    ws.Cells["G3"].Formula = "=SUM(D2:D2000)";
                    ws.Cells["C2:G2000"].Style.Numberformat.Format = "_-Rp* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";
                    // ws.Column(1).Width = 37;
                    //ws.Column(4).Width = 12;
                    //pck.SaveAs(new FileInfo(@"d:\test2.xlsx" ));
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = @"d:\Report\CashReport";
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

        private void Cash_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
            //        "Port=5432;Username=postgres;Password=erika9698;");
            //    NpgsqlConnection conn = new NpgsqlConnection(connstring);
            //    conn.Open();
            //    string sql = "INSERT INTO public.total (date , money_in, money_out) Values (now(), (SELECT SUM(cash_in) FROM public.cash_report date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC), (SELECT SUM(cash_out) FROM public.cash_report date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ORDER BY date ASC))";
            //    Npgsql_Helper.Exec(sql);

            //}
            //catch (Exception msg)
            //{
            //    MessageBox.Show("ERROR");
            //}
            //timer1.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
            //        "Port=5432;Username=postgres;Password=erika9698;");
            //    NpgsqlConnection conn = new NpgsqlConnection(connstring);
            //    conn.Open();
            //    string sql = ("SELECT * FROM total ORDER BY date DESC LIMIT 1");
            //    NpgsqlCommand da = new NpgsqlCommand(sql, conn);
            //    NpgsqlDataReader dr = da.ExecuteReader();

            //    while (dr.Read())
            //    {
            //        money_in = dr["money_in"].ToString();
            //        money_out = dr["money_out"].ToString();
            //    }
            //    cash_in = string.Format("{0:C}", Convert.ToDecimal(money_in));
            //    cash_out = string.Format("{0:C}", Convert.ToDecimal(money_out));
            //    label8.Text = cash_in;
            //    label9.Text = cash_out;
            //    timer1.Enabled = false;
            //}
            //catch (Exception msg)
            //{
            //    MessageBox.Show(msg.Message);
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    string connstring = String.Format("Server=localhost;Database=MAS_Inventory;" +
            //        "Port=5432;Username=postgres;Password=erika9698;");
            //    NpgsqlConnection conn = new NpgsqlConnection(connstring);
            //    conn.Open();
            //    string sql = "INSERT INTO public.total (date , money_in, money_out) Values (now(), (SELECT SUM(cash_in) FROM public.cash_report where date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "'), (SELECT SUM(cash_out) FROM public.cash_report where date BETWEEN '" + this.dateTimePicker1.Text + "'AND '" + this.dateTimePicker2.Text + "' ))";
            //    Npgsql_Helper.Exec(sql);

            //}
            //catch (Exception msg)
            //{
            //    MessageBox.Show("ERROR");
            //}
        }
    }
}
