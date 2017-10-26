using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintersProject
{
    public partial class NewPrinterForm : Form
    {
        DataTable dtManufacturers;
        DataTable dtTypes;
        List<Supplie> allSuppliesList = new List<Supplie>();
        List<Supplie> addedSuppliesList = new List<Supplie>();
        DataGridView dataGridView1;

        public NewPrinterForm(DataGridView dgv)
        {
            InitializeComponent();
            dataGridView1 = dgv;
        }

        private void NewPrinterForm_Load(object sender, EventArgs e)
        {
            fillManufacturersCombo();
            fillTypeCombo();
            fillSuppliesList();
        }

        private void fillSuppliesList()
        {
            allSuppliesList = Database.getSupplies();
            this.listBoxAllSupplies.ValueMember = "name";
            this.listBoxAllSupplies.DataSource = allSuppliesList;
        }
        private void fillManufacturersCombo()
        {
            dtManufacturers = Database.getManufacturers();
            foreach (DataRow da in dtManufacturers.Rows)
                comboBoxManufacturer.Items.Add(da[1].ToString());
        }

        private void fillTypeCombo()
        {
            dtTypes = Database.getPrinterTypes();
            foreach (DataRow da in dtTypes.Rows)
                comboBoxType.Items.Add(da[1].ToString());
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            String manufacturer = comboBoxManufacturer.Text.ToString();
            String name = textBoxName.Text.ToString();
            String type = comboBoxType.Text.ToString();
            String mark = textBoxMark.Text.ToString();
            if (manufacturer == string.Empty || name == string.Empty
                || type == string.Empty || mark == string.Empty || addedSuppliesList.Count() == 0)
            {
                MessageBox.Show("Niste unijeli sve podatke!");
            }
            else
            {
                int manufacturer_id = 0, type_id = 0, addedRecords = 0;
                foreach(DataRow da in dtManufacturers.Rows)
                { 
                    if (manufacturer == da[1].ToString())
                    {
                        int.TryParse(da[0].ToString(), out manufacturer_id); 
                    }
                        
                }
                foreach (DataRow da in dtTypes.Rows)
                {
                    if (type == da[1].ToString())
                    {
                        int.TryParse(da[0].ToString(), out type_id);
                    }

                }
                addedRecords = Database.addNewPrinter(manufacturer_id, name, type_id, mark, addedSuppliesList);
                if(addedRecords == 1)
                {
                    MessageBox.Show("Printer uspješno upisan!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Greška pri upisu printera!");
                }
            }

            List<Printer> printerList = Database.getAllPrinters();
            dataGridView1.DataSource = printerList;
            dataGridView1.AutoResizeColumns();
        }


        private void listBoxAllSupplies_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBoxAllSupplies.Items.Count == 0)
                return;

            int index = listBoxAllSupplies.IndexFromPoint(e.X, e.Y);
            string s = listBoxAllSupplies.Items[index].ToString();
            DragDropEffects dde1 = DoDragDrop(index.ToString(), DragDropEffects.All);

        }

        private void listBoxAddedSupplies_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void listBoxAddedSupplies_DragDrop(object sender, DragEventArgs e)
        {
            
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string str = (string)e.Data.GetData(DataFormats.StringFormat);
                if (!addedSuppliesList.Any(supplie => supplie.id == (allSuppliesList[Int32.Parse(str)]).id))
                {
                    addedSuppliesList.Add(allSuppliesList[Int32.Parse(str)]);
                    this.listBoxAddedSupplies.DataSource = null;
                    this.listBoxAddedSupplies.DataSource = addedSuppliesList;
                }
            }
        }

        private void listBoxAddedSupplies_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBoxAddedSupplies.Items.Count == 0)
                return;

            int index = listBoxAddedSupplies.IndexFromPoint(e.X, e.Y);
            string s = listBoxAddedSupplies.Items[index].ToString();
            DragDropEffects dde1 = DoDragDrop(s,
                DragDropEffects.All);

            if (dde1 == DragDropEffects.All)
            {
                addedSuppliesList.RemoveAt(listBoxAddedSupplies.IndexFromPoint(e.X, e.Y));
                this.listBoxAddedSupplies.DataSource = null;
                this.listBoxAddedSupplies.DataSource = addedSuppliesList;
            }
        }

        private void listBoxAllSupplies_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void listBoxAllSupplies_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    }
}
