using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintersProject
{
    class Department
    {
        public int id { get; set; }
        public string name { get; set; }

        public Department (int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public override string ToString()
        {
            return this.name;
        }
        public int getDepartment()
        {
            return id;
        }
    }
}
