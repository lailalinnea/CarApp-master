using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarApp
{
    public partial class Form1 : Form
    {
        Database dbObject = new Database();

        public Form1()
        {
            InitializeComponent();
            InitListView();
            txtRegNr.Focus();
        }


        #region EVENTS

        /*private void btnRemove_Click_Click(object sender, EventArgs e)
        {

        }*/


        /// <summary>
        /// 
        /// </summary>
        /// 
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRegNr.Text))
            {
                txtRegNr.Text = txtRegNr.Text.ToUpper();
                PrintData(txtRegNr.Text);
            }
            else
            {
                MessageBox.Show("You have to enter a regristrations number", "Input missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRegNr.Text) || string.IsNullOrEmpty(txtMake.Text))
            {
                MessageBox.Show("You have to fill in all of the boxes", "Invalid input");
            }
            else
            {
                Car car = new Car(txtRegNr.Text, txtMake.Text, txtModell.Text, Convert.ToInt32(txtYear.Text), cbxForSale.Checked);
                AddCarToListView(car);

                int result = dbObject.AddCarRow(car);
                MessageBox.Show("You have added " + Convert.ToString(result) + " number of cars");

                ClearTextboxes();
                btnClear.Enabled = true;
            }
        }

        private void AddCarToListView(Car car)
        {
            ListViewItem item = CreateListViewItem(car);
            lsvCars.Items.Add(item);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lsvCars.SelectedItems.Count > 0)
            {
                var item = lsvCars.SelectedItems[0];
                lsvCars.Items.Remove(item);
                MessageBox.Show("The car with the registration number " + item.Text + " has been removed", "removal of car");
            }
            else
            {
                MessageBox.Show("No car was marked to be removed", "removal of car");
            }
            btnClear.Enabled = (lsvCars.Items.Count > 0);
        }

        private void RemoveCarFromListView(ListViewItem listViewItem)
        {
            if (lsvCars.SelectedItems.Count > 0)
            {
                lsvCars.Items.Remove(listViewItem);
                MessageBox.Show("The car with the regristrations number " + listViewItem.Text + " has been removed", "Removal of car");
            }
            else
            {
                MessageBox.Show("No car was selected to be removed", "Removal of car");
            }
            btnClear.Enabled = (lsvCars.Items.Count > 0);
        }

        private void lsvCars_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Enabled = (lsvCars.SelectedItems.Count > 0);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lsvCars.Items.Clear();
            txtRegNr.Focus();
        }
        #endregion EVENTS

        #region HELPFUNKTIONS

        private void PrintData(string regNr)
        {
            string token = "ZYdERdMQ1BLgQ9DP6hwZpO7ScLeXcJUm";
            string call = string.Format($"https://api.biluppgifter.se/api/v1/vehicle/regno/{regNr}?api_token={token}");

            try
            {
                WebRequest request = HttpWebRequest.Create(call);

                WebResponse response = request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());

                string carJSON = reader.ReadToEnd();

                JObject jsonCar = JObject.Parse(carJSON);

                txtMake.Text = jsonCar["data"]["basic"]["data"]["make"].ToString();
                txtModell.Text = jsonCar["data"]["basic"]["data"]["model"].ToString();
                txtYear.Text = jsonCar["data"]["basic"]["data"]["model_year"].ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Car with registrations number {regNr} could not be found\n\nMassage: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ListViewItem CreateListViewItem(Car car)
        {
            ListViewItem item = new ListViewItem(car.GetRegNr());
            item.SubItems.Add(car.GetMake());
            item.SubItems.Add(car.GetForSale() ? "Yes" : "No");
            item.SubItems.Add(car.GetModel());
            item.SubItems.Add(car.GetYear().ToString());

            /*if (forSale == true)
            {
                item.SubItems.Add("Yes");
            }
            else
            {
                item.SubItems.Add("No");
            }*/

            return item;
        }

        private void ClearTextboxes()
        {
            txtRegNr.Clear();
            txtMake.Clear();
            txtModell.Clear();
            txtYear.Clear();
            cbxForSale.Checked = false;
            txtRegNr.Focus();
        }

        private void InitListView()
        {
            List<Car> listOfCars = dbObject.GetRowsFromCars();
            foreach (var item in listOfCars)
            {
                AddCarToListView(item);
            }
        }

        #endregion HELPFUNKTIONS
    }
}
