using System;


namespace PrintersProject
{
    class WarehouseItem
    {
        public Supplie supplie { get; set; }
        public int quantity { get; set; }

        public WarehouseItem(int id, String name, int quantity) {
            this.supplie = new Supplie(id, name);
            this.quantity = quantity;
        }
    }
}
