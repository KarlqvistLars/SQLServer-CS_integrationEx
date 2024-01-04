using FormUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormUI
{
    public partial class Dashboard : Form
    {
        List<Person> people = new List<Person>();

        int index = 0;

        public Dashboard()
        {
            InitializeComponent();
            peopleFoundListbox.DataSource = people;
            peopleFoundListbox.DisplayMember = "FullInfo";
        }

        // Uppdaterar formuläret
        private void UpdateBinding()
        {
            peopleFoundListbox.DataSource = people;
            peopleFoundListbox.DisplayMember = "FullInfo";

            //testTextBox.Text = "";
            firstNameInsText.Text = "";
            lastNameInsText.Text = "";
            emailAddressInsText.Text = "";
            phoneInsText.Text = "";
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();
            people = db.GetPeople(lastNameText.Text);
            UpdateBinding();
        }

        private void insertRecordButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            db.InsertPerson(firstNameInsText.Text, lastNameInsText.Text, emailAddressInsText.Text, phoneInsText.Text);

            firstNameInsText.Text = "";
            lastNameInsText.Text = "";
            emailAddressInsText.Text = "";
            phoneInsText.Text = "";
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();
            people = db.ShowAll();
            UpdateBinding();
        }

        // 
        private void peopleFoundListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            firstNameInsText.ResetText();
            //testTextBox.ResetText();
            var input = peopleFoundListbox.Text;
            input = input.Remove(input.IndexOf(" "));
            // Keeps track on the idex of the highligthed row
            index = Int32.Parse(input);
            DataAccess db = new DataAccess();
            people = db.SelectById(Int32.Parse(input));

            try
            {
                firstNameInsText.Text = people[0].FirstName;
                lastNameInsText.Text = people[0].LastName;
                emailAddressInsText.Text = people[0].EmailAddress;
                phoneInsText.Text = people[0].PhoneNumber;

            }
            catch (Exception)
            {

                //throw;
            }


        }

        private void editButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            db.EditPerson(index ,firstNameInsText.Text, lastNameInsText.Text, emailAddressInsText.Text, phoneInsText.Text);

            people = db.SelectById(index);

            UpdateBinding();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            db.DeletePerson(index);
            
            UpdateBinding();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
