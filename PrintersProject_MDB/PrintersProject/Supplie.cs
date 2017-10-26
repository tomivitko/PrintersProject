using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintersProject
{
    class Supplie
    {
        public int id { get; set; }
        public String name { get; set; }
        public Supplie(int id, String name)
        {
            this.id = id;
            this.name = name;
        }
        public int getId()
        {
            return this.id;
        }
        public void setId(int id)
        {
            this.id = id;
        }
        public String getName()
        {
            return this.name;
        }
        public void setName(String name)
        {
            this.name = name;
        }
        public override string ToString()
        {
            return this.name.ToString();
        }
    }
}
