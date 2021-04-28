using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp
{
    class Database
    {
        public SQLiteConnection dbConn;
        private string databaseFilename = "./car.db";

        public Database()
        {
            if (!File.Exists(databaseFilename))
            {
                File.Create(databaseFilename);
            }
            dbConn = new SQLiteConnection("Data Source=" + databaseFilename);
        }

        public void OpenConnection()
        {
            if (dbConn.State != System.Data.ConnectionState.Open)
            {
                dbConn.Open();
            }
        }

        public void CloseConnection()
        {
            if (dbConn.State != System.Data.ConnectionState.Closed)
            {
                dbConn.Close();
            }
        }

        public int AddCarRow(Car car)
        {
            string qInsert = "INSERT INTO car ('regNr','make','model','year','forSale') VALUES (@regNr, @make, @model, @year, @forSale);";

            SQLiteCommand dbCommand = new SQLiteCommand(qInsert, dbConn);
            OpenConnection();


        }
    }
}
