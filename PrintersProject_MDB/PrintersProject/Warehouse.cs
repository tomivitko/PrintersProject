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
    public partial class Warehouse : Form
    {
        public Warehouse()
        {
            InitializeComponent();
            fillDataGridView();
        }

        private void fillDataGridView (){
            List<WarehouseItem> warehouseItemList = Database.getWarehouseStatus();
            dataGridViewWarehouse.DataSource = warehouseItemList;

        }


    }
}
