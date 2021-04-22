using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.sqlite;

namespace CarApp
{
    class Car
    {
        private string regNr;
        private string make;
        private string model;
        private int year;
        private bool forSale;

        public Car(string regNr, string make, string model, int year, bool forSale)
        {
            this.regNr = regNr;
            this.make = make;
            this.model = model;
            this.year = year;
            this.forSale = forSale;
        }

        public string GetRegNr()
        {
            return regNr;
        }

        public void SetRegNr(string regNr)
        {
            this.regNr = regNr;
        }

        public string GetMake()
        {
            return make;
        }

        public void SetMake(string make)
        {
            this.make = make;
        }

        public string GetModel()
        {
            return model;
        }

        public void SetModel(string model)
        {
            this.model = model;
        }

        public int GetYear()
        {
            return year;
        }

        public void SetYear(int year)
        {
            this.year = year;
        }

        public bool GetForSale()
        {
            return forSale;
        }

        public void SetForSale(bool forSale)
        {
            this.forSale = forSale;
        }
    }
}
