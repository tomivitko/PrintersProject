using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintersProject
{
    class Printer
    {
        public int id { get; set; }
        public string printerMark { get; set; }
        public string printerType { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public string department { get; set; }

        public Printer(int id, string printerMark, string printerType,
            string name, string model, string department)
        {
            this.id = id;
            this.printerMark = printerMark;
            this.printerType = printerType;
            this.name = name;
            this.model = model;
            this.department = department;
        }

        public int getId()
        {
            return this.id;
        }
        public void setId(int id)
        {
            this.id = id;
        }
        public string getName()
        {
            return this.name;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public string getPrinterType()
        {
            return this.printerType;
        }
        public void setPrinterType(string printerType)
        {
            this.printerType = printerType;
        }
        public string getPrinterMark()
        {
            return this.printerMark;
        }
        public void setPrinterMark(string printerMark)
        {
            this.printerMark = printerMark;
        }
        public string getModel()
        {
            return this.model;
        }
        public void setModel(string model)
        {
            this.model = model;
        }
        public string getDepartment()
        {
            return this.department;
        }
        public void setDepartment(string department)
        {
            this.department = department;
        }
    }
}
