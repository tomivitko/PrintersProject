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
    public partial class AddSupplieForm : Form
    {
        public AddSupplieForm()
        {
            InitializeComponent();
            this.comboBoxSupplie.DataSource = Database.getSupplies();
            this.comboBoxSupplie.DisplayMember = "name";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (this.textBoxQuantity.Text == String.Empty)
                MessageBox.Show("Niste upisali količinu!");
            else
            {
                int id_supplie = ((Supplie)this.comboBoxSupplie.SelectedItem).getId();
                int quantity = Int32.Parse(this.textBoxQuantity.Text);
                int addedRecords = Database.addSupplieToWarehouse(id_supplie, quantity);

                if (addedRecords == 1)
                {
                    MessageBox.Show("Materijal uspješno upisan!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Greška pri upisu materijala!");
                }
            }
            

        }
    }
}
