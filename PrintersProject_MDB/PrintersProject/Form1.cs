using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using excelRef = Microsoft.Office.Interop.Excel;
using System.Threading;

namespace PrintersProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            fillTable();
        }

        public void fillTable()
        {
            List<Printer> printerList = Database.getAllPrinters();
            dataGridView1.DataSource = printerList;
            dataGridView1.AutoResizeColumns();
        }

        private void chechItemCountThread()
        {

        }

        private void printerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form newPrinter = new NewPrinterForm(dataGridView1);
            newPrinter.Show();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > 0)
                dataGridView1.Rows[e.RowIndex].Selected = true;
            Printer printer = (Printer)dataGridView1.CurrentRow.DataBoundItem;
            this.textBoxMark.Text = printer.getPrinterMark();
            this.textBoxType.Text = printer.getPrinterType();
            this.textBoxManufacturer.Text = printer.getName();
            this.textBoxModel.Text = printer.getModel();
            List<Department> departmentList = Database.getAllDepartments();
            this.comboBoxDepartment.DataSource = departmentList;
            comboBoxDepartment.SelectedIndex = comboBoxDepartment.FindStringExact(printer.getDepartment());

            List<Supplie> suppliesList = Database.getPrinterSupplies(printer.id);
            this.listBoxSupplies.ValueMember = "name";
            this.listBoxSupplies.DataSource = suppliesList;

        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            int i;
            bool bNum = int.TryParse(textBoxQuantity.Text, out i);

            if (textBoxQuantity.Text != "" && bNum)
            {
                Printer printer = (Printer)dataGridView1.CurrentRow.DataBoundItem;
                Supplie supplie = (Supplie)this.listBoxSupplies.SelectedItem;
                int ok = Database.addSupplieToPrinter(supplie, Int32.Parse(textBoxQuantity.Text), printer);
                if (ok == 1)
                    MessageBox.Show("Materijal uspješno dodan.");
                else
                    MessageBox.Show("Greška pri dodavanju materijala.");
            }
            else
                MessageBox.Show("U polje mora biti upisan broj.");

            this.textBoxQuantity.Text = "";
        }

        private void OtherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form newOther = new NewOtherForm();
            newOther.Show();
        }

        private void dodajMaterijalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form newAddSupplieForm = new AddSupplieForm();
            newAddSupplieForm.Show();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (this.textBoxMark.Text != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Jeste li sigurni?", "Izbrisati?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int addedRecords = 0;
                    Printer printer = (Printer)dataGridView1.CurrentRow.DataBoundItem;
                    addedRecords = Database.deletePrinter(printer.getId());
                    if (addedRecords == 1)
                    {
                        MessageBox.Show("Printer izbrisan!");
                        fillTable();
                    }
                    else
                        MessageBox.Show("Greška pri brisanju!");
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
            else
                MessageBox.Show("Niste odabrali printer");
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (this.textBoxMark.Text != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Jeste li sigurni?", "Promjeniti?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int addedRecords = 0;
                    Printer printer = (Printer)dataGridView1.CurrentRow.DataBoundItem;
                    Department department = (Department)comboBoxDepartment.SelectedItem;
                    addedRecords = Database.addPrinterToDepartment(printer.getId(), department.id);
                    if (addedRecords == 1)
                    {
                        MessageBox.Show("Printer dodjeljen!");
                        fillTable();
                    }
                    else
                        MessageBox.Show("Greška pri dodjeli!");
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
            else
                MessageBox.Show("Niste odabrali printer!");
        }

        private void stanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form newWarehouseStatus = new Warehouse();
            newWarehouseStatus.Show();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Printer printer = (Printer)dataGridView1.CurrentRow.DataBoundItem;

            DataTable result = Database.getPrinterSuppliesConsumption(printer.getId(), 
                dateTimePickerStart.Value.ToString("yyyy-MM-dd"),
                dateTimePickerEnd.Value.AddDays(1).ToString("yyyy-MM-dd"));

            excelRef.Application excelApp;
            excelRef.Workbook excelBook;
            excelRef.Worksheet excelSheet;
            object misValue = System.Reflection.Missing.Value;

            excelApp = new excelRef.Application();
            excelApp.DisplayAlerts = false;
            excelApp.Visible = true;
            excelBook = excelApp.Workbooks.Add(misValue);
            excelSheet = (excelRef.Worksheet)excelBook.Worksheets.get_Item(1);

            excelSheet.Cells[1, 1] = "Potrošnja printera: " + printer.getPrinterMark();

            excelSheet.Cells[3, 1] = "Oznaka printera";
            excelSheet.Cells[3, 2] = "Model";
            excelSheet.Cells[3, 3] = "Materijal";
            excelSheet.Cells[3, 4] = "Količina";

            int i = 4; 
            foreach (DataRow row in result.Rows)
            {
                excelSheet.Cells[i, 1] = row[0].ToString();
                excelSheet.Cells[i, 2] = row[1].ToString();
                excelSheet.Cells[i, 3] = row[2].ToString();
                excelSheet.Cells[i, 4] = row[3].ToString();
                i++;
            }

        }

        private void potrosnjaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataTable result = Database.getAllPrinterSuppliesConsumption();

            excelRef.Application excelApp;
            excelRef.Workbook excelBook;
            excelRef.Worksheet excelSheet;
            object misValue = System.Reflection.Missing.Value;

            excelApp = new excelRef.Application();
            excelApp.DisplayAlerts = false;
            excelApp.Visible = true;
            excelBook = excelApp.Workbooks.Add(misValue);
            excelSheet = (excelRef.Worksheet)excelBook.Worksheets.get_Item(1);

            excelSheet.Cells[1, 1] = "Potrošnja svih printera";

            excelSheet.Cells[3, 1] = "Oznaka printera";
            excelSheet.Cells[3, 2] = "Model";
            excelSheet.Cells[3, 3] = "Materijal";
            excelSheet.Cells[3, 4] = "Količina";

            int i = 4;
            foreach (DataRow row in result.Rows)
            {
                excelSheet.Cells[i, 1] = row[0].ToString();
                excelSheet.Cells[i, 2] = row[1].ToString();
                excelSheet.Cells[i, 3] = row[2].ToString();
                excelSheet.Cells[i, 4] = row[3].ToString();
                i++;
            }
        }
    }
}
