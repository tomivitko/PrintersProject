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
    public partial class NewOtherForm : Form
    {
        public NewOtherForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            int addedRecords = 0;
            if(this.textBoxName.Text == string.Empty)
            {
                MessageBox.Show("Upišite naziv!");
            }
            else
            {
                if (radioButtonDepartment.Checked)
                {
                    addedRecords = Database.addNewOther(this.textBoxName.Text, 1);
                }
                if (radioButtonManufacturer.Checked)
                    addedRecords = Database.addNewOther(this.textBoxName.Text, 2);
                if (radioButtonSupplie.Checked)
                    addedRecords = Database.addNewOther(this.textBoxName.Text, 3);

                if (addedRecords == 1)
                {
                    MessageBox.Show("Uspješno upisano!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Greška pri upisu podataka!");
                }
            }
        }
    }
}
